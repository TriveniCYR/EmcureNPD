var SelectedBUValue = 0;
var UserwiseBusinessUnit;
var _PIDFPBFId = 0;
var _mode = 0;
var _rndView = 0;
var _analyticalView = 0;
var _clinicalView = 0;
var _pbf = true;
var _strengthArray = [];
var _CostOfLitigationArray = [];
var _currencySymbol = '₹';
var _firstLoad = true;
var _oralName = '';
var isValidPBFForm = true;
var PBFLinesArr = [];
let IsRaEditable = false;
var rndmasterdata_dbValueOf_lineId = 0;
var arrRnDTabList = [   //custom-tabs-department-RnD-tab-
    'DosageFormulation',
    'APIRequirment',
    'ExcipientRequirement',
    'PackagingMaterial',
    'ToolingandChangePartCost',
    'CapexMiscellaneouseExpenses',
    'PlantSupportCost',
    'ReferenceProductDetail',
    'ManPowerCostProjectDuration',
    'FillingExpenses',
    'PhaseWiseBudget',
    'HeadWiseBudget'
]
$(document).ready(function () {
    try {
        _PIDFPBFId = parseInt($('#hdnPIDFPBFId').val());
        _oralName = $('#OralName').val();
        $('#hdnPIDFId').val(_PIDFID);
        $('#Pidfid').val(_PIDFID);
        // _mode = $('#hdnIsView').val(); //parseInt($('#hdnPIDFId').val());
        _mode = getParameterByName("IsView");
        _pbf = getParameterByName("pbf");
    } catch (e) {
        _mode = getParameterByName("IsView");
        /* _PIDFId = parseInt(getParameterByName("pidfid"));*/
    }
    GetRa(_PIDFID, _PIDFPBFId);
    if (_mode == 1) {
        PBFreadOnlyForm();
    }
    GetPBFDropdown();

    $(document).on("change", ".calcRNDBatchSizesPrototypeFormulation, .calcRNDBatchSizesScaleUpbatch, .calcRNDBatchSizesExhibitBatch1, .calcRNDBatchSizesExhibitBatch2, .calcRNDBatchSizesExhibitBatch3", function () {
        $("input[class~='rndExicipientRsperkg']").trigger('change');
        SetPhaseWiseBudget();
    });

    $(document).on("change", ".AnalyticalTestTypeId", function () {
        var _selectedTestType = $(this).val();
        if (_selectedTestType != "" && _selectedTestType != "0") {
            var returnFromFunction = 0;
            for (var i = 1; i < 4; i++) {
                if ($(this).parent().parent().hasClass("analyticalActivity" + i + "")) {
                    $.each($('.analyticalActivity' + i + '').find(".AnalyticalTestTypeId"), function () {
                        if ($(this).val() == _selectedTestType) {
                            returnFromFunction++;
                        }
                    });
                    if (returnFromFunction > 1) {
                        $(this).val("");
                        toastr.error("Test type is already selected, please select another");
                        return false;
                    }
                }
            }
        }
        $(this).parent().parent().find('.analyticalRsTest').val($(this).find(':selected').attr('data-testTypePrice'));
        $(this).parent().parent().find('.analyticalPrototypeDevelopment').val($(this).find(':selected').text());
        $(this).parent().parent().find('.analyticalNumberOfTest').val("1");
    });
    $(document).on("change", ".rndPackingTypeId", function () {
        var _selectedTestType = $(this).val();
        if (_selectedTestType != "" && _selectedTestType != "0") {
            var returnFromFunction = 0;
            for (var i = 1; i < 4; i++) {
                if ($(this).parent().parent().hasClass("packagingActivity" + i + "")) {
                    $.each($('.packagingActivity' + i + '').find(".rndPackingTypeId"), function () {
                        if ($(this).val() == _selectedTestType) {
                            returnFromFunction++;
                        }
                    });
                    if (returnFromFunction > 1) {
                        $(this).val("");
                        toastr.error("Packaging type is already selected, please select another");
                        return false;
                    }
                }
            }
        }
        $(this).parent().parent().find('.rndPackagingUnitofMeasurement').val($(this).find(':selected').attr('data-packingunit'));
        $(this).parent().parent().find('.rndPackagingRsperUnit').val($(this).find(':selected').attr('data-packingcost'));
    });
    $(document).on("change", ".rndFillingExpensesRegionId", function () {
        var _selectedBusinessUnit = $(this).val();
        if (_selectedBusinessUnit != "" && _selectedBusinessUnit != "0") {
            var returnFromFunction = 0;
            for (var i = 1; i < 4; i++) {
                if ($(this).parent().parent().hasClass("FillingExpensesActivity" + i + "")) {
                    $.each($('.FillingExpensesActivity' + i + '').find(".rndFillingExpensesRegionId"), function () {
                        if ($(this).val() == _selectedBusinessUnit) {
                            returnFromFunction++;
                        }
                    });
                    if (returnFromFunction > 1) {
                        $(this).val("");
                        toastr.error("Business unit is already selected, please select another");
                        return false;
                    } else {
                        var _filteredStrength = $.grep(_CostOfLitigationArray, function (n, i) {
                            return n.businessUnitId === parseInt(_selectedBusinessUnit);
                        });
                        var element = $(this).parent().parent().find(".rndFillingExpensesTotalCost");
                        if (element.val() == "") {
                            if (_filteredStrength != null && _filteredStrength != undefined && _filteredStrength.length > 0) {
                                element.val(ConvertToNumber(_filteredStrength[0].costOfLitication) * 100000);
                            } else {
                                element.val("");
                            }
                        }
                    }
                }
            }
        } else {
            $(this).parent().parent().find(".rndFillingExpensesTotalCost").val("");
            $(this).parent().parent().find(".rndFillingExpensesStrengthCheckbox").prop("checked", false);
            $(this).parent().parent().find(".FillingExpensesStrengthValue").val("");
            $(this).parent().parent().find(".rndTotalFillingExpenseStrength").val("");
        }
    });
    $(document).on("change", ".rndExicipientPrototype", function () {
        var _enteredText = $(this).val();
        if (_enteredText != "") {
            var returnFromFunction = 0;
            for (var i = 1; i < 4; i++) {
                if ($(this).parent().parent().hasClass("exicipientActivity" + i + "")) {
                    $.each($('.exicipientActivity' + i + '').find(".rndExicipientPrototype"), function () {
                        if ($(this).val() == _enteredText) {
                            returnFromFunction++;
                        }
                    });

                }
            }
            if (returnFromFunction > 1) {
                $(this).val("");
                toastr.error("Same Excipient Protoype can not be select, Please Select some different value");
                return false;
            }
            else {
                $(this).parent().parent().find('.rndExicipientRsperkg').val($(this).find(':selected').attr('data-excipientcost')).trigger('change');

            }
        }
    });
    $(document).on("change", ".rndPlantSupportDevelopment", function () {
        var _enteredText = $(this).val();
        if (_enteredText != "") {
            var returnFromFunction = 0;
            for (var i = 1; i < 4; i++) {
                if ($(this).parent().parent().hasClass("PlantSupportCostActivity" + i + "")) {
                    $.each($('.PlantSupportCostActivity' + i + '').find(".rndPlantSupportDevelopment"), function () {
                        if ($(this).val() == _enteredText) {
                            returnFromFunction++;
                        }
                    });
                    if (returnFromFunction > 1) {
                        $(this).val("");
                        toastr.error("Same value can not be enter, Please Enter some different value");
                        return false;
                    }
                }
            }
        }
    });
    $(document).on("change", ".rndCapexMiscMiscellaneous", function () {
        var _enteredText = $(this).val();
        if (_enteredText != "") {
            var returnFromFunction = 0;
            for (var i = 1; i < 4; i++) {
                if ($(this).parent().parent().hasClass("CapexMiscActivity" + i + "")) {
                    $.each($('.CapexMiscActivity' + i + '').find(".rndCapexMiscMiscellaneous"), function () {
                        if ($(this).val() == _enteredText) {
                            returnFromFunction++;
                        }
                    });
                    if (returnFromFunction > 1) {
                        $(this).val("");
                        toastr.error("Same value can not be enter, Please Enter some different value");
                        return false;
                    }
                }
            }
        }
    });
    $(document).on("change", ".rndToolingChangePartPrototype", function () {
        var _enteredText = $(this).val();
        if (_enteredText != "") {
            var returnFromFunction = 0;
            for (var i = 1; i < 4; i++) {
                if ($(this).parent().parent().hasClass("ToolingChangePartActivity" + i + "")) {
                    $.each($('.ToolingChangePartActivity' + i + '').find(".rndToolingChangePartPrototype"), function () {
                        if ($(this).val() == _enteredText) {
                            returnFromFunction++;
                        }
                    });
                    if (returnFromFunction > 1) {
                        $(this).val("");
                        toastr.error("Same value can not be enter, Please Enter some different value");
                        return false;
                    }
                }
            }
        }
    });
    $(document).on("change", ".calcFastingOrFed, .calcNoOfVolunteers, .calcClinicalCost, .calcBioAnalyticalCost, .calcDocCostandStudy", function () {
        var _BioStudyTypeId = $(this).parent().parent().attr("data-biostudytypeid");
        var _StrengthId = $(this).parent().attr("data-strengthid");

        var _ClinicalRows = $('.clinicalcal_' + _BioStudyTypeId + '').find("[data-strengthid=" + _StrengthId + "]");

        var FastingOrFed = _ClinicalRows.find(".calcFastingOrFed").val();
        var NoOfVol = _ClinicalRows.find(".calcNoOfVolunteers").val();
        var ClinicalCost = _ClinicalRows.find(".calcClinicalCost").val();
        var BioAnalyticalCost = _ClinicalRows.find(".calcBioAnalyticalCost").val();
        var DocCostStudy = _ClinicalRows.find(".calcDocCostandStudy").val();

        var _Sum = 0;
        $.each($(this).parent().parent().find("input[type=number]"), function (index, item) {
            if ($(item).attr("class").indexOf("TotalStrength") === -1) {
                _Sum += ConvertToNumber(($(item).val() == "" ? 0 : $(item).val()));
            }
        });

        // set total for the row
        $(this).parent().parent().find(".TotalStrength").val(formatNumber(_Sum));

        // formula to calculate the total cost for one strength (all the properties) (one column)
        var totalCostFastingFed = (FastingOrFed == "" ? 0 : ConvertToNumber(FastingOrFed)) * ((NoOfVol == "" ? 0 : ConvertToNumber(NoOfVol)) * ((ClinicalCost == "" ? 0 : ConvertToNumber(ClinicalCost)) + (BioAnalyticalCost == "" ? 0 : ConvertToNumber(BioAnalyticalCost))) + (DocCostStudy == "" ? 0 : ConvertToNumber(DocCostStudy)));

        // set total for all the property in the table for one strength
        $('.clinicalcal_' + _BioStudyTypeId + 'Total').find("[data-strengthid=" + _StrengthId + "]").find(".calcTotalCostForStrengthClinical").val(formatNumber(totalCostFastingFed));

        var _TotalSum = 0;
        $.each($('.clinicalcal_' + _BioStudyTypeId + 'Total').find(".calcTotalCostForStrengthClinical"), function (index, item) {
            _TotalSum += ConvertToNumber(($(item).val() == "" ? 0 : $(item).val()));
        });

        // set total for all the property in the table
        $('.clinicalcal_' + _BioStudyTypeId + 'Total').find(".calcTotalCostForStrengthClinicalTotal").val(formatNumber(_TotalSum));

        var _TotalBioStudySum = 0;
        $.each($('.calcTotalCostForStrengthClinical'), function (index, item) {
            _TotalBioStudySum += ConvertToNumber(($(item).val() == "" ? 0 : $(item).val()));
        });

        var _TotalBioStudyCost = 0;
        $.each($('.totalBioStudyCost'), function (index, item) {
            var _strengthId = $(item).parent().attr("data-strengthid");
            var _TotalBioStudySum = 0;
            $.each($('.calcTotalCostForStrengthClinical'), function (i, it) {
                var _childStrengthId = $(it).parent().attr("data-strengthid");
                if (_childStrengthId == _strengthId) {
                    _TotalBioStudySum += ConvertToNumber(($(it).val() == "" ? 0 : $(it).val()));
                }
            });
            $(item).val(formatNumber(_TotalBioStudySum));
            _TotalBioStudyCost += _TotalBioStudySum;
        });
        $('.totalBioStudyCostTotal').val(formatNumber(_TotalBioStudyCost));
        SetPhaseWiseBudget();
    });
    /*API Requirement*/
    $(document).on("change", "#RNDMasterEntities_ApirequirementMarketPrice", function () {
        $("input[class~='calcRNDApirequirementsPrototype']").trigger('change');
    });
    $(document).on("change", ".calcRNDApirequirementsPrototype, .calcRNDApirequirementsScaleUp, .calcRNDApirequirementsExhibitBatch1, .calcRNDApirequirementsExhibitBatch2, .calcRNDApirequirementsExhibitBatch3, .calcRNDApirequirementsPrototypeCost, .calcRNDApirequirementsScaleUpCost, .calcRNDApirequirementsExhibitBatchCost, .calcRNDApirequirementsMarketPrice", function () {
        var marketprice = $("#RNDMasterEntities_ApirequirementMarketPrice").val();
        marketprice = ConvertToNumber((marketprice == "" ? 0 : marketprice));
        var _BioStudyTypeId = $(this).parent().parent().attr("data-biostudytypeid");
        var _StrengthId = $(this).parent().attr("data-strengthid");

        var _APIRows = $('.ApiRequirement_' + _BioStudyTypeId + '').find("[data-strengthid=" + _StrengthId + "]");

        var Prototype = _APIRows.find(".calcRNDApirequirementsPrototype").val();
        var ScaleUp = _APIRows.find(".calcRNDApirequirementsScaleUp").val();
        var ExhibitBatch1 = _APIRows.find(".calcRNDApirequirementsExhibitBatch1").val();
        var ExhibitBatch2 = _APIRows.find(".calcRNDApirequirementsExhibitBatch2").val();
        var ExhibitBatch3 = _APIRows.find(".calcRNDApirequirementsExhibitBatch3").val();
        var PrototypeCost = _APIRows.find(".calcRNDApirequirementsPrototypeCost").val();
        var ScaleUpCost = _APIRows.find(".calcRNDApirequirementsScaleUpCost").val();
        var ExhibitBatchCost = _APIRows.find(".calcRNDApirequirementsExhibitBatchCost").val();

        var _Sum = 0;
        $.each($(this).parent().parent().find("input[type=number]"), function (index, item) {
            if ($(item).attr("class").indexOf("TotalStrength") === -1) {
                _Sum += ConvertToNumber(($(item).val() == "" ? 0 : $(item).val()));
            }
        });

        // set total for the row
        $(this).parent().parent().find(".TotalStrength").val(formatNumber(_Sum));

        var _TotalPrototypeCost = 0;
        $.each($('.calcRNDApirequirementsPrototypeCost'), function () {
            _TotalPrototypeCost += ConvertToNumber($(this).val() == "" ? 0 : $(this).val());
        });
        $('.APIReqPrototypeCostTotalCost').val(formatNumber(_TotalPrototypeCost));

        var _TotalScaleupCost = 0;
        $.each($('.calcRNDApirequirementsScaleUpCost'), function () {
            _TotalScaleupCost += ConvertToNumber($(this).val() == "" ? 0 : $(this).val());
        });
        $('.APIReqScaleupCostTotalCost').val(formatNumber(_TotalScaleupCost));

        var _TotalExhibitBatchCost = 0;
        $.each($('.calcRNDApirequirementsExhibitBatchCost'), function () {
            _TotalExhibitBatchCost += ConvertToNumber($(this).val() == "" ? 0 : $(this).val());
        });
        $('.APIReqExhibitBatchTotalCost').val(formatNumber(_TotalExhibitBatchCost));

        //prototype cost
        var prototypecost = (marketprice * (Prototype == "" ? 0 : ConvertToNumber(Prototype)));
        $('.ApiRequirement_' + _BioStudyTypeId).find("[data-strengthid=" + _StrengthId + "]").find(".calcRNDApirequirementsPrototypeCost").val(formatNumber(prototypecost));
        //scale up cost
        var scaleupcost = (marketprice * (ScaleUp == "" ? 0 : ConvertToNumber(ScaleUp)));
        $('.ApiRequirement_' + _BioStudyTypeId).find("[data-strengthid=" + _StrengthId + "]").find(".calcRNDApirequirementsScaleUpCost").val(formatNumber(scaleupcost));
        //exhibit cost
        var exhibitcost = ((marketprice * ((ExhibitBatch1 == "" ? 0 : ConvertToNumber(ExhibitBatch1)) + (ExhibitBatch2 == "" ? 0 : ConvertToNumber(ExhibitBatch2)) + (ExhibitBatch3 == "" ? 0 : ConvertToNumber(ExhibitBatch3)))));
        $('.ApiRequirement_' + _BioStudyTypeId).find("[data-strengthid=" + _StrengthId + "]").find(".calcRNDApirequirementsExhibitBatchCost").val(formatNumber(exhibitcost));

        // formula to calculate the total cost for one strength (all the properties) (one column)
        var totalapirequirement = ((prototypecost == "" ? 0 : ConvertToNumber(prototypecost)) + ((scaleupcost == "" ? 0 : ConvertToNumber(scaleupcost)) + ((exhibitcost == "" ? 0 : ConvertToNumber(exhibitcost)))));
        // set total for all the property in the table for one strength
        $('.ApiRequirement_' + _BioStudyTypeId + 'Total').find("[data-strengthid=" + _StrengthId + "]").find(".calcTotalCostForStrength").val(formatNumber(totalapirequirement));

        var _TotalSum = 0;
        $.each($('.ApiRequirement_' + _BioStudyTypeId + 'Total').find(".calcTotalCostForStrength"), function (index, item) {
            _TotalSum += ConvertToNumber(($(item).val() == "" ? 0 : $(item).val()));
        });

        // set total for all the property in the table
        $('.ApiRequirement_' + _BioStudyTypeId + 'Total').find(".calcTotalCostForStrengthTotal").val(formatNumber(_TotalSum));
        SetPhaseWiseBudget();
    });
    /*Reference Product Details*/
    $(document).on("change", ".calcRNDRPDUnitCostOfReferenceProduct, .calcRNDRPDFormulationDevelopment, .calcRNDRPDPilotBe, .calcRNDRPDPharmasuiticalEquivalence, .calcRNDRPDPivotalBio", function () {

        var _BioStudyTypeId = $(this).parent().parent().attr("data-biostudytypeid");

        $.each($('.RPDcal_' + _BioStudyTypeId), function (index, item) {
            var _TotalForStrength = 0;
            $.each(_strengthArray, function (i, t) {
                var _StrengthId = t.pidfProductStrengthId;
                var strengthElement = $(item).find('[data-strengthid=' + _StrengthId + ']').find(".RPDStrengthValue");
                if (!strengthElement.hasClass('calcRNDRPDUnitCostOfReferenceProduct')) {
                    var UnitCost = $('#tablerndreferenceproductdetail').find('[data-strengthid=' + _StrengthId + ']').find('.calcRNDRPDUnitCostOfReferenceProduct').val();
                    var _strengthValue = $(item).find('[data-strengthid=' + _StrengthId + ']').find(".RPDStrengthValue").val();
                    _TotalForStrength += ConvertToNumber(UnitCost == "" ? 0 : UnitCost) * ConvertToNumber(_strengthValue == "" ? 0 : _strengthValue);
                }
            });
            $(item).find(".TotalStrength").val(formatNumber(_TotalForStrength));
        });

        var _TotalCostForAllStrength = 0;
        $.each(_strengthArray, function (i, t) {
            var _TotalCostForStrength = 0;
            var _StrengthId = t.pidfProductStrengthId;
            $.each($('.RPDcal_' + _BioStudyTypeId), function (index, item) {
                var strengthElement = $(item).find('[data-strengthid=' + _StrengthId + ']').find(".RPDStrengthValue");
                if (!strengthElement.hasClass('calcRNDRPDUnitCostOfReferenceProduct')) {
                    var UnitCost = $('#tablerndreferenceproductdetail').find('[data-strengthid=' + _StrengthId + ']').find('.calcRNDRPDUnitCostOfReferenceProduct').val();
                    var _strengthValue = $(item).find('[data-strengthid=' + _StrengthId + ']').find(".RPDStrengthValue").val();
                    _TotalCostForStrength += ConvertToNumber(UnitCost == "" ? 0 : UnitCost) * ConvertToNumber(_strengthValue == "" ? 0 : _strengthValue);
                }
            });
            _TotalCostForAllStrength += _TotalCostForStrength;
            $('.RPDcal_' + _BioStudyTypeId + 'Total').find('[data-strengthid=' + _StrengthId + ']').find('.calcTotalCostForStrengthRPD').val(formatNumber(_TotalCostForStrength));
        });
        $('.RPDcal_' + _BioStudyTypeId + 'Total').find('.calcTotalCostForStrengthTotalRPD').val(formatNumber(_TotalCostForAllStrength));
        SetPhaseWiseBudget();
    });
    /*Man power cost*/
    $(document).on("change", "#RNDMasterEntities_ManHourRate", function () {
        $("input[class~='rndMPCDurationInDays']").trigger('change');
    });
    $(document).on("change", ".rndMPCDurationInDays, .rndMPCManPowerInDays", function () {
        var manhourrate = $("#RNDMasterEntities_ManHourRate").val();
        manhourrate = ConvertToNumber((manhourrate == "" ? 0 : manhourrate));
        var _BioStudyTypeId = $(this).parent().parent().attr("data-biostudytypeid");
        var _activityId = $(this).parent().parent().attr("data-activityid");

        var _duration = $('#tablerrndmanpowercostprojectduration').find("[data-activityid=" + _activityId + "]").find(".rndMPCDurationInDays").val();
        var _manPower = $('#tablerrndmanpowercostprojectduration').find("[data-activityid=" + _activityId + "]").find(".rndMPCManPowerInDays").val();
        var _calculateDays = ConvertToNumber(_duration == "" ? 0 : _duration) * ConvertToNumber(_manPower == "" ? 0 : _manPower);

        //$('#tablerrndmanpowercostprojectduration').find("[data-activityid=" + _activityId + "]").find(".sumstrength").val(_calculateDays);

        var totalHours = (_calculateDays * 8);
        $('#tablerrndmanpowercostprojectduration').find("[data-activityid=" + _activityId + "]").find(".rndMPCStrengthValue").val(formatNumber((totalHours / _strengthArray.length)));

        $('#tablerrndmanpowercostprojectduration').find("[data-activityid=" + _activityId + "]").find(".TotalStrength").val(formatNumber(totalHours));

        var _TotalCostForAllStrength = 0;
        var _TotalCostForAllPrototype = 0;
        var _TotalCostForAllScaleup = 0;
        var _TotalCostForAllExhibit = 0;
        $.each(_strengthArray, function (i, t) {
            var _TotalCostForStrength = 0;
            var _TotalCostForPrototype = 0;
            var _TotalCostForScaleup = 0;
            var _TotalCostForExhibit = 0;

            var _StrengthId = t.pidfProductStrengthId;
            $.each($('.manpowercost_' + _BioStudyTypeId), function (index, item) {
                var _strengthValue = ConvertToNumber($(item).find('[data-strengthid=' + _StrengthId + ']').find(".rndMPCStrengthValue").val());
                var _totalValue = ConvertToNumber(manhourrate * (_strengthValue == "" ? 0 : _strengthValue));
                var _a = ConvertToNumber($(this).attr("data-activityid"));
                if (_a == 1 || _a == 2 || _a == 3 || _a == 4 || _a == 5) {
                    _TotalCostForPrototype += _totalValue;
                } else if (_a == 6) {
                    _TotalCostForScaleup += _totalValue;
                } else if (_a == 7 || _a == 8 || _a == 9 || _a == 10 || _a == 11) {
                    _TotalCostForExhibit += _totalValue;
                }
                _TotalCostForStrength += _totalValue
            });
            _TotalCostForAllPrototype += ConvertToNumber(_TotalCostForPrototype);
            _TotalCostForAllScaleup += ConvertToNumber(_TotalCostForScaleup);
            _TotalCostForAllExhibit += ConvertToNumber(_TotalCostForExhibit);

            _TotalCostForAllStrength += ConvertToNumber(_TotalCostForStrength);

            $('.manpowercost_' + _BioStudyTypeId + 'Total').find('[data-strengthid=' + _StrengthId + ']').find('.calcTotalPrototypeCostForStrength').val(formatNumber(_TotalCostForPrototype));
            $('.manpowercost_' + _BioStudyTypeId + 'Total').find('[data-strengthid=' + _StrengthId + ']').find('.calcTotalScaleupCostForStrength').val(formatNumber(_TotalCostForScaleup));
            $('.manpowercost_' + _BioStudyTypeId + 'Total').find('[data-strengthid=' + _StrengthId + ']').find('.calcTotalExhibitCostForStrength').val(formatNumber(_TotalCostForExhibit));

            $('.manpowercost_' + _BioStudyTypeId + 'Total').find('[data-strengthid=' + _StrengthId + ']').find('.calcTotalCostForStrength').val(formatNumber(_TotalCostForStrength));
        });

        $('.manpowercost_' + _BioStudyTypeId + 'Total').find('.calcTotalPrototypeCostForStrengthTotal').val(formatNumber(_TotalCostForAllPrototype));
        $('.manpowercost_' + _BioStudyTypeId + 'Total').find('.calcTotalScaleupCostForStrengthTotal').val(formatNumber(_TotalCostForAllScaleup));
        $('.manpowercost_' + _BioStudyTypeId + 'Total').find('.calcTotalExhibitCostForStrengthTotal').val(formatNumber(_TotalCostForAllExhibit));

        $('.manpowercost_' + _BioStudyTypeId + 'Total').find('.calcTotalCostForStrengthTotal').val(formatNumber(_TotalCostForAllStrength));
        SetPhaseWiseBudget();
    });
    /*Pakaging Material*/
    $(document).on("change", ".rndPackingTypeId, .rndPackagingRsperUnit, .rndPackagingStrengthValue", function () {
        var _ActivityTypeId = $(this).parent().parent().attr("data-activitytypeid");

        $.each($('.packagingActivity' + _ActivityTypeId), function (index, item) {
            var _TotalForStrength = 0;
            $.each(_strengthArray, function (i, t) {
                var _StrengthId = t.pidfProductStrengthId;
                var _strengthValue = $(item).find('[data-strengthid=' + _StrengthId + ']').find(".rndPackagingStrengthValue").val();
                _TotalForStrength += ConvertToNumber(_strengthValue == "" ? 0 : _strengthValue);
            });
            $(item).find(".TotalStrength").val(formatNumber(_TotalForStrength));
        });

        var _TotalCostForAllStrength = 0;
        $.each(_strengthArray, function (i, t) {
            var _TotalCostForStrength = 0;
            var _StrengthId = t.pidfProductStrengthId;
            $.each($('.packagingActivity' + _ActivityTypeId), function (index, item) {
                var _strengthValue = $(item).find('[data-strengthid=' + _StrengthId + ']').find(".rndPackagingStrengthValue").val();
                var rsPerTest = ConvertToNumber($(item).find(".rndPackagingRsperUnit").val() == "" ? 0 : $(item).find(".rndPackagingRsperUnit").val());
                _TotalCostForStrength += (rsPerTest * ConvertToNumber(_strengthValue == "" ? 0 : _strengthValue));
            });
            _TotalCostForAllStrength += _TotalCostForStrength;
            $('.packagingActivity' + _ActivityTypeId + 'Total').find('[data-strengthid=' + _StrengthId + ']').find('.calcTotalCostForStrengthpackaging').val(formatNumber(_TotalCostForStrength));
        });
        $('.packagingActivity' + _ActivityTypeId + 'Total').find('.calcTotalCostForPackaging' + _ActivityTypeId).val(formatNumber(_TotalCostForAllStrength));

        var _FinalPKCost = 0;
        $.each(_strengthArray, function (i, t) {
            var _TotalCostForPK = 0;
            $.each($('#tablerndpackagingmaterialrequirement').find('[data-strengthid=' + t.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthpackaging'), function (index, item) {
                _TotalCostForPK += ConvertToNumber($(item).val() == "" ? 0 : $(item).val());
            });
            $('.calcTotalCostPKRow').find('[data-strengthid=' + t.pidfProductStrengthId + ']').find('.calcFinalCostForStrength').val(formatNumber(_TotalCostForPK));
            _FinalPKCost += _TotalCostForPK;
        });
        $('.calcTotalCostPKRow').find('.PackagingFinalTotal').val(formatNumber(_FinalPKCost));
        SetPhaseWiseBudget();
    });
    /*Excipient Requirement*/
    $(document).on("change", "#RNDBatchSizeId", function () {
        $("input[class~='rndExicipientRsperkg']").trigger('change');
    });
    $(document).on("change", ".rndExicipientPrototype, .rndExicipientRsperkg, .rndExicipientQuantity, .rndExicipientStrengthValue", function () {
        var _ActivityTypeId = $(this).parent().parent().attr("data-activitytypeid");

        var batchvalue = $("#RNDBatchSizeId option:selected").text();

        if (batchvalue.toString().toLowerCase() == "liters") {
            $('#spAPIRequirementMarketPrice').text("(Rs/gm)");
        } else {
            $('#spAPIRequirementMarketPrice').text("(Rs/kg)");
        }

        $.each($('.exicipientActivity' + _ActivityTypeId), function (index, item) {
            var _TotalForStrength = 0;
            $.each(_strengthArray, function (i, t) {
                var _StrengthId = t.pidfProductStrengthId;
                var _batchSizePrototype = GetValueFromBatchSize(_ActivityTypeId, _StrengthId);
                var _strengthValue = $(item).find('[data-strengthid=' + _StrengthId + ']').find(".rndExicipientStrengthValue").val();
                _TotalForStrength += ConvertToNumber(_strengthValue == "" ? 0 : _strengthValue) * ConvertToNumber(_batchSizePrototype == "" ? 0 : _batchSizePrototype);
            });
            _TotalForStrength = ((_TotalForStrength) / (batchvalue.toString().toLowerCase() == "liters" ? 1000.0 : 1000000.0));
            $(item).find(".rndExicipientQuantity").val(_TotalForStrength.toFixed(2));
            var _rsPerKg = $(item).find(".rndExicipientRsperkg").val();
            $(item).find(".TotalStrength").val(formatNumber(((_rsPerKg == "" ? 0 : _rsPerKg) * _TotalForStrength)));
        });

        var _TotalCostForAllStrength = 0;
        $.each(_strengthArray, function (i, t) {
            var _TotalCostForStrength = 0;
            var _StrengthId = t.pidfProductStrengthId;
            var _batchSizePrototype = GetValueFromBatchSize(_ActivityTypeId, _StrengthId);
            $.each($('.exicipientActivity' + _ActivityTypeId), function (index, item) {
                var _TotalQuantityForStrength = 0;
                var _strengthValue = $(item).find('[data-strengthid=' + _StrengthId + ']').find(".rndExicipientStrengthValue").val();
                _TotalQuantityForStrength = ConvertToNumber(_strengthValue == "" ? 0 : _strengthValue) * ConvertToNumber(_batchSizePrototype == "" ? 0 : _batchSizePrototype);
                _TotalQuantityForStrength = ((_TotalQuantityForStrength) / (batchvalue.toString().toLowerCase() == "liters" ? 1000.0 : 1000000.0));
                var _rsPerKg = $(item).find(".rndExicipientRsperkg").val();
                _TotalCostForStrength += ((_rsPerKg == "" ? 0 : _rsPerKg) * _TotalQuantityForStrength);
            });
            _TotalCostForAllStrength += _TotalCostForStrength;
            $('.exicipientActivity' + _ActivityTypeId + 'Total').find('[data-strengthid=' + _StrengthId + ']').find('.calcTotalCostForStrengthexicipient').val(formatNumber(_TotalCostForStrength));
        });
        $('.exicipientActivity' + _ActivityTypeId + 'Total').find('.calcTotalCostForexicipient' + _ActivityTypeId).val(formatNumber(_TotalCostForAllStrength));

        var _FinalEXCost = 0;
        $.each(_strengthArray, function (i, t) {
            var _TotalCostForEx = 0;
            $.each($('#tablerndexicipientrequirement').find('[data-strengthid=' + t.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthexicipient'), function (index, item) {
                _TotalCostForEx += ConvertToNumber($(item).val() == "" ? 0 : $(item).val());
            });
            $('.calcTotalCostExRow').find('[data-strengthid=' + t.pidfProductStrengthId + ']').find('.calcFinalCostForStrength').val(formatNumber(_TotalCostForEx));
            _FinalEXCost += _TotalCostForEx;
        });
        $('.calcTotalCostExRow').find('.exicipientFinalTotal').val(formatNumber(_FinalEXCost));
        SetPhaseWiseBudget();
    });
    /*Analytical Tab*/
    $(document).on("change", ".AnalyticalTestTypeId, .analyticalRsTest, .analyticalNumberOfTest, .analyticalStrengthValue", function () {
        var _ActivityTypeId = $(this).parent().parent().attr("data-activitytypeid");
        $('.analyticalActivity' + _ActivityTypeId + 'Total').find('.calcTotalCostForStrengthAnalytical').val("0");
        $('.calcTotalCostAnalyticalRow').find('.calcTotalCostForAnalytical').val("0");
        var totalanalyticalCost = 0;
        var totalAnalyticalCostForStrength = 0;

        $.each($('.analyticalActivity' + _ActivityTypeId + ''), function (index, item) {
            var RsTest = ConvertToNumber($(this).find(".analyticalRsTest").val() == "" ? 0 : $(this).find(".analyticalRsTest").val());
            var _Sum = 0;
            $.each($(this).find(".analyticalStrengthValue"), function (index, item) {
                var _StrengthId = $(item).parent().attr("data-strengthid");
                var strengthval = ConvertToNumber($(item).val() == "" ? 0 : $(item).val());
                var _calculatedValue = ((RsTest) * (strengthval));
                var _currentStrengthTotalElement = $('.analyticalActivity' + _ActivityTypeId + 'Total').find('[data-strengthid=' + _StrengthId + ']').find('.calcTotalCostForStrengthAnalytical');
                var _currentStrengthTotal = (_currentStrengthTotalElement.val() == "" ? 0 : ConvertToNumber(_currentStrengthTotalElement.val()));

                _Sum += _calculatedValue;
                _currentStrengthTotal += _calculatedValue;
                _currentStrengthTotalElement.val(formatNumber(_currentStrengthTotal));
            });
            // set total for the row
            $(this).find(".TotalStrength").val(formatNumber(_Sum));
        });

        $.each($('.calcTotalCostForStrengthAnalytical'), function (index, item) {
            var _StrengthId = $(item).parent().attr("data-strengthid");
            var strengthval = ConvertToNumber($(item).val() == "" ? 0 : $(item).val());
            var _currentStrengthTotalElement = $('.calcTotalCostAnalyticalRow').find('[data-strengthid=' + _StrengthId + ']').find('.calcTotalCostForAnalytical');
            var _currentStrengthTotal = (_currentStrengthTotalElement.val() == "" ? 0 : ConvertToNumber(_currentStrengthTotalElement.val()));
            _currentStrengthTotal += strengthval;
            _currentStrengthTotalElement.val(formatNumber(_currentStrengthTotal));
        });
        $.each($('.analyticalActivity' + _ActivityTypeId + 'Total').find(".calcTotalCostForStrengthAnalytical"), function (index, item) {
            totalAnalyticalCostForStrength += ConvertToNumber($(item).val() == "" ? 0 : $(item).val());
        });
        $.each($('.calcTotalCostAnalyticalRow').find('.calcTotalCostForAnalytical'), function (index, item) {
            totalanalyticalCost += ConvertToNumber($(item).val() == "" ? 0 : $(item).val());
        });

        $('.analyticalActivity' + _ActivityTypeId + 'Total').find('.calcTotalCostForAnalytical' + _ActivityTypeId + '').val(formatNumber(totalAnalyticalCostForStrength));
        $('.AnalyticalFinalTotal').val(formatNumber(totalanalyticalCost));
        SetPhaseWiseBudget();
    });
    $(document).on("change", ".analyticalTotalAMVCost, .rndanalyticalStrengthIsChecked", function () {
        var _TotalAMVCost = ($('.analyticalTotalAMVCost').val() == "" ? 0 : $('.analyticalTotalAMVCost').val());
        $('.analyticalAMVStrengthValue').val("");
        $.each($('.rndanalyticalStrengthIsChecked:checked'), function () {
            $(this).parent().find(".analyticalAMVStrengthValue").val(formatNumber((_TotalAMVCost / $('.rndanalyticalStrengthIsChecked:checked').length)));
        });
        if ($('.rndanalyticalStrengthIsChecked:checked').length > 0) {
            $('.analyticalTotalAMVCostStrength').val(formatNumber(_TotalAMVCost));
        } else {
            $('.analyticalTotalAMVCostStrength').val("");
        }
        SetPhaseWiseBudget();
    });
    /*Tooling Change Part Cost*/
    $(document).on("change", ".rndToolingChangePartCost, .ToolingChangePartStrengthValue", function () {
        var _ActivityTypeId = $(this).parent().parent().attr("data-activitytypeid");

        $.each($('.ToolingChangePartActivity' + _ActivityTypeId), function (index, item) {
            var _TotalForStrength = 0;
            $.each(_strengthArray, function (i, t) {
                var _StrengthId = t.pidfProductStrengthId;
                var _strengthValue = $(item).find('[data-strengthid=' + _StrengthId + ']').find(".ToolingChangePartStrengthValue").val();
                var _ToolingCost = $(item).find(".rndToolingChangePartCost").val();
                _TotalForStrength += ConvertToNumber(_ToolingCost == "" ? 0 : _ToolingCost) * ConvertToNumber(_strengthValue == "" ? 0 : _strengthValue);
            });
            $(item).find(".TotalStrength").val(formatNumber(_TotalForStrength));
        });

        var _TotalCostForAllStrength = 0;
        $.each(_strengthArray, function (i, t) {
            var _TotalCostForStrength = 0;
            var _StrengthId = t.pidfProductStrengthId;
            $.each($('.ToolingChangePartActivity' + _ActivityTypeId), function (index, item) {
                var _strengthValue = $(item).find('[data-strengthid=' + _StrengthId + ']').find(".ToolingChangePartStrengthValue").val();
                var rsPerTest = ConvertToNumber($(item).find(".rndToolingChangePartCost").val() == "" ? 0 : $(item).find(".rndToolingChangePartCost").val());
                _TotalCostForStrength += (rsPerTest * (_strengthValue == "" ? 0 : _strengthValue));
            });
            _TotalCostForAllStrength += _TotalCostForStrength;
            $('.ToolingChangePartActivity' + _ActivityTypeId + 'Total').find('[data-strengthid=' + _StrengthId + ']').find('.calcTotalCostForStrengthTooling').val(formatNumber(_TotalCostForStrength));
        });
        $('.ToolingChangePartActivity' + _ActivityTypeId + 'Total').find('.calcTotalCostForTooling' + _ActivityTypeId).val(formatNumber(_TotalCostForAllStrength));

        var _FinalTCCost = 0;
        $.each(_strengthArray, function (i, t) {
            var _TotalCostForTC = 0;
            $.each($('#tablerndtoolingchangepart').find('[data-strengthid=' + t.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthTooling'), function (index, item) {
                _TotalCostForTC += ConvertToNumber($(item).val() == "" ? 0 : $(item).val());
            });
            $('.calcTotalCostTCRow').find('[data-strengthid=' + t.pidfProductStrengthId + ']').find('.calcFinalCostForStrength').val(formatNumber(_TotalCostForTC));
            _FinalTCCost += _TotalCostForTC;
        });
        $('.calcTotalCostTCRow').find('.ToolingFinalTotal').val(formatNumber(_FinalTCCost));
        SetPhaseWiseBudget();
    });
    /*Plant support cost*/
    $(document).on("change", "#RNDMasterEntities_PlanSupportCostRsPerDay", function () {
        $("input[class~='calcRNDPlantSupportCostsScaleUp']").trigger('change');
        $("input[class~='calcRNDPlantSupportCostsExhibit']").trigger('change');
    });
    $(document).on("change", ".calcRNDPlantSupportCostsScaleUp, .calcRNDPlantSupportCostsExhibit", function () {
        var rsPerTest = $("#RNDMasterEntities_PlanSupportCostRsPerDay").val() == "" ? 0 : $("#RNDMasterEntities_PlanSupportCostRsPerDay").val();
        var _ActivityTypeId = $(this).parent().parent().attr("data-activitytypeid");
        var _StrengthId = $(this).parent().attr("data-strengthid");
        var _plantSupportCostRows = $('.PlantSupportCost_' + _ActivityTypeId + '').find("[data-strengthid=" + _StrengthId + "]");

        //var Prototype = _APIRows.find(".calcRNDPlantSupportCostsPrototype").val();
        var ScaleUp = _plantSupportCostRows.find(".calcRNDPlantSupportCostsScaleUp").val();
        var Exhibit = _plantSupportCostRows.find(".calcRNDPlantSupportCostsExhibit").val();

        var _Sum = 0;
        $.each($(this).parent().parent().find("input[type=number]"), function (index, item) {
            if ($(item).attr("class").indexOf("TotalStrength") === -1) {
                _Sum += ConvertToNumber(($(item).val() == "" ? 0 : $(item).val()));
            }
        });

        // set total for the row
        $(this).parent().parent().find(".TotalStrength").val(formatNumber(_Sum));

        var _TotalCostForStrength = ((ConvertToNumber(ScaleUp == "" ? 0 : ScaleUp) + ConvertToNumber(Exhibit == "" ? 0 : Exhibit)) * ConvertToNumber(rsPerTest));
        $('.PlantSupportCost_' + _ActivityTypeId + 'Total').find('[data-strengthid=' + _StrengthId + ']').find('.calcTotalCostForStrength').val(formatNumber(_TotalCostForStrength));
        var _TotalCostForAllStrength = 0;
        $.each($('.PlantSupportCost_' + _ActivityTypeId + 'Total').find(".calcTotalCostForStrength"), function (index, item) {
            var _strengthValue = ConvertToNumber($(this).val());
            _TotalCostForAllStrength += (_strengthValue == "" ? 0 : _strengthValue);
        });
        $('.PlantSupportCost_' + _ActivityTypeId + 'Total').find('.calcTotalCostForStrengthTotal').val(formatNumber(_TotalCostForAllStrength));
        SetPhaseWiseBudget();
    });
    /*Capex Misc Cost*/
    $(document).on("change", ".CapexMiscStrengthValue", function () {
        var _ActivityTypeId = $(this).parent().parent().attr("data-activitytypeid");

        $.each($('.CapexMiscActivity' + _ActivityTypeId), function (index, item) {
            var _TotalForStrength = 0;
            $.each(_strengthArray, function (i, t) {
                var _StrengthId = t.pidfProductStrengthId;
                var _strengthValue = $(item).find('[data-strengthid=' + _StrengthId + ']').find(".CapexMiscStrengthValue").val();
                _TotalForStrength += ConvertToNumber(_strengthValue == "" ? 0 : _strengthValue);
            });
            $(item).find(".TotalStrength").val(formatNumber(_TotalForStrength));
        });

        var _TotalCostForAllStrength = 0;
        $.each(_strengthArray, function (i, t) {
            var _TotalCostForStrength = 0;
            var _StrengthId = t.pidfProductStrengthId;
            $.each($('.CapexMiscActivity' + _ActivityTypeId), function (index, item) {
                var _strengthValue = $(item).find('[data-strengthid=' + _StrengthId + ']').find(".CapexMiscStrengthValue").val();
                _TotalCostForStrength += ConvertToNumber(_strengthValue == "" ? 0 : _strengthValue);
            });
            _TotalCostForAllStrength += _TotalCostForStrength;
            $('.CapexMiscActivity' + _ActivityTypeId + 'Total').find('[data-strengthid=' + _StrengthId + ']').find('.calcTotalCostForStrengthMisc').val(formatNumber(_TotalCostForStrength));
        });
        $('.CapexMiscActivity' + _ActivityTypeId + 'Total').find('.calcTotalCostForMisc' + _ActivityTypeId).val(formatNumber(_TotalCostForAllStrength));
        SetPhaseWiseBudget();
    });
    // Filling Expense
    $(document).on("change", ".rndFillingExpensesStrengthCheckbox, .rndFillingExpensesRegionId, .rndFillingExpensesTotalCost", function () {
        var _activityTypeId = $(this).parent().parent().attr("data-activitytypeid");

        if ($(this).hasClass("rndFillingExpensesStrengthCheckbox")) {
            if ($(this).is(":checked")) {
                var _selectedBusinessUnit = $(this).parent().parent().find(".rndFillingExpensesRegionId").val();
                if (_selectedBusinessUnit == "" || _selectedBusinessUnit == null || _selectedBusinessUnit == undefined) {
                    $(this).prop("checked", false);
                    toastr.error("Please select business unit");
                    return;
                }
            }
        }

        var _cost = $(this).parent().parent().find(".rndFillingExpensesTotalCost").val();
        var _totalStrength = $(this).parent().parent().find('.rndFillingExpensesStrengthCheckbox:checked').length;
        /*if ($(this).parent().parent().find('.rndFillingExpensesStrengthCheckbox:checked').length > 0) {*/
        $.each($(this).parent().parent().find('.rndFillingExpensesStrengthCheckbox'), function (index, it) {
            if ($(it).is(":checked")) {
                $(it).parent().find(".FillingExpensesStrengthValue").val((_cost == "" ? 0 : _cost) / _totalStrength);
            } else {
                $(it).parent().find(".FillingExpensesStrengthValue").val("");
            }

        });
        if ($(this).parent().parent().find('.rndFillingExpensesStrengthCheckbox:checked').length > 0) {
            $(this).parent().parent().find('.rndTotalFillingExpenseStrength').val((_cost == "" ? 0 : _cost));
        } else {
            $(this).parent().parent().find('.rndTotalFillingExpenseStrength').val("");
        }
        /*}*/

        var _TotalCostForAllStrength = 0;
        $.each(_strengthArray, function (i, t) {
            var _TotalCostForStrength = 0;
            var _StrengthId = t.pidfProductStrengthId;
            $.each($('.FillingExpensesActivity' + _activityTypeId), function (index, item) {
                var _strengthValue = ConvertToNumber($(item).find('[data-strengthid=' + _StrengthId + ']').find(".FillingExpensesStrengthValue").val());
                _TotalCostForStrength += ConvertToNumber(_strengthValue == "" ? 0 : _strengthValue);
            });
            _TotalCostForAllStrength += _TotalCostForStrength;
            $('.FillingExpensesActivity' + _activityTypeId + 'Total').find('[data-strengthid=' + _StrengthId + ']').find('.calcTotalCostForStrengthFilling').val(formatNumber(_TotalCostForStrength));
        });
        $('.FillingExpensesActivity' + _activityTypeId + 'Total').find('.calcTotalCostForStrengthTotalFilling').val(formatNumber(_TotalCostForAllStrength));
        SetPhaseWiseBudget();
    });

    getPIDFAccordion(_PIDFAccordionURL, _PIDFID, "dvPIDFAccrdion");
    getIPDAccordion(_IPDAccordionURL, _EncPIDFID, _PIDFBusinessUnitId, "dvIPDAccrdion");
    getCommercialAccordion(_CommercialAccordionURL, _EncPIDFID, _PIDFBusinessUnitId, "dvCommercialAccrdion");
    //BindRA();
    //$('#custom-tabs-department-RA-tab').click( function () {
       // GetRa(_PIDFID, _PIDFPBFId);
    //});
   
    $('#btnNextRnDTabSelectedValue').val(0); //custom-tabs-department-RnD-tab-
});

