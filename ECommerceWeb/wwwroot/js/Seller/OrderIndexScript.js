function updateOrderStatus(button, orderItemId) {
    const select = button.previousElementSibling;
    const newStatus = select.value;

    if (newStatus === 'Update status') {
        alert('Please select a valid status.');
        return;
    }

    const url = `/seller/order/updatestatus?orderItemId=${encodeURIComponent(orderItemId)}&status=${encodeURIComponent(newStatus)}`;

    fetch(url, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    })
        .then(response => {
            if (!response.ok) throw new Error('Failed to update status');
            return response.json();
        })
        .then(data => {
            if (data.success) {
                const statusBadge = button.parentElement.querySelector('.status-badge');
                statusBadge.textContent = newStatus;

                statusBadge.className = 'badge status-badge';
                switch (newStatus) {
                    case 'Pending':
                        statusBadge.classList.add('bg-warning');
                        break;
                    case 'Processed':
                        statusBadge.classList.add('bg-info');
                        break;
                    case 'Shipped':
                        statusBadge.classList.add('bg-primary');
                        break;
                    case 'Out for Delivery':
                    case 'Delivered':
                        statusBadge.classList.add('bg-success');
                        break;
                    case 'Cancelled':
                        statusBadge.classList.add('bg-danger');
                        break;
                }
                alert('Status updated successfully!');
            } else {
                alert('Failed to update status.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('An error occurred while updating status.');
        });
}