#pragma checksum "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "98d8bf7e00d99ccab0d52d743d187fce9efeb328"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PIDFApproval_Index), @"mvc.1.0.view", @"/Views/PIDFApproval/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/PIDFApproval/Index.cshtml", typeof(AspNetCore.Views_PIDFApproval_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"98d8bf7e00d99ccab0d52d743d187fce9efeb328", @"/Views/PIDFApproval/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    public class Views_PIDFApproval_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "Pending Approval", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
  
    ViewData["Title"] = "PIDFList";

#line default
#line hidden
            BeginContext(136, 688, true);
            WriteLiteral(@"<div class=""content-wrapper"">
    <section class=""content pt-5"">
        <div class=""container-fluid"">
            <div class=""row"">
                <section class=""col-lg-12"">
                    <!-- Custom tabs (Charts with tabs)-->
                    <div class=""card"">
                        <div class=""card-header"">
                            <h3 class=""card-title"">
                                <i class=""far fa-list-alt mr-2""></i> PIDF List
                            </h3>
                            <div class=""card-tools md-left"">
                                <div class=""mybtn-group"">
                                        <a class=""btn btn-primary""");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 824, "\"", 861, 1);
#line 18 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
WriteAttributeValue("", 831, Url.Action("PIDFList","PIDF"), 831, 30, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(862, 128, true);
            WriteLiteral("><i class=\"far fa-list-alt mr-1\"></i>PIDF List</a>\r\n\r\n                                        <a class=\"btn btn-outline-primary\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 990, "\"", 1023, 1);
#line 20 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
WriteAttributeValue("", 997, Url.Action("PIDF","PIDF"), 997, 26, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1024, 807, true);
            WriteLiteral(@"><i class=""far fa-plus-square mr-1""></i> Create New Project</a>
                                        <div class=""export-btn""></div>
                                </div>
                            </div>
                        </div>
                        <div class=""card-body"">
                            <button type=""button"" onclick=""getPIDFList(0,0,20)"" class=""btn btn-outline-primary mr-3"">Pending Approval</button>
                            <button type=""button"" onclick=""getPIDFList(0,0,16)"" class=""btn btn-outline-primary mr-3"">Approved PIDF</button>
                            <button type=""button"" onclick=""getPIDFList(0,0,10)"" class=""btn btn-outline-primary mr-3"">Rejected PIDF</button>

                            <select id=""dropdown1"">
                                ");
            EndContext();
            BeginContext(1831, 23, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "98d8bf7e00d99ccab0d52d743d187fce9efeb3286662", async() => {
                BeginContext(1839, 6, true);
                WriteLiteral("Select");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1854, 34, true);
            WriteLiteral("\r\n                                ");
            EndContext();
            BeginContext(1888, 130, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "98d8bf7e00d99ccab0d52d743d187fce9efeb3287871", async() => {
                BeginContext(1921, 88, true);
                WriteLiteral("\r\n                                    Pending Approval\r\n                                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2018, 34, true);
            WriteLiteral("\r\n                                ");
            EndContext();
            BeginContext(2052, 28, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "98d8bf7e00d99ccab0d52d743d187fce9efeb3289367", async() => {
                BeginContext(2070, 1, true);
                WriteLiteral("2");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2080, 295, true);
            WriteLiteral(@"
                            </select>
                            <table id=""PIDFTable"" class=""table table-bordered table-striped table-hover"" style=""width:100%;"">
                                <thead>
                                    <tr>
                                        <th>");
            EndContext();
            BeginContext(2376, 27, false);
#line 40 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                                       Write(SharedLocalizer["Id"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2403, 51, true);
            WriteLiteral("</th>\r\n                                        <th>");
            EndContext();
            BeginContext(2455, 37, false);
#line 41 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                                       Write(SharedLocalizer["Project Name"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2492, 51, true);
            WriteLiteral("</th>\r\n                                        <th>");
            EndContext();
            BeginContext(2544, 33, false);
#line 42 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                                       Write(SharedLocalizer["Strength"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2577, 7, true);
            WriteLiteral("</th>\r\n");
            EndContext();
            BeginContext(5395, 44, true);
            WriteLiteral("                                        <th>");
            EndContext();
            BeginContext(5440, 39, false);
#line 75 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                                       Write(SharedLocalizer["Project Status"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(5479, 290, true);
            WriteLiteral(@"</th>

                                    </tr>
                                </thead>
                            </table>
                        </div>

                    </div>
                </section>
            </div>


        </div>
    </section>

</div>

");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(5787, 908, true);
                WriteLiteral(@"
    <script>

        var dataTable, getPIDFList;

        $(document).ready(function () {


            var dataTable = $('#PIDFTable').DataTable({
                responsive: true,
                lengthChange: true,
                autoWidth: true,
                columnDefs: [

                    { responsivePriority: 1, targets: -1 }
                ],

                buttons: [
                    {
                        extend: 'excel', text: '<i class=""far fa-file-excel""></i>', className: ""btn-primary"", exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'colvis', text: '<i class=""fas fa-list""></i> Select Fields', className: ""btn-primary""
                    }
                ],
                language: {
                    ""emptyTable"": """);
                EndContext();
                BeginContext(6696, 51, false);
