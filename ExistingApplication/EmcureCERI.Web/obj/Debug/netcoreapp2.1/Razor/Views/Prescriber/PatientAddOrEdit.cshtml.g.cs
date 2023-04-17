#pragma checksum "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e20897194957621f05fda9a1e5a01b6f1bc8ceea"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Prescriber_PatientAddOrEdit), @"mvc.1.0.view", @"/Views/Prescriber/PatientAddOrEdit.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Prescriber/PatientAddOrEdit.cshtml", typeof(AspNetCore.Views_Prescriber_PatientAddOrEdit))]
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
#line 1 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\_ViewImports.cshtml"
using EmcureCERI.Web;

#line default
#line hidden
#line 2 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\_ViewImports.cshtml"
using EmcureCERI.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e20897194957621f05fda9a1e5a01b6f1bc8ceea", @"/Views/Prescriber/PatientAddOrEdit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e39e5b73b640da0f63a8b55154c17e2e753e04bc", @"/Views/_ViewImports.cshtml")]
    public class Views_Prescriber_PatientAddOrEdit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EmcureCERI.Web.Models.PrescriberViewModels.PatientViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
  

    Layout = null;

#line default
#line hidden
            BeginContext(189, 123, true);
            WriteLiteral("<style type=\"text/css\">\r\n    .field-validation-error {\r\n        color: #ff0000;\r\n        display: block;\r\n    }\r\n</style>\r\n");
            EndContext();
