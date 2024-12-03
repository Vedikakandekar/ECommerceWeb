$(document).ready(function ()
    {
    $('#AddToWishListBtn').on('click', function () {
        $.ajax({
            url: '/Customer/Home/likes',
            type: 'GET',
            data: {
                productId: product.Id
            },
            success: function (response) {

                if (response.message == "ADDED") {
                    $(`#AddToWishListBtn`).text('Added To WishList');
                }
                else if (response.message == "NOT-LOGGED-IN") {
                    alert("Please Login first to add Product in wishlist..!!");
                }
                else if (response.message == "PRODUCT-NOT-FOUND" || response.message == "BAD-REQUEST") {
                    alert("Something Went wrong with product..Product Not found!!!");
                }
                else if (response.message == "REMOVED") {
                    $(`#AddToWishListBtn`).text('Removed From WishList');
                }
            },
            error: function () {
                alert('Failed to fetch products. Please try again later.');
            }
        });
    });

    });