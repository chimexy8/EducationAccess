#pragma checksum "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\History\_Transaction.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6e9afea86c25e1f805331f1531dcc6b4e47cc31f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_History__Transaction), @"mvc.1.0.view", @"/Views/History/_Transaction.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/History/_Transaction.cshtml", typeof(AspNetCore.Views_History__Transaction))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e9afea86c25e1f805331f1531dcc6b4e47cc31f", @"/Views/History/_Transaction.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"26bc9580941072e1fd7ea28b5daa5928d919cacb", @"/Views/_ViewImports.cshtml")]
    public class Views_History__Transaction : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/img/png/avatar.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(120, 543, true);
            WriteLiteral(@"
<div class=""head sub"">
    <a href=""#"" class=""btn btn-link form-link"" data-link=""fund-trn-phone"">
        <i class=""icon auto"">
            <svg width=""16"" height=""15"" viewBox=""0 0 16 15"" fill=""none""
                 xmlns=""http://www.w3.org/2000/svg"">
                <path d=""M16 6.61564V8.42502H3.98955L9.49477 13.4055L8.08057 14.6849L0.161133 7.52033L8.08057 0.355713L9.49477 1.63513L3.98955 6.61564H16Z""
                      fill=""#5D2684"" />
            </svg>
        </i>
    </a>
    <h4>Repeat Transaction</h4>
</div>
");
            EndContext();
            BeginContext(663, 5311, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6e9afea86c25e1f805331f1531dcc6b4e47cc31f4491", async() => {
                BeginContext(669, 76, true);
                WriteLiteral("\r\n    <div class=\"summary-card\">\r\n        <div class=\"avater\">\r\n            ");
                EndContext();
                BeginContext(745, 41, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "6e9afea86c25e1f805331f1531dcc6b4e47cc31f4955", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(786, 43, true);
                WriteLiteral("\r\n            <p>\r\n                <strong>");
                EndContext();
                BeginContext(830, 25, false);
#line 21 "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\History\_Transaction.cshtml"
                   Write(ViewBag.Result.Receipient);

#line default
#line hidden
                EndContext();
                BeginContext(855, 33, true);
                WriteLiteral("</strong>\r\n                <span>");
                EndContext();
                BeginContext(889, 20, false);
#line 22 "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\History\_Transaction.cshtml"
                 Write(ViewBag.Result.Phone);

#line default
#line hidden
                EndContext();
                BeginContext(909, 90, true);
                WriteLiteral("</span>\r\n            </p>\r\n        </div>\r\n        <div class=\"sum\">\r\n            <strong>");
                EndContext();
                BeginContext(1000, 21, false);
#line 26 "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\History\_Transaction.cshtml"
               Write(ViewBag.Result.Amount);

#line default
#line hidden
                EndContext();
                BeginContext(1021, 90, true);
                WriteLiteral("</strong>\r\n            <label>Charge: <em>₦10</em></label>\r\n        </div>\r\n    </div>\r\n\r\n");
                EndContext();
                BeginContext(2530, 1450, true);
                WriteLiteral(@"
    <p class=""m-0"">Select how you want to authorize this transaction:</p>
    <div class=""select-list"">
        <div class=""item form-link"" data-link=""fund-trn-otptoken"">
            <i class=""icon auto"">
                <svg width=""13"" height=""27"" viewBox=""0 0 13 27"" fill=""none""
                     xmlns=""http://www.w3.org/2000/svg"">
                    <path d=""M9.64037 0.424805C8.93011 0.424805 8.35832 0.9966 8.35832 1.70686V5.55301H3.23012C1.80959 5.55301 0.666016 6.6966 0.666016 8.11711V23.5017C0.666016 24.9222 1.80959 26.0658 3.23012 26.0658H9.64037C11.0609 26.0658 12.2045 24.9222 12.2045 23.5017V8.11711C12.2045 7.16698 11.6863 6.34916 10.9224 5.90609V1.70686C10.9224 0.9966 10.3506 0.424805 9.64037 0.424805ZM3.23012 8.11711H9.64037V14.5274H3.23012V8.11711ZM3.23012 17.0915H4.51217V18.3735H3.23012V17.0915ZM5.79422 17.0915H7.07627V18.3735H5.79422V17.0915ZM8.35832 17.0915H9.64037V18.3735H8.35832V17.0915ZM3.23012 19.6556H4.51217V20.9376H3.23012V19.6556ZM5.79422 19.6556H7.07627V20.9376H5.79422V19.655");
                WriteLiteral(@"6ZM8.35832 19.6556H9.64037V20.9376H8.35832V19.6556ZM3.23012 22.2197H4.51217V23.5017H3.23012V22.2197ZM5.79422 22.2197H7.07627V23.5017H5.79422V22.2197ZM8.35832 22.2197H9.64037V23.5017H8.35832V22.2197Z""
                          fill=""#FBB700"" />
                </svg>
            </i>
            <p>
                <strong>Send OTP to Mobile Phone</strong>
                <span class=""ellipsis"">A code will be sent to ");
                EndContext();
                BeginContext(3981, 13, false);
#line 70 "C:\Users\User\Music\scholar\EducationAccess2.0\FlashMoney\Views\History\_Transaction.cshtml"
                                                         Write(ViewBag.Phone);

#line default
#line hidden
                EndContext();
                BeginContext(3994, 1973, true);
                WriteLiteral(@" </span>
            </p>
            <i class=""icon auto check"">
                <svg width=""24"" height=""24"" viewBox=""0 0 24 24"" fill=""none""
                     xmlns=""http://www.w3.org/2000/svg"">
                    <path d=""M21 7.00015L9 19.0001L3.5 13.5001L4.91421 12.0859L9 16.1717L19.5858 5.58594L21 7.00015Z""
                          fill=""#6AC895"" />
                </svg>
            </i>
        </div>
        <div class=""item form-link"" data-link=""fund-trn-transpin"">
            <i class=""icon auto"">
                <svg width=""17"" height=""20"" viewBox=""0 0 17 20"" fill=""none""
                     xmlns=""http://www.w3.org/2000/svg"">
                    <path d=""M8.18182 0L0 3.63636V9.09091C0 14.1364 3.49091 18.8545 8.18182 20C12.8727 18.8545 16.3636 14.1364 16.3636 9.09091V3.63636L8.18182 0ZM8.18182 5.45455C9.45455 5.45455 10.728 6.45455 10.728 7.72727V9.09091C11.2735 9.09091 11.8182 9.63709 11.8182 10.2735V13.4553C11.8182 14.0007 11.272 14.5455 10.6356 14.5455H5.63565C5.0902 14.5455 4.");
                WriteLiteral(@"54545 13.9993 4.54545 13.3629V10.1811C4.54545 9.63564 5.0902 9.09091 5.63565 9.09091V7.72727C5.63565 6.45455 6.90909 5.45455 8.18182 5.45455ZM8.18182 6.54473C7.45455 6.54473 6.81818 7 6.81818 7.72727V9.09091H9.54545V7.72727C9.54545 7 8.90909 6.54473 8.18182 6.54473Z""
                          fill=""#FBB700"" />
                </svg>
            </i>
            <p>
                <strong>Use Transaction PIN</strong>
                <span class=""ellipsis"">Your 4-digit PIN for transactions over N20,000</span>
            </p>
            <i class=""icon auto check"">
                <svg width=""24"" height=""24"" viewBox=""0 0 24 24"" fill=""none""
                     xmlns=""http://www.w3.org/2000/svg"">
                    <path d=""M21 7.00015L9 19.0001L3.5 13.5001L4.91421 12.0859L9 16.1717L19.5858 5.58594L21 7.00015Z""
                          fill=""#6AC895"" />
                </svg>
            </i>
        </div>
    </div>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