#line 13 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
 using (Html.BeginForm("PatientAddOrEdit", "Prescriber", FormMethod.Post, new { onsubmit = "return SubmitForm(this)" })) 
{
    

#line default
#line hidden
            BeginContext(443, 23, false);
#line 15 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
            EndContext();
            BeginContext(468, 165, true);
            WriteLiteral("    <div class=\"row\">\r\n        <div class=\"col-sm-4\">\r\n            <label>Date</label>\r\n            <div class=\'input-group\' id=\"Point1DatePicker\">\r\n                ");
            EndContext();
            BeginContext(634, 84, false);
#line 20 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.TextBoxFor(model => model.Point1Date, new { @class = "form-control input-sm" }));

#line default
#line hidden
            EndContext();
            BeginContext(718, 201, true);
            WriteLiteral("\r\n                <span class=\"input-group-addon\" id=\"openDatePicker\">\r\n                    <span class=\"glyphicon glyphicon-calendar\"></span>\r\n                </span>\r\n            </div>\r\n            ");
            EndContext();
            BeginContext(920, 88, false);
#line 25 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
       Write(Html.ValidationMessageFor(model => model.Point1Date, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(1008, 44, true);
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <p><label>");
            EndContext();
            BeginContext(1053, 56, false);
#line 28 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
         Write(SharedLocalizer["Documentation of the Informed Consent"]);

#line default
#line hidden
            EndContext();
            BeginContext(1109, 14, true);
            WriteLiteral("</label></p>\r\n");
            EndContext();
            BeginContext(1125, 242, true);
            WriteLiteral("    <ul class=\"list-group\">\r\n        <li class=\"list-group-item\">\r\n            <div class=\"flex-container\">\r\n                <div class=\"listSrNo\">(i)</div>\r\n                <div class=\"listContent\" style=\"flex-grow: 8\">\r\n                    ");
            EndContext();
            BeginContext(1368, 88, false);
#line 35 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
               Write(SharedLocalizer["Print the patient information sheet and informed consent form  "].Value);

#line default
#line hidden
            EndContext();
            BeginContext(1456, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(1694, 20, true);
            WriteLiteral("                    ");
            EndContext();
            BeginContext(1715, 67, false);
#line 38 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
               Write(Html.ActionLink("Download", "Getpaintemnt_infosheet", "Prescriber"));

#line default
#line hidden
            EndContext();
            BeginContext(1782, 26, true);
            WriteLiteral("\r\n                </div>\r\n");
            EndContext();
            BeginContext(2218, 251, true);
            WriteLiteral("            </div>\r\n        </li>\r\n\r\n        <li class=\"list-group-item\">\r\n            <div class=\"flex-container\">\r\n                <div class=\"listSrNo\">(ii)</div>\r\n                <div class=\"listContent\" style=\"flex-grow: 8\">\r\n                    ");
            EndContext();
            BeginContext(2470, 79, false);
#line 55 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
               Write(SharedLocalizer["insert prescriber’s name and contact number on page 1 "].Value);

#line default
#line hidden
            EndContext();
            BeginContext(2549, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(2626, 24, true);
            WriteLiteral("                </div>\r\n");
            EndContext();
            BeginContext(3060, 250, true);
            WriteLiteral("            </div>\r\n        </li>\r\n        <li class=\"list-group-item\">\r\n            <div class=\"flex-container\">\r\n                <div class=\"listSrNo\">(iii)</div>\r\n                <div class=\"listContent\" style=\"flex-grow: 8\">\r\n                    ");
            EndContext();
            BeginContext(3311, 102, false);
#line 72 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
               Write(SharedLocalizer["insert patient’s name on page 8 and, if applicable, the name of the guardian "].Value);

#line default
#line hidden
            EndContext();
            BeginContext(3413, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(3490, 24, true);
            WriteLiteral("                </div>\r\n");
            EndContext();
            BeginContext(3924, 249, true);
            WriteLiteral("            </div>\r\n        </li>\r\n        <li class=\"list-group-item\">\r\n            <div class=\"flex-container\">\r\n                <div class=\"listSrNo\">(iv)</div>\r\n                <div class=\"listContent\" style=\"flex-grow: 8\">\r\n                    ");
            EndContext();
            BeginContext(4174, 177, false);
#line 89 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
               Write(SharedLocalizer["Ask patient or guardian to read the patient information sheet and then sign it at the bottom of each page where indicated and also in the box on page 8 "].Value);

#line default
#line hidden
            EndContext();
            BeginContext(4351, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(4428, 24, true);
            WriteLiteral("                </div>\r\n");
            EndContext();
            BeginContext(4862, 248, true);
            WriteLiteral("            </div>\r\n        </li>\r\n        <li class=\"list-group-item\">\r\n            <div class=\"flex-container\">\r\n                <div class=\"listSrNo\">(v)</div>\r\n                <div class=\"listContent\" style=\"flex-grow: 8\">\r\n                    ");
            EndContext();
            BeginContext(5111, 107, false);
#line 106 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
               Write(SharedLocalizer["scan the document and upload it using the upload button at the bottom of this page"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(5218, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(5295, 24, true);
            WriteLiteral("                </div>\r\n");
            EndContext();
            BeginContext(5729, 35, true);
            WriteLiteral("            </div>\r\n        </li>\r\n");
            EndContext();
            BeginContext(7576, 11, true);
            WriteLiteral("    </ul>\r\n");
            EndContext();
            BeginContext(7995, 12, true);
            WriteLiteral("    <br />\r\n");
            EndContext();
            BeginContext(8013, 14, true);
            WriteLiteral("    <p><label>");
            EndContext();
            BeginContext(8028, 44, false);
#line 159 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
         Write(SharedLocalizer["Name of the patient"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(8072, 123, true);
            WriteLiteral("</label></p>\r\n    <div class=\"row\">\r\n        <div class=\"col-sm-6\">\r\n            <div class=\"form-group\">\r\n                ");
            EndContext();
            BeginContext(8196, 73, false);
#line 163 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.LabelFor(model => model.FirstName, new { @class = "control-label" }));

#line default
#line hidden
            EndContext();
            BeginContext(8269, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(8288, 107, false);
#line 164 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.EditorFor(model => model.FirstName, new { htmlAttributes = new { @class = "form-control input-sm" } }));

#line default
#line hidden
            EndContext();
            BeginContext(8395, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(8414, 87, false);
#line 165 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.ValidationMessageFor(model => model.FirstName, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(8501, 124, true);
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n        <div class=\"col-sm-6\">\r\n            <div class=\"form-group\">\r\n                ");
            EndContext();
            BeginContext(8626, 72, false);
#line 170 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.LabelFor(model => model.LastName, new { @class = "control-label" }));

#line default
#line hidden
            EndContext();
            BeginContext(8698, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(8717, 106, false);
#line 171 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.EditorFor(model => model.LastName, new { htmlAttributes = new { @class = "form-control input-sm" } }));

#line default
#line hidden
            EndContext();
            BeginContext(8823, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(8842, 86, false);
#line 172 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.ValidationMessageFor(model => model.LastName, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(8928, 124, true);
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-sm-6\">\r\n            <label>");
            EndContext();
            BeginContext(9053, 38, false);
#line 178 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
              Write(SharedLocalizer["Date of Birth"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(9091, 88, true);
            WriteLiteral("</label>\r\n            <div class=\'input-group\' id=\"DateOfBirthPicker\">\r\n                ");
            EndContext();
            BeginContext(9180, 85, false);
#line 180 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.TextBoxFor(model => model.DateOfBirth, new { @class = "form-control input-sm" }));

#line default
#line hidden
            EndContext();
            BeginContext(9265, 201, true);
            WriteLiteral("\r\n                <span class=\"input-group-addon\" id=\"openDatePicker\">\r\n                    <span class=\"glyphicon glyphicon-calendar\"></span>\r\n                </span>\r\n            </div>\r\n            ");
            EndContext();
            BeginContext(9467, 89, false);
#line 185 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
       Write(Html.ValidationMessageFor(model => model.DateOfBirth, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(9556, 104, true);
            WriteLiteral("\r\n        </div>\r\n        <div class=\"col-sm-6\">\r\n            <div class=\"form-group\">\r\n                ");
            EndContext();
            BeginContext(9661, 41, false);
#line 189 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.Label(@SharedLocalizer["Age"].Value));

#line default
#line hidden
            EndContext();
            BeginContext(9702, 140, true);
            WriteLiteral("\r\n                <div class=\"dummyInputBox\" id=\"calcAge\"></div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <br />\r\n    <p><label>");
            EndContext();
            BeginContext(9843, 72, false);
#line 195 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
         Write(SharedLocalizer["Name of the legal representative, if applicable"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(9915, 123, true);
            WriteLiteral("</label></p>\r\n    <div class=\"row\">\r\n        <div class=\"col-sm-6\">\r\n            <div class=\"form-group\">\r\n                ");
            EndContext();
            BeginContext(10039, 74, false);
#line 199 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.LabelFor(model => model.RFirstName, new { @class = "control-label" }));

#line default
#line hidden
            EndContext();
            BeginContext(10113, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(10132, 108, false);
#line 200 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.EditorFor(model => model.RFirstName, new { htmlAttributes = new { @class = "form-control input-sm" } }));

#line default
#line hidden
            EndContext();
            BeginContext(10240, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(10259, 88, false);
#line 201 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.ValidationMessageFor(model => model.RFirstName, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(10347, 124, true);
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n        <div class=\"col-sm-6\">\r\n            <div class=\"form-group\">\r\n                ");
            EndContext();
            BeginContext(10472, 73, false);
#line 206 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.LabelFor(model => model.RLastName, new { @class = "control-label" }));

#line default
#line hidden
            EndContext();
            BeginContext(10545, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(10564, 107, false);
#line 207 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.EditorFor(model => model.RLastName, new { htmlAttributes = new { @class = "form-control input-sm" } }));

#line default
#line hidden
            EndContext();
            BeginContext(10671, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(10690, 87, false);
#line 208 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
           Write(Html.ValidationMessageFor(model => model.RLastName, "", new { @class = "text-danger" }));

#line default
#line hidden
            EndContext();
            BeginContext(10777, 50, true);
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
            EndContext();
            BeginContext(10829, 94, true);
            WriteLiteral("    <div class=\"row\">\r\n        <div class=\"col-sm-12 text-center\">\r\n            <br /><br />\r\n");
            EndContext();
            BeginContext(11081, 65, true);
            WriteLiteral("            <button name=\"submit\" class=\"btn btn-default btn-sm\">");
            EndContext();
            BeginContext(11147, 53, false);
#line 217 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
                                                            Write(SharedLocalizer["Informed Consent Form Upload"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(11200, 316, true);
            WriteLiteral(@"</button>
        </div>
    </div>
    <script type=""text/javascript"">


    function uploadPDF(id) {
        
            var url = ""/Prescriber/PatientUploadDocument/""+ id;
            $.get(url)
                .done(function (response) {
                    $('#patientViewModal .modal-title').html('");
            EndContext();
            BeginContext(11517, 53, false);
#line 228 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
                                                         Write(SharedLocalizer["Informed Consent Form Upload"].Value);

#line default
#line hidden
            EndContext();
            BeginContext(11570, 4947, true);
            WriteLiteral(@"');
                    $('#patientViewModal .modal-body').html(response);
                    $('#patientViewModal').modal('show');
                });
        }

        $(function () {
            $('#Point1DatePicker').datetimepicker({
                maxDate: moment(),
                format: 'DD-MM-YYYY'
            });
        });
        $('#Point1DatePicker').on('dp.change', function (e) {
            $('.dispDate').text($('#Point1Date').val());
        })

        $(function () {
            $('#DateOfBirthPicker').datetimepicker({
                maxDate: moment(),
                format: 'DD-MM-YYYY'
            });
        });
        $('#DateOfBirthPicker').on('dp.change', function (e) {
            $('#calcAge').html(getAge(changeDateFormat($('#DateOfBirth').val())));
        })

        // extend jquery range validator to work for required checkboxes
        var defaultRangeValidator = $.validator.methods.range;
        $.validator.methods.range = function (value, e");
            WriteLiteral(@"lement, param) {
            if (element.type === 'checkbox') {
                // if it's a checkbox return true if it is checked
                return element.checked;
            } else {
                // otherwise run the default validation function
                return defaultRangeValidator.call(this, value, element, param);
            }
        }

        function changeDateFormat(inputDate) {  // expects Y-m-d
            var splitDate = inputDate.split('-');
            if (splitDate.count == 0) {
                return null;
            }

            var day = splitDate[0];
            var month = splitDate[1];
            var year = splitDate[2];

            return month + '\\' + day + '\\' + year;
        }

        // ************************************
        function getAge(dateString) {
            var now = new Date();
            var today = new Date(now.getYear(), now.getMonth(), now.getDate());

            var yearNow = now.getYear();
            var ");
            WriteLiteral(@"monthNow = now.getMonth();
            var dateNow = now.getDate();

            var dob = new Date(dateString.substring(6, 10),
                dateString.substring(0, 2) - 1,
                dateString.substring(3, 5)
            );
           // alert(""dob "" + dob);
            var yearDob = dob.getYear();
            var monthDob = dob.getMonth();
            var dateDob = dob.getDate();
            var age = {};
            var ageString = """";
            var yearString = """";
            var monthString = """";
            var dayString = """";


            yearAge = yearNow - yearDob;

            if (monthNow >= monthDob)
                var monthAge = monthNow - monthDob;
            else {
                yearAge--;
                var monthAge = 12 + monthNow - monthDob;
            }

            if (dateNow >= dateDob)
                var dateAge = dateNow - dateDob;
            else {
                monthAge--;
                var dateAge = 31 + dateNow - dateDob;

");
            WriteLiteral(@"                if (monthAge < 0) {
                    monthAge = 11;
                    yearAge--;
                }
            }

            age = {
                years: yearAge,
                months: monthAge,
                days: dateAge
            };

            if (age.years > 1) yearString = "" years"";
            else yearString = "" year"";
            if (age.months > 1) monthString = "" months"";
            else monthString = "" month"";
            if (age.days > 1) dayString = "" days"";
            else dayString = "" day"";


            if ((age.years > 0) && (age.months > 0) && (age.days > 0))
                ageString = age.years + yearString + "", "" + age.months + monthString + "", and "" + age.days + dayString + "" old."";
            else if ((age.years == 0) && (age.months == 0) && (age.days > 0))
                ageString = ""Only "" + age.days + dayString + "" old!"";
            else if ((age.years > 0) && (age.months == 0) && (age.days == 0))
                ageStrin");
            WriteLiteral(@"g = age.years + yearString + "" old. Happy Birthday!!"";
            else if ((age.years > 0) && (age.months > 0) && (age.days == 0))
                ageString = age.years + yearString + "" and "" + age.months + monthString + "" old."";
            else if ((age.years == 0) && (age.months > 0) && (age.days > 0))
                ageString = age.months + monthString + "" and "" + age.days + dayString + "" old."";
            else if ((age.years > 0) && (age.months == 0) && (age.days > 0))
                ageString = age.years + yearString + "" and "" + age.days + dayString + "" old."";
            else if ((age.years == 0) && (age.months > 0) && (age.days == 0))
                ageString = age.months + monthString + "" old."";
            else ageString = ""Oops! Could not calculate age!"";

            return ageString;
        }
    </script>
");
            EndContext();
#line 357 "D:\work\EmcureGITRepo\EmcureNPD\ExistingApplication\EmcureCERI.Web\Views\Prescriber\PatientAddOrEdit.cshtml"
}

#line default
#line hidden
            BeginContext(16520, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EmcureCERI.Web.Models.PrescriberViewModels.PatientViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
