#pragma checksum "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\AccessDenied.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "31198dab1d09d7a50ec2a547c4ddf5f6812cc748"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_AccessDenied), @"mvc.1.0.view", @"/Views/Account/AccessDenied.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Account/AccessDenied.cshtml", typeof(AspNetCore.Views_Account_AccessDenied))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"31198dab1d09d7a50ec2a547c4ddf5f6812cc748", @"/Views/Account/AccessDenied.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_AccessDenied : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\AccessDenied.cshtml"
  
    ViewData["Title"] = @SharedLocalizer["Access denied"].Value;

#line default
#line hidden
            BeginContext(165, 633, true);
            WriteLiteral(@"




<div class=""content-wrapper pt-3"">


    <!-- Main content -->
    <section class=""content"">
        <div class=""container-fluid"">
            <!-- Small boxes (Stat box) -->
            <!-- Main row -->
            <div class=""row"">
                <section class=""col-lg-2""></section>
                <section class=""col-lg-8"">
                    <!-- Custom tabs (Charts with tabs)-->
                    <div class=""card"">
                        <div class=""card-header"">
                            <h3 class=""card-title text-danger"">
                                <i class=""fas fa-user mr-2""></i> ");
            EndContext();
            BeginContext(799, 17, false);
#line 25 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\AccessDenied.cshtml"
                                                            Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(816, 480, true);
            WriteLiteral(@"
                            </h3>
                            <div class=""card-tools"">
                                        <button class=""btn btn-primary btn-sm"" onclick=""javascript:window.history.back();""><i class=""fas fa-undo mr-1""></i> Back</button>
                            </div>

                        </div>
                        <!-- /.card-header -->
                        <div class=""card-body"">
                            <p class=""text-danger"">");
            EndContext();
            BeginContext(1297, 65, false);
#line 34 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\AccessDenied.cshtml"
                                              Write(SharedLocalizer["You do not have access to this resource."].Value);

#line default
#line hidden
            EndContext();
            BeginContext(1362, 394, true);
            WriteLiteral(@"</p>
                        </div>
                        <!-- /.card-body -->
                    </div>
                    <!-- /.card -->

                </section>
                <section class=""col-lg-2""></section>

            </div>
            <!-- /.row (main row) -->
        </div>
        <!-- /.container-fluid -->
    </section>
    <!-- /.content -->
</div>
");
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
