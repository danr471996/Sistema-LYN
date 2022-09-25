$(document).ready(function () {
    $('#dltipocredito').change(function () {
        $(this).find("option:selected").each(function () {
            var opcion = $(this).attr("value");
            if (opcion == 1) {
                $('#dvcantcredito').fadeOut();
                console.log("entro");
            } else if (opcion == 2) {
                $('#dvcantcredito').fadeIn();
            } else {
                $('#dvcantcredito').fadeOut();
            }
        });

    }).change();


});