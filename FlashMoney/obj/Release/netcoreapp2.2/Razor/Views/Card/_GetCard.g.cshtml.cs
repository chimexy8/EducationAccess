#pragma checksum "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Card\_GetCard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c1c606ce0636b7683fdd3dba8615f833d92493aa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Card__GetCard), @"mvc.1.0.view", @"/Views/Card/_GetCard.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Card/_GetCard.cshtml", typeof(AspNetCore.Views_Card__GetCard))]
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
#line 1 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\_ViewImports.cshtml"
using FlashMoney;

#line default
#line hidden
#line 2 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\_ViewImports.cshtml"
using FlashMoney.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c1c606ce0636b7683fdd3dba8615f833d92493aa", @"/Views/Card/_GetCard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"26bc9580941072e1fd7ea28b5daa5928d919cacb", @"/Views/_ViewImports.cshtml")]
    public class Views_Card__GetCard : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/img/png/card-master.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("item link"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/cards.html"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(120, 6, true);
            WriteLiteral("\r\n\r\n\r\n");
            EndContext();
            BeginContext(126, 3877, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c1c606ce0636b7683fdd3dba8615f833d92493aa4607", async() => {
                BeginContext(132, 1162, true);
                WriteLiteral(@"
    <p>Add funds to your wallet so you can perform tarnsactions with</p>
    <span class=""error_message""></span>
    <div class=""input with-icon left"">
        <i class=""icon auto"">
            <svg width=""12"" height=""15"" viewBox=""0 0 12 15"" fill=""none""
                 xmlns=""http://www.w3.org/2000/svg"">
                <path opacity=""0.4""
                      d=""M1.7602 15.0002V9.86022H0.200195V8.58022H1.7602V6.94022H0.200195V5.66022H1.7602V0.720215H3.8402L5.7402 5.66022H8.1002V0.720215H9.6802V5.66022H11.2402V6.94022H9.6802V8.58022H11.2402V9.86022H9.6802V15.0002H7.5802L5.6802 9.86022H3.3202V15.0002H1.7602ZM3.2802 5.66022H4.1202L3.2802 3.18022H3.2002L3.2802 5.66022ZM3.3202 8.58022H5.2002L4.6002 6.94022H3.2802L3.3202 8.58022ZM6.8002 8.58022H8.1402L8.1002 6.94022H6.2002L6.8002 8.58022ZM8.1002 12.1802H8.1802L8.1202 9.86022H7.2802L8.1002 12.1802Z""
                      fill=""#2F1343"" />
            </svg>
        </i>
        <input class=""currency"" type=""text"" id=""amounttoadd"" placeholder=""Amount ");
                WriteLiteral("to Add\" />\r\n        <label>Amount to Add</label>\r\n    </div>\r\n\r\n    <p class=\"m-0\">Select Source:</p>\r\n    <div class=\"select-list\">\r\n\r\n\r\n");
                EndContext();
#line 26 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Card\_GetCard.cshtml"
         if (ViewBag.SavedCards != null)
        {
            foreach (var item in ViewBag.SavedCards)
            {


#line default
#line hidden
                BeginContext(1418, 126, true);
                WriteLiteral("                <div class=\"item \">\r\n                    <a>\r\n                        <input type=\"hidden\" class=\"phonenumber\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1544, "\"", 1566, 1);
#line 33 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Card\_GetCard.cshtml"
WriteAttributeValue("", 1552, ViewBag.Phone, 1552, 14, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1567, 61, true);
                WriteLiteral(" />\r\n                        <input type=\"hidden\" class=\"cId\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1628, "\"", 1644, 1);
#line 34 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Card\_GetCard.cshtml"
WriteAttributeValue("", 1636, item.Id, 1636, 8, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1645, 31, true);
                WriteLiteral(" />\r\n\r\n                        ");
                EndContext();
                BeginContext(1676, 46, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c1c606ce0636b7683fdd3dba8615f833d92493aa7699", async() => {
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
                BeginContext(1722, 31, true);
                WriteLiteral("\r\n                        <p>\r\n");
                EndContext();
#line 38 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Card\_GetCard.cshtml"
                               var phone = $"**** **** **** {item.CardNumber.Substring(item.CardNumber.Length - 4)} ";

#line default
#line hidden
                BeginContext(1874, 36, true);
                WriteLiteral("                            <strong>");
                EndContext();
                BeginContext(1911, 5, false);
#line 39 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Card\_GetCard.cshtml"
                               Write(phone);

#line default
#line hidden
                EndContext();
                BeginContext(1916, 71, true);
                WriteLiteral("</strong>\r\n                            <span class=\"ellipsis\">Expired: ");
                EndContext();
                BeginContext(1988, 17, false);
#line 40 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Card\_GetCard.cshtml"
                                                       Write(item.CardExpMonth);

#line default
#line hidden
                EndContext();
                BeginContext(2005, 3, true);
                WriteLiteral(" / ");
                EndContext();
                BeginContext(2009, 16, false);
#line 40 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Card\_GetCard.cshtml"
                                                                            Write(item.CardExpYear);

#line default
#line hidden
                EndContext();
                BeginContext(2025, 3, true);
                WriteLiteral(" | ");
                EndContext();
                BeginContext(2029, 13, false);
#line 40 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Card\_GetCard.cshtml"
                                                                                                Write(item.CardName);

#line default
#line hidden
                EndContext();
                BeginContext(2042, 561, true);
                WriteLiteral(@"</span>
                        </p>
                        <i class=""icon auto check"">
                            <svg width=""24"" height=""24"" viewBox=""0 0 24 24"" fill=""none""
                                 xmlns=""http://www.w3.org/2000/svg"">
                                <path d=""M21 7.00015L9 19.0001L3.5 13.5001L4.91421 12.0859L9 16.1717L19.5858 5.58594L21 7.00015Z""
                                      fill=""#6AC895"" />
                            </svg>
                        </i>
                    </a>



                </div>
");
                EndContext();
#line 54 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Card\_GetCard.cshtml"
            }
        }

#line default
#line hidden
                BeginContext(2629, 16, true);
                WriteLiteral("\r\n\r\n\r\n\r\n        ");
                EndContext();
                BeginContext(2645, 1329, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c1c606ce0636b7683fdd3dba8615f833d92493aa11985", async() => {
                    BeginContext(2686, 1284, true);
                    WriteLiteral(@"
            <i class=""icon auto"">
                <svg width=""20"" height=""17"" viewBox=""0 0 20 17"" fill=""none""
                     xmlns=""http://www.w3.org/2000/svg"">
                    <path d=""M17.4992 11.9998H19.9992V13.6665H17.4992V16.1665H15.8325V13.6665H13.3325V11.9998H15.8325V9.49984H17.4992V11.9998ZM15.832 3.66488V1.99821H2.4987V3.66488H15.832ZM15.832 6.99825H2.4987V11.9982H11.6659V13.6648H2.4987C1.57785 13.6648 0.832031 12.9199 0.832031 11.9982L0.840374 1.99821C0.840374 1.07651 1.57785 0.331543 2.4987 0.331543H15.832C16.7529 0.331543 17.4987 1.07651 17.4987 1.99821V7.83317H15.832V6.99825Z""
                          fill=""#FBB700"" />
                </svg>
            </i>
            <p>
                <strong>Add New Debit/Credit Card</strong>
                <span class=""ellipsis"">mastercard, visa, etc..</span>
            </p>
            <i class=""icon auto arrow"">
                <svg width=""7"" height=""11"" viewBox=""0 0 7 11"" fill=""none""
                     xmlns=""http://www.w3.");
                    WriteLiteral(@"org/2000/svg"">
                    <path d=""M0.154297 9.82022L3.97599 5.99855L0.154297 2.17687L1.33263 0.998535L6.33265 5.99855L1.33263 10.9986L0.154297 9.82022Z""
                          fill=""#101010"" />
                </svg>
            </i>
        ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3974, 22, true);
                WriteLiteral("\r\n    </div>\r\n\r\n\r\n\r\n\r\n");
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
            BeginContext(4003, 4, true);
            WriteLiteral("\r\n\r\n");
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
