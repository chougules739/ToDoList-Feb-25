﻿using System.Web.Optimization;

namespace TodoList
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/TodoContent/css").Include(
                      "~/TodoContent/CSS/main.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapJS").Include(
                "~/Content/JS/jquery.min.js",
                "~/Content/JS/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrapCSS").Include(
                      "~/Content/bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUiJS").Include(
                "~/Content/JS/jquery-1.11.1.min.js",
                "~/Content/JS/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/TodoListBundles/jqueryUiJS").Include(
                "~/TodoContent/JS/taskManager.js"));

            bundles.Add(new StyleBundle("~/bundles/jqueryUiCSS").Include(
                      "~/Content/jquery-ui.css"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusiveJS").Include(
                "~/Scripts/jquery-3.3.1.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js"));
        }
    }
}