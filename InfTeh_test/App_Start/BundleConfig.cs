using System.Web;
using System.Web.Optimization;

namespace InfTeh_test
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // ------------------------------------------------------------------- //

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.cookie.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom-js").Include(
                        "~/Scripts/custom/Preloader.js",
                        "~/Scripts/custom/ObjectSelection.js",
                        "~/Scripts/custom/SideMenuCookieOpen.js",
                        "~/Scripts/select2.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/dropzone-js").Include(
                "~/Scripts/jquery.filedrop.js"));

            // ------------------------------------------------------------------- //

            bundles.Add(new StyleBundle("~/bundles/css/bootstrap").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-grid.css",
                "~/Content/bootstrap-reboot.css",
                "~/Content/font-awesome.min.css",
                "~/Content/bootstrap.css.map"));

            bundles.Add(new StyleBundle("~/bundles/css/custom-css").Include(
                "~/Content/css/select2.min.css",
                "~/Content/css/global.css"));

            // ------------------------------------------------------------------- //
        }
    }
}
