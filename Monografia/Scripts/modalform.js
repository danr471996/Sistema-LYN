﻿
$(document).ready(function () {

    $.ajaxSetup({ cache: false });
    // busca los elementos el atributo data-modal y le suscribe el evento click
    $('a[data-modal]').on('click', function (e) {
        // Abre la ventana modal con el formulario solicitado 
        openmodal(this.href);
        return false;
    });
    $('i[data-modal]').on('click', function (e) {
        var idaccion = $(this).attr("id");
        var idticket = $("button.active").attr("id");
       var url = '/Facturacion/Deleteticket/?numeroticket=' + idticket;
       //window.location.href = url;
        // Abre la ventana modal con el formulario solicitado 
       openmodal(url);
        return false;
    });

    $('#modalGenerica').on('hidden.bs.modal', function () {
        $('#contentModal').html('');

    })
});
function openmodal(url) {
    // Hace una petición get y carga el formulario en la ventana modal
    $('#contentModal').load(url, function () {

        var myModal = new bootstrap.Modal(document.getElementById('modalGenerica'), {
            keyboard: true
        });
        myModal.show();
        // Suscribe el evento submit
        bindForm(this);
    });
}
function bindForm(dialog) {

    console.log(dialog)
    if (dialog == false) {
    window.location = window.location;
    }else{
    // Suscribe el formulario en la ventana modal con el evento submit
    $('form', dialog).submit(function () {
        if ($(this).valid()) {
            // Realiza una petición ajax
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    // Si la petición es satisfactoria, se recarga la página actual
                    if (result.success) {
                        window.location = window.location;
                 
                        sesionestadoproceso(true, result.mensaje);
                  
                    } else {
                        sesionestadoproceso(false, result.mensaje);
                        window.location = window.location;
                        // $('#contentModal').html(result);
                        // bindForm(false);
                    }
                }
            });

            return false;
        } else {
            return false;
        }
  
    });

        }
}
function sesionestadoproceso(estadoproceso,mensaje) {
    sessionStorage.setItem("estadoproceso", estadoproceso);
    sessionStorage.setItem("mensaje", mensaje);
}
