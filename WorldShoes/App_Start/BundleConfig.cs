using System.Web;
using System.Web.Optimization;

namespace WorldShoes
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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/respond.js"));


            bundles.Add(new ScriptBundle("~/bundles/sitejs").Include(
                     "~/Scripts/price-range.js",
                     "~/Scripts/jquery.scrollUp.min.js",
                     "~/Scripts/bootstrap.js",
                     "~/Scripts/jquery.prettyPhoto.js",
                     "~/Scripts/main.js",
                     "~/Scripts/contact.js",
                     "~/Scripts/gmaps.js",
                     "~/Scripts/html5shiv.js",
                     "~/Scripts/jquery.elevatezoom.js",
                      "~/Scripts/jquery.zoom.js",
                       "~/Scripts/jquery.rateyo.js",
                      "~/Scripts/MyScript.js",
                      "~/Scripts/RequisicoesAjax.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/adminjs").Include(
                        "~/Scripts/jquery.min.js",
                         "~/Scripts/bootstrap.js",
                        "~/Scripts/metisMenu.min.js",
                        "~/Scripts/startmin.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                         "~/Scripts/jquery.maskedinput.js"
                        ));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                     "~/Content/animate.css",
                     "~/Content/font-awesome.min.css",
                     "~/Content/main.css",
                     "~/Content/prettyPhoto.css",
                     "~/Content/price-range.css",
                     "~/Content/responsive.css",
                     "~/Content/jquery.rateyo.css"
                ));

            //bundles.Add(new StyleBundle("~/Content/admin").Include(
            //          "~/Content/metisMenu.min.css",
            //          "~/Content/timeline.css",
            //          "~/Content/startmin.css",
            //          "~/Content/morris.css",
            //          "~/Content/admin.css"
            //          ));

            bundles.Add(new StyleBundle("~/Content/admin").Include(
                      "~/Content/metisMenu.min.css",
                      "~/Content/timeline.css",
                      "~/Content/startmin.css",
                      "~/Content/morris.css"
                      ));


        }
    }
}
