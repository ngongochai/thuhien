//////////////////////////////////////////////////////
// 1. Summary Box
// 2. Fix top navigation
// 3. Init Fancy Box
// 4. Carousel
// 5. Display back-to-top button
// 6. Reload Carousel
//////////////////////////////////////////////////////
//Make a dot in job title
$(document).on('ready', function () {
    var max_height = 20;
    $('.text-dot').each(function () {
        if ($(this).outerHeight() > max_height) {
            $(this).dotdotdot({
                height: max_height
            })
        }
    })
})
function checkPhoneNumber(st) {
    var _40 = "0123456789-+() ";
    var _41 = trim(st);
    if (_41.length < 7) {
        return false;
    }
    for (i = 0; i < _41.length; i++) {
        ch = _41.charAt(i);
        if (_40.indexOf(ch) < 0) {
            return false;
        }
    }
    return true;
}

function setLocalStorage(key, value) {
    jQuery.cookie(key, value);
}

function getLocalStorage(key) {
    return jQuery.cookie(key);
}

function removeLocalStorage(key) {
    jQuery.removeCookie(key);
}

function initApplyForm() {
    //7. Apply Form - Inline Edit Form
    //-----------------------------------------
    $.fn.editable.defaults.mode = 'inline';
    $.fn.editable.defaults;

    $('#app-title').editable({
        emptytext: currentTitleText,
        emptyclass: "hint",
        onblur: "submit",
        success: function (response, newValue) {
            $('#appJobTitle').val(newValue);
        }
    });

    $('#app-phone').editable({
        emptytext: phoneNumberText,
        emptyclass: "hint",
        onblur: "submit",
        success: function (response, newValue) {
            $('#appPhoneNumber').val(newValue);
        },
        tpl: '<input type="text" maxlength="15">'
    });

    if(optionalCoverLetter == "1" || optionalCoverLetter == '2'){
        $('#app-cover').editable({
            emptytext: coverLetterText,
            emptyclass: "hint",
            rows: 10,
            onblur: "submit",
            onkeyup: limiter('appCoverLetter', 'txtCoverLetterRemain', language, 10000),
            validate: function (value) {
                if ($.trim(value) == '' && optionalCoverLetter == 1) return emptyCoverLetterText;
            },
            success: function (response, newValue) {
                $('#appCoverLetter').val(newValue);
                limiter('appCoverLetter', 'txtCoverLetterRemain', language, 10000);
            }
        });

        $('#app-cover')[0].onmousedown = function (event) {
            event.preventDefault();
            return false;
        };
    }

    var data = {};
    data.action = 'loadApplyForm';
    data.jobid = jobid;
    $.ajax({
        type: 'POST',
        data: data,
        async: false,
        url: '/jobseekers/apply_on_oneclick.php',
        dataType: 'json',
        success: function (data) {
            if (data.status == 'SUCCESS') {
                if (data.preferedLanguage && data.preferedLanguage.selectedid != '3') {
                    $('#appPreferLanguage').css('display', '');
                    $('#appPreferLanguageName').html(data.preferedLanguage.selectedname);
                }

                $('#app-fullName').html(data.userInfo.firstname + " " + data.userInfo.lastname);
                $('#app-userName').html(data.userInfo.username);

                $('#app-title').editable('option', 'value', data.userInfo.jobtitle);
                $('#appJobTitle').val(data.userInfo.jobtitle);

                $('#app-phone').editable('setValue', data.userInfo.cellphone);
                $('#appPhoneNumber').val(data.userInfo.cellphone);

                $('#app-email').text(data.userInfo.username);

                // Process a list of applying resumes
                var resumeApplyList = $.parseJSON(data.resumes);
                if (resumeApplyList.length > 0) {
                    // Reset legacy resume options
                    jQuery("#applyResume").html('');

                    // Render a list of supporting resumes
                    var resumes = jQuery.map(resumeApplyList, function(resume) {
                        var html = '';
                        html += '<div class="row select-resume__item">';
                        html += '<div class="col-sm-12">';
                        html += '<div class="radio radio-success">' +
                                    '<input tabindex="5" type="radio" name="resumeApply" id="optionsRadios' + resume.value + '" value="' + resume.value + '" data-type="' + resume.type + '""' + (!resume.enabled ? ' disabled="disabled"' : '') + ' />' +
                                    '<label for="optionsRadios' + resume.value + '">' +
                                        '<span>' + resume.text + '</span>';

                        if (resume.hint != '') {
                            html += '<span class="small gray-light select-resume__hint">' + resume.hint + '</span>';
                        }

                        html += '</label>'+
                            '</div>' +
                            '</div>';

                        if (resume.attached != 1 && resume.enabled) {
                            html += data.searchable;
                        }

                        html += '</div>';

                        jQuery("#applyResume").append(html);
                    });

                }

                // Choose an first enable resume method
                var resumeOption = $('.select-resume').find('input:radio:enabled').first();
                resumeOption.prop("checked", true);
                $('#appDefaultResume').val(resumeOption.val())

                $('#app-cover').editable('setValue', data.coverletter_row.coverletter);
                $('#appCoverLetter').val(data.coverletter_row.coverletter);

                $('#myModal').modal('show');

                $('#myModal').on('shown.bs.modal', function (e) {
                    if (data.userInfo.cellphone == '' || data.userInfo.cellphone == null) {
                        var appPhone = $('#app-phone');

                        // Focus on phone number if it is empty
                        appPhone.next().find('input.form-control').focus();

                        // Add phone number validation rules
                        appPhone.editable('option', 'validate', function (value) {
                            $('#errorAppPhone').css('display', 'none');
                            if ($.trim(value) == '') return emptyPhoneNumberText;
                            if (!checkPhoneNumber($.trim(value))) return validPhoneNumberText;
                        });

                        appPhone.editable('show', false);
                    }
                });

                applyProcess.init();

                $('#make-searchable').change(function() {
                    $('#appSearchable').val(jQuery(this).prop('checked') ? 1 : 0);
                });
            } else if (data.status == 'REDIRECT') {
                window.location.href = data.redirectUrl;
            } else if (data.status == 'ERROR') {
                $('#errorAlertPopup').removeClass('hide').addClass('in').slideDown();
                setTimeout(function () {
                    $('#errorAlertPopup').slideUp();
                }, 5000);
            } else {
                $('#errorAlertPopup').removeClass('hide').addClass('in').slideDown();
                setTimeout(function () {
                    $('#errorAlertPopup').slideUp();
                }, 5000);
            }
        }
    });
}

