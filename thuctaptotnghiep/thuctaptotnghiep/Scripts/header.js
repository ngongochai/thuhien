    $('#download-resume-button').click(function (e) {
        e.stopPropagation();
    });
    $('#upload-resume-button').click(function (e) {
        e.stopPropagation();
    });
var frm_header = document.registrationInfoForm;
$('.dropdown-remove-avatar-btn').click(function () {
    $('#dialog-remove-avatar').modal('show')
})
$('#saveHeaderInforBtn').click(function () {
    $('#registrationInfoForm').submit();
});
var optionsHeaderForm = {
    beforeSubmit: checkValidFormHeader,  // pre-submit callback
    success: showResponseHeaderForm,  // post-submit callback
    type: 'post',        // 'get' or 'post', override for form's 'method' attribute
    dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
    data: {action: 'save-header-info'}
};
$('#registrationInfoForm').on('submit', function (e) {
    e.preventDefault(); // <-- important
    $(this).ajaxSubmit(optionsHeaderForm);
    return false;
});
function showResponseHeaderForm(data) {
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $('#error-message-update-header').slideDown();
        setTimeout(function () {
            $('#error-message-update-header').slideUp();
        }, 5000);
    } else if (data.status == 'SUCCESS') {
        var $thisFormWrapper = $('#saveHeaderInforBtn').parents('.form-wrapper');
        mdlLiveEdit.saveContent($thisFormWrapper);
        //reloadProfileLevel();
        updateCompletedStatus();
    }
    $('#saveHeaderInforBtn').css('display', '');
    $('#cancelHeaderInforBtn').css('display', '');
    $('#updateHeaderProcessBtn').css('display', 'none');
}
function checkValidFormHeader(data, form, options) {
    var has_error = false;
    $('#registrationInfoForm').find("span.error-message").attr('hidden', 'hidden');
    $('#registrationInfoForm').find('.has-error').removeClass('has-error');
    $.each(data, function (index, item) {
        switch (item.name) {
            case 'Hovatendem':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $('#errLastName').removeAttr('hidden').parent().addClass('has-error');
                } else if (emailExistsInString(item.value) || phoneExistsInString(item.value)) {
                    has_error = true;
                    $('#errEmailInLastName').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'Ten':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $('#errFirstName').removeAttr('hidden').parent().addClass('has-error');
                } else if (emailExistsInString(item.value) || phoneExistsInString(item.value)) {
                    has_error = true;
                    $('#errEmailInFirstName').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'Namkinhnghiem':
                if ($.trim(item.value) == '') {
                    if (!$('#no-experience-check-box').is(':checked')) {
                        has_error = true;
                        $('#errYearOfExperience').removeAttr('hidden').parent().addClass('has-error');
                    }
                } else if ( item.value < 0 || item.value > 99) {
                    has_error = true;
                    $('#errYearOfExperience').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'Chucdanh':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $('#errExpectedPosition').removeAttr('hidden').parent().addClass('has-error');
                } else if (emailExistsInString(item.value) || phoneExistsInString(item.value)) {
                    has_error = true;
                    $('#errEmailInExpectedPosition').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
        }
    });
    if (!has_error) {
        $('#saveHeaderInforBtn').css('display', 'none');
        $('#cancelHeaderInforBtn').css('display', 'none');
        $('#updateHeaderProcessBtn').css('display', '');
    }
    return !has_error;
}
function deleteResume() {
    $('#dialog-delete-resume').modal('hide');
    var options = {
        success: postSimpleAjaxSubmit,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: {action: 'delete-resume'} // extra data
    };
    $('#deleteResumeConfirmForm').ajaxSubmit(options);
}
function postSimpleAjaxSubmit(data) {
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $('#errorMessageHeader').slideDown();
        setTimeout(function () {
            $('#errorMessageHeader').slideUp();
        }, 5000);
    } else {

    }
}
function updateSearchable() {
    var options = {
        success: postSimpleAjaxSubmit,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: {action: 'change-searchable'} // extra data
    };
    $('#publicResumeForm').ajaxSubmit(options);
}

