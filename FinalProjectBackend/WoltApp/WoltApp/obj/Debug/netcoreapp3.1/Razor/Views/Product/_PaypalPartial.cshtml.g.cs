#pragma checksum "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Product\_PaypalPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "13ce86729301a42722f0147220e852ac9f02670d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product__PaypalPartial), @"mvc.1.0.view", @"/Views/Product/_PaypalPartial.cshtml")]
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
#nullable restore
#line 1 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\_ViewImports.cshtml"
using WoltApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\_ViewImports.cshtml"
using WoltBusiness.DTOs;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\_ViewImports.cshtml"
using WoltBusiness.DTOs.Account;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\_ViewImports.cshtml"
using WoltBusiness.DTOs.Basket;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\_ViewImports.cshtml"
using WoltEntity.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"13ce86729301a42722f0147220e852ac9f02670d", @"/Views/Product/_PaypalPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"34e4055ed0ed037c99b5601413ee1febd5f2cade", @"/Views/_ViewImports.cshtml")]
    public class Views_Product__PaypalPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("https://www.paypal.com/us/cgi-bin/webscr"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Product\_PaypalPartial.cshtml"
  
int count=1;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "13ce86729301a42722f0147220e852ac9f02670d4977", async() => {
                WriteLiteral("\r\n    <input type=\"hidden\" name=\"cmd\" value=\"_xclick\">\r\n    <input type=\"hidden\" name=\"business\" value=\"nigarorucova2002@gmail.com\">\r\n\r\n");
#nullable restore
#line 11 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Product\_PaypalPartial.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
                WriteLiteral("        <input type=\"hidden\"");
                BeginWriteAttribute("name", " name=\"", 302, "\"", 325, 2);
                WriteAttributeValue("", 309, "item_name_", 309, 10, true);
#nullable restore
#line 13 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Product\_PaypalPartial.cshtml"
WriteAttributeValue("", 319, count, 319, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("value", " value=\"", 326, "\"", 344, 1);
#nullable restore
#line 13 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Product\_PaypalPartial.cshtml"
WriteAttributeValue("", 334, item.Name, 334, 10, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n        <input type=\"hidden\"");
                BeginWriteAttribute("name", " name=\"", 376, "\"", 396, 2);
                WriteAttributeValue("", 383, "amount_", 383, 7, true);
#nullable restore
#line 14 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Product\_PaypalPartial.cshtml"
WriteAttributeValue("", 390, count, 390, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("value", " value=\"", 397, "\"", 416, 1);
#nullable restore
#line 14 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Product\_PaypalPartial.cshtml"
WriteAttributeValue("", 405, item.Price, 405, 11, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n");
#nullable restore
#line 15 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Product\_PaypalPartial.cshtml"
        count++;
    }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n    <input type=\"hidden\" name=\"currency_code\" value=\"USD\">\r\n    <input type=\"image\" src=\"http://www.paypal.com/en_US/i/btn/x-click-but01.gif\" name=\"submit\" alt=\"Make payments with PayPal - it\'s fast, free and secure!\">\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
