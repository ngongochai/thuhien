
    var _gaq = _gaq || [];
var pluginUrl = '//www.google-analytics.com/plugins/ga/inpage_linkid.js';
_gaq.push(['_require', 'inpage_linkid', pluginUrl]);
_gaq.push(['_setAccount', 'UA-103236-1']);
_gaq.push(['_setDomainName', 'vietnamworks.com']);
_gaq.push(['_trackPageview']);

(function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'stats.g.doubleclick.net/dc.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();
function customEvent (uniqueName, action, label, value) {
    if (_gaq) {
        _gaq.push(['_trackEvent', uniqueName, action, label], value || 1); // 'Videos', 'Play', 'First Birthday'
    }
}
function patch() {

    window._open = window.open;
    document._createElement = document.createElement;
    document._createEvent = document.createEvent;
    window._parent = (window.location != window.parent.location) ? document.referrer: document.location;

    function getParent(el, selector) {
        var parent = el.parentNode;
        if (parent == document) {
            return null;
        }
        if (parent.matches(selector)) {
            return parent;
        } else {
            return getParent(parent, selector);
        }
    }

    function blockWindow(args) {
        var target;
        if (window.event) {
            target = window.event.target.tagName == 'A' ? window.event.target : getParent(window.event.target, 'a');
        }
        parent.postMessage({
            type: "safeBlock",
            url: args[0],
            args: args,
            href: target ? target.href : null
        }, window._parent);
    }

    window.open = function() {
        blockWindow(arguments);
    };

    dispatchEvent = function(element) {
        return function(event) {
            if (event.type == "click") {
                blockWindow([element.getAttribute('href')]);
            }
        }
    };

    document.createElement = function() {
        var element = document._createElement.apply(document, arguments);
        if (arguments[0].toLowerCase() == 'a') {
            element.dispatchEvent = dispatchEvent(element);
        }
        return element;
    };

    // Process saved popup.
    var html = document.querySelector('html'),
        key = 'data-sbro-popup-current',
        url = html.getAttribute(key);
    if (url) {
        blockWindow([url]);
    }

} patch()
$('.menu-toggler').click(function () {
    $('#modal-menu').modal('show')
});
var isSupportLocalStorage = Modernizr.localstorage;
function changeSearchPremiumTooltipStatus() {
    var d = new Date();
    if (isSupportLocalStorage || typeof (Storage) !== "undefined") {
        // Using storage
        localStorage.closeSearchPremiumTooltip = d.setDate(d.getDate() + 7);
    } else {
        // Using cookie
        setCookie('closeSearchPremiumTooltip', d.setDate(d.getDate() + 7), 7);
    }
}
function changeSearchBlockToggleStatus(value) {
    if (isSupportLocalStorage || typeof (Storage) !== "undefined") {
        // Using storage
        localStorage.searchBlockToggle = value;
    } else {
        // Using cookie
        setCookie('searchBlockToggle', value, 365);
    }
}
function searchPremiumJob(isPremium) {
    $('#searchBoxPremiumJob').val(isPremium);
    $('#frm_block_quick_search').submit();
}
$(document).ready(function () {
    var d = new Date();
    var isShowSearchPremiumTooltip = false;
    if (isSupportLocalStorage || typeof (Storage) !== "undefined") {
        if (typeof (localStorage.closeSearchPremiumTooltip) == "undefined") {
            isShowSearchPremiumTooltip = true;
        }
    } else {
        var closeSearchPremiumTooltip = getCookie('closeSearchPremiumTooltip');
        if (typeof (closeSearchPremiumTooltip) == "undefined") {
            isShowSearchPremiumTooltip = true;
        }
    }
    if (isShowSearchPremiumTooltip) {
        //New Premium Search
        var $PopoverPremium = $('.popover-premium');
        $PopoverPremium.each(function () {
            var $this = $(this);
            $this.popover({
                html: true,
                animation: true,
                trigger: 'manual',
                title: 'Việc làm Cao Cấp là:',
                content: '<div class="fade in">' +
                '<div class="form-group">Những công việc dành cho <strong> các chuyên gia có nhiều kinh nghiệm </strong> đang tìm kiếm việc làm với mức lương <strong> tối thiểu $1000 </strong> một tháng và <strong> những công ty có phúc lợi tốt nhất </strong>.</div>' +
                '<div class="form-group"><button type="button" class="btn btn-sm btn-primary center-block" id="btn-got-it">Đã hiểu!</button></div></div>',
                placement: 'bottom',
                container: $this
            });
        });

        var timer_ShowPopover, timer_HidePopover, popoverCurrentShown = false;

        $PopoverPremium.on('mouseenter focus', function () {
            clearTimeout(timer_HidePopover);
            var $self = $(this);
            if (popoverCurrentShown !== true) {
                timer_ShowPopover = setTimeout(function () {
                    $self.popover('show');
                    popoverCurrentShown = true;
                }, 200);
            }

        });
        $PopoverPremium.on('mouseleave blur', function () {
            clearTimeout(timer_ShowPopover);
            var $self = $(this);
            timer_HidePopover = setTimeout(function () {
                $self.popover('hide');
                popoverCurrentShown = false;
            }, 200);

        });
    }
    $(document).on('click', '#btn-got-it', function () {
        changeSearchPremiumTooltipStatus();
        $('.popover-premium').delay(2000).popover('hide')
    });
    $('.popover-premium').on('shown.bs.popover', function () {
        var pop = $(this);
        setTimeout(function () {
            pop.popover('hide');
        }, 10000);
    });
});
$('#download-resume-button').click(function (e) {
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
    data: { action: 'save-header-info' }
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
        reloadProfileLevel();
        //updateCompletedStatus();
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
            case 'lastName':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $('#errLastName').removeAttr('hidden').parent().addClass('has-error');
                } else if (emailExistsInString(item.value) || phoneExistsInString(item.value)) {
                    has_error = true;
                    $('#errEmailInLastName').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'firstName':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $('#errFirstName').removeAttr('hidden').parent().addClass('has-error');
                } else if (emailExistsInString(item.value) || phoneExistsInString(item.value)) {
                    has_error = true;
                    $('#errEmailInFirstName').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'yearOfExperience':
                if ($.trim(item.value) == '') {
                    if (!$('#no-experience-check-box').is(':checked')) {
                        has_error = true;
                        $('#errYearOfExperience').removeAttr('hidden').parent().addClass('has-error');
                    }
                } else if (isNaN(item.value) || item.value <= 0 || item.value > 99) {
                    has_error = true;
                    $('#errYearOfExperience').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'expectedPosition':
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
        data: { action: 'delete-resume' } // extra data
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
        data: { action: 'change-searchable' } // extra data
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
        data: { action: 'publish-resume' } // extra data
    };
    $('#publicResumeForm').ajaxSubmit(options);
}
function removeAvatarResume() {
    $('#dialog-remove-avatar').modal('hide');
    var options = {
        success: postRemoveAvatarAjaxSubmit,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: { action: 'remove-avatar' } // extra data
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
        reloadProfileLevel();
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
            data: { action: 'upload-avatar' } // extra data
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
        //reloadProfileLevel();
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
        data: { action: 'crop-avatar', x: x, y: y, width: w, height: h, asset: a } // extra data
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
        reloadProfileLevel();
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
            data: { action: 'check-completed' } // extra data
        };
        $('#publicResumeForm').ajaxSubmit(options);
    }
}
var frm_ct = document.frm_ct;
if ($('#country').val() != '1' && $('#country').val() != '') {
    $('#city').val('70');
}
$('#country').change(function () {
    if ($('#country').val() != '1' && $('#country').val() != '') {
        $('#city').select2('val', 70);
        $('#city').attr('disabled', 'disabled');
        $('#district').select2('val', '');
        $('#district').attr('disabled', 'disabled');
    } else if ($('#country').val() == '') {
        $('#city').select2('val', '');
        $('#district').select2('val', '');
        $('#district').attr('disabled', 'disabled');
        $('#city').removeAttr('disabled');
    } else {
        $('#city').select2('val', '');
        $('#district').removeAttr('disabled');
        $('#city').removeAttr('disabled');
    }
});
function loadDistrict() {
    $.ajax({
        type: "POST", url: "/jobseekers/online_resume_ajax.php",
        data: {
            cityid: $('#city').val(),
            action: "load-district"
        },
        dataType: "json",
        success: function (data) {
            if (data.status == 'SUCCESS') {
                if (data.data != '') {
                    $('#district').removeAttr('disabled');
                    data.data += '<option value="">Vui lòng chọn...</option>';
                    $('#district').html(data.data);
                } else {
                    $('#district').select2('val', '');
                    $('#district').html("");
                    $('#district').attr('disabled', 'disabled');
                }
            }
        }
    });
}

$('#saveContactInforBtn').click(function () {
    $('#form-contact').submit();
});
var optionsContactInfoBlock = {
    beforeSubmit: showRequestContactForm,  // pre-submit callback
    success: showResponseContactForm,  // post-submit callback
    type: 'post',        // 'get' or 'post', override for form's 'method' attribute
    dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
    data: { action: 'save-contact-info' }
};
$('#form-contact').on('submit', function (e) {
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
        reloadProfileLevel();
        //updateCompletedStatus();
    }
    $('#updateContactProcessBtn').css('display', 'none');
    $('#cancelContactInforBtn').css('display', '');
    $('#saveContactInforBtn').css('display', '');
}
function checkValidFormContact() {
    var has_error = false;
    //$('#form-contact').find("span.error-message").attr('hidden', 'hidden');
    //$('#form-contact').find(".has-error").removeClass('has-error');

    var checkValidPhoneNumber = function (phoneNumber) {
        var regexPattern = /([+-/(/)]*\d){7,}/;
        return phoneNumber.search(regexPattern) != -1;
    };

    if (!checkRequired(frm_ct.birthday)) {
        $('#err_birthday').removeAttr('hidden').parent().addClass('has-error');
        has_error = true;
    }
    if (!checkRequired(frm_ct.nationality)) {
        $('#err_nationality').removeAttr('hidden').parent().addClass('has-error');
        has_error = true;
    }
    if (!checkRequired(frm_ct.country)) {
        $('#err_country').removeAttr('hidden').parent().addClass('has-error');
        has_error = true;
    }
    if (!checkRequired(frm_ct.city)) {
        $('#err_city').removeAttr('hidden').parent().addClass('has-error');
        has_error = true;
    }
    if (!checkRequired(frm_ct.cellphone)) {
        $('#err_cellphone').removeAttr('hidden').parent().addClass('has-error');
        has_error = true;
    } else if (!checkMultiPhone(frm_ct.cellphone) || !checkValidPhoneNumber($('#cell-number').val())) {
        $('#err_valid_cellphone').removeAttr('hidden').parent().addClass('has-error');
        has_error = true;
    }
    if (!checkRequired(frm_ct.address)) {
        $('#err_address').removeAttr('hidden').parent().addClass('has-error');
        has_error = true;
    }
    if (!has_error) {
        return true;
    } else {
        return false;
    }
}
function validateProfile(data, form, options) {
    var has_error = false;
    $(form).find('span.error-message').attr('hidden', 'hidden');
    $(form).find('.has-error').removeClass('has-error');
    $.each(data, function (index, item) {
        switch (item.name) {
            case 'profile':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $(form).find('span.msg-profile-required').removeAttr('hidden').parent().addClass('has-error');
                } else {
                    elementTiny = tinyMCE.get(form.find('textarea')[0].id);
                    var profile = $(elementTiny.getContent()).text().replace(/(<([^>]+)>)/ig, '');
                    profile = profile.replace('&nbsp;', '');
                    if (profile.length > 5000) {
                        has_error = true;
                        $(form).find('span.msg-profile-limit-max').removeAttr('hidden').parent().addClass('has-error');
                    } else if (emailExistsInString(profile) || phoneExistsInStringSpecialChar(profile)) {
                        has_error = true;
                        $(form).find('span.msg-profile-valid').removeAttr('hidden').parent().addClass('has-error');
                    }
                }
                break;
        }
    });

    if (!has_error) {
        $(form).find("button.btn-remove-this-section").css('display', 'none');
        $(form).find("button.btn-cancel").css('display', 'none');
        $(form).find("button.btn-save").css('display', 'none');
        $(form).find("button.btn-sending").css('display', '');
    }
    return !has_error;
}
function postSaveProfileAjaxSubmit(data, status, xhr, form) {
    $(form).find("button.btn-remove-this-section").css('display', '');
    $(form).find("button.btn-cancel").css('display', '');
    $(form).find("button.btn-save").css('display', '');
    $(form).find("button.btn-sending").css('display', 'none');
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $('#error-message-update-profile').slideDown();
        setTimeout(function () {
            $('#error-message-update-profile').slideUp();
        }, 5000);
    } else {
        mdlLiveEdit.saveContent(form);
        reloadProfileLevel();
        $('#card-profile').fadeOut();
    }
}
function saveProfile() {
    var options = {
        beforeSubmit: validateProfile, // pre-submit callback
        success: postSaveProfileAjaxSubmit,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: { action: 'save-profile' } // extra data
    };
    $('#profileForm').ajaxSubmit(options);
}
function postDeleteProfileAjaxSubmit(data, status, xhr, form) {
    $(form).find("button.btn-remove-this-section").css('display', '');
    $(form).find("button.btn-cancel").css('display', '');
    $(form).find("button.btn-save").css('display', '');
    $(form).find("button.btn-sending").css('display', 'none');
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $(form).find('#error-message-update-profile').slideDown();
        setTimeout(function () {
            $(form).find('#error-message-update-profile').slideUp();
        }, 5000);
    } else {
        $('#profile-objective').data('current_data', '').val('');
        $('#profile-objective').parents('.edit-field').siblings('.view-field').find('.view-control').html('');
        $('.section-profile').slideUp();
        $('#card-profile').fadeIn();
    }
}
function deleteProfileConfirmed() {
    var confirm_dialog = $('#dialog-delete-section');
    var form = confirm_dialog.data('target-form');
    confirm_dialog.off('click', 'button.btn-primary', deleteProfileConfirmed);
    confirm_dialog.modal('hide');
    var options = {
        beforeSubmit: function () {
            var form = $('#profileForm');
            $(form).find('span.error-message').attr('hidden', 'hidden');
            $(form).find('.has-error').removeClass('has-error');
            $(form).find("button.btn-remove-this-section").css('display', 'none');
            $(form).find("button.btn-cancel").css('display', 'none');
            $(form).find("button.btn-save").css('display', 'none');
            $(form).find("button.btn-sending").css('display', '');
        },
        success: postDeleteProfileAjaxSubmit,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: { action: 'delete-profile' } // extra data
    };
    $(form).ajaxSubmit(options);
}
function deleteProfile(form) {
    var confirm_dialog = $('#dialog-delete-section');
    confirm_dialog.data('target-form', form);
    confirm_dialog.on('click', 'button.btn-primary', deleteProfileConfirmed);
    confirm_dialog.modal('show');
}
function showUploadPhotoDialog() {
    $('#avatar-upload').trigger('click');
}
var NoLimitSkill = '50';
var buttonCancelLabel = 'Hủy<';
var buttonSaveLabel = 'Lưu';
function addSkillForJobseeker(skillName) {
    $('#key-skill-empty-error').attr('hidden', 'hidden');
    $('#key-skills').parent('span').removeClass('has-error');
    if (trim(skillName) == '') {
        $('#btn-add-tag').find('i').removeClass('fa-spinner fa-pulse').addClass('fa-plus');
        $('#key-skill-empty-error').removeAttr('hidden');
        $('#key-skills').parent('span').addClass('has-error');
    } else {
        $.ajax({
            type: "POST",
            url: "https://www.vietnamworks.com/jobseekers/update_skill.php",
            data: { action: 'add-skill', skillName: skillName },
            dataType: "json",
            success: function (data) {
                $('#btn-add-tag').find('i').removeClass('fa-spinner fa-pulse').addClass('fa-plus');
                if (data.status == 'SUCCESS') {
                    $('#key-skill-limit-error').attr('hidden', 'hidden');
                    var skillTagExist = false;
                    $("#tags-edit input.input-tag-name[name='skills[]']").each(function () {
                        if ($(this).val() == data.data.skillId) {
                            skillTagExist = true;
                            if (typeof ($(this).attr('disabled')) != 'undefined') {
                                $(this).removeAttr('disabled');
                                $(this).parent('span.tag-xs').css('display', '');
                            }
                        }
                    });
                    if (skillTagExist) {
                        mdlKeySkill.highLightTag(data.data.skillId);
                    } else {
                        mdlKeySkill.addTag(data.data.skillId, data.data.skillName);
                    }
                    //addTag(data.data.skillId, data.data.skillName);
                } else if (data.status == 'ERROR') {
                    if (data.statusCode == '404') {
                        $('#key-skill-limit-error').removeAttr('hidden');
                    } else {
                        $('#errorMessage').slideDown();
                        setTimeout(function () {
                            $('#errorMessage').slideUp();
                        }, 5000);
                    }
                }
            },
            error: function (data) {
                $('#btn-add-tag').find('i').removeClass('fa-spinner fa-pulse').addClass('fa-plus');
            }
        });
    }
}
$('#btn-add-tag').click(function () {
    $(this).find('i').removeClass('fa-plus').addClass('fa-spinner fa-pulse');
    addSkillForJobseeker($('#key-skills').val());
});
function showResponseSkillForm(data) {
    $('#updateSkillProcessBtn').css('display', 'none');
    $('#cancelSkillInfoBtn').css('display', '');
    $('#saveSkillInfoBtn').css('display', '');
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $('#errorMessage').slideDown();
        setTimeout(function () {
            $('#errorMessage').slideUp();
        }, 5000);
    } else if (data.status == 'SUCCESS') {
        mdlKeySkill.saveSkill();
        reloadProfileLevel();
    }
}
var options = {
    beforeSubmit: showRequestSkillForm,  // pre-submit callback
    success: showResponseSkillForm,  // post-submit callback
    type: 'post',        // 'get' or 'post', override for form's 'method' attribute
    dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
    data: { action: 'add-skill-jobseeker', resumeId: $('#skillBlockresumeId').val() }
};
function showRequestSkillForm(formData, jqForm, options) {
    $('#key-skill-error').attr('hidden', 'hidden');
    $('#key-skill-limit-error').attr('hidden', 'hidden');
    if ($("#tags-edit input.input-tag-name[name='skills[]']").length > 0) {
        var countSkill = 0;
        $("#tags-edit input.input-tag-name[name='skills[]']").each(function () {
            if ($(this).val() != "" && $(this).val() > 0) {
                if (typeof ($(this).attr('disabled')) == 'undefined') {
                    countSkill = countSkill + 1;
                }
            }
        });
        if (countSkill > 0) {
            $('#saveSkillInfoBtn').css('display', 'none');
            $('#cancelSkillInfoBtn').css('display', 'none');
            $('#updateSkillProcessBtn').css('display', '');
            return true;
        } else if (countSkill > NoLimitSkill) {
            $('#key-skill-limit-error').removeAttr('hidden');
            return false;
        } else {
            $('#key-skill-error').removeAttr('hidden');
            return false;
        }
    } else {
        $('#key-skill-error').removeAttr('hidden');
        return false;
    }
}
$('#saveSkillInfoBtn').click(function (e) {
    e.preventDefault(); // <-- important
    $('#updateSkillFrm').ajaxSubmit(options);
    return false;
});
//TypeAHead
$('#key-skills').typeahead({
    highlight: true,
    minLength: 1,
    maxLength: 10
},
{
    source: function (q, cb) {
        $.ajax({
            url: 'https://www.vietnamworks.com//jobseekers/skill_auto_completed_ajax.php',
            type: 'POST',
            dataType: 'JSON',
            data: { query: q },
            success: function (data) {
                var suggests = [];
                $.each(data, function (idx, elem) {
                    if (idx < 10) {
                        suggests.push({ value: elem });
                    }
                });
                cb(suggests);
            }
        });
    }
});
$('.section-languages').on('click', '.btn-remove-language', function () {
    varFormLanguage = $(this).parents('form');
    var optionsLanguageForm = {
        beforeSubmit: checkValidFormLanguageRemove,  // pre-submit callback
        success: responseRemoveLanguageForm,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: { action: 'remove-language-info', resumeId: $('#languageBlockResumeId').val() }
    };
    varFormLanguage.one('submit', function (e) {
        e.preventDefault(); // <-- important
        $(this).ajaxSubmit(optionsLanguageForm);
        return false;
    });
    varFormLanguage.submit();
    function responseRemoveLanguageForm(data) {
        showResponseRemoveLanguageForm(data, varFormLanguage);
    }
});
function checkValidFormLanguageRemove(data, form, options) {
    form.find('.btn-remove-language').css('display', 'none');
    form.find('.btn-save-language').css('display', 'none');
    form.find('.btn-cancel-language').css('display', 'none');
    form.find('.btn-process-language').css('display', '');
    return true;
}
$('.section-languages').on('click', '.btn-save-language', function () {
    varFormLanguage = $(this).parents('form');
    var optionsLanguageForm = {
        beforeSubmit: checkValidFormLanguage,  // pre-submit callback
        success: responseLanguageForm,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: { action: 'save-language-info', resumeId: $('#languageBlockResumeId').val() }
    };
    varFormLanguage.one('submit', function (e) {
        e.preventDefault(); // <-- important
        $(this).ajaxSubmit(optionsLanguageForm);
        return false;
    });
    varFormLanguage.submit();
    function responseLanguageForm(data) {
        showResponseLanguageForm(data, varFormLanguage);
    }
});
function showResponseRemoveLanguageForm(data, form) {
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $('#error-message-update-language').slideDown();
        setTimeout(function () {
            $('#error-message-update-language').slideUp();
        }, 5000);
    } else if (data.status == 'SUCCESS') {
        mdlLiveEdit.removeAddedForm(form.closest('.form-wrapper'));
        reloadProfileLevel();
    }
    form.find('.btn-remove-language').css('display', '');
    form.find('.btn-save-language').css('display', '');
    form.find('.btn-cancel-language').css('display', '');
    form.find('.btn-process-language').css('display', 'none');
}
function showResponseLanguageForm(data, form) {
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $('#error-message-update-language').slideDown();
        setTimeout(function () {
            $('#error-message-update-language').slideUp();
        }, 5000);
    } else if (data.status == 'SUCCESS') {
        form.attr('id', 'form-language-' + data.resumeLanguageId);
        form.find("input[name='resumeLanguageId']").val(data.resumeLanguageId);
        form.find("select[name^='language']").attr('id', 'language' + data.resumeLanguageId).attr('name', 'language' + data.resumeLanguageId);
        form.find("input[name^='language-level']").attr('name', 'language-level' + data.resumeLanguageId);

        form.find('#lang')
        mdlLiveEdit.saveContent(form);
        reloadProfileLevel();
        mdlLiveEdit.checkContentSection(form.closest('fieldset'));
    }
    form.find('.btn-remove-language').css('display', '');
    form.find('.btn-save-language').css('display', '');
    form.find('.btn-cancel-language').css('display', '');
    form.find('.btn-process-language').css('display', 'none');
}
function checkValidFormLanguage(data, form, options) {
    var has_error = false;
    var hasLanguage = false;
    form.find("span.error-message").attr('hidden', 'hidden');
    form.find('.has-error').removeClass('has-error');
    $.each(data, function (index, item) {
        switch (item.name) {
            case 'language0':
                if (isNaN(item.value) || item.value <= 0) {
                    has_error = true;
                } else if ($("input[name=language-level0]:checked").val() > 0) {
                    hasLanguage = true;
                }
                break;
            case 'language1':
                if (isNaN(item.value) || item.value <= 0) {
                    has_error = true;
                } else if ($("input[name=language-level1]:checked").val() > 0) {
                    hasLanguage = true;
                }
                break;
            case 'language2':
                if (isNaN(item.value) || item.value <= 0) {
                    has_error = true;
                } else if ($("input[name=language-level2]:checked").val() > 0) {
                    hasLanguage = true;
                }
                break;
            case 'language3':
                if (isNaN(item.value) || item.value <= 0) {
                    has_error = true;
                } else if ($("input[name=language-level3]:checked").val() > 0) {
                    hasLanguage = true;
                }
                break;
        }
    });
    if (!hasLanguage) {
        has_error = true;
        form.find('span.lang-required-valid').removeAttr('hidden').parent().addClass('has-error');
    }

    if (!has_error) {
        form.find('.btn-remove-language').css('display', 'none');
        form.find('.btn-save-language').css('display', 'none');
        form.find('.btn-cancel-language').css('display', 'none');
        form.find('.btn-process-language').css('display', '');
    }
    return !has_error;
}
function clearErrorMessageEmploymentHistory(form) {
    $(form).find('span.error-message').attr('hidden', 'hidden');
    $(form).find('p.error-message').attr('hidden', 'hidden');
    $(form).find('.has-error').removeClass('has-error');
}
function validateEmploymentHistory(data, form, options) {
    var has_error = false;
    clearErrorMessageEmploymentHistory(form);
    $.each(data, function (index, item) {
        switch (item.name) {
            case 'position':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $(form).find('span.msg-position-required').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                } else if (emailExistsInString(item.value) || phoneExistsInString(item.value)) {
                    has_error = true;
                    $(form).find('span.msg-position-valid').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'companyName':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $(form).find('span.msg-company-required').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                } else if (emailExistsInString(item.value) || phoneExistsInString(item.value)) {
                    has_error = true;
                    $(form).find('span.msg-company-valid').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'description':
                if (item.value != '') {
                    elementTiny = tinyMCE.get(form.find('textarea')[0].id);
                    var description = $(elementTiny.getContent()).text().replace(/(<([^>]+)>)/ig, '');
                    description = description.replace('&nbsp;', '');
                    if (description.length < 100) {
                        has_error = true;
                        $(form).find('p.msg-description-limit-min').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                    } else if (description.length > 5000) {
                        has_error = true;
                        $(form).find('p.msg-description-limit-max').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                    } else if (emailExistsInString(description) || phoneExistsInStringSpecialChar(description)) {
                        has_error = true;
                        $(form).find('p.msg-description-valid').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                    }
                }
                break;
        }
    });

    if ($(form).find("input[name='fromDate']").datepicker("getDate").getMonth() >= 0) {
        if ($(form).find("input[name='currentExp']").is(':checked')) {
            if ($(form).find("input[name='fromDate']").datepicker("getDate").getTime() > (new Date()).getTime()) {
                // "from date" is in the future
                $(form).find('span.msg-from-date-no-future-date').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                has_error = true;
            } else {
                $(form).find('span.msg-to-date-required').attr('hidden', 'hidden');
                $(form).find('span.msg-to-date-no-future-date').attr('hidden', 'hidden');
            }
        } else {
            fromDate = ($(form).find("input[name='fromDate']").datepicker("getDate").getMonth()) + ($(form).find("input[name='fromDate']").datepicker("getDate").getYear() * 12);
            toDate = ($(form).find("input[name='toDate']").datepicker("getDate").getMonth()) + ($(form).find("input[name='toDate']").datepicker("getDate").getYear() * 12);
            if (isNaN(toDate)) {
                // If "to date" is empty
                $(form).find('span.msg-to-date-required').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                has_error = true;
            } else if ($(form).find("input[name='fromDate']").datepicker("getDate").getTime() > (new Date()).getTime()) {
                // if "from date" is in the future
                $(form).find('span.msg-from-date-no-future-date').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                has_error = true;
            } else if ($(form).find("input[name='toDate']").datepicker("getDate").getTime() > (new Date()).getTime()) {
                // If "to date" is in the future
                $(form).find('span.msg-to-date-no-future-date').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                has_error = true;
            } else if (fromDate > toDate) {
                $(form).find('span.msg-to-date-valid').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                has_error = true;
            }
        }
    } else {
        toDate = ($(form).find("input[name='toDate']").datepicker("getDate").getMonth()) + ($(form).find("input[name='toDate']").datepicker("getDate").getYear() * 12);
        if ($(form).find("input[name='currentExp']").is(':checked') || toDate >= 0) {
            $(form).find('span.msg-from-date-valid').css('display', '').removeAttr('hidden').parent().addClass('has-error');
            has_error = true;
        } else if ($(form).find("input[name='fromDate']").datepicker("getDate").getTime() > (new Date()).getTime()) {
            // "from Date" is in the future
            $(form).find('span.msg-from-date-no-future-date').css('display', '').removeAttr('hidden').parent().addClass('has-error');
            has_error = true;
        } else if ($(form).find("input[name='toDate']").datepicker("getDate").getTime() > (new Date()).getTime()) {
            // "to Date" is in the future
            $(form).find('span.msg-to-date-no-future-date').css('display', '').removeAttr('hidden').parent().addClass('has-error');
            has_error = true;
        }
    }

    if (!has_error) {
        $(form).find("button.btn-remove-this-section").css('display', 'none');
        $(form).find("button.btn-cancel").css('display', 'none');
        $(form).find("button.btn-save").css('display', 'none');
        $(form).find("button.btn-sending").css('display', '');
    }
    return !has_error;
}
function sortExperiences() {

    // Destroy TinyMCE before sorting, since sorting will cause tinyMCE to malfunction
    var $sectionEmploymentFormView = $('div .section-employment form.form-view');
    $sectionEmploymentFormView.not('.sample').find('textarea').each(function () {
        tinymce.EditorManager.get($(this).attr('id')).destroy();
    });

    // Sort
    $sectionEmploymentFormView.sortElements(function (a, b) {
        return $(a).attr('data-order') > $(b).attr('data-order') ? 1 : -1;
    });

    // Init tinyMCE again
    $sectionEmploymentFormView.not('.sample').find('textarea').each(function () {
        $(this).tinymce({
            theme: 'modern',
            toolbar: false,
            menubar: false,
            statusbar: false,
            setup: function (editor) {
                var numberOfCharacterAllowed = 5000;
                editor.on('keyup focus init', function () {
                    updateCountdown(editor, $(editor.getContainer()).siblings('.countdown'), numberOfCharacterAllowed, true);
                }).on('keydown', function (event) {
                    // Stop enter text when reaching its limit
                    var key = event.which;
                    if ($(editor.getContent()).text().length >= numberOfCharacterAllowed && !(key == 46 || key == 8 || key == 37 || key == 39)) {
                        event.preventDefault();
                        event.stopPropagation();
                        // return false;
                    }
                });
            },
            valid_elements: 'a[class|href|rel|rev|tabindex|title],'
            + 'abbr[class|title],'
            + 'blockquote[cite|class],'
            + 'br[style],'
            + 'cite[class],'
            + 'dd[class],'
            + 'del[cite|class|datetime],'
            + 'dfn[class],'
            + 'div[class],'
            + 'dl[class],'
            + 'dt[class],'
            + 'em/i[class],'
            + 'h1[class],'
            + 'h2[class],'
            + 'h3[class],'
            + 'h4[class],'
            + 'h5[class]'
            + 'hr[class|noshade<noshade|size],'
            + 'img[align<bottom?left?middle?right?top|alt|class|height|longdesc|src|title|width],'
            + 'ins[cite|class|datetime],'
            + 'li[class|type],'
            + 'ol[class|start|type],'
            + 'p[class],'
            + 'pre/listing/plaintext/xmp[class],'
            + 'q[cite|class],'
            + 'small[class],'
            + 'span[class],'
            + 'strong/b[class],'
            + 'sub[class],'
            + 'sup[class],'
            + 'table[class|id|summary|title],'
            + 'td[colspan|rowspan],'
            + 'th[colspan|rowspan],'
            + 'tr[rowspan],'
            + 'ul[class|id|title|type]'
            + 'u[class|id|title]'
            + "font[class|color|dir<ltr?rtl|face|id|lang|size|style|title]"
        });
    });

}
function postSaveEmploymentHistoryAjaxSubmit(data, status, xhr, form) {
    $(form).find("button.btn-remove-this-section").css('display', '');
    $(form).find("button.btn-cancel").css('display', '');
    $(form).find("button.btn-save").css('display', '');
    $(form).find("button.btn-sending").css('display', 'none');
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $(form).find('.error-message-update-employment').slideDown();
        setTimeout(function () {
            $(form).find('.error-message-update-employment').slideUp();
        }, 5000);
    } else {
        mdlLiveEdit.saveContent(form);
        console.log('After save')

        if (data.hasOwnProperty('data') && data.data.hasOwnProperty('entry_id')) {
            $(form).find("input[name='entry_id']").val(data.data.entry_id);
        }
        $("div .section-employment form.form-view").each(function (index) {
            var entryId = $(this).find("input[name='entry_id']").val();
            var formOrder = $(this);
            $.each(data.orders, function (i, item) {
                if (entryId == item.entryid) {
                    formOrder.attr('data-order', item.experienceorder);
                }
            });
        });
        //form.attr('data-order',data.data.experienceorder);
        sortExperiences();
        reloadProfileLevel();
        mdlLiveEdit.setLatestFields(data.mostrecentemployer, data.mostrecentposition);
        mdlLiveEdit.checkContentSection(form.closest('fieldset'));
    }
}
function saveEmploymentHistory(form) {
    var options = {
        beforeSubmit: validateEmploymentHistory, // pre-submit callback
        success: postSaveEmploymentHistoryAjaxSubmit,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: { action: 'save-employment-history' } // extra data
    };
    $(form).ajaxSubmit(options);
}
function postDeleteEmploymentHistoryAjaxSubmit(data, status, xhr, form) {
    $(form).find("button.btn-remove-this-section").css('display', '');
    $(form).find("button.btn-cancel").css('display', '');
    $(form).find("button.btn-save").css('display', '');
    $(form).find("button.btn-sending").css('display', 'none');
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $(form).find('.error-message-update-employment').slideDown();
        setTimeout(function () {
            $(form).find('.error-message-update-employment').slideUp();
        }, 5000);
    } else {
        mdlLiveEdit.removeAddedForm(form);
        mdlLiveEdit.setLatestFields(data.mostrecentemployer, data.mostrecentposition);
        reloadProfileLevel();
    }
}
function deleteEmploymentHistoryConfirmed() {
    var confirm_dialog = $('#dialog-delete-section');
    var form = confirm_dialog.data('target-form');
    confirm_dialog.off('click', 'button.btn-primary', deleteEmploymentHistoryConfirmed);
    confirm_dialog.modal('hide');
    var options = {
        success: postDeleteEmploymentHistoryAjaxSubmit,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: { action: 'delete-employment-history' } // extra data
    };
    $(form).ajaxSubmit(options);
}
function deleteEmploymentHistory(form) {
    var confirm_dialog = $('#dialog-delete-section')
    confirm_dialog.data('target-form', form);
    confirm_dialog.on('click', 'button.btn-primary', deleteEmploymentHistoryConfirmed);
    confirm_dialog.modal('show');
}
function clearErrorMessageEducationHistory(form) {
    $(form).find('p.error-message').attr('hidden', 'hidden');
    $(form).find('span.error-message').attr('hidden', 'hidden');
    $(form).find('.has-error').removeClass('has-error');
}
function validateEducationHistory(data, form, options) {
    var has_error = false;
    clearErrorMessageEducationHistory(form);
    $.each(data, function (index, item) {
        switch (item.name) {
            case 'subject':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $(form).find('span.msg-subject-required').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                } else if (emailExistsInString(item.value) || phoneExistsInString(item.value)) {
                    has_error = true;
                    $(form).find('span.msg-subject-valid').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'school':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $(form).find('span.msg-school-required').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                } else if (emailExistsInString(item.value) || phoneExistsInString(item.value)) {
                    has_error = true;
                    $(form).find('span.msg-school-valid').removeAttr('hidden').css('display', '').parent().addClass('has-error');
                }
                break;
            case 'qualification':
                if (item.value == '0') {
                    has_error = true;
                    $(form).find('span.msg-qualification-required').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'description':
                if (item.value != '') {
                    elementTiny = tinyMCE.get(form.find('textarea')[0].id);
                    var description = $(elementTiny.getContent()).text().replace(/(<([^>]+)>)/ig, '');
                    description = description.replace('&nbsp;', '');
                    if (description.length > 5000) {
                        has_error = true;
                        $(form).find('p.msg-description-limit-max').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                    } else if (emailExistsInString(description) || phoneExistsInStringSpecialChar(description)) {
                        has_error = true;
                        $(form).find('p.msg-description-valid').css('display', '').removeAttr('hidden').parent().addClass('has-error');
                    }
                }
                break;
        }
    });

    // Check "From Date" first; if "from date" exist
    if ($(form).find("input[name='fromDate']").datepicker("getDate").getMonth() >= 0) {

        fromDate = ($(form).find("input[name='fromDate']").datepicker("getDate").getMonth()) + ($(form).find("input[name='fromDate']").datepicker("getDate").getYear() * 12);
        toDate = ($(form).find("input[name='toDate']").datepicker("getDate").getMonth()) + ($(form).find("input[name='toDate']").datepicker("getDate").getYear() * 12);

        if (isNaN(toDate)) {
            // If "to date" is not a number
            $(form).find('span.msg-to-date-required').css('display', '').removeAttr('hidden').parent().addClass('has-error');
            has_error = true;
        } else if ($(form).find("input[name='fromDate']").datepicker("getDate").getTime() > (new Date()).getTime()) {
            // If "from date" is in the future
            $(form).find('span.msg-from-date-no-future-date').css('display', '').removeAttr('hidden').parent().addClass('has-error');
            has_error = true;
        } else if ($(form).find("input[name='toDate']").datepicker("getDate").getTime() > (new Date()).getTime()) {
            // If "to date" is in the future
            $(form).find('span.msg-to-date-no-future-date').css('display', '').removeAttr('hidden').parent().addClass('has-error');
            has_error = true;
        } else if (fromDate > toDate) {
            // if "from date" is before "to date"
            $(form).find('span.msg-to-date-valid').css('display', '').removeAttr('hidden').parent().addClass('has-error');
            has_error = true;
        }
    } else {

        fromDate = ($(form).find("input[name='fromDate']").datepicker("getDate").getMonth()) + ($(form).find("input[name='fromDate']").datepicker("getDate").getYear() * 12);
        toDate = ($(form).find("input[name='toDate']").datepicker("getDate").getMonth()) + ($(form).find("input[name='toDate']").datepicker("getDate").getYear() * 12);

        if (toDate > 0) {
            // If to date exist, then "from date" should not be empty
            $(form).find('span.msg-from-date-required').css('display', '').removeAttr('hidden').parent().addClass('has-error');
            has_error = true;
        } else if ($(form).find("input[name='fromDate']").datepicker("getDate").getTime() > (new Date()).getTime()) {
            // If "from date" is in the future
            $(form).find('span.msg-from-date-no-future-date').css('display', '').removeAttr('hidden').parent().addClass('has-error');
            has_error = true;
        } if ($(form).find("input[name='toDate']").datepicker("getDate").getTime() > (new Date()).getTime()) {
            // If "to date" is in the future
            $(form).find('span.msg-to-date-no-future-date').css('display', '').removeAttr('hidden').parent().addClass('has-error');
            has_error = true;
        }
    }

    if (!has_error) {
        $(form).find("button.btn-remove-this-section").css('display', 'none');
        $(form).find("button.btn-cancel").css('display', 'none');
        $(form).find("button.btn-save").css('display', 'none');
        $(form).find("button.btn-sending").css('display', '');
    }
    return !has_error;
}
function sortEducations() {
    // Destroy TinyMCE before sorting, since sorting will cause tinyMCE to malfunction
    var $sectionEducationFormView = $('div .section-education form.form-view');
    $sectionEducationFormView.not('.sample').find('textarea').each(function () {
        tinymce.EditorManager.get($(this).attr('id')).destroy();
    });

    // Sort
    $sectionEducationFormView.sortElements(function (a, b) {
        return $(a).attr('data-order') > $(b).attr('data-order') ? 1 : -1;
    });

    // Init tinyMCE again
    $sectionEducationFormView.not('.sample').find('textarea').each(function () {
        $(this).tinymce({
            theme: 'modern',
            toolbar: false,
            menubar: false,
            statusbar: false,
            setup: function (editor) {
                var numberOfCharacterAllowed = 5000;
                editor.on('keyup focus init', function () {
                    updateCountdown(editor, $(editor.getContainer()).siblings('.countdown'), numberOfCharacterAllowed, true);
                }).on('keydown', function (event) {
                    // Stop enter text when reaching its limit
                    var key = event.which;
                    if ($(editor.getContent()).text().length >= numberOfCharacterAllowed && !(key == 46 || key == 8 || key == 37 || key == 39)) {
                        event.preventDefault();
                        event.stopPropagation();
                        // return false;
                    }
                });
            },
            valid_elements: 'a[class|href|rel|rev|tabindex|title],'
            + 'abbr[class|title],'
            + 'blockquote[cite|class],'
            + 'br[style],'
            + 'cite[class],'
            + 'dd[class],'
            + 'del[cite|class|datetime],'
            + 'dfn[class],'
            + 'div[class],'
            + 'dl[class],'
            + 'dt[class],'
            + 'em/i[class],'
            + 'h1[class],'
            + 'h2[class],'
            + 'h3[class],'
            + 'h4[class],'
            + 'h5[class]'
            + 'hr[class|noshade<noshade|size],'
            + 'img[align<bottom?left?middle?right?top|alt|class|height|longdesc|src|title|width],'
            + 'ins[cite|class|datetime],'
            + 'li[class|type],'
            + 'ol[class|start|type],'
            + 'p[class],'
            + 'pre/listing/plaintext/xmp[class],'
            + 'q[cite|class],'
            + 'small[class],'
            + 'span[class],'
            + 'strong/b[class],'
            + 'sub[class],'
            + 'sup[class],'
            + 'table[class|id|summary|title],'
            + 'td[colspan|rowspan],'
            + 'th[colspan|rowspan],'
            + 'tr[rowspan],'
            + 'ul[class|id|title|type]'
            + 'u[class|id|title]'
            + "font[class|color|dir<ltr?rtl|face|id|lang|size|style|title]"
        });
    });
}
function postSaveEducationHistoryAjaxSubmit(data, status, xhr, form) {
    $(form).find("button.btn-remove-this-section").css('display', '');
    $(form).find("button.btn-cancel").css('display', '');
    $(form).find("button.btn-save").css('display', '');
    $(form).find("button.btn-sending").css('display', 'none');
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $(form).find('.error-message-update-education').slideDown();
        setTimeout(function () {
            $(form).find('.error-message-update-education').slideUp();
        }, 5000);
    } else {
        mdlLiveEdit.saveContent(form);
        if (data.hasOwnProperty('data') && data.data.hasOwnProperty('entry_id')) {
            $(form).find("input[name='entry_id']").val(data.data.entry_id);
        }
        $("div .section-education form.form-view").each(function (index) {
            var entryId = $(this).find("input[name='entry_id']").val();
            var formOrder = $(this);
            $.each(data.orders, function (i, item) {
                if (entryId == item.entryid) {
                    formOrder.attr('data-order', item.educationorder);
                }
            });
        });
        //form.attr('data-order',data.data.experienceorder);
        sortEducations();

        reloadProfileLevel();
        mdlLiveEdit.checkContentSection(form.closest('fieldset'));
        if (data.hasOwnProperty('data') && data.data.hasOwnProperty('entry_id')) {
            $(form).find("input[name='entry_id']").val(data.data.entry_id);
        }
    }
}
function saveEducationHistory(form) {
    var options = {
        beforeSubmit: validateEducationHistory, // pre-submit callback
        success: postSaveEducationHistoryAjaxSubmit,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: { action: 'save-education-history' } // extra data
    };
    $(form).ajaxSubmit(options);
}
function postDeleteEducationHistoryAjaxSubmit(data, status, xhr, form) {
    $(form).find("button.btn-remove-this-section").css('display', '');
    $(form).find("button.btn-cancel").css('display', '');
    $(form).find("button.btn-save").css('display', '');
    $(form).find("button.btn-sending").css('display', 'none');
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $(form).find('.error-message-update-education').slideDown();
        setTimeout(function () {
            $(form).find('.error-message-update-education').slideUp();
        }, 5000);
    } else {
        mdlLiveEdit.removeAddedForm(form);
        reloadProfileLevel();
    }
}
function deleteEducationHistoryConfirmed() {
    var form = $('#dialog-delete-section').data('target-form');
    $('#dialog-delete-section').off('click', 'button.btn-primary', deleteEducationHistoryConfirmed);
    $('#dialog-delete-section').modal('hide');
    var options = {
        success: postDeleteEducationHistoryAjaxSubmit,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: { action: 'delete-education-history' } // extra data
    };
    $(form).ajaxSubmit(options);
}
function deleteEducationHistory(form) {
    $('#dialog-delete-section').data('target-form', form);
    $('#dialog-delete-section').on('click', 'button.btn-primary', deleteEducationHistoryConfirmed);
    $('#dialog-delete-section').modal('show');
}
function validateReference(data, form, options) {
    var has_error = false;
    $(form).find('span.error-message').attr('hidden', 'hidden');
    $(form).find('.has-error').removeClass('has-error');

    $.each(data, function (index, item) {
        switch (item.name) {
            case 'name':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $(form).find('span.msg-name-required').removeAttr('hidden').parent().addClass('has-error');
                } else if (emailExistsInString(item.value) || phoneExistsInString(item.value)) {
                    has_error = true;
                    $(form).find('span.msg-name-invalid').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'position':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $(form).find('span.msg-position-required').removeAttr('hidden').parent().addClass('has-error');
                } else if (emailExistsInString(item.value) || phoneExistsInString(item.value)) {
                    has_error = true;
                    $(form).find('span.msg-position-invalid').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'company':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $(form).find('span.msg-company-required').removeAttr('hidden').parent().addClass('has-error');
                } else if (emailExistsInString(item.value) || phoneExistsInString(item.value)) {
                    has_error = true;
                    $(form).find('span.msg-company-invalid').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'email':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $(form).find('span.msg-email-required').removeAttr('hidden').parent().addClass('has-error');
                } else if (!_checkEmail(item.value)) {
                    has_error = true;
                    $(form).find('span.msg-email-invalid').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'phone':
                if ($.trim(item.value) != '' && !_checkPhone(item.value)) {
                    has_error = true;
                    $(form).find('span.msg-phone-invalid').removeAttr('hidden').parent().addClass('has-error');
                }
                break;
        }
    });

    if (!has_error) {
        $(form).find("button.btn-remove-this-section").css('display', 'none');
        $(form).find("button.btn-cancel").css('display', 'none');
        $(form).find("button.btn-save").css('display', 'none');
        $(form).find("button.btn-sending").css('display', '');
    }
    return !has_error;
}
function postSaveReferenceAjaxSubmit(data, status, xhr, form) {
    $(form).find("button.btn-remove-this-section").css('display', '');
    $(form).find("button.btn-cancel").css('display', '');
    $(form).find("button.btn-save").css('display', '');
    $(form).find("button.btn-sending").css('display', 'none');
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $(form).find('.error-message-update-reference').slideDown();
        setTimeout(function () {
            $(form).find('.error-message-update-reference').slideUp();
        }, 5000);
    } else {
        mdlLiveEdit.saveContent(form);
        reloadProfileLevel();
        mdlLiveEdit.checkContentSection(form.closest('fieldset'));
        if (data.hasOwnProperty('data') && data.data.hasOwnProperty('entry_id')) {
            $(form).find("input[name='entry_id']").val(data.data.entry_id);
        }
    }
}
function saveReference(form) {
    var options = {
        beforeSubmit: validateReference, // pre-submit callback
        success: postSaveReferenceAjaxSubmit,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: { action: 'save-reference' } // extra data
    };
    $(form).ajaxSubmit(options);
}
function postDeleteReferenceAjaxSubmit(data, status, xhr, form) {
    $(form).find("button.btn-remove-this-section").css('display', '');
    $(form).find("button.btn-cancel").css('display', '');
    $(form).find("button.btn-save").css('display', '');
    $(form).find("button.btn-sending").css('display', 'none');
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $(form).find('.error-message-update-reference').slideDown();
        setTimeout(function () {
            $(form).find('.error-message-update-reference').slideUp();
        }, 5000);
    } else {
        mdlLiveEdit.removeAddedForm(form);
        reloadProfileLevel();
    }
}
function deleteReferenceConfirmed() {
    var confirm_dialog = $('#dialog-delete-section');
    var form = confirm_dialog.data('target-form');
    confirm_dialog.off('click', 'button.btn-primary', deleteReferenceConfirmed);
    confirm_dialog.modal('hide');
    var options = {
        success: postDeleteReferenceAjaxSubmit,  // post-submit callback
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
        data: { action: 'delete-reference' } // extra data
    };
    $(form).ajaxSubmit(options);
}
function deleteReference(form) {
    var confirm_dialog = $('#dialog-delete-section');
    confirm_dialog.data('target-form', form);
    confirm_dialog.on('click', 'button.btn-primary', deleteReferenceConfirmed);
    confirm_dialog.modal('show');
}
$('#saveSummaryInforBtn').click(function () {
    $('#form-summary').submit();
});
var optionsSummaryForm = {
    beforeSubmit: checkValidFormSummary,  // pre-submit callback
    success: showResponseSummaryForm,  // post-submit callback
    type: 'post',        // 'get' or 'post', override for form's 'method' attribute
    dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type)
    data: { action: 'save-summary-info' }
};
$('#form-summary').on('submit', function (e) {
    e.preventDefault(); // <-- important
    $(this).ajaxSubmit(optionsSummaryForm);
    return false;
});
function showResponseSummaryForm(data) {
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR' || data.status == 'ERROR') {
        $('#error-message-update-summary').slideDown();
        setTimeout(function () {
            $('#error-message-update-summary').slideUp();
        }, 5000);
    } else if (data.status == 'SUCCESS') {
        var $thisFormWrapper = $('#saveSummaryInforBtn').parents('.form-wrapper');
        mdlLiveEdit.saveContent($thisFormWrapper);
        mdlBenefit.saveExpectedBenefit();
        reloadProfileLevel();
        //updateCompletedStatus();
    }
    $('#saveSummaryInforBtn').css('display', '');
    $('#cancelSummaryInforBtn').css('display', '');
    $('#updateSummaryProcessBtn').css('display', 'none');
}
function checkValidFormSummary(data, form, options) {
    var has_error = false;
    $('#form-summary').find("span.error-message").attr('hidden', 'hidden');
    $('#form-summary').find('.has-error').removeClass('has-error');
    var hasLocation = hasIndustry = hasBenefit = false;
    $.each(data, function (index, item) {
        switch (item.name) {
            case 'expectedJobLevel':
                if ($.trim(item.value) == '') {
                    has_error = true;
                    $('#errExpectedJobLevel').show().removeAttr('hidden').parent().addClass('has-error');
                }
                break;
            case 'expectedLocation[]':
                hasLocation = true;
                break;
            case 'benefits[]':
                hasBenefit = true;
                break;
            case 'expectedJobCategory[]':
                hasIndustry = true;
                break;
            case 'expectedSalaryRange':
                if (isNaN(item.value)) {
                    has_error = true;
                    $('#errNotValidExpectedSalaryRange').show().removeAttr('hidden').parent().addClass('has-error');
                } else if (item.value <= 0 || $.trim(item.value) == '') {
                    has_error = true;
                    $('#errExpectedSalaryRange').show().removeAttr('hidden').parent().addClass('has-error');
                } else {
                    $('#specific-salary-input').val(parseInt($('#specific-salary-input').val()));
                }
                break;
        }
    });
    if (!hasLocation) {
        has_error = true;
        $('#errExpectedLocation').show().removeAttr('hidden').parent().addClass('has-error');
    }
    if (!hasIndustry) {
        has_error = true;
        $('#errExpectedJobCategory').show().removeAttr('hidden').parent().addClass('has-error');
    }
    if (!hasBenefit) {
        has_error = true;
    }

    if (!has_error) {
        $('#saveSummaryInforBtn').css('display', 'none');
        $('#cancelSummaryInforBtn').css('display', 'none');
        $('#updateSummaryProcessBtn').css('display', '');
    }
    return !has_error;
}
var FIREBASE_REFRESH_TOKEN_URL = "/quan-ly-nghe-nghiep/phan-hoi-cua-nha-tuyen-dung/?action=renewFirebaseJWT";
var FRIEND_LINK_FETCH_URL = "/quan-ly-nghe-nghiep/phan-hoi-cua-nha-tuyen-dung/?action=fetchEmployerLink&employerIds=";
var firebaseApiKey = "AIzaSyAJLgT_Xo5Jg_YyPaWYOrk5sFnAtEcOIfU";
var firebaseAuthDomain = "vietnamworks-message.firebaseapp.com";
var firebaseDatabaseURL = "https://vietnamworks-message.firebaseio.com";
// Toggle the new profile notification
$(function () {
    "use strict";
    setTimeout(function () {
        $('.profile-is-available p').addClass('fadeInRight').show()
    }, 200);

    $('.js-top-banner').click(function () {
        $(this).find('a').simulate('click');
    });

    // Close other dropdowns whenever a dropdown opens
    var $notificationPopover = $('.alert-notifications');
    var $userAccountMenuDropdown = $('.dropdown.user-account');
    $notificationPopover.on('shown.bs.popover', function () {
        $userAccountMenuDropdown.removeClass('open');
    });
    $userAccountMenuDropdown.on('shown.bs.dropdown', function () {
        $notificationPopover.popover('hide');
    })

})
var system_version = 'r133s1d20170316t1';
var APP_WEBROOT_JOBSEEKER = 'https://www.vietnamworks.com/',
        language = 1,
        base_url = 'https://www.vietnamworks.com/';
