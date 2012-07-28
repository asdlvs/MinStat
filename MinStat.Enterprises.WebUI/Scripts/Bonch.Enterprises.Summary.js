$(document).ready(function () {
    $('div[name^="activityModal"] div[name ^= "emptytitleerror"]').css('display', 'none');
    $('div[name^="activityModal"] input[name^="savesummary"]').click(function () {
        var title = $(this).closest('*[name ^= "activityModal"]').children('.modal-body').children('input').val();
        if (title == undefined || title == null || title.replace(/(^\s+)|(\s+$)/g, "") == "") {
            $(this).closest('*[name ^= "activityModal"]').children('.modal-body').children('*[name ^= "emptytitleerror"]').css('display', 'block');
            return false;
        }
    });
    $('.uploadcsv').click(function () {
        $('#uploadcsvmodal').modal('show');
    });

    $('.activityModalPublish').click(function () {
        var summaryId = $(this).attr('data-value');
        $('#activityModalPublish #summaryId').val(summaryId);
    });

    $('.changeactivities').click(function () {
        var summaryId = $(this).attr('summary-id');
        $('#activityModal0 #summaryId').val(summaryId);
        var summaryTitle = $(this).attr('summary-title');
        $('#activityModal0 #Title').val(summaryTitle);
        if (summaryId != 0 && summaryId != undefined && summaryId != null && summaryId.replace(/(^\s+)|(\s+$)/g, "") != "") {
            $.get('Summaries/Activities', { summaryId: summaryId }, function (activities) {
                $.each(activities, function (el) {
                    $('#Activity' + activities[el].Id).attr("checked", "checked");
                });
            });
        } else {
            $('input[id ^= "Activity"]').removeAttr("checked");
        }
        $('#activityModal0').modal('show');
    });
});