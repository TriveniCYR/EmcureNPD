#pragma checksum "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Reports\DRFUpdateHistoryReport.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e214f9a0c0e4a944ed4b5207e7aa9e5a7b311b6d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reports_DRFUpdateHistoryReport), @"mvc.1.0.view", @"/Views/Reports/DRFUpdateHistoryReport.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Reports/DRFUpdateHistoryReport.cshtml", typeof(AspNetCore.Views_Reports_DRFUpdateHistoryReport))]
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
#line 1 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\_ViewImports.cshtml"
using EmcureCERI.Web;

#line default
#line hidden
#line 2 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\_ViewImports.cshtml"
using EmcureCERI.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e214f9a0c0e4a944ed4b5207e7aa9e5a7b311b6d", @"/Views/Reports/DRFUpdateHistoryReport.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    public class Views_Reports_DRFUpdateHistoryReport : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EmcureCERI.Web.Models.IndexPageModel>
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
#line 4 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Reports\DRFUpdateHistoryReport.cshtml"
  
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
#line 42 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Reports\DRFUpdateHistoryReport.cshtml"
WriteAttributeValue("", 1291, Url.Action("Index","Dashboard"), 1291, 32, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1324, 902, true);
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
            BeginContext(2226, 51, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e214f9a0c0e4a944ed4b5207e7aa9e5a7b311b6d6872", async() => {
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
            BeginContext(2277, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(2299, 3681, true);
                WriteLiteral(@"
    <script>            


        var columns = [           

            {
                title: 'SrNo',
                target: 0,
                data: function (item) {
                    return item.srNo;
                }

            },
            {
                title: 'DRF No.',
                target: 1,
                data: function (item) {
                    return item.drfNo;
                }

            },
            {
                title: 'DRF',
                target: 2,
                data: function (item) {
                    return item.genericName;
                }

            },

          {
                title: 'Description',
                target: 3,
                data: function (item) {
                    return item.histroyDescription;
                }

            },

            {
                title: 'User Name',
                target: 4,
                data: function (item) {
                    return item.");
                WriteLiteral(@"userName;
                }

            },
            {
                title: 'DRF Date',
                target: 5,
                data: function (item) {
                    return formatDateToDDMMMYYYY(item.drfDate) + ' ' + formatTime(item.drfDate);
                }
            },          
            

        ];

        $(document).ready(function () {            
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
                ajax: '/Reports/GetDRFUpdateHistoryDetails");
                WriteLiteral(@"',
                treeGrid: {
                    'left': 10,
                    'expandIcon': '<span>+<\/span>',
                    'collapseIcon': '<span>-<\/span>'
                }
            });            
            table.buttons().container()
                .appendTo('.export-btn');

            $("".buttons-excel"").removeClass(""btn-secondary"");
            $("".buttons-collection"").removeClass(""btn-secondary"");

        });

        function formatDateToDDMMMYYYY(date) {
            const months = [""Jan"", ""Feb"", ""Mar"", ""Apr"", ""May"", ""Jun"", ""Jul"", ""Aug"", ""Sep"", ""Oct"", ""Nov"", ""Dec""];
            let current_datetime = new Date(date);

            var tempday = '';
            var tempmonth = '';
            if (current_datetime.getDate() < 10) {
                tempday = '0' + current_datetime.getDate();
            }
            else {
                tempday = current_datetime.getDate();
            }
            let formatted_date = tempday + ""-"" + months[current_datet");
                WriteLiteral(@"ime.getMonth()] + ""-"" + current_datetime.getFullYear();
            return formatted_date;
        }

        function formatTime(date) {
            let data = new Date(date)
            let hrs = data.getHours()
            let mins = data.getMinutes()
            let secs = data.getSeconds();
            if (hrs <= 9)
                hrs = '0' + hrs
            if (mins < 10)
                mins = '0' + mins
            if (secs < 10)
                secs = '0' + secs
            const postTime = hrs + ':' + mins + ':' + secs
            return postTime
        }

    </script>
");
                EndContext();
            }
            );
            BeginContext(5983, 2, true);
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