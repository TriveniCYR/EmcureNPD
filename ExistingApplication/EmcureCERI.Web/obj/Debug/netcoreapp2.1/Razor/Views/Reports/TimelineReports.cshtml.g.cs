#pragma checksum "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Reports\TimelineReports.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "596526026962caf0d7e16acbe37b8eb90f9fbd04"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reports_TimelineReports), @"mvc.1.0.view", @"/Views/Reports/TimelineReports.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Reports/TimelineReports.cshtml", typeof(AspNetCore.Views_Reports_TimelineReports))]
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
#line 1 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\_ViewImports.cshtml"
using EmcureCERI.Web;

#line default
#line hidden
#line 2 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\_ViewImports.cshtml"
using EmcureCERI.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"596526026962caf0d7e16acbe37b8eb90f9fbd04", @"/Views/Reports/TimelineReports.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    public class Views_Reports_TimelineReports : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EmcureCERI.Web.Models.IndexPageModel>
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
#line 4 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Reports\TimelineReports.cshtml"
  
    ViewData["Title"] = "Reports View";


#line default
#line hidden
            BeginContext(189, 1098, true);
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
                                <i class=""fas fa-list mr-2""></i> Report List
                            </h3>
                            <div class=""card-tools md-left"">
                                <div class=""myb");
            WriteLiteral("tn-group\">\r\n                                    <a class=\"btn btn-primary\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1287, "\"", 1326, 1);
