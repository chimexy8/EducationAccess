#pragma checksum "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Transaction\GoBackWallet.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "03ce58c15ae978781531f2942d223f62c52b2646"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Transaction_GoBackWallet), @"mvc.1.0.view", @"/Views/Transaction/GoBackWallet.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Transaction/GoBackWallet.cshtml", typeof(AspNetCore.Views_Transaction_GoBackWallet))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"03ce58c15ae978781531f2942d223f62c52b2646", @"/Views/Transaction/GoBackWallet.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"26bc9580941072e1fd7ea28b5daa5928d919cacb", @"/Views/_ViewImports.cshtml")]
    public class Views_Transaction_GoBackWallet : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FlashMoney.Models.OverviewModel>
    {
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(40, 984, true);
            WriteLiteral(@"<div class=""card form-card"">
    <!-- Loading  -->
    <div class=""form-section loading"">
        <div class=""la-ball-pulse"">
            <div></div>
            <div></div>
            <div></div>
        </div>
    </div>


    <!-- Funding Options -->
    <div class=""form-section fund-trn-recipient-option patterned active"" data-default=""true"">
        <div class=""head sub"">
            <a href=""#"" class=""btn btn-link"" data-dismiss=""modal"">
                <i class=""icon auto"">
                    <svg width=""16"" height=""15"" viewBox=""0 0 16 15"" fill=""none""
                         xmlns=""http://www.w3.org/2000/svg"">
                        <path d=""M16 6.61564V8.42502H3.98955L9.49477 13.4055L8.08057 14.6849L0.161133 7.52033L8.08057 0.355713L9.49477 1.63513L3.98955 6.61564H16Z""
                              fill=""#5D2684"" />
                    </svg>
                </i>
            </a>
            <h4>Fund Wallet</h4>
        </div>
        ");
            EndContext();
            BeginContext(1024, 4624, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03ce58c15ae978781531f2942d223f62c52b26464489", async() => {
                BeginContext(1030, 4611, true);
                WriteLiteral(@"
            <p>Select how you will like to fund your wallet</p>
            <div class=""select-list"">
                <div class=""item"" id=""FundButton"">
                    <i class=""icon auto"">
                        <svg width=""16"" height=""22"" viewBox=""0 0 16 22"" fill=""none""
                             xmlns=""http://www.w3.org/2000/svg"">
                            <path d=""M14 9.5H2C1.46957 9.5 0.960859 9.71071 0.585786 10.0858C0.210714 10.4609 0 10.9696 0 11.5V19.5C0 20.0304 0.210714 20.5391 0.585786 20.9142C0.960859 21.2893 1.46957 21.5 2 21.5H14C14.5304 21.5 15.0391 21.2893 15.4142 20.9142C15.7893 20.5391 16 20.0304 16 19.5V11.5C16 10.9696 15.7893 10.4609 15.4142 10.0858C15.0391 9.71071 14.5304 9.5 14 9.5ZM14 19.5H2V15.5H14V19.5ZM14 13.5H2V11.5H14V13.5ZM13 3.5V8.5H11.5V5H5.88L8.3 7.43L7.24 8.5L3 4.25L7.24 0L8.3 1.07L5.88 3.5H13Z""
                                  fill=""#FBB700"" />
                        </svg>
                    </i>
                    <p>
                        <stro");
                WriteLiteral(@"ng>Debit / Credit Card</strong>
                        <span class=""ellipsis"">Fund using a valid debit or credit card</span>
                    </p>
                    <i class=""icon auto arrow"">
                        <svg width=""14"" height=""14"" viewBox=""0 0 14 14"" fill=""none""
                             xmlns=""http://www.w3.org/2000/svg"">
                            <path d=""M0.333194 7.83343V6.16676L10.3419 6.16676L5.75419 1.57909L6.93269 0.400596L13.5322 7.00009L6.93269 13.5996L5.75419 12.4211L10.3419 7.83343L0.333194 7.83343Z""
                                  fill=""#2F1343"" />
                        </svg>
                    </i>
                </div>
                <div class=""item form-link"" data-link=""wallet-ussd"">
                    <i class=""icon auto"">
                        <svg width=""9"" height=""20"" viewBox=""0 0 9 20"" fill=""none""
                             xmlns=""http://www.w3.org/2000/svg"">
                            <path d=""M7 0C6.73478 0 6.48043 0.105357 6.29289 ");
                WriteLiteral(@"0.292893C6.10536 0.48043 6 0.734784 6 1V4H2C0.89 4 0 4.89 0 6V18C0 19.11 0.89 20 2 20H7C8.11 20 9 19.11 9 18V6C9 5.26 8.6 4.62 8 4.28V1C8 0.734784 7.89464 0.48043 7.70711 0.292893C7.51957 0.105357 7.26522 0 7 0ZM2 6H7V11H2V6ZM2 13H3V14H2V13ZM4 13H5V14H4V13ZM6 13H7V14H6V13ZM2 15H3V16H2V15ZM4 15H5V16H4V15ZM6 15H7V16H6V15ZM2 17H3V18H2V17ZM4 17H5V18H4V17ZM6 17H7V18H6V17Z""
                                  fill=""#FBB700"" />
                        </svg>
                    </i>
                    <p>
                        <strong>USSD</strong>
                        <span class=""ellipsis"">Transfer to your wallet using ussd codes</span>
                    </p>
                    <i class=""icon auto arrow"">
                        <svg width=""14"" height=""14"" viewBox=""0 0 14 14"" fill=""none""
                             xmlns=""http://www.w3.org/2000/svg"">
                            <path d=""M0.333194 7.83343V6.16676L10.3419 6.16676L5.75419 1.57909L6.93269 0.400596L13.5322 7.00009L6.93269 13.5996L5.7");
                WriteLiteral(@"5419 12.4211L10.3419 7.83343L0.333194 7.83343Z""
                                  fill=""#2F1343"" />
                        </svg>
                    </i>
                </div>
                <div class=""item form-link"" data-link=""wallet-bank-trn"">
                    <i class=""icon auto"">
                        <svg width=""20"" height=""16"" viewBox=""0 0 20 16"" fill=""none""
                             xmlns=""http://www.w3.org/2000/svg"">
                            <path d=""M13 10V7H16V5L20 8.5L16 12V10H13ZM12 3.7V5H0V3.7L6 0L12 3.7ZM5 6H7V11H5V6ZM1 6H3V11H1V6ZM11 6V8.5L9 10.3V6H11ZM7.1 12L6.5 12.5L8.2 14H0V12H7.1ZM15 11V14H12V16L8 12.5L12 9V11H15Z""
                                  fill=""#FBB700"" />
                        </svg>
                    </i>
                    <p>
                        <strong>Bank Transfer</strong>
                        <span class=""ellipsis"">Standard bank transfer to your wallet</span>
                    </p>
                    <i class=""icon auto arro");
                WriteLiteral(@"w"">
                        <svg width=""14"" height=""14"" viewBox=""0 0 14 14"" fill=""none""
                             xmlns=""http://www.w3.org/2000/svg"">
                            <path d=""M0.333194 7.83343V6.16676L10.3419 6.16676L5.75419 1.57909L6.93269 0.400596L13.5322 7.00009L6.93269 13.5996L5.75419 12.4211L10.3419 7.83343L0.333194 7.83343Z""
                                  fill=""#2F1343"" />
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
            BeginContext(5648, 741, true);
            WriteLiteral(@"
    </div>

    <!-- USSD Option -->
    <div class=""form-section wallet-ussd"">
        <div class=""head sub"">
            <a href=""#"" class=""btn btn-link form-link"" data-link=""fund-trn-recipient-option"">
                <i class=""icon auto"">
                    <svg width=""16"" height=""15"" viewBox=""0 0 16 15"" fill=""none""
                         xmlns=""http://www.w3.org/2000/svg"">
                        <path d=""M16 6.61564V8.42502H3.98955L9.49477 13.4055L8.08057 14.6849L0.161133 7.52033L8.08057 0.355713L9.49477 1.63513L3.98955 6.61564H16Z""
                              fill=""#5D2684"" />
                    </svg>
                </i>
            </a>
            <h4>Fund Wallet: USSD</h4>
        </div>
        ");
            EndContext();
            BeginContext(6389, 9372, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03ce58c15ae978781531f2942d223f62c52b264611576", async() => {
                BeginContext(6395, 86, true);
                WriteLiteral("\r\n            <p>Add funds to your wallet so you can perform tarnsactions with</p>\r\n\r\n");
                EndContext();
                BeginContext(7584, 8170, true);
                WriteLiteral(@"
            <p class=""m-0"">Select & Copy / Dial USSD Code</p>
            <div class=""select-list ussd"">
                <div class=""item"">
                    <p>
                        <span>Access Bank</span>
                        <strong>*901*1*200*45182930001#</strong>
                    </p>
                    <a href=""tel:*901*1*200*45182930001#"">
                        <i class=""icon auto check"">
                            <svg width=""19"" height=""19"" viewBox=""0 0 19 19"" fill=""none""
                                 xmlns=""http://www.w3.org/2000/svg"">
                                <path d=""M17.8333 13.1458C16.5833 13.1458 15.2292 12.9375 14.0833 12.5208H13.7708C13.4583 12.5208 13.25 12.625 13.0417 12.8333L10.75 15.125C7.83333 13.5625 5.33333 11.1667 3.875 8.25L6.16667 5.95833C6.47917 5.64583 6.58333 5.22917 6.375 4.91667C6.0625 3.77083 5.85417 2.41667 5.85417 1.16667C5.85417 0.645833 5.33333 0.125 4.8125 0.125H1.16667C0.645833 0.125 0.125 0.645833 0.125 1.16667C0.125 10.9583 8.0416");
                WriteLiteral(@"7 18.875 17.8333 18.875C18.3542 18.875 18.875 18.3542 18.875 17.8333V14.1875C18.875 13.6667 18.3542 13.1458 17.8333 13.1458ZM2.20833 2.20833H3.77083C3.875 3.14583 4.08333 4.08333 4.29167 4.91667L3.04167 6.16667C2.625 4.91667 2.3125 3.5625 2.20833 2.20833ZM16.7917 16.7917C15.4375 16.6875 14.0833 16.375 12.8333 15.9583L14.0833 14.7083C14.9167 14.9167 15.8542 15.125 16.7917 15.125V16.7917ZM12.625 0.125V1.6875H16.2708L10.5417 7.41667L11.5833 8.45833L17.3125 2.72917V6.375H18.875V0.125H12.625Z""
                                      fill=""#6AC895"" />
                            </svg>
                        </i>
                    </a>
                </div>
                <div class=""item"">
                    <p>
                        <span>FCMB Bank</span>
                        <strong>*329*200*45182930001#</strong>
                    </p>
                    <a href=""tel:*329*200*45182930001#"">
                        <i class=""icon auto check"">
                            <svg width=""19"" he");
                WriteLiteral(@"ight=""19"" viewBox=""0 0 19 19"" fill=""none""
                                 xmlns=""http://www.w3.org/2000/svg"">
                                <path d=""M17.8333 13.1458C16.5833 13.1458 15.2292 12.9375 14.0833 12.5208H13.7708C13.4583 12.5208 13.25 12.625 13.0417 12.8333L10.75 15.125C7.83333 13.5625 5.33333 11.1667 3.875 8.25L6.16667 5.95833C6.47917 5.64583 6.58333 5.22917 6.375 4.91667C6.0625 3.77083 5.85417 2.41667 5.85417 1.16667C5.85417 0.645833 5.33333 0.125 4.8125 0.125H1.16667C0.645833 0.125 0.125 0.645833 0.125 1.16667C0.125 10.9583 8.04167 18.875 17.8333 18.875C18.3542 18.875 18.875 18.3542 18.875 17.8333V14.1875C18.875 13.6667 18.3542 13.1458 17.8333 13.1458ZM2.20833 2.20833H3.77083C3.875 3.14583 4.08333 4.08333 4.29167 4.91667L3.04167 6.16667C2.625 4.91667 2.3125 3.5625 2.20833 2.20833ZM16.7917 16.7917C15.4375 16.6875 14.0833 16.375 12.8333 15.9583L14.0833 14.7083C14.9167 14.9167 15.8542 15.125 16.7917 15.125V16.7917ZM12.625 0.125V1.6875H16.2708L10.5417 7.41667L11.5833 8.45833L17.3125 2.72917V6.375");
                WriteLiteral(@"H18.875V0.125H12.625Z""
                                      fill=""#6AC895"" />
                            </svg>
                        </i>
                    </a>
                </div>
                <div class=""item"">
                    <p>
                        <span>Fidelity Bank</span>
                        <strong>*770*45182930001*200#</strong>
                    </p>
                    <a href=""tel:*770*45182930001*200#"">
                        <i class=""icon auto check"">
                            <svg width=""19"" height=""19"" viewBox=""0 0 19 19"" fill=""none""
                                 xmlns=""http://www.w3.org/2000/svg"">
                                <path d=""M17.8333 13.1458C16.5833 13.1458 15.2292 12.9375 14.0833 12.5208H13.7708C13.4583 12.5208 13.25 12.625 13.0417 12.8333L10.75 15.125C7.83333 13.5625 5.33333 11.1667 3.875 8.25L6.16667 5.95833C6.47917 5.64583 6.58333 5.22917 6.375 4.91667C6.0625 3.77083 5.85417 2.41667 5.85417 1.16667C5.85417 0.645833 5.33333 0.125");
                WriteLiteral(@" 4.8125 0.125H1.16667C0.645833 0.125 0.125 0.645833 0.125 1.16667C0.125 10.9583 8.04167 18.875 17.8333 18.875C18.3542 18.875 18.875 18.3542 18.875 17.8333V14.1875C18.875 13.6667 18.3542 13.1458 17.8333 13.1458ZM2.20833 2.20833H3.77083C3.875 3.14583 4.08333 4.08333 4.29167 4.91667L3.04167 6.16667C2.625 4.91667 2.3125 3.5625 2.20833 2.20833ZM16.7917 16.7917C15.4375 16.6875 14.0833 16.375 12.8333 15.9583L14.0833 14.7083C14.9167 14.9167 15.8542 15.125 16.7917 15.125V16.7917ZM12.625 0.125V1.6875H16.2708L10.5417 7.41667L11.5833 8.45833L17.3125 2.72917V6.375H18.875V0.125H12.625Z""
                                      fill=""#6AC895"" />
                            </svg>
                        </i>
                    </a>
                </div>
                <div class=""item"">
                    <p>
                        <span>First Bank</span>
                        <strong>*894*200*45182930001#</strong>
                    </p>
                    <a href=""tel:*894*200*45182930001#"">
            ");
                WriteLiteral(@"            <i class=""icon auto check"">
                            <svg width=""19"" height=""19"" viewBox=""0 0 19 19"" fill=""none""
                                 xmlns=""http://www.w3.org/2000/svg"">
                                <path d=""M17.8333 13.1458C16.5833 13.1458 15.2292 12.9375 14.0833 12.5208H13.7708C13.4583 12.5208 13.25 12.625 13.0417 12.8333L10.75 15.125C7.83333 13.5625 5.33333 11.1667 3.875 8.25L6.16667 5.95833C6.47917 5.64583 6.58333 5.22917 6.375 4.91667C6.0625 3.77083 5.85417 2.41667 5.85417 1.16667C5.85417 0.645833 5.33333 0.125 4.8125 0.125H1.16667C0.645833 0.125 0.125 0.645833 0.125 1.16667C0.125 10.9583 8.04167 18.875 17.8333 18.875C18.3542 18.875 18.875 18.3542 18.875 17.8333V14.1875C18.875 13.6667 18.3542 13.1458 17.8333 13.1458ZM2.20833 2.20833H3.77083C3.875 3.14583 4.08333 4.08333 4.29167 4.91667L3.04167 6.16667C2.625 4.91667 2.3125 3.5625 2.20833 2.20833ZM16.7917 16.7917C15.4375 16.6875 14.0833 16.375 12.8333 15.9583L14.0833 14.7083C14.9167 14.9167 15.8542 15.125 16.7917 15.125V16.");
                WriteLiteral(@"7917ZM12.625 0.125V1.6875H16.2708L10.5417 7.41667L11.5833 8.45833L17.3125 2.72917V6.375H18.875V0.125H12.625Z""
                                      fill=""#6AC895"" />
                            </svg>
                        </i>
                    </a>
                </div>
                <div class=""item"">
                    <p>
                        <span>GTB Bank</span>
                        <strong>*737*2*200*45182930001#</strong>
                    </p>
                    <a href=""tel:*737*2*200*45182930001#"">
                        <i class=""icon auto check"">
                            <svg width=""19"" height=""19"" viewBox=""0 0 19 19"" fill=""none""
                                 xmlns=""http://www.w3.org/2000/svg"">
                                <path d=""M17.8333 13.1458C16.5833 13.1458 15.2292 12.9375 14.0833 12.5208H13.7708C13.4583 12.5208 13.25 12.625 13.0417 12.8333L10.75 15.125C7.83333 13.5625 5.33333 11.1667 3.875 8.25L6.16667 5.95833C6.47917 5.64583 6.58333 5.22917 6.375");
                WriteLiteral(@" 4.91667C6.0625 3.77083 5.85417 2.41667 5.85417 1.16667C5.85417 0.645833 5.33333 0.125 4.8125 0.125H1.16667C0.645833 0.125 0.125 0.645833 0.125 1.16667C0.125 10.9583 8.04167 18.875 17.8333 18.875C18.3542 18.875 18.875 18.3542 18.875 17.8333V14.1875C18.875 13.6667 18.3542 13.1458 17.8333 13.1458ZM2.20833 2.20833H3.77083C3.875 3.14583 4.08333 4.08333 4.29167 4.91667L3.04167 6.16667C2.625 4.91667 2.3125 3.5625 2.20833 2.20833ZM16.7917 16.7917C15.4375 16.6875 14.0833 16.375 12.8333 15.9583L14.0833 14.7083C14.9167 14.9167 15.8542 15.125 16.7917 15.125V16.7917ZM12.625 0.125V1.6875H16.2708L10.5417 7.41667L11.5833 8.45833L17.3125 2.72917V6.375H18.875V0.125H12.625Z""
                                      fill=""#6AC895"" />
                            </svg>
                        </i>
                    </a>
                </div>
            </div>

            <div class=""action"">
                <a class=""btn btn-primary"" data-dismiss=""modal"">Continue</a>
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
            BeginContext(15761, 763, true);
            WriteLiteral(@"
    </div>

    <!-- Bank Transfer Option -->
    <div class=""form-section wallet-bank-trn"">
        <div class=""head sub"">
            <a href=""#"" class=""btn btn-link form-link"" data-link=""fund-trn-recipient-option"">
                <i class=""icon auto"">
                    <svg width=""16"" height=""15"" viewBox=""0 0 16 15"" fill=""none""
                         xmlns=""http://www.w3.org/2000/svg"">
                        <path d=""M16 6.61564V8.42502H3.98955L9.49477 13.4055L8.08057 14.6849L0.161133 7.52033L8.08057 0.355713L9.49477 1.63513L3.98955 6.61564H16Z""
                              fill=""#5D2684"" />
                    </svg>
                </i>
            </a>
            <h4>Fund Wallet: Bank Transfer</h4>
        </div>
        ");
            EndContext();
            BeginContext(16524, 945, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03ce58c15ae978781531f2942d223f62c52b264622553", async() => {
                BeginContext(16530, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 224 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Transaction\GoBackWallet.cshtml"
              
                var phone = User.FindFirst("phone")?.Value;
                var accountNumber = !string.IsNullOrEmpty(phone) ? phone.Substring(phone.Length - 10) : "";
             

#line default
#line hidden
                BeginContext(16732, 233, true);
                WriteLiteral(";\r\n            <p>Make a transfer into your wallet account using details provided below</p>\r\n\r\n            <div class=\"summary-card\">\r\n                <p>\r\n                    <span>Account Number</span>\r\n                    <strong>");
                EndContext();
                BeginContext(16966, 13, false);
#line 233 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Transaction\GoBackWallet.cshtml"
                       Write(accountNumber);

#line default
#line hidden
                EndContext();
                BeginContext(16979, 129, true);
                WriteLiteral("</strong>\r\n                </p>\r\n                <p>\r\n                    <span>Account Name</span>\r\n                    <strong>");
                EndContext();
                BeginContext(17109, 33, false);
#line 237 "C:\Users\Sadiq Oyapero\Documents\dev\flashmoneywebv2\FlashMoney\Views\Transaction\GoBackWallet.cshtml"
                       Write(User.FindFirst("fullname")?.Value);

#line default
#line hidden
                EndContext();
                BeginContext(17142, 320, true);
                WriteLiteral(@"</strong>
                </p>
                <p>
                    <span>Bank</span>
                    <strong>FCMB</strong>
                </p>
            </div>

            <div class=""action"">
                <a class=""btn btn-primary"" data-dismiss=""modal"">Continue</a>
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
            BeginContext(17469, 20, true);
            WriteLiteral("\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FlashMoney.Models.OverviewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
