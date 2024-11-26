$(document).ready(function () {

    console.log(cartItemList);
    console.log(addressList);
    function updateSummary() {
        var subtotal = 0;
        for (let i = 0; i < cartItemList.length; i++) {
            const item = cartItemList[i];
            let itemTotal = parseFloat($(`#product-totalprice-${item.ProductId}`).text().replace("Total: $ ", ""));
            subtotal += itemTotal;
            const shippingFees = parseFloat($('#shipping-fees').text().replace("$", ""));
            $('#subtotal').text("$" + subtotal.toFixed(2));
            $('#total').text("$" + (subtotal + shippingFees).toFixed(2));
        }
    }
    for (let i = 0; i < cartItemList.length; i++) {
        const item = cartItemList[i];
        console.log(i + "    " + item);
        $(`#product-quantity-increment-${item.ProductId}`).on('click', function () {
            let subtotal = 0;
            let quantity = parseInt($(`#product-quantity-${item.ProductId}`).val(), 10);
            let price = parseFloat($(`#product-price-${item.ProductId}`).text());
            if (isNaN(quantity)) {
                quantity = 0;
            }
            quantity += 1;
            subtotal = price * quantity;
            $(`#product-quantity-${item.ProductId}`).text(quantity);
            $(`#product-quantity-${item.ProductId}`).val(quantity);
            $(`#product-totalprice-${item.ProductId}`).text("Total: $ " + subtotal);
            $(`#quantityInput-${item.ProductId}`).val(quantity);
            updateSummary();
        });
        $(`#product-quantity-decrement-${item.ProductId}`).on('click', function () {
            let subtotal = 0;
            let quantity = parseInt($(`#product-quantity-${item.ProductId}`).val(), 10);
            let price = parseFloat($(`#product-price-${item.ProductId}`).text());
            if (!isNaN(quantity) && quantity > 1) {
                quantity -= 1;
                subtotal = price * quantity;
                $(`#product-quantity-${item.ProductId}`).text(quantity);
                $(`#product-quantity-${item.ProductId}`).val(quantity);
                $(`#product-totalprice-${item.ProductId}`).text("Total: $ " + subtotal);
                $(`#quantityInput-${item.ProductId}`).val(quantity);
                updateSummary();
            }
        });
    }

    function getSelectedAddress() {
        for (let i = 0; i < addressList.length; i++) {
            const item = addressList[i];
            if ($(`#addressCard-${item.AddressId}`).hasClass('selected')) {
                return {
                    id: item.AddressId,
                    name: item.FullName,
                    address: item.Street
                };
            }
        }
        return null;
    }

    for (let i = 0; i < addressList.length; i++) {
        const item = addressList[i];
        $(`#addressCard-${item.AddressId}`).on('click', function () {
            $('.address-card').removeClass('selected');
            $(this).addClass('selected');
            $(`#addressCard-${item.AddressId}`).addClass("selected");
            $('#AddressId').val(item.AddressId);
            $('#FullName').val(item.FullName);
            $('#PhoneNumber').val(item.PhoneNumber);
            $('#Street').val(item.Street);
            $('#City').val(item.City);
            $('#State').val(item.City);
            $('#Country').val(item.Country);
            $('#ZipCode').val(item.ZipCode);
        });
    }
    $('#addNewAddressBtn').on('click', function () {
        debugger
        $('#shippingOverlay').css('display', 'flex');
    });
    $('#cancelBtn').on('click', function () {
        $('#shippingOverlay').css('display', 'none');
    });
    $('#AddressForm').on('submit', function (e) {
        e.preventDefault();
        const addressData = {
            FullName: $('#fullName').val(),
            Street: $('#address').val(),
            City: $('#city').val(),
            State: $('#state').val(),
            Country: $('#country').val(),
            ZipCode: $('#zipCode').val(),
            PhoneNumber: $('#phonenumber').val(),
            AddressId: $('#AddressId').val(),
            CustomerId: $('#CustomerId').val()
        };
        $.ajax({
            url: '/Customer/Cart/AddAddress',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(addressData),
            success: function (data) {
                if (data.success) {
                    location.reload();
                    $('#shippingOverlay').css('display', 'none');
                    $('#AddressForm')[0].reset();
                } else {
                    alert('Error: Could not add address');
                }
            },
            error: function (error) {
                console.error('Error:', error);
                alert('There was an error adding the address.');
            }
        });
        $('#shippingOverlay').css('display', 'none');
        $('#AddressForm')[0].reset();
    });

    $('#OrderForm').on('submit', function () {
        const selectedAddress = getSelectedAddress();
        if (selectedAddress) {
            alert(`Proceeding to payment with address:\nName: ${selectedAddress.name}\nAddress: ${selectedAddress.address}`);
            OrderForm.submit();
        }
        else {
            alert('Please select a shipping address');
        }
    });
});