#line 42 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Reports\TimelineReports.cshtml"
WriteAttributeValue("", 1294, Url.Action("Index","Dashboard"), 1294, 32, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1327, 695, true);
            WriteLiteral(@"><i class=""fas fa-undo mr-1""></i>Back</a>

                                    <div class=""export-btn mybtn-group""></div>
                                </div>
                            </div>
                        </div>

                        <div class=""card-body report-scroll"">
                            <div class=""row"">
                                <div class=""col-lg-4 col-md-12 zindexupper"">
                                    <div class=""row"">
                                        <div class=""col-md-6 col-sm-12 col-xs-12 form-group"">
                                            <label for=""country"">DRF</label>
                                            ");
            EndContext();
            BeginContext(2023, 237, false);
#line 55 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Reports\TimelineReports.cshtml"
                                       Write(Html.DropDownListFor(m => m.CommonVModel.InitializationID, new SelectList(string.Empty, "Value", "Text"), "Select", htmlAttributes: new { @class = "form-control input-sm f-s-15 bg-eff4f55c w100per molecule_filter ", id = "cmbMolecule" }));

#line default
#line hidden
            EndContext();
            BeginContext(2260, 46, true);
            WriteLiteral("\r\n                                            ");
            EndContext();
            BeginContext(2307, 99, false);
#line 56 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Reports\TimelineReports.cshtml"
                                       Write(Html.ValidationMessageFor(m => m.CommonVModel.InitializationID, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(2406, 273, true);
            WriteLiteral(@"
                                        </div>

                                        <div class=""col-md-6 col-sm-12 col-xs-12 form-group"">
                                            <label for=""molecule"">Status</label>
                                            ");
            EndContext();
            BeginContext(2680, 225, false);
#line 61 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Reports\TimelineReports.cshtml"
                                       Write(Html.DropDownListFor(m => m.CommonVModel.StatusID, new SelectList(string.Empty, "Value", "Text"), "Select", htmlAttributes: new { @class = "form-control input-sm f-s-15 bg-eff4f55c w100per country_filter", id = "cmbStatus" }));

#line default
#line hidden
            EndContext();
            BeginContext(2905, 46, true);
            WriteLiteral("\r\n                                            ");
            EndContext();
            BeginContext(2952, 91, false);
#line 62 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Reports\TimelineReports.cshtml"
                                       Write(Html.ValidationMessageFor(m => m.CommonVModel.StatusID, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(3043, 761, true);
            WriteLiteral(@"
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=""row"">
                                <div class=""col-md-12"">
                                    <div class=""table-responsive match-filter"">
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
            BeginContext(3804, 51, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "596526026962caf0d7e16acbe37b8eb90f9fbd049684", async() => {
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
            BeginContext(3855, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(3877, 145, true);
                WriteLiteral("\r\n    <script>\r\n\r\n        var defaultValue = \"<option value=\'\'>Select</option>\";\r\n        $.ajax({\r\n            type: \"POST\",\r\n            url: \'");
                EndContext();
                BeginContext(4023, 47, false);
#line 89 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Reports\TimelineReports.cshtml"
             Write(Url.Action("GetDropdownsForReports", "Reports"));

#line default
#line hidden
                EndContext();
                BeginContext(4070, 15983, true);
                WriteLiteral(@"/',
            //contentType: ""application/json; charset=utf-8"",
            dataType: ""json"",
            //data: data,
            success: function (response) {
                //debugger
                //console.log(response);

                $('#cmbStatus').empty();
                $('#cmbStatus').empty().append(
                    defaultValue
                );
                $.each(response.data.statusList, function (i, List) {
                    $(""#cmbStatus"").append('<option value=""' + List.statusID + '"">' +
                        List.status + '</option>');
                });

                $('#cmbMolecule').empty();
                $('#cmbMolecule').empty().append(
                    defaultValue
                );
                $.each(response.data.moleculeList, function (i, List) {
                    $(""#cmbMolecule"").append('<option value=""' + List.initializationID + '"">' +
                        List.genericName + '</option>');
                });
     ");
                WriteLiteral(@"       },
            failure: function () {
                $('#cmbStatus').empty().append(
                    defaultValue
                );
                $('#cmbStatus').val("""");

                $('#cmbMolecule').empty().append(
                    defaultValue
                );
                $('#cmbMolecule').val("""");
            }
        });


        var columns = [
            //{

            //    title: '',
            //    target: 0,
            //    className: 'treegrid-control',
            //    data: function (item) {
            //        if (item.srNo) {
            //            return '<span>+<\/span>';
            //        }
            //        return '';
            //    }

            //},

            {
                title: 'SrNo',
                target: 1,
                data: function (item) {
                    return item.srNo;
                }

            },
            {
                title: 'DRF No.',
               ");
                WriteLiteral(@" target: 2,
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
                target: 7");
                WriteLiteral(@",
                data: function (item) {
                    return item.strength_Name;
                }
            },
            //{
            //    title: 'In-House OR Third-Party',
            //    target: 8,
            //    data: function (item) {
            //        return item.partner;
            //    }
            //},
            {
                title: 'Status',
                target: 8,
                data: function (item) {
                    return item.status;
                }
            },
            {
                title: 'Created Date',
                target: 9,
                data: function (item) {
                    return item.createdDate;
                }
            },
            {
                title: 'DRF CreatedBy',
                target: 10,
                data: function (item) {
                    if (item.drfCreatedDate.includes('-')) {
                        return '0 Days,0 Hours';
                    }
           ");
                WriteLiteral(@"         else {
                        return item.drfCreatedDate;
                    }
                }
            },

            {
                title: 'Country Manager Approved',
                target: 11,
                data: function (item) {
                    return item.cmnNameApprovedDate;


                }
            },
              {
                title: 'Country Manager Timeline',
                target: 12,
                data: function (item) {
                    //return item.cM_Approved_DayHours;
                    if (item.cM_Approved_DayHours != null) {
                        if (item.cM_Approved_DayHours.includes('-')) {
                            return '0 Days,0 Hours';
                        }
                        else {
                            return item.cM_Approved_DayHours;
                        }
                    }
                    else {
                        return item.cM_Approved_DayHours;
                    }");
                WriteLiteral(@"
                }
            },

            {
                title: 'Reginal Manager Approved',
                target: 13,
                data: function (item) {
                    return item.lmApprovedDate;
                }
            },

              {
                  title: 'Reginal Manager Timeline',
                target: 14,
                data: function (item) {
                   // return item.lM_Approved_DayHours;
                    if (item.lM_Approved_DayHours != null) {
                        if (item.lM_Approved_DayHours.includes('-')) {
                            return '0 Days,0 Hours';
                        }
                        else {
                            return item.lM_Approved_DayHours;
                        }
                    }
                    else {
                        return item.lM_Approved_DayHours;
                    }
                }
            },

            {
                title: 'HOD Approved',
  ");
                WriteLiteral(@"              target: 15,
                data: function (item) {
                    return item.hodApprovedDate;
                }
            },

              {
                title: 'HOD Approved Timeline',
                target: 16,
                data: function (item) {
                    //return item.hoD_Approved_DayHours;

                    if (item.hoD_Approved_DayHours != null) {
                        if (item.hoD_Approved_DayHours.includes('-')) {
                            return '0 Days,0 Hours';
                        }
                        else {
                            return item.hoD_Approved_DayHours;
                        }
                    }
                    else {
                        return item.hoD_Approved_DayHours;
                    }
                }
            },

            {
                title: 'IP Approved',
                target: 17,
                data: function (item) {
                    return item.iP_App");
                WriteLiteral(@"roved;
                }
            },

              {
                title: 'IP Approved Timeline',
                target: 18,
                data: function (item) {
                    //return item.after1stApproved_IPDept_DayHourse;

                    if (item.after1stApproved_IPDept_DayHourse != null) {
                        if (item.after1stApproved_IPDept_DayHourse.includes('-')) {
                            return '0 Days,0 Hours';
                        }
                        else {
                            return item.after1stApproved_IPDept_DayHourse;
                        }
                    }
                    else {
                        return item.after1stApproved_IPDept_DayHourse;
                    }
                }
            },

            {
                title: 'Manufacturing Approved',
                target: 19,
                data: function (item) {
                    return item.manufacturing_Approved;



               ");
                WriteLiteral(@" }
            },

              {
                title: 'Manufacturing Approved Timeline',
                target: 20,
                data: function (item) {
                    //return item.after1stApproved_ManufacturingDept_DayHourse;

                    if (item.after1stApproved_ManufacturingDept_DayHourse != null) {
                        if (item.after1stApproved_ManufacturingDept_DayHourse.includes('-')) {
                            return '0 Days,0 Hours';
                        }
                        else {
                            return item.after1stApproved_ManufacturingDept_DayHourse;
                        }
                    }
                    else {
                        return item.after1stApproved_ManufacturingDept_DayHourse;
                    }
                }
            },

            {
                title: 'SCM Approved',
                target: 21,
                data: function (item) {
                    return item.scM_Approved;");
                WriteLiteral(@"
                }
            },
            {
                title: 'SCM Approved Timeline',
                target: 22,
                data: function (item) {
                    //return item.after1stApproved_SCMDept_DayHourse;

                    if (item.after1stApproved_SCMDept_DayHourse != null) {
                        if (item.after1stApproved_SCMDept_DayHourse.includes('-')) {
                            return '0 Days,0 Hours';
                        }
                        else {
                            return item.after1stApproved_SCMDept_DayHourse;
                        }
                    }
                    else {
                        return item.after1stApproved_SCMDept_DayHourse;
                    }
                }
            },
            {
                title: 'RA Approved',
                target: 23,
                data: function (item) {
                    return item.rA_Approved;
                }
            },
            {");
                WriteLiteral(@"
                title: 'RA Approved Timeline',
                target: 24,
                data: function (item) {
                    //return item.after1stApproved_RADept_DayHourse;

                    if (item.after1stApproved_RADept_DayHourse != null) {
                        if (item.after1stApproved_RADept_DayHourse.includes('-')) {
                            return '0 Days,0 Hours';
                        }
                        else {
                            return item.after1stApproved_RADept_DayHourse;
                        }
                    }
                    else {
                        return item.after1stApproved_RADept_DayHourse;
                    }
                }
            },


            {
                title: 'Medical Approved',
                target: 25,
                data: function (item) {
                    return item.medical_Approved;
                }
            },
            {
                title: 'Medical Approved T");
                WriteLiteral(@"imeline',
                target: 26,
                data: function (item) {
                    //return item.after1stApproved_MedicalDept_DayHourse;
                    if (item.after1stApproved_MedicalDept_DayHourse != null) {
                        if (item.after1stApproved_MedicalDept_DayHourse.includes('-')) {
                            return '0 Days,0 Hours';
                        }
                        else {
                            return item.after1stApproved_MedicalDept_DayHourse;
                        }
                    }
                    else {
                        return item.after1stApproved_MedicalDept_DayHourse;
                    }
                }
            },
            {
                title: 'Finance Approved',
                target: 27,
                data: function (item) {
                    return item.finance_Approved;
                }
            },
            {
                title: 'Finance Approved Timeline',
        ");
                WriteLiteral(@"        target: 28,
                data: function (item) {
                    //return item.after1stApproved_FinanceialDept_DayHourse;
                    if (item.after1stApproved_FinanceialDept_DayHourse != null) {
                        if (item.after1stApproved_FinanceialDept_DayHourse.includes('-')) {
                            return '0 Days,0 Hours';
                        }
                        else {
                            return item.after1stApproved_FinanceialDept_DayHourse;
                        }
                    }
                    else {
                        return item.after1stApproved_FinanceialDept_DayHourse;
                    }
                }
            }

        ];

        $(document).ready(function () {
            //var StatusId = 19;
            // var regEx = ""^"" + StatusId + ""$"";
            var table = $('#TimelineMoleculeTable').DataTable({
                columns: columns,
                dom: 'Bfrtip',
                buttons");
                WriteLiteral(@": [
                    {
                        extend: 'excel', text: '<i class=""far fa-file-excel""></i> Export In Excel ', className: ""btn-primary"", exportOptions: {
                            columns: ':visible'
                        }
                    },
                    { extend: 'colvis', className: ""btn-primary"" }
                ],

                lengthChange: false,
                //columnDefs: [
                //    {
                //        visible: false,
                //        targets: 8
                //    }

                //],
                //initComplete: function () {
                //    this
                //        .api()
                //        .column(8)
                //        .search(regEx, true, false,true)
                //        .draw()
                //},
                ajax: '/Reports/GetReportTimeline',
                treeGrid: {
                    'left': 10,
                    'expandIcon': '<span>+<\/span>',
  ");
                WriteLiteral(@"                  'collapseIcon': '<span>-<\/span>'
                }

            });

            $('.molecule_filter').on('change', function () {
                var term = $('#cmbMolecule').find("":selected"").text();

               // alert(term);
                if (term == ""Please Select Option"") {

                    table.columns(2).search("""", true, false, true).draw();
                }
                else {
                    var regExp = ""^"" + term + ""$"";

                    table.columns(2).search(regExp, true, false, true).draw();
                }

            });

            $('.country_filter').on('change', function () {
                var term = $('#cmbStatus').find("":selected"").text();
                // alert(term);
                if (term == ""Please Select Option"") {

                    table.columns(7).search("""", true, false, true).draw();
                }
                else {
                    var regExp = ""(^|,)"" + term + ""(,|$)"";

           ");
                WriteLiteral(@"         table.columns(7).search(regExp, true, false, true).draw();
                }

            });
            table.buttons().container()
                .appendTo('.export-btn');

            //$('.status_filter').on('click', function () {
            //    var term = $(this).data('status');
            //    var regExp = ""^"" + term + ""$"";
            //    table.columns(8).search(regExp, true, false,true).draw();

            //});

            $("".buttons-excel"").removeClass(""btn-secondary"");
            $("".buttons-collection"").removeClass(""btn-secondary"");

        });


    </script>
");
                EndContext();
            }
            );
            BeginContext(20056, 2, true);
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