$(document).on('ready', function () {

    // 1. Summary Box
    //-----------------------------------------

    var $SummaryBox = $('.summary-box');
    var $body = $('body');
    var $SectionJobDetail = $('#section-job-detail');
    var $JobInfo = $('.job-info');
    var bottomMost;

    // Re-calculate & fix the left position of the SummaryBox when the window resize.

    if (!!$('.dynamic-navbar')) {
        $(document).scroll(function () {

            var dynamic_navbar_top = $('#what-we-offer').offset().top;
            var winTop = $(window).scrollTop();

            if ($body.width() >= 992) {
                bottomMost = $SectionJobDetail.offset().top + $SectionJobDetail.outerHeight();
            } else {
                bottomMost = $JobInfo.offset().top + $JobInfo.outerHeight();
            }


            // 2. Fix Navigation
            //-----------------------------------------
            if (winTop > dynamic_navbar_top) {
                $('.dynamic-navbar').addClass('navbar-fixed-top').fadeIn(200).removeClass('hide');
            } else {
                $('.dynamic-navbar').fadeOut(200).promise().done(function () {
                    $(this).removeClass('navbar-fixed-top');
                });
            }
        });
    }

    // 3. Fancy Box
    //-----------------------------------------

    $('.multi-carousel a').fancybox(
        {
            helpers: {
                media: true
            }
        }
    );


    // 4. Carousel
    //-----------------------------------------

    // Init the Multi-Carousel
    var $multiCarousel = $('.multi-carousel');

    // Make the carousel responsive
    $multiCarousel.on('jcarousel:createend jcarousel:reload', function () {
        var element = $(this),
            width = element.innerWidth(),
            totalItem = $('.multi-carousel li').length;
        if (totalItem === 2) {
            width = width / 2;
        }
        else if (totalItem === 1) {
            width = element.innerWidth();
        } else {
            if (width > 400) {
                width = width / 3;
            } else if (width > 300) {
                width = width / 2;
            }
            else {
                width = element.innerWidth();
            }
        }


        element.jcarousel('items').each(function () {
            $(this).css({
                'width': width + 'px'
            })
        });

        if (totalItem > 1) {
            // Controls
            $('.jcarousel-control-prev').jcarouselControl({
                target: '-=1'
            });

            $('.jcarousel-control-next').jcarouselControl({
                target: '+=1'
            });

            // Pagination
            $('.jcarousel-pagination')
                .on('jcarouselpagination:active', 'a', function () {
                    $(this).addClass('active');
                })
                .on('jcarouselpagination:inactive', 'a', function () {
                    $(this).removeClass('active');
                })
                .on('click', function (e) {
                    e.preventDefault();
                })
                .jcarouselPagination({
                    perPage: 1,
                    item: function (page) {
                        return '<a href="#' + page + '">' + page + '</a>';
                    }
                });
        } else {
            $('.jcarousel-pagination,.jcarousel-control-prev,.jcarousel-control-next').remove();
        }

    });

    // Equalize the height of all items
    function multiCarouselHeight() {
        var element = $('.multi-carousel'), shortestHeight;

        shortestHeight = element.find('img:first-child').height();

        element.find('img').each(function () {

            if ((shortestHeight - $(this).height()) > 0) {
                shortestHeight = $(this).height();
            }
            if (shortestHeight < 100) {
                shortestHeight = 100;
            }
        });
        element.find('a').css({'height': shortestHeight});
        element.find('img').each(function () {
            var elementHeight = $(this).height();
            $(this).css({
                'margin-top': -((elementHeight - shortestHeight) / 2)
            });
        })

    }

    $multiCarousel.on('jcarousel:createend jcarousel:reloadend', function () {
        multiCarouselHeight();
    });

    $multiCarousel.jcarousel({
        wrap: 'circular',
        animation: 'fast'
    });
    $('.fade-in-on-page-loaded').fadeIn().addClass('in');
    $('.multi-carousel').jcarousel('reload');

    // 6. Reload Carousel after the window is loaded to fit the images
    //------------------------------------------------------------
    $(window).load(function () {
        $('.multi-carousel').jcarousel('reload');
    });


    // 5. Display back-to-top button
    //-----------------------------------------

    var backToTopConfig = {
        duration: 500,
        start_top: 200,
        btn: $('.go-top:first')
    };

    $(document).scroll(function () {
        var winTop = $(window).scrollTop();
        if (winTop > backToTopConfig.start_top) {
            backToTopConfig.btn.removeClass('hide').fadeIn(backToTopConfig.duration);
        } else {
            backToTopConfig.btn.fadeOut(backToTopConfig.duration);
        }
    });

    // 6. Suggested Jobs
    //-----------------------------------------

    // Hide in mobile at start and when it reaches the footer
    var $floatSidebar = $('.float-sidebar'),
        $selectJob = $('.selected-table-wrapper'),
        footerTop = $('footer').offset().top;

    if ($floatSidebar.length > 0) {
        $floatSidebar.addClass('animated fadeInUp').show();
        $(document).scroll(function () {

            var bodyWidth = $body.width(),
                floatSidebarTop,
                winTop,
                titleTop;

            if (bodyWidth >= 768 && bodyWidth <= 1600) {
                floatSidebarTop = $floatSidebar.offset().top;

                if (footerTop <= floatSidebarTop) {
                    $floatSidebar.addClass('animated fadeOutDown').removeClass('fadeInUp');
                    $('.selected-table-wrapper').addClass('animated fadeOutDown').removeClass('fadeInUp');
                } else {
                    $floatSidebar.addClass('animated fadeInUp').removeClass('fadeOutDown');
                    if ($selectJob.find('tbody tr').length > 1) {
                        $('.selected-table-wrapper').addClass('animated fadeInUp').removeClass('fadeOutDown');
                    }
                }

            } else if (bodyWidth < 768) {
                winTop = $(window).scrollTop();
                floatSidebarTop = $floatSidebar.offset().top;
                footerTop = $('footer').offset().top;
                titleTop = $('#job-detail').offset().top;

                if (footerTop >= floatSidebarTop && winTop >= titleTop) {
                    if (!$floatSidebar.hasClass('in')) {
                        $floatSidebar.stop().addClass('in').slideDown().promise().done(function () {
                            $('#suggested-job-carousel').jcarousel('reload');
                        });
                    }
                } else {
                    $floatSidebar.slideUp().removeClass('in');
                }
            }
        });
    } else {
        // If there is no suggested jobs
        if ($selectJob.length > 0) {

            $selectJob.addClass('no-suggested-job');

            $(document).scroll(function () {
                var bodyWidth = $body.width(),
                    selectJobTop;

                // Hide it when it's over the footer
                if (bodyWidth >= 768 && bodyWidth <= 1600) {
                    selectJobTop = $selectJob.offset().top;
                    footerTop = $('footer').offset().top;
                    if (footerTop <= selectJobTop) {
                        if ($selectJob.find('tbody tr').length > 1) {
                            $selectJob.addClass('anmiated fadeOutDown').removeClass('fadeInUp');
                        }
                    } else {
                        $selectJob.addClass('animated fadeInUp').removeClass('fadeOutDown');
                    }
                }
            });
        }
    }

    // Show Select Job Panel if it exists and there are selected jobs (minus a template row)
    if (($selectJob.length) > 0 && ($selectJob.find('tbody tr').length > 1)) {
        $selectJob.addClass('animated fadeInUp').show();
    }

    $("#appCoverLetter").on('change', function () {
        limiter('appCoverLetter', 'txtCoverLetterRemain', language, 10000);
    });

    $('#fileAttach').click(function () {
        $('#optionsRadios3').prop('checked', true);
        $('.select-resume__make-searchable').hide();
    });

    // Show uploading filename whenever user choose a new resume
    $("#fileAttach").change(function(e) {
        var fileName = $(this).val().split('/').pop().split('\\').pop();
        $('#resume-filename').text(fileName);
    });

    $('#optionsRadios3').change(function (e) {
        $('.select-resume__make-searchable').hide();
    });

    $('#applySendProcessBtn').click(function () {
        setTimeout(function () {
            if (checkApplyForm()) {
                resumeType = $('input[name=resumeApply]:checked').attr('data-type');
                $('#appliedType').val(resumeType);

                applyUTM('applyJobForm');
                $('#applyJobForm').submit();
            }
        }, 50);
    });

    removeLocalStorage('phone_number');
});

