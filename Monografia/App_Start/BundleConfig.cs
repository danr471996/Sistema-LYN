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
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/eventformularios.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/ Content/tooltip.css"));

            bundles.Add(new StyleBundle("~/Content/fontawesome/css").Include(
                      "~/Content/fontawesome/css/all.css"));


            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(

                        "~/Scripts/Datatable/jquery.dataTables.min.js",

                         "~/Scripts/Datatable/dataTables.bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/datatables").Include(

                     "~/Scripts/Datatable/dataTables.bootstrap.min.css",
                     "~/Scripts/Datatable/jquery.dataTables.min.css"));


        }
    }
}
