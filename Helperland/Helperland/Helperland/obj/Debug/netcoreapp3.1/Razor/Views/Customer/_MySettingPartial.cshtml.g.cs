#pragma checksum "D:\Mayur\work\tatvasoft-trainee-work\Helperland\Helperland\Helperland\Views\Customer\_MySettingPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a2c1533a00dc00c0d01f0230205e786b080b4044"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer__MySettingPartial), @"mvc.1.0.view", @"/Views/Customer/_MySettingPartial.cshtml")]
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
#line 1 "D:\Mayur\work\tatvasoft-trainee-work\Helperland\Helperland\Helperland\Views\_ViewImports.cshtml"
using Helperland;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Mayur\work\tatvasoft-trainee-work\Helperland\Helperland\Helperland\Views\_ViewImports.cshtml"
using Helperland.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a2c1533a00dc00c0d01f0230205e786b080b4044", @"/Views/Customer/_MySettingPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"702ffd2b5be7ae0e459460177c8ec8b8df008652", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer__MySettingPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div");
            BeginWriteAttribute("class", " class=\"", 4, "\"", 12, 0);
            EndWriteAttribute();
            WriteLiteral(@">

    <ul class=""nav nav-tabs nav-justified"">
        <li class=""nav-item"">
            <a class=""nav-link active"" href=""javascript:;"" data-bs-toggle=""tab""  id=""MyDetails"">
                <span class=""first-span"">My Details</span>
                <span class=""icon details""></span>
            </a>
        </li>
        <li class=""nav-item"">
            <a class=""nav-link"" href=""javascript:;"" data-bs-toggle=""tab"" id=""MyAddress"">
                <span class=""first-span"">My Addresses</span>
                <span class=""icon address""></span>
            </a>

        </li>
        <li class=""nav-item"">
            <a class=""nav-link"" href=""javascript:;"" data-bs-toggle=""tab"" id=""Changepassword"">
                <span class=""first-span"">Change Password</span>
                <span class=""icon password""></span>
            </a>

        </li>
    </ul>

    <div class=""tab-content"">

        <div");
            BeginWriteAttribute("class", " class=\"", 944, "\"", 952, 0);
            EndWriteAttribute();
            WriteLiteral(" id=\"content-mydetails\">\r\n            \r\n        </div>\r\n    </div>\r\n\r\n</div>\r\n\r\n");
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
