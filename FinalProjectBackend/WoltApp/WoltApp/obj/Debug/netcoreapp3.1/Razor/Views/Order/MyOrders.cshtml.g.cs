#pragma checksum "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Order\MyOrders.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "27336b57da873b234e9431b49f33c07236e34c7d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Order_MyOrders), @"mvc.1.0.view", @"/Views/Order/MyOrders.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27336b57da873b234e9431b49f33c07236e34c7d", @"/Views/Order/MyOrders.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"34e4055ed0ed037c99b5601413ee1febd5f2cade", @"/Views/_ViewImports.cshtml")]
    public class Views_Order_MyOrders : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<FullOrder>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Order\MyOrders.cshtml"
  
    ViewData["Title"] = "MyOrders";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container"">
    <div class=""row"">
        <div class=""col-lg-12"">
            <table class=""table table-striped"">
                <thead>
                    <tr>
                        <th scope=""col"">User FullName</th>
                        <th scope=""col"">Address</th>
                        <th scope=""col"">Order Item Count</th>
                        <th scope=""col"">Order Total</th>

                    </tr>
                </thead>
                <tbody>

");
#nullable restore
#line 21 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Order\MyOrders.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 24 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Order\MyOrders.cshtml"
                           Write(item.AppUser.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 25 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Order\MyOrders.cshtml"
                           Write(item.Adress);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 26 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Order\MyOrders.cshtml"
                           Write(item.Orders.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 27 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Order\MyOrders.cshtml"
                           Write(item.TotalCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n");
#nullable restore
#line 29 "C:\Users\Nigar\Desktop\New folder (7)\FinalProject\FinalProjectBackend\WoltApp\WoltApp\Views\Order\MyOrders.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<FullOrder>> Html { get; private set; }
    }
}
#pragma warning restore 1591