function convertFormToJSON() {
    const array = $('form').serializeArray(); 
    const json = {};
    $.each(array, function () {
        json[this.name] = this.value;
    });
    return json;
}
function PostPBFFormbyNext() {
    objMainForm = {};
    var formdata = convertFormToJSON();
    $.extend(objMainForm, { 'PIDFId': parseInt(_PIDFID) });
    $.extend(objMainForm, { 'pbfEntity': formdata });
    ajaxServiceMethod('/PBF/PBF', 'POST', PostPBFFormbyNextSuccess, PostPBFFormbyNextError, JSON.stringify(objMainForm));
}
function PostPBFFormbyNextSuccess(data) {
    try {
       // $('#SavePIDFModel').modal('hide');
        if (data._Success === true) {
            toastr.success(data._Message);
            //IsShowCancel_Save_buttons(true);
           // window.location.href = "/PIDF/PIDFList?ScreenId=4";
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Save Commercial Error:' + e.message);
    }
}

function PostPBFFormbyNextError(x, y, z) {
    toastr.error('');
}

$('#btnNextRnDTab').click(function () {
    var NextTabIndex = parseInt($('#btnNextRnDTabSelectedValue').val()) + 1;   
    var newxtTabId = '#custom-tabs-department-RnD-tab-' + arrRnDTabList[NextTabIndex];
    $('#btnNextRnDTabSelectedValue').val(NextTabIndex);
    $(newxtTabId).click();
    PostPBFFormbyNext();
});
$('[id^=custom-tabs-department-RnD-tab-]').click(function () {
    var tabid = $(this).attr('id');
    var arrtabid = tabid.split('-')[5];
   var indexOftab = arrRnDTabList.indexOf(arrtabid);
    $('#btnNextRnDTabSelectedValue').val(indexOftab);
   
   
});

function GetPBFDropdown() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllPBF + "/" + _PIDFID, 'GET', GetPBFDropdownSuccess, GetPBFDropdownError);
}
function GetPBFDropdownSuccess(data) {
    try {
        if (data != null) {
            $.each(data.MasterBusinessUnit, function (index, object) {
                $('#MarketMappingId').append($('<option>').text(object.businessUnitName).attr('value', object.businessUnitId));
            });
            $('#MarketMappingId').select2({ dropdownAdapter: $.fn.select2.amd.require('select2/selectAllAdapter'), placeholder: "Select Market..", });

            var _emptyOption = '<option value="">-- Select --</option>';
            $('#BERequirementId').append(_emptyOption);
            $('#PbfDosageFormId').append(_emptyOption);
            $('#PlantId').append(_emptyOption);
            $('#ddlPlantId_Tab').append(_emptyOption);
            $('#WorkflowId').append(_emptyOption);
            $('#FillingTypeId').append(_emptyOption);
            $('#PbfFormRNDDivisionId').append(_emptyOption);
            $('#PbfPackagingTypeId').append(_emptyOption);
            // $('#PbfManufacturingId').append(_emptyOption);
            $('#PbfRFDCountryId').append(_emptyOption);
            //$('#raAllCountryId0').append(_emptyOption);
            $('#ProductTypeId').append(_emptyOption);
            $('#GeneralProjectComplexity').append(_emptyOption);
            // $('#GeneralProductTypeId').append(_emptyOption);
            $('#GeneralFormulationGLId').append(_emptyOption);
            $('#GeneralAnalyticalGLId').append(_emptyOption);
            $(data.MasterBERequirement).each(function (index, item) {
                $('#BERequirementId').append('<option value="' + item.beRequirementId + '">' + item.beRequirementName + '</option>');
                // BindRaCountry($("#BERequirementId").val());
            });
            $(data.MasterDosage).each(function (index, item) {
                $('#PbfDosageFormId').append('<option value="' + item.dosageId + '">' + item.dosageName + '</option>');
            });
            $(data.MasterPlant).each(function (index, item) {
                $('#PlantId').append('<option value="' + item.plantId + '">' + item.plantNameName + '</option>');
            });
            $(data.MasterPlant).each(function (index, item) {
                $('#ddlPlantId_Tab').append('<option value="' + item.plantId + '">' + item.plantNameName + '</option>');
            });
            $(data.MasterWorkflow).each(function (index, item) {
                $('#WorkflowId').append('<option value="' + item.workflowId + '">' + item.workflowName + '</option>');
            });
            $(data.MasterFilingType).each(function (index, item) {
                $('#FillingTypeId').append('<option value="' + item.filingTypeId + '">' + item.filingTypeName + '</option>');
            });
            $(data.MasterFormRnDDivision).each(function (index, item) {
                $('#PbfFormRNDDivisionId').append('<option value="' + item.formRnDDivisionId + '">' + item.formRnDDivisionName + '</option>');
            });
            $(data.MasterPackagingType).each(function (index, item) {
                $('#PbfPackagingTypeId').append('<option value="' + item.packagingTypeId + '">' + item.packagingTypeName + '</option>');
            });
            //$(data.MasterManufacturing).each(function (index, item) {
            //    $('#PbfManufacturingId').append('<option value="' + item.manufacturingId + '">' + item.manufacturingName + '</option>');
            //});
            $(data.MasterCountry).each(function (index, item) {
                $('#PbfRFDCountryId').append('<option value="' + item.countryID + '">' + item.countryName + '</option>');
                //$('#raAllCountryId0').append('<option value="' + item.countryID + '">' + item.countryName + '</option>');
            });
            for (var i = 1; i < 6; i++) {
                $('#GeneralProjectComplexity').append('<option value="' + i + '">' + i + '</option>');
            }
            $(data.MasterProductType).each(function (index, item) {
                $('#ProductTypeId').append('<option value="' + item.productTypeId + '">' + item.productTypeName + '</option>');
            });
            //$(data.MasterProductType).each(function (index, item) {
            //    $('#GeneralProductTypeId').append('<option value="' + item.productTypeId + '">' + item.productTypeName + '</option>');
            //});
            $(data.MasterFormulationGL).each(function (index, item) {
                $('#GeneralFormulationGLId').append('<option value="' + item.userId + '">' + item.fullName + '</option>');
            });
            $(data.MasterAnalyticalGL).each(function (index, item) {
                $('#GeneralAnalyticalGLId').append('<option value="' + item.userId + '">' + item.fullName + '</option>');
            });
            $(data.MasterTestLicense).each(function (index, item) {
                $('#testlicence').append('&nbsp;<input type="checkbox" name="TestLicenseAvailability" id="License' + item.testLicenseId + '" value="' + item.testLicenseId + '">&nbsp;' + item.testLicenseName);
            });

            try {
                if ($('#ProjectName').val() == "") {
                    $('#ProjectName').val(data.PIDFEntity[0].moleculeName);
                }
                if ($('#PatentStatus').val() == "") {
                    if (data.PIDFIPDEntity.length > 0) {
                        $('#PatentStatus').val(data.PIDFIPDEntity[0].patentStatus);
                        //$('#PatentStatus').val('active');
                    }
                }
                if (data.PIDFEntity.length > 0) {
                    //refereceProduct details _ old Code
                    //$('#dvPBFContainer').find('#BrandName').val(data.PIDFEntity[0].rfdBrand);
                    //$('#dvPBFContainer').find('#RFDApplicant').val(data.PIDFEntity[0].rfdApplicant);
                    //$('#dvPBFContainer').find('#RFDIndication').val(data.PIDFEntity[0].rfdIndication);

                    //$('#hdnBrandName').val(data.PIDFEntity[0].rfdBrand);
                    //$('#hdnPbfRFDCountryId').val(data.PIDFEntity[0].rfdCountryId);
                    //$('#PbfRFDCountryId').val($('#hdnPbfRFDCountryId').val() == 0 ? "" : $('#hdnPbfRFDCountryId').val());
                    //$('#RFDCountryId').val(data.PIDFEntity[0].rfdCountryId);
                    //$('#hdnRFDApplicant').val(data.PIDFEntity[0].rfdApplicant);
                    //$('#hdnRFDIndication').val(data.PIDFEntity[0].rfdIndication);
                }

                if (_PIDFPBFId > 0) {
                    $("#Pidfpbfid").val(_PIDFPBFId);
                    $("#PBFGeneralId").val($("#hdnPBFGeneralId").val() == 0 ? "" : $('#hdnPBFGeneralId').val());
                    $('#BERequirementId').val($('#hdnBERequirementId').val() == 0 ? "" : $('#hdnBERequirementId').val());
                    $('#PbfDosageFormId').val($('#hdnPbfDosageFormId').val() == 0 ? "" : $('#hdnPbfDosageFormId').val());
                    $('#PlantId').val($('#hdnPlantId').val() == 0 ? "" : $('#hdnPlantId').val());

                    $('#WorkflowId').val($('#hdnWorkflowId').val() == 0 ? "" : $('#hdnWorkflowId').val());
                    $('#FillingTypeId').val($('#hdnFillingTypeId').val() == 0 ? "" : $('#hdnFillingTypeId').val());
                    $('#PbfFormRNDDivisionId').val($('#hdnPbfFormRNDDivisionId').val() == 0 ? "" : $('#hdnPbfFormRNDDivisionId').val());
                    $('#PbfPackagingTypeId').val($('#hdnPbfPackagingTypeId').val() == 0 ? "" : $('#hdnPbfPackagingTypeId').val());
                    // $('#PbfManufacturingId').val($('#hdnPbfManufacturingId').val() == 0 ? "" : $('#hdnPbfManufacturingId').val());
                    $('#PbfRFDCountryId').val($('#hdnPbfRFDCountryId').val() == 0 ? "" : $('#hdnPbfRFDCountryId').val());
                    $('#ProductTypeId').val($('#hdnProductTypeId').val() == 0 ? "" : $('#hdnProductTypeId').val());
                    $('#GeneralProjectComplexity').val($('#hdnGeneralProjectComplexity').val() == 0 ? "" : $('#hdnGeneralProjectComplexity').val());
                    //$('#GeneralProductTypeId').val($('#hdnGeneralProductTypeId').val() == 0 ? "" : $('#hdnGeneralProductTypeId').val());
                    $('#GeneralFormulationGLId').val($('#hdnGeneralFormulationGLId').val() == 0 ? "" : $('#hdnGeneralFormulationGLId').val());
                    $('#GeneralAnalyticalGLId').val($('#hdnGeneralAnalyticalGLId').val() == 0 ? "" : $('#hdnGeneralAnalyticalGLId').val());
                    $("#MarketMappingId").val($("#hdnMarketMappingIds").val().split(',')).trigger('change');
                    var license = $("#TestLicenseAvailability").val().split(',');
                    $.each(license, function (index, item) {
                        $("#License" + item.trim()).prop("checked", true);
                    });
                }
            } catch (e) {
            }
            _strengthArray = data.PIDFStrengthEntity;
            PBFBindBusinessUnit(data.MasterBusinessUnit);

            GetPBFTabDetails();
            UserwiseBusinessUnit = UserWiseBUList.split(',');
            SetPBFBU_Tab();
            /*SetPBFDisableForOtherUserBU(); */
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPBFDropdownError(x, y, z) {
    toastr.error(ErrorMessage);
}
$('#PlantId').on('change', function () {
    $('#ddlPlantId_Tab').val($('#PlantId').val()).trigger("change");
});
$('#ddlPBFLine').on('change', function () {
    var SelectedLineid = parseInt($('#ddlPBFLine').val());
    $('#RNDMasterEntities_PBFLine').val(SelectedLineid);
    var filterPlantLine = $.grep(PBFLinesArr, function (n) {
        return n.lineId === SelectedLineid
    });
    if (filterPlantLine != undefined && filterPlantLine.length > 0)
        $('#RNDMasterEntities_PlanSupportCostRsPerDay').val(filterPlantLine[0].lineCost).trigger('change');

});
$('#ddlPlantId_Tab').change(function (e) {
    if ($(this).val() != "") {
        if (parseInt($(this).val()) > 0) {
            $('#RNDMasterEntities_PlantId_Tab').val(parseInt($(this).val()));
            ajaxServiceMethod($('#hdnBaseURL').val() + getLineByPlantId + "/" + parseInt($(this).val()), 'GET', getLineByPlantIdSuccess, getLineByPlantIdError);
        }
    }
});
function getLineByPlantIdSuccess(data) {
    try {
        $('#ddlPBFLine').find('option').remove()
        PBFLinesArr = data._object;
        if (data._object != null && data._object.length > 0) {
            var _emptyOption = '<option value="">-- Select --</option>';
            $('#ddlPBFLine').append(_emptyOption);
            $(data._object).each(function (index, item) {
                $('#ddlPBFLine').append('<option value="' + item.lineId + '">' + item.lineName + '</option>');
            });

            try {
                if (rndmasterdata_dbValueOf_lineId > 0) {
                    $('#ddlPBFLine').val(rndmasterdata_dbValueOf_lineId);
                    $('#RNDMasterEntities_PBFLine').val(rndmasterdata_dbValueOf_lineId);
                }
            } catch (e) {
            }
        }
    }
    catch (e) {
        toastr.error(ErrorMessage);
    }
}
function getLineByPlantIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
function BindReferenceProductDetails(data) {
    if (data.length > 0) {
        $('#dvPBFContainer').find('#BrandName').val(data[0].rfdBrand);
        $('#dvPBFContainer').find('#RFDApplicant').val(data[0].rfdApplicant);
        $('#dvPBFContainer').find('#RFDIndication').val(data[0].rfdIndication);
        $('#PbfRFDCountryId').val(data[0].rfdCountryId);

        $('#dvPBFContainer').find('#RFDInnovators').val(data[0].rfdInnovators);
        $('#dvPBFContainer').find('#RFDInitialRevenuePotential').val(data[0].rfdInitialRevenuePotential);
        $('#dvPBFContainer').find('#RFDPriceDiscounting').val(data[0].rfdPriceDiscounting);
        $('#dvPBFContainer').find('#RFDCommercialBatchSize').val(data[0].rfdCommercialBatchSize);
    }
}
function GetPBFTabDetails() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetPBFAllTabDetails + "/" + _PIDFID + "/" + _selectBusinessUnit, 'GET', GetPBFTabDetailsSuccess, GetPBFTabDetailsError);
}
function GetPBFTabDetailsSuccess(data) {
    try {
        if (data != null) {
            //_currencySymbol = (data.IPDCostOfLitigation.length > 0 ? data.IPDCostOfLitigation[0].currencySymbol : "$");
            _CostOfLitigationArray = data.IPDCostOfLitigation;
            CreatePhaseWiseBudgetTable();
            CreateTotalExpensesTable();
            var _emptyOption = '<option value="">-- Select --</option>';
            // PBFBindStrength(data.PIDFPBFGeneralStrength);
            BindClinical(data.PBFClinicalEntity);
            BindAnalytical(data.PBFAnalyticalEntity, data.PBFAnalyticalCostEntity);
            // BindRA();
            bindRaDropDowns();
            $('#RNDBatchSizeId').append(_emptyOption);
            $(data.MasterBatchSize).each(function (index, item) {
                $('#RNDBatchSizeId').append('<option value="' + item.batchSizeNumberId + '">' + item.batchSizeNumberName + '</option>');
            });
            if (data.PBFRNDMasterEntity.length > 0) {
                if (data.PBFRNDMasterEntity[0].batchSizeId > 0) {
                    $('#RNDBatchSizeId').val(data.PBFRNDMasterEntity[0].batchSizeId == 0 ? "" : data.PBFRNDMasterEntity[0].batchSizeId);
                }
            }
            BindRNDBatchSize(data.PBFRNDBatchSize, data.PBFRNDMasterEntity);
            BindRNDExicipient(data.PBFRNDExicipientEntity);
            BindRNDPackaging(data.PBFRNDPackagingEntity);
            BindRNDAPIRequirement(data.PBFRNDAPIRequirement);
            BindRNDToolingchangepart(data.PBFRNDToolingChangePart);
            BindRNDCapexMiscExpenses(data.PBFRNDCapexMiscExpenses);
            BindRNDPlantSupportCost(data.PBFRNDPlantSupportCost);
            BindRNDReferenceProductDetail(data.PBFRNDReferenceProductDetail);
            BindRNDFillingExpenses(data.PBFRNDFillingExpenses, data.MasterBusinessUnit);
            BindRNDManPowerCost(data.PBFRNDManPowerCost)
            BindHeadWiseBudget(data.HeadWiseBudget)
            BindReferenceProductDetails(data.PBFReferenceProductDetail)
            $(data.MasterTestType).each(function (index, item) {
                $('.AnalyticalTestTypeId').append('<option value="' + item.testTypeId + '" data-TestTypeCode="' + item.testTypeCode + '" data-TestTypePrice="' + item.testTypePrice + '">' + item.testTypeCode + ": " + item.testTypeName + '</option>');
            });
            $(_strengthArray).each(function (index, item) {
                $('.AMVstrengths').append('<option value="' + item.pidfProductStrengthId + '">' + getStrengthName(item.pidfProductStrengthId) + '</option>');
            });
            $(data.MasterPackingType).each(function (index, item) {
                $('.rndPackingTypeId').append('<option value="' + item.packingTypeId + '" data-PackingUnit="' + item.unit + '" data-PackingCost="' + item.packingCost + '">' + item.packingTypeName + '</option>');
            });
            $(data.RNDExicipientPrototype).each(function (index, item) {
                $('.rndExicipientPrototype').append('<option value="' + item.excipientRequirementId + '" data-ExcipientCost="' + item.excipientRequirementCost + '">' + item.excipientRequirementName + '</option>');
            });
            $.each($('.AnalyticalTestTypeId'), function (index, item) {
                if ($(this).next().val() != undefined && $(this).next().val() != null) {
                    $(this).val($(this).next().val());
                }
            });
            $.each($('.rndPackingTypeId'), function (index, item) {
                if ($(this).next().val() != undefined && $(this).next().val() != null) {
                    $(this).val($(this).next().val());
                }
            });
            $.each($('.rndExicipientPrototype'), function (index, item) {
                if ($(this).next().val() != undefined && $(this).next().val() != null) {
                    $(this).val($(this).next().val());
                }
            });
           
            //  alert('<option value="' + this.value + '">' + this.text + '</option>');
           
            SetChildRowDeleteIconPBF();
            if (_mode == 1) {
                PBFreadOnlyForm();
            }
            SetPBFDisableForOtherUserBU();
            _firstLoad = false;
            SetPhaseWiseBudget();
            SetHeadWiseBudget();
            PBFTabsReadOnly();
            $('input[type="number"]').on("keypress keyup blur", function (event) {
                var patt = new RegExp(/[0-9]*[.]{1}[0-9]{2}/i);
                var matchedString = $(this).val().match(patt);
                if (matchedString) {
                    $(this).val(matchedString);
                }
                if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                    let checkval = parseInt($(this).val());
                    if (checkval < 0) {
                        $(this).val("");
                    }
                    event.preventDefault();
                }
            });
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPBFTabDetailsError(x, y, z) {
    toastr.error(ErrorMessage);
}

function getStrengthName(strengthId) {
    var _filteredStrength = $.grep(_strengthArray, function (n, i) {
        return n.pidfProductStrengthId === strengthId;
    });
    if (_filteredStrength != null && _filteredStrength != undefined && _filteredStrength.length > 0) {
        return _filteredStrength[0].strength + " " + _filteredStrength[0].unitofMeasurementName;
    } else { return ""; }
}
function getValueFromStrengthByProrotype(data, strengthId, propertyName, excipientProrotype) {
    var _filteredStrength = $.grep(data, function (n, i) {
        return n.strengthId === strengthId && n.exicipientPrototype === excipientProrotype;
    });
    if (_filteredStrength != null && _filteredStrength != undefined && _filteredStrength.length > 0) {
        if (_filteredStrength[0][propertyName] != null && _filteredStrength[0][propertyName] != undefined) {
            return _filteredStrength[0][propertyName];
        } else { return ""; }
    } else { return ""; }
}
function getValueFromStrengthId(data, strengthId, propertyName) {
    var _filteredStrength = $.grep(data, function (n, i) {
        return n.strengthId === strengthId;
    });
    if (_filteredStrength != null && _filteredStrength != undefined && _filteredStrength.length > 0) {
        if (_filteredStrength[0][propertyName] != null && _filteredStrength[0][propertyName] != undefined) {
            return _filteredStrength[0][propertyName];
        } else { return ""; }
    } else { return ""; }
}
function getValueFromStrengthTestTypeId(data, strengthId, propertyName, testTypeId) {
    var _filteredStrength = $.grep(data, function (n, i) {
        return n.strengthId === strengthId && n.testTypeId == testTypeId;
    });
    if (_filteredStrength != null && _filteredStrength != undefined && _filteredStrength.length > 0) {
        if (_filteredStrength[0][propertyName] != null && _filteredStrength[0][propertyName] != undefined) {
            return _filteredStrength[0][propertyName];
        } else { return ""; }
    } else { return ""; }
}
function getValueFromStrengthBusinessUnitId(data, strengthId, propertyName, businessUnitId) {
    var _filteredStrength = $.grep(data, function (n, i) {
        return n.strengthId === strengthId && n.businessUnitId == businessUnitId;
    });
    if (_filteredStrength != null && _filteredStrength != undefined && _filteredStrength.length > 0) {
        if (_filteredStrength[0][propertyName] != null && _filteredStrength[0][propertyName] != undefined) {
            return (_filteredStrength[0][propertyName] ? "checked" : "");
        } else { return ""; }
    } else { return ""; }
}
function getCheckboxCheckedStrength(data, strengthId, propertyName) {
    var _filteredStrength = $.grep(data, function (n, i) {
        return n.strengthId === strengthId;
    });
    if (_filteredStrength != null && _filteredStrength != undefined && _filteredStrength.length > 0) {
        if (_filteredStrength[0][propertyName] != null && _filteredStrength[0][propertyName] != undefined) {
            return (_filteredStrength[0][propertyName] ? "checked" : "");
        } else { return ""; }
    } else { return ""; }
}

function getValueFromStrengthPackingTypeId(data, strengthId, propertyName, packingTypeId) {
    var _filteredStrength = $.grep(data, function (n, i) {
        return n.strengthId === strengthId && n.packingTypeId == packingTypeId;
    });
    if (_filteredStrength != null && _filteredStrength != undefined && _filteredStrength.length > 0) {
        if (_filteredStrength[0][propertyName] != null && _filteredStrength[0][propertyName] != undefined) {
            return _filteredStrength[0][propertyName];
        } else { return ""; }
    } else { return ""; }
}
function getValueFromStrengthByPlantSupportDevelopment(data, strengthId, propertyName, plantSupportDevelopment) {
    var _filteredStrength = $.grep(data, function (n, i) {
        return n.strengthId === strengthId && n.plantSupportDevelopment === plantSupportDevelopment;
    });
    if (_filteredStrength != null && _filteredStrength != undefined && _filteredStrength.length > 0) {
        if (_filteredStrength[0][propertyName] != null && _filteredStrength[0][propertyName] != undefined) {
            return _filteredStrength[0][propertyName];
        } else { return ""; }
    } else { return ""; }
}
function getValueFromStrengthByMiscellaneousDevelopment(data, strengthId, propertyName, miscellaneousDevelopment) {
    var _filteredStrength = $.grep(data, function (n, i) {
        return n.strengthId === strengthId && n.miscellaneousDevelopment === miscellaneousDevelopment;
    });
    if (_filteredStrength != null && _filteredStrength != undefined && _filteredStrength.length > 0) {
        if (_filteredStrength[0][propertyName] != null && _filteredStrength[0][propertyName] != undefined) {
            return _filteredStrength[0][propertyName];
        } else { return ""; }
    } else { return ""; }
}
function SetPBFDisableForOtherUserBU() {
    var BU_VALUE = $("#PIDFBusinessUnitId").val();
    var status = UserwiseBusinessUnit.indexOf(BU_VALUE);
    //var IsViewInMode = ($("#hdnPBFIsView").val() == '1')
    /*if (status == -1 || IsViewInMode) {*/
    if (status == -1) {
        // SetPBFFormReadonly();
        PBFreadOnlyForm();
    }
    //else {
    //    $("#dvPBFContainer").find("input, button, submit, textarea, select,a,i").prop('disabled', false);
    //}
}
//function SetPBFFormReadonly() {
//    $("#dvPBFContainer").find("input, button, submit, textarea, select,a,i").prop('disabled', true);
//}
function PBFBUtabClick(pidfidval, BUVal) {
    SelectedBUValue = 0;
    var i, tabcontent, butab;
    SelectedBUValue = BUVal;
    $("#BusinessUnitId").val(SelectedBUValue);
    $("#PbfFormEntities_BusinessUnitId").val(SelectedBUValue);
    butab = document.getElementsByClassName("BUtab");
    for (i = 0; i < butab.length; i++) {
        butab[i].className = butab[i].className.replace(" active", "");
    }
    //SetPBFDisableForOtherUserBU();
    var BUAnchorId = '#BUtab_' + BUVal;
    $(BUAnchorId).addClass('active');
    if (_mode > 0) window.location.href = 'PBF?pidfid=' + btoa(pidfidval) + '&bui=' + btoa(BUVal) + '&pbf=' + _pbf + '&IsView=' + _mode;
    else window.location.href = 'PBF?pidfid=' + btoa(pidfidval) + '&bui=' + btoa(BUVal) + '&pbf=' + _pbf;
}

function SetPBFBU_Tab() {
    var PIDFBusinessUnitId = 0;

    if ($("#BusinessUnitId").val() > 0)
        PIDFBusinessUnitId = $("#BusinessUnitId").val();
    else
        PIDFBusinessUnitId = $("#PIDFBusinessUnitId").val();
    SelectedBUValue = PIDFBusinessUnitId;
    $("#Pidfid").val(_PIDFID);
    var pidfId = $("#PIDFId").val();
    var BUAnchorId = '#BUtab_' + PIDFBusinessUnitId;
    $(BUAnchorId).addClass('active');
    //window.location.href = 'PBFClinicalDetailsForm?pidfid=' + btoa(pidfId) + '&bui=' + btoa(SelectedBUValue) + '&strength=' + btoa(selectedStrength);
}

function PBFBindBusinessUnit(data) {
    var businessUnitHTML = "";
    var businessUnitPanel = "";
    $.each(data, function (index, item) {
        businessUnitHTML += '<li class="nav-item p-0">\
            <a class="nav-link '+ (item.businessUnitId == _selectBusinessUnit ? "active" : "") + ' px-2" href="#custom-tabs-' + item.businessUnitId + '" data-toggle="pill" aria-selected="true" onclick="PBFBUtabClick(' + _PIDFID + ', ' + item.businessUnitId + ')" id="custom-tabs-two-' + item.businessUnitId + '-tab">' + item.businessUnitName + '</a></li>';
    });
    $('#dvPBFContainer').find('#custom-tabs-two-tab').html(businessUnitHTML);
}
function PBFBindStrength(data) {
    var strengthHTML = '<thead class="bg-primary"><tr>';
    $.each(_strengthArray, function (index, item) {
        strengthHTML += '<td><input type="hidden" class="control-label" id="GeneralStrengthEntities[' + index + '].StrengthId" name="GeneralStrengthEntities[' + index + '].StrengthId" value="' + item.pidfProductStrengthId + '" /><b>' + getStrengthName(item.pidfProductStrengthId) + '</b></td>';
    });
    strengthHTML += "</tr></thead>";
    strengthHTML += "<tbody><tr>";
    for (var count = 0; count < _strengthArray.length; count++) {
        strengthHTML += '<td><input type="text" class="form-control" id="GeneralStrengthEntities[' + [count] + '].ProjectCode" name="GeneralStrengthEntities[' + [count] + '].ProjectCode" placeholder="Project Code" value="' + (getValueFromStrengthId(data, _strengthArray[count].pidfProductStrengthId, "projectCode")) + '" /></td>';
    }
    strengthHTML += '</tr>';
    strengthHTML += "<tr>";
    for (var count = 0; count < _strengthArray.length; count++) {
        strengthHTML += '<td> <input type="text" class="form-control" id="GeneralStrengthEntities[' + [count] + '].ImprintingEmbossingCode" name="GeneralStrengthEntities[' + [count] + '].ImprintingEmbossingCode" placeholder="Imprinting Embossing Code" value="' + (getValueFromStrengthId(data, _strengthArray[count].pidfProductStrengthId, "imprintingEmbossingCode")) + '" /></td>';
    }
    strengthHTML += '</tr></tbody>';

    $('#tableGeneralStrength').html(strengthHTML);
}

function SavePBFForm(_SaveType) {
    $('.pbftablesplantCost').show();
    if (_SaveType == 'Draft') {
        $('#AddPBFForm').validate().settings.ignore = "*";

    } else {
        validateDynamicControldDetailsPBF();
    }

    if ($("#AddPBFForm").valid()) {
        //var abc = new Date();
        //toastr.error(abc.toString());
        $('#loading-wrapper').show();
    }
    setlicense();
    SetAnalyticalChildRows();
    SetPhaseWiseBudget();
    SetHeadWiseBudget();
    SetRNDChildRows();
    $('#AddPBFForm').find('#SaveType').val(_SaveType);
    return false;
}
function setlicense() {
    var selected = new Array();
    $.each($("input[name='TestLicenseAvailability']:checked"), function () {
        selected.push($(this).val());
    });
    $("#TestLicenseAvailability").val(selected.join(","))
}
//Clinical Start
function CreateClinicalTable(data, bioStudyTypeId) {
    var objectname = "";
    var tableTitle = "";
    var fastingOrFed = "";
    if (bioStudyTypeId == 1 || bioStudyTypeId == 3) {
        fastingOrFed = "Fasting";
    } else {
        fastingOrFed = "Fed";
    }
    if (bioStudyTypeId == 1 || bioStudyTypeId == 2) {
        tableTitle = "Pilot Bio";
    } else {
        tableTitle = "Pivotal Bio";
    }

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (_strengthArray.length + 2) + '">' + tableTitle + " " + fastingOrFed + '</td>';

    var _iterator = (bioStudyTypeId - 1) * _strengthArray.length;

    objectname += "<tr class='clinicalcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>" + fastingOrFed + "</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="hidden" class="hdnBioStudyTypeId" id="ClinicalEntities[' + [(i + _iterator)] + '].BioStudyTypeId" name="ClinicalEntities[' + [(i + _iterator)] + '].BioStudyTypeId" value="' + bioStudyTypeId + '" /><input type="hidden" id="ClinicalEntities[' + [(i + _iterator)] + '].StrengthId" name="ClinicalEntities[' + [(i + _iterator)] + '].StrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control calcFastingOrFed" id="ClinicalEntities[' + [(i + _iterator)] + '].FastingOrFed" name="ClinicalEntities[' + [(i + _iterator)] + '].FastingOrFed" placeholder="' + fastingOrFed + '" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "fastingOrFed")) + '" /></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /></td></tr>";

    objectname += "<tr class='clinicalcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Number of Volunteers</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="number" class="form-control calcNoOfVolunteers" id="ClinicalEntities[' + [(i + _iterator)] + '].NumberofVolunteers" name="ClinicalEntities[' + [(i + _iterator)] + '].NumberofVolunteers" placeholder="Number of Volunteers" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "numberofVolunteers")) + '"  /></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /></td></tr>";

    objectname += "<tr class='clinicalcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Clinical Cost/Vol.</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> <input type="number" class="form-control calcClinicalCost" id="ClinicalEntities[' + [(i + _iterator)] + '].ClinicalCostAndVolume" name="ClinicalEntities[' + [(i + _iterator)] + '].ClinicalCostAndVolume" placeholder="Clinical Cost And Volume" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "clinicalCostAndVolume")) + '" /></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /></td></tr>";

    objectname += "<tr  class='clinicalcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Bio analytical Cost/Vol.</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> <input type="number" class="form-control calcBioAnalyticalCost" id="ClinicalEntities[' + [(i + _iterator)] + '].BioAnalyticalCostAndVolume" name="ClinicalEntities[' + [(i + _iterator)] + '].BioAnalyticalCostAndVolume" placeholder="Bio Analytical Cost And Volume" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "bioAnalyticalCostAndVolume")) + '" /></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /></td></tr>";

    objectname += "<tr class='clinicalcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Doc. Cost/study</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> <input type="number" class="form-control calcDocCostandStudy" id="ClinicalEntities[' + [(i + _iterator)] + '].DocCostandStudy" name="ClinicalEntities[' + [(i + _iterator)] + '].DocCostandStudy" placeholder="Doc Cost and Study" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "docCostandStudy")) + '"/></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /></td></tr>";

    objectname += "<tr class='clinicalcal_" + bioStudyTypeId + "Total' data-biostudytypeid='" + bioStudyTypeId + "'><td class='text-bold'>Total Cost</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthClinical' readonly='readonly' tabindex=-1 /></td>";
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthClinicalTotal' readonly='readonly' tabindex=-1 /></td></tr>";

    return objectname;
}
function BindClinical(data) {
    var bioStudyHTML = '<thead class="bg-primary text-bold"><tr><td>Bio Study Cost</td>';
    $.each(_strengthArray, function (index, item) {
        bioStudyHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    bioStudyHTML += '<td>Total</td></tr></thead><tbody>';

    for (var i = 1; i < 5; i++) {
        bioStudyHTML += CreateClinicalTable($.grep(data, function (n, x) { return n.bioStudyTypeId == i; }), i);
    }

    bioStudyHTML += '<tr><td class="text-bold">Total Bio Study Cost</td>';
    $.each(_strengthArray, function (index, item) {
        bioStudyHTML += "<td data-strengthid='" + item.pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control totalBioStudyCost' readonly='readonly' tabindex=-1 /></td>";
    });
    bioStudyHTML += "<td>" + _currencySymbol + "<input type='text' class='form-control totalBioStudyCostTotal' readonly='readonly' tabindex=-1 /></td></tr></tbody>";
    $('#tableclinical').html(bioStudyHTML);

    $("input[class~='calcFastingOrFed']").trigger('change');
    $("input[class~='calcNoOfVolunteers']").trigger('change');
    $("input[class~='calcClinicalCost']").trigger('change');
    $("input[class~='calcBioAnalyticalCost']").trigger('change');
    $("input[class~='calcDocCostandStudy']").trigger('change');
}
//Clinical End
//Analytical Start
function CreateAnalyticalTable(costData, data, activityTypeId) {
    var objectname = "";
    var tableTitle = "";

    if (activityTypeId == 1) {
        tableTitle = "Prototype";
    } else if (activityTypeId == 2) {
        tableTitle = "Scale Up";
    } else if (activityTypeId == 3) {
        tableTitle = "Exhibit Batch";
    } else {
        tableTitle = "Total AMV Cost";
    }
    //var _iterator = (activityTypeId - 1) * _strengthArray.length;

    var _counter = (data.length == 0 ? 1 : data.length);

    var _testType = [];

    if (activityTypeId != 4) {
        objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (4 + _strengthArray.length) + '">' + tableTitle + '</td>';
        for (var a = 0; a < _counter; a++) {
            if (data.length > 0) {
                if (_testType.indexOf(data[a].testTypeId) !== -1) {
                    continue;
                }
            }
            objectname += '<tr  id="analyticalRow" class="analyticalactivity analyticalActivity' + (activityTypeId) + '" data-activitytypeid="' + activityTypeId + '">'
                + '<td><select class="form-control readonlyUpdate AnalyticalTestTypeId"><option value = "" > --Select --</option ></select><input type="hidden" value="' + (data.length > 0 ? data[a].testTypeId : "") + '" /></td>'
                + '<td style="display:none;"><input type="number" class="form-control totalAnalytical analyticalNumberOfTest" min="0" value="' + (data.length > 0 ? data[a].numberoftests : "") + '"  /></td>'
                /* + '<td><input type="text" class="form-control totalAnalytical analyticalPrototypeDevelopment" value="' + (data.length > 0 ? data[a].prototypeDevelopment : "") + '"  /></td>'*/
                + '<td><input type="number" class="form-control totalAnalytical analyticalRsTest" min="0" value="' + (data.length > 0 ? data[a].costPerTest : "") + '"  /></td>'
            for (var i = 0; i < _strengthArray.length; i++) {
                objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input class="analyticalTypeId" type="hidden" value="' + activityTypeId + '" /><input type="hidden" class="analyticalStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control analyticalStrengthValue" min="0" value="' + (data.length > 0 ? getValueFromStrengthTestTypeId(data, _strengthArray[i].pidfProductStrengthId, "prototypeCost", data[a].testTypeId) : "") + '" /></td>';
            }
            objectname += "<td><input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /></td><td> <i class='fa-solid fa-circle-plus nav-icon text-success operationButton' id='addIcon' onclick='addRowanalytical(this);'></i> <i class='fa-solid fa-trash nav-icon text-red strengthDeleteIcon operationButton DeleteIcon' onclick='deleteRowanalytical(this);' ></i></td></tr>";

            if (data.length > 0) {
                _testType.push(data[a].testTypeId);
            }
        }

        objectname += "<tr class='analyticalActivity" + activityTypeId + "Total' data-activitytypeid='" + activityTypeId + "'><td class='text-bold'>Total Cost</td><td></td>";
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthAnalytical' readonly='readonly' tabindex=-1 /></td>";
        }
        objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForAnalytical" + activityTypeId + "' readonly='readonly' tabindex=-1 /></td><td></td>";
        objectname += "</tr>";
    } else {
        objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (4 + _strengthArray.length) + '">' + tableTitle + '</td>';
        objectname += '<tr  id="analyticalRow" class="analyticalactivity analyticalActivity' + (activityTypeId) + '" data-activitytypeid="' + activityTypeId + '">'
            + '<td><input type="text" class="form-control analyticalTotalAMVTitle" id="AnalyticalAMVCosts.TotalAmvtitle" name="AnalyticalAMVCosts.TotalAmvtitle" placeholder="" value="' + (costData.length > 0 ? (costData[0].totalAMVTitle == null ? "" : costData[0].totalAMVTitle) : "") + '" /></td>'
            + '<td>' + _currencySymbol + '<input type="number" class="form-control analyticalTotalAMVCost" min="0" id="AnalyticalAMVCosts.TotalAmvcost" name="AnalyticalAMVCosts.TotalAmvcost"  placeholder="" value="' + (costData.length > 0 ? costData[0].totalAMVCost : "") + '"  /></td>'
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input class="analyticalTypeId" type="hidden" value="' + activityTypeId + '" /><input type="hidden" class="analyticalStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="checkbox" id="rndanalyticalStrengthIsChecked' + _strengthArray[i].pidfProductStrengthId + '" class="rndanalyticalStrengthIsChecked rndanalyticalStrengthIsChecked' + _strengthArray[i].pidfProductStrengthId + '" ' + (costData.length > 0 ? getCheckboxCheckedStrength(costData, _strengthArray[i].pidfProductStrengthId, "isChecked") : "") + '> &nbsp; ' + _currencySymbol + '<input type="text" class="form-control analyticalAMVStrengthValue inline-textbox" readonly="readonly" tabindex=-1 disabled="true" /></td>';
        }
        objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control analyticalTotalAMVCostStrength' readonly='readonly' tabindex=-1 /></td><td></td></tr>";

        if (data.length > 0) {
            _testType.push(data[a].testTypeId);
        }
    }
    return objectname;
}
function BindAnalytical(data, costData) {
    var analyticalactivityHTML = '<thead class="bg-primary text-bold"><tr>'
        + '<td>Test Type</td>'
        + '<td style="display:none;">Number of sample</td>'
        /* + '<td>Prototype Development</td>'*/
        + '<td>Rs /test</td>'
    $.each(_strengthArray, function (index, item) {
        analyticalactivityHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + ' (Prototype Cost)</td>';
    });
    analyticalactivityHTML += '<td width="12%">Total</td><td width="7%">Action</td></tr></thead><tbody id="tblanalyticalBody">';

    for (var i = 1; i < 5; i++) {
        analyticalactivityHTML += CreateAnalyticalTable(costData, $.grep(data, function (n, x) { return n.activityTypeId == i; }), i);
    }

    analyticalactivityHTML += '<tr class="calcTotalCostAnalyticalRow"><td colspan="2" class="text-bold">Total Cost</td>';
    $.each(_strengthArray, function (index, item) {
        analyticalactivityHTML += "<td data-strengthid='" + item.pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForAnalytical' readonly='readonly' tabindex=-1/></td>";
    });
    analyticalactivityHTML += "<td>" + _currencySymbol + "<input type='text' class='form-control AnalyticalFinalTotal' readonly='readonly' tabindex=-1 /><td></td></tr></tbody>";

    analyticalactivityHTML += '<tr><td colspan="' + (4 + _strengthArray.length) + '"><label style="vertical-align: top;margin-right: 10px;">Remark</label><textarea style="width:90% !important;" id="remark" class="form-control" id="AnalyticalAMVCosts.Remark" name="AnalyticalAMVCosts.Remark" maxlength="500" placeholder="Remark">' + (costData.length > 0 ? (costData[0].remark == null ? "" : costData[0].remark) : "") + '</textarea></td> </tr>';

    $('#tableanalytical').html(analyticalactivityHTML);

    SetChildRowDeleteIconPBF();
    //$("input[class~='AnalyticalTestTypeId']").trigger('change');
    $("input[class~='analyticalStrengthValue']").trigger('change');
    $("input[class~='rndanalyticalStrengthIsChecked']").trigger('change');
}

function addRowanalytical(element) {
    var node = $(element).parent().parent().clone(true);
    node.find("input.form-control").val("");
    $(element).parent().parent().after(node);
    SetChildRowDeleteIconPBF();
}
function deleteRowanalytical(element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIconPBF();
    $("input[class~='analyticalStrengthValue']").trigger('change');
}
function SetChildRowDeleteIconPBF() {
    for (var i = 1; i < 4; i++) {
        if ($('#tablerndexicipientrequirement tbody').find('.exicipientActivity' + i.toString()).length > 1) {
            $('#tablerndexicipientrequirement tbody').find('.exicipientActivity' + i.toString()).find('.DeleteIcon').show();
        } else {
            $('#tablerndexicipientrequirement tbody').find('.exicipientActivity' + i.toString()).find('.DeleteIcon').hide();
        }
    }
    for (var i = 1; i < 4; i++) {
        if ($('#tablerndpackagingmaterialrequirement tbody').find('.packagingActivity' + i.toString()).length > 1) {
            $('#tablerndpackagingmaterialrequirement tbody').find('.packagingActivity' + i.toString()).find('.DeleteIcon').show();
        } else {
            $('#tablerndpackagingmaterialrequirement tbody').find('.packagingActivity' + i.toString()).find('.DeleteIcon').hide();
        }
    }
    for (var i = 1; i < 4; i++) {
        if ($('#tablerndtoolingchangepart tbody').find('.ToolingChangePartActivity' + i.toString()).length > 1) {
            $('#tablerndtoolingchangepart tbody').find('.ToolingChangePartActivity' + i.toString()).find('.DeleteIcon').show();
        } else {
            $('#tablerndtoolingchangepart tbody').find('.ToolingChangePartActivity' + i.toString()).find('.DeleteIcon').hide();
        }
    }
    for (var i = 1; i < 4; i++) {
        if ($('#tableanalytical tbody').find('.analyticalActivity' + i.toString()).length > 1) {
            $('#tableanalytical tbody').find('.analyticalActivity' + i.toString()).find('.DeleteIcon').show();
        } else {
            $('#tableanalytical tbody').find('.analyticalActivity' + i.toString()).find('.DeleteIcon').hide();
        }
    }
    for (var i = 1; i < 2; i++) {
        if ($('#tablerndcapexmiscellaneousexpenses tbody').find('.CapexMiscActivity' + i.toString()).length > 1) {
            $('#tablerndcapexmiscellaneousexpenses tbody').find('.CapexMiscActivity' + i.toString()).find('.DeleteIcon').show();
        } else {
            $('#tablerndcapexmiscellaneousexpenses tbody').find('.CapexMiscActivity' + i.toString()).find('.DeleteIcon').hide();
        }
    }
    //for (var i = 1; i < 2; i++) {
    //    if ($('#tablerndplantsupportcost tbody').find('.PlantSupportCostActivity' + i.toString()).length > 1) {
    //        $('#tablerndplantsupportcost tbody').find('.PlantSupportCostActivity' + i.toString()).find('.DeleteIcon').show();
    //    } else {
    //        $('#tablerndplantsupportcost tbody').find('.PlantSupportCostActivity' + i.toString()).find('.DeleteIcon').hide();
    //    }
    //}
    for (var i = 1; i < 2; i++) {
        if ($('#tablerndfilingexpenses tbody').find('.FillingExpensesActivity' + i.toString()).length > 1) {
            $('#tablerndfilingexpenses tbody').find('.FillingExpensesActivity' + i.toString()).find('.DeleteIcon').show();
        } else {
            $('#tablerndfilingexpenses tbody').find('.FillingExpensesActivity' + i.toString()).find('.DeleteIcon').hide();
        }
    }
    //for (var i = 1; i < 4; i++) {
    //    if ($('#tableanalytical tbody tr.analyticalActivity' + i + '').length > 1) {
    //        $('.analyticalActivity' + i + '').find(".DeleteIcon").show();
    //    } else {
    //        $('.analyticalActivity' + i + '').find(".DeleteIcon").hide();
    //    }
    //    if ($('#tablerndexicipientrequirement tbody tr.exicipientActivity' + i + '').length > 1) {
    //        $('.exicipientActivity' + i + '').find(".DeleteIcon").show();
    //    } else {
    //        $('.exicipientActivity' + i + '').find(".DeleteIcon").hide();
    //    }
    //    if ($('#tablerndpackagingmaterialrequirement tbody tr.packagingActivity' + i + '').length > 1) {
    //        $('.packagingActivity' + i + '').find(".DeleteIcon").show();
    //    } else {
    //        $('.packagingActivity' + i + '').find(".DeleteIcon").hide();
    //    }

    //    if ($('#tablerndtoolingchangepart tbody tr.ToolingChangePartActivity' + i + '').length > 1) {
    //        $('.ToolingChangePartActivity' + i + '').find(".DeleteIcon").show();
    //    } else {
    //        $('.ToolingChangePartActivity' + i + '').find(".DeleteIcon").hide();
    //    }
    //    if ($('#tablerndcapexmiscellaneousexpenses tbody tr.CapexMiscActivity' + i + '').length > 1) {
    //        $('.CapexMiscActivity' + i + '').find(".DeleteIcon").show();
    //    } else {
    //        $('.CapexMiscActivity' + i + '').find(".DeleteIcon").hide();
    //    }
    //    if ($('#tablerndplantsupportcost tbody tr.PlantSupportCostActivity' + i + '').length > 1) {
    //        $('.PlantSupportCostActivity' + i + '').find(".DeleteIcon").show();
    //    } else {
    //        $('.PlantSupportCostActivity' + i + '').find(".DeleteIcon").hide();
    //    }
    //    if ($('#tablerndfilingexpenses tbody tr.FillingExpensesActivity' + i + '').length > 1) {
    //        $('.FillingExpensesActivity' + i + '').find(".DeleteIcon").show();
    //    } else {
    //        $('.FillingExpensesActivity' + i + '').find(".DeleteIcon").hide();
    //    }
    //}
}
function SetAnalyticalChildRows() {
    var _AnalyticalArray = [];
    var _AnalyticalAMVCostStrengthArray = [];

    for (var i = 1; i < 5; i++) {
        $.each($('#tableanalytical tbody tr.analyticalActivity' + i + ''), function () {
            var TestTypeId = $(this).find(".AnalyticalTestTypeId").val();
            var ActivityTypeId = $(this).find(".analyticalTypeId").val();
            var Numberoftests = $(this).find(".analyticalNumberOfTest").val();
            var PrototypeDevelopment = '100' // $(this).find(".analyticalPrototypeDevelopment").val();
            var CostPerTest = $(this).find(".analyticalRsTest").val();

            $.each($(this).find(".analyticalStrengthId"), function (index, item) {
                if (i == 4) {
                    //var TotalAMVTitle = $(this).find(".analyticalTotalAMVTitle").val();
                    //var TotalAMVCost = $(this).find(".analyticalTotalAMVCost").val();
                    //$.each($(this).find(".analyticalStrengthId"), function (index, item) {
                    var _AnalyticalAMVCostObject = new Object();
                    _AnalyticalAMVCostObject.StrengthId = $(this).val();
                    _AnalyticalAMVCostObject.IsChecked = $(this).parent().find('#rndanalyticalStrengthIsChecked' + $(this).val()).is(":checked");
                    if (_AnalyticalAMVCostObject.IsChecked == true) {
                        _AnalyticalAMVCostStrengthArray.push(_AnalyticalAMVCostObject);
                    }
                    //});
                } else {
                    var _AnalyticalObject = new Object();
                    _AnalyticalObject.StrengthId = $(this).val();
                    _AnalyticalObject.PrototypeCost = $(this).parent().find(".analyticalStrengthValue").val();
                    _AnalyticalObject.TestTypeId = TestTypeId;
                    _AnalyticalObject.ActivityTypeId = ActivityTypeId;
                    _AnalyticalObject.Numberoftests = Numberoftests;
                    _AnalyticalObject.PrototypeDevelopment = PrototypeDevelopment;
                    _AnalyticalObject.CostPerTest = CostPerTest;
                    if (_AnalyticalObject.PrototypeCost != "" && _AnalyticalObject.PrototypeCost != undefined
                        && _AnalyticalObject.PrototypeCost != null && _AnalyticalObject.TestTypeId != "0") {
                        _AnalyticalArray.push(_AnalyticalObject);
                    }
                }
            });
        });
    }

    $('#hdnAnalyticalData').val(JSON.stringify(_AnalyticalArray));
    $('#hdnAnalyticalStrengthMappingData').val(JSON.stringify(_AnalyticalAMVCostStrengthArray));
}
//Analytical End

//R&D Start
//Exicipient Requirement table start
function CreateRNDExicipientTable(data, activityTypeId) {
    var objectname = "";
    var tableTitle = "";

    if (activityTypeId == 1) {
        tableTitle = "Excipient Protoype";
    } else if (activityTypeId == 2) {
        tableTitle = "Excipient Scale Up";
    } else {
        tableTitle = "Excipient Exhibit";
    }

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (6 + _strengthArray.length) + '">' + tableTitle + '</td>';

    var _counter = (data.length == 0 ? 1 : data.length);

    var _testType = [];
    for (var a = 0; a < _counter; a++) {
        if (data.length > 0) {
            if (_testType.indexOf(data[a].exicipientPrototype) !== -1) {
                continue;
            }
        }
        objectname += '<tr  id="ExicipientRow" class="exicipientactivity exicipientActivity' + (activityTypeId) + '" data-activitytypeid="' + activityTypeId + '">'
            // + '<td><input type="text" class="form-control rndExicipientPrototype" value="' + (data.length > 0 ? data[a].exicipientPrototype : "") + '"  /></td>'
            + '<td><select class="form-control readOnlyUpdate rndExicipientPrototype"><option value = "" > --Select --</option ></select><input type="hidden" value="' + (data.length > 0 ? data[a].exicipientPrototype : "") + '" /></td>'
            + '<td><input type="number" class="form-control rndExicipientRsperkg" min="0" value="' + (data.length > 0 ? data[a].rsPerKg : "") + '"  /></td>'
            + '<td><input type="text" class="form-control rndExicipientQuantity" min="0" readonly="readonly" tabindex=-1 /><span>Kg</span></td>';//value="' + (data.length > 0 ? data[a].mgPerUnitDosage : "") + '"
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input class="rndExicipientTypeId" type="hidden" value="' + activityTypeId + '" /><input type="hidden" class="rndExicipientStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control rndExicipientStrengthValue" min="0" value="' + (data.length > 0 ? getValueFromStrengthByProrotype(data, _strengthArray[i].pidfProductStrengthId, "exicipientDevelopment", data[a].exicipientPrototype) : "") + '" /><span>Mg</span></td>';
        }
        objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /></td><td> <i class='fa-solid fa-circle-plus nav-icon text-success operationButton' id='addIcon' onclick='addRowRNDExicipient(this);'></i> <i class='fa-solid fa-trash nav-icon text-red strengthDeleteIcon operationButton DeleteIcon' onclick='deleteRowRNDExicipient(this);' ></i></td></tr>";

        if (data.length > 0) {
            _testType.push(data[a].exicipientPrototype);
        }
    }
    objectname += "<tr class='exicipientActivity" + activityTypeId + "Total' data-activitytypeid='" + activityTypeId + "'><td class='text-bold'>Total Cost</td><td></td><td></td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthexicipient' readonly='readonly' tabindex=-1 /></td>";
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength calcTotalCostForexicipient" + activityTypeId + "' readonly='readonly' tabindex=-1 /></td></tr>";
    return objectname;
}
function BindRNDExicipient(data) {
    var ExicipientActivityHTML = '<thead class="bg-primary text-bold"><tr>'
        + '<td>Excipient Prototype</td>'
        + '<td>Rs / Kg</td>'
        + '<td>Quantity</td>';
    $.each(_strengthArray, function (index, item) {
        ExicipientActivityHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    ExicipientActivityHTML += '<td>Total</td><td>Action</td></tr></thead><tbody id="tblExicipientBody">';

    for (var i = 1; i < 4; i++) {
        ExicipientActivityHTML += CreateRNDExicipientTable($.grep(data, function (n, x) { return n.activityTypeId == i; }), i);
    }

    ExicipientActivityHTML += '<tr class="calcTotalCostExRow"><td colspan="3">Total Cost</td>';
    $.each(_strengthArray, function (index, item) {
        ExicipientActivityHTML += "<td data-strengthid='" + item.pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcFinalCostForStrength' readonly='readonly' tabindex=-1 /></td>";
    });
    ExicipientActivityHTML += "<td>" + _currencySymbol + "<input type='text' class='form-control exicipientFinalTotal' readonly='readonly' tabindex=-1 /></td></tr></tbody>";
    $('#tablerndexicipientrequirement').html(ExicipientActivityHTML);
    SetChildRowDeleteIconPBF();
    //$("input[class~='rndExicipientPrototype']").trigger('change');
    $("input[class~='rndExicipientRsperkg']").trigger('change');
    //$("input[class~='rndExicipientQuantity']").trigger('change');
    /*$("input[class~='rndExicipientStrengthValue']").trigger('change');*/
}
function addRowRNDExicipient(element) {
    var node = $(element).parent().parent().clone(true);
    node.find("input.form-control").val("");
    $(element).parent().parent().after(node);
    SetChildRowDeleteIconPBF();
}
function deleteRowRNDExicipient(element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIconPBF();
    $("input[class~='rndExicipientStrengthValue']").trigger('change');
}
//Exicipient Requirement table end
//Packaging Material table start
function CreateRNDPackagingTable(data, activityTypeId) {
    var objectname = "";
    var tableTitle = "";

    if (activityTypeId == 1) {
        tableTitle = "Packaging Protoype";
    } else if (activityTypeId == 2) {
        tableTitle = "Packaging Scale Up";
    } else {
        tableTitle = "Packaging Exhibit";
    }
    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (6 + _strengthArray.length) + '">' + tableTitle + '</td>';
    var _counter = (data.length == 0 ? 1 : data.length);
    var _packagingType = [];
    for (var a = 0; a < _counter; a++) {
        if (data.length > 0) {
            if (_packagingType.indexOf(data[a].packingTypeId) !== -1) {
                continue;
            }
        }

        objectname += '<tr  id="PackagingRow" class="packagingactivity packagingActivity' + (activityTypeId) + '" data-activitytypeid="' + activityTypeId + '">'
            + '<td><select class="form-control readOnlyUpdate rndPackingTypeId"><option value = "" > --Select --</option ></select><input type="hidden" value="' + (data.length > 0 ? data[a].packingTypeId : "") + '" /></td>'
            + '<td><input type="text" class="form-control rndPackagingUnitofMeasurement" value="' + (data.length > 0 ? data[a].unitOfMeasurement : "") + '"/></td>'
            + '<td><input type="number" class="form-control rndPackagingRsperUnit" min="0"  value="' + (data.length > 0 ? data[a].rsPerUnit : "") + '" /></td>';
        //+ '<td><input type="number" class="form-control rndPackagingQuantity" min="0" value="' + (data.length > 0 ? data[a].quantity : "") + '"  /></td>'
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input class="rndPackagingActivityId" type="hidden" value="' + activityTypeId + '" /><input type="hidden" class="rndPackagingStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control rndPackagingStrengthValue" min="0" value="' + (data.length > 0 ? getValueFromStrengthPackingTypeId(data, _strengthArray[i].pidfProductStrengthId, "packagingDevelopment", data[a].packingTypeId) : "") + '"/><span>Kg</span></td>';
        }
        objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /><span>Kg</span></td><td> <i class='fa-solid fa-circle-plus nav-icon text-success operationButton' id='addIcon' onclick='addRowRNDPackaging(this);'></i> <i class='fa-solid fa-trash nav-icon text-red strengthDeleteIcon operationButton DeleteIcon'  onclick='deleteRowRNDPackaging(this);' ></i></td></tr>";

        if (data.length > 0) {
            _packagingType.push(data[a].packingTypeId);
        }
    }
    objectname += "<tr class='packagingActivity" + activityTypeId + "Total' data-activitytypeid='" + activityTypeId + "'><td class='text-bold'>Total Cost</td><td></td><td></td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthpackaging' readonly='readonly' tabindex=-1 /></td>";
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength calcTotalCostForPackaging" + activityTypeId + "' readonly='readonly' tabindex=-1 /></td></tr>";
    objectname += "</tr>";

    return objectname;
}
function BindRNDPackaging(data) {
    var PackagingActivityHTML = '<thead class="bg-primary text-bold"><tr>'
        + '<td>Packing Type</td>'
        + '<td>Unit of Measurement</td>'
        + '<td>Rs / Unit</td>';
    $.each(_strengthArray, function (index, item) {
        PackagingActivityHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    PackagingActivityHTML += '<td>Total</td><td>Action</td></tr></thead><tbody id="tblPackagingBody">';

    for (var i = 1; i < 4; i++) {
        PackagingActivityHTML += CreateRNDPackagingTable($.grep(data, function (n, x) { return n.activityTypeId == i; }), i);
    }
    //PackagingActivityHTML += '<tr><td class="text-left text-bold bg-light" colspan="10"></td></tr>';
    //PackagingActivityHTML += '<tr><td class="text-bold">Total Packaging Costs</td>';
    //PackagingActivityHTML += '<tr><td class="text-bold">Total Cost (for all Strength)</td>';
    //PackagingActivityHTML += "<td><input type='number' class='form-control PakagingMaterialTotal' readonly='readonly' tabindex=-1 /></td></tr></tbody>";
    PackagingActivityHTML += '<tr class="calcTotalCostPKRow"><td colspan="3">Total Cost</td>';
    $.each(_strengthArray, function (index, item) {
        PackagingActivityHTML += "<td data-strengthid='" + item.pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcFinalCostForStrength' readonly='readonly' tabindex=-1 /></td>";
    });
    PackagingActivityHTML += "<td>" + _currencySymbol + "<input type='text' class='form-control PackagingFinalTotal' readonly='readonly' tabindex=-1 /></td></tr></tbody>";

    $('#tablerndpackagingmaterialrequirement').html(PackagingActivityHTML);
    SetChildRowDeleteIconPBF();

    $("input[class~='rndPackagingStrengthValue']").trigger('change');
    //$("input[class~='rndPackingTypeId']").trigger('change');
}
function addRowRNDPackaging(element) {
    var node = $(element).parent().parent().clone(true);
    node.find("input.form-control").val("");
    $(element).parent().parent().after(node);
    SetChildRowDeleteIconPBF();
}
function deleteRowRNDPackaging(element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIconPBF();
    $("input[class~='rndPackagingStrengthValue']").trigger('change');
}
//Packaging Material table end
//Batch size table start
function CreateBatchsizeTable(data, bioStudyTypeId) {
    var objectname = "";

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (_strengthArray.length + 2) + '">Batch Size</td>';

    var _iterator = (bioStudyTypeId - 1) * _strengthArray.length;

    objectname += "<tr class='batchsize_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Salt</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="number" class="form-control calcRNDBatchSizesSalt" id="RNDBatchSizes[' + [(i + _iterator)] + '].salt" name="RNDBatchSizes[' + [(i + _iterator)] + '].salt" placeholder="salt" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "salt")) + '"  /></td>';
    }

    objectname += "<tr class='batchsize_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Prototype Formulation</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="hidden" id="RNDBatchSizes[' + [(i + _iterator)] + '].StrengthId" name="RNDBatchSizes[' + [(i + _iterator)] + '].StrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control BatchSize_Excipient_PrototypeFormulation_' + _strengthArray[i].pidfProductStrengthId + ' calcRNDBatchSizesPrototypeFormulation" id="RNDBatchSizes[' + [(i + _iterator)] + '].PrototypeFormulation" name="RNDBatchSizes[' + [(i + _iterator)] + '].PrototypeFormulation" placeholder="Prototype Formulation" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "prototypeFormulation")) + '" /></td>';
    }
    //objectname += "<td><input type='number' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /></td></tr>";

    objectname += "<tr class='batchsize_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Scale Up batch</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="number" class="form-control calcRNDBatchSizesScaleUpbatch" id="RNDBatchSizes[' + [(i + _iterator)] + '].ScaleUpbatch" name="RNDBatchSizes[' + [(i + _iterator)] + '].ScaleUpbatch" placeholder="ScaleUp batch" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "scaleUpbatch")) + '"  /></td>';
    }
    //objectname += "<td><input type='number' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /></td></tr>";

    objectname += "<tr class='batchsize_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Exhibit Batch 1</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> <input type="number" class="form-control calcRNDBatchSizesExhibitBatch1" id="RNDBatchSizes[' + [(i + _iterator)] + '].ExhibitBatch1" name="RNDBatchSizes[' + [(i + _iterator)] + '].ExhibitBatch1" placeholder="Exhibit Batch 1" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibitBatch1")) + '" /></td>';
    }
    //objectname += "<td><input type='number' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /></td></tr>";

    objectname += "<tr class='batchsize_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Exhibit Batch 2</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> <input type="number" class="form-control calcRNDBatchSizesExhibitBatch2" id="RNDBatchSizes[' + [(i + _iterator)] + '].ExhibitBatch2" name="RNDBatchSizes[' + [(i + _iterator)] + '].ExhibitBatch2" placeholder="Exhibit Batch 2" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibitBatch2")) + '" /></td>';
    }
    //objectname += "<td><input type='number' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /></td></tr>";

    objectname += "<tr class='batchsize_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Exhibit Batch 3</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> <input type="number" class="form-control calcRNDBatchSizesExhibitBatch3" id="RNDBatchSizes[' + [(i + _iterator)] + '].ExhibitBatch3" name="RNDBatchSizes[' + [(i + _iterator)] + '].ExhibitBatch3" placeholder="Exhibit Batch 3" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibitBatch3")) + '"/></td>';
    }
    //objectname += "<td><input type='number' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /></td></tr>";

    //objectname += "<tr class='batchsize_" + bioStudyTypeId + "Total' data-biostudytypeid='" + bioStudyTypeId + "'><td class='text-bold'>Total Cost</td>";
    //for (var i = 0; i < _strengthArray.length; i++) {
    //    objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'><input type='number' class='form-control calcTotalCostForStrength' readonly='readonly' tabindex=-1/></td>";
    //}
    //objectname += "<td><input type='number' class='form-control calcTotalCostForStrengthTotal' readonly='readonly' tabindex=-1 /></td></tr>";

    return objectname;
}
function BindRNDBatchSize(data, rndmasterdata) {
    var batchSizeHTML = '<thead class="bg-primary text-bold"><tr><td>Batch Size</td>';
    $.each(_strengthArray, function (index, item) {
        batchSizeHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    batchSizeHTML += '</tr></thead><tbody>';

    batchSizeHTML += CreateBatchsizeTable(data, 1);

    batchSizeHTML += "</tbody>";
    $('#tablerndbatchsize').html(batchSizeHTML);

    if (rndmasterdata.length > 0) {

        if (rndmasterdata[0].lineId > 0) {
            rndmasterdata_dbValueOf_lineId = rndmasterdata[0].lineId;
        }

        //if (rndmasterdata[0].lineId > 0 && $('#ddlPBFLine').find('option').length > 1) {
        //    $("#ddlPBFLine").val(rndmasterdata[0].lineId);
        //    $("#RNDMasterEntities_PBFLine").val(rndmasterdata[0].lineId);
        //} 
        $("#RNDMasterEntities_ApirequirementMarketPrice").val(rndmasterdata[0].apiRequirementMarketPrice);
        $("#RNDMasterEntities_ApirequirementVendorName").val(rndmasterdata[0].apiRequirementVendorName);
        if (rndmasterdata[0].plantId > 0) {
            $("#RNDMasterEntities_PlantId_Tab").val(rndmasterdata[0].plantId);
            $('#ddlPlantId_Tab').val(rndmasterdata[0].plantId);
            $('#ddlPlantId_Tab').trigger('change');
        }
        else {
            $('#ddlPlantId_Tab').val($('#PlantId').val()).trigger('change');
        }
        $("#RNDMasterEntities_PlanSupportCostRsPerDay").val(rndmasterdata[0].planSupportCostRsPerDay);
        $("#RNDMasterEntities_ManHourRate").val(rndmasterdata[0].manHourRate);
    }
    $("input[class~='calcRNDBatchSizesPrototypeFormulation']").trigger('change');
    $("input[class~='calcRNDBatchSizesScaleUpbatch']").trigger('change');
    $("input[class~='calcRNDBatchSizesExhibitBatch1']").trigger('change');
    $("input[class~='calcRNDBatchSizesExhibitBatch2']").trigger('change');
    $("input[class~='calcRNDBatchSizesExhibitBatch3']").trigger('change');
}
//Batch size table end
//API requirement table start
function CreateAPIrequirementTable(data, bioStudyTypeId) {
    var objectname = "";

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (_strengthArray.length + 2) + '">API Requirement</td>';

    var _iterator = (bioStudyTypeId - 1) * _strengthArray.length;

    objectname += "<tr class='ApiRequirement_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Prototype</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="hidden" id="RNDApirequirements[' + [(i + _iterator)] + '].StrengthId" name="RNDApirequirements[' + [(i + _iterator)] + '].StrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control calcRNDApirequirementsPrototype" id="RNDApirequirements[' + [(i + _iterator)] + '].Prototype" name="RNDApirequirements[' + [(i + _iterator)] + '].Prototype" placeholder="Prototype" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "prototype")) + '" /><span>Kg</span></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /><span>Kg</span></td></tr>";

    objectname += "<tr class='ApiRequirement_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Scale Up </td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="number" class="form-control calcRNDApirequirementsScaleUp" id="RNDApirequirements[' + [(i + _iterator)] + '].ScaleUp" name="RNDApirequirements[' + [(i + _iterator)] + '].ScaleUp" placeholder="ScaleUp" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "scaleUp")) + '"  /><span>Kg</span></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /><span>Kg</span></td></tr>";

    objectname += "<tr class='ApiRequirement_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Exhibit Batch 1</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> <input type="number" class="form-control calcRNDApirequirementsExhibitBatch1" id="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatch1" name="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatch1" placeholder="Exhibit Batch 1" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibitBatch1")) + '" /><span>Kg</span></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /><span>Kg</span></td></tr>";

    objectname += "<tr class='ApiRequirement_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Exhibit Batch 2</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> <input type="number" class="form-control calcRNDApirequirementsExhibitBatch2" id="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatch2" name="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatch2" placeholder="Exhibit Batch 2" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibitBatch2")) + '" /><span>Kg</span></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /><span>Kg</span></td></tr>";

    objectname += "<tr class='ApiRequirement_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Exhibit Batch 3</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> <input type="number" class="form-control calcRNDApirequirementsExhibitBatch3" id="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatch3" name="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatch3" placeholder="Exhibit Batch 3" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibitBatch3")) + '"/><span>Kg</span></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /><span>Kg</span></td></tr>";
    objectname += "<tr class='ApiRequirement_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Prototype Cost</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> ' + _currencySymbol + '<input type="text" class="form-control calcRNDApirequirementsPrototypeCost" readonly="readonly" tabindex=-1 id="RNDApirequirements[' + [(i + _iterator)] + '].PrototypeCost" name="RNDApirequirements[' + [(i + _iterator)] + '].PrototypeCost" placeholder="Prototype Cost" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "prototypeCost")) + '"/></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength APIReqPrototypeCostTotalCost' readonly='readonly' tabindex=-1 /></td></tr>";
    objectname += "<tr class='ApiRequirement_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Scale Up Cost</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> ' + _currencySymbol + '<input type="text" class="form-control calcRNDApirequirementsScaleUpCost" readonly="readonly" tabindex=-1 id="RNDApirequirements[' + [(i + _iterator)] + '].ScaleUpCost" name="RNDApirequirements[' + [(i + _iterator)] + '].ScaleUpCost" placeholder="Scale Up Cost" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "scaleUpCost")) + '"/></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength APIReqScaleupCostTotalCost' readonly='readonly' tabindex=-1 /></td></tr>";
    objectname += "<tr class='ApiRequirement_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Exhibit Batch Cost</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> ' + _currencySymbol + '<input type="text" class="form-control calcRNDApirequirementsExhibitBatchCost"  readonly="readonly" tabindex=-1 id="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatchCost" name="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatchCost" placeholder="Exhibit Batch Cost" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibitBatchCost")) + '"/></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength APIReqExhibitBatchTotalCost' readonly='readonly' tabindex=-1 /></td></tr>";
    objectname += "<tr class='ApiRequirement_" + bioStudyTypeId + "Total' data-biostudytypeid='" + bioStudyTypeId + "'><td class='text-bold'>Total Cost</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrength' readonly='readonly' tabindex=-1 /></td>";
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthTotal' readonly='readonly' tabindex=-1 /></td></tr>";

    return objectname;
}
function BindRNDAPIRequirement(data) {
    var APIRequirementHTML = '<thead class="bg-primary text-bold"><tr><td>API Requirement</td>';
    $.each(_strengthArray, function (index, item) {
        APIRequirementHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    APIRequirementHTML += '<td>Total</td></tr></thead><tbody>';

    APIRequirementHTML += CreateAPIrequirementTable(data, 1);

    APIRequirementHTML += "</tbody>";
    $('#tablerndapirequirement').html(APIRequirementHTML);

    $("input[class~='calcRNDApirequirementsPrototype']").trigger('change');
    $("input[class~='calcRNDApirequirementsScaleUp']").trigger('change');
    $("input[class~='calcRNDApirequirementsExhibitBatch1']").trigger('change');
    $("input[class~='calcRNDApirequirementsExhibitBatch2']").trigger('change');
    $("input[class~='calcRNDApirequirementsExhibitBatch3']").trigger('change');
    //$("input[class~='calcRNDApirequirementsPrototypeCost']").trigger('change');
    //$("input[class~='calcRNDApirequirementsScaleUpCost']").trigger('change');
    //$("input[class~='calcRNDApirequirementsExhibitBatchCost']").trigger('change');
}
//API requirement table end
//Tooling change part table start
function CreateToolingchangepartTable(data, activityTypeId) {
    var objectname = "";
    if (activityTypeId == 1) {
        tableTitle = "Protoype";
    } else if (activityTypeId == 2) {
        tableTitle = "Scale Up";
    } else {
        tableTitle = "Exhibit";
    }

    //var _iterator = (activityTypeId - 1) * _strengthArray.length;

    var _counter = (data.length == 0 ? 1 : data.length);

    var _activityType = [];

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (8 + _strengthArray.length) + '">' + tableTitle + '</td></tr>';
    for (var a = 0; a < _counter; a++) {
        if (data.length > 0) {
            if (_activityType.indexOf(data[a].prototype) !== -1) {
                continue;
            }
        }
        objectname += '<tr  id="ToolingChangePartRow" class="toolingchangepartactivity ToolingChangePartActivity' + (activityTypeId) + '" data-activitytypeid="' + activityTypeId + '">'
            + '<td><input type="text" class="form-control totalTCP rndToolingChangePartPrototype" value="' + (data.length > 0 ? data[a].prototype : "") + '" /></td>'
            + '<td><input type="number" class="form-control totalTCP rndToolingChangePartCost" min="0" value="' + (data.length > 0 ? data[a].cost : "") + '"  /></td>'
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input class="rndToolingChangePartActivityId" type="hidden" value="' + activityTypeId + '" /><input type="hidden" class="rndToolingChangePartStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control ToolingChangePartStrengthValue" min="0"  value="' + (data.length > 0 ? getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "strengthUnitQuantity") : "") + '" /></td>';
        }
        objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control totalTCP TotalStrength' readonly='readonly' tabindex=-1 /></td><td> <i class='fa-solid fa-circle-plus nav-icon text-success operationButton' id='addIcon' onclick='addRowToolingChangePart(this);'></i> <i class='fa-solid fa-trash nav-icon text-red strengthDeleteIcon operationButton DeleteIcon' onclick='deleteRowToolingChangePart(this);' ></i></td></tr>";
        if (data.length > 0) {
            _activityType.push(data[a].prototype);
        }
    }
    objectname += "<tr class='ToolingChangePartActivity" + activityTypeId + "Total' data-activitytypeid='" + activityTypeId + "'><td class='text-bold'>Total Cost</td><td></td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthTooling' readonly='readonly' tabindex=-1 /></td>";
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength calcTotalCostForTooling" + activityTypeId + "' readonly='readonly' tabindex=-1 /></td></tr>";
    objectname += "</tr>";
    return objectname;
}
function BindRNDToolingchangepart(data) {
    var toolingchangepartHTML = '<thead class="bg-primary text-bold"><tr>'
        + '<td> </td>'
        + '<td>Cost</td>'
    $.each(_strengthArray, function (index, item) {
        toolingchangepartHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + ' (Cost)</td>';
    });
    toolingchangepartHTML += '<td>Total</td><td>Action</td></tr></thead><tbody id="tblToolingChangePartBody">';
    for (var i = 1; i < 4; i++) {
        toolingchangepartHTML += CreateToolingchangepartTable($.grep(data, function (n, x) { return n.activityTypeId == i; }), i);
    }

    toolingchangepartHTML += '<tr class="calcTotalCostTCRow"><td colspan="2">Total Cost</td>';
    $.each(_strengthArray, function (index, item) {
        toolingchangepartHTML += "<td data-strengthid='" + item.pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcFinalCostForStrength' readonly='readonly' tabindex=-1 /></td>";
    });
    toolingchangepartHTML += "<td>" + _currencySymbol + "<input type='text' class='form-control ToolingFinalTotal' readonly='readonly' tabindex=-1 /></td></tr></tbody>";

    $('#tablerndtoolingchangepart').html(toolingchangepartHTML);

    SetChildRowDeleteIconPBF();

    /*$("input[class~='ToolingChangePartStrengthValue']").trigger('change');*/
    $("input[class~='rndToolingChangePartCost']").trigger('change');
}
function addRowToolingChangePart(element) {
    var node = $(element).parent().parent().clone(true);
    node.find("input.form-control").val("");
    $(element).parent().parent().after(node);
    SetChildRowDeleteIconPBF();
}
function deleteRowToolingChangePart(element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIconPBF();
    $("input[class~='ToolingChangePartStrengthValue']").trigger('change');
}
//Tooling change part table end
//Capex and Misc Exp table start
function CreateCapexMiscTable(data, activityTypeId) {
    var objectname = "";
    var tableTitle = "Capex and Miscellaneous Expenses";

    //var _iterator = (activityTypeId - 1) * _strengthArray.length;

    var _counter = (data.length == 0 ? 1 : data.length);

    var _activityType = [];

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (6 + _strengthArray.length) + '">' + tableTitle + '</td>';
    for (var a = 0; a < _counter; a++) {
        if (data.length > 0) {
            if (_activityType.indexOf(data[a].miscellaneousDevelopment) !== -1) {
                continue;
            }
        }
        objectname += '<tr  id="CapexMiscRow" class="CapexMiscactivity CapexMiscActivity' + (activityTypeId) + '" data-activitytypeid="' + activityTypeId + '">'
            + '<td><input type="text" class="form-control totalCapexMisc rndCapexMiscMiscellaneous" value="' + (data.length > 0 ? data[a].miscellaneousDevelopment : "") + '" /></td>'
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input class="rndCapexMiscActivityId" type="hidden" value="' + activityTypeId + '" /><input type="hidden" class="rndCapexMiscStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" />' + _currencySymbol + '<input type="number" class="form-control CapexMiscStrengthValue" min="0" value="' + (data.length > 0 ? getValueFromStrengthByMiscellaneousDevelopment(data, _strengthArray[i].pidfProductStrengthId, "strengthMiscellaneousExpense", data[a].miscellaneousDevelopment) : "") + '" /></td>';
        }
        objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /></td><td> <i class='fa-solid fa-circle-plus nav-icon text-success operationButton' id='addIcon' onclick='addRowCapexMisc(this);'></i> <i class='fa-solid fa-trash nav-icon text-red strengthDeleteIcon operationButton DeleteIcon' onclick='deleteRowCapexMisc(this);' ></i></td></tr>";
        if (data.length > 0) {
            _activityType.push(data[a].miscellaneousDevelopment);
        }
    }

    return objectname;
}
function BindRNDCapexMiscExpenses(data) {
    var capexmiscexpensesHTML = '<thead class="bg-primary text-bold"><tr>'
        + '<td> Capex & Misc </td>';
    $.each(_strengthArray, function (index, item) {
        capexmiscexpensesHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + ' (Cost)</td>';
    });
    capexmiscexpensesHTML += '<td>Total</td><td>Action</td></tr></thead><tbody id="tblCapexMiscBody">';
    for (var i = 1; i < 2; i++) {
        capexmiscexpensesHTML += CreateCapexMiscTable($.grep(data, function (n, x) { return n.activityTypeId == i; }), i);
    }

    capexmiscexpensesHTML += '<tr class="CapexMiscActivity1Total"><td class="text-bold">Total Cost</td>';

    $.each(_strengthArray, function (index, item) {
        capexmiscexpensesHTML += "<td data-strengthid='" + _strengthArray[index].pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthMisc' readonly='readonly' tabindex=-1  min='0' /></td>";
    });

    capexmiscexpensesHTML += "<td>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForMisc1' readonly='readonly' tabindex=-1/><td></td></tr></tbody>";

    $('#tablerndcapexmiscellaneousexpenses').html(capexmiscexpensesHTML);

    SetChildRowDeleteIconPBF();

    $("input[class~='CapexMiscStrengthValue']").trigger('change');
}
function addRowCapexMisc(element) {
    var node = $(element).parent().parent().clone(true);
    node.find("input.form-control").val("");
    $(element).parent().parent().after(node);
    SetChildRowDeleteIconPBF();
}
function deleteRowCapexMisc(element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIconPBF();
    $("input[class~='CapexMiscStrengthValue']").trigger('change');
}
//Capex and Misc Exp table end
//PlantSupportCost table start
function CreatePlantSupportCostTable(data, activityTypeId) {
    var objectname = "";

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (_strengthArray.length + 2) + '">Plant Support</td>';

    var _iterator = (activityTypeId - 1) * _strengthArray.length;

    //objectname += "<tr class='PlantSupportCost_" + activityTypeId + "' data-activitytypeid='" + activityTypeId + "'><td>Prototype</td>";
    //for (var i = 0; i < _strengthArray.length; i++) {
    //    objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="number" id="RNDPlantSupportCosts[' + [(i + _iterator)] + '].StrengthId" name="RNDPlantSupportCosts[' + [(i + _iterator)] + '].StrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control calcRNDPlantSupportCostsPrototype" id="RNDPlantSupportCosts[' + [(i + _iterator)] + '].Prototype" name="RNDPlantSupportCosts[' + [(i + _iterator)] + '].Prototype" placeholder="Prototype" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "prototype")) + '" /><span>Kg</span></td>';
    //}
    //objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /><span>Kg</span></td></tr>";

    objectname += "<tr class='PlantSupportCost_" + activityTypeId + "' data-activitytypeid='" + activityTypeId + "'><td>Scale Up </td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="hidden" id="RNDPlantSupportCosts[' + [(i + _iterator)] + '].StrengthId" name="RNDPlantSupportCosts[' + [(i + _iterator)] + '].StrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control calcRNDPlantSupportCostsScaleUp" id="RNDPlantSupportCosts[' + [(i + _iterator)] + '].ScaleUp" name="RNDPlantSupportCosts[' + [(i + _iterator)] + '].ScaleUp" placeholder="Scale Up" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "scaleUp")) + '"  /><span>days</span></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength TotalPSCScaleUp' readonly='readonly' tabindex=-1 /><span>days</span></td></tr>";

    objectname += "<tr class='PlantSupportCost_" + activityTypeId + "' data-activitytypeid='" + activityTypeId + "'><td>Exhibit</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> <input type="number" class="form-control calcRNDPlantSupportCostsExhibit" id="RNDPlantSupportCosts[' + [(i + _iterator)] + '].Exhibit" name="RNDPlantSupportCosts[' + [(i + _iterator)] + '].Exhibit" placeholder="Exhibit" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibit")) + '" /><span>days</span></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength TotalPSCExhibit' readonly='readonly' tabindex=-1 /><span>days</span></td></tr>";

    objectname += "<tr class='PlantSupportCost_" + activityTypeId + "Total' data-activitytypeid='" + activityTypeId + "'><td class='text-bold'>Total Cost</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrength' readonly='readonly' tabindex=-1 /></td>";
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthTotal' readonly='readonly' tabindex=-1 /></td></tr>";


    return objectname;
}
function BindRNDPlantSupportCost(data) {
    var PlantSupportCostHTML = '<thead class="bg-primary text-bold"><tr><td>Plan Support</td>';
    $.each(_strengthArray, function (index, item) {
        PlantSupportCostHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    PlantSupportCostHTML += '<td>Total</td></tr></thead><tbody>';

    PlantSupportCostHTML += CreatePlantSupportCostTable(data, 1);

    PlantSupportCostHTML += "</tbody>";
    $('#tablerndplantsupportcost').html(PlantSupportCostHTML);

    $("input[class~='calcRNDPlantSupportCostsScaleUp']").trigger('change');
    $("input[class~='calcRNDPlantSupportCostsExhibit']").trigger('change');
}
//Reference Product Detail table start
function CreateReferenceProductDetailTable(data, bioStudyTypeId) {
    var objectname = "";

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (_strengthArray.length + 2) + '">Reference Product Detail</td>';

    var _iterator = (bioStudyTypeId - 1) * _strengthArray.length;

    objectname += "<tr class=' RPDcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "' data-rowid='0'><td>Unit Cost Of Reference Product</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="hidden" id="RNDReferenceProductDetails[' + [(i + _iterator)] + '].StrengthId" name="RNDReferenceProductDetails[' + [(i + _iterator)] + '].StrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control calcRNDRPDUnitCostOfReferenceProduct RPDStrengthValue" id="RNDReferenceProductDetails[' + [(i + _iterator)] + '].UnitCostOfReferenceProduct" name="RNDReferenceProductDetails[' + [(i + _iterator)] + '].UnitCostOfReferenceProduct" placeholder="UnitCostOfReferenceProduct" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "unitCostOfReferenceProduct")) + '" /></td>';
    }
    objectname += "<td></td></tr>";

    objectname += "<tr class='RPDcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "' data-rowid='1'><td>Formulation Development</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="number" class="form-control calcRNDRPDFormulationDevelopment RPDStrengthValue" id="RNDReferenceProductDetails[' + [(i + _iterator)] + '].FormulationDevelopment" name="RNDReferenceProductDetails[' + [(i + _iterator)] + '].FormulationDevelopment" placeholder="Formulation Development" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "formulationDevelopment")) + '"  /></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength TotalFormulation' readonly='readonly' tabindex=-1 /></td></tr>";

    objectname += "<tr class='RPDcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "' data-rowid='2'><td>Pilot Be</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> <input type="number" class="form-control calcRNDRPDPilotBe RPDStrengthValue" id="RNDReferenceProductDetails[' + [(i + _iterator)] + '].PilotBe" name="RNDReferenceProductDetails[' + [(i + _iterator)] + '].PilotBe" placeholder="Pilot Be" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "pilotBE")) + '" /></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength TotalPilotBe' readonly='readonly' tabindex=-1 /></td></tr>";

    objectname += "<tr class='RPDcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "' data-rowid='3'><td>Pharmaceutical Equivalence</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> <input type="number" class="form-control calcRNDRPDPharmasuiticalEquivalence RPDStrengthValue" id="RNDReferenceProductDetails[' + [(i + _iterator)] + '].PharmasuiticalEquivalence" name="RNDReferenceProductDetails[' + [(i + _iterator)] + '].PharmasuiticalEquivalence" placeholder="Pharmasuitical Equivalence" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "pharmasuiticalEquivalence")) + '" /></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength TotalPharmasuiticalEquivalence' readonly='readonly' tabindex=-1 /></td></tr>";

    objectname += "<tr class='RPDcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "' data-rowid='4'><td>Pivotal Bio</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"> <input type="number" class="form-control calcRNDRPDPivotalBio RPDStrengthValue" id="RNDReferenceProductDetails[' + [(i + _iterator)] + '].PivotalBio" name="RNDReferenceProductDetails[' + [(i + _iterator)] + '].PivotalBio" placeholder="Pivotal Bio" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "pivotalBio")) + '"/></td>';
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength TotalPivotalBio' readonly='readonly' tabindex=-1 /></td></tr>";

    objectname += "<tr class='RPDcal_" + bioStudyTypeId + "Total' data-biostudytypeid='" + bioStudyTypeId + "'><td class='text-bold'>Total Cost</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthRPD' readonly='readonly' tabindex=-1 /></td>";
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthTotalRPD' readonly='readonly' tabindex=-1  min='0' /></td></tr>";

    return objectname;
}
function BindRNDReferenceProductDetail(data) {
    var RPDHTML = '<thead class="bg-primary text-bold"><tr><td>Reference Product Detail</td>';
    $.each(_strengthArray, function (index, item) {
        RPDHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    RPDHTML += '<td>Total</td></tr></thead><tbody>';

    RPDHTML += CreateReferenceProductDetailTable(data, 1);

    RPDHTML += '</tbody>';
    $('#tablerndreferenceproductdetail').html(RPDHTML);

    $("input[class~='calcRNDRPDUnitCostOfReferenceProduct']").trigger('change');
    //$("input[class~='calcRNDRPDFormulationDevelopment']").trigger('change');
    //$("input[class~='calcRNDRPDPilotBe']").trigger('change');
    //$("input[class~='calcRNDRPDPharmasuiticalEquivalence']").trigger('change');
    //$("input[class~='calcRNDRPDPivotalBio']").trigger('change');
}
//RPD table end
// Filling Expenses table Start
function CreateFillingExpensesTable(data, activityTypeId) {
    var objectname = "";
    var tableTitle = "Filling Expenses";

    //var _iterator = (activityTypeId - 1) * _strengthArray.length;

    var _counter = (data.length == 0 ? 1 : data.length);
    var _activityType = [];

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (6 + _strengthArray.length) + '">' + tableTitle + '</td>';
    for (var a = 0; a < _counter; a++) {
        var BUID = data.length > 0 ? data[a].businessUnitId : 0;
        if (data.length > 0) {
            if (_activityType.indexOf(data[a].businessUnitId) !== -1) {
                continue;
            }
        }

        objectname += '<tr data-activitytypeid="' + activityTypeId + '" id="FillingExpensesRow" class="FillingExpensesactivity FillingExpensesActivity' + (activityTypeId) + '">'
            + '<td><input type="number" class="form-control totalFillingExpenses rndFillingExpensesTotalCost" min="0" value="' + (data.length > 0 ? data[a].totalCost : "") + '"/></td>'
            + '<td><select class="form-control readOnlyUpdate rndFillingExpensesRegionId"><option value = "" > --Select --</option ></select><input type="hidden" value="' + (data.length > 0 ? data[a].businessUnitId : "") + '"/></td>'
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '">' + _currencySymbol + '<input type="hidden" class="rndFillingExpensesStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="checkbox" id="rndFillingExpensesStrengthIsChecked' + _strengthArray[i].pidfProductStrengthId + '" class="rndFillingExpensesStrengthCheckbox rndFillingExpensesStrengthIsChecked' + _strengthArray[i].pidfProductStrengthId + '" ' + (data.length > 0 ? getValueFromStrengthBusinessUnitId(data, _strengthArray[i].pidfProductStrengthId, "isChecked", data[a].businessUnitId) : "") + '  > &nbsp; <input type="text" class="form-control FillingExpensesStrengthValue inline-textbox" readonly="readonly" tabindex=-1 disabled="true" min="0" /></td>';
        }
        objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control rndTotalFillingExpenseStrength' readonly='readonly' tabindex=-1 /></td><td> <i class='fa-solid fa-circle-plus nav-icon text-success operationButton' id='addIcon' onclick='addRowFillingExpenses(this);'></i> <i class='fa-solid fa-trash nav-icon text-red strengthDeleteIcon operationButton DeleteIcon' onclick='deleteRowFillingExpenses(this);' ></i></td></tr>";
        if (data.length > 0) {
            _activityType.push(data[a].businessUnitId);
        }
    }

    objectname += "<tr class='FillingExpensesActivity" + activityTypeId + "Total' data-activitytypeid='" + activityTypeId + "'><td colspan='2' class='text-bold'>Total Cost</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthFilling' readonly='readonly' tabindex=-1 /></td>";
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthTotalFilling' readonly='readonly' tabindex=-1  min='0' /></td></tr>";


    return objectname;
}
function BindRNDFillingExpenses(data, businessUnit) {
    var FillingExpensesexpensesHTML = '<thead class="bg-primary text-bold"><tr>'
        + '<td>Total Cost</td>'
        + '<td>Business Unit </td>';
    $.each(_strengthArray, function (index, item) {
        FillingExpensesexpensesHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + ' (Cost)</td>';
    });
    FillingExpensesexpensesHTML += '<td>Total</td><td>Action</td></tr></thead><tbody id="tblFillingExpensesBody">';
    for (var i = 1; i < 2; i++) {
        FillingExpensesexpensesHTML += CreateFillingExpensesTable(data, 1);
    }

    //FillingExpensesexpensesHTML += '<tr><td class="text-bold">Total Cost</td>';

    //$.each(_strengthArray, function (index, item) {
    //    FillingExpensesexpensesHTML += "<td><input type='number' class='form-control' readonly='readonly' tabindex=-1 /></td>";
    //});

    //FillingExpensesexpensesHTML += "<td><input type='number' class='form-control' readonly='readonly' tabindex=-1 /><td></td></tr></tbody>";
    $('#tablerndfilingexpenses').html(FillingExpensesexpensesHTML);
    SetChildRowDeleteIconPBF();

    $(businessUnit).each(function (index, item) {
        $('.rndFillingExpensesRegionId').append('<option value="' + item.businessUnitId + '">' + item.businessUnitName + '</option>');
    });

    $.each($('.rndFillingExpensesRegionId'), function (index, item) {
        if ($(this).next().val() != undefined && $(this).next().val() != null) {
            $(this).val($(this).next().val());
        }
    });

    $("select[class~='rndFillingExpensesRegionId']").trigger('change');
    /*    $("input[class~='rndFillingExpensesStrengthCheckbox']").trigger('change');*/
}
function addRowFillingExpenses(element) {
    var node = $(element).parent().parent().clone(true);
    node.find("input.form-control").val("");
    node.find("input.rndFillingExpensesStrengthCheckbox").prop("checked", false);
    $(element).parent().parent().after(node);
    SetChildRowDeleteIconPBF();
}
function deleteRowFillingExpenses(element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIconPBF();
    $("select[class~='rndFillingExpensesRegionId']").trigger('change');
}
// Filling Expenses table End

//MPC table start
function CreateRNDManPowerCostTable(data) {
    var bioStudyTypeId = 1;
    var objectname = "";

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (3 + _strengthArray.length + 1) + '"></td></tr>';
    var _counter = (data.length == 0 ? 1 : data.length);
    var _projectActivities = [];
    for (var a = 0; a < _counter; a++) {
        if (data.length > 0) {
            if (_projectActivities.indexOf(data[a].projectActivitiesId) !== -1) {
                continue;
            }
        }
        objectname += '<tr class="MPCActivity  manpowercost_' + bioStudyTypeId + '" data-biostudytypeid="' + bioStudyTypeId + '" data-activityid="' + data[a].projectActivitiesId + '"><td  data-activityid="' + data[a].projectActivitiesId + '"><input type="number" class="form-control rndMPCDurationInDays rndMPCDurationInDays' + data[a].projectActivitiesId + '" min="0" value="' + (data.length > 0 ? data[a].durationInDays : "") + '"   /></td><td><input type="hidden" class="rndMPCProjectActivitiesId" value="' + data[a].projectActivitiesId + '" />' + data[a].projectActivitiesName + '</td><td  data-activityid="' + data[a].projectActivitiesId + '"><input type="number" class="form-control rndMPCManPowerInDays rndMPCManPowerInDays' + data[a].projectActivitiesId + '" min="0" value="' + (data.length > 0 ? data[a].manPowerInDays : "") + '" /></td>';
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="hidden" class="rndMPCStrengthId"  value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="text" class="form-control rndMPCStrengthValue" readonly="readonly" tabindex=-1/><span>manhrs</span></td>';
        }
        objectname += "<td><input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /><span>manhrs</span></td></tr>";
        if (data.length > 0) {
            _projectActivities.push(data[a].projectActivitiesId);
        }
    }

    objectname += "<tr class='manpowercost_" + bioStudyTypeId + "Total' data-biostudytypeid='" + bioStudyTypeId + "'><td>Cost</td><td colspan='2'><b>Prototype</b></td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'><input type='text' class='form-control calcTotalPrototypeCostForStrength' readonly='readonly' tabindex=-1 /></td>";
    }
    objectname += "<td><input type='text' class='form-control calcTotalPrototypeCostForStrengthTotal' readonly='readonly' tabindex=-1  /></td></tr>";

    objectname += "<tr class='manpowercost_" + bioStudyTypeId + "Total' data-biostudytypeid='" + bioStudyTypeId + "'><td>Cost</td><td colspan='2'><b>Scaleup</b></td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'><input type='text' class='form-control calcTotalScaleupCostForStrength' readonly='readonly' tabindex=-1 /></td>";
    }
    objectname += "<td><input type='text' class='form-control calcTotalScaleupCostForStrengthTotal' readonly='readonly' tabindex=-1   /></td></tr>";

    objectname += "<tr class='manpowercost_" + bioStudyTypeId + "Total' data-biostudytypeid='" + bioStudyTypeId + "'><td>Cost</td><td colspan='2'><b>Exhibit</b></td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'><input type='text' class='form-control calcTotalExhibitCostForStrength' readonly='readonly' tabindex=-1 /></td>";
    }
    objectname += "<td><input type='text' class='form-control calcTotalExhibitCostForStrengthTotal' readonly='readonly' tabindex=-1  /></td></tr>";

    objectname += "<tr class='manpowercost_" + bioStudyTypeId + "Total' data-biostudytypeid='" + bioStudyTypeId + "'><td colspan='3'><b>Total Cost</b></td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrength' readonly='readonly' tabindex=-1 /></td>";
    }
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthTotal' readonly='readonly' tabindex=-1  /></td></tr>";

    return objectname;
}
function BindRNDManPowerCost(data) {
    var RPDHTML = '<thead class="bg-primary text-bold"><tr><td>Number Of Days </td><td>Project Activities </td><td>Number Of Manpower </td>';
    $.each(_strengthArray, function (index, item) {
        RPDHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    RPDHTML += '<td>Total</td></tr></thead><tbody>';

    RPDHTML += CreateRNDManPowerCostTable(data);

    RPDHTML += "</tbody>";
    $('#tablerrndmanpowercostprojectduration').html(RPDHTML);
    $("input[class~='rndMPCDurationInDays']").trigger('change');
    //$("input[class~='rndMPCManPowerInDays']").trigger('change');
}
//MPC table end
//Phase Wise Budget table Start
function CreatePhaseWiseBudgetTable() {
    var bioStudyTypeId = 1;
    var PWBHTML = '<thead class="bg-primary text-bold"><tr><td>Activities</td>'
    $.each(_strengthArray, function (index, item) {
        PWBHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + ' (Cost)</td>';
    });
    PWBHTML += '<td>Total</td><td>CUM. Total</td><td>% Of Total</td></tr></thead><tbody>';

    PWBHTML += "<tr class='phasewisebudget_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Feasability</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        PWBHTML += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="hidden" value="' + _strengthArray[i].pidfProductStrengthId + '" />' + _currencySymbol + '<input type="text" class="form-control calcPWBRNDFeasability"  readonly="readonly" tabindex=-1 /></td>';
    }
    PWBHTML += "<td>" + _currencySymbol + "<input type='text' class='form-control totalPWB' readonly='readonly' tabindex=-1 /></td><td>" + _currencySymbol + "<input type='text' class='form-control CumtotalPWBFeasability CumTotalPWB' readonly='readonly' tabindex=-1 /></td><td>" + _currencySymbol + "<input type='text' class='form-control PercenttotalPWB' readonly='readonly' tabindex=-1 /></td></tr>";

    PWBHTML += "<tr class='phasewisebudget_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Prototype development</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        PWBHTML += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="hidden" value="' + _strengthArray[i].pidfProductStrengthId + '" />' + _currencySymbol + '<input type="text" class="form-control calcRNDPWBPrototypedevelopment" readonly="readonly" tabindex=-1 /></td>';
    }
    PWBHTML += "<td>" + _currencySymbol + "<input type='text' class='form-control totalPWB' readonly='readonly' tabindex=-1 /></td><td>" + _currencySymbol + "<input type='text' class='form-control CumtotalPWBPrototype CumTotalPWB' readonly='readonly' tabindex=-1 /></td><td>" + _currencySymbol + "<input type='text' class='form-control PercenttotalPWB' readonly='readonly' tabindex=-1 /></td></tr>";

    PWBHTML += "<tr class='phasewisebudget_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>R&D Scale Up</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        PWBHTML += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="hidden" value="' + _strengthArray[i].pidfProductStrengthId + '" />' + _currencySymbol + '<input type="text" class="form-control calcRNDPWBScaleUp"  readonly="readonly" tabindex=-1 /></td>';
    }
    PWBHTML += "<td>" + _currencySymbol + "<input type='text' class='form-control totalPWB' readonly='readonly' tabindex=-1 /></td><td>" + _currencySymbol + "<input type='text' class='form-control CumtotalPWBScaleUp CumTotalPWB' readonly='readonly' tabindex=-1 /></td><td>" + _currencySymbol + "<input type='text' class='form-control PercenttotalPWB' readonly='readonly' tabindex=-1 /></td></tr>";

    PWBHTML += "<tr class='phasewisebudget_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>AMV / AMT</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        PWBHTML += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="hidden" value="' + _strengthArray[i].pidfProductStrengthId + '" />' + _currencySymbol + '<input type="text" class="form-control calcRNDPWBAMV"  readonly="readonly" tabindex=-1 /></td>';
    }
    PWBHTML += "<td>" + _currencySymbol + "<input type='text' class='form-control totalPWB' readonly='readonly' tabindex=-1 /></td><td>" + _currencySymbol + "<input type='text' class='form-control CumtotalPWBPWBAMV CumTotalPWB' readonly='readonly' tabindex=-1 /></td><td>" + _currencySymbol + "<input type='text' class='form-control PercenttotalPWB' readonly='readonly' tabindex=-1 /></td></tr>";

    PWBHTML += "<tr class='phasewisebudget_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Exhibit and Scalability</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        PWBHTML += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="hidden" value="' + _strengthArray[i].pidfProductStrengthId + '" />' + _currencySymbol + '<input type="text" class="form-control calcRNDPWBExhibitScalability" readonly="readonly" tabindex=-1 /></td>';
    }
    PWBHTML += "<td>" + _currencySymbol + "<input type='text' class='form-control totalPWB' readonly='readonly' tabindex=-1 /></td><td>" + _currencySymbol + "<input type='text' class='form-control CumtotalPWBExhibitScalability CumTotalPWB' readonly='readonly' tabindex=-1 /></td><td>" + _currencySymbol + "<input type='text' class='form-control PercenttotalPWB' readonly='readonly' tabindex=-1 /></td></tr>";

    PWBHTML += "<tr class='phasewisebudget_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Filing</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        PWBHTML += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="hidden" value="' + _strengthArray[i].pidfProductStrengthId + '" />' + _currencySymbol + '<input type="text" class="form-control calcRNDPWBFiling" readonly="readonly" tabindex=-1 /></td>';
    }
    PWBHTML += "<td>" + _currencySymbol + "<input type='text' class='form-control totalPWB' readonly='readonly' tabindex=-1 /></td><td>" + _currencySymbol + "<input type='text' class='form-control CumtotalPWBFiling CumTotalPWB' readonly='readonly' tabindex=-1 /></td><td>" + _currencySymbol + "<input type='text' class='form-control PercenttotalPWB' readonly='readonly' tabindex=-1 /></td></tr>";

    PWBHTML += "<tr class='phasewisebudget_" + bioStudyTypeId + "Total' data-biostudytypeid='" + bioStudyTypeId + "'><td class='text-bold'>Total Cost</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        PWBHTML += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrength' readonly='readonly' tabindex=-1 /></td>";
    }
    PWBHTML += "<td>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForStrengthTotal' readonly='readonly' tabindex=-1 /></td></tr>";

    PWBHTML += "</tbody>";
    $('#tablerndphasewisebudget').html(PWBHTML);
}
//Phase Wise Budget table end

