#pragma checksum "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3dab5a08dca60e83cda873c68608431faae2a379"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DRF_DRFList), @"mvc.1.0.view", @"/Views/DRF/DRFList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DRF/DRFList.cshtml", typeof(AspNetCore.Views_DRF_DRFList))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3dab5a08dca60e83cda873c68608431faae2a379", @"/Views/DRF/DRFList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    public class Views_DRF_DRFList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
  
    ViewData["Title"] = "DRFList";

#line default
#line hidden
            BeginContext(135, 786, true);
            WriteLiteral(@"<!-- For Gred pages CSS  -->
<div class=""content-wrapper"">
    <section class=""content pt-3"">
        <div class=""container-fluid"">
            <div class=""row"">
                <section class=""col-lg-12"">
                    <!-- Custom tabs (Charts with tabs)-->
                    <div class=""card"">
                        <div class=""card-header"">
                            <h3 class=""card-title"">
                                <i class=""far fa-list-alt mr-2""></i> Project List
                            </h3>
                            <div class=""card-tools"">
                                <ul class=""nav nav-pills ml-auto"">
                                    <li class=""nav-item mr-3"">
                                        <a class=""btn btn-primary""");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 921, "\"", 966, 1);
#line 20 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
WriteAttributeValue("", 928, Url.Action("GanttSummary","GanttNew"), 928, 38, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(967, 428, true);
            WriteLiteral(@"><i class=""fas fa-project-diagram mr-2""></i>Project Gantt Chart</a>
                                    </li>
                                    <li class=""nav-item mr-3"">
                                        <div class=""export-btn""></div>
                                    </li>
                                    <li class=""nav-item mr-3 d-none"">
                                        <a class=""btn btn-primary""");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1395, "\"", 1430, 1);
#line 26 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
WriteAttributeValue("", 1402, Url.Action("DRFList","DRF"), 1402, 28, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1431, 253, true);
            WriteLiteral("><i class=\"fas fa-project-diagram mr-2\"></i>Project Gantt Chart</a>\r\n                                    </li>\r\n                                    <li class=\"nav-item  d-none\">\r\n                                        <a class=\"btn btn-outline-primary\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1684, "\"", 1724, 1);
