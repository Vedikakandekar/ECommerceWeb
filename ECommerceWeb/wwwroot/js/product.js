
document.addEventListener("DOMContentLoaded", InitializeDatatable);

function InitializeDatatable() {


    $.ajax({
        url: '/Seller/Product/getall', // Update with your API endpoint
        method: 'get',
        dataType: 'json',
        success: function (data) {
            $('#productTable').DataTable(
                {
                    data: data,
                    columns: [
                        { data: 'id' },
                        { data: 'name' },
                        { data: 'description' },
                        { data: 'price' },
                        { data: 'category.name' },
                        { data: 'stock' },
                        { data: 'imageUrl' }
                    ]
                });
            }
        });
 

}































/*

    const table = $('#productTable').DataTable({
        "ajax": {
            url: '/Seller/Product/getall', // Update with your API endpoint
            method: 'post',
            dataType: 'json'

        },
        data: data,
        "columns": [
            { data: 'id' },
            { data: 'name' },
            { data: 'price' },
            { data: 'category.name' },
            { data: 'description' },
            { data: 'stock' },
            { data: 'imageUrl' }

        ],

        pageLength: 4, // Adjust based on cards per page
        initComplete: function () {


            console.log("Id : " + table.rows().data[]);
            updateCards(table.rows().data());
        }
    });



    // Handle pagination and sorting changes to update cards
    table.on('draw', function () {
        updateCards(table.rows({ page: 'current' }).data());
    });

    function updateCards(data) {
        $('#cardContainer').empty(); // Clear current cards
        data.each(function (row) {
            console.log("Id : " + row.category.name)
        });
        data.each(function (row) {

            const cardHtml = `<div class="col-md-4 col-sm-6">
                <div class="card Product-card h-100">
                    <div class="card-icons">
                        <a href="/Product/Upsert/${row.id}">
                     
                            <i class="bi bi-pencil-fill text-primary"></i>
                        </a>
                        <a href="/Product/Delete/${row.id}">
                            <i class="bi bi-trash3-fill text-danger"></i>
                        </a>
                    </div>
                    <img src="${row.imageUrl}?height=200&width=300" width="100%" class="card-img-top" style="border-radius:5px; border:1px solid #bbb9b9" alt="Img"/>
                        <div class="card-body">
                            <h5 class="card-title">${row.name}</h5>
                            <h6 class="card-subtitle">${row.category.name}</h6>
                            <h4 class="card-subtitle mb-2 text-body-secondary">Cost: ${row.price}</h4>
                            <h4 class="card-subtitle mb-2 text-body-secondary">In stock: ${row.stock}</h4>
                            <p class="card-text">${row.description}</p>
                        </div>
                </div>
            </div>`;

            $('#cardContainer').append(cardHtml);
        });
    }
}*/