#line 120 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                              Write(SharedLocalizer["No data available in table"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(6747, 33, true);
                WriteLiteral("\",\r\n                    \"info\": \"");
                EndContext();
                BeginContext(6781, 68, false);
#line 121 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                        Write(SharedLocalizer["Showing _START_ to _END_ of _TOTAL_ entries"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(6849, 37, true);
                WriteLiteral("\",\r\n                    \"infoEmpty\":\"");
                EndContext();
                BeginContext(6887, 52, false);
#line 122 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                            Write(SharedLocalizer["Showing 0 to 0 of 0 entries"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(6939, 40, true);
                WriteLiteral("\",\r\n                    \"infoFiltered\":\"");
                EndContext();
                BeginContext(6980, 60, false);
#line 123 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                               Write(SharedLocalizer["(filtered from _MAX_ total entries)"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(7040, 38, true);
                WriteLiteral("\",\r\n                    \"lengthMenu\":\"");
                EndContext();
                BeginContext(7079, 44, false);
#line 124 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                             Write(SharedLocalizer["Show _MENU_ entries"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(7123, 43, true);
                WriteLiteral("\",\r\n                    \"loadingRecords\": \"");
                EndContext();
                BeginContext(7167, 35, false);
#line 125 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                                  Write(SharedLocalizer["Loading..."].Value);

#line default
#line hidden
                EndContext();
                BeginContext(7202, 39, true);
                WriteLiteral("\",\r\n                    \"processing\": \"");
                EndContext();
                BeginContext(7242, 38, false);
#line 126 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                              Write(SharedLocalizer["Processing..."].Value);

#line default
#line hidden
                EndContext();
                BeginContext(7280, 35, true);
                WriteLiteral("\",\r\n                    \"search\": \"");
                EndContext();
                BeginContext(7316, 31, false);
#line 127 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                          Write(SharedLocalizer["Search"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(7347, 39, true);
                WriteLiteral("\",\r\n                    \"zeroRecords\":\"");
                EndContext();
                BeginContext(7387, 50, false);
#line 128 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                              Write(SharedLocalizer["No matching records found"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(7437, 72, true);
                WriteLiteral("\",\r\n                    \"paginate\": {\r\n                        \"first\":\"");
                EndContext();
                BeginContext(7510, 30, false);
#line 130 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                            Write(SharedLocalizer["First"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(7540, 36, true);
                WriteLiteral("\",\r\n                        \"last\":\"");
                EndContext();
                BeginContext(7577, 29, false);
#line 131 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                           Write(SharedLocalizer["Last"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(7606, 37, true);
                WriteLiteral("\",\r\n                        \"next\": \"");
                EndContext();
                BeginContext(7644, 29, false);
#line 132 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                            Write(SharedLocalizer["Next"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(7673, 40, true);
                WriteLiteral("\",\r\n                        \"previous\":\"");
                EndContext();
                BeginContext(7714, 33, false);
