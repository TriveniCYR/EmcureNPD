#pragma checksum "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Masters\DRFEnableDisabledList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "167938222334641d87ce54a98d2fdf095719bac2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Masters_DRFEnableDisabledList), @"mvc.1.0.view", @"/Views/Masters/DRFEnableDisabledList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Masters/DRFEnableDisabledList.cshtml", typeof(AspNetCore.Views_Masters_DRFEnableDisabledList))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"167938222334641d87ce54a98d2fdf095719bac2", @"/Views/Masters/DRFEnableDisabledList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    public class Views_Masters_DRFEnableDisabledList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "POST", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onsubmit", new global::Microsoft.AspNetCore.Html.HtmlString("return SubmitDRFForm(this)"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/dataTables.treeGrid.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.SingleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(92, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Masters\DRFEnableDisabledList.cshtml"
  
    ViewData["Title"] = "Reports View";


#line default
#line hidden
            BeginContext(144, 1095, true);
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
            BeginWriteAttribute("href", " href=\"", 1239, "\"", 1278, 1);
#line 41 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Masters\DRFEnableDisabledList.cshtml"
WriteAttributeValue("", 1246, Url.Action("Index","Dashboard"), 1246, 32, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1279, 874, true);
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
            BeginContext(2167, 65, true);
            WriteLiteral("<div id=\"AddModel\" class=\"modal themeModal \" role=\"dialog\">\r\n    ");
            EndContext();
            BeginContext(2232, 891, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "167938222334641d87ce54a98d2fdf095719bac27926", async() => {
                BeginContext(2290, 826, true);
                WriteLiteral(@"
        <!-- Small boxes (Stat box) -->
        <!-- Main row -->
        <div class=""modal-dialog"">
            <!-- Modal content-->
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <h4 id=""modelHeaderName"" class=""modal-title""></h4>
                    <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
                </div>
                <div class=""modal-body"">
                    <p id=""modelMessageDescription""></p>
                </div>
                <div class=""card-footer text-center"">
                    <button type=""submit"" class=""btn btn-primary "">Yes</button>
                    <a class=""btn btn-danger"" onclick=""HideAddModel()"">No</a>

                </div>


            </div>
        </div>
    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3123, 10, true);
            WriteLiteral("\r\n</div>\r\n");
            EndContext();
            BeginContext(3133, 51, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "167938222334641d87ce54a98d2fdf095719bac210523", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3184, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(3206, 4991, true);
                WriteLiteral(@"
    <script>
        var isdrfenabled;
        var drfid;
        var temptrue = ""true"";
        var tempfalse = ""false"";
        function ShowAddModel(data,tempdrfid) {
            
            drfid = tempdrfid;
            if (data === true) {
                //alert(data);
                isdrfenabled = ""true"";                
                var temphead = document.getElementById(""modelHeaderName"");
                temphead.innerHTML = ""Active DRF"";

                var tempdesc = document.getElementById(""modelMessageDescription"");
                tempdesc.innerHTML = ""Are you sure to Active DRF?"";
            }
            else {
                isdrfenabled = ""false"";
                var temphead = document.getElementById(""modelHeaderName"");
                temphead.innerHTML = ""In Active DRF"";
                var tempdesc = document.getElementById(""modelMessageDescription"");
                tempdesc.innerHTML = ""Are you sure to In Active DRF?"";
            }
           // ale");
                WriteLiteral(@"rt(isdrfenabled);
            $('#AddModel').modal('show');
        }

        function HideAddModel() {
            $('#AddModel').modal('hide');
        }

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
                title: 'Countr");
                WriteLiteral(@"y',
                target: 5,
                data: function (item) {
                    return item.country;
                }

            },
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
                title: 'Action',
                target: 9,
                data: function (item) {
                    if (item.isActive === 'false') {
                        return '<a  class=""btn btn-primary btn-sm"" onclick=""Sho");
                WriteLiteral(@"wAddModel(' + true + ',' + item.initializationID +')"" data-toggle=""tooltip"" title=""Active"" ><i class=""fas fa-eye""></i></a>';
                    }
                    else {
                        return '<a class=""btn btn-primary btn-sm"" onclick=""ShowAddModel(' + false + ',' + item.initializationID +')"" data-toggle=""tooltip"" title=""In Active"" ><i class=""fas fa-eye""></i></a>';
                    }

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

");
                WriteLiteral(@"                lengthChange: false,
                ajax: '/Masters/GetAllDRFList',
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

        function SubmitDRFForm(form) {
            var url = ""/Masters/GetAllDRFList"";
         $.validator.unobtrusive.parse(form);
        if ($(form).valid()) {
            $('#loading').show();
            var tempdata = { DRFID: drfid, isDRFActive: isdrfenabled };
                $.ajax({
                    type : ""POST"",
                    url : '");
                EndContext();
                BeginContext(8198, 49, false);
#line 244 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Masters\DRFEnableDisabledList.cshtml"
                      Write(Url.Action("UpdateDRFEnabledDisabled", "Masters"));

#line default
#line hidden
                EndContext();
                BeginContext(8247, 385, true);
                WriteLiteral(@"/',//form.action,
                    data: tempdata,
                    success: function (result) {
                        //console.log(result);
                        $('#loading').hide();
                         $('#AddModel').modal('hide');
                        if (result.data === ""success"") {
                            openCommonModal('successModal modal-sm', '");
                EndContext();
                BeginContext(8633, 42, false);
#line 251 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Masters\DRFEnableDisabledList.cshtml"
                                                                 Write(Html.Raw(SharedLocalizer["Success"].Value));

#line default
#line hidden
                EndContext();
                BeginContext(8675, 352, true);
                WriteLiteral(@"', 'Record has been updated successfully.', true);
                            /* setTimeout(function () { location.href = url; }, 2000);*/
                            setTimeout(function () { location.reload(); }, 2000);
                        }
                        else {
                            openCommonModal('alertModal modal-sm', '");
                EndContext();
                BeginContext(9028, 30, false);
#line 256 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Masters\DRFEnableDisabledList.cshtml"
                                                               Write(SharedLocalizer["Error"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(9058, 165, true);
                WriteLiteral("\', result.message, false);\r\n                        }\r\n                    }\r\n                });\r\n            }\r\n            return false;\r\n    }\r\n\r\n    </script>\r\n");
                EndContext();
            }
            );
            BeginContext(9226, 2, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
