#pragma checksum "C:\Users\emret\source\repos\NKUOtomayon\NKUOtomayon.WebUI\Views\Home\PdfByLesson.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "13a269d954f5598823883facc1acd0eeaa8b9e55"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_PdfByLesson), @"mvc.1.0.view", @"/Views/Home/PdfByLesson.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/PdfByLesson.cshtml", typeof(AspNetCore.Views_Home_PdfByLesson))]
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
#line 1 "C:\Users\emret\source\repos\NKUOtomayon\NKUOtomayon.WebUI\Views\_ViewImports.cshtml"
using NKUOtomayon.WebUI;

#line default
#line hidden
#line 2 "C:\Users\emret\source\repos\NKUOtomayon\NKUOtomayon.WebUI\Views\_ViewImports.cshtml"
using NKUOtomayon.WebUI.Models;

#line default
#line hidden
#line 4 "C:\Users\emret\source\repos\NKUOtomayon\NKUOtomayon.WebUI\Views\_ViewImports.cshtml"
using NKUOtomayon.WebUI.Extensions;

#line default
#line hidden
#line 5 "C:\Users\emret\source\repos\NKUOtomayon\NKUOtomayon.WebUI\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 6 "C:\Users\emret\source\repos\NKUOtomayon\NKUOtomayon.WebUI\Views\_ViewImports.cshtml"
using NKUOtomayon.WebUI.Identity;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"13a269d954f5598823883facc1acd0eeaa8b9e55", @"/Views/Home/PdfByLesson.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a1554a7218df22155fe29bd049877e15b3f16a5b", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_PdfByLesson : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<System.Collections.Generic.List<nkuotomasyon.entity.PdfFile>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(69, 21, true);
            WriteLiteral("\r\n<div class=\"row\">\r\n");
            EndContext();
#line 4 "C:\Users\emret\source\repos\NKUOtomayon\NKUOtomayon.WebUI\Views\Home\PdfByLesson.cshtml"
     if (Model != null)
    {

#line default
#line hidden
            BeginContext(122, 32, true);
            WriteLiteral("        <div class=\"col-md-8\">\r\n");
            EndContext();
#line 7 "C:\Users\emret\source\repos\NKUOtomayon\NKUOtomayon.WebUI\Views\Home\PdfByLesson.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(211, 73, true);
            WriteLiteral("                <div class=\"form-group row\">\r\n                    <label>");
            EndContext();
            BeginContext(285, 9, false);
#line 10 "C:\Users\emret\source\repos\NKUOtomayon\NKUOtomayon.WebUI\Views\Home\PdfByLesson.cshtml"
                      Write(item.Name);

#line default
#line hidden
            EndContext();
            BeginContext(294, 63, true);
            WriteLiteral("</label>\r\n                    <a class=\"btn btn-primary btn-sm\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 357, "\"", 373, 1);
#line 11 "C:\Users\emret\source\repos\NKUOtomayon\NKUOtomayon.WebUI\Views\Home\PdfByLesson.cshtml"
WriteAttributeValue("", 364, item.Url, 364, 9, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(374, 39, true);
            WriteLiteral(">Download</a>\r\n                </div>\r\n");
            EndContext();
#line 13 "C:\Users\emret\source\repos\NKUOtomayon\NKUOtomayon.WebUI\Views\Home\PdfByLesson.cshtml"
            }

#line default
#line hidden
            BeginContext(428, 16, true);
            WriteLiteral("        </div>\r\n");
            EndContext();
#line 15 "C:\Users\emret\source\repos\NKUOtomayon\NKUOtomayon.WebUI\Views\Home\PdfByLesson.cshtml"
    }

#line default
#line hidden
            BeginContext(451, 8, true);
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<System.Collections.Generic.List<nkuotomasyon.entity.PdfFile>> Html { get; private set; }
    }
}
#pragma warning restore 1591