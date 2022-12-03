#pragma checksum "/Users/alina/Desktop/GridCom/GridCom/Views/AverageUndersupply/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cb086329070c6db2c217d0e30d1d08e558465fe3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_AverageUndersupply_Index), @"mvc.1.0.view", @"/Views/AverageUndersupply/Index.cshtml")]
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
#line 1 "/Users/alina/Desktop/GridCom/GridCom/Views/_ViewImports.cshtml"
using GridCom;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/alina/Desktop/GridCom/GridCom/Views/_ViewImports.cshtml"
using GridCom.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cb086329070c6db2c217d0e30d1d08e558465fe3", @"/Views/AverageUndersupply/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"22b215b0637bac241fdd5b24576f5089d1862046", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_AverageUndersupply_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<GridCom.Models.Outage>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 2 "/Users/alina/Desktop/GridCom/GridCom/Views/AverageUndersupply/Index.cshtml"
   ViewData["Title"] = "Средний недоотпуск помесячно Буэс"; 

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>Средний недоотпуск помесячно Буэс</h1>\n");
            WriteLiteral("<br>\n<h3>За период:</h3>\n<br>\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cb086329070c6db2c217d0e30d1d08e558465fe33844", async() => {
                WriteLiteral(@"
    С: <input type=""date"" name=""startdate"" alt=""d"" />
    По: <input type=""date"" name=""enddate"" />
    <input type=""submit"" value=""Выбрать"" class=""btn btn-primary"" />
    <br>
    <br>
    <table class=""table"" id=""headerTable"">
        <thead>
            <tr>
                <th> РЭС </th>
                <th>  Дата отключения</th>
                <th>  Средний недоотпуск</th>

            </tr>
        </thead>
        <tbody id=""tbodyId"">
");
#nullable restore
#line 24 "/Users/alina/Desktop/GridCom/GridCom/Views/AverageUndersupply/Index.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                <tr>\n                    <td>\n                        ");
#nullable restore
#line 28 "/Users/alina/Desktop/GridCom/GridCom/Views/AverageUndersupply/Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.RES.Name_RES));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                    </td>\n                    <td>\n                        ");
#nullable restore
#line 31 "/Users/alina/Desktop/GridCom/GridCom/Views/AverageUndersupply/Index.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Date_outage));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                    </td>\n\n                    <td>\n                        ");
#nullable restore
#line 35 "/Users/alina/Desktop/GridCom/GridCom/Views/AverageUndersupply/Index.cshtml"
                   Write(Model.Where(m => m.RESID == item.RESID).Average(x => x.UnderSupply));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                    </td>\n                </tr>\n");
#nullable restore
#line 38 "/Users/alina/Desktop/GridCom/GridCom/Views/AverageUndersupply/Index.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </tbody>\n    </table>\n    <br>\n");
                WriteLiteral(@"    <div>
        <input type=""submit"" class=""btn btn-primary"" value=""Экспорт в Эксель"" onclick=""fnExcelReport();"" />
    </div>
    <br>


    <div style=""width:100%"" id=""printButton"">
        Построить график &raquo;
    </div>
    <canvas id=""myChart""></canvas>


");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"


<script src=""https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.3.2/chart.js""></script>
<script>
    const dataLabelsRemove = [];
    const dataElements = [];
    const data1 = document.querySelectorAll('tbody tr');
    Array.from(data1).forEach(item => {
        if (!dataLabelsRemove.includes(item.querySelector('td:first-child').innerHTML.trim())) {
            dataLabelsRemove.push(item.querySelector('td:first-child').innerHTML.trim());
            dataElements.push(item.querySelectorAll('td'));
        }
    });
    console.log(dataElements);
    document.getElementById('tbodyId').innerHTML = '';
    dataElements.forEach(item => {
        const id = 'id' + Math.floor(Math.random() * 1000);
        document.getElementById('tbodyId').insertAdjacentHTML('beforeend', `<tr id=${id}></tr>`);
        item.forEach(item => {
            document.getElementById('tbodyId').querySelector(`#${id}`).appendChild(item);
        })
    });

    document.getElementById('printButton').addEventListener('click', () => {
    ");
            WriteLiteral(@"    var ctx = document.getElementById('myChart').getContext('2d');
        const dataLabels = [];
        const dataValues = [];
        const data2 = document.querySelectorAll('tbody tr');
        Array.from(data2).forEach(item => {
            dataLabels.push(item.querySelector('td:first-child').innerHTML.trim());
            dataValues.push(item.querySelector('td:last-child').innerHTML.trim());
        });
        console.log(dataLabels);
        console.log(dataValues);
       
        var chart = new Chart(ctx, {
            type: 'bar',
            data:
            {
                labels: dataLabels,
                datasets: [{
                    label: ""Средний недоотпуск помесячно БуЭС"",
                    backgroundColor: 'rgb(0,145,215)',
                    borderColor: 'rgb(0,145,215)',

                    data: dataValues,
                }]
            },
            options: {}
        });
    })</script>


");
            DefineSection("scripts", async() => {
                WriteLiteral("\n");
                WriteLiteral(@"
    <script>function fnExcelReport() {

            var tab_text = ""<table border='2px'><tr bgcolor='#87AFC6'>"";

            var textRange; var j = 0;
            tab = document.getElementById('headerTable'); // id таблицы

            for (j = 0; j < tab.rows.length; j++) {
                tab_text = tab_text + tab.rows[j].innerHTML + ""</tr>"";

            }

            tab_text = tab_text + ""</table>"";

            var ua = window.navigator.userAgent;
            var msie = ua.indexOf(""MSIE "");

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // исключение, если браузер - Интернет Explorer
            {

                txtArea1.document.open(""txt/html"", ""replace"");
                txtArea1.document.write(tab_text);
                txtArea1.document.close();
                txtArea1.focus();
                sa = txtArea1.document.execCommand(""SaveAs"", true, ""Say Thanks to Sumit.xls"");
            }
            else {

                sa = window.open('data:application/v");
                WriteLiteral("nd.ms-excel,\' + encodeURIComponent(tab_text));\n\n            }\n            return (sa);\n        }</script>\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<GridCom.Models.Outage>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
