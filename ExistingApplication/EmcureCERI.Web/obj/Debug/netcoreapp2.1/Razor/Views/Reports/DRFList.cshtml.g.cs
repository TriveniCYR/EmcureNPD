#pragma checksum "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\Reports\DRFList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d6d0e0722faf4b50f04a87f17a076db4ddf35942"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reports_DRFList), @"mvc.1.0.view", @"/Views/Reports/DRFList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Reports/DRFList.cshtml", typeof(AspNetCore.Views_Reports_DRFList))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\_ViewImports.cshtml"
using EmcureCERI.Web;

#line default
#line hidden
#line 2 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\_ViewImports.cshtml"
using EmcureCERI.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d6d0e0722faf4b50f04a87f17a076db4ddf35942", @"/Views/Reports/DRFList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    public class Views_Reports_DRFList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EmcureCERI.Web.Models.IndexPageModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/dataTables.treeGrid.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.SingleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(137, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\Reports\DRFList.cshtml"
  
    ViewData["Title"] = "Reports View";


#line default
#line hidden
            BeginContext(189, 1095, true);
            WriteLiteral(@"
<style>
    .treegrid-control span {
        cursor: pointer;
        padding: 0px 5px;
        display: block;
        box-shadow: 0 0 1px #999;
        width: 20px;
        height: 25px;
    }

    .treegrid-control-open span {
        cursor: pointer;
        padding: 0px 7px;
        display: block;
        box-shadow: 0 0 1px #999;
        width: 20px;
        height: 25px;
    }
</style>

<div class=""content-wrapper"">
    <section class=""content pt-3"">
        <div class=""container-fluid"">
            <div class=""row"">
                <div class=""col-lg-12"">
                    <!-- Custom tabs (Charts with tabs)-->
                    <div class=""card"">
                        <div class=""card-header"">
                            <h3 class=""card-title"">
                                <i class=""fas fa-list mr-2""></i> DRF List
                            </h3>
                            <div class=""card-tools md-left"">
                                <div class=""mybtn-");
            WriteLiteral("group\">\r\n                                    <a class=\"btn btn-primary\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1284, "\"", 1323, 1);
#line 42 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\Reports\DRFList.cshtml"
WriteAttributeValue("", 1291, Url.Action("Index","Dashboard"), 1291, 32, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1324, 874, true);
            WriteLiteral(@"><i class=""fas fa-undo mr-1""></i>Back</a>
                                    <div class=""export-btn mybtn-group""></div>
                                </div>
                            </div>
                        </div>

                        <div class=""card-body report-scroll"">
                            <div class=""row"">
                                <div class=""col-md-12"">
                                    <div class=""table-responsive"">
                                        <table id=""TimelineMoleculeTable"" class=""table table-bordered"" cellspacing=""0"" width=""100%""></table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</div>
");
            EndContext();
            BeginContext(2198, 51, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d6d0e0722faf4b50f04a87f17a076db4ddf359426534", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2249, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(2271, 3244, true);
                WriteLiteral(@"
    <script>



        var columns = [

            {
                title: 'SrNo',
                target: 1,
                data: function (item) {
                    return item.srNo;
                }

            },
            {
                title: 'DRF No.',
                target: 2,
                data: function (item) {
                    return item.drfNo;
                }

            },
            {
                title: 'DRF',
                target: 3,
                data: function (item) {
                    return item.genericName;
                }

            },

          {
                title: 'Region',
                target: 4,
                data: function (item) {
                    return item.region;
                }

            },

            {
                title: 'Country',
                target: 5,
                data: function (item) {
                    return item.country;
                }

         ");
                WriteLiteral(@"   },
            {
                title: 'Pharmaceutical<br>Form',
                target: 6,
                data: function (item) {
                    return item.pharmaceutical_Form;
                }
            },
            {
                title: 'Strength',
                target: 7,
                data: function (item) {
                    return item.strength_Name;
                }
            },
            {
                title: 'Status',
                target: 8,
                data: function (item) {
                    return item.status;
                }
            },
            {
                title: 'View History',
                target: 9,
                data: function (item) {
                    return '<a class=""btn btn-primary btn-sm"" href=""/Reports/DRFUpdateHistoryReport?DRFID=' + item.initializationID + '"" data-toggle=""tooltip"" title=""View Update History"" ><i class=""fas fa-eye""></i></a>';
                }
            },


        ];
");
                WriteLiteral(@"
        $(document).ready(function () {
            //var StatusId = 19;
            // var regEx = ""^"" + StatusId + ""$"";
            var table = $('#TimelineMoleculeTable').DataTable({
                columns: columns,
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'excel', text: '<i class=""far fa-file-excel""></i> Export In Excel ', className: ""btn-primary"", exportOptions: {
                            columns: ':visible'
                        }
                    },
                    { extend: 'colvis', className: ""btn-primary"" }
                ],

                lengthChange: false,
                ajax: '/Reports/GetReportTimeline',
                treeGrid: {
                    'left': 10,
                    'expandIcon': '<span>+<\/span>',
                    'collapseIcon': '<span>-<\/span>'
                }

            });
            table.buttons().container()
                .appendTo('.export-btn");
                WriteLiteral("\');\r\n\r\n            $(\".buttons-excel\").removeClass(\"btn-secondary\");\r\n            $(\".buttons-collection\").removeClass(\"btn-secondary\");\r\n\r\n        });\r\n\r\n\r\n    </script>\r\n");
                EndContext();
            }
            );
            BeginContext(5518, 2, true);
            WriteLiteral("\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.Extensions.Localization.IStringLocalizer<SharedResource> SharedLocalizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EmcureCERI.Web.Models.IndexPageModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
