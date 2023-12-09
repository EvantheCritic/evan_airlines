// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function addToCart(flightJson) {
    $.ajax({
        type: "POST",
        url: "/Flight/AddToCart",
        contentType: "application/json",
        data: JSON.stringify(JSON.parse(flightJson)),
        success: function (data) {
            alert("Flight added to cart!");
        },
        error: function (error) {
            console.error("Error adding flight to cart:", error);
        }
    });
}