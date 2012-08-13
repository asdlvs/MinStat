$(document).ready(function () {
    $.validator.methods.number = function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
    }; 



    $('#addpersonbutton').click(function () {
        $('#deletebutton').css('display', 'none');
        $('#persontoedit #Id').val(0);
        $('#persontoedit #Title').val('');
        $('#persontoedit #Gender').val(false);
        $('#persontoedit #BirthYear').val('');
        $('#persontoedit #ActivityId').val('');
        $('#persontoedit #HiringYear').val('');
        $('#persontoedit #Post').val('');
        $('#persontoedit #StartPostYear').val('');
        $('#persontoedit #EducationLevelId').val('');
        $('#persontoedit #WasQualificationIncrease').val(false);
        $('#persontoedit #PostLevelId').val('');
        $('#persontoedit #WasValidate').val(false);
        $('#persontoedit #YearSalary').val('');
        $('#persontoedit #DismissalYear').val('');
        $('#editPerson').modal('show');
    });

    $('.editperson').click(function () {
        $('#deletebutton').css('display', 'block');
        var td = $(this).children(".init");

        $('#persontoedit #Id').val(td.children("#id").val());
        $('#persontoedit #Title').val(td.children("#title").val());
        if (td.children("#gender").val() == "True") {
            $('#persontoedit #Gender option:nth-child(1)').attr('selected', 'selected');
        } else {
            $('#persontoedit #Gender option:nth-child(2)').attr('selected', 'selected');
        }
        $('#persontoedit #BirthYear').val(td.children("#birthyear").val());
        $('#persontoedit #ActivityId').val(td.children("#activityId").val());
        $('#persontoedit #HiringYear').val(td.children("#hiringyear").val());
        $('#persontoedit #Post').val(td.children("#post").val());
        $('#persontoedit #StartPostYear').val(td.children("#startpostyear").val());
        $('#persontoedit #EducationLevelId').val(td.children("#educationlevelid").val());
        if (td.children("#wasqualificationincrease").val() == "True") {
            $('#persontoedit #WasQualificationIncrease').attr("checked", "checked");
        }
        $('#persontoedit #PostLevelId').val(td.children("#postlevelid").val());
        if (td.children("#wasvalidate").val() == "True") {
            $('#persontoedit #WasValidate').attr("checked", "checked");
        }
        $('#persontoedit #YearSalary').val(td.children("#yearsalary").val());
        $('#persontoedit #DismissalYear').val(td.children("#dismissalyear").val());

        $('#editPerson').modal('show');
    });
});