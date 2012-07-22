$(document).ready(function () {
    $('a[data-toggle="tab"]').on('shown', function (e) {
        $('#reportType').val(e.target);
    });
    $('#createReport').click(function () {
        $('form').submit();
    });
});