function getQueryParams(name){
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);

    return (null === results) ? '' : results[1] || 0;
}

function applyUTM(formId) {
    var form = jQuery('#' + formId);
    var utm_source = getQueryParams('utm_source');
    var utm_medium = getQueryParams('utm_medium');
    var utm_term = getQueryParams('utm_term');
    var utm_content = getQueryParams('utm_content');
    var utm_campaign = getQueryParams('utm_campaign');

    form.append('<input type="hidden" name="utms[utm_source]" value="' + utm_source + '">');
    form.append('<input type="hidden" name="utms[utm_medium]" value="' + utm_medium + '">');
    form.append('<input type="hidden" name="utms[utm_term]" value="' + utm_term + '">');
    form.append('<input type="hidden" name="utms[utm_content]" value="' + utm_content + '">');
    form.append('<input type="hidden" name="utms[utm_campaign]" value="' + utm_campaign + '">');
}

var options = {
    //target:        '#output2',   // target element(s) to be updated with server response
    beforeSubmit: showRequest,  // pre-submit callback
    success: showResponse,  // post-submit callback
    error: showApplyError,

    // other available options:
    //url:       url         // override for form's 'action' attribute
    type: 'post',        // 'get' or 'post', override for form's 'method' attribute
    dataType: 'json'        // 'xml', 'script', or 'json' (expected server response type)
    //clearForm: true        // clear all form fields after successful submit
    //resetForm: true        // reset the form after successful submit

    // $.ajax options can be used here too, for example:
    //timeout:   3000
};

