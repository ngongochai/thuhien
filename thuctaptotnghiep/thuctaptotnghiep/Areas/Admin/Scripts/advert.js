function savaadvert(html, nameadvert) {
    if (html.length <= 50) {
        swal({
            title: "Nhập Chính xác thông tin",
            text: "Vui Lòng Kiểm Tra Lại thông Tin",
            type: "error",
            timer: 1000,
            showConfirmButton: false

        });
    }
    else {
        $.ajax({
            url: "/Admin/Advert/" + nameadvert + "",
            type: "POST",
            dataType: "json",
            data: { "html": html },
            success: function (data) {
                if (data == 0) {
                    swal({
                        title: "Đã Xảy ra lỗi",
                        text: "Vui Lòng Kiểm Tra Lại và làm mới trang",
                        type: "error",
                        timer: 1000,
                        showConfirmButton: false

                    });
                }
                else {
                    swal({
                        title: "Lưu Thành công",
                        text: "Thay đổi đã được lưu",
                        type: "success",
                        timer: 500,
                        showConfirmButton: false

                    });
                }

            },
            error: function (data) {
                swal({
                    title: "Đã Xảy ra lỗi",
                    text: "Vui Lòng Kiểm Tra Lại và làm mới trang",
                    type: "error",
                    timer: 1000,
                    showConfirmButton: false

                });
                location.reload();
            }
        })
    }
}