#pragma checksum "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7bf815d3a5a7c657bf95a343087090b5380c8875"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DRFInitialization_ProjectList), @"mvc.1.0.view", @"/Views/DRFInitialization/ProjectList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DRFInitialization/ProjectList.cshtml", typeof(AspNetCore.Views_DRFInitialization_ProjectList))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7bf815d3a5a7c657bf95a343087090b5380c8875", @"/Views/DRFInitialization/ProjectList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    public class Views_DRFInitialization_ProjectList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
  
    ViewData["Title"] = "ProjectList";

#line default
#line hidden
            BeginContext(139, 903, true);
            WriteLiteral(@"<style>
	table.dataTable.dtr-inline.collapsed > tbody > tr[role=""row""] > td.dtr-control:before, table.dataTable.dtr-inline.collapsed > tbody > tr[role=""row""] > th.dtr-control:before {
		top: 24px;
	}
</style>

<div class=""content-wrapper"">
    <section class=""content pt-3"">
        <div class=""container-fluid"">
            <div class=""row"">
                <section class=""col-lg-12"">
                    <!-- Custom tabs (Charts with tabs)-->
                    <div class=""card"">
                        <div class=""card-header"">
                            <h3 class=""card-title"">
                                <i class=""far fa-copy mr-2""></i> Dossier List
                            </h3>
                            <div class=""card-tools md-left"">
                                <div class=""mybtn-group"">
                                        <a class=""btn btn-primary""");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1042, "\"", 1087, 1);
#line 24 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
WriteAttributeValue("", 1049, Url.Action("GanttSummary","GanttNew"), 1049, 38, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1088, 173, true);
            WriteLiteral("><i class=\"fas fa-project-diagram mr-2\"></i>Dossier Gantt Chart</a>\r\n                                    \r\n                                        <a class=\"btn btn-primary\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1261, "\"", 1314, 1);
#line 26 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
WriteAttributeValue("", 1268, Url.Action("ProjectList","DRFInitialization"), 1268, 46, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1315, 155, true);
            WriteLiteral("><i class=\"far fa-copy mr-1\"></i>Dossier List</a>\r\n                                    \r\n                                        <a class=\"btn btn-primary\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1470, "\"", 1517, 1);
#line 28 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
WriteAttributeValue("", 1477, Url.Action("Index","DRFInitialization"), 1477, 40, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1518, 150, true);
            WriteLiteral("><i class=\"far fa-list-alt mr-1\"></i>DRF</a>\r\n                                    \r\n                                        <a class=\"btn btn-primary\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1668, "\"", 1727, 1);
#line 30 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
WriteAttributeValue("", 1675, Url.Action("DRFInitialization","DRFInitialization"), 1675, 52, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1728, 747, true);
            WriteLiteral(@"><i class=""far fa-plus-square mr-1""></i> Create New DRF</a>
                                    
                                        <div class=""export-btn mybtn-group""></div>
                                    
                                </div>
                            </div>
                        </div>
                        
                        <div class=""card-body"">
                            <div class=""table-responsive"">
                                <table id=""DRFTable"" class=""table table-bordered table-striped table-hover dt-responsive nowrap"" width=""100%"">
                                    <thead>
                                        <tr>
                                            <th>");
            EndContext();
            BeginContext(2476, 27, false);
#line 43 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Id"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2503, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(2559, 35, false);
#line 44 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Dossier No"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2594, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(2650, 37, false);
#line 45 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Project Name"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2687, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(2743, 32, false);
#line 46 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Country"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2775, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(2831, 37, false);
#line 47 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Generic Name"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2868, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(2924, 35, false);
#line 48 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Brand Name"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2959, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3015, 40, false);
#line 49 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Order Frequency"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3055, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3111, 33, false);
#line 50 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Strength"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3144, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3200, 34, false);
#line 51 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Pack Size"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3234, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3290, 35, false);
#line 52 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Pack Style"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3325, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3381, 29, false);
#line 53 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Form"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3410, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3466, 30, false);
#line 54 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Plant"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3496, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3552, 34, false);
#line 55 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Incoterms"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3586, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3642, 34, false);
#line 56 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["MA Holder"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3676, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3732, 43, false);
#line 57 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Dossier to be sent"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3775, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3831, 40, false);
#line 58 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Project Manager"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3871, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(3927, 41, false);
#line 59 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Dossier Template"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3968, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(4024, 31, false);
#line 60 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Status"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(4055, 55, true);
            WriteLiteral("</th>\r\n                                            <th>");
            EndContext();
            BeginContext(4111, 39, false);
#line 61 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                                           Write(SharedLocalizer["Final Approved"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(4150, 372, true);
            WriteLiteral(@"</th>

                                        </tr>
                                    </thead>
                                </table>
                            </div>
                            

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
                BeginContext(4540, 128, true);
                WriteLiteral("\r\n    <script>           \r\n\r\n\r\n        $(document).ready(function () {\r\n            var dataTable = $(\'#DRFTable\').DataTable({\r\n");
                EndContext();
                BeginContext(4712, 51, true);
                WriteLiteral("                order: [],\r\n                ajax: \"");
                EndContext();
                BeginContext(4764, 62, false);
#line 90 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\DRFInitialization\ProjectList.cshtml"
                  Write(Url.Action("GetFinalApprovedProjectList", "DRFInitialization"));

#line default
#line hidden
                EndContext();
                BeginContext(4826, 2023, true);
                WriteLiteral(@"/"",
                responsive: true,
                columnDefs: [					
                    { responsivePriority: 1, targets: 0 },
					{ responsivePriority: 2, targets: 5 },
                    { responsivePriority: 3, targets: -4 },                     
                     { responsivePriority: 4, targets: -1 }									
					
                ],
                ""dom"": 'Bfrtip',
                ""buttons"": [
                    {
                        ""extend"": 'excel', ""text"": '<i class=""far fa-file-excel mr-1""></i> Export In Excel ', ""className"": ""btn-primary"", ""exportOptions"": {
                            ""columns"": ':visible'
                        }
                    },
                    { ""extend"": 'colvis', ""className"": ""btn-primary"" }
                ],
                columns: [
                    { ""data"": ""initializationID"" },					
                    { ""data"": ""drfNo"" },
                    { ""data"": ""projectName"" },
                    { ""data"": ""countryName"" },
 ");
                WriteLiteral(@"                   { ""data"": ""genericName"" },
                    { ""data"": ""brandName"" },
                    { ""data"": ""orderFrequency"" },
                    { ""data"": ""strengthName"" },
                    { ""data"": ""packSizeName"" },
                    { ""data"": ""packStyleName"" },
                    { ""data"": ""formName"" },
                    { ""data"": ""plantName"" },
                    { ""data"": ""incotermsName"" },
                    { ""data"": ""maHolder"" },
                    { ""data"": ""nameDossierSend"" },
                    { ""data"": ""projectManager"" },
                    { ""data"": ""dossierTemplate"" },
                    { ""data"": ""status"" },
					{ ""data"": ""final_Approved_Date"" }
					
                ]
            });
            dataTable.draw().buttons().container().appendTo('.export-btn');
            $("".buttons-excel"").removeClass(""btn-secondary"");
            $("".buttons-collection"").removeClass(""btn-secondary"");
        });


    </script>
");
                EndContext();
            }
            );
            BeginContext(6852, 4, true);
            WriteLiteral("\r\n\r\n");
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
