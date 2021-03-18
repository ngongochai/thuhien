$(document).ready(function () {
    $('.applyjob').click(function () {
        var url = $(this).data('url');
        var returnurl = $(this).data('bind');
       var val= $("#jobid").val();
        $.ajax({
            url: "/Job/CheckUser",
            type: "POST",
            dataType: "json",
            data:{"id":val},
            success: function (data) {
                if (data.status == 1) {
                    $('#myModal1').modal('show');
                    $("#email").val(data.email);
                    $("#phone").val(data.phone);
                }
                else if (data.status == 2) {
                    $('#myModal1').modal('show');
                    $("#exampleModalLabel").html("Việc làm này đã có trong danh sách của bạn");
                    $(".tontai").html("<h2>Vui lòng chọn công việc tương tự để nộp đơn ứng tuyển</h2>");
                    $("#footer").css("display", "block");
                }else if (data.status == 3) {
                    window.location.href = "/";
                }
                else {
                    window.location.href = url+"?ReturnUrl="+returnurl;

                }
                
            }
        })
    })
    $("#confirmjob").click(function () {
        if ($("#phone").val() == "") {
            $("#phone_error").css("display", "block");
            return false;
        }
        if ($("#email").val() == "") {
            $("#email_error").css("display", "block");
            return false;
        }
        else {
            $("#applyjob").submit(function (event) {

                return true;

            })
        }
    })
    $("#listapplyjob").click(function () {
        location.replace("/Member/ListApplyJob");
    })
})