//Total Expenses table Start
function CreateTotalExpensesTable() {
    var bioStudyTypeId = 1;
    var PWBHTML = '<thead class="bg-primary text-bold"><tr><td></td>'
    $.each(_strengthArray, function (index, item) {
        PWBHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + ' (Cost)</td>';
    });
    PWBHTML += '<td>Total</td></tr></thead><tbody>';

    PWBHTML += "<tr class='totalexpenses_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId + "'><td>Total Expenses</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        PWBHTML += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="hidden" value="' + _strengthArray[i].pidfProductStrengthId + '" />' + _currencySymbol + '<input type="text" class="form-control calcTE" readonly="readonly" tabindex=-1 /><span>Lacs</span></td>';
    }
    PWBHTML += "<td>" + _currencySymbol + "<input type='text' class='form-control totalTE' readonly='readonly' tabindex=-1 /><span>Lacs</span></td></tr>";


    PWBHTML += "</tbody>";
    $('#tablerndtotalexpenses').html(PWBHTML);
}
//Phase Wise Budget table end

//HWB table start
function CreateHeadWiseBudgetTable(data) {
    var bioStudyTypeId = 1;
    var objectname = "";

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="5">Budget Heads</td></tr>';
    var _counter = (data.length == 0 ? 1 : data.length);
    var _projectActivities = [];
    for (var a = 0; a < _counter; a++) {
        if (data.length > 0) {
            if (_projectActivities.indexOf(data[a].projectActivitiesId) !== -1) {
                continue;
            }
        }
        objectname += '<tr class="HWBActivity headwisebudget_' + bioStudyTypeId + '" data-biostudytypeid="' + bioStudyTypeId + '" data-activityid="' + data[a].projectActivitiesId + '"><td><input type="hidden" class="rndHWBProjectActivitiesId" value="' + data[a].projectActivitiesId + '" />' + data[a].projectActivitiesName + '</td><td  data-activityid="' + data[a].projectActivitiesId + '">' + _currencySymbol + '<input type="text" class="form-control rndHWBPrototype rndHWBPrototype' + data[a].projectActivitiesId + '" readonly="readonly" value="' + (data.length > 0 ? data[a].prototype : "") + '"   /></td><td  data-activityid="' + data[a].projectActivitiesId + '">' + _currencySymbol + '<input type="test" class="form-control rndHWBScaleUp rndHWBScaleUp' + data[a].projectActivitiesId + '" readonly="readonly" value="' + (data.length > 0 ? data[a].scaleUp : "") + '"   /></td><td  data-activityid="' + data[a].projectActivitiesId + '">' + _currencySymbol + '<input type="test" class="form-control rndHWBExhibit rndHWBExhibit' + data[a].projectActivitiesId + '" readonly="readonly" value="' + (data.length > 0 ? data[a].exhibit : "") + '" /></td>';
        //for (var i = 0; i < _strengthArray.length; i++) {
        //    objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId + '"><input type="hidden" class="rndHWBStrengthId"  value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="text" class="form-control rndHWBStrengthValue" readonly="readonly" tabindex=-1/><span>manhrs</span></td>';
        //}
        objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control TotalStrength' readonly='readonly' tabindex=-1 /></td></tr>";
        if (data.length > 0) {
            _projectActivities.push(data[a].projectActivitiesId);
        }
    }
    objectname += "<tr class='headwisebudget_" + bioStudyTypeId + "Total' data-biostudytypeid='" + bioStudyTypeId + "'><td><b>Total Cost</b></td>";
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForPrototype' readonly='readonly' tabindex=-1 /></td> <td>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForScaleUp' readonly='readonly' tabindex=-1 /></td><td>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostForExhibit' readonly='readonly' tabindex=-1 /></td>";
    objectname += "<td>" + _currencySymbol + "<input type='text' class='form-control calcTotalCostHeadWiseBudget' readonly='readonly' tabindex=-1   /></td></tr>";

    return objectname;
}
function BindHeadWiseBudget(data) {
    var HWBHTML = '<thead class="bg-primary text-bold"><tr><td>Budget Heads</td><td>Prototype </td><td>ScaleUp </td><td>Exhibit </td>';
    //$.each(_strengthArray, function (index, item) {
    //    HWBHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    //});
    HWBHTML += '<td>Total</td></tr></thead><tbody>';

    HWBHTML += CreateHeadWiseBudgetTable(data);

    HWBHTML += "</tbody>";
    $('#tableHeadWiseBudget').html(HWBHTML);
    //$("input[class~='rndHWBDurationInDays']").trigger('change');
    //$("input[class~='rndHWBManPowerInDays']").trigger('change');
}
//HWB table end