$('#applyJobForm').on('submit', function (e) {
    e.preventDefault(); // <-- important
    // inside event callbacks 'this' is the DOM element so we first
    // wrap it in a jQuery object and then invoke ajaxSubmit
    $(this).ajaxSubmit(options);

    // !!! Important !!!
    // always return false to prevent standard browser submit and page navigation
    return false;
});

// pre-submit callback
function showRequest(formData, jqForm, options) {
    $('#applyCancelProcessBtn').css('display', 'none');
    $('#applySendProcessBtn').css('display', 'none');
    $('#applySendingProcessBtn').css('display', '');
    return true;
}

function showResponse(data) {
    $('#applyCancelProcessBtn').css('display', '');
    $('#applySendProcessBtn').css('display', '');
    $('#applySendingProcessBtn').css('display', 'none');
    if (data.status == 'REDIRECT') {
        // Tracking resume type with Google Analytics
        var resumeType = $('.select-resume').find('input:radio:checked').attr('data-type');

        if (resumeType == 'online') {
            _gaq.push(['_trackEvent', 'ApplyJob', 'Click', 'APPLY_ONLINE']);
        }
        else if (resumeType == 'last_attachment') {
            _gaq.push(['_trackEvent', 'ApplyJob', 'Click', 'APPLY_OLD']);
        }
        else if (resumeType == 'new_attachment') {
            _gaq.push(['_trackEvent', 'ApplyJob', 'Click', 'APPLY_NEW']);
        }

        window.location.href = data.redirectUrl;
    } else if (data.status == 'ERROR') {
        if (data.field == 'onlineResumeId') {
            $('#errorAppResume').css("display", "");
        } else if (data.field == 'coverLetter') {
            $('#errorCover').css("display", "");
        } else if (data.field == 'resumeFile') {
            $('#errorAppResumeFile').css("display", "");
        } else {
            $('#errorAppSystem').html(data.message);
            $('#errorAppSystem').css("display", "");
        }
    } else  if ( data.status =='SYSTEM_ERROR') {
        $('#errorAppResumeFile').css("display", "");
    }
}

