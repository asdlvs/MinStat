$(document).ready(function () {
    $('div[name^="activityModal"] div[name ^= "emptytitleerror"]').css('display', 'none');
    $('div[name^="activityModal"] input[name^="savesummary"]').click(function () {
        var title = $(this).closest('*[name ^= "activityModal"]').children('.modal-body').children('input').val();
        if (title == undefined || title == null || title.replace(/(^\s+)|(\s+$)/g, "") == "") {
            $(this).closest('*[name ^= "activityModal"]').children('.modal-body').children('*[name ^= "emptytitleerror"]').css('display', 'block');
            return false;
        }
    });
});