#pragma checksum "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Restaurant\Comment.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ddc679a3530875c184127bc0b81f9b7aa80a3707"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Restaurant_Comment), @"mvc.1.0.view", @"/Views/Restaurant/Comment.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ddc679a3530875c184127bc0b81f9b7aa80a3707", @"/Views/Restaurant/Comment.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"34e4055ed0ed037c99b5601413ee1febd5f2cade", @"/Views/_ViewImports.cshtml")]
    public class Views_Restaurant_Comment : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Comment>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Restaurant", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteComment", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-body"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<section id=""basketItem"" class=""h-100"" style=""background-color: #e1f5fe;"">
    <div class=""container py-5 h-100"">
        <div class=""row d-flex justify-content-center align-items-center h-100"">
            <div class=""col-12"">
                <div class=""card card-registration card-registration-2"" style=""border-radius: 15px;"">
                    <div class=""card-body p-0"">
                        <div class=""row g-0"">
                            <div class=""col-lg-12"">
                                <div class=""p-5"">
                                    <div class=""d-flex justify-content-between align-items-center mb-5"">
                                        <h1 class=""Shopcardtitle mb-0 text-black"">Comments</h1>
                                    </div>
                                    <hr class=""my-4"">
");
#nullable restore
#line 16 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Restaurant\Comment.cshtml"
                                     if (Model.Count != 0)
                                    {
                                        foreach (var comment in Model)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                            <div class=""row mb-4 d-flex justify-content-between align-items-center"">
                                                <div class=""media g-mb-30 media-comment"">
                                                    <img class=""d-flex g-width-50 g-height-50 rounded-circle g-mb-15"" src=""/Admin/assets/img/avatars/default-avatar.png"">
                                                    <div class=""media-body u-shadow-v18 g-bg-secondary px-3"">
                                                        <div class=""g-mb-15"">
                                                            <h5 class=""h5 g-color-gray-dark-v1 mb-0"">");
#nullable restore
#line 25 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Restaurant\Comment.cshtml"
                                                                                                Write(comment.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                                                            <p class=\" g-color-gray-dark-v1 mb-0\"></p>\r\n                                                            <span class=\"g-color-gray-dark-v4 g-font-size-12\">");
#nullable restore
#line 27 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Restaurant\Comment.cshtml"
                                                                                                         Write(comment.CreatedDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                                        </div>\r\n                                                        <p>\r\n                                                            ");
#nullable restore
#line 30 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Restaurant\Comment.cshtml"
                                                       Write(comment.Content);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                        </p>\r\n");
#nullable restore
#line 32 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Restaurant\Comment.cshtml"
                                                         if (comment.UserName == User.Identity.Name || User.IsInRole("Admin"))
                                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ddc679a3530875c184127bc0b81f9b7aa80a370710024", async() => {
                WriteLiteral("\r\n                                                                <button type=\"submit\" class=\"btn btn-pinterest\"> Delete </button>\r\n                                                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 34 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Restaurant\Comment.cshtml"
                                                                                                                                         WriteLiteral(comment.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 37 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Restaurant\Comment.cshtml"
                                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    </div>\r\n                                                </div>\r\n                                            </div>\r\n                                            <hr class=\"my-4\">\r\n");
#nullable restore
#line 42 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Restaurant\Comment.cshtml"
                                        }
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                        <div class=""d-flex justify-content-center w-100"">
                                            <h1 class=""Shopcardtitle mb-0 text-black"">No Comments</h1>
                                        </div>
                                        <hr class=""my-4"">
");
#nullable restore
#line 50 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Restaurant\Comment.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <div class=\"pt-5\">\r\n                                        <h6 class=\"mb-0\">\r\n                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ddc679a3530875c184127bc0b81f9b7aa80a370714854", async() => {
                WriteLiteral("\r\n                                                <i class=\"fas fa-long-arrow-alt-left me-2\"></i>Go Back\r\n                                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                        </h6>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Comment>> Html { get; private set; }
    }
}
#pragma warning restore 1591
