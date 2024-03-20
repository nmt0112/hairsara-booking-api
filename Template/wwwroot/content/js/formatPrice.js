// formatPrice.js

// Định dạng giá tiền khi tải xong trang
document.addEventListener('DOMContentLoaded', function () {
    var priceElements = document.getElementsByClassName('price');
    for (var i = 0; i < priceElements.length; i++) {
        var price = parseFloat(priceElements[i].getAttribute('data-price'));
        var formattedPrice = price.toLocaleString('vi-VN') + ' VNĐ';
        priceElements[i].textContent = formattedPrice;
    }
});
