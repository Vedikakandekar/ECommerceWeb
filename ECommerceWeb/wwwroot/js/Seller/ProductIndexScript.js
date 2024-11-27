
let filteredProducts = [...ProductList];
let allProducts = [...ProductList];

$(document).ready(function () {
    renderProducts()
    $('#applyBtn').on('click', function () {
        let searchTerm = $('#searchInput').val();
        let category = $('#category-filter').val();
        let price = $('#price-filter').val();

        if (searchTerm.trim() != '' || category != "-Select Category-" || price != "-Sort by Price-") {
            $(this).hide();
            $('#resetBtn').show();
            $('#price-filter').prop('disabled', true);
            $('#category-filter').prop('disabled', true);
            $('#searchInput').prop('disabled', true);
            if (category === "-Select Category-") {
                category = "";
            }
            if (price === "-Sort by Price-") {
                price = "";
            }
         
            $.ajax({
                url: '/Seller/Product/SearchSellerProduct',
                type: 'GET',
                data: {
                    searchString: searchTerm,
                    categoryFilter: category,
                    priceFilter: price
                },
                datatype: 'json',
                success: function (data) {
                    if (data.success) {

                        console.log(data);

                        filteredProducts = data.products;
                        renderProducts();
                    }
                },
                error: function (jqXHR, textStatus, err) {
                    console.log("AJX Error : ", textStatus, err)
                }

            });
           
        }
    });

    $('#resetBtn').on('click',function () {
        $(this).hide();
        $('#searchInput').val('');
        $('#applyBtn').show();
        $('#price-filter').prop('disabled', false);
        $('#category-filter').prop('disabled', false);
        $('#searchInput').prop('disabled', false);
        $('#price-filter').prop('selectedIndex', 0);
        $('#category-filter').prop('selectedIndex', 0);
        filteredProducts = allProducts;
        renderProducts();
    });
});
function renderProducts() {
    $('#product-container').empty();
    const template = $('#product-template').html();
    filteredProducts.forEach(product => {
        let productHtml = $(template);
        productHtml.find('#edit-product').attr('href', `/seller/product/Upsert?id=${product.id}`);
        productHtml.find('#delete-product').attr('href', `/seller/product/delete?id=${product.id}`);
        productHtml.find('#product-img').attr('src', `${product.imageUrl}`);
        productHtml.find('#product-name').text(product.name);
        productHtml.find('#product-category').text(product.category.name);
        productHtml.find('#product-price').text(product.price);
        productHtml.find('#product-stock').text(product.stock);
        $('#product-container').append(productHtml);
    });
}
