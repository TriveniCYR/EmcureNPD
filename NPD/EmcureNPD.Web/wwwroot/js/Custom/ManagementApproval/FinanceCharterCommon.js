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

        html += '<thead class="bg-light">';
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

        var Arr_FinanceTable_tr = GetBatchSizeCostingTRValues();

        jQuery.each(Arr_FinanceTable_tr, function (index, trObj) {
            //$('#FinanceTableBoy tr').each(function (index, value) {

            let SKU = trObj.SKU;
            let PackSize = trObj.PackSize;

            html += '<tr class="bg-light"><td class="text-left" colspan="15"><b>' + SKU + " - " + PackSize + '</b></td></tr>';

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
                html += "<td>" + _units.toFixed(3) + "</td>";
                MS_td_data.push(_units);
            }
            html += "</tr>";
            //---------------------End-MS%_Row-------------------------------------------------------   
            //---------------------Start-NSP_Row-------------------------------------------------------
            html += "<tr class='" + _uniqueClass + "'><td>NSP</td><td>" + trObj.hdnNSPLow + "</td><td>" + trObj.hdnNSPMid + "</td><td>" + trObj.hdnNSPHigh + "</td><td>NSP</td>";
            var nsp_Percentage = GetNSPPercentage(trObj.hdnNSPLow, trObj.hdnNSPMid, trObj.hdnNSPHigh);
            // let marketInPacks = formatToNumber($(this).find('.Marketinpacks').val());
            let nsp_Erosion = formatToNumber($("#PriceErosion").val(), true);

            for (var i = 0; i < 10; i++) {
                var Row_th_Index = $('#tblCommercialPerPack').find('.thYearCounter:eq(' + i + ')').text();
                var trYearIndex = formatToNumber((Row_th_Index == '-') ? '0' : Row_th_Index);
                let _units = (nsp_Percentage / 100) * (Math.pow((1 - (nsp_Erosion / 100)), trYearIndex))
                _units = isNaN(_units) ? 0 : _units;
                html += "<td>" + _units.toFixed(3) + "</td>";
                NSP_td_data.push(_units);
            }
            html += "</tr>";
            //---------------------End-NSP_Row-------------------------------------------------------
            //---------------------Start-COGS_Row-------------------------------------------------------
            html += "<tr class='" + _uniqueClass + "'><td>COGS</td><td>" + trObj.emcureCOGs_pack + "</td><td>" + trObj.emcureCOGs_pack + "</td><td>" + trObj.emcureCOGs_pack + "</td><td>COGS/Unit</td>";
            let COGS_Escalation = formatToNumber($("#EscalationinCOGS").val(), true);
            var COGS_Percentage = GetCOGSPercentage(trObj.emcureCOGs_pack, trObj.emcureCOGs_pack, trObj.emcureCOGs_pack);
            for (var i = 0; i < 10; i++) {
                var Row_th_Index = $('#tblCommercialPerPack').find('.thYearCounter:eq(' + i + ')').text();
                var trYearIndex = formatToNumber((Row_th_Index == '-') ? '0' : Row_th_Index);

                let _units = (COGS_Percentage / 100) * (Math.pow((1 + (COGS_Escalation / 100)), trYearIndex))

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
                result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";

            }
            html += "</tr>";
        });
        /* -----------Grand SUM-Rows--------------------*/
        html += "<tr><td></td></tr>";
        /*---------------Sales------------------------------*/
        html += "<tr class='SumOfSales bg-light'><td></td><td></td><td></td><td></td><td>Sales</td>";
        for (var i = 0; i < 10; i++) {
            let result = SumOfSales[i];
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        /*---------------COGS--------------------------*/
        html += "<tr class='SumOfCOGS bg-light'><td></td><td></td><td></td><td></td><td>COGS</td>";
        for (var i = 0; i < 10; i++) {
            let result = SumOfCOGS[i];
            result = isNaN(result) ? 0 : result; html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        /*-----------------GC------------------------*/
        html += "<tr class='SumOfGC bg-light'><td></td><td></td><td></td><td></td><td>GC</td>";
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
function GetMarketSharePercentage(low, mid, high) {
    let marketSharePercentage = 0;
    try {
        if ($('#MSPersentage').val() == "1") {
            marketSharePercentage = parseInt(low);
        } else if ($('#MSPersentage').val() == "2") {
            marketSharePercentage = parseInt(mid);
        } else if ($('#MSPersentage').val() == "3") {
            marketSharePercentage = parseInt(high);
        }
        else {
            marketSharePercentage = parseInt(low);
        }
    } catch (e) {
    }
    return marketSharePercentage;
}
function GetNSPPercentage(low, mid, high) {
    let NSPPercentage = 0;
    try {
        if ($('#TargetPriceScenario').val() == "1") {
            NSPPercentage = parseInt(low);
        } else if ($('#TargetPriceScenario').val() == "2") {
            NSPPercentage = parseInt(mid);
        } else if ($('#TargetPriceScenario').val() == "3") {
            NSPPercentage = parseInt(high);
        } else {
            NSPPercentage = parseInt(low);
        }
    } catch (e) {
    }
    return NSPPercentage;
}
function GetCOGSPercentage(low, mid, high) {
    let COGSPercentage = 0;
    try {
        if ($('#TargetPriceScenario').val() == "1") {
            COGSPercentage = parseInt(low);
        } else if ($('#TargetPriceScenario').val() == "2") {
            COGSPercentage = parseInt(mid);
        } else if ($('#TargetPriceScenario').val() == "3") {
            COGSPercentage = parseInt(high);
        } else {
            COGSPercentage = parseInt(low);
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
            return parseInt(input);
        }
    } catch (e) {
        return 0;
    }
}