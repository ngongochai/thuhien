$(document).ready(function () {
    $("#applyuv").click(function () {
        $('#myModal1').modal('show');
    })
    $('#confirmuv').click(function () {
        if ($("#notes").val() == "") {
            $("#note_error").css("display", "block");
            return false;
        }
        else {
            var mess = $("#notes").val();
            mess = mess.replace(/\r?\n/g, '<br />');
            $("#notes").val(mess);
            return true;
        }
    })
    $('.change_status').on('change', function (e) {
        var status = $(this).val();
        var input = $(this).attr("id");
        var id = input.replace(/\D+/g, '');

        $.ajax({
            url: "/ManagerJob/ChangeStatus",
            type: "POST",
            dataType: "json",
            data: { "status": status, "madk": id },
            success: function (data) {
                if (data == 1) {
                    swal({
                        title: "Đã Gửi Mail!",
                        text: "Chúng tôi đã gửi một email thông báo đến ứng viên!",
                        type: "success",
                        confirmButtonText: "OK"
                    },
               function (isConfirm) {
                   if (isConfirm) {
                       location.reload();
                   }
               });
                }
                else if (data == 0) {
                    swal({
                        title: "Lỗi!",
                        text: "Đã xảy ra lỗi!",
                        type: "error",
                        confirmButtonText: "OK"
                    },
               function (isConfirm) {
                   if (isConfirm) {
                       location.reload();
                   }
               });
                }
                else if (data == 3) {
                    location.reload();
                }
            },
            error: function (error) {
                swal({
                    title: "Lỗi!",
                    text: "Đã xảy ra lỗi!",
                    type: "error",
                    confirmButtonText: "OK"
                },
            function (isConfirm) {
                if (isConfirm) {
                    location.reload();
                }
});
            }
        })
    });
    $('.action_save').on('change', function (e) {
        var status = $(this).val();
        var input = $(this).attr("id");
        var id = input.replace(/\D+/g, '');

        $.ajax({
            url: "/ManagerJob/ChangeStatusSave",
            type: "POST",
            dataType: "json",
            data: { "status": status, "mahs": id },
            success: function (data) {
                if (data == 1) {
                    swal({
                        title: "Đã Gửi Mail!",
                        text: "Chúng tôi đã gửi một email thông báo đến ứng viên!",
                        type: "success",
                        confirmButtonText: "OK"
                    },
               function (isConfirm) {
                   if (isConfirm) {
                       location.reload();
                   }
               });
                }
                else if (data == 0) {
                    swal({
                        title: "Lỗi!",
                        text: "Đã xảy ra lỗi!",
                        type: "error",
                        confirmButtonText: "OK"
                    },
                function (isConfirm) {
                    if (isConfirm) {
                        location.reload();
                    }
                });
                }
                else if (data == 3) {
                    location.reload();
                }
            },
            error: function (error) {
                swal({
                    title: "Lỗi!",
                    text: "Đã xảy ra lỗi!",
                    type: "error",
                    confirmButtonText: "OK"
                },
            function (isConfirm) {
                if (isConfirm) {
                    location.reload();
                }
            });
            }
        })
    });
})