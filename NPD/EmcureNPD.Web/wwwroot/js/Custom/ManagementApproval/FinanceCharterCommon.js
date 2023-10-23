var SelectedTabBU_Finance = '';
var CommcercialBU_NPS_MS_Data = [];

function RenderCommercialPerPack() {
    $('#tblCommercialPerPack').html('');

    let html = "";
    let _startDate = $('#ProjectStartDate').val();
    let _launchDate = $('#ProductLaunchDate').val();
    if (_startDate != "" && _launchDate != "") {
        let _ProjectStartDate = new Date(_startDate);
        let _ProjectLaunchDate = new Date(_launchDate);
        let tentativeStartDate = _ProjectStartDate;

        tentativeStartDate.setMonth(_ProjectStartDate.getMonth() - 3);

        let _FinancialYearStartDate = new Date(tentativeStartDate.getFullYear() + "/03/31");
        let _BeginYear = tentativeStartDate.getFullYear();
        if (_FinancialYearStartDate > tentativeStartDate) {
            _BeginYear = tentativeStartDate.getFullYear() - 1;
        }

        var _financialYearLaunchDate = new Date(_ProjectLaunchDate.getFullYear() + "/03/31");
        var _SalesBeginYear = _financialYearLaunchDate.getFullYear();
        if (_financialYearLaunchDate < _ProjectLaunchDate) {
            _SalesBeginYear = _ProjectLaunchDate.getFullYear() + 1;
        }

        html += '<thead class="bg-primary">';
        html += '<tr><th colspan="4">Commercials per pack</th><th></th>';
        var counter = 0;
        for (var i = 0; i < 10; i++) {
            if ((_BeginYear + i) >= _SalesBeginYear) {
                counter++;
            }
            html += "<th class='thYearCounter'>" + (counter > 0 ? counter : "-") + "</th>";
        }
        html += "</tr>";

        html += "<tr><th>Scenarios</th><th class='scenario1'>Low</th><th class='scenario2'>Medium</th><th class='scenario3'>High</th><th></th>";
        for (var i = 0; i < 10; i++) {
            html += "<th>" + "Mar-" + (_BeginYear + i).toString().substr(-2) + "</th>";
        }
        html += "</tr>";
        html += "</thead><tbody>";

        html += '<tr><td colspan="4"></td><td>Revenue Months</td>';
        for (var i = 0; i < 10; i++) {
            var _projectionDate = new Date((_BeginYear + i) + "/03/31");
            const diffTime = Math.abs(_projectionDate - _ProjectLaunchDate);
            const diffMonths = Math.round(diffTime / (1000 * 60 * 60 * 24 * 30));
            html += "<td class='trRevenueMonths'>" + ((_projectionDate < _ProjectLaunchDate) ? "0" : (diffMonths > 12 ? 12 : diffMonths)) + "</td>";
        }
        html += "</tr>";
        $('#tblCommercialPerPack').html(html);
        SumOfSales = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
        SumOfCOGS = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
        SumOfGC = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];


        //-----------BU loop--------
        var ArrOfUniqueBUs = Array.from(new Set(CommcercialBU_NPS_MS_Data.map((item) => item.businessUnitId)))
        if (SelectedTabBU_Finance != 0) {
            ArrOfUniqueBUs = [SelectedTabBU_Finance];
        }
        jQuery.each(ArrOfUniqueBUs, function (BUind, BUObj) {

            var Arr_FinanceTable_tr = GetBatchSizeCostingTRValues();
            jQuery.each(Arr_FinanceTable_tr, function (index, trObj) {
                //$('#FinanceTableBoy tr').each(function (index, value) {

                let SKU = trObj.SKU;
                let PackSize = trObj.PackSize;
                let _BUName = '';

                if (_selectBusinessUnit != SelectedTabBU_Finance) {

                    var Filtered_CommcercialBU_NPS_MS_Data = $.grep(CommcercialBU_NPS_MS_Data, function (n, i) {
                        return n.businessUnitId == SelectedTabBU_Finance
                    });
                    
                    trObj.hdnMSLow = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].marketSharePercentageLow : 0
                    trObj.hdnMSMid = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].marketSharePercentageMedium : 0
                    trObj.hdnMSHigh = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].marketSharePercentageHigh : 0
                    trObj.marketInPacks = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].marketinpacks : 0
                    trObj.hdnNSPLow = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].nspUnitsLow : 0
                    trObj.hdnNSPMid = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].nspUnitsMedium : 0
                    trObj.hdnNSPHigh = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].nspUnitsHigh : 0

                }

                if (ScreenName == 'ManagementApproval' && SelectedTabBU_Finance=='0') {
                    var BU = BUObj;
                    var Filtered_CommcercialBU_NPS_MS_Data = $.grep(CommcercialBU_NPS_MS_Data, function (n, i) {
                        return n.businessUnitId == BU
                    });
                    _BUName = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].businessUnitName : ''
                    trObj.hdnMSLow = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].marketSharePercentageLow : 0
                    trObj.hdnMSMid = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].marketSharePercentageMedium : 0
                    trObj.hdnMSHigh = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].marketSharePercentageHigh : 0
                    trObj.marketInPacks = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].marketinpacks : 0
                    trObj.hdnNSPLow = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].nspUnitsLow : 0
                    trObj.hdnNSPMid = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].nspUnitsMedium : 0
                    trObj.hdnNSPHigh = (Filtered_CommcercialBU_NPS_MS_Data.length > 0) ? Filtered_CommcercialBU_NPS_MS_Data[0].nspUnitsHigh : 0

                }
                if (_BUName != '')
                    _BUName = '-' + _BUName;

                html += '<tr class="bg-light"><td class="text-left" colspan="15"><b>' + SKU + " - " + PackSize + _BUName +'</b></td></tr>';

                var _uniqueClass = "tr_" + SKU + "_" + PackSize;
                var MS_td_data = [];
                var NSP_td_data = [];
                var COGS_td_data = [];
                /* ---------------Get values from Commercial Module--------------------------------------------------------*/


                //---------------------Start-MS%_Row-------------------------------------------------------
                html += "<tr class='" + _uniqueClass + "'><td>MS%</td><td>" + trObj.hdnMSLow + "</td><td>" + trObj.hdnMSMid + "</td><td>" + trObj.hdnMSHigh + "</td><td>Units</td>";

                var marketSharePercentage = GetMarketSharePercentage(trObj.hdnMSLow, trObj.hdnMSMid, trObj.hdnMSHigh);
                let marketInPacks = formatToNumber(trObj.marketInPacks);
                let msErosion = formatToNumber($("#MarketShareErosionrate").val(), true);

                for (var i = 0; i < 10; i++) {
                    var Row_th_Index = $('#tblCommercialPerPack').find('.thYearCounter:eq(' + i + ')').text();
                    var trYearIndex = formatToNumber((Row_th_Index == '-') ? '0' : Row_th_Index);
                    var trRevenueMonths = formatToNumber($('#tblCommercialPerPack').find('.trRevenueMonths:eq(' + i + ')').text());
                    let _units = ((marketInPacks * (marketSharePercentage / 100)) * (Math.pow((1 - (msErosion / 100)), trYearIndex))) * (trRevenueMonths / 12);
                    _units = isNaN(_units) ? 0 : _units;
                    html += "<td>" + _units.toFixed() + "</td>";
                    MS_td_data.push(_units);
                }
                html += "</tr>";
                //---------------------End-MS%_Row-------------------------------------------------------   
                //---------------------Start-NSP_Row-------------------------------------------------------
                html += "<tr class='" + _uniqueClass + "'><td>NSP</td><td>" + trObj.hdnNSPLow + "</td><td>" + trObj.hdnNSPMid + "</td><td>" + trObj.hdnNSPHigh + "</td><td>NSP</td>";
                var nsp_val = GetNSPPercentage(trObj.hdnNSPLow, trObj.hdnNSPMid, trObj.hdnNSPHigh);
                // let marketInPacks = formatToNumber($(this).find('.Marketinpacks').val());
                let nsp_Erosion = formatToNumber($("#PriceErosion").val(), true);

                for (var i = 0; i < 10; i++) {
                    var Row_th_Index = $('#tblCommercialPerPack').find('.thYearCounter:eq(' + i + ')').text();
                    var trYearIndex = formatToNumber((Row_th_Index == '-') ? '0' : Row_th_Index);
                    let _units = (nsp_val) * (Math.pow((1 - (nsp_Erosion / 100)), trYearIndex))
                    _units = isNaN(_units) ? 0 : _units;
                    html += "<td>" + _units.toFixed(3) + "</td>";
                    NSP_td_data.push(_units);
                }
                html += "</tr>";
                //---------------------End-NSP_Row-------------------------------------------------------
                //---------------------Start-COGS_Row-------------------------------------------------------
                html += "<tr class='" + _uniqueClass + "'><td>COGS</td><td>" + trObj.emcureCOGs_pack + "</td><td>" + trObj.emcureCOGs_pack + "</td><td>" + trObj.emcureCOGs_pack + "</td><td>COGS/Unit</td>";
                let COGS_Escalation = formatToNumber($("#EscalationinCOGS").val(), true);
                var COGS_val = GetCOGSPercentage(trObj.emcureCOGs_pack, trObj.emcureCOGs_pack, trObj.emcureCOGs_pack);
                for (var i = 0; i < 10; i++) {
                    var Row_th_Index = $('#tblCommercialPerPack').find('.thYearCounter:eq(' + i + ')').text();
                    var trYearIndex = formatToNumber((Row_th_Index == '-') ? '0' : Row_th_Index);

                    let _units = (COGS_val) * (Math.pow((1 + (COGS_Escalation / 100)), trYearIndex))

                    _units = isNaN(_units) ? 0 : _units;
                    html += "<td>" + _units.toFixed(3) + "</td>";
                    COGS_td_data.push(_units);
                }
                html += "</tr>";
                //---------------------END-COGS_Row-------------------------------------------------------
                //--------------------Sales-------------------------------------------------------
                var Sales_data = [];
                html += "<tr class='" + _uniqueClass + "'><td></td><td></td><td></td><td></td><td>Sales</td>";
                for (var i = 0; i < 10; i++) {
                    let result = MS_td_data[i] * NSP_td_data[i];

                    result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
                    Sales_data.push(result);
                    SumOfSales[i] = SumOfSales[i] + result;
                }
                html += "</tr>";
                //--------------------COGS-------------------------------------------------------
                var COGS_data = [];
                html += "<tr class='" + _uniqueClass + "'><td></td><td></td><td></td><td></td><td>COGS</td>";
                for (var i = 0; i < 10; i++) {
                    let result = MS_td_data[i] * COGS_td_data[i];
                    result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
                    COGS_data.push(result);
                    SumOfCOGS[i] = SumOfCOGS[i] + result;
                }
                html += "</tr>";
                //--------------------GC-------------------------------------------------------
                var GC_data = [];
                html += "<tr class='" + _uniqueClass + "'><td></td><td></td><td></td><td></td><td>GC</td>";
                for (var i = 0; i < 10; i++) {
                    let result = Sales_data[i] - COGS_data[i];
                    result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
                    GC_data.push(result);
                    SumOfGC[i] = SumOfGC[i] + result;
                }
                html += "</tr>";
                //--------------------GC%-------------------------------------------------------
                html += "<tr class='" + _uniqueClass + "'><td></td><td></td><td></td><td></td><td>GC%</td>";
                for (var i = 0; i < 10; i++) {
                    let result = GC_data[i] / Sales_data[i];
                    result = (Math.abs(result) == Infinity || isNaN(result)) ? 0 : result;
                    result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(0) + "</td>";

                }
                html += "</tr>";
            });
            //--------Bu loop----------
        });

        /* -----------Grand SUM-Rows--------------------*/
        html += "<tr><td>&nbsp</td></tr>";
        /*---------------Sales------------------------------*/
        html += "<tr class='SumOfSales bg-lightgreen'><td></td><td></td><td></td><td></td><td><b>Sales</b></td>";
        for (var i = 0; i < 10; i++) {
            let result = SumOfSales[i];
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        /*---------------COGS--------------------------*/
        html += "<tr class='SumOfCOGS bg-lightgreen'><td></td><td></td><td></td><td></td><td><b>COGS</b></td>";
        for (var i = 0; i < 10; i++) {
            let result = SumOfCOGS[i];
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        /*-----------------GC------------------------*/
        html += "<tr class='SumOfGC bg-lightgreen'><td></td><td></td><td></td><td></td><td><b>GC</b></td>";
        for (var i = 0; i < 10; i++) {
            let result = SumOfGC[i];
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";

        if ($('#FinanceTableBoy tr').length > 0) {

        }
        html += "</tbody>";
    }
    $('#tblCommercialPerPack').html(html);
}
function RenderFinanceProjection() {
    $('#tblFinanceProjection').html('');

    let html = "";
    let _startDate = $('#ProjectStartDate').val();
    let _launchDate = $('#ProductLaunchDate').val();
    if (_startDate != "" && _launchDate != "") {
        let _ProjectStartDate = new Date(_startDate);
        let _ProjectLaunchDate = new Date(_launchDate);
        let tentativeStartDate = _ProjectStartDate;

        tentativeStartDate.setMonth(_ProjectStartDate.getMonth() - 3);

        let _FinancialYearStartDate = new Date(tentativeStartDate.getFullYear() + "/03/31");
        let _BeginYear = tentativeStartDate.getFullYear();
        if (_FinancialYearStartDate > tentativeStartDate) {
            _BeginYear = tentativeStartDate.getFullYear() - 1;
        }

        var _financialYearLaunchDate = new Date(_ProjectLaunchDate.getFullYear() + "/03/31");
        var _SalesBeginYear = _financialYearLaunchDate.getFullYear();
        if (_financialYearLaunchDate < _ProjectLaunchDate) {
            _SalesBeginYear = _ProjectLaunchDate.getFullYear() + 1;
        }


        html += '<thead class="bg-primary">';
        html += '<tr><th colspan="3">Finance Projection</th>';
        var counter = 0;
        for (var i = 0; i < 10; i++) {
            if ((_BeginYear + i) >= _SalesBeginYear) {
                counter++;
            }
            html += "<th class='thYearCounter'>" + (counter > 0 ? counter : "-") + "</th>";
        }
        html += "</tr>";
        var Projection_Year_data = [];
        html += "<tr><th colspan='3' >NPV calculations CAD</th>";
        for (var i = 0; i < 10; i++) {
            html += "<th>" + "Mar-" + (_BeginYear + i).toString().substr(-2) + "</th>";
            Global_Projection_Year_data.push((_BeginYear + i).toString());
            Projection_Year_data.push(new Date(((_BeginYear + i).toString() + '-03-31').toString()));
        }

        html += "</tr>";
        html += "</thead><tbody>";

        html += '<tr><td colspan="3" >#Operating Months</td>';
        var Operating_Months_Projection_data = [];
        for (var i = 0; i < 10; i++) {
            var _projectionDate = new Date((_BeginYear + i) + "/03/31");
            const diffTime = Math.abs(_projectionDate - _ProjectLaunchDate);
            const diffMonths = Math.round(diffTime / (1000 * 60 * 60 * 24 * 30));
            let result = ((_projectionDate < _ProjectLaunchDate) ? "0" : (diffMonths > 12 ? 12 : diffMonths));
            Operating_Months_Projection_data.push(parseInt(result));
            html += "<td class='trRevenueMonths'>" + result + "</td>";
        }
        html += "</tr>";
        $('#tblFinanceProjection').html(html);
        var SumOfGrossSales = SumOfSales;
        /*---------------Gross Sales--------------------------*/
        html += "<tr class=''><td colspan='3'>Gross Sales</td>";
        for (var i = 0; i < 10; i++) {
            let result = SumOfGrossSales[i];
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        /*---------------Net Sales--------------------------*/
        html += "<tr class='bg-light'><td colspan='3' ><b>Net Sales</b></td>";
        for (var i = 0; i < 10; i++) {
            let result = SumOfSales[i];
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
        }
        /*------------[COGS]-Cost of goods sold--------------------------*/
        html += "<tr class='bg-light'><td colspan='3' ><b><b>Cost of goods sold</b></td>";
        for (var i = 0; i < 10; i++) {
            let result = SumOfCOGS[i];
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        /*---------------Expiries-----------------------*/
        html += "<tr class='Expiries'><td colspan='3' ><b>Expiries</b></td>";
        for (var i = 0; i < 10; i++) {
            html += "<td> <input onchange='ExpiriesValueChange(this," + i + ");' type='number' name='" + Global_Projection_Year_data[i] + "' value='" + Expiries_Yearwise_Data[i] + "' class='form-control Expiriestxtbox UpdateProjectionCommercial' id='ProjectionExpiries_" + i + "' > </td>";
        }
        html += "</tr>";
        /*-----------[GC]-Gross margin--------------------------*/
        var GC_Projection = [];
        html += "<tr class='lblHeading'><td colspan='3' ><b>Gross margin</b></td>";
        for (var i = 0; i < 10; i++) {
            let result = SumOfSales[i] - (SumOfCOGS[i] + formatToNumber(Expiries_Yearwise_Data[i], true));
            result = (isNaN(result) ? 0 : result)
            html += "<td>" + result.toFixed(3) + "</td>";
            GC_Projection.push(result);
        }
        html += "</tr>";
        /*-----------[GC%]-Gross margin%--------------------------*/
        var CGPercentage = [];
        html += "<tr class='lblHeading percentage'><td colspan='3' >Gross margin%</td>";
        for (var i = 0; i < 10; i++) {
            let result = GC_Projection[i] / SumOfSales[i];
            result = (Math.abs(result) == Infinity || isNaN(result) || result == -Infinity) ? 0 : result;
            result = isNaN(result) ? 0 : result;
            html += "<td>" + result.toFixed(3) + "</td>";
            CGPercentage.push(result);
        }
        html += "</tr>";
        html += "<tr class='lblHeading txtColor-limegreen emptyRow'><td><b>Expenses as defined as per profit share agreement</b></td><td colspan='12' ></td></tr>";
        /*-----------MA Annual fees--------------------------*/
        var Marketing_Allowance_Value = formatToNumber($('#MarketingAllowance').val()) / 100; // G20
        html += "<tr class='lblHeading'><td colspan='3' >MA Annual fees</td>";
        for (var i = 0; i < 10; i++) {
            let result = Marketing_Allowance_Value * SumOfGrossSales[i];
            result = isNaN(result) ? 0 : result;
            html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        /*---------------Annual confirmatory release testing-----------------------*/
        html += "<tr class='Expiries'><td colspan='3' >Annual confirmatory release testing</td>";
        for (var i = 0; i < 10; i++) {
            html += "<td> <input type='number' onchange='AnnualconfirmatoryValueChange(this," + i + ");' name='" + Global_Projection_Year_data[i] + "' value='" + AnnualConfirmatoryRelease_Data[i] + "' class='form-control AnnualConfirmatoryRtxtbox UpdateProjectionCommercial' id='ProjectionAnnualConfirmatoryRelease_" + i + "' > </td>";
        }
        html += "</tr>";
        /*-----------Opex--------------------------*/
        var Opexasapercenttosale = formatToNumber($('#Opexasapercenttosale').val()) / 100; // G21
        html += "<tr class='lblHeading'><td colspan='3' >Opex</td>";
        for (var i = 0; i < 10; i++) {
            let result = Opexasapercenttosale * SumOfGrossSales[i];
            result = isNaN(result) ? 0 : result;
            html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        /*----------- EBITDA - before Profit share--------------------------*/
        var EBITDA_projection = [];
        html += "<tr class='lblHeading'><td colspan='3' > <b> EBITDA - before Profit share</b></td>";
        for (var i = 0; i < 10; i++) {
            let result = GC_Projection[i] - ((Marketing_Allowance_Value * SumOfGrossSales[i]) + formatToNumber(AnnualConfirmatoryRelease_Data[i], true) + (Opexasapercenttosale * SumOfGrossSales[i]))
            EBITDA_projection.push(result);
            result = isNaN(result) ? 0 : result;
            html += "<td>" + result.toFixed(3) + "</td>";

        }
        html += "</tr>";
        /*-----------EBITDA % (before PS)--------------------------*/
        html += "<tr class='lblHeading percentage'><td colspan='3' > EBITDA % (before PS)</td>";
        for (var i = 0; i < 10; i++) {
            let result = EBITDA_projection[i] / SumOfSales[i];
            result = (Math.abs(result) == Infinity || isNaN(result)) ? 0 : result;
            result = isNaN(result) ? 0 : result;
            html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        /*-----------Profit Share--------------------------*/
        var Profit_Share_Value = formatToNumber($('#ExternalProfitSharepercent').val()) / 100; // G16
        html += "<tr class='lblHeading'><td colspan='3' > <b>Profit Share</b></td>";
        for (var i = 0; i < 10; i++) {
            let result = EBITDA_projection[i] * Profit_Share_Value;
            result = isNaN(result) ? 0 : result;
            html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        html += "<tr class='lblHeading bg-light emptyRow'><td colspan='13' >&nbsp</td>";
        /*-----------MA Annual fees--------------------------*/
        //var Marketing_Allowance_Value = $('#MarketingAllowance').val(); // G20
        //html += "<tr class='lblHeading'><td colspan='3' >MA Annual fees</td>";
        //for (var i = 0; i < 10; i++) {
        //    let result = Marketing_Allowance_Value * SumOfGrossSales[i];
        //    result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
        //}
        html += "</tr>";
        /*-----------Net Income - PBT--------------------------*/
        var Net_Income_PBT_projection_data = [];
        html += "<tr class='lblHeading'><td colspan='3' ><b> Net Income - PBT</b></td>";
        for (var i = 0; i < 10; i++) {
            let result = EBITDA_projection[i] - (EBITDA_projection[i] * Profit_Share_Value) + (Marketing_Allowance_Value * SumOfGrossSales[i]);
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
            Net_Income_PBT_projection_data.push(result);
        }
        html += "</tr>";
        /*-----------% Margin--------------------------*/
        html += "<tr class='lblHeading percentage'><td colspan='3' >% Margin</td>";
        for (var i = 0; i < 10; i++) {
            let result = Net_Income_PBT_projection_data[i] / SumOfSales[i];
            result = (Math.abs(result) == Infinity || isNaN(result)) ? 0 : result;
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        html += "<tr class='lblHeading bg-light emptyRow'><td colspan='13' >&nbsp</td>";
        /*-----------Tax--------------------------*/
        var Incometaxrate_Value = formatToNumber($('#Incometaxrate').val()) / 100; // G14
        html += "<tr class='lblHeading'><td colspan='3' ><b>Tax</b></td>";
        for (var i = 0; i < 10; i++) {
            let result = Net_Income_PBT_projection_data[i] * Incometaxrate_Value;
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        html += "<tr class='lblHeading bg-light emptyRow'><td colspan='13' >&nbsp</td>";

        /*-----------Net Income - PAT--------------------------*/
        var Net_Income_PAT_projection_data = [];
        html += "<tr class='lblHeading'><td colspan='3' ><b>Net Income - PAT</b></td>";
        for (var i = 0; i < 10; i++) {
            let result = Net_Income_PBT_projection_data[i] - (Net_Income_PBT_projection_data[i] * Incometaxrate_Value);
            Net_Income_PAT_projection_data.push(result);
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        /*-----------% PAT Margin--------------------------*/
        html += "<tr class='lblHeading percentage'><td colspan='3' >% PAT Margin</td>";
        for (var i = 0; i < 10; i++) {
            let result = Net_Income_PAT_projection_data[i] / SumOfSales[i];
            result = (Math.abs(result) == Infinity || isNaN(result)) ? 0 : result;
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        html += "<tr class='lblHeading bg-light emptyRow'><td colspan='13' >&nbsp</td></tr>";
        /*-----------Receivables--------------------------*/
        var Temp_WCT_html = "";
        Temp_WCT_html += "<tr class='lblHeading'><td colspan='3' > Receivables </td>";
        var CollectioninDays_Value = $('#CollectioninDays').val();
        var Receivables_Projection = [];
        for (var i = 0; i < 10; i++) {
            let result = 0;
            result = SumOfSales[i] * CollectioninDays_Value / 365;
            Receivables_Projection.push(result);
            result = isNaN(result) ? 0 : result;
            Temp_WCT_html += "<td>" + result.toFixed(3) + "</td>";
        }
        Temp_WCT_html += "</tr>";
        /*-----------Inventory--------------------------*/
        Temp_WCT_html += "<tr class='lblHeading'><td colspan='3' > Inventory </td>";
        var InventoryinDays_Value = $('#InventoryinDays').val();
        var Inventory_Projection = [];
        for (var i = 0; i < 10; i++) {
            let result = 0;
            result = SumOfCOGS[i] * InventoryinDays_Value / 365;
            Inventory_Projection.push(result);
            result = isNaN(result) ? 0 : result;
            Temp_WCT_html += "<td>" + result.toFixed(3) + "</td>";
        }
        Temp_WCT_html += "</tr>";
        /*-----------Creditors--------------------------*/
        Temp_WCT_html += "<tr class='lblHeading'><td colspan='3' > Creditors </td>";
        var CreditorinDays_Value = $('#CreditorinDays').val();
        var Creditors_Projection = [];
        for (var i = 0; i < 10; i++) {
            let result = 0;
            result = -(SumOfCOGS[i] + (EBITDA_projection[i] * Profit_Share_Value)) * CreditorinDays_Value / 365
            Creditors_Projection.push(result);
            result = isNaN(result) ? 0 : result;
            Temp_WCT_html += "<td>" + result.toFixed(3) + "</td>";
        }
        Temp_WCT_html += "</tr>";
        /*-----------Working capital Total--------------------------*/
        Temp_WCT_html += "<tr class='lblHeading'><td colspan='3' ><b> Working capital Total </b> </td>";
        var Working_capital_Total = [];
        for (var i = 0; i < 10; i++) {
            let result = 0;
            result = Receivables_Projection[i] + Inventory_Projection[i] + Creditors_Projection[i];
            Working_capital_Total.push(result);
            result = isNaN(result) ? 0 : result;
            Temp_WCT_html += "<td>" + result.toFixed(3) + "</td>";
        }
        Temp_WCT_html += "</tr>";
        /*-----------Incremental working capital--------------------------*/
        html += "<tr class='lblHeading'><td colspan='3' ><b> Incremental working capital</b>  </td>";
        var Incremental_working_capital_projection_data = [];
        for (var i = 0; i < 10; i++) {
            let result = 0;
            if (i > 0) {
                result = Working_capital_Total[i - 1] - Working_capital_Total[i];
            }

            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
            Incremental_working_capital_projection_data.push(result);
        }
        html += "</tr>";


        html += "<tr class='lblHeading bg-light emptyRow'><td><b>Deal Terms</b></td><td colspan='12' ></td></tr>";
        /*-----------R&D analytical cost--------------------------*/
        html += "<tr class='lblHeading '><td colspan='3' >R&D analytical cost</td>";
        var RnD_analytical_cost_projection_data = [];
        var cost_Value = $('#RandDanalyticalcost').val();
        var compareDate_string = $('#RandDanalyticalcostPhaseEndDate').val();
        var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        compareDate.setMonth(compareDate.getMonth() + 1);
        for (var i = 0; i < 10; i++) {
            let result = 0;
            if (i == 0) {
                result = 0;
            } else {
                if ((compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1])) {
                    result = -cost_Value * (1 - (Incometaxrate_Value));
                }
            }

            result = isNaN(result) ? 0 : result;
            html += "<td>" + result.toFixed() + "</td>";
            RnD_analytical_cost_projection_data.push(result);
        }
        html += "</tr>";
        /*-----------RLD sample cost--------------------------*/
        html += "<tr class='lblHeading '><td colspan='3' >RLD sample cost</td>";
        var RLD_sample_projection_data = [];
        var cost_Value = $('#Rldsamplecost').val();
        var compareDate_string = $('#RldsamplecostPhaseEndDate').val();
        var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        compareDate.setMonth(compareDate.getMonth() + 1);
        for (var i = 0; i < 10; i++) {
            let result = 0;
            if (i == 0) {
                result = 0;
            } else {
                if ((compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1])) {
                    result = -cost_Value;
                    result = result * (1 - (Incometaxrate_Value));
                }
            }
            result = isNaN(result) ? 0 : result;
            html += "<td>" + result.toFixed() + "</td>";
            RLD_sample_projection_data.push(result);
        }
        html += "</tr>";
        /*-----------Batch manufacturing cost--------------------------*/
        html += "<tr class='lblHeading '><td colspan='3' >Batch manufacturing cost</td>";
        var Batch_manufacturing_projection_data = [];
        var cost_Value = $('#BatchmanufacturingcostOrApiactualsEst').val();
        var compareDate_string = $('#BatchmanufacturingcostOrApiactualsEstPhaseEndDate').val();
        var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        compareDate.setMonth(compareDate.getMonth() + 1);
        for (var i = 0; i < 10; i++) {
            let result = 0;
            if (i == 0) {
                result = 0;
            } else {
                if ((compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1])) {
                    result = -cost_Value;
                    result = result * (1 - (Incometaxrate_Value));
                }
            }

            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed() + "</td>";
            Batch_manufacturing_projection_data.push(result);
        }
        html += "</tr>";
        /*-----------6 months stability cost--------------------------*/
        html += "<tr class='lblHeading '><td colspan='3' >6 months stability cost</td>";
        var Sixmonths_stability_costprojection_data = [];
        var cost_Value = $('#Sixmonthsstabilitycost').val();
        var compareDate_string = $('#SixmonthsstabilitycostPhaseEndDate').val();
        var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        compareDate.setMonth(compareDate.getMonth() + 1);
        for (var i = 0; i < 10; i++) {
            let result = 0;
            if (i == 0) {
                result = 0;
            } else {
                if ((compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1])) {
                    result = -cost_Value;
                    result = result * (1 - (Incometaxrate_Value));
                }
            }
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed() + "</td>";
            Sixmonths_stability_costprojection_data.push(result);
        }
        html += "</tr>";
        /*-----------Tech Transfer--------------------------*/
        html += "<tr class='lblHeading '><td colspan='3' >Tech Transfer</td>";
        var Tech_Transfer_projection_data = [];
        var cost_Value = $('#TechTransfer').val();
        var compareDate_string = $('#TechTransferPhaseEndDate').val();
        var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        compareDate.setMonth(compareDate.getMonth() + 1);
        for (var i = 0; i < 10; i++) {
            let result = 0;
            if (i == 0) {
                result = 0;
            } else {
                if ((compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1])) {
                    result = -cost_Value;
                    result = result * (1 - (Incometaxrate_Value));
                }
            }
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed() + "</td>";
            Tech_Transfer_projection_data.push(result);
        }
        html += "</tr>";
        /*-----------BE studies--------------------------*/
        html += "<tr class='lblHeading '><td colspan='3' >  BE studies  </td>";
        var BE_studies_projection_data = [];
        var cost_Value = $('#Bestudies').val();
        var compareDate_string = $('#BestudiesPhaseEndDate').val();
        var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        compareDate.setMonth(compareDate.getMonth() + 1);
        for (var i = 0; i < 10; i++) {
            let result = 0;
            if (i == 0) {
                result = 0;
            } else {
                if ((compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1])) {
                    result = -cost_Value;
                    result = result * (1 - (Incometaxrate_Value));
                }
            }
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed() + "</td>";
            BE_studies_projection_data.push(result);
        }
        html += "</tr>";
        /*-----------Filing fees--------------------------*/
        html += "<tr class='lblHeading '><td colspan='3' >  Filing fees  </td>";
        var Filing_fees_projection_data = [];
        var cost_Value = $('#Filingfees').val();
        var compareDate_string = $('#FilingfeesPhaseEndDate').val();
        var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        compareDate.setMonth(compareDate.getMonth() + 1);
        for (var i = 0; i < 10; i++) {
            let result = 0;
            if (i == 0) {
                result = 0;
            } else {

                if ((compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1])) {
                    result = -cost_Value;
                    result = result * (1 - (Incometaxrate_Value));
                }
            }

            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed() + "</td>";
            Filing_fees_projection_data.push(result);
        }
        html += "</tr>";
        /*-----------Other fees--------------------------*/
        html += "<tr class='lblHeading '><td colspan='3' >  Other fees </td>";
        var Other_fees_projection_data = [];
        var cost_Value = $('#ToolingAndChangeParts').val();
        var compareDate_string = $('#ToolingAndChangePartsPhaseEndDate').val();
        var compareDate = (compareDate_string != "") ? new Date(compareDate_string) : new Date();
        compareDate.setMonth(compareDate.getMonth() + 1);
        for (var i = 0; i < 10; i++) {
            let result = 0;
            if (i == 0) {
                result = 0;
            } else {
                if ((compareDate <= Projection_Year_data[i] && compareDate > Projection_Year_data[i - 1])) {
                    result = -cost_Value;
                    result = result * (1 - (Incometaxrate_Value));
                }
            }
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed() + "</td>";
            Other_fees_projection_data.push(result);
        }
        html += "</tr>";

        /*-----------Total investment--------------------------*/
        html += "<tr class='lblHeading bg-light'><td colspan='3' ><b> Total investment </b></td>";
        var TotalInvestment_projection_data = [];
        for (var i = 0; i < 10; i++) {
            let result = 0;
            result = Other_fees_projection_data[i] +
                Filing_fees_projection_data[i] +
                BE_studies_projection_data[i] +
                Tech_Transfer_projection_data[i] +
                Sixmonths_stability_costprojection_data[i] +
                Batch_manufacturing_projection_data[i] +
                RLD_sample_projection_data[i] +
                RnD_analytical_cost_projection_data[i];

            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
            TotalInvestment_projection_data.push(result);

        }
        html += "</tr>";
        /*-----------Net Cash Flow--------------------------*/
        html += "<tr class='lblHeading bg-light'><td colspan='3' > <b>Net Cash Flow </b></td>";
        var NetCashFlow_projection_data = [];
        for (var i = 0; i < 10; i++) {
            let result = 0;
            result = Incremental_working_capital_projection_data[i] + Net_Income_PAT_projection_data[i] + TotalInvestment_projection_data[i];

            result = isNaN(result) ? 0 : result; html += "<td><b>" + result.toFixed(3) + "</b></td>";
            NetCashFlow_projection_data.push(result);
        }
        html += "</tr>";
        /*-----------Cumulative cash flow -Combined--------------------------*/
        html += "<tr class='lblHeading bg-light'><td colspan='3' > <b> Cumulative cash flow -Combined </b> </td>";
        var Cumulative_cash_flow_projection_data = [];
        for (var i = 0; i < 10; i++) {
            let result = 0;
            if (i > 0) {
                result = NetCashFlow_projection_data[i] + Cumulative_cash_flow_projection_data[i - 1];
            }

            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
            Cumulative_cash_flow_projection_data.push(result);
        }
        html += "</tr>";
        /*-----------Discount Factor Projection--------------------------*/
        html += "<tr class='lblHeading percentage'><td colspan='3' > Discount Factor Projection </td>";
        var DiscountRate_Value = $('#DiscountRate').val();
        var Discount_Factor_Projection = [];
        for (var i = 0; i < 10; i++) {
            var Row_Projection_th_Index = $('#tblFinanceProjection').find('.thYearCounter:eq(' + i + ')').text();
            var trProjectionYearIndex = formatToNumber((Row_Projection_th_Index == '-') ? '0' : Row_Projection_th_Index);
            let result = 0;
            result = 1 / (Math.pow((1 + DiscountRate_Value / 100), trProjectionYearIndex));

            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
            Discount_Factor_Projection.push(result);
        }
        html += "</tr>";
        /*-----------Discount Cash Flow--------------------------*/
        html += "<tr class='lblHeading bg-light'><td colspan='3' ><b> Discount Cash Flow</b> </td>";
        var Discount_Cash_Flow_Projection = [];
        for (var i = 0; i < 10; i++) {
            let result = 0;
            result = Discount_Factor_Projection[i] * NetCashFlow_projection_data[i];

            result = isNaN(result) ? 0 : result; html += "<td><b>" + result.toFixed(3) + "</b></td>";
            Discount_Cash_Flow_Projection.push(result);
        }
        html += "</tr>";
        /*-----------Cumulative Discount Cash Flow--------------------------*/
        html += "<tr class='lblHeading bg-light'><td colspan='3' ><b> Cumulative Discount cash flow </b></td>";
        var Cumulative_Discount_cash_flow_projection_data = [];
        for (var i = 0; i < 10; i++) {
            let result = 0;
            if (i > 0) {
                result = Discount_Cash_Flow_Projection[i] + Cumulative_Discount_cash_flow_projection_data[i - 1];
            }

            result = isNaN(result) ? 0 : result; html += "<td><b>" + result.toFixed(3) + "</b></td>";
            Cumulative_Discount_cash_flow_projection_data.push(result);
        }
        html += "</tr>";
        /*-----------Simple payback in # of months--------------------------*/
        html += "<tr class='lblHeading bg-light'><td colspan='3' ><b> Simple payback in # of months </b></td>";
        var Simple_payback_Projection = [];
        for (var i = 0; i < 10; i++) {
            let result = 0;
            if (i > 0) {
                if (Cumulative_cash_flow_projection_data[i - 1] > 0) {
                    result = 0;
                }
                else {
                    if (Cumulative_cash_flow_projection_data[i] > 0) {
                        result = -Cumulative_cash_flow_projection_data[i - 1] / NetCashFlow_projection_data[i] * 12;
                    }
                    else {
                        result = Operating_Months_Projection_data[i];
                    }
                }
            }


            result = isNaN(result) ? 0 : result;
            html += "<td>" + result.toFixed(0) + "</td>";
            Simple_payback_Projection.push(result);
        }
        html += "</tr>";
        /*-----------Discounted Payback in # of months--------------------------*/
        html += "<tr class='lblHeading bg-light'><td colspan='3' > <b>Discounted payback in # of months </b> </td>";
        var Discounted_payback_Projection = [];
        for (var i = 0; i < 10; i++) {
            let result = 0;
            if (i > 0) {
                if (Cumulative_Discount_cash_flow_projection_data[i - 1] > 0) {
                    result = 0;
                }
                else {
                    if (Cumulative_Discount_cash_flow_projection_data[i] > 0) {
                        result = -Cumulative_Discount_cash_flow_projection_data[i - 1] / Discount_Cash_Flow_Projection[i] * 12;
                    }
                    else {
                        result = Operating_Months_Projection_data[i];
                    }
                }
            }

            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(0) + "</td>";
            Discounted_payback_Projection.push(result);
        }
        html += "</tr>";
        html += "<tr class='lblHeading bg-light emptyRow'><td><b>Working capital</b></td><td colspan='12' ></td></tr>";

        html += Temp_WCT_html;

        html += "</tbody>";

        /*-------------------------->> Output grid--------------------------------*/
        var TempHtml = "";
        var GestationPeriodinYears_value = formatToNumber($("#GestationPeriodinYears").val(), true);
        var DCFP = Discount_Cash_Flow_Projection;
        TempHtml += '<thead class="bg-primary">';
        TempHtml += '<tr><th>Output grid</th> <th></th> <th>From launch</th></tr>';
        TempHtml += "</thead><tbody>";
        TempHtml += "<tr> <td>10-Year NPV to Emcure</td> <td> " + eval(Discount_Cash_Flow_Projection.join('+')).toFixed(3) + " </td><td></td></tr>";
        let Year5Result = DCFP[0] + DCFP[1] + DCFP[2] + DCFP[3] + DCFP[4];
        TempHtml += "<tr> <td>5-Year NPV to Emcure</td> <td> " + Year5Result.toFixed(3) + " </td><td></td></tr>";
        TempHtml += "<tr> <td>Discounted payback in yrs</td> <td> <b>" + (eval(Discounted_payback_Projection.join('+')) / 12).toFixed(3) + "</b> </td>";
        TempHtml += "<td><b> " + ((eval(Discounted_payback_Projection.join('+')) / 12) - GestationPeriodinYears_value).toFixed(3) + "</b></td></tr>";
        TempHtml += "<tr> <td>Total investment</td> <td> " + eval(TotalInvestment_projection_data.join('+')).toFixed(3) + " </td><td></td></tr>";
        TempHtml += "</tbody>";
        $('#tblOutputGridFinanceProjection').html(TempHtml);
    }
    $('#tblFinanceProjection').html(html);

}
function UpdateDynamicTextBoxValues(table) {
    if (table.length == 10) {
        for (var i = 0; i < 10; i++) {
            Expiries_Yearwise_Data[i] = table[i].expiries;
            AnnualConfirmatoryRelease_Data[i] = table[i].annualConfirmatoryRelease;
        }
    }

}
function GetMarketSharePercentage(low, mid, high) {
    let marketSharePercentage = 0;
    try {
        if ($('#MSPersentage').val() == "1") {
            marketSharePercentage = parseFloat(low);
        } else if ($('#MSPersentage').val() == "2") {
            marketSharePercentage = parseFloat(mid);
        } else if ($('#MSPersentage').val() == "3") {
            marketSharePercentage = parseFloat(high);
        }
        else {
            marketSharePercentage = parseFloat(low);
        }
    } catch (e) {
    }
    return marketSharePercentage;
}
function GetNSPPercentage(low, mid, high) {
    let NSPPercentage = 0;
    try {
        if ($('#TargetPriceScenario').val() == "1") {
            NSPPercentage = parseFloat(low);
        } else if ($('#TargetPriceScenario').val() == "2") {
            NSPPercentage = parseFloat(mid);
        } else if ($('#TargetPriceScenario').val() == "3") {
            NSPPercentage = parseFloat(high);
        } else {
            NSPPercentage = parseFloat(low);
        }
    } catch (e) {
    }
    return NSPPercentage;
}
function GetCOGSPercentage(low, mid, high) {
    let COGSPercentage = 0;
    try {
        if ($('#TargetPriceScenario').val() == "1") {
            COGSPercentage = parseFloat(low);
        } else if ($('#TargetPriceScenario').val() == "2") {
            COGSPercentage = parseFloat(mid);
        } else if ($('#TargetPriceScenario').val() == "3") {
            COGSPercentage = parseFloat(high);
        } else {
            COGSPercentage = parseFloat(low);
        }
    } catch (e) {
    }
    return COGSPercentage;
}
function formatToNumber(input, isFloat) {
    try {
        if (isFloat) {
            return parseFloat(input);
        } else {
            return parseFloat(input);
        }
    } catch (e) {
        return 0;
    }
}
function fnGetActiveBusinessUnit() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetActiveEncryptedBusinessUnit, 'GET', GetActiveBusinessUnitSuccess, GetActiveBusinessUnitError);
}
function GetActiveBusinessUnitSuccess(data) {
    var businessUnitHTML = "";
    var businessUnitPanel = "";
    $.each(data._object, function (index, item) {
        let buClassName = item.businessUnitName.toLowerCase() === 'india' ? 'in' : item.businessUnitName.toLowerCase();
        businessUnitHTML += '<li class="nav-item p-0">\
            <a class="nav-link '+ (item.businessUnitId == _selectBusinessUnit ? "active" : "") + ' px-2" href="#custom-tabs-' + buClassName + '" data-toggle="pill" aria-selected="true" onclick="loadFinanceProjectionData(' + _selectedPidfId + ',\'' + item.encBusinessUnitId + '\',' + item.businessUnitId + ')" id="custom-tabs-two-' + item.businessUnitId + '-tab">' + item.businessUnitName + '</a></li>';
        businessUnitPanel += '<div class="tab-pane ' + ((item.businessUnitId == _selectBusinessUnit ? "fade show active" : "")) + '" id="custom-tabs-' + item.businessUnitId + '" role="tabpanel" aria-labelledby="custom-tabs-two-' + item.businessUnitId + '-tab"></div>';
    });
    if (true) {
        let buClassName = 'All';
        businessUnitHTML += '<li class="nav-item p-0">\
            <a class="nav-link '+ (false ? "active" : "") + ' px-2" href="#custom-tabs-' + buClassName + '" data-toggle="pill" aria-selected="true" onclick="loadFinanceProjectionData(' + _selectedPidfId + ',\'' + '0' + '\',' + '0' + ')" id="custom-tabs-two-' + '0' + '-tab">' + 'All' + '</a></li>';
        businessUnitPanel += '<div class="tab-pane ' + ((false ? "fade show active" : "")) + '" id="custom-tabs-' + '0' + '" role="tabpanel" aria-labelledby="custom-tabs-two-' + '0' + '-tab"></div>';

    }
    $('#custom-tabs-business-tab').html(businessUnitHTML);
    renderpbfbu(data);
}
function GetActiveBusinessUnitError(x, y, z) {
    toastr.error(ErrorMessage);
}
var CurrentscreenId_Finance = '';

function BussinesUnitInterestedFinance(pidfid, buid, screenId, callback) {
    CurrentscreenId_Finance = screenId;
    ajaxServiceMethod($('#hdnBaseURL').val() + GetIsInterestedByPIDFandBUurlFinance + "/" + pidfid + "/" + buid, 'GET', function (data) {
        BussinesUnitInterestedFinanceSuccess(data, callback);
    }, BussinesUnitInterestedFinanceError);
}

function BussinesUnitInterestedFinanceSuccess(data, callback) {
    var BUTabData_Div = '.clsContentUnderBUTab_' + CurrentscreenId_Finance;
    var NonIntNote_Div = '.dvNotInterestedBUNote_' + CurrentscreenId_Finance;
    var NonIntNote_HeadingNote = '.dvNotInterestedBUNoteHeading_' + CurrentscreenId_Finance;

    var IsInterested = DispalyStatusOfBUByInterested(data, BUTabData_Div, NonIntNote_Div, NonIntNote_HeadingNote);

    // Call the callback function and pass the IsInterested value
    if (typeof callback === "function") {
        callback(IsInterested);
    }
}

function BussinesUnitInterestedFinanceError(x, y, z) {
    toastr.error(ErrorMessage);
}


//-----------------Non-Finace charter code----------------------
function GetCommercialSummaryBudgetData() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetCommercialSummaryBudget + '/' + _selectedPidfId, 'GET', GetCommercialSummaryBudgetSuccess, GetCommercialSummaryBudgetError);
}
function GetCommercialSummaryBudgetSuccess(data) {
    console.log(data);
    var tableBody = document.querySelector("#tblcommercialsummary tbody");

    data._object.forEach(function (item) {
        var row = document.createElement("tr");
        var cells = [
            item.businessUnitName,
            item.pidfStrengthPackSize,
            "",
        ];

        var countryNameAdded = false;

        for (var i = 1; i <= 5; i++) {
            var yearData = item.yearDetails[i];
            if (yearData) {
                if (!countryNameAdded) {
                    cells[2] = yearData.countryName;
                    countryNameAdded = true;
                } else {

                }

                cells.push(yearData.price);
                cells.push(yearData.units);
                cells.push(yearData.value);
            } else {
                cells.push("-");
                cells.push("-");
                cells.push("-");
            }
        }

        cells.forEach(function (cellData) {
            var cell = document.createElement("td");
            cell.textContent = cellData;
            row.appendChild(cell);
        });

        tableBody.appendChild(row);
    });
}
function GetCommercialSummaryBudgetError(x, y, z) {
    toastr.error(ErrorMessage);
}
function renderpbfbu(data) {
    var businessUnitHTML = "";
    var businessUnitPanel = "";
    $.each(data._object, function (index, item) {
        let buClassName = item.businessUnitName.toLowerCase() === 'india' ? 'in' : item.businessUnitName.toLowerCase();
        businessUnitHTML += '<li class="nav-item p-0">\
            <a class="nav-link '+ (item.businessUnitId == _selectBusinessUnit ? "active" : "") + ' px-2" href="#custom-tabs-' + buClassName + '" data-toggle="pill" aria-selected="true" onclick="loadpbf(' + _selectedPidfId + ',\'' + item.encBusinessUnitId + '\',' + item.businessUnitId + ')" id="custom-tabs-two-' + item.businessUnitId + '-tab">' + item.businessUnitName + '</a></li>';
        businessUnitPanel += '<div class="tab-pane ' + ((item.businessUnitId == _selectBusinessUnit ? "fade show active" : "")) + '" id="custom-tabs-' + item.businessUnitId + '" role="tabpanel" aria-labelledby="custom-tabs-two-' + item.businessUnitId + '-tab"></div>';
    });
    $('#custom-tabs-pbffinance-tab').html(businessUnitHTML);
}
