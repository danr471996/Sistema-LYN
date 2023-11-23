$(function () {

    $.ajaxSetup({ cache: false });

    const yourUsername = document.querySelector("#yourUsername");
    const passwordField = document.querySelector("#yourPassword");
    const eyeIcon = document.querySelector("#eye");
    var urlDelFormulario = window.location.href;

    if (eyeIcon != null || yourUsername != null || passwordField != null || urlDelFormulario.includes("ajuste_inventario") || urlDelFormulario.includes("agregar_inventario") || urlDelFormulario.includes("Venta") || urlDelFormulario.includes("Createpromocion")) {


        if (eyeIcon != null) { 
        eyeIcon.addEventListener('click', function (e) {
            // toggle the type attribute
            const type = passwordField.getAttribute('type') === 'password' ? 'text' : 'password';
            passwordField.setAttribute('type', type);
            // toggle the eye / eye slash icon
            const typeicon = eyeIcon.getAttribute('class') === 'far fa-eye fa-lg' ? 'far fa-eye-slash fa-lg' : 'far fa-eye fa-lg';
            eyeIcon.setAttribute('class', typeicon);

        });
    }
        var mensaje = document.getElementById("Mensaje").value;
        if (mensaje != "" && !(urlDelFormulario.includes("ajuste_inventario") || urlDelFormulario.includes("agregar_inventario") || urlDelFormulario.includes("Venta") || urlDelFormulario.includes("Createpromocion"))) {

            sesionestadoproceso(false, mensaje)
        }
        if (mensaje != "" && (urlDelFormulario.includes("ajuste_inventario") || urlDelFormulario.includes("agregar_inventario") || urlDelFormulario.includes("Createpromocion"))) {
            sesionestadoproceso(true, mensaje)
        }

        if (mensaje != "" && (urlDelFormulario.includes("Venta"))) {
            sesionestadoproceso("maxticket", mensaje)
        }
    }

  
    // Espera a que el modal se muestre completamente
    $('#modalGenerica').on('shown.bs.modal', function (e) {
        $(function () {
    

            ejecutascripts();
            desvinculaevent();
            $('a[data-modal]').on('click', function (e) {
                $('.modal').modal('hide');
                // Abre la ventana modal con el formulario solicitado 
                openmodal(this.href);
                return false;
            });


        $('#btndescargar').on("click", function () {
            var factura = $('#idfactura').attr("value");
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

     
        });
    });

  
});
function desvinculaevent() {
    // Desvincular eventos existentes
    $('a[data-modal]').off('click');
}
function redimensionatable(tableIds) {
    var observer = window.ResizeObserver ? new ResizeObserver(function (entries) {
        entries.forEach(function (entry) {
            $(entry.target).DataTable().columns.adjust();
        });
    }) : null;

    // Function to add a datatable to the ResizeObserver entries array
    resizeHandler = function ($table) {
        if (observer)
            observer.observe($table[0]);
    };

    // Iniciar el manejo de redimensionamiento adicional en todas las tablas
    tableIds.forEach(function (id) {
        var $table = $('#' + id);
        resizeHandler($table);
    });


}

function lettersOnly(evt, entity) {
    evt = evt || window.event;
    var charCode = evt.charCode || evt.keyCode || evt.which || 0;

    if (entity == 'P'||entity=='D') {
        if ((charCode > 31 && charCode !== 32 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122))) {
           
            return false;
        }
        }else {
            if ((charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122))) {
            
                return false;
            }
        }
    return true;

}