var isIndexPage = false;
var isInnerShowTooltipPage = false;

function switchLanguage(changeToLanguage) {
    setCookie('ChangeLanguageTo', changeToLanguage);
    window.location = 'https://www.vietnamworks.com/my-profile';
}

var jobseekerDomain = 'https://www.vietnamworks.com/';
var responseURI = '/ho-so';
var currentLanguage = 1;
var contentMD5 = '4c443c7e2c515d6b4b4d693c2f63434a7773226a614846733c4c4d4348';
var langID = '1';
(function () {
    var gr = document.createElement('script');
    gr.type = 'text/javascript';
    gr.async = !0;
    gr.src = document.location.protocol + '//vietnamworks-sin.gravityrd-services.com/js/vietnamworks/gr_reco4-min.js';
    var s = document.getElementsByTagName('script')[0];
    s.parentNode.insertBefore(gr, s);
})();
(function (w, d, s, l, i) {
    w[l] = w[l] || []; w[l].push({
        'gtm.start':
                new Date().getTime(), event: 'gtm.js'
    }); var f = d.getElementsByTagName(s)[0],
    j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
    '//www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
})(window, document, 'script', 'dataLayer', 'GTM-TKNN69');
/ Select 2
$('#locationMainSearch,#cateListMainSearch').each(function () {
    $(this).select2({width: '100%'}).Select2SelectAll();
});
$('#search-widget select.job-level').select2({width: '100%', minimumResultsForSearch: 20});

