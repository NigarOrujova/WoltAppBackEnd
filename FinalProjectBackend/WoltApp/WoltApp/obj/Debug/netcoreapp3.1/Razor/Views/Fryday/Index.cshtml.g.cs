#pragma checksum "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "99793e0a5ab5aab27db9d0c1cee2bb3fcf80c052"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Fryday_Index), @"mvc.1.0.view", @"/Views/Fryday/Index.cshtml")]
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
#line 1 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\_ViewImports.cshtml"
using WoltApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\_ViewImports.cshtml"
using WoltBusiness.DTOs;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\_ViewImports.cshtml"
using WoltEntity.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99793e0a5ab5aab27db9d0c1cee2bb3fcf80c052", @"/Views/Fryday/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4d0b4d4946a839ca0beeba8b79b5d57297e85b73", @"/Views/_ViewImports.cshtml")]
    public class Views_Fryday_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<RestaurantDTO>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<section id=\"products\">\r\n    <div class=\"container\">\r\n        <div class=\"row\">\r\n            <div class=\"col-lg-2 col-md-12\">\r\n                <div class=\"cardCategory\">\r\n                    <ul>\r\n");
#nullable restore
#line 12 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                         foreach (var item in Model.RestaurantCategories)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <li><a");
            BeginWriteAttribute("href", " href=\"", 398, "\"", 425, 2);
            WriteAttributeValue("", 405, "#", 405, 1, true);
#nullable restore
#line 14 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
WriteAttributeValue("", 406, item.Category.Name, 406, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 14 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                                                          Write(item.Category.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n");
#nullable restore
#line 15 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </ul>\r\n                </div>\r\n            </div>\r\n            <div class=\"col-lg-6 col-md-12\">\r\n");
#nullable restore
#line 20 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                 foreach (var pro in Model.RestaurantCategories)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"menu-item\">\r\n                        <div class=\"categoryName\">\r\n                            <h2");
            BeginWriteAttribute("id", " id=\"", 814, "\"", 837, 1);
#nullable restore
#line 24 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
WriteAttributeValue("", 819, pro.Category.Name, 819, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 24 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                                                   Write(pro.Category.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                        </div>\r\n                        <div class=\"product\">\r\n");
#nullable restore
#line 27 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                             foreach (var item in Model.RestaurantProducts.Where(p => p.Product.CategoryId == pro.CategoryId))
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div class=\"product-item\">\r\n                                    <div class=\"line\"></div>\r\n                                    <div class=\"card\" data-toggle=\"modal\" data-target=\"#mcProduct-");
#nullable restore
#line 31 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                                                                                             Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""">
                                        <div class=""row g-0"">
                                            <div class=""col-md-8"">
                                                <div class=""card-body"">
                                                    <h5 class=""card-title"">");
#nullable restore
#line 35 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                                                                      Write(item.Product.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                                                    <p class=\"card-text\">\r\n                                                        ");
#nullable restore
#line 37 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                                                   Write(item.Product.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                    </p>\r\n                                                    <p class=\"card-text\"><span>AZN ");
#nullable restore
#line 39 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                                                                              Write(item.Product.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span></p>
                                                </div>
                                            </div>
                                            <div class=""col-md-4"">
                                                <div class=""cardImg"">
                                                    <img");
            BeginWriteAttribute("src", " src=\"", 2273, "\"", 2314, 2);
            WriteAttributeValue("", 2279, "./assets/img/", 2279, 13, true);
#nullable restore
#line 44 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
WriteAttributeValue("", 2292, item.Product.ImageURL, 2292, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@"
                                                         class=""img-fluid rounded-start"" alt=""..."">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Modal -->
                                    <div class=""modal fade""");
            BeginWriteAttribute("id", " id=\"", 2729, "\"", 2752, 2);
            WriteAttributeValue("", 2734, "mcProduct-", 2734, 10, true);
#nullable restore
#line 51 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
WriteAttributeValue("", 2744, item.Id, 2744, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" tabindex=\"-1\" role=\"dialog\"");
            BeginWriteAttribute("aria-labelledby", "\r\n                                         aria-labelledby=\"", 2781, "\"", 2867, 2);
            WriteAttributeValue("", 2841, "exampleModalLabel-", 2841, 18, true);
#nullable restore
#line 52 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
WriteAttributeValue("", 2859, item.Id, 2859, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" aria-hidden=""true"">
                                        <div class=""modal-dialog modal-dialog-centered"" role=""document"">
                                            <div class=""modal-content"">
                                                <div class=""modal-header"">
                                                    <h5 class=""modal-title""");
            BeginWriteAttribute("id", " id=\"", 3220, "\"", 3251, 2);
            WriteAttributeValue("", 3225, "exampleModalLabel-", 3225, 18, true);
#nullable restore
#line 56 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
WriteAttributeValue("", 3243, item.Id, 3243, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                                        <img");
            BeginWriteAttribute("src", " src=\"", 3315, "\"", 3356, 2);
            WriteAttributeValue("", 3321, "./assets/img/", 3321, 13, true);
#nullable restore
#line 57 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
WriteAttributeValue("", 3334, item.Product.ImageURL, 3334, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@"
                                                             class=""img-fluid rounded-start"" alt=""..."">
                                                    </h5>
                                                    <button type=""button"" class=""close"" data-dismiss=""modal""
                                                            aria-label=""Close"">
                                                        <span aria-hidden=""true"">&times;</span>
                                                    </button>
                                                </div>
                                                <div class=""modal-body"">
                                                    <h5 class=""card-title"">");
#nullable restore
#line 66 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                                                                      Write(item.Product.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                                                    <p class=\"card-text\">\r\n                                                        ");
#nullable restore
#line 68 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                                                   Write(item.Product.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                    </p>\r\n                                                    <p class=\"card-text\"><span>AZN ");
#nullable restore
#line 70 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                                                                              Write(item.Product.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span></p>
                                                </div>
                                                <div class=""modal-footer"">
                                                    <button type=""button"" class=""btn btn-secondary""
                                                            data-dismiss=""modal"">
                                                        Close
                                                    </button>
                                                    <button type=""button"" class=""btn btn-primary"">Add basket</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
");
#nullable restore
#line 83 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"line\"></div>\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 87 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n            <div class=\"col-lg-4 col-md-12\">\r\n                <h2>Məkan məlumatları</h2>\r\n                <h3>Ünvan</h3>\r\n                <p>");
#nullable restore
#line 92 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
              Write(Model.Restaurant.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                <h3>Contact</h3>
                <p>
                    Allergiyanız və ya pəhrizlə bağlı məhdudiyyətləriniz varsa, restoran ilə əlaqə saxlayın.
                    Restoran sorğu əsasında yemək ilə bağlı məlumatlar təmin edəcək.
                </p>
                <div class=""phone-number"">
                    <a");
            BeginWriteAttribute("href", " href=\"", 5932, "\"", 5974, 2);
            WriteAttributeValue("", 5939, "tel:", 5939, 4, true);
#nullable restore
#line 99 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
WriteAttributeValue("", 5943, Model.Restaurant.ContactNumber, 5943, 31, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><span>");
#nullable restore
#line 99 "C:\Users\Nigar\Desktop\New folder (6)\FinalProjectBackend\WoltApp\WoltApp\Views\Fryday\Index.cshtml"
                                                                   Write(Model.Restaurant.ContactNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></a>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<RestaurantDTO> Html { get; private set; }
    }
}
#pragma warning restore 1591