function postPublishResumeAjaxSubmit(data) {
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $('#errorMessageHeader').slideDown();
        setTimeout(function () {
            $('#errorMessageHeader').slideUp();
        }, 5000);
    } else {
        $('#btnPublishResume').addClass('disabled').attr('hidden', 'hidden');
        $('.lbl-status-completed').attr('hidden', 'hidden');
        $('.lbl-status-in-progress').removeAttr('hidden');
    }
}
function publishResume() {
    var options = {
        success: postPublishResumeAjaxSubmit,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: {action: 'publish-resume'} // extra data
    };
    $('#publicResumeForm').ajaxSubmit(options);
}
function removeAvatarResume() {
    $('#dialog-remove-avatar').modal('hide');
    var options = {
        success: postRemoveAvatarAjaxSubmit,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: {action: 'remove-avatar'} // extra data
    };
    $('#removeAvatarForm').ajaxSubmit(options);
}
function postRemoveAvatarAjaxSubmit(data) {
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $('#errorMessageHeader').slideDown();
        setTimeout(function () {
            $('#errorMessageHeader').slideUp();
        }, 5000);
    } else if (data.status == 'SUCCESS') {
        removeAvatar();
        //reloadProfileLevel();
    }
}
$(function () {
    $('#avatar-upload').on('change', function () {
        $('#avatar-menu').removeClass('open');
        ajaxUploadAvatar(this);
    });
});

function ajaxUploadAvatar(uploader) {
    var avatarFile = $(uploader).val();
    var avatarFileNameParts = avatarFile.split(".");
    var avatarFileExt = avatarFileNameParts[avatarFileNameParts.length - 1].toLowerCase();
    if (avatarFileExt == 'gif' || avatarFileExt == 'jpg' || avatarFileExt == 'png') {
        var options = {
            beforeSubmit: function () {
                $('#avatar-menu').removeClass('open');
            },
            success: postUploadAvatarAjaxSubmit,  // post-submit callback
            error: function (xhr, status, error) {
                handleError(xhr);
            },
            type: 'post',        // 'get' or 'post', override for form's 'method' attribute
            dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
            data: {action: 'upload-avatar'} // extra data
        };
        $('#uploadAvatarForm').ajaxSubmit(options);
    } else {
        $('#avatar-upload').val('');
        $('#errUploadAvatar').show();
        setTimeout(function () {
            $('#errUploadAvatar').hide();
        }, 5000);
    }
}
function handleError(xhr) {
    $('#avatar-upload').val('');
    if (xhr.status == 413) {
        $('#errUploadAvatar').show();
        setTimeout(function () {
            $('#errUploadAvatar').hide();
        }, 5000);
    } else {
        $('#errRequiredAvatar').show();
        setTimeout(function () {
            $('#errRequiredAvatar').hide();
        }, 5000);
    }
}