$('select#page-size').change(function(event) {
    if (page_size_urls[$(this).val()]) {
        window.location = page_size_urls[$(this).val()];
    }
});

$('.job-post .view-more').click(function() {
    window.open($(this).data('joburl'), '_blank');
});

var keywordCondition = '';
var industryCondition = '-1';
var locationCondition = '-1';
var jobLevelCondition = '';
var salaryCondition = '';

var totalJobAlert = 0;
    
var token = 'd08f715a86a05eea16fd47a4d144d1ea';
var system_version = 'r133s1d20170316t1';
var APP_WEBROOT_JOBSEEKER = 'https://www.vietnamworks.com/',
        language = 1,
        languageID = 1,
        base_url = 'https://www.vietnamworks.com/';
var isIndexPage = 'false';
var isInnerShowTooltipPage = false;

function switchLanguage(changeToLanguage) {
    setCookie('ChangeLanguageTo', changeToLanguage);
    window.location = 'https://www.vietnamworks.com/my-profile';}
$('#search-widget select.job-level').select2({width: '100%', minimumResultsForSearch: 20});
$(function () {
    $('.editable-location-select').data('options', [{"value": "29", "text": "Ho Chi Minh"}, {
        "value": "24",
        "text": "Ha Noi"
    }, {"value": "71", "text": "Mekong Delta"}, {"value": "2", "text": "An Giang"}, {
        "value": "3",
        "text": "Ba Ria - Vung Tau"
    }, {"value": "4", "text": "Bac Can"}, {"value": "5", "text": "Bac Giang"}, {
        "value": "6",
        "text": "Bac Lieu"
    }, {"value": "7", "text": "Bac Ninh"}, {"value": "8", "text": "Ben Tre"}, {
        "value": "9",
        "text": "Bien Hoa"
    }, {"value": "10", "text": "Binh Dinh"}, {"value": "11", "text": "Binh Duong"}, {
        "value": "12",
        "text": "Binh Phuoc"
    }, {"value": "13", "text": "Binh Thuan"}, {"value": "14", "text": "Ca Mau"}, {
        "value": "15",
        "text": "Can Tho"
    }, {"value": "16", "text": "Cao Bang"}, {"value": "17", "text": "Da Nang"}, {
        "value": "18",
        "text": "Dac Lac"
    }, {"value": "69", "text": "Dien Bien"}, {"value": "19", "text": "Dong Nai"}, {
        "value": "20",
        "text": "Dong Thap"
    }, {"value": "21", "text": "Gia Lai"}, {
        "value": "22",
        "text": "Ha Giang"
    }, {"value": "23", "text": "Ha Nam"}, {"value": "25", "text": "Ha Tay"}, {
        "value": "26",
        "text": "Ha Tinh"
    }, {"value": "27", "text": "Hai Duong"}, {"value": "28", "text": "Hai Phong"}, {
        "value": "30",
        "text": "Hoa Binh"
    }, {"value": "31", "text": "Hue"}, {"value": "32", "text": "Hung Yen"}, {
        "value": "33",
        "text": "Khanh Hoa"
    }, {"value": "34", "text": "Kon Tum"}, {"value": "35", "text": "Lai Chau"}, {
        "value": "36",
        "text": "Lam Dong"
    }, {"value": "37", "text": "Lang Son"}, {"value": "38", "text": "Lao Cai"}, {
        "value": "39",
        "text": "Long An"
    }, {"value": "40", "text": "Nam Dinh"}, {"value": "41", "text": "Nghe An"}, {
        "value": "42",
        "text": "Ninh Binh"
    }, {"value": "43", "text": "Ninh Thuan"}, {"value": "44", "text": "Phu Tho"}, {
        "value": "45",
        "text": "Phu Yen"
    }, {"value": "46", "text": "Quang Binh"}, {"value": "47", "text": "Quang Nam"}, {
        "value": "48",
        "text": "Quang Ngai"
    }, {"value": "49", "text": "Quang Ninh"}, {"value": "50", "text": "Quang Tri"}, {
        "value": "51",
        "text": "Soc Trang"
    }, {"value": "52", "text": "Son La"}, {"value": "53", "text": "Tay Ninh"}, {
        "value": "54",
        "text": "Thai Binh"
    }, {"value": "55", "text": "Thai Nguyen"}, {"value": "56", "text": "Thanh Hoa"}, {
        "value": "57",
        "text": "Thua Thien-Hue"
    }, {"value": "58", "text": "Tien Giang"}, {"value": "59", "text": "Tra Vinh"}, {
        "value": "60",
        "text": "Tuyen Quang"
    }, {"value": "61", "text": "Kien Giang"}, {"value": "62", "text": "Vinh Long"}, {
        "value": "63",
        "text": "Vinh Phuc"
    }, {"value": "65", "text": "Yen Bai"}, {"value": "66", "text": "Other"}, {
        "value": "70",
        "text": "International"
    }, {"value": "72", "text": "Hau Giang"}]);
    $('.editable-location-select').data('selected', 24);


    $('.editable-form').editableForm({item_width: '300px', item_width_sm: '215px', item_width_xs: '215px'});
    $('.editable-form').editableForm('saveButton').click(function (event) {
        event.stopPropagation();
        $('.editable-form').editableForm('update');
    });

    var onlineProfileLevel = $('#online-profile-level').val();
    var numberDays = "28";
    var isApproved = "0";
    setTimeout(function () {
        if(onlineProfileLevel != ''){
            if(onlineProfileLevel == 0){
                profileSuggestion.complete();
                setTimeout(function () {
                    profileSuggestion.destroySearchable();
                }, 8000);
            }else{
                if (!($('#chb-searchable').is(':checked')) && isApproved == '1') {
                    profileSuggestion.searchable();
                    setTimeout(function () {
                        profileSuggestion.destroySearchable();
                    }, 8000);
                }else if(onlineProfileLevel == 1) {
                    profileSuggestion.upgrade();
                    setTimeout(function () {
                        profileSuggestion.destroyUpgrade();
                    }, 8000);
                }
            }
        }
        if (numberDays > 365) {
            profileSuggestion.update();
            setTimeout(function () {
                profileSuggestion.destroyUpdate();
            }, 8000);
        }
    }, 800);
});


