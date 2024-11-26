
$(document).ready(function () {
    let filteredOrders = [...orders];
    let allOrders = [...orders];

    function renderOrders() {
        const orderTemplate = document.querySelector('#order-template').content;
        const orderList = $('#order-list');
        $('#order-list').empty();
        filteredOrders.forEach(orderItem => {
            const clone = $(orderTemplate).clone();
            clone.find('.order-image').attr('src', orderItem.Product.ImageUrl);
            clone.find('.order-date').text(orderItem.Order.OrderDate);
            clone.find('.order-state')
                .addClass(getStateClass(orderItem.Status.StatusName))
                .attr('id', `productStatus+${orderItem.OrderItemId}`)
                .text(orderItem.Status.StatusName);
            clone.find('.card-title').text(orderItem.Product.Name);
            clone.find('.description').text(orderItem.Product.Description);
            clone.find('.quantity').text(`Quantity: ${orderItem.Quantity}`);
            clone.find('.total-price').text(`Total Price: $${orderItem.TotalPrice}`);
            clone.find('.shipping-address').html(`
                                        <strong>Shipping Address:</strong><br>
                                        ${shippingAddresses[orderItem.Order.OrderId].FullName}, ${shippingAddresses[orderItem.Order.OrderId].PhoneNumber}<br>
                                        ${shippingAddresses[orderItem.Order.OrderId].Street}, ${shippingAddresses[orderItem.Order.OrderId].City}, ${shippingAddresses[orderItem.Order.OrderId].Country}
                                    `);
            orderList.append(clone);
        });
    }


    renderOrders();

    $('#status-filter').on('change', function () {
        let currentStatus = $('#status-filter').val();
        if (currentStatus === "default") {

            filteredOrders = allOrders;
            renderOrders();
        }
        else {
            filteredOrders = [];
            debugger
            allOrders.forEach(o => {
                if (o.Status.StatusName == currentStatus) {
                    debugger
                    console.log(o.Status.StatusName);
                    console.log(currentStatus);
                    filteredOrders.push(o);
                }
            });
            console.log(filteredOrders);
            renderOrders();
        }
    });


    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/orderStatusHub")
        .build();
    connection.on("ReceiveStatusUpdate", function (status, orderItemId, productName) {
        alert(`Your Order ${productName} status has been updated to: ${status}`);
        const statusSpan = document.getElementById(`productStatus+${orderItemId}`);
        statusSpan.textContent = status;
        updateStatusClass(statusSpan, status);
    });
    connection.start().catch(err => console.error(err.toString()));

    function updateStatusClass(statusSpan, status) {
        statusSpan.className = 'order-state'; // Reset classes
        statusSpan.classList.add(getStateClass(status));
    }


    function getStateClass(status) {
        switch (status) {
            case "Pending": return "state-pending";
            case "Processed": return "state-processed";
            case "Shipped": return "state-shipped";
            case "Out for Delivery": return "state-out-for-delivery";
            case "Delivered": return "state-delivered";
            case "Cancelled": return "state-cancelled";
            default: return "";
        }
    }
});