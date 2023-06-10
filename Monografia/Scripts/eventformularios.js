$(document).ready(function () {

    $.ajaxSetup({ cache: false });

    const yourUsername = document.querySelector("#yourUsername");
    const passwordField = document.querySelector("#yourPassword");
    const eyeIcon = document.querySelector("#eye");


    if (eyeIcon != null || yourUsername != null || passwordField != null) {
        $('#btningreso').on("click", function () {
            var datoslogin = {
                Login: yourUsername.value,
                Contraseña: passwordField.value
            }
            //form encoded data
            var dataType = 'application/json; charset=utf-8';
            $.ajax({
                type: "POST",
                url: '/usuariologin/Login',
                dataType: 'json',
                contentType: dataType,
                data: JSON.stringify(datoslogin),
                success: function (result) {
                    console.log("termino peticion ajax")
                    console.log(result)
                    if (result.success) {
                        console.log("entro a redirigir")
                        window.location.href = '/Usuariologin/Paginainicio'
                    } else {
                        if (result.existeusuario == false) {
                            console.log("entro usuario no existe")
                            sesionestadoproceso(result.success, result.mensaje);
                            toastsuccess();
                            // window.location.href = '/Usuariologin/Login'
                        } else {
                            sesionestadoproceso(result.success, result.mensaje);
                            toasterror();
                        }


                    }
                }
            });

        });


        eyeIcon.addEventListener('click', function (e) {
            // toggle the type attribute
            const type = passwordField.getAttribute('type') === 'password' ? 'text' : 'password';
            passwordField.setAttribute('type', type);
            // toggle the eye / eye slash icon
            const typeicon = eyeIcon.getAttribute('class') === 'bi bi-eye' ? 'bi bi-eye-slash' : 'bi bi-eye';
            eyeIcon.setAttribute('class', typeicon);

        });
    }

  
           function sesionestadoproceso(estadoproceso, mensaje) {
               sessionStorage.setItem("estadoproceso", estadoproceso);
               sessionStorage.setItem("mensaje", mensaje);
           }



    $('#dltipocredito').change(function () {
        $(this).find("option:selected").each(function () {
            var opcion = $(this).attr("value");
            if (opcion == 1) {
                hidediv("dvcantcredito");

            } else if (opcion == 2) {
                var div = $('#prueba').html();
                showdiv("frmdatosclientes",div);
   
            } else {
                hidediv("dvcantcredito");
            }
        });

    }).change();


    $('#ckinventario').change(function () {
        if ($(this).is(":checked")) {
            var div = $('#prueba').html();

            showdiv("bordered-producto", div);
        } else {
            hidediv("dvcantidades");
        }

    });
    $('input[type="radio"][name="productos.Id_tipoventas"]').change(function () {
        
     if ($('#rdpaquete').is(":checked")) {

            var li = $('#prueba2').html();

            showdiv("borderedTab", li);

            var div = $('#prueba3').html();

            showdiv("borderedTabContent", div);
        } else {

            hidediv("paquete");
            hidediv("bordered-paquete");
        }
    });

  

    function showdiv(idelementhtml, elementhtmladd) {
        $('#' + idelementhtml).append($(elementhtmladd).hide().fadeIn());

    }

    function hidediv(idelementhtml) {
        $('#' + idelementhtml).fadeOut("normal", function () {
            $(this).remove();
        });
    }




    $('#btndescargar').on("click", function () {
        var factura = $('#idfactura').attr("value");
        console.log(factura);
            $.ajax({
                url: '/Facturacion/downloadpdf',
                type: "POST",
                data: { idfactura: factura },
                success: function (data, jqXHR, response) {
                    if (jqXHR == "success") {
                        var bytes = _base64ToArrayBuffer(data.message);
                        saveByteArray(data.filename, bytes);
                        window.location = window.location;
                    }
                }
            });
        
    })

    function _base64ToArrayBuffer(base64) {
        var binary_string = window.atob(base64);
        var len = binary_string.length;
        var bytes = new Uint8Array(len);
        for (var i = 0; i < len; i++) {
            bytes[i] = binary_string.charCodeAt(i);
        }
        return bytes.buffer;
    }

    function saveByteArray(reportName, byte) {
        var blob = new Blob([byte], { type: "application/octetstream" });
        var isIE = false || !!document.documentMode;
        if (isIE) {
            window.navigator.msSaveBlob(blob, reportName);
        } else {
            var url = window.URL || window.webkitURL;
            link = url.createObjectURL(blob);
            var a = $("<a />");
            a.attr("download", reportName);
            a.attr("href", link);
            $("body").append(a);
            a[0].click();
            $("body").remove(a);
        }
    };
});