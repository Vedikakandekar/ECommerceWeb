
$(document).ready(function () {
    
    let currentPage = 1; 
    
    const pageSize = 8; 

    function fetchProducts() {
        const search = $('#searchInput').val().trim();
        const category = $('#category-filter').val();
        const priceOrder = $('#price-filter').val();
        
        $.ajax({
            url: '/Customer/Home/GetProducts',
            type: 'GET',
            data: {
                search: search,
                category: category,
                priceOrder: priceOrder,
                page: currentPage,
                pageSize: pageSize
            },
            success: function (response) {
              
                renderProducts(response.products);
                renderPagination(response.currentPage, response.totalPages);
            },
            error: function () {
                alert('Failed to fetch products. Please try again later.');
            }
        });
    }

  
    function renderProducts(products) {
        
        const $container = $('#products-container');
        const $template = $('#product-template').html();
        $container.empty();
        if (products.length === 0) {
            $container.append('<p class="text-center">No products found.</p>');
            return;
        }
        $.each(products, function (index, product) {
            const $clone = $($template);

            const $img = $clone.find('img');
            $img.attr('src', product.imageUrl.replace(/\\/g, '/'));
            $img.attr('alt', product.name);
            $img.attr('id', `product-image-${product.id}`);

            const $like = $clone.find('i');
            $like.attr('id', `like-${product.id}`);
            const ProductID = product;
            const isLiked = product.IsLiked;
            debugger
            if (product.isLiked) {
                $like.addClass('likedIt');
            }
            else {
                $like.removeClass('likedIt');
            }
           
            const $name = $clone.find('.card-title');
            const truncatedName = product.name.length > 20 ? product.name.substring(0, 20) + '...' : product.name;
            $name.text(truncatedName || 'Unnamed Product');
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
     
        debugger
        $.each(products, function (index, product) {
            $(`#like-${product.id}`).on('click', function () {
                debugger
                $.ajax({
                    url: '/Customer/Home/likes',
                    type: 'GET',
                    data: {
                        productId: product.id
                    },
                    success: function (response) {

                        if (response.message == "ADDED") {
                            $(`#like-${product.id}`).addClass('likedIt');
                        }
                        else if (response.message == "NOT-LOGGED-IN") {
                            alert("Please Login first to add Product in wishlist..!!");
                        }
                        else if (response.message == "PRODUCT-NOT-FOUND" || response.message == "BAD-REQUEST") {
                            alert("Something Went wrong with product..Product Not found!!!");
                        }
                        else if (response.message == "REMOVED") {
                            $(`#like-${product.id}`).removeClass('likedIt');
                        }
                    },
                    error: function () {
                        alert('Failed to fetch products. Please try again later.');
                    }
                });
            })
        });
    }

  
    function renderPagination(currentPage, totalPages) {
        const $pagination = $('.pagination');
        $pagination.empty();

        if (currentPage > 1) {
            $pagination.append(`
                <li class="page-item">
                    <a class="page-link" href="#" data-page="${currentPage - 1}">Previous</a>
                </li>
            `);
        }

        for (let i = 1; i <= totalPages; i++) {
            $pagination.append(`
                <li class="page-item ${i === currentPage ? 'active' : ''}">
                    <a class="page-link" href="#" data-page="${i}">${i}</a>
                </li>
            `);
        }

        if (currentPage < totalPages) {
            $pagination.append(`
                <li class="page-item">
                    <a class="page-link" href="#" data-page="${currentPage + 1}">Next</a>
                </li>
            `);
        }
    }

    $(document).on('click', '.pagination .page-link', function (e) {
        e.preventDefault();
        currentPage = parseInt($(this).data('page'));
        fetchProducts();
    });

    $('#searchBtn').on('click', function () {

        if ($('#searchInput').val().trim() != '' || $('#category-filter').val() != "-Select Category-" || $('#price-filter').val() != "-Sort by Price-") {
            $(this).hide();
            $('#resetBtn').show();
            $('#price-filter').prop('disabled', true);
            $('#category-filter').prop('disabled', true);
            $('#searchInput').prop('disabled', true);
            currentPage = 1;
            fetchProducts();
        }
    });

    $('#resetBtn').on('click', function () {
        $(this).hide();
        $('#searchInput').val('');
        $('#searchBtn').show();
        $('#price-filter').prop('disabled', false);
        $('#category-filter').prop('disabled', false);
        $('#searchInput').prop('disabled', false);
        $('#price-filter').prop('selectedIndex', 0);
        $('#category-filter').prop('selectedIndex', 0);
        currentPage = 1; 
        fetchProducts();
    });

    fetchProducts();
});




















/*$(document).ready(function () {


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

});*/