#pragma checksum "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "66a8327573525f7a9b1f3d050163c9473f3070b4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Prescriber_RegisteredPatients), @"mvc.1.0.view", @"/Views/Prescriber/RegisteredPatients.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Prescriber/RegisteredPatients.cshtml", typeof(AspNetCore.Views_Prescriber_RegisteredPatients))]
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
#line 1 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\_ViewImports.cshtml"
using EmcureCERI.Web;

#line default
#line hidden
#line 2 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\_ViewImports.cshtml"
using EmcureCERI.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"66a8327573525f7a9b1f3d050163c9473f3070b4", @"/Views/Prescriber/RegisteredPatients.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    public class Views_Prescriber_RegisteredPatients : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
  
    ViewData["Title"] = @SharedLocalizer["Registered Patients"].Value;

#line default
#line hidden
            BeginContext(171, 88, true);
            WriteLiteral("<section class=\"section\">\r\n    <div class=\"container-fluid\">\r\n        <h4>\r\n            ");
            EndContext();
            BeginContext(260, 49, false);
#line 8 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
       Write(SharedLocalizer["Registered Patients List"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(309, 332, true);
            WriteLiteral(@"
        </h4>
        <div class=""tableContainer clearfix"">
            <div class=""table-responsive"">
                <table id=""addPatientTableId"" class=""table stripe row-border order-column"" style=""width:100%"">
                    <thead>
                        <tr>
                            <th style=""width: 50px;"">");
            EndContext();
            BeginContext(642, 35, false);
#line 15 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                Write(SharedLocalizer["Patient Id"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(677, 39, true);
            WriteLiteral("</th>\r\n                            <th>");
            EndContext();
            BeginContext(717, 35, false);
#line 16 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                           Write(SharedLocalizer["First Name"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(752, 39, true);
            WriteLiteral("</th>\r\n                            <th>");
            EndContext();
            BeginContext(792, 34, false);
#line 17 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                           Write(SharedLocalizer["Last Name"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(826, 39, true);
            WriteLiteral("</th>\r\n                            <th>");
            EndContext();
            BeginContext(866, 31, false);
#line 18 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                           Write(SharedLocalizer["Status"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(897, 61, true);
            WriteLiteral("</th>\r\n                            <th style=\"width: 120px;\">");
            EndContext();
            BeginContext(959, 37, false);
#line 19 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                 Write(SharedLocalizer["Consent Form"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(996, 61, true);
            WriteLiteral("</th>\r\n                            <th style=\"width: 120px;\">");
            EndContext();
            BeginContext(1058, 38, false);
#line 20 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                 Write(SharedLocalizer["Baseline Form"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(1096, 59, true);
            WriteLiteral("</th>\r\n                            <th style=\"width:50px;\">");
            EndContext();
            BeginContext(1156, 31, false);
#line 21 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                               Write(SharedLocalizer["Action"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(1187, 224, true);
            WriteLiteral("</th>\r\n                        </tr>\r\n                    </thead>\r\n                </table>\r\n            </div>\r\n        </div>\r\n        <div class=\"text-right addRecordBtnContainer\">\r\n            <a class=\"btn btn-default\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 1411, "\"", 1479, 3);
            WriteAttributeValue("", 1421, "PopupForm(\'", 1421, 11, true);
#line 28 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
WriteAttributeValue("", 1432, Url.Action("PatientAddOrEdit","Prescriber"), 1432, 44, false);

#line default
#line hidden
            WriteAttributeValue("", 1476, "\');", 1476, 3, true);
            EndWriteAttribute();
            BeginContext(1480, 28, true);
            WriteLiteral("><i class=\"fa fa-plus\"></i> ");
            EndContext();
            BeginContext(1509, 36, false);
#line 28 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                                                                                                  Write(SharedLocalizer["Add Patient"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(1545, 630, true);
            WriteLiteral(@"</a>
        </div>

        <div id=""patientViewModal"" class=""modal themeModal "" role=""dialog"">
            <div class=""modal-dialog"">
                <!-- Modal content-->
                <div class=""modal-content"">
                    <div class=""modal-header"">
                        <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
                        <h4 class=""modal-title""></h4>
                    </div>
                    <div class=""modal-body"">
                        <p></p>
                    </div>
                </div>
            </div>
        </div>
        ");
            EndContext();
            BeginContext(2176, 49, false);
#line 45 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
   Write(await Html.PartialAsync("../Pdf/_PdfConsentForm"));

#line default
#line hidden
            EndContext();
            BeginContext(2225, 26, true);
            WriteLiteral("\r\n    </div>\r\n</section>\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(2269, 265, true);
                WriteLiteral(@"
<script>
        var Popup, dataTable, getPrescriber;
        $(document).ready(function () {

            var dataTable = $('#addPatientTableId').DataTable({
                responsive: true,
                language: {
                    ""emptyTable"": """);
                EndContext();
                BeginContext(2535, 51, false);
#line 56 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                              Write(SharedLocalizer["No data available in table"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(2586, 33, true);
                WriteLiteral("\",\r\n                    \"info\": \"");
                EndContext();
                BeginContext(2620, 68, false);
#line 57 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                        Write(SharedLocalizer["Showing _START_ to _END_ of _TOTAL_ entries"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(2688, 37, true);
                WriteLiteral("\",\r\n                    \"infoEmpty\":\"");
                EndContext();
                BeginContext(2726, 52, false);
#line 58 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                            Write(SharedLocalizer["Showing 0 to 0 of 0 entries"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(2778, 40, true);
                WriteLiteral("\",\r\n                    \"infoFiltered\":\"");
                EndContext();
                BeginContext(2819, 60, false);
#line 59 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                               Write(SharedLocalizer["(filtered from _MAX_ total entries)"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(2879, 38, true);
                WriteLiteral("\",\r\n                    \"lengthMenu\":\"");
                EndContext();
                BeginContext(2918, 44, false);
#line 60 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                             Write(SharedLocalizer["Show _MENU_ entries"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(2962, 43, true);
                WriteLiteral("\",\r\n                    \"loadingRecords\": \"");
                EndContext();
                BeginContext(3006, 35, false);
#line 61 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                  Write(SharedLocalizer["Loading..."].Value);

#line default
#line hidden
                EndContext();
                BeginContext(3041, 39, true);
                WriteLiteral("\",\r\n                    \"processing\": \"");
                EndContext();
                BeginContext(3081, 38, false);
#line 62 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                              Write(SharedLocalizer["Processing..."].Value);

#line default
#line hidden
                EndContext();
                BeginContext(3119, 35, true);
                WriteLiteral("\",\r\n                    \"search\": \"");
                EndContext();
                BeginContext(3155, 31, false);
#line 63 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                          Write(SharedLocalizer["Search"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(3186, 39, true);
                WriteLiteral("\",\r\n                    \"zeroRecords\":\"");
                EndContext();
                BeginContext(3226, 50, false);
#line 64 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                              Write(SharedLocalizer["No matching records found"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(3276, 72, true);
                WriteLiteral("\",\r\n                    \"paginate\": {\r\n                        \"first\":\"");
                EndContext();
                BeginContext(3349, 30, false);
#line 66 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                            Write(SharedLocalizer["First"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(3379, 36, true);
                WriteLiteral("\",\r\n                        \"last\":\"");
                EndContext();
                BeginContext(3416, 29, false);
#line 67 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                           Write(SharedLocalizer["Last"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(3445, 37, true);
                WriteLiteral("\",\r\n                        \"next\": \"");
                EndContext();
                BeginContext(3483, 29, false);
#line 68 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                            Write(SharedLocalizer["Next"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(3512, 40, true);
                WriteLiteral("\",\r\n                        \"previous\":\"");
                EndContext();
                BeginContext(3553, 33, false);
#line 69 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                               Write(SharedLocalizer["Previous"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(3586, 241, true);
                WriteLiteral("\"\r\n                    },\r\n                }\r\n            });\r\n\r\n            getPrescriber = function () {\r\n                $(\'#loaderContainer\').show();\r\n                $.ajax({\r\n                    type: \"GET\",\r\n                    url: \'");
                EndContext();
                BeginContext(3828, 38, false);
#line 78 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                     Write(Url.Action("GetPatient", "Prescriber"));

#line default
#line hidden
                EndContext();
                BeginContext(3866, 833, true);
                WriteLiteral(@"/',
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function (response) {
                        $('#loaderContainer').hide();
                        var jsonObject = response.data;
                        var result = jsonObject.map(function (item) {
                            var result = [];
                            result.push(item.UniqueId);
                            result.push(item.FirstName);
                            result.push(item.LastName);
                            result.push(item.FStatus);
                            if (item.CStatus != ""Pending"") {
                                if (item.IsConsentFcheckByAdmin) {
                                    result.push(""<a data-toggle='tooltip' title='");
                EndContext();
                BeginContext(4700, 33, false);
#line 92 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                                            Write(SharedLocalizer["Approved"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(4733, 209, true);
                WriteLiteral("\' class=\'btn btn-success btn-sm\'> <span class=\'glyphicon glyphicon-ok\' aria-hidden=\'true\'></span> </a >\");\r\n                                } else {\r\n                                    result.push(\"<a title=\'");
                EndContext();
                BeginContext(4943, 33, false);
#line 94 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                      Write(SharedLocalizer["Rejected"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(4976, 474, true);
                WriteLiteral(@"' class='btn btn-danger btn-sm'> <span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a >"");
                                }
                            } else {
                                result.push("""");
                            }
                            if (item.BStatus != ""Pending"") {
                                if (item.IsBaselineDataByAdmin) {
                                    result.push(""<a data-toggle='tooltip' title='");
                EndContext();
                BeginContext(5451, 33, false);
#line 101 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                                            Write(SharedLocalizer["Approved"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(5484, 209, true);
                WriteLiteral("\' class=\'btn btn-success btn-sm\'> <span class=\'glyphicon glyphicon-ok\' aria-hidden=\'true\'></span> </a >\");\r\n                                } else {\r\n                                    result.push(\"<a title=\'");
                EndContext();
                BeginContext(5694, 33, false);
#line 103 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                      Write(SharedLocalizer["Rejected"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(5727, 376, true);
                WriteLiteral(@"' class='btn btn-danger btn-sm'> <span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a >"");
                                }
                            } else {
                                result.push("""");
                            }
                           
                            result.push(""<a class='btn btn-default btn-sm'  title='");
                EndContext();
                BeginContext(6104, 37, false);
#line 109 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                                              Write(SharedLocalizer["View Details"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(6141, 8, true);
                WriteLiteral("\' href=\'");
                EndContext();
                BeginContext(6150, 47, false);
#line 109 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                                                                                            Write(Url.Action("InformedConsentForm", "Prescriber"));

#line default
#line hidden
                EndContext();
                BeginContext(6197, 462, true);
                WriteLiteral(@"/"" + item.Id + ""' ><span class='glyphicon glyphicon-list-alt'></span></a >"");
                            return result;
                        });
                        dataTable.clear();
                        dataTable.rows.add(result); // add to DataTable instance
                        dataTable.draw(); // always redraw
                    },
                    failure: function () {
                        $(""#addPatientTableId"").append('");
                EndContext();
                BeginContext(6660, 78, false);
#line 117 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                   Write(SharedLocalizer["Error when fetching data please contact administrator"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(6738, 571, true);
                WriteLiteral(@"');
                    }
                });
            }
            getPrescriber();
        });

        var specialElementHandlers = {
            // element with id of ""bypass"" - jQuery style selector
            '.no-export': function (element, renderer) {
                // true = ""handled elsewhere, bypass text extraction""
                return true;
            }
        };

        function PopupForm(url) {
            $.get(url)
                .done(function (response) {
                    $('#patientViewModal .modal-title').html('");
                EndContext();
                BeginContext(7310, 46, false);
#line 135 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                         Write(SharedLocalizer["Informed Consent Form"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(7356, 1029, true);
                WriteLiteral(@"');
                    $('#patientViewModal .modal-body').html(response);
                    $('#patientViewModal').modal('show');
                });
        }

        function SubmitForm(form) {
            $.validator.unobtrusive.parse(form);
            if ($(form).valid()) {
                $.ajax({
                    type : ""POST"",
                    url : form.action,
                    data : $(form).serialize(),
                    success: function (result) {
                        if (result.success === ""success"") {
                            console.log(result.data);
                            var value = result.data;
                            //debugger;
                            //alert(value.Id);
                            $('#firstName').html(value.FirstName);
                            $('#lastName').html(value.LastName);
                            if (value.RfirstName == null || value.RfirstName == """") {
                                $('#rfirstName').h");
                WriteLiteral("tml(\'");
                EndContext();
                BeginContext(8386, 39, false);
#line 157 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                  Write(SharedLocalizer["Non Applicable"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(8425, 582, true);
                WriteLiteral(@"');
                            } else {
                                $('#rfirstName').html(value.RfirstName);
                            }
                            if (value.RejectionReason == null || value.RejectionReason == """") {
                                $('#dobId').html('');
                            } else {
                                $('#dobId').html(value.RejectionReason);
                            }
                            if (value.RlastName == null || value.RlastName == """") {
                                $('#rlastName').html('");
                EndContext();
                BeginContext(9008, 39, false);
#line 167 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                 Write(SharedLocalizer["Non Applicable"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(9047, 310, true);
                WriteLiteral(@"');
                            } else {
                                $('#rlastName').html(value.RlastName);
                            }
                            $('#date').text(value.Point1Date);
                            if (value.Point1) {
                                $('#point1').html('");
                EndContext();
                BeginContext(9358, 27, false);
#line 173 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                              Write(SharedLocalizer["Ok"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(9385, 136, true);
                WriteLiteral("\');\r\n                            }\r\n                            if (value.Point2) {\r\n                                $(\'#point2\').html(\'");
                EndContext();
                BeginContext(9522, 27, false);
#line 176 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                              Write(SharedLocalizer["Ok"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(9549, 136, true);
                WriteLiteral("\');\r\n                            }\r\n                            if (value.Point3) {\r\n                                $(\'#point3\').html(\'");
                EndContext();
                BeginContext(9686, 27, false);
#line 179 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                              Write(SharedLocalizer["Ok"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(9713, 136, true);
                WriteLiteral("\');\r\n                            }\r\n                            if (value.Point4) {\r\n                                $(\'#point4\').html(\'");
                EndContext();
                BeginContext(9850, 27, false);
#line 182 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                              Write(SharedLocalizer["Ok"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(9877, 136, true);
                WriteLiteral("\');\r\n                            }\r\n                            if (value.Point5) {\r\n                                $(\'#point5\').html(\'");
                EndContext();
                BeginContext(10014, 27, false);
#line 185 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                              Write(SharedLocalizer["Ok"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(10041, 136, true);
                WriteLiteral("\');\r\n                            }\r\n                            if (value.Point6) {\r\n                                $(\'#point6\').html(\'");
                EndContext();
                BeginContext(10178, 27, false);
#line 188 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                              Write(SharedLocalizer["Ok"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(10205, 136, true);
                WriteLiteral("\');\r\n                            }\r\n                            if (value.Point7) {\r\n                                $(\'#point7\').html(\'");
                EndContext();
                BeginContext(10342, 27, false);
#line 191 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                              Write(SharedLocalizer["Ok"].Value);

#line default
#line hidden
                EndContext();
                BeginContext(10369, 450, true);
                WriteLiteral(@"');
                            }
                             uploadPDF(value.Id)
                           // exportPDF(""informed-consent-form"", ""InformedConsentForm.pdf"");
                            $('#patientViewModal').modal('hide');
                            //dataTable.ajax.reload();
                            getPrescriber();
                           
                           // openCommonModal('successModal modal-sm', '");
                EndContext();
                BeginContext(10820, 42, false);
#line 199 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                                   Write(Html.Raw(SharedLocalizer["Success"].Value));

#line default
#line hidden
                EndContext();
                BeginContext(10862, 4, true);
                WriteLiteral("\', \'");
                EndContext();
                BeginContext(10867, 70, false);
#line 199 "D:\E Drive\Nilesh_Jain\Clients\Emcure_Pharma\NeoSOFTRepo\NPD\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\RegisteredPatients.cshtml"
                                                                                                                  Write(Html.Raw(SharedLocalizer["Record has been added successfully."].Value));

#line default
#line hidden
                EndContext();
                BeginContext(10937, 134, true);
                WriteLiteral("\', false);\r\n                        }\r\n                    }\r\n                });\r\n            }\r\n            return false;\r\n    }\r\n\r\n");
                EndContext();
                BeginContext(11532, 4, true);
                WriteLiteral("\r\n\r\n");
                EndContext();
                BeginContext(11985, 11, true);
                WriteLiteral("</script>\r\n");
                EndContext();
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.Extensions.Localization.IStringLocalizer<SharedResource> SharedLocalizer { get; private set; }
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