#pragma checksum "D:\PROGRAMMING\TISLR2\TISLR2\Views\Home\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5f0abc2f65fb1ecd66c6fcb635a295a358ca839b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Details), @"mvc.1.0.view", @"/Views/Home/Details.cshtml")]
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
#line 1 "D:\PROGRAMMING\TISLR2\TISLR2\Views\_ViewImports.cshtml"
using TISLR2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\PROGRAMMING\TISLR2\TISLR2\Views\_ViewImports.cshtml"
using TISLR2.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5f0abc2f65fb1ecd66c6fcb635a295a358ca839b", @"/Views/Home/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"856bb63eafa4d4eb5d4307304d4d0d71f74e53f4", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TISLR2.Models.User>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\PROGRAMMING\TISLR2\TISLR2\Views\Home\Details.cshtml"
  
    ViewBag.Title = Model.Name;

#line default
#line hidden
#nullable disable
            WriteLiteral("<h2>Пользователь ");
#nullable restore
#line 5 "D:\PROGRAMMING\TISLR2\TISLR2\Views\Home\Details.cshtml"
            Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n<div>\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>Идентификатор</dt>\r\n        <dd>");
#nullable restore
#line 9 "D:\PROGRAMMING\TISLR2\TISLR2\Views\Home\Details.cshtml"
       Write(Model.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt>Номер телефона</dt>\r\n        <dd>");
#nullable restore
#line 11 "D:\PROGRAMMING\TISLR2\TISLR2\Views\Home\Details.cshtml"
       Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt>Электронная почта</dt>\r\n        <dd>");
#nullable restore
#line 13 "D:\PROGRAMMING\TISLR2\TISLR2\Views\Home\Details.cshtml"
       Write(Model.Phone);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n        <dt>Возраст</dt>\r\n        <dd>");
#nullable restore
#line 15 "D:\PROGRAMMING\TISLR2\TISLR2\Views\Home\Details.cshtml"
       Write(Model.Age);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\r\n    </dl>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TISLR2.Models.User> Html { get; private set; }
    }
}
#pragma warning restore 1591