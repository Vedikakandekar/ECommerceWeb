$(document).ready(function () {


    const itemsPerPage = 12;
    let currentPage = 1;
    let filteredProducts = [...products];
    let AllProducts = [...products];
    function renderProducts(page) {

        const start = (page - 1) * itemsPerPage;
        const end = start + itemsPerPage;
        const pageProducts = filteredProducts.slice(start, end);
        const $container = $('#products-container');
        const $template = $('#product-template').html();
        $('#products-container').empty();

        $.each(pageProducts, function (index, product) {
            const $clone = $($template);

            const $img = $clone.find('img');
            $img.attr('src', product.imageUrl.replace(/\\/g, '/'));
            $img.attr('alt', product.name);
            $img.attr('id', `product-image-${product.id}`);

            const $name = $clone.find('.card-title');
            $name.text(product.name || 'Unnamed Product');
            $name.attr('id', `product-name-${product.id}`);

            const $price = $clone.find('span');
            const priceValue = product.price;
            $price.text(priceValue.toLocaleString('en-US', { style: 'currency', currency: 'USD' }));
            $price.attr('id', `product-price-${product.id}`);

            const $detailsButton = $clone.find('a');
            $detailsButton.attr('href', `/Customer/Home/Details?productId=${product.id}`);
            $detailsButton.attr('id', `details-button-${product.id}`);

            $container.append($clone);
        });

        renderPagination();
    }
    function renderPagination() {
        const totalPages = Math.ceil(filteredProducts.length / itemsPerPage);
        $('#pagination').empty();

        for (let i = 1; i <= totalPages; i++) {
            $('#pagination').append(`
                                    <li class="page-item ${i === currentPage ? 'active' : ''}">
                                        <a class="page-link" href="#" data-page="${i}">${i}</a>
                                    </li>
                                `);
        }
    }


    $(document).on('click', '.pagination .page-link', function (e) {
        e.preventDefault();
        currentPage = parseInt($(this).data('page'));
        renderProducts(currentPage);
    });
    renderProducts(currentPage);

    $('#searchBtn').on('click', function () {
        let searchTearm = $('#searchInput').val();
        let category = $('#category-filter').val();
        let price = $('#price-filter').val();

        if (searchTearm.trim() != '' || category != "-Select Category-" || price != "-Sort by Price-") {
            debugger
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
                url: '/customer/home/SearchProduct',
                type: 'GET',
                data: {
                    searchString: searchTearm,
                    categoryFilter: category,
                    priceFilter: price
                },
                datatype: 'json',
                success: function (data) {
                    if (data.success) {

                        console.log(data);

                        filteredProducts = data.products;
                        renderProducts(currentPage);
                    }

                },
                error: function (jqXHR, textStatus, err) {
                    console.log("AJX Error : ", textStatus, err)
                }

            });
        }

    });

    $('#resetBtn').click(function () {
        $(this).hide();
        $('#searchInput').val('');
        $('#searchBtn').show();
        $('#price-filter').prop('disabled', false);
        $('#category-filter').prop('disabled', false);
        $('#searchInput').prop('disabled', false);
        $('#price-filter').prop('selectedIndex', 0);
        $('#category-filter').prop('selectedIndex', 0);
        filteredProducts = AllProducts;
        renderProducts(currentPage);
    });

});