function ejecutascripts() {
    // Desvincula el manejador del evento 'change' antes de agregarlo nuevamente
    $(document).off('change', '#dltipocredito');
    $(document).on('change', '#dltipocredito', function () {
        $(this).find("option:selected").each(function () {

            var opcion = $(this).attr("value");

            if (opcion == 1) {
                hidediv("dvcantcredito");

            }
            if (opcion == 2) {
                var div = $('#prueba').html();
                showdiv("frmdatosclientes", div);

            }
            if (opcion == 0) {
                hidediv("dvcantcredito");
            }
        });

    });

    // Dispara el evento change manualmente
    $('#dltipocredito').trigger('change');

    // Desvincula el manejador del evento 'change' antes de agregarlo nuevamente
    $(document).off('change', '#ckinventario');
    $(document).on('change', '#ckinventario', function () {
        if ($(this).is(":checked")) {
            var div = $('#prueba').html();

            showdiv("bordered-producto", div);
        } else {
            hidediv("dvcantidades");
        }

    });

    $('#ckinventario').trigger('change');



    // Desvincula el manejador del evento 'change' antes de agregarlo nuevamente
    $(document).off('change', 'input[type="radio"][name="productos.Id_tipoventas"]');
    $(document).on('change', 'input[type="radio"][name="productos.Id_tipoventas"]', function () {

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

    if (!$('#rdpaquete').is(":checked")) {

        $('input[type="radio"][name="productos.Id_tipoventas"]').trigger('change');
    } else {

        $('#rdpaquete').trigger('change');
    }
   

    var cliente = document.getElementById("idcliente");
    if (cliente != null) {

        cliente.value ='';
    }
}
function showdiv(idelementhtml, elementhtmladd) {
     $(elementhtmladd).remove();
    $('#' + idelementhtml).append($(elementhtmladd).hide().fadeIn());

}

function hidediv(idelementhtml) {
    $('#' + idelementhtml).fadeOut("normal", function () {
        $(this).remove();
    });
}
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

function selectButton(button) {
   
    var row = button.parentNode.parentNode; // Obtener la fila actual
    var cliente = document.getElementById("idcliente");
    var table = document.getElementById("tbclientes");
    var rows = table.getElementsByTagName("tr");


    for (var i = 0; i < rows.length; i++) {
        var currentRow = rows[i];
        if (currentRow !== row) {
            var btnChecked = currentRow.getElementsByClassName("btn-checked")[0];
            var btnSelect = currentRow.getElementsByClassName("btn-select")[0];
            if (btnChecked != null) {
                btnChecked.style.display = "none";
                btnSelect.style.display = "inline-block";
            }

        }
    }
    var btnSelect = row.getElementsByClassName("btn-select")[0];
    var btnChecked = row.getElementsByClassName("btn-checked")[0];

    btnSelect.style.display = "none";
    btnChecked.style.display = "inline-block";

    cliente.value = btnSelect.value;
}

function desselectButton(button) {
    var row = button.parentNode.parentNode; // Obtener la fila actual
    var btnSelect = row.getElementsByClassName("btn-select")[0];
    var btnChecked = row.getElementsByClassName("btn-checked")[0];
    var cliente = document.getElementById("idcliente");

    btnChecked.style.display = "none";
    btnSelect.style.display = "inline-block";

    cliente.value = "";
}
function handleChange(input) {
    var inputtotalpago
    var inputmontopago
    var inputmontovuelto
    var inputtipocambiodolar
    var tipocambio

    if (input.id == "montopagodolar") {
        inputtotalpago = document.getElementById('montototaldolar');
        inputmontopago = document.getElementById('montopagodolar');
        inputmontovuelto = document.getElementById('montovueltocordobas');
        inputtipocambiodolar = document.getElementById('tipocambiodolar');
    } else {
        inputtotalpago = document.getElementById('montototalpago');
        inputmontopago = document.getElementById('montopago');
        inputmontovuelto = document.getElementById('montovuelto');
    }

    var totalpago = parseFloat(inputtotalpago.value)
    var montopago = parseFloat(inputmontopago.value)
    if (inputtipocambiodolar != null) {
        tipocambio = parseFloat(inputtipocambiodolar.value)
    }
    if (input.id == "montopagodolar") {
        calculopago("D", totalpago, montopago, tipocambio, inputmontovuelto)//D es dolares
    } else {
        calculopago("C", totalpago, montopago, tipocambio, inputmontovuelto)//C es cordobas
    }



}

function calculopago(tipopago, totalpago, montopago, tipocambio, inputmontovuelto) {

    if (tipopago == "D") {
        // Check if the input values are valid numbers
        if (!isNaN(totalpago) && !isNaN(montopago)) {
            // Perform the subtraction
            var converdolarcordobas = montopago * tipocambio;
            inputmontovuelto.value = totalpago - converdolarcordobas ;

        }
    } else {
        // Check if the input values are valid numbers
        if (!isNaN(totalpago) && !isNaN(montopago)) {
            // Perform the subtraction
            inputmontovuelto.value = totalpago - montopago;
        }
    }


}
function sesionestadoproceso(estadoproceso, mensaje) {
    sessionStorage.setItem("estadoproceso", estadoproceso);
    sessionStorage.setItem("mensaje", mensaje);
}