function postUploadAvatarAjaxSubmit(data) {
    var avatarElement = jQuery('#errUploadAvatar');
    avatarElement.hide();

    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $('#errorMessageHeader').slideDown();
        avatarElement.show();
        setTimeout(function () {
            $('#errorMessageHeader').slideUp();
            avatarElement.hide();
        }, 5000);
    } else if (data.status == 'SUCCESS') {
        if (data.data.action == 'none') {
            $('#img-user-pic').removeClass('hidden');
            $('#img-user-pic').find('img').attr("src", data.data.img_url + '?lastmod=' + Math.floor(Math.random() * 99999));
            $('#default-user-pic').addClass('hidden');
            $('#remove-avatar-menu').removeClass('hidden');
        } else if (data.data.action == 'crop') {
            $('#crop-preview').attr("src", data.data.img_url + '?lastmod=' + Math.floor(Math.random() * 99999));
            $('#target').attr("src", data.data.img_url + '?lastmod=' + Math.floor(Math.random() * 99999));
            $('#target').data('original-data', data.data.img_url);
            $('#cropAvatarHash').val(data.data.hash);
            $('#change-photo-modal').modal('show').on('shown.bs.modal', function () {
                // Create variables (in this scope) to hold the API and image size
                var jcrop_api,
                        boundx,
                        boundy,
                // Grab some information about the preview pane
                        $pcnt = $('#preview-pane').find('.preview-container'),
                        $pimg = $pcnt.find('img'),

                        select_x = Math.floor((data.data.img_info.width - data.data.img_info.w) / 2),
                        select_y = Math.floor((data.data.img_info.height - data.data.img_info.h) / 2);
                $('#target').Jcrop({
                    //aspectRatio: data.data.img_info.w/data.data.img_info.h,
                    aspectRatio: 1,
                    onChange: updatePreview,
                    onSelect: updatePreview,
                    setSelect: [select_x, select_y, select_x + data.data.img_info.w, select_y + data.data.img_info.h]
                }, function () {
                    // Use the API to get the real image size
                    var bounds = this.getBounds();
                    boundx = bounds[0];
                    boundy = bounds[1];
                    // Store the API in the jcrop_api variable
                    window.jcrop_api = this;
                });

                function updatePreview(c) {
                    if (parseInt(c.w) > 0) {
                        var rx = data.data.img_info.w / c.w;
                        var ry = data.data.img_info.h / c.h;

                        $pimg.css({
                            width: Math.round(rx * boundx) + 'px',
                            height: Math.round(ry * boundy) + 'px',
                            marginLeft: '-' + Math.round(rx * c.x) + 'px',
                            marginTop: '-' + Math.round(ry * c.y) + 'px'
                        });
                    }
                }
            });
        }
        ////reloadProfileLevel();
    }
}
function cropAvatar() {
    var selection = window.jcrop_api.tellSelect(),
            x = selection.x,
            y = selection.y,
            w = Math.round(selection.w),
            h = Math.round(selection.h),
            a = $('#target').data('original-data');

    var options = {
        success: postCropAvatarAjaxSubmit,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: {action: 'crop-avatar', x: x, y: y, width: w, height: h, asset: a} // extra data
    };
    $('#cropAvatarForm').ajaxSubmit(options);
    if (window.jcrop_api) {
        window.jcrop_api.destroy();
    }
}
function postCropAvatarAjaxSubmit(data) {
    $('#avatar-upload').val('');
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $('#errorMessageHeader').slideDown();
        setTimeout(function () {
            $('#errorMessageHeader').slideUp();
        }, 5000);
    } else if (data.status == 'SUCCESS') {
        $('#img-user-pic').removeClass('hidden');
        $('#img-user-pic').html('<img src="' + data.data.img_url + '?lastmod=' + Math.floor(Math.random() * 99999) + '" class="img-responsive img-circle"/>');
        //$('#img-user-pic').find('img').attr("src", data.data.img_url + '?lastmod=' + Math.floor(Math.random() * 99999));
        $('#default-user-pic').addClass('hidden');
        $('#remove-avatar-menu').removeClass('hidden');
        $('#change-photo-modal').modal('hide');
        $('.dropdown-remove-avatar-btn').closest('li').show().prev('.divider').show();
        //reloadProfileLevel();
    }
}
function cancelCrop() {
    if (window.jcrop_api) {
        window.jcrop_api.destroy();
    }
}

function postCheckCompletedAjaxSubmit(data) {
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $('#errorMessageHeader').slideDown();
        setTimeout(function () {
            $('#errorMessageHeader').slideUp();
        }, 5000);
    } else { // SUCCESS
        if (data.data.is_completed) {
            $('#btnPublishResume').removeClass('btn-default')
                    .removeClass('disabled')
                    .addClass('btn-primary');
            $('.lbl-status-incompleted').attr('hidden', 'hidden');
            $('.lbl-status-completed').removeAttr('hidden');
        } else {
            $('#btnPublishResume').removeClass('btn-primary')
                    .addClass('btn-default')
                    .addClass('disabled');
            $('.lbl-status-completed').attr('hidden', 'hidden');
            $('.lbl-status-incompleted').removeAttr('hidden');
        }
    }
}
function updateCompletedStatus() {
    var isPublished = false;
    if (isPublished) {
        // do nothing
    } else {
        var options = {
            success: postCheckCompletedAjaxSubmit,  // post-submit callback
            type: 'get',        // 'get' or 'post', override for form's 'method' attribute
            dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
            data: {action: 'check-completed'} // extra data
        };
        $('#publicResumeForm').ajaxSubmit(options);
    }
}