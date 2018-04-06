using System.Web;
using System.Web.Optimization;

namespace MinhaEmpresa.Vendas.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/ko").Include(
                        "~/Scripts/knockout-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                        "~/Scripts/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app/navigation").Include(
                        "~/ScriptsApp/Navigation/Navigation.js").Include(
                        "~/ScriptsApp/Navigation/RouteTable.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerydatatables")
                .Include("~/Scripts/DataTables/jquery.dataTables.min.js")
                .Include("~/Scripts/DataTables/dataTables.bootstrap.min.js")
            );

            bundles.Add(new ScriptBundle("~/bundles/select2")
                .Include("~/Scripts/select2.min.js")
            );

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/toastr.min.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.min.css",
                      "~/Content/css/select2.min.css",
                      "~/Content/site.css"));
        }
    }
}
