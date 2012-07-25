$(document).ready(function () {
  $('.editperson').click(function () {
    var td = $(this).children(".init");

    $('#persontoedit #Id').val(td.children("#id").val());
    $('#persontoedit #Title').val(td.children("#title").val());
    $('#persontoedit #Gender').val(td.children("#gender").val());
    $('#persontoedit #BirthYear').val(td.children("#birthyear").val());
    $('#persontoedit #ActivityId').val(td.children("#activityId").val());
    $('#persontoedit #HiringYear').val(td.children("#hiringyear").val());
    $('#persontoedit #Post').val(td.children("#post").val());
    $('#persontoedit #StartPostYear').val(td.children("#startpostyear").val());
    $('#persontoedit #EducationLevelId').val(td.children("#educationlevelid").val());
    $('#persontoedit #WasQualificationIncrease').val(td.children("#wasqualificationincrease").val());
    $('#persontoedit #PostLevelId').val(td.children("#postlevelid").val());
    $('#persontoedit #WasValidate').val(td.children("#wasvalidate").val());
    $('#persontoedit #YearSalary').val(td.children("#yearsalary").val());
    $('#persontoedit #DismissalYear').val(td.children("#dismissalyear").val());

    $('#editPerson').modal('show');
  });
});