#pragma checksum "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\Manage\_ManageNav.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b2f7eb447554d0e60fa64bf13cc955b5e228d0fc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Manage__ManageNav), @"mvc.1.0.view", @"/Views/Manage/_ManageNav.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Manage/_ManageNav.cshtml", typeof(AspNetCore.Views_Manage__ManageNav))]
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
#line 1 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\Manage\_ManageNav.cshtml"
using EmcureCERI.Web.Views.Manage;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b2f7eb447554d0e60fa64bf13cc955b5e228d0fc", @"/Views/Manage/_ManageNav.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a019622cc0247a5b372edf0adf687502aaf1316b", @"/Views/Manage/_ViewImports.cshtml")]
    public class Views_Manage__ManageNav : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ChangePassword", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ContactDetails", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SecurityQuestion", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\Manage\_ManageNav.cshtml"
  
    var hasExternalLogins = (await SignInManager.GetExternalAuthenticationSchemesAsync()).Any();

#line default
#line hidden
            BeginContext(317, 5, true);
            WriteLiteral("\r\n<li");
            EndContext();
            BeginWriteAttribute("class", " class=\"", 322, "\"", 386, 3);
            WriteAttributeValue("", 330, "nav-item", 330, 8, true);
            WriteAttributeValue(" ", 338, "mr-1", 339, 5, true);
#line 8 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\Manage\_ManageNav.cshtml"
WriteAttributeValue(" ", 343, ManageNavPages.IndexNavClass(ViewContext), 344, 42, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(387, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(388, 115, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b2f7eb447554d0e60fa64bf13cc955b5e228d0fc6116", async() => {
                BeginContext(434, 32, true);
                WriteLiteral("<i class=\"fas fa-user mr-2\"></i>");
                EndContext();
                BeginContext(467, 32, false);
#line 8 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\Manage\_ManageNav.cshtml"
                                                                                                                                              Write(SharedLocalizer["Profile"].Value);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(503, 10, true);
            WriteLiteral("</li>\r\n<li");
            EndContext();
            BeginWriteAttribute("class", " class=\"", 513, "\"", 586, 3);
            WriteAttributeValue("", 521, "nav-item", 521, 8, true);
            WriteAttributeValue(" ", 529, "mr-1", 530, 5, true);
#line 9 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\Manage\_ManageNav.cshtml"
WriteAttributeValue(" ", 534, ManageNavPages.ChangePasswordNavClass(ViewContext), 535, 51, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(587, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(588, 124, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b2f7eb447554d0e60fa64bf13cc955b5e228d0fc8601", async() => {
                BeginContext(643, 31, true);
                WriteLiteral("<i class=\"fas fa-key mr-2\"></i>");
                EndContext();
                BeginContext(675, 33, false);
#line 9 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\Manage\_ManageNav.cshtml"
                                                                                                                                                               Write(SharedLocalizer["Password"].Value);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(712, 30, true);
            WriteLiteral("</li>\r\n<li id=\"contactdetails\"");
            EndContext();
            BeginWriteAttribute("class", " class=\"", 742, "\"", 816, 3);
            WriteAttributeValue("", 750, "nav-item", 750, 8, true);
            WriteAttributeValue(" ", 758, "mr-1", 759, 5, true);
#line 10 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\Manage\_ManageNav.cshtml"
WriteAttributeValue("  ", 763, ManageNavPages.ContactDetailsNavClass(ViewContext), 765, 51, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(817, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(818, 136, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b2f7eb447554d0e60fa64bf13cc955b5e228d0fc11127", async() => {
                BeginContext(873, 36, true);
                WriteLiteral("<i class=\"fas fa-envelope mr-2\"></i>");
                EndContext();
                BeginContext(910, 40, false);
#line 10 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\Manage\_ManageNav.cshtml"
                                                                                                                                                                                         Write(SharedLocalizer["Contact Details"].Value);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(954, 10, true);
            WriteLiteral("</li>\r\n<li");
            EndContext();
            BeginWriteAttribute("class", " class=\"", 964, "\"", 1034, 2);
            WriteAttributeValue("", 972, "nav-item", 972, 8, true);
#line 11 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\Manage\_ManageNav.cshtml"
WriteAttributeValue(" ", 980, ManageNavPages.SecurityQuestionNavClass(ViewContext), 981, 53, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1035, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(1036, 140, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b2f7eb447554d0e60fa64bf13cc955b5e228d0fc13609", async() => {
                BeginContext(1093, 36, true);
                WriteLiteral("<i class=\"fas fa-question mr-2\"></i>");
                EndContext();
                BeginContext(1130, 42, false);
#line 11 "C:\Users\user\Desktop\Latest Emcure\ExistingApplication\EmcureCERI.Web\Views\Manage\_ManageNav.cshtml"
                                                                                                                                                                   Write(SharedLocalizer["Security Question"].Value);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
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
            BeginContext(1176, 7, true);
            WriteLiteral("</li>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.Extensions.Localization.IStringLocalizer<SharedResource> SharedLocalizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> SignInManager { get; private set; }
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