$(function () {
    $('#workingCompanyName').next('input').attr('tabindex', '1');
    $('#cellPhone').next('input').attr('tabindex', '2');
    $('#resumeFile').attr('tabindex', '3');
    $('#userProfileForm').find('.fe-btn-save').attr('tabindex', '4');
    $('#userProfileForm').find('.fe-btn-cancel').attr('tabindex', '5');
    $('#resumeFile').focus(function () {
        $('.file-upload-label').addClass('focused');
    }).blur(function () {
        $('.file-upload-label').removeClass('focused');
    });
})
</script>
<script>
$(function () {
    var $link = document.location.href;
    if ($link.search('#skill') > 0 || $link.search('#ky-nang') > 0) {
        addSectionContent('.section-skills');
    }

    setTimeout(function () {
        $('.profile-messages').removeClass('invisible').addClass('animated fadeInRight');
    }, 400)
})
</script>
<script type="text/javascript">
    // Select 2
    $('#locationMainSearch,#cateListMainSearch').each(function () {
        $(this).select2({width: '100%'}).Select2SelectAll();
    });

$('#search-widget select.job-level').select2({width: '100%', minimumResultsForSearch: 20});



//process for reload profile level
function reloadProfileLevel() {
    $("#loading-profile-level").show();
    $.ajax({
        url: APP_WEBROOT_JOBSEEKER + "/jobseekers/online_resume_ajax.php",
        method: "POST",
        data: {action: 'reload-profile-level', resumeid: '4387814'},
    }).done(function (data) {
        $("#profileLevel").html(data);
        //profileSuggestion.destroyUpdate();
    });
}