#line 133 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                               Write(SharedLocalizer["Previous"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(7747, 379, true);
                WriteLiteral(@"""
                    },
                }
            });

            getPIDFList = function (PidfID, userID, StatusId) {
				var data = {
					PidfID: parseInt(PidfID),
					userID: parseInt(userID),
                    StatusId:parseInt(StatusId)
				};
                console.log(data);
                  $.ajax({
                    type: ""POST"",
					url: '");
                EndContext();
                BeginContext(8127, 49, false);
#line 147 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                     Write(Url.Action("GetPIDFApprovalList", "PIDFApproval"));

#line default
#line hidden
                EndContext();
                BeginContext(8176, 3254, true);
                WriteLiteral(@"/',
					dataType: ""json"",
					data: data,
                    success: function (response) {
                        //$('#loaderContainer').hide();
						var jsonObject = response.data;
                        var result = jsonObject.map(function (item) {
                            var setUrl = ""/PIDFApproval/PIDFApproval/"" + item.PidfID;
                            var result = [];
                            //result.push(item.PidfNo);
                            //result.push(item.ProjectorProductName);

                            result.push(item.PIDFNo);
                            //result.push(item.ProductName);
                            result.push('<a href=""' + setUrl + '"">' + item.ProjectorProductName + '</a>');
                            result.push(item.Strengths);
                            //result.push(item.PatentStatus);
                            //result.push(item.ContinentName);
                            //result.push(item.CountryName);
                       ");
                WriteLiteral(@"     //result.push(item.Packing);
                            //result.push(item.BatchSize);
                            //result.push(item.PackSize);
                            //result.push(item.Cogs);
                            //result.push(item.Freight);
                            //result.push(item.TotalCIFCost);
                            //result.push(item.CIFPricePerUnit);
                            //result.push(item.CIFPricePerPack);
                            //result.push(item.ProfitPerPack);
                            //result.push(item.PercentCont);
                            //result.push(item.QtyOneyear);
                            //result.push(item.QtyTwoyear);
                            //result.push(item.QtyThreeyear);
                            //result.push(item.VolOneyear);
                            //result.push(item.VolTwoyear);
                            //result.push(item.VolThreeyear);
                            //result.push(item.Cogs1);
          ");
                WriteLiteral(@"                  //result.push(item.Cogs2);
                            //result.push(item.Cogs3);
                            //result.push(item.ContriOne);
                            //result.push(item.ContriTwo);
                            //result.push(item.ContriThree);
                            //result.push(item.ContributionThreeYear);
                            //result.push(item.CostofThreeBatches);
                            //result.push(item.RandDCost);
                            //result.push(item.FilingCost);
                            //result.push(item.StabilityCost);
                            //result.push(item.TotalInvest);
                            //result.push(item.Roi);
                            result.push(item.PidfStatus);
                            return result;
                        });
                        dataTable.clear();
                        dataTable.rows.add(result); // add to DataTable instance
                        dataTable.draw()");
                WriteLiteral(".buttons().container().appendTo(\'.export-btn\'); // always redraw\r\n                    },\r\n                    failure: function () {\r\n                        $(\"#PIDFTable\").append(\'");
                EndContext();
                BeginContext(11431, 78, false);
#line 203 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\PIDFApproval\Index.cshtml"
                                           Write(SharedLocalizer["Error when fetching data please contact administrator"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(11509, 703, true);
                WriteLiteral(@"');
                    }
                });

            }

            getPIDFList(0,0,20);
            $('#dropdown1').on('change', function () {
                alert(""Hii"");
                getPIDFList.columns(37).search(this.value).draw();
            });
            $("".buttons-excel"").removeClass(""btn-secondary"");
            $("".buttons-collection"").removeClass(""btn-secondary"");
        });



    //   $('#PIDFTable').on('click', ' tbody tr', function () {
    //        var drfTable = $('#PIDFTable').DataTable();
    //        var data = drfTable.row(this).data();
    //       location.href = ""/DRF/DRFShowDetails?Id="" + data[0];
    //} );




    </script>
");
                EndContext();
            }
            );
            BeginContext(12215, 2, true);
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
