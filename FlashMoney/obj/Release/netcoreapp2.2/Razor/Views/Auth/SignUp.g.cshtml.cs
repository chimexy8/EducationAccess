#pragma checksum "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Auth\SignUp.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2547891078c98ce70a04fd38a8ef22a5da69db09"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Auth_SignUp), @"mvc.1.0.view", @"/Views/Auth/SignUp.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Auth/SignUp.cshtml", typeof(AspNetCore.Views_Auth_SignUp))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2547891078c98ce70a04fd38a8ef22a5da69db09", @"/Views/Auth/SignUp.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"26bc9580941072e1fd7ea28b5daa5928d919cacb", @"/Views/_ViewImports.cshtml")]
    public class Views_Auth_SignUp : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FlashMoney.Models.SignUpViewmodel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Register", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Auth", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-link"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("placeholder", new global::Microsoft.AspNetCore.Html.HtmlString("First Name"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("autocomplete", new global::Microsoft.AspNetCore.Html.HtmlString("“off”"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("placeholder", new global::Microsoft.AspNetCore.Html.HtmlString("Last Name"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("date"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("placeholder", new global::Microsoft.AspNetCore.Html.HtmlString("Date of Birth"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("placeholder", new global::Microsoft.AspNetCore.Html.HtmlString("Password"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SignUp", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Auth\SignUp.cshtml"
  
    ViewData["Title"] = "SignUp";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(131, 134, true);
            WriteLiteral("\r\n\r\n\r\n<!-- Create Account: Profile -->\r\n<div class=\"form-section create-account-profile active\">\r\n    <div class=\"head sub\">\r\n        ");
            EndContext();
            BeginContext(265, 478, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2547891078c98ce70a04fd38a8ef22a5da69db098750", async() => {
                BeginContext(333, 406, true);
                WriteLiteral(@"
            <i class=""icon auto"">
                <svg width=""16"" height=""15"" viewBox=""0 0 16 15"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                    <path d=""M16 6.61564V8.42502H3.98955L9.49477 13.4055L8.08057 14.6849L0.161133 7.52033L8.08057 0.355713L9.49477 1.63513L3.98955 6.61564H16Z""
                          fill=""#5D2684"" />
                </svg>
            </i>
        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(743, 56, true);
            WriteLiteral("\r\n        <h4>Create an account</h4>\r\n    </div>\r\n\r\n    ");
            EndContext();
            BeginContext(799, 9215, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2547891078c98ce70a04fd38a8ef22a5da69db0910890", async() => {
                BeginContext(861, 132, true);
                WriteLiteral("\r\n        <h4 class=\"text-gradient\">Profile</h4>\r\n        <p>We need some information about you to complete your setup</p>\r\n        ");
                EndContext();
                BeginContext(993, 60, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2547891078c98ce70a04fd38a8ef22a5da69db0911410", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#line 26 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Auth\SignUp.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.All;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1053, 132, true);
                WriteLiteral("\r\n        <div class=\"row\">\r\n            <div class=\"col-sm-12 col-md-6\">\r\n                <div class=\"input\">\r\n                    ");
                EndContext();
                BeginContext(1185, 72, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "2547891078c98ce70a04fd38a8ef22a5da69db0913343", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 30 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Auth\SignUp.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.FirstName);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1257, 105, true);
                WriteLiteral("\r\n                    <label>First Name</label>\r\n                </div>\r\n            </div>\r\n            ");
                EndContext();
                BeginContext(1362, 39, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "2547891078c98ce70a04fd38a8ef22a5da69db0915244", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 34 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Auth\SignUp.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Phone);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_6.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1401, 105, true);
                WriteLiteral("\r\n            <div class=\"col-sm-12 col-md-6\">\r\n                <div class=\"input\">\r\n                    ");
                EndContext();
                BeginContext(1506, 70, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "2547891078c98ce70a04fd38a8ef22a5da69db0917185", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 37 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Auth\SignUp.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.LastName);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1576, 248, true);
                WriteLiteral("\r\n                    <label>Last Name</label>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"row\">\r\n            <div class=\"col-sm-12 col-md-6\">\r\n                <div class=\"input with-icon\">\r\n                    ");
                EndContext();
                BeginContext(1824, 83, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "2547891078c98ce70a04fd38a8ef22a5da69db0919242", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 45 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Auth\SignUp.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.date);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1907, 1261, true);
                WriteLiteral(@"
                    <label>Date of Birth</label>
                    <i class=""icon sm"">
                        <svg width=""20"" height=""20"" viewBox=""0 0 20 20"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                            <g opacity=""0.3"">
                                <path d=""M5.83333 10.0001H7.5V11.6667H5.83333V10.0001ZM17.5 5.00007V16.6667C17.5 17.5834 16.75 18.3334 15.8333 18.3334H4.16667C3.24167 18.3334 2.5 17.5834 2.5 16.6667L2.50833 5.00007C2.50833 4.08341 3.24167 3.33341 4.16667 3.33341H5V1.66675H6.66667V3.33341H13.3333V1.66675H15V3.33341H15.8333C16.75 3.33341 17.5 4.08341 17.5 5.00007ZM4.16667 6.66674H15.8333V5.00007H4.16667V6.66674ZM15.8333 16.6667V8.33341H4.16667V16.6667H15.8333ZM12.5 11.6667V10.0001H14.1667V11.6667H12.5ZM9.16667 11.6667V10.0001H10.8333V11.6667H9.16667ZM5.83333 13.3334H7.5V15.0001H5.83333V13.3334ZM12.5 15.0001V13.3334H14.1667V15.0001H12.5ZM9.16667 15.0001V13.3334H10.8333V15.0001H9.16667Z""
                                      fill=""#101010"" />
            ");
                WriteLiteral("                </g>\r\n                        </svg>\r\n                    </i>\r\n                </div>\r\n            </div>\r\n            <div class=\"col-sm-12 col-md-6\">\r\n                <div class=\"input with-icon\">\r\n                    ");
                EndContext();
                BeginContext(3168, 219, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2547891078c98ce70a04fd38a8ef22a5da69db0922447", async() => {
                    BeginContext(3193, 26, true);
                    WriteLiteral("\r\n                        ");
                    EndContext();
                    BeginContext(3219, 41, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2547891078c98ce70a04fd38a8ef22a5da69db0922878", async() => {
                        BeginContext(3245, 6, true);
                        WriteLiteral("Gender");
                        EndContext();
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    BeginWriteTagHelperAttribute();
                    __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                    __tagHelperExecutionContext.AddHtmlAttribute("disabled", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                    BeginWriteTagHelperAttribute();
                    __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                    __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(3260, 26, true);
                    WriteLiteral("\r\n                        ");
                    EndContext();
                    BeginContext(3286, 21, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2547891078c98ce70a04fd38a8ef22a5da69db0924909", async() => {
                        BeginContext(3294, 4, true);
                        WriteLiteral("Male");
                        EndContext();
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(3307, 26, true);
                    WriteLiteral("\r\n                        ");
                    EndContext();
                    BeginContext(3333, 23, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2547891078c98ce70a04fd38a8ef22a5da69db0926268", async() => {
                        BeginContext(3341, 6, true);
                        WriteLiteral("Female");
                        EndContext();
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(3356, 22, true);
                    WriteLiteral("\r\n                    ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
#line 59 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Auth\SignUp.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Gender);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3387, 741, true);
                WriteLiteral(@"
                    <label>Gender</label>
                    <i class=""icon sm"">
                        <svg width=""20"" height=""20"" viewBox=""0 0 20 20"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                            <g opacity=""0.3"">
                                <path d=""M6.17833 7.15332L10 10.975L13.8217 7.15332L15 8.33165L10 13.3317L5 8.33165L6.17833 7.15332Z""
                                      fill=""#101010"" />
                            </g>
                        </svg>
                    </i>
                </div>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-12"">
                <div class=""input with-icon password-check"">
                    ");
                EndContext();
                BeginContext(4128, 69, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "2547891078c98ce70a04fd38a8ef22a5da69db0929736", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 79 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Auth\SignUp.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Password);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(4197, 5810, true);
                WriteLiteral(@"
                    <label>Password</label>
                    <a class=""toggle active"">
                        <i class=""icon sm hide"">
                            <svg width=""15"" height=""15"" viewBox=""0 0 15 15"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                                <g opacity=""0.5"">
                                    <path d=""M7.39639 5.6343L9.36389 7.60175C9.36577 7.56682 9.37452 7.53425 9.37452 7.49863C9.37452 6.46307 8.53514 5.62365 7.49952 5.62365C7.46389 5.62365 7.43139 5.63239 7.39639 5.6343ZM4.70513 6.12552L5.67201 7.09175C5.64263 7.22307 5.62451 7.35863 5.62451 7.49863C5.62451 8.53425 6.46389 9.37363 7.49952 9.37363C7.64014 9.37363 7.77577 9.35557 7.90639 9.32675L8.87327 10.2931C8.45702 10.4981 7.99452 10.6236 7.49952 10.6236C5.77389 10.6236 4.37451 9.22494 4.37451 7.49863C4.37451 7.00363 4.50013 6.54176 4.70513 6.12552ZM1.24951 2.66929L2.67388 4.09427L2.95827 4.37866C1.92702 5.18615 1.11264 6.25988 0.624512 7.49863C1.70451 10.2418 4.37326 12.1861 7.49952 12.1861C8");
                WriteLiteral(@".46827 12.1861 9.39264 11.9987 10.2395 11.6599L10.5045 11.9243L12.3289 13.7493L13.1245 12.9537L2.04514 1.87427L1.24951 2.66929ZM7.49952 4.37365C9.22514 4.37365 10.6245 5.77304 10.6245 7.49863C10.6245 7.90238 10.542 8.28557 10.4026 8.63988L12.2289 10.4661C13.1714 9.67801 13.917 8.66113 14.3745 7.49863C13.2945 4.75554 10.6264 2.81115 7.49952 2.81115C6.62452 2.81115 5.78763 2.96802 5.00951 3.24741L6.35827 4.59617C6.71264 4.45678 7.09577 4.37365 7.49952 4.37365Z""
                                          fill=""#2F1343"" />
                                </g>
                            </svg>
                        </i>
                        <i class=""icon sm show"">
                            <svg width=""15"" height=""15"" viewBox=""0 0 15 15"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                                <g opacity=""0.5"">
                                    <path d=""M7.49999 5.62378C6.46437 5.62378 5.625 6.46314 5.625 7.49876C5.625 8.53439 6.46437 9.37376 7.49999 9.37376C8.53562 9.37376 ");
                WriteLiteral(@"9.37499 8.53439 9.37499 7.49876C9.37499 6.46314 8.53562 5.62378 7.49999 5.62378ZM7.49999 10.6238C5.77437 10.6238 4.375 9.22439 4.375 7.49876C4.375 5.77316 5.77437 4.37378 7.49999 4.37378C9.22562 4.37378 10.625 5.77316 10.625 7.49876C10.625 9.22439 9.22562 10.6238 7.49999 10.6238ZM7.49999 2.81128C4.37375 2.81128 1.705 4.75567 0.625 7.49876C1.705 10.2419 4.37375 12.1863 7.49999 12.1863C10.6269 12.1863 13.295 10.2419 14.375 7.49876C13.295 4.75567 10.6269 2.81128 7.49999 2.81128Z""
                                          fill=""#2F1343"" />
                                </g>
                            </svg>
                        </i>
                    </a>

                    <div class=""checker"">
                        <a class=""item"">
                            <i class=""icon auto"">
                                <svg width=""18"" height=""18"" viewBox=""0 0 18 18"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                                    <g opacity=""0.2"">
                            ");
                WriteLiteral(@"            <path d=""M15.75 5.24987L6.75 14.2499L2.625 10.1249L3.68566 9.06422L6.75 12.1286L14.6894 4.18921L15.75 5.24987Z""
                                              fill=""white"" />
                                    </g>
                                </svg>
                            </i>
                            <span>Minimum of 8 Characters</span>
                        </a>
                        <a class=""item"">
                            <i class=""icon auto"">
                                <svg width=""18"" height=""18"" viewBox=""0 0 18 18"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                                    <g opacity=""0.2"">
                                        <path d=""M15.75 5.24987L6.75 14.2499L2.625 10.1249L3.68566 9.06422L6.75 12.1286L14.6894 4.18921L15.75 5.24987Z""
                                              fill=""white"" />
                                    </g>
                                </svg>
                            </i>
               ");
                WriteLiteral(@"             <span>Must contain an Alphabet</span>
                        </a>
                        <a class=""item"">
                            <i class=""icon auto"">
                                <svg width=""18"" height=""18"" viewBox=""0 0 18 18"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                                    <g opacity=""0.2"">
                                        <path d=""M15.75 5.24987L6.75 14.2499L2.625 10.1249L3.68566 9.06422L6.75 12.1286L14.6894 4.18921L15.75 5.24987Z""
                                              fill=""white"" />
                                    </g>
                                </svg>
                            </i>
                            <span>At least one Number</span>
                        </a>
                        <a class=""item"">
                            <i class=""icon auto"">
                                <svg width=""18"" height=""18"" viewBox=""0 0 18 18"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                   ");
                WriteLiteral(@"                 <g opacity=""0.2"">
                                        <path d=""M15.75 5.24987L6.75 14.2499L2.625 10.1249L3.68566 9.06422L6.75 12.1286L14.6894 4.18921L15.75 5.24987Z""
                                              fill=""white"" />
                                    </g>
                                </svg>
                            </i>
                            <span>At least one Special Character (#*)</span>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div class=""action"">
            <button class=""btn btn-primary "" type=""submit"">Next</button>
        </div>
    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_11.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_12.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(10014, 10, true);
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FlashMoney.Models.SignUpViewmodel> Html { get; private set; }
    }
}
#pragma warning restore 1591
