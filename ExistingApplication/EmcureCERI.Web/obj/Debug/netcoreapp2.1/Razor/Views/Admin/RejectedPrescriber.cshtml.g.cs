#pragma checksum "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Admin\RejectedPrescriber.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f88bd73e344f338fbfd7641762d657a1139bc60c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_RejectedPrescriber), @"mvc.1.0.view", @"/Views/Admin/RejectedPrescriber.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/RejectedPrescriber.cshtml", typeof(AspNetCore.Views_Admin_RejectedPrescriber))]
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
#line 1 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Admin\RejectedPrescriber.cshtml"
using Microsoft.Extensions.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f88bd73e344f338fbfd7641762d657a1139bc60c", @"/Views/Admin/RejectedPrescriber.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_RejectedPrescriber : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EmcureCERI.Web.Models.PrescriberViewModels.ReasonViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Admin\RejectedPrescriber.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(194, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 8 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Admin\RejectedPrescriber.cshtml"
 using (Html.BeginForm("RejectedPrescriber", "Admin", FormMethod.Post, new { onsubmit = "return SubmitFormReject(this)" }))
{
    

#line default
#line hidden
            BeginContext(329, 33, false);
#line 10 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Admin\RejectedPrescriber.cshtml"
Write(Html.HiddenFor(model => model.Id));

#line default
#line hidden
            EndContext();
            BeginContext(364, 110, true);
            WriteLiteral("    <div class=\"row\">\r\n        <div class=\"col-sm-12\">\r\n            <div class=\"form-group\">\r\n                ");
            EndContext();
            BeginContext(475, 86, false);
#line 14 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Admin\RejectedPrescriber.cshtml"
           Write(Html.LabelFor(model => model.Reason, htmlAttributes: new { @class = "control-label" }));

#line default
#line hidden
            EndContext();
            BeginContext(561, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(580, 92, false);
#line 15 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Admin\RejectedPrescriber.cshtml"
           Write(Html.TextAreaFor(model => model.Reason, new { @class = "form-control input-sm", @rows = 4 }));

#line default
#line hidden
            EndContext();
            BeginContext(672, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(691, 84, false);
#line 16 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Admin\RejectedPrescriber.cshtml"
           Write(Html.ValidationMessageFor(model => model.Reason, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(775, 192, true);
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-sm-12 text-center\">\r\n            <div class=\"form-group\">\r\n                <input type=\"submit\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 967, "\"", 1005, 1);
#line 23 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Admin\RejectedPrescriber.cshtml"
WriteAttributeValue("", 975, SharedLocalizer["Save"].Value, 975, 30, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1006, 126, true);
            WriteLiteral(" class=\"btn btn-default btn-sm\" />\r\n                <button type=\"button\" class=\"btn btn-default btn-sm\" data-dismiss=\"modal\">");
            EndContext();
            BeginContext(1133, 31, false);
#line 24 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Admin\RejectedPrescriber.cshtml"
                                                                                     Write(SharedLocalizer["Cancel"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(1164, 59, true);
            WriteLiteral("</button>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
            EndContext();
#line 28 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Admin\RejectedPrescriber.cshtml"
}

#line default
#line hidden
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IStringLocalizer<SharedResource> SharedLocalizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EmcureCERI.Web.Models.PrescriberViewModels.ReasonViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
