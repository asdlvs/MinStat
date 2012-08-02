$(document).ready(function () {
    $('#federalDistrictId').popover('hide');
    $('#federalSubjectId').popover('hide');
    $('#enterpriseId').popover('hide');
    
    if ($("#federalDistrictId").val() != null && $("#federalDistrictId").val() != undefined && $("#federalDistrictId").val() != 0) {
        fillFederalSubjectsLists($("#federalDistrictId").val());

    }
    $("#federalDistrictId").change(function () {
        fillFederalSubjectsLists($("#federalDistrictId").val());
    });
});

function fillFederalSubjectsLists(federalDistrictId) {
    $.get("/MinStat.AnalizeUI/Region/FederalSubjects", { federalDistrictId: federalDistrictId }, function (data) {
        $("#federalSubjectId").empty();
        $("#federalSubjectId").append($('<option value="0">Все субъекты</option>'));
        $.each(data, function (el) {
            $("#federalSubjectId").append($('<option value="' + el + '">' + data[el] + '</option>'));
        });
        $("#federalSubjectId [value='" + $('#selectedFederalSubject').val() + "']").attr("selected", "selected");
        if ($("#federalSubjectId").val() != null && $("#federalSubjectId").val() != undefined && $("#federalSubjectId").val() != 0) {
            fillEntepriseList($("#federalSubjectId").val());
        }
        $("#federalSubjectId").change(function () {
            fillEntepriseList($("#federalSubjectId").val());
        });
    });
}

function fillEntepriseList(federalSubjectId) {
    $.get("/MinStat.AnalizeUI/Region/Enterprises", { federalSubjectId: federalSubjectId }, function (datae) {
        $("#enterpriseId").empty();
        $("#enterpriseId").append($('<option value="0">Все предприятия</option>'));
        $.each(datae, function (el) {
            $("#enterpriseId").append($('<option value="' + el + '">' + datae[el] + '</option>'));
        });
        $("#enterpriseId [value='" + $('#selectedEnterprise').val() + "']").attr("selected", "selected");
    });
}

