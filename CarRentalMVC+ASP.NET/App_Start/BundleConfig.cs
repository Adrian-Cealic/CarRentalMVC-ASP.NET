using System.Web;
using System.Web.Optimization;

namespace CarRentalMVC_ASP.NET
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //script bundles
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new Bundle("~/bundles/counterup").Include(
                      "~/Content/lib/counterup/counterup.min.js"
            ));

            bundles.Add(new Bundle("~/bundles/easing").Include(
                       "~/Content/lib/easing/easing.js"
            ));

            bundles.Add(new Bundle("~/bundles/lightbox/css").Include(
                       "~/Content/lib/lightbox/css/*.css"
            ));
            bundles.Add(new Bundle("~/bundles/lightbox/script").Include(
           "~/Content/lib/lightbox/js/*.js"
            ));


            bundles.Add(new Bundle("~/bundles/owlcarouselscript").Include(
            "~/Content/lib/owlcarousel/owl.carousel.js"
            ));

            bundles.Add(new Bundle("~/bundles/waypoints").Include(
                        "~/Content/lib/waypoints/waypoints.min.js"
            ));

            bundles.Add(new Bundle("~/bundles/wow").Include(
                        "~/Content/lib/wow/wow.min.js"
            ));

            bundles.Add(new Bundle("~/bundles/main").Include(
                        "~/Scripts/main.js"
            ));

            //style bundles
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Styles/styles.css", new CssRewriteUrlTransform()
            ));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/Styles/bootstrap.min.css"
            ));

            bundles.Add(new Bundle("~/bundles/animate").Include(
            "~/Content/lib/animate/animate.css"
            ));
            bundles.Add(new Bundle("~/bundles/owlcarouselcss").Include(
            "~/Content/lib/owlcarousel/assets/*.css"
            ));


        }
    }
}
