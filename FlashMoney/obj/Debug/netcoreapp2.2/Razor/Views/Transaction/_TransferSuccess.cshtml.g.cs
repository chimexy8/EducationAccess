#pragma checksum "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\Transaction\_TransferSuccess.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9da01fecb39935e2abb104513df7fa84dd386eaf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Transaction__TransferSuccess), @"mvc.1.0.view", @"/Views/Transaction/_TransferSuccess.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Transaction/_TransferSuccess.cshtml", typeof(AspNetCore.Views_Transaction__TransferSuccess))]
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
#line 1 "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\_ViewImports.cshtml"
using FlashMoney;

#line default
#line hidden
#line 2 "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\_ViewImports.cshtml"
using FlashMoney.Models;

#line default
#line hidden
#line 1 "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\Transaction\_TransferSuccess.cshtml"
using System.Globalization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9da01fecb39935e2abb104513df7fa84dd386eaf", @"/Views/Transaction/_TransferSuccess.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"26bc9580941072e1fd7ea28b5daa5928d919cacb", @"/Views/_ViewImports.cshtml")]
    public class Views_Transaction__TransferSuccess : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FlashMoney.Models.TransactionModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/img/png/alert-success.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("success-img"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/img/png/alert-error.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("error-img"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(72, 406, true);
            WriteLiteral(@"<div class=""card form-card"">
    <!-- Loading  -->
    <div class=""form-section loading"">
        <div class=""la-ball-pulse"">
            <div></div>
            <div></div>
            <div></div>
        </div>
    </div>
    <!-- Success & Error -->
    <div class=""form-section alert sub success active"">
        <div class=""head"">
            <h4>Flash Money</h4>
        </div>
        ");
            EndContext();
            BeginContext(478, 68, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "9da01fecb39935e2abb104513df7fa84dd386eaf6476", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(546, 10, true);
            WriteLiteral("\r\n        ");
            EndContext();
            BeginContext(556, 64, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "9da01fecb39935e2abb104513df7fa84dd386eaf7736", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(620, 75, true);
            WriteLiteral("\r\n\r\n        <h3>Transfer Successful</h3>\r\n        <p>\r\n            <strong>");
            EndContext();
            BeginContext(696, 57, false);
#line 22 "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\Transaction\_TransferSuccess.cshtml"
               Write(Model.Amount.ToString("c", new CultureInfo("ha-Latn-NG")));

#line default
#line hidden
            EndContext();
            BeginContext(753, 30, true);
            WriteLiteral("</strong> has been sent to\r\n\r\n");
            EndContext();
#line 24 "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\Transaction\_TransferSuccess.cshtml"
             if (!string.IsNullOrEmpty(Model.Receipient))
            {

#line default
#line hidden
            BeginContext(857, 24, true);
            WriteLiteral("                <strong>");
            EndContext();
            BeginContext(882, 16, false);
#line 26 "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\Transaction\_TransferSuccess.cshtml"
                   Write(Model.Receipient);

#line default
#line hidden
            EndContext();
            BeginContext(898, 3, true);
            WriteLiteral(" - ");
            EndContext();
            BeginContext(902, 22, false);
#line 26 "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\Transaction\_TransferSuccess.cshtml"
                                       Write(Model.DestinationPhone);

#line default
#line hidden
            EndContext();
            BeginContext(924, 11, true);
            WriteLiteral("</strong>\r\n");
            EndContext();
#line 27 "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\Transaction\_TransferSuccess.cshtml"
            }
            else
            {

#line default
#line hidden
            BeginContext(983, 24, true);
            WriteLiteral("                <strong>");
            EndContext();
            BeginContext(1008, 22, false);
#line 30 "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\Transaction\_TransferSuccess.cshtml"
                   Write(Model.DestinationPhone);

#line default
#line hidden
            EndContext();
            BeginContext(1030, 11, true);
            WriteLiteral("</strong>\r\n");
            EndContext();
#line 31 "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\Transaction\_TransferSuccess.cshtml"
            }

#line default
#line hidden
            BeginContext(1056, 644, true);
            WriteLiteral(@"
        </p>

        <div class=""sub-info"">
            <i class=""icon"">
                <svg width=""18"" height=""18"" viewBox=""0 0 18 18"" fill=""none""
                     xmlns=""http://www.w3.org/2000/svg"">
                    <path d=""M9.83266 6.49837H8.16599V4.83171H9.83266V6.49837ZM9.83266 13.165H8.16599V8.165H9.83266V13.165ZM8.99932 0.665039C4.39684 0.665039 0.666016 4.39586 0.666016 8.99833C0.666016 13.6009 4.39684 17.3317 8.99932 17.3317C13.6018 17.3317 17.3327 13.6009 17.3327 8.99833C17.3327 4.39586 13.6018 0.665039 8.99932 0.665039Z""
                          fill=""#2F1343"" />
                </svg>
            </i>
");
            EndContext();
            BeginContext(1898, 24, true);
            WriteLiteral("        </div>\r\n        ");
            EndContext();
            BeginContext(1922, 80, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9da01fecb39935e2abb104513df7fa84dd386eaf12339", async() => {
                BeginContext(1990, 8, true);
                WriteLiteral("Continue");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2002, 22, true);
            WriteLiteral("\r\n    </div>\r\n\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FlashMoney.Models.TransactionModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