function SetRNDChildRows() {
    var _RNDexicipientArray = [];
    var _RNDpackagingArray = [];
    var _RNDToolingChangePartArray = [];
    var _RNDCapexMiscArray = [];
    var _RNDPlantSupportCostArray = [];
    var _RNDFillingExpensesArray = [];
    var _RNDMenPowerCostArray = [];
    var _RNDHeadWiseBudgetArray = [];
    var _RNDPhaseWiseBudgetArray = [];

    for (var i = 1; i < 4; i++) {
        $.each($('#tablerndexicipientrequirement tbody tr.exicipientActivity' + i + ''), function () {
            var ExicipientPrototype = $(this).find(".rndExicipientPrototype").val();
            var RsPerKg = $(this).find(".rndExicipientRsperkg").val();
            var Quantity = $(this).find(".rndExicipientQuantity").val();
            var ActivityTypeId = $(this).find(".rndExicipientTypeId").val();

            $.each($(this).find(".rndExicipientStrengthId"), function (index, item) {
                var _rndExicipientObject = new Object();
                _rndExicipientObject.StrengthId = $(this).val();
                _rndExicipientObject.ExicipientDevelopment = $(this).parent().find(".rndExicipientStrengthValue").val();
                _rndExicipientObject.ExicipientPrototype = ExicipientPrototype;
                _rndExicipientObject.ActivityTypeId = ActivityTypeId;
                _rndExicipientObject.RsPerKg = RsPerKg;
                _rndExicipientObject.MgPerUnitDosage = Quantity;
                if (_rndExicipientObject.ExicipientDevelopment != "") {
                    _RNDexicipientArray.push(_rndExicipientObject);
                }
            });
        });

        $('#hdnrndExicipientData').val(JSON.stringify(_RNDexicipientArray));

        $.each($('#tablerndpackagingmaterialrequirement tbody tr.packagingActivity' + i + ''), function () {
            var PackingTypeId = $(this).find(".rndPackingTypeId").val();
            var ActivityTypeId = $(this).find(".rndPackagingActivityId").val();
            var UnitOfMeasurement = $(this).find(".rndPackagingUnitofMeasurement").val();
            /*  var PrototypeDevelopment = $(this).find(".rndPackagingPrototype").val();*/
            var RsPerUnit = $(this).find(".rndPackagingRsperUnit").val();
            var Quantity = 0;

            $.each($(this).find(".rndPackagingStrengthId"), function (index, item) {
                var _rndPackagingObject = new Object();
                _rndPackagingObject.StrengthId = $(this).val();
                _rndPackagingObject.PackagingDevelopment = $(this).parent().find(".rndPackagingStrengthValue").val();
                _rndPackagingObject.PackingTypeId = PackingTypeId;
                _rndPackagingObject.ActivityTypeId = ActivityTypeId;
                /* _rndPackagingObject.PrototypeDevelopment = PrototypeDevelopment;*/
                _rndPackagingObject.RsPerUnit = RsPerUnit;
                _rndPackagingObject.UnitOfMeasurement = UnitOfMeasurement;
                _rndPackagingObject.Quantity = Quantity;

                if (_rndPackagingObject.PackagingDevelopment != "" && _rndPackagingObject.PackingTypeId != "0") {
                    _RNDpackagingArray.push(_rndPackagingObject);
                }
            });
        });
        $('#hdnrndPackagingData').val(JSON.stringify(_RNDpackagingArray));

        $.each($('#tablerndtoolingchangepart tbody tr.ToolingChangePartActivity' + i + ''), function () {
            var Prototype = $(this).find(".rndToolingChangePartPrototype").val();
            var Cost = $(this).find(".rndToolingChangePartCost").val();
            var ActivityTypeId = $(this).find(".rndToolingChangePartActivityId").val();
            $.each($(this).find(".rndToolingChangePartStrengthId"), function (index, item) {
                var _rndToolingChangePartObject = new Object();
                _rndToolingChangePartObject.StrengthId = $(this).val();
                _rndToolingChangePartObject.StrengthUnitQuantity = $(this).parent().find(".ToolingChangePartStrengthValue").val();
                _rndToolingChangePartObject.Prototype = Prototype;
                _rndToolingChangePartObject.Cost = Cost;
                _rndToolingChangePartObject.ActivityTypeId = ActivityTypeId;

                if (_rndToolingChangePartObject.StrengthUnitQuantity != "") {
                    _RNDToolingChangePartArray.push(_rndToolingChangePartObject);
                }
            });
        });

        $('#hdnrndToolingChangePartData').val(JSON.stringify(_RNDToolingChangePartArray));

        if (i == 1) {
            var misccount = i
            $.each($('#tablerndcapexmiscellaneousexpenses tbody tr.CapexMiscActivity' + i + ''), function () {
                var MiscellaneousDevelopment = $(this).find(".rndCapexMiscMiscellaneous").val();
                var ActivityTypeId = $(this).find(".rndCapexMiscActivityId").val();
                $.each($(this).find(".rndCapexMiscStrengthId"), function (index, item) {
                    var _rndCapexMiscObject = new Object();
                    _rndCapexMiscObject.StrengthId = $(this).val();
                    _rndCapexMiscObject.StrengthMiscellaneousExpense = $(this).parent().find(".CapexMiscStrengthValue").val();
                    _rndCapexMiscObject.MiscellaneousDevelopment = MiscellaneousDevelopment;
                    _rndCapexMiscObject.ActivityTypeId = ActivityTypeId;
                    if (_rndCapexMiscObject.StrengthMiscellaneousExpense != "") {
                        _RNDCapexMiscArray.push(_rndCapexMiscObject);
                    }
                });
                misccount++;
            });

            $('#hdnrndCapexMiscellaneousExpensesData').val(JSON.stringify(_RNDCapexMiscArray));

            //var plantsupportcount = i
            //$.each($('#tablerndplantsupportcost tbody tr.PlantSupportCostActivity' + i + ''), function () {
            //    var PlantSupportDevelopment = $(this).find(".rndPlantSupportDevelopment").val();
            //    var ActivityTypeId = $(this).find(".rndPlantSupportActivityId").val();
            //    $.each($(this).find(".rndPlantSupportCostStrengthId"), function (index, item) {
            //        var _rndPlantSupportObject = new Object();
            //        _rndPlantSupportObject.StrengthId = $(this).val();
            //        _rndPlantSupportObject.StrengthPlantSupportDays = $(this).parent().find(".PlantSupportCostStrengthValue").val();
            //        _rndPlantSupportObject.PlantSupportDevelopment = PlantSupportDevelopment;
            //        _rndPlantSupportObject.ActivityTypeId = ActivityTypeId;
            //        if (_rndPlantSupportObject.StrengthPlantSupportDays != "") {
            //            _RNDPlantSupportCostArray.push(_rndPlantSupportObject);
            //        }
            //    });
            //    plantsupportcount++;
            //});

            //$('#hdnrndPlantSupportCostData').val(JSON.stringify(_RNDPlantSupportCostArray));

            var fillingcount = i
            $.each($('#tablerndfilingexpenses tbody tr.FillingExpensesActivity' + i + ''), function () {
                var BusinessUnitId = $(this).find(".rndFillingExpensesRegionId").val();
                var TotalCost = $(this).find(".rndFillingExpensesTotalCost").val();

                $.each($(this).find(".rndFillingExpensesStrengthId"), function (index, item) {
                    var _rndFillingExpensesObject = new Object();
                    _rndFillingExpensesObject.StrengthId = $(this).val();
                    _rndFillingExpensesObject.ExpensesStrengthValue = $(this).parent().find(".FillingExpensesStrengthValue").val();
                    _rndFillingExpensesObject.IsChecked = $(this).parent().find('#rndFillingExpensesStrengthIsChecked' + $(this).val()).is(":checked");
                    _rndFillingExpensesObject.BusinessUnitId = BusinessUnitId;
                    _rndFillingExpensesObject.TotalCost = TotalCost;
                    if (_rndFillingExpensesObject.BusinessUnitId > 0) {
                        _RNDFillingExpensesArray.push(_rndFillingExpensesObject);
                    }
                    //if (_rndFillingExpensesObject.IsChecked == true) {
                    //    _RNDFillingExpensesArray.push(_rndFillingExpensesObject);
                    //}
                });
                fillingcount++;
            });

            $('#hdnrndFillingExpensesData').val(JSON.stringify(_RNDFillingExpensesArray));

            $.each($('#tablerrndmanpowercostprojectduration tbody tr.MPCActivity'), function () {
                var _rndMPCObject = new Object();
                var DurationInDays = $(this).find(".rndMPCDurationInDays").val();
                var ProjectActivitiesId = $(this).find(".rndMPCProjectActivitiesId").val();
                var ManPowerInDays = $(this).find(".rndMPCManPowerInDays").val();

                _rndMPCObject.DurationInDays = DurationInDays;
                _rndMPCObject.ProjectActivitiesId = ProjectActivitiesId;
                _rndMPCObject.ManPowerInDays = ManPowerInDays;
                _RNDMenPowerCostArray.push(_rndMPCObject);

            });

            $('#hdnrndManPowerCostProjectDuration').val(JSON.stringify(_RNDMenPowerCostArray));

            $.each($('#tableHeadWiseBudget tbody tr.HWBActivity'), function () {
                var _rndHWBObject = new Object();
                var Prototype = $(this).find(".rndHWBPrototype").val();
                var ProjectActivitiesId = $(this).find(".rndHWBProjectActivitiesId").val();
                var ScaleUp = $(this).find(".rndHWBScaleUp").val();
                var Exhibit = $(this).find(".rndHWBExhibit").val();

                _rndHWBObject.Prototype = ConvertToNumber(Prototype == "" ? 0 : Prototype);
                _rndHWBObject.ProjectActivitiesId = ProjectActivitiesId;
                _rndHWBObject.ScaleUp = ConvertToNumber(ScaleUp == "" ? 0 : ScaleUp);
                _rndHWBObject.Exhibit = ConvertToNumber(Exhibit == "" ? 0 : Exhibit);
                _RNDHeadWiseBudgetArray.push(_rndHWBObject);
            });

            $('#hdnrndHeadWiseBudget').val(JSON.stringify(_RNDHeadWiseBudgetArray));

            var FeasabilityCumTotal = $('#tablerndphasewisebudget tbody tr.phasewisebudget_' + i).find(".CumtotalPWBFeasability").val();
            var PrototypeCumTotal = $('#tablerndphasewisebudget tbody tr.phasewisebudget_' + i).find(".CumtotalPWBPrototype").val();
            var ScaleUpCumTotal = $('#tablerndphasewisebudget tbody tr.phasewisebudget_' + i).find(".CumtotalPWBScaleUp").val();
            var AMVCumTotal = $('#tablerndphasewisebudget tbody tr.phasewisebudget_' + i).find(".CumtotalPWBPWBAMV").val();
            var ExhibitCumTotal = $('#tablerndphasewisebudget tbody tr.phasewisebudget_' + i).find(".CumtotalPWBExhibitScalability").val();
            var FilingCumTotal = $('#tablerndphasewisebudget tbody tr.phasewisebudget_' + i).find(".CumtotalPWBFiling").val();
            var _rndPWBObject = new Object();
            _rndPWBObject.FeasabilityCumTotal = ConvertToNumber(FeasabilityCumTotal == "" ? 0 : FeasabilityCumTotal);
            _rndPWBObject.PrototypeCumTotal = ConvertToNumber(PrototypeCumTotal == "" ? 0 : PrototypeCumTotal);
            _rndPWBObject.ScaleUpCumTotal = ConvertToNumber(ScaleUpCumTotal == "" ? 0 : ScaleUpCumTotal);
            _rndPWBObject.AMVCumTotal = ConvertToNumber(AMVCumTotal == "" ? 0 : AMVCumTotal);
            _rndPWBObject.ExhibitCumTotal = ConvertToNumber(ExhibitCumTotal == "" ? 0 : ExhibitCumTotal);
            _rndPWBObject.FilingCumTotal = ConvertToNumber(FilingCumTotal == "" ? 0 : FilingCumTotal);
            _RNDPhaseWiseBudgetArray.push(_rndPWBObject);

            //$.each($('#tablerndphasewisebudget tbody tr.phasewisebudget_' + i), function () {
            //    var flag = true;
            //    var _rndPWBObject = new Object();
            //    var FeasabilityCumTotal = $(this).find(".CumtotalPWBFeasability").val();
            //    var PrototypeCumTotal = $(this).find(".CumtotalPWBPrototype").val();
            //    var ScaleUpCumTotal = $(this).find(".CumtotalPWBScaleUp").val();
            //    var AMVCumTotal = $(this).find(".CumtotalPWBPWBAMV").val();
            //    var ExhibitCumTotal = $(this).find(".CumtotalPWBExhibitScalability").val();
            //    var FilingCumTotal = $(this).find(".CumtotalPWBFiling").val();

            //    _rndPWBObject.FeasabilityCumTotal = ConvertToNumber(FeasabilityCumTotal == "" ? 0 : FeasabilityCumTotal);
            //    _rndPWBObject.PrototypeCumTotal = ConvertToNumber(PrototypeCumTotal == "" ? 0 : PrototypeCumTotal);
            //    _rndPWBObject.ScaleUpCumTotal = ConvertToNumber(ScaleUpCumTotal == "" ? 0 : ScaleUpCumTotal);
            //    _rndPWBObject.AMVCumTotal = ConvertToNumber(AMVCumTotal == "" ? 0 : AMVCumTotal);
            //    _rndPWBObject.ExhibitCumTotal = ConvertToNumber(ExhibitCumTotal == "" ? 0 : ExhibitCumTotal);
            //    _rndPWBObject.FilingCumTotal = ConvertToNumber(FilingCumTotal == "" ? 0 : FilingCumTotal);
            //    _RNDPhaseWiseBudgetArray.push(_rndPWBObject);
            //    //if (_RNDPhaseWiseBudgetArray.length > 0) {
            //    //    return false;
            //    //}
            //});

            $('#hdnrndPhaseWiseBudget').val(JSON.stringify(_RNDPhaseWiseBudgetArray));
        }
    }
}
//R&D End
function PBFreadOnlyForm() {

    $('#AddPBFForm').find('input,select,textarea,checkbox').attr('readonly', true).attr('disabled', true);
    //$('#tableGeneralStrength, #tablerndbatchsize, #tablerndapirequirement, #tablerndexicipientrequirement, #tablerndpackagingmaterialrequirement, #tablerndtoolingchangepart, #tablerndcapexmiscellaneousexpenses, #tablerndplantsupportcost, #tablerndreferenceproductdetail, #tablerrndmanpowercostprojectduration, #tblFillingExpensesBody').find('input').attr('readonly', true).attr('disabled', true);
    ////$('button').attr('readonly', true).attr('disabled', true);
    //$('#AddPBFForm').find('select').attr('readonly', true).attr('disabled', true).trigger("change");
    $('#AddPBFForm').find('.operationButton').hide();
    $('#btnPBFCancel').show();
}
function PBFTabsReadOnly() {
    _analyticalView = $('#hdnPBFAnalyticalPermission').val();
    _clinicalView = $('#hdnPBFClinicalPermission').val();
    _rndView = $('#hdnPBFRNDPermission').val();

    if (!_analyticalView || _analyticalView == undefined) {
        $('#custom-tabs-Analytical').find('input,select,textarea,checkbox').attr('readonly', true).attr('disabled', true);
        $('#custom-tabs-Analytical').find('.operationButton').hide();
    }
    if (!_clinicalView || _clinicalView == undefined) {
        $('#custom-tabs-Clinical').find('input,select,textarea,checkbox').attr('readonly', true).attr('disabled', true);
        $('#custom-tabs-Clinical').find('.operationButton').hide();

    }
    if (!_rndView || _rndView == undefined) {
        $('#custom-tabs-RnD').find('input,select,textarea,checkbox').attr('readonly', true).attr('disabled', true);
        $('#custom-tabs-RnD').find('.operationButton').hide();

    }
    if (_oralName == "Injectable") {
        $('#msgclinicalnote').show();
        $('#custom-tabs-Clinical').find('input,select,textarea').val('');
        $('#custom-tabs-Clinical').find('input,select,textarea,checkbox').attr('readonly', true).attr('disabled', true);
    }

}
function GetValueFromBatchSize(_ActivityTypeId, _StrengthId) {
    if (parseInt(_ActivityTypeId) == 1) {
        return $('#tablerndbatchsize').find('[data-strengthid=' + _StrengthId + ']').find('.calcRNDBatchSizesPrototypeFormulation').val();
    } else if (parseInt(_ActivityTypeId) == 2) {
        return $('#tablerndbatchsize').find('[data-strengthid=' + _StrengthId + ']').find('.calcRNDBatchSizesScaleUpbatch').val();
    } else if (parseInt(_ActivityTypeId) == 3) {
        var _TotalExhibitCost = 0;
        for (var i = 1; i < 4; i++) {
            var _ExhibitValue = $('#tablerndbatchsize').find('[data-strengthid=' + _StrengthId + ']').find('.calcRNDBatchSizesExhibitBatch' + i.toString()).val();
            _TotalExhibitCost += parseFloat(_ExhibitValue == "" ? 0 : _ExhibitValue);
        }
        return _TotalExhibitCost;
    } else {
        return 0;
    }
}
function SetPhaseWiseBudget() {
    if (!_firstLoad) {
        var _manHourRate = $('#RNDMasterEntities_ManHourRate').val();
        var _plantsupportcost = $('#RNDMasterEntities_PlanSupportCostRsPerDay').val();
        var _FormulationActivityTypeId = 1;
        var _ScaleupActivityTypeId = 2;
        var _ExhibitActivityTypeId = 3;
        var _PivotalBioFedId = 4;

        $.each(_strengthArray, function (index, item) {
            /*Set Feasibility*/
            var _projectInitiation = $('.MPCActivity[data-activityid=1]').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.rndMPCStrengthValue').val();
            var _literatureReview = $('.MPCActivity[data-activityid=2]').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.rndMPCStrengthValue').val();

            var PWFeasibility = (ConvertToNumber(_projectInitiation == "" ? 0 : _projectInitiation) + ConvertToNumber(_literatureReview == "" ? 0 : _literatureReview)) * ConvertToNumber(_manHourRate == "" ? 0 : _manHourRate);
            $('.phasewisebudget_1').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcPWBRNDFeasability').val(formatNumber(PWFeasibility));
            /*End of Set Feasibility*/

            /*Protoype Development*/
            var _formulationManPower = $('.MPCActivity[data-activityid=3]').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.rndMPCStrengthValue').val();
            var _analyticalManPower = $('.MPCActivity[data-activityid=4]').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.rndMPCStrengthValue').val();
            var _pilotBioManPower = $('.MPCActivity[data-activityid=5]').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.rndMPCStrengthValue').val();

            var _manPowerTotalFormulation = ((ConvertToNumber(_formulationManPower) + ConvertToNumber(_analyticalManPower) + ConvertToNumber(_pilotBioManPower)) * ConvertToNumber(_manHourRate));

            var _RPDFormulation = $('#tablerndreferenceproductdetail').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcRNDRPDFormulationDevelopment').val();
            var _RPDPilot = $('#tablerndreferenceproductdetail').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcRNDRPDPilotBe').val();
            var _RPDUnit = $('#tablerndreferenceproductdetail').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcRNDRPDUnitCostOfReferenceProduct').val();

            var _RPDTotalFormulation = ((ConvertToNumber(_RPDFormulation) + ConvertToNumber(_RPDPilot)) * ConvertToNumber(_RPDUnit));

            var _APIFormulation = $('#tablerndapirequirement').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcRNDApirequirementsPrototypeCost').val();

            var _ExcipientFormulation = $('#tablerndexicipientrequirement').find(".exicipientActivity" + _FormulationActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthexicipient').val();

            var _PackagingFormulation = $('#tablerndpackagingmaterialrequirement').find(".packagingActivity" + _FormulationActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthpackaging').val();

            var _AnalyticalFormulation = $('#tableanalytical').find(".analyticalActivity" + _FormulationActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthAnalytical').val();

            var _PilotBioFastingClinical = $('#tableclinical').find(".clinicalcal_" + _FormulationActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthClinical').val();

            var _PilotBioFedClinical = $('#tableclinical').find(".clinicalcal_" + _ScaleupActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthClinical').val();

            var _ToolingChangePart = $('#tablerndtoolingchangepart').find(".ToolingChangePartActivity" + _FormulationActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthTooling').val();

            var _CapexMisc = $('#tablerndcapexmiscellaneousexpenses').find(".CapexMiscActivity" + _FormulationActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthMisc').val();

            $('.phasewisebudget_1').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcRNDPWBPrototypedevelopment').val(formatNumber(_manPowerTotalFormulation + _RPDTotalFormulation + ConvertToNumber(_APIFormulation) + ConvertToNumber(_ExcipientFormulation) + ConvertToNumber(_PackagingFormulation) + ConvertToNumber(_AnalyticalFormulation) + ConvertToNumber(_PilotBioFastingClinical) + ConvertToNumber(_PilotBioFedClinical) + ConvertToNumber(_ToolingChangePart) + ConvertToNumber(_CapexMisc)));
            /*end of Protoype Development*/


            /*Scale Up*/
            var _scaleupManPower = $('.MPCActivity[data-activityid=6]').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.rndMPCStrengthValue').val();

            var _manPowerTotalScaleUp = ((ConvertToNumber(_scaleupManPower)) * ConvertToNumber(_manHourRate));

            var _PlantSupportScaleUp = $('#tablerndplantsupportcost').find(".PlantSupportCost_1").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find(".calcRNDPlantSupportCostsScaleUp").val();

            var _PlantSupportTotalScaleUp = (ConvertToNumber(_PlantSupportScaleUp) * ConvertToNumber(_plantsupportcost));

            var _APIScaleup = $('#tablerndapirequirement').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcRNDApirequirementsScaleUpCost').val();

            var _ExcipientScaleup = $('#tablerndexicipientrequirement').find(".exicipientActivity" + _ScaleupActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthexicipient').val();

            var _PackagingScaleup = $('#tablerndpackagingmaterialrequirement').find(".packagingActivity" + _ScaleupActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthpackaging').val();

            var _AnalyticalScaleup = $('#tableanalytical').find(".analyticalActivity" + _ScaleupActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthAnalytical').val();

            var _ToolingChangePartScaleup = $('#tablerndtoolingchangepart').find(".ToolingChangePartActivity" + _ScaleupActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthTooling').val();

            $('.phasewisebudget_1').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcRNDPWBScaleUp').val(formatNumber(_manPowerTotalScaleUp + _PlantSupportTotalScaleUp + ConvertToNumber(_APIScaleup) + ConvertToNumber(_ExcipientScaleup) + ConvertToNumber(_PackagingScaleup) + ConvertToNumber(_AnalyticalScaleup) + ConvertToNumber(_ToolingChangePartScaleup)));
            /*end of Scale up*/


            /*Set AMV ATT*/
            var _AMVATT = $('.MPCActivity[data-activityid=7]').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.rndMPCStrengthValue').val();
            var _analyticalAMV = $('#tableanalytical').find('.analyticalActivity4').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.analyticalAMVStrengthValue').val();
            var PWAMV = ((ConvertToNumber(_AMVATT == "" ? 0 : _AMVATT)) * ConvertToNumber(_manHourRate == "" ? 0 : _manHourRate)) + ConvertToNumber(_analyticalAMV == "" ? 0 : _analyticalAMV);

            $('.phasewisebudget_1').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcRNDPWBAMV').val(formatNumber(PWAMV));
            /*End AMV ATT*/


            /*Exhibit Development*/
            var _ExhibitManPower = $('.MPCActivity[data-activityid=8]').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.rndMPCStrengthValue').val();
            var _pivotalManPower = $('.MPCActivity[data-activityid=9]').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.rndMPCStrengthValue').val();
            var _stabilityManPower = $('.MPCActivity[data-activityid=10]').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.rndMPCStrengthValue').val();

            var _manPowerTotalExhibit = ((ConvertToNumber(_ExhibitManPower) + ConvertToNumber(_pivotalManPower) + ConvertToNumber(_stabilityManPower)) * ConvertToNumber(_manHourRate));

            var _PlantSupportExhibit = $('#tablerndplantsupportcost').find(".PlantSupportCost_1").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find(".calcRNDPlantSupportCostsExhibit").val();

            var _PlantSupportTotalExhibit = (ConvertToNumber(_PlantSupportExhibit) * ConvertToNumber(_plantsupportcost));

            var _RPDExhibit = $('#tablerndreferenceproductdetail').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcRNDRPDPharmasuiticalEquivalence').val();
            var _RPDPivotal = $('#tablerndreferenceproductdetail').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcRNDRPDPivotalBio').val();

            var _RPDTotalExhibit = ((ConvertToNumber(_RPDExhibit) + ConvertToNumber(_RPDPivotal)) * ConvertToNumber(_RPDUnit));

            var _APIExhibit = $('#tablerndapirequirement').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcRNDApirequirementsExhibitBatchCost').val();

            var _ExcipientExhibit = $('#tablerndexicipientrequirement').find(".exicipientActivity" + _ExhibitActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthexicipient').val();

            var _PackagingExhibit = $('#tablerndpackagingmaterialrequirement').find(".packagingActivity" + _ExhibitActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthpackaging').val();

            var _AnalyticalExhibit = $('#tableanalytical').find(".analyticalActivity" + _ExhibitActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthAnalytical').val();

            var _PilotBioFastingClinicalExhibit = $('#tableclinical').find(".clinicalcal_" + _ExhibitActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthClinical').val();

            var _PilotBioFedClinicalExhibit = $('#tableclinical').find(".clinicalcal_" + _PivotalBioFedId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthClinical').val();

            var _ToolingChangePartExhibit = $('#tablerndtoolingchangepart').find(".ToolingChangePartActivity" + _ExhibitActivityTypeId + "Total").find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrengthTooling').val();

            $('.phasewisebudget_1').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcRNDPWBExhibitScalability').val(formatNumber(_manPowerTotalExhibit + _PlantSupportTotalExhibit + _RPDTotalExhibit + ConvertToNumber(_APIExhibit) + ConvertToNumber(_ExcipientExhibit) + ConvertToNumber(_PackagingExhibit) + ConvertToNumber(_AnalyticalExhibit) + ConvertToNumber(_PilotBioFastingClinicalExhibit) + ConvertToNumber(_PilotBioFedClinicalExhibit) + ConvertToNumber(_ToolingChangePartExhibit)));
            /*end of Exhibit Development*/

            /*Set Filling*/
            var _Filing = $('.MPCActivity[data-activityid=11]').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.rndMPCStrengthValue').val();
            var filingExpense = $('#tablerndfilingexpenses').find('.FillingExpensesActivity1Total').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find(".calcTotalCostForStrengthFilling").val();
            var PWFiling = ((ConvertToNumber(_Filing == "" ? 0 : _Filing)) * ConvertToNumber(_manHourRate == "" ? 0 : _manHourRate)) + ConvertToNumber(filingExpense == "" ? 0 : filingExpense);

            $('.phasewisebudget_1').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcRNDPWBFiling').val(formatNumber(PWFiling));
            /*End of Set Filling*/

            /*Total for Strength*/
            var _TotalCostForStrength = 0;
            $('#tablerndphasewisebudget').find(".phasewisebudget_1").each(function (i, t) {
                _TotalCostForStrength += ConvertToNumber($(t).find('[data-strengthid=' + item.pidfProductStrengthId + ']').find("input[type='text']").val());
            });
            //tempaorary setting the total value
            $('.phasewisebudget_1Total').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrength').val(formatNumber(_TotalCostForStrength));
            /*End of Total for Strength*/
        });

        var FinalTotalForPWBStrength = 0;
        $('#tablerndphasewisebudget').find(".phasewisebudget_1").each(function (x, y) {
            var _TotalForSection = 0;
            $.each(_strengthArray, function (index, item) {
                _TotalForSection += ConvertToNumber($(y).find('[data-strengthid=' + item.pidfProductStrengthId + ']').find("input[type=text]:not('.totalPWB')").val());
            });
            FinalTotalForPWBStrength += _TotalForSection;
            $(y).find(".totalPWB").val(formatNumber(_TotalForSection));
            $(y).find(".CumTotalPWB").val(formatNumber(FinalTotalForPWBStrength));
            //$(y).find(".PercenttotalPWB").val(ConvertToNumber(_TotalForSection));
        });
        $('#tablerndphasewisebudget').find(".phasewisebudget_1").each(function (x, y) {
            var _cumTotal = ConvertToNumber($(y).find(".CumTotalPWB").val());
            var PercenttotalPWBresult = ((_cumTotal / FinalTotalForPWBStrength) * 100).toFixed(2);
            PercenttotalPWBresult = isNaN(PercenttotalPWBresult) ? 0 : PercenttotalPWBresult;
            $(y).find(".PercenttotalPWB").val(PercenttotalPWBresult);
        });
        $('#tablerndphasewisebudget').find(".phasewisebudget_1Total").find(".calcTotalCostForStrengthTotal").val(formatNumber(FinalTotalForPWBStrength));

        SetTotalExpenses();
    }
}
function SetHeadWiseBudget() {
    if (!_firstLoad) {
        var _manHourRate = $('#RNDMasterEntities_ManHourRate').val();
        var _plantsupportcost = $('#RNDMasterEntities_PlanSupportCostRsPerDay').val();
        var _FormulationActivityTypeId = 1;
        var _ScaleupActivityTypeId = 2;
        var _ExhibitActivityTypeId = 3;
        var _PivotalBioFedId = 4;


        /*Protoype Development*/
        var _totalManpowerCostPrototype = $('#tablerrndmanpowercostprojectduration').find('.manpowercost_1Total').find('.calcTotalPrototypeCostForStrengthTotal').val();

        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=1]').find('.rndHWBPrototype').val(_totalManpowerCostPrototype);

        var _APIReqCostPrototype = $('#tablerndapirequirement').find('.ApiRequirement_1').find('.APIReqPrototypeCostTotalCost').val();
        var _exicipientPrototype = $('#tablerndexicipientrequirement').find('.exicipientActivity1Total').find('.calcTotalCostForexicipient1').val();
        var _packagingPrototype = $('#tablerndpackagingmaterialrequirement').find('.packagingActivity1Total').find('.calcTotalCostForPackaging1').val();

        var TotalMaterialCostPrototype = (ConvertToNumber(_APIReqCostPrototype) + ConvertToNumber(_exicipientPrototype) + ConvertToNumber(_packagingPrototype));

        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=2]').find('.rndHWBPrototype').val(formatNumber(TotalMaterialCostPrototype));
        var _plantSupportCostPrototype = 0;
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=3]').find('.rndHWBPrototype').val(formatNumber(_plantSupportCostPrototype));

        var _referenceProductFormulationCost = $('#tablerndreferenceproductdetail').find('.RPDcal_1').find('.TotalFormulation').val();
        var _referenceProductPilotBeCost = $('#tablerndreferenceproductdetail').find('.RPDcal_1').find('.TotalPilotBe').val();
        var _referenceProductDetailsCostPrototype = (ConvertToNumber(_referenceProductFormulationCost) + ConvertToNumber(_referenceProductPilotBeCost));
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=4]').find('.rndHWBPrototype').val(formatNumber(_referenceProductDetailsCostPrototype));

        var analyticalCostPrototype = $('#tableanalytical').find('.analyticalActivity1Total').find('.calcTotalCostForAnalytical1').val();
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=5]').find('.rndHWBPrototype').val(analyticalCostPrototype);

        var _clinicalTotalPilotFasting = $('#tableclinical').find('.clinicalcal_1Total').find('.calcTotalCostForStrengthClinicalTotal').val();
        var _clinicalTotalPilotFed = $('#tableclinical').find('.clinicalcal_2Total').find('.calcTotalCostForStrengthClinicalTotal').val();
        var bioStudyCostprototype = ((ConvertToNumber(_clinicalTotalPilotFasting) + ConvertToNumber(_clinicalTotalPilotFed)));
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=6]').find('.rndHWBPrototype').val(formatNumber(bioStudyCostprototype));

        var toolingchangepartprototype = $('#tablerndtoolingchangepart').find('.ToolingChangePartActivity1Total').find('.calcTotalCostForTooling1').val();
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=7]').find('.rndHWBPrototype').val(formatNumber(toolingchangepartprototype));

        var OtherExpensesprototype = $('#tablerndcapexmiscellaneousexpenses').find('.CapexMiscActivity1Total').find('.calcTotalCostForMisc1').val();
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=8]').find('.rndHWBPrototype').val(formatNumber(OtherExpensesprototype));

        var FilingExpensesprototype = 0;
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=9]').find('.rndHWBPrototype').val(FilingExpensesprototype);
        /*end of Protoype Development*/

        /*ScaleUp Development*/
        var _totalManpowerCostScaleUp = $('#tablerrndmanpowercostprojectduration').find('.manpowercost_1Total').find('.calcTotalScaleupCostForStrengthTotal').val();
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=1]').find('.rndHWBScaleUp').val(_totalManpowerCostScaleUp);

        var _APIReqCostScaleUp = $('#tablerndapirequirement').find('.ApiRequirement_1').find('.APIReqScaleupCostTotalCost').val();
        var _exicipientScaleUp = $('#tablerndexicipientrequirement').find('.exicipientActivity2Total').find('.calcTotalCostForexicipient2').val();
        var _packagingScaleUp = $('#tablerndpackagingmaterialrequirement').find('.packagingActivity2Total').find('.calcTotalCostForPackaging2').val();
        var TotalMaterialCostScaleUp = (ConvertToNumber(_APIReqCostScaleUp) + ConvertToNumber(_exicipientScaleUp) + ConvertToNumber(_packagingScaleUp));
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=2]').find('.rndHWBScaleUp').val(formatNumber(TotalMaterialCostScaleUp));

        var _plantSupportCostScaleUp = $('#tablerndplantsupportcost').find('.PlantSupportCost_1').find('.TotalPSCScaleUp').val();
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=3]').find('.rndHWBScaleUp').val(formatNumber(ConvertToNumber(_plantSupportCostScaleUp) * _plantsupportcost));

        var _referenceProductDetailsCostScaleUp = 0;
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=4]').find('.rndHWBScaleUp').val(ConvertToNumber(_referenceProductDetailsCostScaleUp));

        var analyticalCostScaleUp = $('#tableanalytical').find('.analyticalActivity2Total').find('.calcTotalCostForAnalytical2').val();
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=5]').find('.rndHWBScaleUp').val(analyticalCostScaleUp);

        var bioStudyCostScaleUp = 0;
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=6]').find('.rndHWBScaleUp').val(bioStudyCostScaleUp);

        var toolingchangepartScaleUp = $('#tablerndtoolingchangepart').find('.ToolingChangePartActivity2Total').find('.calcTotalCostForTooling2').val();
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=7]').find('.rndHWBScaleUp').val(formatNumber(toolingchangepartScaleUp));

        var OtherExpensesScaleUp = 0;
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=8]').find('.rndHWBScaleUp').val(OtherExpensesScaleUp);

        var FilingExpensesScaleUp = 0;
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=9]').find('.rndHWBScaleUp').val(FilingExpensesScaleUp);

        /*end of ScaleUp Development*/

        /*Exhibit Development*/
        var _totalManpowerCostExhibit = $('#tablerrndmanpowercostprojectduration').find('.manpowercost_1Total').find('.calcTotalExhibitCostForStrengthTotal').val();
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=1]').find('.rndHWBExhibit').val(_totalManpowerCostExhibit);

        var _APIReqCostExhibit = $('#tablerndapirequirement').find('.ApiRequirement_1').find('.APIReqExhibitBatchTotalCost').val();
        var _exicipientExhibit = $('#tablerndexicipientrequirement').find('.exicipientActivity3Total').find('.calcTotalCostForexicipient3').val();
        var _packagingExhibit = $('#tablerndpackagingmaterialrequirement').find('.packagingActivity3Total').find('.calcTotalCostForPackaging3').val();

        var TotalMaterialCostExhibit = (ConvertToNumber(_APIReqCostExhibit) + ConvertToNumber(_exicipientExhibit) + ConvertToNumber(_packagingExhibit));
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=2]').find('.rndHWBExhibit').val(formatNumber(TotalMaterialCostExhibit));

        var _plantSupportCostExhibit = $('#tablerndplantsupportcost').find('.PlantSupportCost_1').find('.TotalPSCExhibit').val();
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=3]').find('.rndHWBExhibit').val(formatNumber(ConvertToNumber(_plantSupportCostExhibit) * _plantsupportcost));

        var _referenceProductDetailsCost = $('#tablerndreferenceproductdetail').find('.RPDcal_1Total').find('.calcTotalCostForStrengthTotalRPD').val();
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=4]').find('.rndHWBExhibit').val(_referenceProductDetailsCost);

        var analyticalCostExhibit = $('#tableanalytical').find('.analyticalActivity3Total').find('.calcTotalCostForAnalytical3').val();
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=5]').find('.rndHWBExhibit').val(analyticalCostExhibit);

        var _clinicalTotalPivotalFasting = $('#tableclinical').find('.clinicalcal_3Total').find('.calcTotalCostForStrengthClinicalTotal').val();
        var _clinicalTotalPivotalFed = $('#tableclinical').find('.clinicalcal_4Total').find('.calcTotalCostForStrengthClinicalTotal').val();
        var bioStudyCostExhibit = ((ConvertToNumber(_clinicalTotalPivotalFasting) + ConvertToNumber(_clinicalTotalPivotalFed)));
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=6]').find('.rndHWBExhibit').val(formatNumber(bioStudyCostExhibit));

        var toolingchangepartExhibit = 0;
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=7]').find('.rndHWBExhibit').val(toolingchangepartExhibit);

        var OtherExpensesExhibit = 0;
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=8]').find('.rndHWBExhibit').val(OtherExpensesExhibit);

        var FilingExpensesExhibit = $('#tablerndfilingexpenses').find('.FillingExpensesActivity1Total').find('.calcTotalCostForStrengthTotalFilling').val();
        $('#tableHeadWiseBudget').find('.headwisebudget_1[data-activityid=9]').find('.rndHWBExhibit').val(FilingExpensesExhibit);

        /*end of Exhibit Development*/

        /*Total for Strength*/
        var _totalCostforPrototype = 0;
        var _totalCostforScaleUp = 0;
        var _totalCostforExhibit = 0;
        var _totalCostForRow = 0;
        for (var i = 1; i < 10; i++) {
            var _prototypeval = $('#tableHeadWiseBudget').find('.rndHWBPrototype' + i).val();
            _totalCostforPrototype += ConvertToNumber(_prototypeval);

            var _scaleupval = $('#tableHeadWiseBudget').find('.rndHWBScaleUp' + i).val();
            _totalCostforScaleUp += ConvertToNumber(_scaleupval);

            var _exhibitval = $('#tableHeadWiseBudget').find('.rndHWBExhibit' + i).val();
            _totalCostforExhibit += ConvertToNumber(_exhibitval);

            _totalCostForRow = ConvertToNumber(_prototypeval) + ConvertToNumber(_scaleupval) + ConvertToNumber(_exhibitval);
            $('#tableHeadWiseBudget').find('[data-activityid=' + i + ']').find('.TotalStrength').val(formatNumber(_totalCostForRow));

        }
        $('#tableHeadWiseBudget').find('.calcTotalCostForPrototype').val(formatNumber(_totalCostforPrototype));
        $('#tableHeadWiseBudget').find('.calcTotalCostForScaleUp').val(formatNumber(_totalCostforScaleUp));
        $('#tableHeadWiseBudget').find('.calcTotalCostForExhibit').val(formatNumber(_totalCostforExhibit));
        $('#tableHeadWiseBudget').find('.calcTotalCostHeadWiseBudget').val(formatNumber(_totalCostforPrototype + _totalCostforScaleUp + _totalCostforExhibit));


        /*End of Total for Strength*/
    }
}
function SetTotalExpenses() {
    //var _manHourRate = $('#RNDMasterEntities_ManHourRate').val();
    var _TotalForStrength = 0;
    $.each(_strengthArray, function (index, item) {
        var totalPWB = $('.phasewisebudget_1Total').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTotalCostForStrength').val();
        $('.totalexpenses_1').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find('.calcTE').val(formatNumber((ConvertToNumber(totalPWB == "" ? 0 : totalPWB) / 100000)));
        var _strengthValue = $('.totalexpenses_1').find('[data-strengthid=' + item.pidfProductStrengthId + ']').find(".calcTE").val();
        _TotalForStrength += ConvertToNumber(_strengthValue == "" ? 0 : _strengthValue);
        $('.totalexpenses_1').find(".totalTE").val(formatNumber(_TotalForStrength));
        $('#TotalExpense').val(formatNumber(_TotalForStrength));
        $('#lblTotalExpenseForPIDF').text(_currencySymbol + " " + formatNumber(_TotalForStrength));
    });

}


