﻿using System.Web;
using System.Web.Optimization;

namespace NobatDehi
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/popper.js"));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                                  "~/Scripts/select2.js",
                                  "~/Scripts/Select2Custom.js"));
            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                                  "~/Scripts/datepicker/persianDatepicker.js",
                                  "~/Scripts/datepicker/prism.js",
                                  "~/Scripts/datepicker/vertical-responsive-menu.min.js",
                                  "~/Scripts/datepicker/datePickerCustom.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/css/select2.css",
                      "~/Content/site.css",
                      "~/Content/datepicker/*.css"));

            
        }
    }
}
