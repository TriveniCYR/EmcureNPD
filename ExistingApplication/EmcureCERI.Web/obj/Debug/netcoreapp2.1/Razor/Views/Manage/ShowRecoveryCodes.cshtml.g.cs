#pragma checksum "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Manage\ShowRecoveryCodes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ca8b3c2507eae88cf0c32b2a0d5ccf63902373a6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Manage_ShowRecoveryCodes), @"mvc.1.0.view", @"/Views/Manage/ShowRecoveryCodes.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Manage/ShowRecoveryCodes.cshtml", typeof(AspNetCore.Views_Manage_ShowRecoveryCodes))]
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
#line 1 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Manage\_ViewImports.cshtml"
using EmcureCERI.Web.Views.Manage;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ca8b3c2507eae88cf0c32b2a0d5ccf63902373a6", @"/Views/Manage/ShowRecoveryCodes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a019622cc0247a5b372edf0adf687502aaf1316b", @"/Views/Manage/_ViewImports.cshtml")]
    public class Views_Manage_ShowRecoveryCodes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EmcureCERI.Web.Models.ManageViewModels.ShowRecoveryCodesViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Manage\ShowRecoveryCodes.cshtml"
  
    ViewData["Title"] = "Recovery codes";
    ViewData.AddActivePage(ManageNavPages.TwoFactorAuthentication);

#line default
#line hidden
            BeginContext(193, 6, true);
            WriteLiteral("\r\n<h4>");
            EndContext();
            BeginContext(200, 17, false);
#line 7 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Manage\ShowRecoveryCodes.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(217, 377, true);
            WriteLiteral(@"</h4>
<div class=""alert alert-warning"" role=""alert"">
    <p>
        <span class=""glyphicon glyphicon-warning-sign""></span>
        <strong>Put these codes in a safe place.</strong>
    </p>
    <p>
        If you lose your device and don't have the recovery codes you will lose access to your account.
    </p>
</div>
<div class=""row"">
    <div class=""col-md-12"">
");
            EndContext();
#line 19 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Manage\ShowRecoveryCodes.cshtml"
         for (var row = 0; row < Model.RecoveryCodes.Length; row += 2)
        {

#line default
#line hidden
            BeginContext(677, 18, true);
            WriteLiteral("            <code>");
            EndContext();
            BeginContext(696, 24, false);
#line 21 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Manage\ShowRecoveryCodes.cshtml"
             Write(Model.RecoveryCodes[row]);

#line default
#line hidden
            EndContext();
            BeginContext(720, 7, true);
            WriteLiteral("</code>");
            EndContext();
            BeginContext(733, 6, true);
            WriteLiteral("&nbsp;");
            EndContext();
            BeginContext(746, 6, true);
            WriteLiteral("<code>");
            EndContext();
            BeginContext(753, 28, false);
#line 21 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Manage\ShowRecoveryCodes.cshtml"
                                                                      Write(Model.RecoveryCodes[row + 1]);

#line default
#line hidden
            EndContext();
            BeginContext(781, 15, true);
            WriteLiteral("</code><br />\r\n");
            EndContext();
#line 22 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Manage\ShowRecoveryCodes.cshtml"
        }

#line default
#line hidden
            BeginContext(807, 18, true);
            WriteLiteral("    </div>\r\n</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EmcureCERI.Web.Models.ManageViewModels.ShowRecoveryCodesViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591