function openPhoto(PhotoId) {
    var path = $('#' + PhotoId).attr('src');
    $('.main-img').attr('src', path);
    $('.main-img').elevateZoom();
}
$('.main-img').elevateZoom();

$(function () {
    $("#rateYo").rateYo().on("rateyo.set", function (e, data) {
        $("#pontuacao").attr("value", data.rating);
    });
});

function carregarPontuacoes(id, valor) {
    $(function () {
        $("#" + id).rateYo({
            rating: valor,
            readOnly: true
        });
    });
}