function formatNumber(value, round, code) {
    if (code == null || code == undefined || code == "") {
        code = "en-US"
    }
    if (round == null || round == undefined || round == "") {
        value = parseFloat(value).toFixed(2);
    } else {
        value = Math.round(parseFloat(value));
    }
    return new Intl.NumberFormat(code).format(value);
}
function ConvertToNumber(number, type) {
    //number = number.toString().replace(",", "");
    number = ReplaceSelector(number);
    if (number == "" || number == null || number == undefined || number == NaN) {
        number = "0";
    }
    if (type == null || type == undefined || type == 1) {
        return parseFloat(number);

    } else {
        return parseInt(number);
    }
}
function ReplaceSelector(value, selector, newvalue) {
    if (newvalue == null || newvalue == undefined) {
        newvalue = "";
    }
    if (selector == null || selector == undefined) {
        selector = ",";
    }
    if (value == null || value == undefined) {
        return "";
    }
    return value.toString().replaceAll(selector, newvalue);

}

function validateDynamicControldDetailsPBF() {
    isValidPBFForm = true;
    $('.customvalidateformcontrol').each(function () {
        validatecontrolsPBF(this);
    });
}
function validatecontrolsPBF(control) {

    if ($(control).val() != null) {
        if ($(control).val().trim() == '') {
            $(control).css("border-color", "red");
            $(control).focus();
            isValidPBFForm = false;
        }
        else {
            $(control).css("border-color", "");
        }
    }
    else {
        $(control).css("border-color", "red");
        $(control).focus();
        isValidPBFForm = false;
    } //return isValidPBFForm;

}

