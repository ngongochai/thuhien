
function validation(data, form, options) {
    var error = true;
    $('#job_phone_error').css("display", "none");
    $('#job_dateex').css("display", "none");
    $('#job_job_title-error').css("display", "none");
    $('#job_level').css("display", "none");
    $('#job_category').css("display", "none");
    $('#job_city').css("display", "none");
    $("#job_minsalary").css("display", "none")
    $("#job_maxsalary").css("display", "none")
    $('#job_descr').css("display", "none");
    $('#job_requi').css("display", "none");
    $('#job_contact_name_error').css("display", "none");
    $('#job_email_for_application-error').css("display", "none");
    $('#job_address_error').css("display", "none");
    $('#job_skill_tag').css("display", "none");
    var skill = $('#tokenfield-1').val();
    var enddate = $("#endate").val().replace(/\//g, '-');
    var db = moment(enddate, "DD-MM-YYYY HH:mm:ss");
    var tryb = db.toDate();
    var today = new Date();
    if (isNaN(tryb)) {
        $('#job_dateex').css("display", "block").html("Vui lòng chọn ngày hết hạn cho công việc");
        error = false;
    }
    else
        if (tryb < today) {
            $('#job_dateex').css("display", "block").html("Ngày không hợp lệ");
            error = false;
        }
    if ($('#job_job_title').val().length < 5) {
        $('#job_job_title-error').css("display", "block");
        error = false;

    }
    if ($('#job_phone').val().length < 6) {
        $('#job_phone_error').css("display", "block");
        document.getElementById("jobPostContainer").scrollIntoView()
        error = false;
    }
    if ($('#job_job_level').val() == 0) {
        $('#job_level').css("display", "block");
        document.getElementById("jobPostContainer").scrollIntoView()
        error = false;
    }
    if ($('#job_job_categories').val() == 0 || $('#job_job_categories').val() == null) {
        $('#job_category').css("display", "block");
        document.getElementById("jobPostContainer").scrollIntoView()
        error = false;

    }
    if ($('#job_job_locations').val() == 0 || $('#job_job_locations').val() == null) {
        $('#job_city').css("display", "block");
        document.getElementById("jobPostContainer").scrollIntoView()
        error = false;
    }
    if (!document.getElementById('inlineCheckbox2').checked) {
        var maxluong = $("#job_maximum_salary").val();
        var minluong = $("#job_minimum_salary").val();
        if ($("#job_minimum_salary").val() == "" || parseInt(minluong) < 0) {
            $("#job_minsalary").css("display", "block")
            document.getElementById("jobPostContainer").scrollIntoView()
            error = false;
        }
        if ($("#job_maximum_salary").val() == "" || parseInt(maxluong) < 0) {
            $("#job_maxsalary").css("display", "block")
            document.getElementById("jobPostContainer").scrollIntoView()
            error = false;
        }

        if (parseInt(maxluong) < parseInt(minluong)) {
            $("#job_maxsalary").css("display", "block")
            document.getElementById("jobPostContainer").scrollIntoView()
            error = false;
        }
    }
    if ($('#job_job_description').val().length == 0) {
        $('#job_descr').css("display", "block");
        document.getElementById("jobPostContainer").scrollIntoView()
        error = false;
    }
    if ($('#job_job_requirements').val().length == 0) {
        $('#job_requi').css("display", "block");
        document.getElementById("jobPostContainer").scrollIntoView()
        error = false;
    }
    if ($("#job_contact_name").val() == "") {
        $('#job_contact_name_error').css("display", "block");
        document.getElementById("jobPostContainer").scrollIntoView()
        error = false;
    }
    var email_contact = $("#job_email_for_application").val();
    var checkemail_contact = validateEmail(email_contact);
    if (!checkemail_contact) {
        $('#job_email_for_application-error').css("display", "block");
        document.getElementById("jobPostContainer").scrollIntoView()
        error = false;
    }
    if ($("#job_address").val() == "") {
        $('#job_address_error').css("display", "block");
        document.getElementById("jobPostContainer").scrollIntoView()
        error = false;
    }
    if ($("#tokenfield-1").val() == "") {
        $('#job_skill_tag').css("display", "block");
        error = false;
    }
    if ($(".token").length > 3) {
        $('#job_skill_tag').css("display", "block");
        alert("Bạn chỉ được nhập tối đa 3 thẻ");
        error = false;
    }
    if (error == false) {
        swal({
            title: "Lỗi!",
            text: "Vui lòng Kiểm tra lại thông tin đã nhập!",
            type: "error",
            confirmButtonText: "OK"
        })
    }
    return error;
}
function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}
