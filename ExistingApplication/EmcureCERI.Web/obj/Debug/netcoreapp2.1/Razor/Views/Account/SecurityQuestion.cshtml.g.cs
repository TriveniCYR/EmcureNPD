#pragma checksum "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e5e5c4f7b587b398a36c89cb46c838fb0f16f621"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_SecurityQuestion), @"mvc.1.0.view", @"/Views/Account/SecurityQuestion.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Account/SecurityQuestion.cshtml", typeof(AspNetCore.Views_Account_SecurityQuestion))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e5e5c4f7b587b398a36c89cb46c838fb0f16f621", @"/Views/Account/SecurityQuestion.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_SecurityQuestion : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SecurityQuestionViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-block"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Account", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Login", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SecurityQuestion", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
  
    ViewData["Title"] = @SharedLocalizer["Security Question"].Value;

#line default
#line hidden
            BeginContext(203, 211, true);
            WriteLiteral("\r\n<section class=\"beforeLoginPage\">\r\n    <div class=\"container-fluid\">\r\n        <div class=\"loginFormContainer\">\r\n            <div class=\"appoinment-form fadeIn wow\" data-wow-duration=\"1s\">\r\n                <h2>");
            EndContext();
            BeginContext(415, 42, false);
#line 11 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
               Write(SharedLocalizer["Security Question"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(457, 286, true);
            WriteLiteral(@"</h2>
                <div class=""divider"">
                    <span><i class=""glyphicon glyphicon-certificate""></i></span>
                </div>
                <div class=""row"">
                    <div class=""col-xs-12 col-sm-12 col-md-12 col-lg-12"">
                        ");
            EndContext();
            BeginContext(743, 2742, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e5e5c4f7b587b398a36c89cb46c838fb0f16f6217197", async() => {
                BeginContext(793, 30, true);
                WriteLiteral("\r\n                            ");
                EndContext();
                BeginContext(823, 60, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e5e5c4f7b587b398a36c89cb46c838fb0f16f6217607", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#line 18 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.All;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(883, 30, true);
                WriteLiteral("\r\n                            ");
                EndContext();
                BeginContext(914, 36, false);
#line 19 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
                       Write(Html.HiddenFor(model => model.Email));

#line default
#line hidden
                EndContext();
                BeginContext(950, 208, true);
                WriteLiteral("\r\n                            <div class=\"row\">\r\n                                <div class=\"col-sm-12\">\r\n                                    <div class=\"form-group\">\r\n                                        ");
                EndContext();
                BeginContext(1159, 108, false);
#line 23 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
                                   Write(Html.DisplayFor(model => model.Question1, new { htmlAttributes = new { @class = "form-control input-sm" } }));

#line default
#line hidden
                EndContext();
                BeginContext(1267, 42, true);
                WriteLiteral("\r\n                                        ");
                EndContext();
                BeginContext(1310, 105, false);
#line 24 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
                                   Write(Html.EditorFor(model => model.Answer1, new { htmlAttributes = new { @class = "form-control input-sm" } }));

#line default
#line hidden
                EndContext();
                BeginContext(1415, 42, true);
                WriteLiteral("\r\n                                        ");
                EndContext();
                BeginContext(1458, 85, false);
#line 25 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
                                   Write(Html.ValidationMessageFor(model => model.Answer1, "", new { @class = "text-danger" }));

#line default
#line hidden
                EndContext();
                BeginContext(1543, 328, true);
                WriteLiteral(@"
                                    </div>
                                </div>
                            </div>
                            <div class=""row"">
                                <div class=""col-sm-12"">
                                    <div class=""form-group"">
                                        ");
                EndContext();
                BeginContext(1872, 108, false);
#line 32 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
                                   Write(Html.DisplayFor(model => model.Question2, new { htmlAttributes = new { @class = "form-control input-sm" } }));

#line default
#line hidden
                EndContext();
                BeginContext(1980, 42, true);
                WriteLiteral("\r\n                                        ");
                EndContext();
                BeginContext(2023, 105, false);
#line 33 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
                                   Write(Html.EditorFor(model => model.Answer2, new { htmlAttributes = new { @class = "form-control input-sm" } }));

#line default
#line hidden
                EndContext();
                BeginContext(2128, 42, true);
                WriteLiteral("\r\n                                        ");
                EndContext();
                BeginContext(2171, 85, false);
#line 34 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
                                   Write(Html.ValidationMessageFor(model => model.Answer2, "", new { @class = "text-danger" }));

#line default
#line hidden
                EndContext();
                BeginContext(2256, 328, true);
                WriteLiteral(@"
                                    </div>
                                </div>
                            </div>
                            <div class=""row"">
                                <div class=""col-sm-12"">
                                    <div class=""form-group"">
                                        ");
                EndContext();
                BeginContext(2585, 108, false);
#line 41 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
                                   Write(Html.DisplayFor(model => model.Question3, new { htmlAttributes = new { @class = "form-control input-sm" } }));

#line default
#line hidden
                EndContext();
                BeginContext(2693, 42, true);
                WriteLiteral("\r\n                                        ");
                EndContext();
                BeginContext(2736, 105, false);
#line 42 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
                                   Write(Html.EditorFor(model => model.Answer3, new { htmlAttributes = new { @class = "form-control input-sm" } }));

#line default
#line hidden
                EndContext();
                BeginContext(2841, 42, true);
                WriteLiteral("\r\n                                        ");
                EndContext();
                BeginContext(2884, 85, false);
#line 43 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
                                   Write(Html.ValidationMessageFor(model => model.Answer3, "", new { @class = "text-danger" }));

#line default
#line hidden
                EndContext();
                BeginContext(2969, 206, true);
                WriteLiteral("\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                            <button type=\"submit\" class=\"btn btn-default btn-block\">");
                EndContext();
                BeginContext(3176, 29, false);
#line 47 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
                                                                               Write(SharedLocalizer["Send"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(3205, 111, true);
                WriteLiteral("</button>\r\n                            <br />\r\n                            <br />\r\n                            ");
                EndContext();
                BeginContext(3316, 136, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e5e5c4f7b587b398a36c89cb46c838fb0f16f62116166", async() => {
                    BeginContext(3410, 38, false);
#line 50 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
                                                                                                                    Write(SharedLocalizer["Back to Login"].Value);

#line default
#line hidden
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3452, 26, true);
                WriteLiteral("\r\n                        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3485, 114, true);
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(3617, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(3624, 52, false);
#line 59 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Account\SecurityQuestion.cshtml"
Write(await Html.PartialAsync("_ValidationScriptsPartial"));

#line default
#line hidden
                EndContext();
                BeginContext(3676, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            BeginContext(3681, 68, true);
            WriteLiteral("<style>\r\n    footer, hr {\r\n        display: none;\r\n    }\r\n</style>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SecurityQuestionViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
