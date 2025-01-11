const { forEach } = require("core-js/core/array");


(function ($) {
    "use strict";
    let THEME = {};

    /*====== Example ======*/
    THEME.Example = function () {
        // Write your code here
    };
    /*====== end Example ======*/

    $(window).on("load", function () { });
    $(document).ready(function () {
        THEME.Example();
    });
})(jQuery);

function fillPageId(pageId) {
    $("#Page").val(pageId);
    $("#filter-search").submit();
}

function changeProductColor(productColorId) {

    fetch(`/Home/ChangeProductColor/${productColorId}`)
        .then(res => res.json())
        .then(data => {

            document.getElementById("product-price-two").innerText = data.price;
            document.getElementById("product-price-one").innerText = data.price;

            var colorItems = document.getElementsByClassName('order-color-id');

            for (colorItem of colorItems) {
                colorItem.value = data.id;
            }

        })
}

function showCreateProductCommentModal(productId) {

    fetch(`/product/CreateProductComment/${productId}`)
        .then(res => res.text())
        .then(data => {

            $("#large-modal-title").html("افزودن نظر به محصول");
            $("#large-modal-body").html(data);
            $("#large-modal").modal('show');

        })
}

function onSuccessCreateProductComment(result) {

    if (result && result.status == 100) {
        Swal.fire({
            title: "عملیات موفق",
            text: result.message,
            icon: "success"
        });
    } else {
        Swal.fire({
            title: "خطا",
            text: result.message,
            icon: "error"
        });
    }

    setTimeout(() => {
        $("#large-modal").modal('hide');
        location.reload();
    }, 2000)

}

function increaseProductQuantity(orderDetailId) {
    fetch(`/order/IncreaseProductQuantity/${orderDetailId}`)
        .then(res => res.json())
        .then(data => {

            location.reload();

        })
}


function decreaseProductQuantity(orderDetailId) {
    fetch(`/order/DecreaseProductQuantity/${orderDetailId}`)
        .then(res => res.json())
        .then(data => {

            location.reload();

        })
}

function deleteOrderDetails(orderDetailId) {
    fetch(`/order/DeleteOrderDetails/${orderDetailId}`)
        .then(res => res.json())
        .then(data => {

            location.reload();

        })
}


