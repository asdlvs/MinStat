$(document).ready(function () {
  if (getParameterByName('activityId') == '') {
    var sddlValue = $('#summaryDropDownList').val();
    if (sddlValue != undefined) {
      fillActivityDdl(sddlValue);
    }
  }

  $('#summaryDropDownList').change(function () {
    fillActivityDdl($('#summaryDropDownList').val());
  });
  $('#activityDropDownList').change(function () {
    location.replace('?summaryId=' + $('#summaryDropDownList').val() + '&activityId=' + $('#activityDropDownList').val());
  });
});

function fillActivityDdl(sddlValue) {
  $.get('/People/Activities', { summaryId: sddlValue }, function (data) {
    $('#activityDropDownList').html('');
    for (var i = 0; i < data.length; i++) {
      $('<option value=\'' + data[i].Value + '\'>' + data[i].Text + '</option>').appendTo('#activityDropDownList');
    }
    if ($('#activityDropDownList').val() != undefined) {
      location.replace('?summaryId=' + sddlValue + '&activityId=' + $('#activityDropDownList').val());
    }
  });
}

function getParameterByName(name) {
  name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
  var regexS = "[\\?&]" + name + "=([^&#]*)";
  var regex = new RegExp(regexS);
  var results = regex.exec(window.location.search);
  if (results == null)
    return "";
  else
    return decodeURIComponent(results[1].replace(/\+/g, " "));
}