//process for reload profile level
loadAppliedJobStatistic();
function loadAppliedJobStatistic() {
    $(".loading-applied-job").show();
    $.ajax({
        url: APP_WEBROOT_JOBSEEKER + "/jobseekers/online_resume_ajax.php",
        method: "POST",
        data: {action: 'load-applied-job-statistic'},
        dataType: 'json'
    }).done(function (data) {
        if(typeof data.open !='undefined') {
            $("#appliedJobStatisticDesktop ul li .number.open, #appliedJobStatisticMobile ul li .number.open").html(data.open);
        }
        if(typeof data.review !='undefined'){
            $("#appliedJobStatisticDesktop ul li .number.review,#appliedJobStatisticMobile ul li .number.review").html(data.review);
        }
        if(typeof data.close !='undefined'){
            $("#appliedJobStatisticDesktop ul li .number.closed,#appliedJobStatisticMobile ul li .number.closed").html(data.close);
        }
        $(".loading-applied-job").hide();
    });
}
var google_conversion_id = 1072665538;
var google_conversion_label = "BRESCMD5mAMQwqe-_wM";
var google_custom_params = window.google_tag_params;
var google_remarketing_only = true;

//    (function() {
//        var gad = document.createElement('script'); gad.type = 'text/javascript'; gad.async = true;
//        gad.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'www.googleadservices.com/pagead/conversion.js';
//        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(gad, s);
//    })();