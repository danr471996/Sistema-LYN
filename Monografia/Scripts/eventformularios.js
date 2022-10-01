$(document).ready(function () {
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

});