using System.Web;
using System.Web.Optimization;

namespace Monografia
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                         "~/Scripts/jquery-{version}.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modalform").Include(
                        "~/Scripts/modalform.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new Bundle("~/bundles/jqueryformularios").Include(
                    "~/Scripts/eventformularios.js"));

            bundles.Add(new Bundle("~/bundles/jquerycustomproyecto").Include(
                      "~/Content/vendor/apexcharts/apexcharts.min.js",
                      "~/Scripts/bootstrap.bundle.min.js",
                      "~/Scripts/vendor/chartjs/chart.min.js",
                      "~/Content/vendor/echarts/echarts.min.js",
                      "~/Content/vendor/quill/quill.min.js",
                      "~/Content/vendor/phpemailform/validate.js",
                       "~/Content/vendor/js/main.js"));



            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
              "~/Content/Estilos.css",
                      "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/csscustomproyecto").Include(
                     "~/Content/vendor/bootstrap-icons/bootstrap-icons.css",
                      "~/Content/vendor/boxicons/css/boxicons.min.css",
                       "~/Content/vendor/quill/quill.snow.css",
                        "~/Content/vendor/quill/quill.bubble.css",
                         "~/Content/vendor/remixicon/remixicon.css",
                          "~/Content/vendor/css/style.css"));

            bundles.Add(new StyleBundle("~/Content/fontawesome/css").Include(
                      "~/Content/fontawesome/css/all.css"));


            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(

                        "~/Scripts/Datatable/jquery.dataTables.min.js",
                         "~/Scripts/Datatable/dataTables.bootstrap5.min.js",
                         "~/Scripts/Datatable/dataTables.select.min.js"));
           

            bundles.Add(new StyleBundle("~/Scripts/Datatable").Include(
                     "~/Scripts/Datatable/dataTables.bootstrap5.min.css",
                     "~/Scripts/Datatable/select.bootstrap5.min.css"));

            
        }
    }
}