function showApplyError(xhr, status, error) {
    $('#applyCancelProcessBtn').css('display', '');
    $('#applySendProcessBtn').css('display', '');
    $('#applySendingProcessBtn').css('display', 'none');
    if (xhr.status == 413) {
        $('#errorAppResumeFile').css("display", "");
    } else {
        $('#errorAppSystem').html(data.message);
        $('#errorAppSystem').css("display", "");
    }
}

function checkApplyForm() {
    hasError = false;
    $("span.error-message").css('display', 'none');
    if ($('#app-phone').parent().find('input.form-control').length > 0 &&
        $.trim($('#app-phone').parent().find('input.form-control').text()) == "") {
        hasError = true;
    } else if ($.trim($('#appPhoneNumber').val()) == "") {
        $('#errorAppPhone').css("display", "");
        hasError = true;
    }
    if (optionalResume == 1) {
        resumeType = $('input[name=resumeApply]:checked').attr('data-type');
        if (resumeType == '') {
            $('#errorAppResume').css("display", "");
            hasError = true;
        } else {
            if (resumeType == 'new_attachment') {
                if ($('#fileAttach').val() == '') {
                    $('#errorAppResume').css("display", "");
                    hasError = true;
                }
            } else {
                if ($('#appDefaultResume').val() <= 0) {
                    $('#errorAppResume').css("display", "");
                    hasError = true;
                }
                if (resumeType != 'online' && resumeType != 'last_attachment') {
                    $('#errorAppResume').css("display", "");
                    hasError = true;
                }
            }
        }
    }
    if (optionalCoverLetter == 1) {
        if ($('#app-cover').parent().find('textarea.form-control').length > 0 &&
            $.trim($('#app-cover').parent().find('textarea.form-control').text()) == "") {
            hasError = true;
        } else if ($.trim($('#appCoverLetter').val()) == "") {
            $('#errorCover').css("display", "");
            hasError = true;
        }
    }
    if (hasError) {
        return false;
    }
    return true;
}


