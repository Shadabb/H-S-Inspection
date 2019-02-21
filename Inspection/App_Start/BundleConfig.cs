using System.Web.Optimization;

namespace Inspection
{
    public static class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/scripts-bootstrap").Include(
                "~/Scripts/jquery-2.2.0.min.js",
                "~/Scripts/jquery-ui.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/grids.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/scripts-site").Include(
                "~/Scripts/wessex.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/css-bootstrap").Include(
                "~/Content/jquery-ui.css",
                "~/Content/bootstrap.min.css",
                "~/Content/fontawesome-all.min.css"
            ));

            bundles.Add(new StyleBundle("~/Content/css-site").Include(
                "~/Content/wessex.css"
            ));
        }
    }
}
