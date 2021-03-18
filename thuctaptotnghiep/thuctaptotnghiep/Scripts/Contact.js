if ($('#city').val() != '0') {
    loadDistrict1();
}
$('#country').change(function () {
    if ($('#country').val() != '1' && $('#country').val() != '') {
        $('#city').select2('val', 0);
        $('#city').attr('disabled', 'disabled');
        $('#district').select2('val', '0');
        $('#district').attr('disabled', 'disabled');
    } else if ($('#country').val() == '') {
        $('#city').select2('val', '');
        $('#district').select2('val', '0');
        $('#district').attr('disabled', 'disabled');
        $('#city').removeAttr('disabled');
    } else {
        $('#city').select2('val', 0);
        $('#district').removeAttr('disabled');
        $('#city').removeAttr('disabled');
    }
});
function loadDistrict1() {
    $.ajax({
        type: "POST", url: "",
        data: {
            MaTP1: $('#city').val(),
            action: "load-district"
        },
        dataType: "json",
        success: function (data) {
            if (data.status == 'SUCCESS') {
                if (data.option != '') {
                    $('#district').removeAttr('disabled');
                    data.option += '<option value="0">Vui lòng chọn...</option>';
                    $('#district').html(data.option);
                    $('#district').select2(). select2('val', data.maqh);
                } else {
                                                        
                    $('#district').html("<option value='0'>Vui lòng chọn...</option>");
                    $('#district').select2('val', 0);
                    $('#district').attr('disabled', 'disabled');
                }
            }
        }
    });
}
function loadDistrict() {
    $.ajax({
        type: "POST", url: "",
        data: {
            MaTP1: $('#city').val(),
            action: "load-district"
        },
        dataType: "json",
        success: function (data) {
            if (data.status == 'SUCCESS') {
                if (data.option != '') {
                    $('#district').removeAttr('disabled');
                    data.option += '<option value="0">Vui lòng chọn...</option>';
                    $('#district').html(data.option);
                } else {
                                                        
                    $('#district').html("<option value='0'>Vui lòng chọn...</option>");
                    $('#district').select2('val', 0);
                    $('#district').attr('disabled', 'disabled');
                }
            }
        }
    });
}

$('#saveContactInforBtn').click(function () {
    $('#form-contact').submit();
});

$('#form-contact').on('submit', function (e) {
    var city = $("#city").select2('data');
    var district=$("#district").select2('data');
    var optionsContactInfoBlock = {
        beforeSubmit: showRequestContactForm,  // pre-submit callback
        success: showResponseContactForm,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: {Thanhpho:city.text,Quanhuyen:district.text, action: 'save-contact-info' }
    };
    e.preventDefault(); // <-- important
    $(this).ajaxSubmit(optionsContactInfoBlock);
    return false;
});
// pre-submit callback
function showRequestContactForm(formData, jqForm, options) {
    validFormContact = checkValidFormContact();
    if (validFormContact) {
        $('#saveContactInforBtn').css('display', 'none');
        $('#cancelContactInforBtn').css('display', 'none');
        $('#updateContactProcessBtn').css('display', '');
        return true;
    } else {
        return false;
    }
}
function showResponseContactForm(data) {
    var quocgia = $("#country").select2('data');
    var city = $("#city").select2('data');
    var district = $("#district").select2('data');
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $('#error-message-update-contact').slideDown();
        setTimeout(function () {
            $('#error-message-update-contact').slideUp();
        }, 5000);
    } else if (data.status == 'SUCCESS') {
        var $thisFormWrapper = $('#saveContactInforBtn').parents('.form-wrapper');
        mdlLiveEdit.saveContent($thisFormWrapper);
        //reloadProfileLevel();
        //updateCompletedStatus();
    }
    $("#country-view-control").html(quocgia.text);
    $("#district-view-control").html(district.text);
    $("#city-view-control").html(city.text);
    $('#updateContactProcessBtn').css('display', 'none');
    $('#cancelContactInforBtn').css('display', '');
    $('#saveContactInforBtn').css('display', '');
}
function checkValidFormContact() {
    var has_error = false;
    //$('#form-contact').find("span.error-message").attr('hidden', 'hidden');
    //$('#form-contact').find(".has-error").removeClass('has-error');

    var checkValidPhoneNumber = function (PhoneNumber) {
        var regexPattern = /([+-/(/)]*\d){7,}/;
        return PhoneNumber.search(regexPattern) != -1;
    };

    if (frm_ct.Ngaysinh.value=="") {
        $('#err_birthday').removeAttr('hidden').parent().addClass('has-error');
        has_error = true;
    }
    //if (!checkRequired(frm_ct.nationality)) {
    //    $('#err_nationality').removeAttr('hidden').parent().addClass('has-error');
    //    has_error = true;
    //}
    if (frm_ct.Quocgia.value==0) {
        $('#err_country').removeAttr('hidden').parent().addClass('has-error');
        has_error = true;
    }
    if (frm_ct.MaTP1.value==0) {
        $('#err_city').removeAttr('hidden').parent().addClass('has-error');
        has_error = true;
    }
    if (frm_ct.Dienthoai1.value=="") {
        $('#err_cellphone').removeAttr('hidden').parent().addClass('has-error');
        has_error = true;
    } else if (10>frm_ct.Dienthoai1.value.length>11 || !checkValidPhoneNumber($('#cell-number').val())) {
        $('#err_valid_cellphone').removeAttr('hidden').parent().addClass('has-error');
        has_error = true;
    }
    if (frm_ct.Diachihientai.value=="") {
        $('#err_address').removeAttr('hidden').parent().addClass('has-error');
        has_error = true;
    }
    if (!has_error) {
        return true;
    } else {
        return false;
    }
}