// Check input status
function checkInputStatus($inputElement) {
    var $thisIconButton = $inputElement.parents('.tag-xs').find('.fa');

    // If input is checked
    if ($inputElement.prop('checked')) {
        $thisIconButton.removeClass('fa-plus btn-add-skill-tag clickable').addClass('fa-check added-tag').parents('.tag-xs').addClass('active')
        $thisIconButton.removeAttr('onclick').off('click').parents('.tag-xs').removeClass('addable').addClass('view-only')
        $thisIconButton.tooltip('destroy')
        $inputElement.attr('disabled', 'disabled')
    }

    // If input is not checked
    else {
        $thisIconButton.tooltip({
            placement: 'top',
            viewport: '.job-tag-add-skill',
            title: tagAddSkill.clickToAdd
        })
    }
}

// Add to my skill
function toggleAddableTag(element) {
    var $thisTag = $(element).parents('.tag-xs');
    var $thisInput = $thisTag.find('input');
    // Check the checkbox to change the status of the tags
    setTimeout(function () {
        checkInputStatus($thisInput)
    }, 50)
}


$('[data-toggle="popover"]').each(function () {
    $(this).popover({
        container: this,
        delay: 200
    })
});

// DOM Loaded
$(function () {
    // Load Skill Section
    $('.job-tag-add-skill-loader').fadeOut().promise().done(function () {
        $('.job-tag-add-skill').fadeIn();
    });

    // Check input status
    $('.job-tag-add-skill').find('input:checkbox').each(function () {
        checkInputStatus($(this))
    });

     // First Letter with color background
    if ($('.initial').length) {
        var thisCompanyName = $('.job-header-info').find('.company-name').text().trim(),
            firstLetter = thisCompanyName.substring(0, 1).toUpperCase(),
            thisBgColor;

        $('.no-company-logo, .with-company-logo').attr('data-target-value', 'false');

        if ($.inArray(firstLetter, ["A", "B", "C", "D"]) > -1) {
            thisBgColor = '#00b9f2';
        } else if ($.inArray(firstLetter, ["E", "F", "G"]) > -1) {
            thisBgColor = '#0085bb';
        } else if ($.inArray(firstLetter, ["H", "I", "J", "K"]) > -1) {
            thisBgColor = '#ba6000';
        } else if ($.inArray(firstLetter, ['L', 'M', 'N']) > -1) {
            thisBgColor = '#ff9900';
        } else if ($.inArray(firstLetter, ["O", "P", "Q", "R"]) > -1) {
            thisBgColor = '#20832a';
        } else if ($.inArray(firstLetter, ['S', 'T', 'U']) > -1) {
            thisBgColor = '#5cb85c';
        } else {
            thisBgColor = '#c9c9c9';
        }
        $('.initial').css({'background-color': thisBgColor}).text(firstLetter);
    }

    $('.company_profile_link').click(function(){
        _gaq.push(['_trackEvent', 'Company Profile', 'Come from Job details']);
    });
});

var applyProcess = {
    initSwitchery: function () {
        var elems = Array.prototype.slice.call(document.querySelectorAll('.switchery'));

        elems.forEach(function(html) {
          var switchery = new Switchery(html, {size: 'small'});

          switchery.element.onchange = function() {
              $('#appSearchable').val(this.checked ? 1 : 0);
          }
        });
    },
    openEmptyField: function () {
        var data = getLocalStorage('phone_number');

        if(data != undefined) {
            return;
        }

        var appPhone = $('#app-phone');
        var thisValObj = appPhone.editable('getValue');
        var thisVal = thisValObj[Object.keys(thisValObj)[0]];

        if (typeof thisVal === "undefined" || $.trim(thisVal) === "") {
            setLocalStorage('phone_number', true);
            appPhone.editable('show', false);
        }
    },
    checkSelectResume: function () {
        $('.select-resume').find('input:radio').each(function () {
            var $self = $(this);
            if ($self.is(':checked')) {
                $self.closest('.select-resume__item').find('.select-resume__make-searchable').fadeIn('fast');
            }

            $self.change(function () {
                if ($self.is(':checked')) {
                    $('#appDefaultResume').val($self.val()); // Set apply resume type

                    $self.closest('.select-resume__item').siblings('.select-resume__item').find('.select-resume__make-searchable').fadeOut('fast').promise().done(function () {
                        $self.closest('.select-resume__item').find('.select-resume__make-searchable').fadeIn('fast');
                    });
                }
            })
        })
    },
    init: function () {
        // this.initSwitchery();
        this.checkSelectResume();
        // this.openEmptyField();
    }
};