#line 29 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
WriteAttributeValue("", 1691, Url.Action("DRFAddOrEdit","DRF"), 1691, 33, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1725, 526, true);
            WriteLiteral(@"><i class=""far fa-plus-square mr-1""></i>New Project</a>
                                    </li>

                                </ul>
                            </div>
                        </div>
                        <div class=""card-body"">
                                <table id=""DRFTable"" class=""table table-bordered table-striped table-hover"" style=""width:100%"">
                                    <thead>
                                        <tr>
                                            <th>");
            EndContext();
            BeginContext(2252, 27, false);
#line 39 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Id"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2279, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(2335, 37, false);
#line 40 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Project Name"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2372, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(2428, 33, false);
#line 41 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Strength"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2461, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(2517, 36, false);
#line 42 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Module Name"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2553, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(2609, 36, false);
#line 43 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Formulation"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2645, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(2701, 34, false);
#line 44 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Continent"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2735, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(2791, 32, false);
#line 45 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Country"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2823, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(2879, 42, false);
#line 46 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Registration Date"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2921, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(2977, 45, false);
#line 47 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Re Registration Date"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3022, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3078, 45, false);
#line 48 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Dossier Filling Date"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3123, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3179, 39, false);
#line 49 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Register Plant"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3218, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3274, 44, false);
#line 50 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Manufacturing Plant"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3318, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3374, 35, false);
#line 51 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Percentage"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3409, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3465, 40, false);
#line 52 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Project Manager"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3505, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3561, 39, false);
#line 53 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Project Status"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3600, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3656, 42, false);
#line 54 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Project Milestone"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3698, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3754, 42, false);
#line 55 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                           Write(SharedLocalizer["Complication Date"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3796, 353, true);
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
                BeginContext(4167, 1040, true);
                WriteLiteral(@"
    <script>
        var dataTable, getDRFList;

        $(document).ready(function () {


            var dataTable = $('#DRFTable').DataTable({
                responsive: true,
                lengthChange: true,
                autoWidth: true,
                dom: 'Bfrtip',
                columnDefs: [
                    { responsivePriority: 1, targets: -5 },
                    { responsivePriority: 1000, targets: 5 },
                    { responsivePriority: 1, targets: -3 },
                    { responsivePriority: 100, targets: -4 }
                ],

                buttons: [
                    {
                        extend: 'excel', text: '<i class=""far fa-file-excel""></i> Export In Excel ', className: ""btn-primary"", exportOptions: {
                            columns: ':visible'
                        }
 },
                    {extend: 'colvis', className:""btn-primary"" }
                 ],
               

                language: {
                   ");
                WriteLiteral(" \"emptyTable\": \"");
                EndContext();
                BeginContext(5208, 51, false);
#line 102 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                              Write(SharedLocalizer["No data available in table"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(5259, 33, true);
                WriteLiteral("\",\r\n                    \"info\": \"");
                EndContext();
                BeginContext(5293, 68, false);
#line 103 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                        Write(SharedLocalizer["Showing _START_ to _END_ of _TOTAL_ entries"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(5361, 37, true);
                WriteLiteral("\",\r\n                    \"infoEmpty\":\"");
                EndContext();
                BeginContext(5399, 52, false);
#line 104 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                            Write(SharedLocalizer["Showing 0 to 0 of 0 entries"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(5451, 40, true);
                WriteLiteral("\",\r\n                    \"infoFiltered\":\"");
                EndContext();
                BeginContext(5492, 60, false);
#line 105 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                               Write(SharedLocalizer["(filtered from _MAX_ total entries)"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(5552, 38, true);
                WriteLiteral("\",\r\n                    \"lengthMenu\":\"");
                EndContext();
                BeginContext(5591, 44, false);
#line 106 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                             Write(SharedLocalizer["Show _MENU_ entries"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(5635, 43, true);
                WriteLiteral("\",\r\n                    \"loadingRecords\": \"");
                EndContext();
                BeginContext(5679, 35, false);
#line 107 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                  Write(SharedLocalizer["Loading..."].Value);

#line default
#line hidden
                EndContext();
                BeginContext(5714, 39, true);
                WriteLiteral("\",\r\n                    \"processing\": \"");
                EndContext();
                BeginContext(5754, 38, false);
#line 108 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                              Write(SharedLocalizer["Processing..."].Value);

#line default
#line hidden
                EndContext();
                BeginContext(5792, 35, true);
                WriteLiteral("\",\r\n                    \"search\": \"");
                EndContext();
                BeginContext(5828, 31, false);
#line 109 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                          Write(SharedLocalizer["Search"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(5859, 39, true);
                WriteLiteral("\",\r\n                    \"zeroRecords\":\"");
                EndContext();
                BeginContext(5899, 50, false);
#line 110 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                              Write(SharedLocalizer["No matching records found"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(5949, 72, true);
                WriteLiteral("\",\r\n                    \"paginate\": {\r\n                        \"first\":\"");
                EndContext();
                BeginContext(6022, 30, false);
#line 112 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                            Write(SharedLocalizer["First"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(6052, 36, true);
                WriteLiteral("\",\r\n                        \"last\":\"");
                EndContext();
                BeginContext(6089, 29, false);
#line 113 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                           Write(SharedLocalizer["Last"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(6118, 37, true);
                WriteLiteral("\",\r\n                        \"next\": \"");
                EndContext();
                BeginContext(6156, 29, false);
#line 114 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                            Write(SharedLocalizer["Next"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(6185, 40, true);
                WriteLiteral("\",\r\n                        \"previous\":\"");
                EndContext();
                BeginContext(6226, 33, false);
#line 115 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                               Write(SharedLocalizer["Previous"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(6259, 269, true);
                WriteLiteral(@"""
                    },
                }

            });
            
            getDRFList = function () {
                //$('#loaderContainer').show();
                               $.ajax({
                    type: ""GET"",
                    url: '");
                EndContext();
                BeginContext(6529, 27, false);
#line 125 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                     Write(Url.Action("GetDRF", "DRF"));

#line default
#line hidden
                EndContext();
                BeginContext(6556, 441, true);
                WriteLiteral(@"/',
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",

                    success: function (response) {
                        //$('#loaderContainer').hide();
                        var jsonObject = response.data;
                        var result = jsonObject.map(function (item) {
                            var result = [];
                            //var setUrl = ");
                EndContext();
                BeginContext(6998, 35, false);
#line 134 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                      Write(Url.Action("DRFShowDetails", "DRF"));

#line default
#line hidden
                EndContext();
                BeginContext(7033, 1906, true);
                WriteLiteral(@"/+item.ProductId;
                            var setUrl = ""/DRF/DRFShowDetails?Id="" + item.ProductId;
                            //result.push(item.ProductId);
                            result.push('<a href=""' + setUrl+ '"" >'+item.ProductId+'</a>');
                            //result.push(item.ProductName);
                            result.push('<a href=""'+ setUrl+'"">'+item.ProductName+'</a>');
                            result.push(item.Strength);
                            result.push(item.ModuleName);
                            result.push(item.Formulation);
                            result.push(item.Continent);
                            result.push(item.Country);
                            result.push(item.RegistrationDate);
                            result.push(item.ReRegistrationDate);
                            result.push(item.DossierFillingDate);
                            result.push(item.RegisterPlant);
                            result.push(item.ManufacturingPla");
                WriteLiteral(@"nt);
                            result.push('<div class=""progress""><div class=""progress-bar"" role=""progressbar"" aria-valuenow=""30"" aria-valuemin=""0"" aria-valuemax=""100"" style=""width:30%"">30%</div></div>');
                            result.push(item.ProjectManager);
                            result.push(item.ProjectStatus);
                            result.push("""");
                            result.push("""");
                            

                            return result;
                        });
                        dataTable.clear();
                        dataTable.rows.add(result); // add to DataTable instance
                        dataTable.draw().buttons().container().appendTo('.export-btn'); // always redraw
                        },
                    failure: function () {
                        $(""#DRFTable"").append('");
                EndContext();
                BeginContext(8940, 78, false);
#line 164 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\DRF\DRFList.cshtml"
                                          Write(SharedLocalizer["Error when fetching data please contact administrator"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(9018, 576, true);
                WriteLiteral(@"');
                                   }
                               });

            }
            getDRFList();
            
            $("".buttons-excel"").removeClass(""btn-secondary"");
            $("".buttons-collection"").removeClass(""btn-secondary"");

        });

      

        /*$('#DRFTable').on('dblclick', ' tbody tr', function () {
            var drfTable = $('#DRFTable').DataTable();
            var data = drfTable.row(this).data();
           location.href = ""/DRF/DRFShowDetails?Id="" + data[0];
    } );*/

      


    </script>
");
                EndContext();
            }
            );
            BeginContext(9597, 2, true);
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
