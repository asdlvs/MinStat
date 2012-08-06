$(document).ready(function () {
    $('input[data-checkboxlevel="onezero"]').change(function () {
        if ($(this).attr('checked') == 'checked') {
            $(this).parent().parent().nextUntil('tr.strong1').children('td.withcheckbox').children('input:checkbox').attr('checked', 'checked');
            var prevOneZero = $(this).parent().parent().prevUntil('tr.strong2', 'tr.strong1').children('td.withcheckbox').children('input:checkbox[checked != checked]');
            var nextOneZero = $(this).parent().parent().nextUntil('tr.strong2', 'tr.strong1').children('td.withcheckbox').children('input:checkbox[checked != checked]');
            if (prevOneZero.length + nextOneZero.length == 0) {
                $(this).parent().parent().prevAll('tr.strong2:first').children('td.withcheckbox').children('input:checkbox').attr('checked', 'checked');
            }
        } else {
            $(this).parent().parent().nextUntil('tr.strong1').children('td.withcheckbox').children('input:checkbox').removeAttr("checked");
            $(this).parent().parent().prevAll('tr.strong2:first').children('td.withcheckbox').children('input:checkbox').removeAttr("checked");
        }
    });

    $('input[data-checkboxlevel="twozeros"]').change(function () {
        if ($(this).attr('checked') == 'checked') {
            $(this).parent().parent().nextUntil('tr.strong2').children('td.withcheckbox').children('input:checkbox').attr('checked', 'checked');
        } else {
            $(this).parent().parent().nextUntil('tr.strong2').children('td.withcheckbox').children('input:checkbox').removeAttr("checked");
        }
    });

    $('input[data-checkboxlevel="actual"]').change(function () {
        if ($(this).attr('checked') != 'checked') {
            $(this).parent().parent().prevAll('tr.strong2:first').children('td.withcheckbox').children('input:checkbox').removeAttr("checked");
            $(this).parent().parent().prevAll('tr.strong1:first').children('td.withcheckbox').children('input:checkbox').removeAttr("checked");
        } else {
            var prevActual = $(this).parent().parent().prevUntil('tr.strong1', 'tr').children('td.withcheckbox').children('input:checkbox[checked != checked]');
            var nextActual = $(this).parent().parent().nextUntil('tr.strong1', 'tr').children('td.withcheckbox').children('input:checkbox[checked != checked]');
            if (prevActual.length + nextActual.length == 0) {
                $(this).parent().parent().prevAll('tr.strong1:first').children('td.withcheckbox').children('input:checkbox').attr('checked', 'checked');
                var prevOneZero = $(this).parent().parent().prevUntil('tr.strong2', 'tr.strong1').children('td.withcheckbox').children('input:checkbox[checked != checked]');
                var nextOneZero = $(this).parent().parent().nextUntil('tr.strong2', 'tr.strong1').children('td.withcheckbox').children('input:checkbox[checked != checked]');
                if (prevOneZero.length + nextOneZero.length == 0) {
                    $(this).parent().parent().prevAll('tr.strong2:first').children('td.withcheckbox').children('input:checkbox').attr('checked', 'checked');
                }
            }
        }
    });
})