#pragma checksum "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0e5452b4df627e0c090b6c013618e24a69e14d53"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_ContactUs), @"mvc.1.0.view", @"/Views/Home/ContactUs.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/ContactUs.cshtml", typeof(AspNetCore.Views_Home_ContactUs))]
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
#line 1 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
using Microsoft.Extensions.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0e5452b4df627e0c090b6c013618e24a69e14d53", @"/Views/Home/ContactUs.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_ContactUs : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EmcureCERI.Web.Models.ContactVM>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
  

    ViewData["Title"] = @SharedLocalizer["Contact Us"].Value;

#line default
#line hidden
            BeginContext(212, 145, true);
            WriteLiteral("<main class=\"main\">\r\n    <section class=\"our-services\" style=\"padding-top: 55px;\">\r\n        <div class=\"container text-center\">\r\n            <h2>");
            EndContext();
            BeginContext(358, 35, false);
#line 11 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
           Write(SharedLocalizer["Contact Us"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(393, 22, true);
            WriteLiteral("</h2>\r\n            <p>");
            EndContext();
            BeginContext(416, 112, false);
#line 12 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
          Write(SharedLocalizer["For more information about Cidofovir or Tillomed, please do not hesitate to contact us."].Value);

#line default
#line hidden
            EndContext();
            BeginContext(528, 485, true);
            WriteLiteral(@"</p>
            <div class=""divider"">
                <span><i class=""glyphicon glyphicon-certificate""></i></span>
            </div>
        </div>
    </section>

    <!-- Contact Form starts -->
    <section id=""contact-page-form"" class=""contact-page-form"">
        <div class=""container-fluid"" style=""padding: 0 30px;"">
            <div class=""row"">
                <div class=""appoinment-form"">
                    <div class=""col-xs-12 col-sm-6 col-md-8 col-lg-8"">
");
            EndContext();
#line 25 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                         using (Html.BeginForm("ContactUs", "Home", FormMethod.Post, new { onsubmit = "return SubmitFormRejectBLForm(this)" }))
                        {

#line default
#line hidden
            BeginContext(1185, 128, true);
            WriteLiteral("                            <div class=\"col-sm-12\">\r\n                                <br />\r\n                                <p>");
            EndContext();
            BeginContext(1314, 92, false);
#line 29 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                              Write(SharedLocalizer["We will be pleased to answer any questions or queries you may have."].Value);

#line default
#line hidden
            EndContext();
            BeginContext(1406, 228, true);
            WriteLiteral("</p>\r\n                                <br />\r\n                            </div>\r\n                            <div class=\"col-sm-6\">\r\n                                <div class=\"form-group\">\r\n                                    ");
            EndContext();
            BeginContext(1635, 102, false);
#line 34 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                               Write(Html.TextBoxFor(model => model.Name, new { @class = "form-control input-sm", @placeholder = "Name:" }));

#line default
#line hidden
            EndContext();
            BeginContext(1737, 38, true);
            WriteLiteral("\r\n                                    ");
            EndContext();
            BeginContext(1776, 82, false);
#line 35 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                               Write(Html.ValidationMessageFor(model => model.Name, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(1858, 224, true);
            WriteLiteral("\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"col-sm-6\">\r\n                                <div class=\"form-group\">\r\n                                    ");
            EndContext();
            BeginContext(2083, 105, false);
#line 40 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                               Write(Html.TextBoxFor(model => model.Email, new { @class = "form-control input-sm", @placeholder = "E-mail:" }));

#line default
#line hidden
            EndContext();
            BeginContext(2188, 38, true);
            WriteLiteral("\r\n                                    ");
            EndContext();
            BeginContext(2227, 83, false);
#line 41 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                               Write(Html.ValidationMessageFor(model => model.Email, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(2310, 78, true);
            WriteLiteral("\r\n                                </div>\r\n                            </div>\r\n");
            EndContext();
            BeginContext(2390, 147, true);
            WriteLiteral("                            <div class=\"col-xs-12\">\r\n                                <div class=\"form-group\">\r\n                                    ");
            EndContext();
            BeginContext(2538, 150, false);
#line 47 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                               Write(Html.TextAreaFor(model => model.Query, new { @class = "form-control input-sm", @rows = 3, @cols = 50, @placeholder = "Your questions or queries..." }));

#line default
#line hidden
            EndContext();
            BeginContext(2688, 38, true);
            WriteLiteral("\r\n                                    ");
            EndContext();
            BeginContext(2727, 83, false);
#line 48 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                               Write(Html.ValidationMessageFor(model => model.Query, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(2810, 78, true);
            WriteLiteral("\r\n                                </div>\r\n                            </div>\r\n");
            EndContext();
            BeginContext(2890, 214, true);
            WriteLiteral("                            <div class=\"col-xs-12\">\r\n                                <div class=\"form-group\">\r\n                                    <button type=\"submit\" class=\"btn btn-default btn-sm\" name=\"signup\">");
            EndContext();
            BeginContext(3105, 29, false);
#line 54 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                                                                                                  Write(SharedLocalizer["Send"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3134, 87, true);
            WriteLiteral("</button>\r\n                                </div>\r\n                            </div>\r\n");
            EndContext();
#line 57 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                        }

#line default
#line hidden
            BeginContext(3248, 187, true);
            WriteLiteral("                    </div>\r\n                    <div class=\"col-xs-12 col-sm-6 col-md-4 col-lg-4\">\r\n                        <div class=\"address-details\">\r\n                            <h4>");
            EndContext();
            BeginContext(3436, 44, false);
#line 61 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                           Write(SharedLocalizer["Contact Information"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3480, 1374, true);
            WriteLiteral(@"</h4>
                            <br />
                            <ul>
                                <li>
                                    <i class=""glyphicon glyphicon-earphone""></i>
                                    <span>
                                        <a href=""tel:+441480402400"">
                                            +441480 402400
                                        </a>
                                    </span>
                                </li>
                                <li>
                                    <i class=""glyphicon glyphicon-envelope""></i> <span>
                                        <a href=""mailto:PVUK@tillomed.co.uk"">
                                            PVUK@tillomed.co.uk
                                        </a>
                                    </span>
                                </li>
                            </ul>

                        </div>
                    </div>
                </div>
 ");
            WriteLiteral(@"           </div>
        </div>
    </section>
    <section>
        <div class=""container-fluid"">
            <div class=""about"" style=""padding-top:0;"">
                <div class=""about-premedi"" style=""padding-top:0;"">
                    <div class=""open-hours-details"">
                        <h4 style=""margin-top: 0; margin-left: 0;"">");
            EndContext();
            BeginContext(4855, 42, false);
#line 92 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                                                              Write(SharedLocalizer["Pharmacovigilance"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(4897, 138, true);
            WriteLiteral("</h4>\r\n                        <div class=\"row\">\r\n                            <ul class=\"col-xs-12\">\r\n                                <li>");
            EndContext();
            BeginContext(5036, 106, false);
#line 95 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                               Write(SharedLocalizer["Patients should always ask their doctors for medical advice about adverse events."].Value);

#line default
#line hidden
            EndContext();
            BeginContext(5142, 81, true);
            WriteLiteral("</li>\r\n                                <li>\r\n                                    ");
            EndContext();
            BeginContext(5224, 96, false);
#line 97 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                               Write(SharedLocalizer["You may report an adverse event related to Tillomed products by calling"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(5320, 461, true);
            WriteLiteral(@"
                                    <a href=""tel:+441480 402400"">
                                        +441480 402400
                                    </a> or
                                    <a href=""mailto:PVUK@tillomed.co.uk"">
                                        PVUK@tillomed.co.uk
                                    </a>
                                </li>
                                <li>
                                    ");
            EndContext();
            BeginContext(5782, 144, false);
#line 106 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                               Write(SharedLocalizer["For any urgent safety issues regarding Cidofovir out of office hours, please contact our out of office number(s) below:"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(5926, 549, true);
            WriteLiteral(@"  
                                    <br />
                                    <a href=""tel:+44 (0) 7753 606357"">
                                        +44(0)7753 606357
                                    </a>
                                    or
                                    <a href=""tel:+44 (0) 7901 556042"">
                                        +44 (0) 7901 556042
                                    </a>
                                </li>
                                <li>
                                    ");
            EndContext();
            BeginContext(6476, 491, false);
#line 117 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Home\ContactUs.cshtml"
                               Write(Html.Raw(SharedLocalizer["If you prefer, you may contact the medicines and healthcare agency  (MHRA) directly. The MHRA has established a reporting service known as the yellow card scheme where healthcare professionals and patients can report serious problems they suspect may be associated with the drugs and medical devices they prescribe, dispense, or use. Visit <a href='https://yellowcard.mhra.gov.uk' target='_blank'>https://yellowcard.mhra.gov.uk</a> for further information."].Value));

#line default
#line hidden
            EndContext();
            BeginContext(6967, 253, true);
            WriteLiteral("\r\n                                </li>\r\n                            </ul>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </section>\r\n    <!-- Contact Form ends -->\r\n</main>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EmcureCERI.Web.Models.ContactVM> Html { get; private set; }
    }
}
#pragma warning restore 1591