//** For RA Tab **//
//
async function GetCountyByBussinessUnitId() {
    let id = SelectedBUValue == 0 ? _selectBusinessUnit : SelectedBUValue;
    ajaxServiceMethod($('#hdnBaseURL').val() + getCountryByBusinessUnitIdurl + "/" + id, 'GET', GetCountryByBusinessUnitSuccess, GetCountryByBusinessUnitError);
}
function GetCountryByBusinessUnitSuccess(data) {
    //alert(data)
    try {
        $('#raCountryId0').find('option').remove()
        if (data._object != null && data._object.length > 0) {
            var _emptyOption = '<option value="">-- Select --</option>';
            $('#raCountryId0').append(_emptyOption);
            $(data._object).each(function (index, item) {
                $('#raCountryId0').append('<option value="' + item.countryId + '">' + item.countryName + '</option>');
            });

            //try {
            //    if ($("#WishListId").val() > 0) {
            //        $('#raCountryId').val($('#hdnCountryId').val());
            //    }
            //} catch (e) {
            //}
        }
    }
    catch (e) {
        toastr.error(ErrorMessage);
    }
}
function GetCountryByBusinessUnitError(x, y, z) {
    toastr.error(ErrorMessage);
}
async function GetTypeOfSubmission() {

    ajaxServiceMethod($('#hdnBaseURL').val() + getTypeOfSubmissionurl, 'GET', GetTypeOfSubmissionSuccess, GetTypeOfSubmissionError);
}
function GetTypeOfSubmissionSuccess(data) {
    try {
      
            $('#TypeOfSubmissionId0').find('option').remove()
            if (data != null && data.length > 0) {
                var _emptyOption = '<option value="">-- Select --</option>';
                $('#TypeOfSubmissionId0').append(_emptyOption);
                $(data).each(function (index, item) {

                    $('#TypeOfSubmissionId0').append('<option value="' + item.id + '">' + item.typeOfSubmission + '</option>');
                });

                //try {
                //    if ($("#WishListId").val() > 0) {
                //        $('#raCountryId').val($('#hdnCountryId').val());
                //    }
                //} catch (e) {
                //}
            }
        
         
        
    }
    catch (e) {
        toastr.error(e.message);
    }
}
function GetTypeOfSubmissionError(x, y, z) {
    toastr.error(ErrorMessage);
}
function BindRA(data=null) {
    let tbody = $("#tableRABody");
    IsRaEditable = false;
    let tr = '';
    if (data == null) {
        tr += `<tr>
             <td><input type="hidden" id="Pidfpbfraid0" class="ra" name="RaEntities[0].Pidfpbfraid" value="0"><select id="raCountryId0" name="RaEntities[0].CountryIdBuId" class="form-control readOnlyUpdate valid"></select></td>
             <td><input type="date" id="Pivotalbatchmanufactured0" name="RaEntities[0].PivotalBatchManufactured" class="form-control readOnlyUpdate  valid"></td>
             <td><input type="date" id="LastdatafromRnD0" name="RaEntities[0].LastDataFromRnD" class="form-control readOnlyUpdate  valid"></td>
             <td><input type="date" id="BEFinalReport0" name="RaEntities[0].BEFinalReport" class="form-control readOnlyUpdate  valid"></td>
             <td><select  id="TypeOfSubmissionId0" name="RaEntities[0].TypeOfSubmissionId" class="form-control readOnlyUpdate  valid"></select></td>
             <td><input type="date" id="DossierReadyDate0" name="RaEntities[0].DossierReadyDate" class="form-control readOnlyUpdate  valid"></td>
             <td><input type="date" id="EarliestSubmissionDExcl0" name="RaEntities[0].EarliestSubmissionDExcl" class="form-control readOnlyUpdate  valid"></td>
              <td><input type="date" id="EarliestLaunchDexcl0" name="RaEntities[0].EarliestLaunchDexcl" class="form-control readOnlyUpdate valid"></td>
              <td> <input type="date" id="LasDateToRegulatory0" name="RaEntities[0].LasDateToRegulatory" class="form-control readOnlyUpdate valid"></td>
             <td>  <i class="fa-solid fa-circle-plus nav-icon text-success operationButton" id="addIcon" onclick="addRowra(this);"></i> <i class="fa-solid fa-trash nav-icon text-red raDeleteIcon operationButton DeleteIcon" onclick="deleteRowra(this);" style="display: none;"></i>
             </td>
             </tr>`;
       
    }
    else if (data!=null) {
        IsRaEditable = true;
        for (let e = 0; e < data.length; e++) {
            tr += `<tr>
             <td><input type="hidden" id="Pidfpbfraid${e}" class="ra" name="RaEntities[${e}].Pidfpbfraid" value="${data[e].pidfpbfraid}"><select id="raCountryId${e}" name="RaEntities[${e}].CountryIdBuId" value="${data[e].countryIdBuId}"  class="form-control readOnlyUpdate  valid"></select></td>
             <td><input type="date" id="Pivotalbatchmanufactured${e}" name="RaEntities[${e}].PivotalBatchManufactured" value="${data[e].pivotalBatchManufactured.split('T')[0]}" class="form-control readOnlyUpdate  valid"></td>
             <td><input type="date" id="LastdatafromRnD${e}" name="RaEntities[${e}].LastDataFromRnD" value="${data[e].lastDataFromRnD.split('T')[0]}" class="form-control readOnlyUpdate  valid"></td>
             <td><input type="date" id="BEFinalReport${e}" name="RaEntities[${e}].BEFinalReport" value="${data[e].befinalReport.split('T')[0]}" class="form-control readOnlyUpdate  valid"></td>
            
             <td><select  id="TypeOfSubmissionId${e}" name="RaEntities[${e}].TypeOfSubmissionId" class="form-control readOnlyUpdate  valid" value="${data[e].typeOfSubmissionId}"></select></td>
             <td><input type="date" id="DossierReadyDate${e}" name="RaEntities[${e}].DossierReadyDate" value="${data[e].dossierReadyDate.split('T')[0]}" class="form-control readOnlyUpdate  valid"></td>
             <td><input type="date" id="EarliestSubmissionDExcl${e}" name="RaEntities[${e}].EarliestSubmissionDExcl" value="${data[e].earliestSubmissionDexcl.split('T')[0]}" class="form-control readOnlyUpdate  valid"></td>
              <td><input type="date" id="EarliestLaunchDexcl${e}" name="RaEntities[${e}].EarliestLaunchDexcl" value="${data[e].earliestLaunchDexcl == null ? '' : data[e].earliestLaunchDexcl.split('T')[0]}" class="form-control readOnlyUpdate  valid"></td>
              <td> <input type="date" id="LasDateToRegulatory${e}" name="RaEntities[${e}].LasDateToRegulatory" value="${data[e].lasDateToRegulatory == null ? '' : data[e].lasDateToRegulatory.split('T')[0]}" class="form-control readOnlyUpdate valid"></td>
             <td> <i class="fa-solid fa-circle-plus nav-icon text-success operationButton" id="addIcon" onclick="addRowra(this);"></i> <i class="fa-solid fa-trash nav-icon text-red raDeleteIcon operationButton DeleteIcon" onclick="deleteRowra(this);" style="display: none;"></i>
             </td>
             </tr>`;
        }

      
    }
    tbody.append(tr);
    ShowHideRaDelete();
     GetCountyByBussinessUnitId();
     GetTypeOfSubmission();
    data = null;
}
function GetRa(PidfId,PifdPbfId) {

    ajaxServiceMethod($('#hdnBaseURL').val() + GetRaurl + "/" + PidfId + "/" + PifdPbfId, 'GET', GetRaSuccess, GetRaError);
}
function GetRaSuccess(response) {
    try {
        if (response.length > 0) {
            console.log(response);
            BindRA(response);
        }
        else {
            BindRA();
        }
    }
    
    catch (e) {
        toastr.error(ErrorMessage);
    }
}
function GetRaError(x, y, z) {
    toastr.error(ErrorMessage);
}
function addRowra(element) {
    var node = $(element).parent().parent().clone(true);
    node.find("input.form-control").val("");
    node.find("select.form-control").removeAttr('value');
    node.find("input.ra").val("");
    $(element).parent().parent().after(node);
    SetRaChildRow();
    ShowHideRaDelete();
}
function ShowHideRaDelete() {
    if ($("#tableRABody").find("tr").length > 1) {
        $(".raDeleteIcon").show();
    }
    else {
        $(".raDeleteIcon").hide();
    }
}
function deleteRowra(element) {
    $(element).closest("tr").remove();
    SetRaChildRow();
    ShowHideRaDelete();

}
function SetRaChildRow() {
    $.each($('#tableRABody tr'), function (index, value) {


        
        $(this).find("td:eq(0) input").attr("name", "RaEntities[" + index.toString() + "].Pidfpbfraid");
        $(this).find("td:eq(0) input").attr("id", "Pidfpbfraid" + index.toString());
        $(this).find("td:eq(0) select").attr("name", "RaEntities[" + index.toString() + "].CountryIdBuId");
        $(this).find("td:eq(0) select").attr("id", "raCountryId" + index.toString());

        $(this).find("td:eq(1) input").attr("name", "RaEntities[" + index.toString() + "].PivotalBatchManufactured");
        $(this).find("td:eq(1) input").attr("id", "PivotalBatchManufactured" + index.toString());


        $(this).find("td:eq(2) input").attr("name", "RaEntities[" + index.toString() + "].LastDataFromRnD");
        $(this).find("td:eq(2) input").attr("id", "LastDataFromRnD" + index.toString());
        $(this).find("td:eq(3) input").attr("name", "RaEntities[" + index.toString() + "].BEFinalReport");
        $(this).find("td:eq(3) input").attr("id", "BEFinalReport" + index.toString());
        //$(this).find("td:eq(4) select").attr("name", "RaEntities[" + index.toString() + "].CountryId");
        //$(this).find("td:eq(4) select").attr("id", "CountryId" + index.toString());
        $(this).find("td:eq(4) select").attr("name", "RaEntities[" + index.toString() + "].TypeOfSubmissionId");
        $(this).find("td:eq(4) select").attr("id", "TypeOfSubmissionId" + index.toString());

        $(this).find("td:eq(5) input").attr("name", "RaEntities[" + index.toString() + "].DossierReadyDate");
        $(this).find("td:eq(5) input").attr("id", "DossierReadyDate" + index.toString());

        $(this).find("td:eq(6) input").attr("name", "RaEntities[" + index.toString() + "].EarliestSubmissionDExcl");
        $(this).find("td:eq(6) input").attr("id", "EarliestSubmissionDExcl" + index.toString());
        $(this).find("td:eq(7) input").attr("name", "RaEntities[" + index.toString() + "].EarliestLaunchDexcl");
        $(this).find("td:eq(7) input").attr("id", "EarliestLaunchDexcl" + index.toString());

        $(this).find("td:eq(8) input").attr("name", "RaEntities[" + index.toString() + "].LasDateToRegulatory");
        $(this).find("td:eq(8) input").attr("id", "LasDateToRegulatory" + index.toString());

        
    });
}
async function bindRaDropDowns() {
    if (IsRaEditable) {

        for (let z = 0; z < $("#tableRABody").find("tr").length; z++) {

            if (z > 0) {
                await $("#TypeOfSubmissionId0 option").each(function () {
                    $("#TypeOfSubmissionId" + z).append('<option value="' + this.value + '">' + this.text + '</option>');

                });
                //await $("#raAllCountryId0 option").each(function () {
                //    $("#raAllCountryId" + z).append('<option value="' + this.value + '">' + this.text + '</option>');
                //});
                await $("#raCountryId0 option").each(function () {
                    $("#raCountryId" + z).append('<option value="' + this.value + '">' + this.text + '</option>');
                });
            }
            $("#TypeOfSubmissionId" + z).val($("#TypeOfSubmissionId" + z).attr('value') == undefined ? 0 : $("#TypeOfSubmissionId" + z).attr('value'))

            /*$("#raAllCountryId" + z).val($("#raAllCountryId" + z).attr('value') == undefined ? 0 : $("#raAllCountryId" + z).attr('value'))*/

            $("#raCountryId" + z).val($("#raCountryId" + z).attr('value') == undefined ? 0 : $("#raCountryId" + z).attr('value'))
        }
    }
}