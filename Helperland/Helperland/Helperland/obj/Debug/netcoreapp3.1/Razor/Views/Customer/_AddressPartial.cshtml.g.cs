#pragma checksum "D:\Mayur\work\tatvasoft-trainee-work\Helperland\Helperland\Helperland\Views\Customer\_AddressPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e8388781e2abfc54312c7c3f008f29531ab492a9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer__AddressPartial), @"mvc.1.0.view", @"/Views/Customer/_AddressPartial.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e8388781e2abfc54312c7c3f008f29531ab492a9", @"/Views/Customer/_AddressPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"702ffd2b5be7ae0e459460177c8ec8b8df008652", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer__AddressPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<UserAddress>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Images/edit-icon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Images/delete-icon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<div class=\"table-responsive row mx-0 mb-3\">\r\n    <div id=\"SuccessAddressDetails\" class=\"alert alert-success d-none\" role=\"alert\">\r\n\r\n    </div>\r\n    <table");
            BeginWriteAttribute("class", " class=\"", 193, "\"", 201, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n        <thead>\r\n            <tr>\r\n\r\n                <th>Addresses</th>\r\n                <th class=\"text-center\">Action</th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 17 "D:\Mayur\work\tatvasoft-trainee-work\Helperland\Helperland\Helperland\Views\Customer\_AddressPartial.cshtml"
             foreach (UserAddress address in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td class=\"contact\">\r\n                        <span>\r\n                            <b>Address:</b> ");
#nullable restore
#line 22 "D:\Mayur\work\tatvasoft-trainee-work\Helperland\Helperland\Helperland\Views\Customer\_AddressPartial.cshtml"
                                       Write(address.AddressLine1);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 22 "D:\Mayur\work\tatvasoft-trainee-work\Helperland\Helperland\Helperland\Views\Customer\_AddressPartial.cshtml"
                                                             Write(address.AddressLine2);

#line default
#line hidden
#nullable disable
            WriteLiteral(", ");
#nullable restore
#line 22 "D:\Mayur\work\tatvasoft-trainee-work\Helperland\Helperland\Helperland\Views\Customer\_AddressPartial.cshtml"
                                                                                    Write(address.PostalCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ");
#nullable restore
#line 22 "D:\Mayur\work\tatvasoft-trainee-work\Helperland\Helperland\Helperland\Views\Customer\_AddressPartial.cshtml"
                                                                                                         Write(address.City);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </span>\r\n                        <span>\r\n\r\n                            <b>Phone number:</b> ");
#nullable restore
#line 26 "D:\Mayur\work\tatvasoft-trainee-work\Helperland\Helperland\Helperland\Views\Customer\_AddressPartial.cshtml"
                                            Write(address.Mobile);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </span>\r\n                    </td>\r\n                    <td class=\"text-center action-buttons\">\r\n                        <a title=\"Edit\" data-from=\"old\" data-id=\"");
#nullable restore
#line 30 "D:\Mayur\work\tatvasoft-trainee-work\Helperland\Helperland\Helperland\Views\Customer\_AddressPartial.cshtml"
                                                            Write(address.AddressId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" onclick=\"showPartial(this)\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "e8388781e2abfc54312c7c3f008f29531ab492a97541", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </a>\r\n                        <a title=\"Delete\" data-from=\"del\" data-id=\"");
#nullable restore
#line 33 "D:\Mayur\work\tatvasoft-trainee-work\Helperland\Helperland\Helperland\Views\Customer\_AddressPartial.cshtml"
                                                              Write(address.AddressId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" onclick=\"showPartial(this)\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "e8388781e2abfc54312c7c3f008f29531ab492a99133", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </a>\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 38 "D:\Mayur\work\tatvasoft-trainee-work\Helperland\Helperland\Helperland\Views\Customer\_AddressPartial.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n\r\n</div>\r\n<a class=\"btn btn-primary add-button\" title=\"Add New Address\" data-from=\"new\" onclick=\"showPartial(this)\">Add New Address</a>\r\n\r\n<div id=\"holdpartialForAddress\">\r\n\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<UserAddress>> Html { get; private set; }
    }
}
#pragma warning restore 1591
