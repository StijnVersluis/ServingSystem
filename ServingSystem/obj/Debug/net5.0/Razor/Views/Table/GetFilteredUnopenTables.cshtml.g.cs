#pragma checksum "C:\Users\stijn\source\repos\ServingSystem\ServingSystem\Views\Table\GetFilteredUnopenTables.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9e66d79135fc50e44b61cfc486dd8aff6ba2f28b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Table_GetFilteredUnopenTables), @"mvc.1.0.view", @"/Views/Table/GetFilteredUnopenTables.cshtml")]
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
#line 1 "C:\Users\stijn\source\repos\ServingSystem\ServingSystem\Views\_ViewImports.cshtml"
using ServingSystem;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\stijn\source\repos\ServingSystem\ServingSystem\Views\_ViewImports.cshtml"
using ServingSystem.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9e66d79135fc50e44b61cfc486dd8aff6ba2f28b", @"/Views/Table/GetFilteredUnopenTables.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1f8aef7a1b69383fd54064aa13b095e53a1e2051", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Table_GetFilteredUnopenTables : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<TableViewModel>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\stijn\source\repos\ServingSystem\ServingSystem\Views\Table\GetFilteredUnopenTables.cshtml"
  
    Layout = "";

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\stijn\source\repos\ServingSystem\ServingSystem\Views\Table\GetFilteredUnopenTables.cshtml"
 foreach (TableViewModel table in Model)
{
    

#line default
#line hidden
#nullable disable
            WriteLiteral("<button data-toggle=\"modal\" data-target=\"#NewTableModal\" class=\"btn list-group-item text-left\" data-id=\"");
#nullable restore
#line 7 "C:\Users\stijn\source\repos\ServingSystem\ServingSystem\Views\Table\GetFilteredUnopenTables.cshtml"
                                                                                                             Write(table.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-name=\"");
#nullable restore
#line 7 "C:\Users\stijn\source\repos\ServingSystem\ServingSystem\Views\Table\GetFilteredUnopenTables.cshtml"
                                                                                                                                   Write(table.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">");
#nullable restore
#line 7 "C:\Users\stijn\source\repos\ServingSystem\ServingSystem\Views\Table\GetFilteredUnopenTables.cshtml"
                                                                                                                                                Write(table.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</button>");
#nullable restore
#line 7 "C:\Users\stijn\source\repos\ServingSystem\ServingSystem\Views\Table\GetFilteredUnopenTables.cshtml"
                                                                                                                                                                                
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<TableViewModel>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
