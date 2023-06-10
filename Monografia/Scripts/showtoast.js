$(document).ready(function () {
toastr.options = {
        "closeButton": true,
        "debug": true,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-bottom-full-width",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
}
    if (sessionStorage.getItem("estadoproceso") == "true") {
        toastr.success(sessionStorage.getItem("mensaje"));
        limpiarestadoproceso();
    }


    if (sessionStorage.getItem("estadoproceso") == "false") {
        toastr.error(sessionStorage.getItem("mensaje"))
        limpiarestadoproceso();
    }



function limpiarestadoproceso() {
    sessionStorage.removeItem("estadoproceso");
    sessionStorage.removeItem("mensaje");
}
});