/////////////////////////////////////////////////////////////////////
// I. General
// --------------------------------------------------------------
// 1. Misc
// 2. Get content from input and save to the "view controls"
// 3. Reset content of a form function
// 4. Edit/Save form
// --------------------------------------------------------------
// II. Header
// --------------------------------------------------------------
// 1. Change Avatar
// --------------------------------------------------------------
// III. Card Section
// --------------------------------------------------------------
// IV. Contact Information
// --------------------------------------------------------------
// V. Summary Box
// --------------------------------------------------------------
// 1. Misc
// 2. Language Proficiency
// 3. Salary
// --------------------------------------------------------------
// VI. Key Skills, Profile Objectives
// --------------------------------------------------------------
// VII. Employment History, Education History, Reference Sections
// --------------------------------------------------------------
// 1. Misc
// 2. Calculate the number of years and months: To Date - From Date. e.g "5 years 2 months"
// 3. Add/ Remove from samples
// --------------------------------------------------------------
// VIII. Others
/////////////////////////////////////////////////////////////////////


// I. General
// ----------------------------------------------------

// 1. Misc
// ----------------------------

// Remove all error messages on focus
$(document).on('focus change', 'select, input ,textarea ', function () {
    "use strict";
    var $hasErrorElement = $(this).parents('.has-error');
    $hasErrorElement.find('.error-message').fadeOut().promise().done(function () {
        $(this).attr('hidden', 'hidden').removeAttr('style');
    });
    $hasErrorElement.removeClass('has-error');
});

// Vietnamese translation for bootstrap-datepicker
(function ($) {
    "use strict";
    $.fn.datepicker.dates['vi'] = {
        days: ["Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy", "Chủ nhật"],
        daysShort: ["CN", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "CN"],
        daysMin: ["CN", "T2", "T3", "T4", "T5", "T6", "T7", "CN"],
        months: ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"],
        monthsShort: ["Th1", "Th2", "Th3", "Th4", "Th5", "Th6", "Th7", "Th8", "Th9", "Th10", "Th11", "Th12"],
        today: "Hôm nay",
        clear: "Xóa",
        format: "dd/mm/yyyy"
    };
}(jQuery));

// Scroll page to any element and click it
var goToElement = function (gotoId) {
    var elementId = $(gotoId);
    $('html, body')
        .animate({
            scrollTop: elementId.offset().top
        }, 1000).promise().done(function () {
        $(gotoId).find('.form-wrapper').trigger('click');
    });
};

// Scroll page to any element with 100px offset
function scrollToThisSection(element) {
    "use strict";
    $("html, body").animate({
        scrollTop: $(element).offset().top - 100
    }, 200);
}

// Count down maxlength
function updateCountdown(element, thisCounter, maxLength, isTinyMCE) {
    var remaining;
    if (isTinyMCE == true) {

        remaining = maxLength - $(element.getContent()).text().length;

    } else {
        remaining = maxLength - $(element).val().length;
    }
    $(thisCounter).text(remaining + " " + $(thisCounter).data('text'));
}

// Calculate "To-Date";
function calculateToDate($formWrapper) {
    var $toDateEditControl = $formWrapper.find('input.datepicker[data-control="to-date"]');
    var $toDateViewControl = $formWrapper.find('span.view-control[data-control="to-date"]');
    var $presentCheckbox = $formWrapper.find('.to-present-checkbox');
    var toDateText = "";
    if ($presentCheckbox.is(":checked")) {
        toDateText = $presentCheckbox.data('text-value');
        $toDateViewControl.text(toDateText).removeClass('placeholder');
    } else if (($toDateEditControl.val() !== "")) {
        toDateText = $toDateEditControl.val();
        $toDateViewControl.text(toDateText).removeClass('placeholder');
    } else {
        toDateText = $toDateEditControl.attr('placeholder');
        $toDateViewControl.text(toDateText).addClass('placeholder');
    }
}


$(function () {

    // Init Select2
    $('#education').select2({width: "100%", minimumResultsForSearch: "-1"});
    $('.current-language-select, .language-level-select, #city, #country,#nationality, #district').select2({width: "100%"});
    $('#current-job-level, #expected-job-level').select2({width: "100%", minimumResultsForSearch: "-1"});
    $('#expected-location,#expected-job-category').select2({width: "100%", maximumSelectionSize: 3});
    $('select.qualification').filter(function () {
        if (!$(this).parents('form').hasClass('sample')) {
            $(this).select2({width: "100%", minimumResultsForSearch: "-1"});
        }
    });

    // Init TinyMCE
    $('.countdown-target').each(function () {
        if (!$(this).parents('form').hasClass('sample')) {
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
                        if ($(editor.getContent()).text().length >= numberOfCharacterAllowed && !(key == 46 || key == 8 || key == 37 || key == 39 )) {
                            event.preventDefault();
                            event.stopPropagation();
                            return false;
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
        }
    });

    //Init Tooltips
    $('[data-toggle="tooltip"]').tooltip({delay: 200});

});


// ----------------------------------------------------------------------
goToPosition = function (elementId, offset) {
    "use strict";

    if (typeof offset === "undefined") {
        offset = 0;
    }

    if (typeof $(elementId).offset() !== "undefined") {
        $('body,html').animate({
            scrollTop: $(elementId).offset().top - offset
        }, 300);
    }

};


// II. Header
// ----------------------------------------------------


// IV. Contact Information
// ----------------------------------------------------

// Init date picker
$(function () {
    $(document).on('mouseenter', '.datepicker', function () {
        $(this).addClass('hover');
    }).on('mouseleave', '.datepicker', function () {
        $(this).removeClass('hover');
    });

    var $dateOfBirth = $('#date-of-birth');
    var $datePicker = $('.datepicker');
    var $dayPicker = $('.day-picker');

    if (languageID == 2) {
        $dateOfBirth.datepicker({format: 'dd/mm/yyyy', language: 'en', autoclose: true, toggleActive: false});
        $dayPicker.datepicker({format: 'dd/mm/yyyy', language: 'en', autoclose: true, toggleActive: false});
        $datePicker.datepicker({format: 'mm/yyyy', startDate: '-50y', minViewMode: 'months', language: 'en', autoclose: true, toggleActive: false});
    } else {
        $dateOfBirth.datepicker({format: 'dd/mm/yyyy', language: 'vi', autoclose: true, toggleActive: false});
        $dayPicker.datepicker({format: 'dd/mm/yyyy', language: 'vi', autoclose: true, toggleActive: false});
        $datePicker.datepicker({format: 'mm/yyyy', startDate: '-50y', minViewMode: 'months', language: 'vi', autoclose: true, toggleActive: false});
    }

    $dateOfBirth.blur(function () {
        if (!$('.datepicker').hasClass('hover')) {
            $(this).datepicker('hide');
        }
    });

    $datePicker.blur(function () {
        if (!$('.datepicker').hasClass('hover')) {
            $(this).datepicker('hide');
        }
    });
});

// VII. Employment History, Education History, Reference Sections
// ----------------------------------------------------------------------

// 1. Misc
// ---------------------------

$(function () {
    "use strict";

    // Header & Avatar
    //==================================
    $('.current-highest-education').click(function (e) {
        e.stopPropagation();
        $('body,html').animate({
            'scrollTop': $('#form-education').offset().top
        }).promise().done(function () {
            var $firstForm = $('#form-education').find('.form-wrapper.form-view:not(:hidden)').eq(0);
            $firstForm.click();
            if ($firstForm.length === 0) {
                $('#btn-add-education').click();
            }
        });
    });

    $('.current-company').click(function (e) {
        e.stopPropagation();
        $('body,html').animate({
            'scrollTop': $('#form-employment').offset().top
        }).promise().done(function () {
            // Open the form with the latest date or add new if there is none;

            var $toDates = $('#form-employment').find('.view-control').not('.placeholder').filter('[data-control = "to-date"]');

            var latestDate = moment($toDates.eq(0).text(), "MM/YYYY");
            var $latestDate;

            $.each($toDates, function (i, el) {
                var thisDate = moment($(el).text(), "MM/YYYY");
                if (thisDate.isSameOrAfter(latestDate)) {
                    latestDate = thisDate;
                    $latestDate = $(el);
                }
            });

            var $latestForm = $latestDate.closest('.form-wrapper');
            $latestForm.click();
            if ($latestForm.length === 0) {
                $('#btn-add-employment').click();
            }
            // Wait until all animations are completed, then focus on the company name;
            $(':animated').promise().done(function () {
                $('.company-name').filter(':visible').focus();
            });
        });
    });

    // Contact Details
    //==================================
    // Toggle Contact Details
    $('.contact-information-toggler').click(function () {
        $('.section-contact').slideToggle('fast');
    });

});

var removeAvatar = function () {
    "use strict";
    $('.avatar').empty().append('<div class="avatar-sample"><i class="fa fa-3x fa-user"></i></div>');
    $('.dropdown-remove-avatar-btn').closest('li').hide().prev('.divider').hide();
};

var mdlKeySkill = (function () {
    "use strict";

    var $tags = {
        tagList: $('#tags-edit'),
        deleteButtonClass: '.ic-close',
        addButton: $('#btn-add-tag'),
        nameInput: $('#key-skills'),
        editMode: false,
        template: $('<span class="tag-xs tag-new"><span class="tag-name"></span><a href="#" class="ic-close"><i class="fa fa-fw fa-remove"></i></a><input class="input-tag-name" type="hidden" name="skills[]" data-tag-name=""></span>)'),
        addable: true
    };

    var NoLimitSkill = '50';

    var validMaxNumberofSkill = function (number) {
        var numberOfVisibleSkill = $('#tags-edit').find('.tag-xs:visible').length, lessThan5 = numberOfVisibleSkill < parseInt(number, 10);
        if (lessThan5 === false) {
            $('#key-skill-input-wrapper').slideUp();
            $('#key-skill-limit-error').stop().slideDown();
            $tags.addable = false;
        } else {
            $tags.addable = true;
            $('#key-skill-input-wrapper').slideDown();
            $('#key-skill-limit-error').stop().slideUp();

        }
    };


    // Add Tag
    var addTag = function (id, strName) {
        if ($tags.addable && $.trim(strName) !== "") {
            var tempHTML = $tags.template.find('.tag-name').text(strName).end().find('.input-tag-name').attr('data-tag-name', $tags.nameInput.val()).val(id).end();
            tempHTML.clone().hide().appendTo($tags.tagList).fadeIn('fast');

            // Validate after Add
            validMaxNumberofSkill(NoLimitSkill);
        }
        $tags.nameInput.val("");
        // If input is a "typeahead" object then use the typeahead function
        if ($.fn.typeahead) {
            $tags.nameInput.typeahead('val', "");
        }
    };

    // Highlight Tag
    var highLightTag = function (id) {
        var highLightDuration = 2000, $thisTag = $('input[value="' + id + '"]').parents('.tag-xs');
        $thisTag.addClass('highlighted');
        $tags.nameInput.val("").focus();
        if ($.fn.typeahead) {
            $tags.nameInput.typeahead('val', "").focus();
        }
        setTimeout(function () {
            $thisTag.removeClass('highlighted');
        }, highLightDuration);
    };

    // Save Skill
    var saveSkill = function () {
        $('#edit-skill-actions, #skill-edit-mode').fadeOut().promise().done(function () {
            var tempHTML = $('#skill-edit-mode')
                .find('#tags-edit').clone().find('.ic-close').remove()
                .end().find('.tag-new').removeClass('tag-new')
                .end().html();
            $('#skill-view-mode').hide().html(tempHTML).fadeIn();
            $('#tags-edit').find('.tag-xs').filter(function () {
                if ($(this).css('display') === "none") {
                    return true;
                }
            }).remove();
        });
        $tags.nameInput.val("");
        if ($.fn.typeahead) {
            $tags.nameInput.typeahead('val', "");
        }
        $('#skill-edit-mode').find('.tag-new').removeClass('tag-new');
        $('#open-edit-skill').fadeIn();
        $tags.editMode = false;
    };

    // Cancel Save Skill
    var cancelSaveSkill = function () {
        $('#tags-edit').find('.tag-xs').fadeIn().find('input').removeAttr('disabled');
        $tags.nameInput.val("").parents('.has-error').removeClass('has-error');
        if ($.fn.typeahead) {
            $tags.nameInput.typeahead('val', "");
        }
        $('#edit-skill-actions, #skill-edit-mode, #key-skill-error').fadeOut().promise().done(function () {
            $('#skill-edit-mode').find('.tag-new').remove();
            $('#skill-view-mode').fadeIn();
        });
        $tags.editMode = false;
        $('#open-edit-skill').fadeIn();
        $('.skill-section').removeClass('edit-mode').find('.form-btn-edit').show();
    };


    var initModule = function () {
        // Enter to add skill tags
        $tags.nameInput.keyup(function (e) {
            if (e.keyCode === 13) {
                $tags.addButton.click();
            }
        });

        // Stop Propergation
        $('.skill-section button').click(function (e) {
            e.stopPropagation();
        });

        // Delete Tag
        $tags.tagList.on('click', $tags.deleteButtonClass, function (e) {
            e.preventDefault();
            $(this).parents('.tag-xs').animate({width: 'toggle', opacity: 'toggle'}, 'fast').find('.input-tag-name').attr('disabled', 'disabled');
            validMaxNumberofSkill(parseInt(NoLimitSkill, 10) + 1);
            $tags.nameInput.val("").focus();
            if ($.fn.typeahead) {
                $tags.nameInput.typeahead('val', "").focus();
            }

        });

        // Load data from "edit section" to "view section"
        saveSkill();

        // Open Edit Skill
        $('#open-edit-skill button,.skill-section').click(function () {
            if ($tags.editMode === false) {
                $('#open-edit-skill').fadeOut();
                $(this).parents('.form-btn-edit').fadeOut();
                $('#skill-view-mode').fadeOut('fast').promise().done(function () {
                    $('#skill-edit-mode,#edit-skill-actions').fadeIn('fast').promise().done(function () {
                        $('#key-skills').focus();
                        // Check maximum number of skills
                        validMaxNumberofSkill(NoLimitSkill);
                    });

                });

                $tags.editMode = true;
            }
        });
    };

    // Public API
    return {
        highLightTag: highLightTag,
        saveSkill: saveSkill,
        cancelSaveSkill: cancelSaveSkill,
        addTag: addTag,
        validMaxNumberofSkill: validMaxNumberofSkill,
        init: initModule
    };

})();

var mdlBenefit = (function () {
    "use strict";

    var $benefit = {
        editMode: false,
        editModeSection: $('#benefit-edit-mode')
    };

    // Check if the tag is activate based on the checkboxes;
    var checkTags = function ($thisLabel) {
        if ($thisLabel.find('input').is(':checked')) {
            $thisLabel.addClass('active');
            $thisLabel.find('.ic-check .fa').addClass('fa-check');
        } else {
            $thisLabel.removeClass('active');
            $thisLabel.find('.ic-check .fa-check').removeClass('fa-check');
        }
    };

    // If the number of selected tags is more than 5, disable the rest;
    var checkSelectedTags = function () {
        var numberOfSelectedTags = $benefit.editModeSection.find('.active').length;

        if (numberOfSelectedTags >= 5) {
            $('.max-number-of-benefits').slideDown();
            $benefit.editModeSection.find('input:not(":checked")').attr('disabled', 'disabled').parents('label').addClass('disabled');
        } else {
            $benefit.editModeSection.find('input:not(":checked")').removeAttr('disabled').parents('label').removeClass('disabled');
            $('.max-number-of-benefits').slideUp();
        }

        if (numberOfSelectedTags === 0) {
            $('.min-number-of-benefits').slideDown();
            $('#saveBenefitBtn').css('display', 'none');
        } else {
            $('.min-number-of-benefits').slideUp();
            $('#saveBenefitBtn').removeAttr('style');
        }

    };

    // Save benefits
    var saveExpectedBenefit = function () {
        $('#edit-benefit-actions, #benefit-edit-mode').fadeOut().promise().done(function () {
            $('#benefit-view-mode').html($benefit.editModeSection.html()).find('input, .max-number-of-benefits').remove().end().fadeIn();
        });

        $('#open-edit-benefit').fadeIn();
        $benefit.editMode = false;
    };

    // Cancel Save Benefit
    var cancelSaveBenefit = function () {
        // Fade Out Edit Elements then Fade In View Elements
        $('#edit-benefit-actions, #benefit-edit-mode').fadeOut().promise().done(function () {
            $('#benefit-view-mode, #open-edit-benefit').fadeIn();
        });

        // Restore all inputs to its original state
        $benefit.editModeSection.find('input').each(function () {
            $(this).prop('checked', $(this).data('current_state'));
        }).promise().done(function () {
            $benefit.editModeSection.find('label').each(function () {
                checkTags($(this));
                checkSelectedTags();
            });
        });

        // Set Edit Mode to false
        $benefit.editMode = false;
    };

    // Init module (after DOM is loaded): check & load content
    var initModule = function () {
        // Check active tags after DOM is loaded & whenever the label is clicked;
        $benefit.editModeSection.find('label').each(function () {
            checkTags($(this));
            checkSelectedTags();
        }).click(function () {
            checkTags($(this));
            checkSelectedTags();
        });

        // Load content from "edit section" to "view section";
        saveExpectedBenefit();

        // Open Edit Benefit
        $('.benefit-section').click(function () {
            if ($benefit.editMode === false) {
                $(this).parents('.form-btn-edit').fadeOut();
                $('#benefit-view-mode').fadeOut('fast').promise().done(function () {
                    $('#benefit-edit-mode,#edit-benefit-actions').fadeIn('fast');
                });

                // Save input state every time edit mode is triggered
                $benefit.editModeSection.find('input').each(function () {
                    $(this).data('current_state', $(this).prop('checked'));
                });

                // Set Edit Mode to true
                $benefit.editMode = true;

            }
        });
    };

    // Public API
    return {
        saveExpectedBenefit: saveExpectedBenefit,
        cancelSaveBenefit: cancelSaveBenefit,
        init: initModule
    };

})();

var mdlLiveEdit = (function () {
    "use strict";

    // Get content from input and save to the "view controls" function
    var getContent = function (element) {

        $(element).find('.view-control').each(function () {
            var dataType = $(this).data('type');
            var dataControl = $(this).data('control');

            // Select Element
            if (dataType === 'select') {
                var selectEditControl = $(this).parents('.view-field').siblings('.edit-field').find('select.edit-control').filter(function () {
                    if ($(this).data('control') === dataControl) {
                        return true;
                    }
                });

                // If there is no value, use the placeholder
                var selectText = selectEditControl.find('option:selected').text();
                var selectValue = selectEditControl.find('option:selected').val();
                if (selectValue === undefined || selectValue.trim() === "" || selectValue < 0) {
                    selectText = selectEditControl.data('placeholder');
                    $(this).addClass('placeholder');
                } else {
                    $(this).removeClass('placeholder');
                }
                $(this).text(selectText);
            }

            // Multiple Select Element
            else if (dataType === 'multiple-select') {
                var multipleSelectValue = "";
                var $mulSelectCtrl = $(this).parents('.view-field').siblings('.edit-field').find('select.edit-control').filter(function () {
                    if ($(this).data('control') === dataControl) {
                        return true;
                    }
                });
                var $selectedOptions = $mulSelectCtrl.find('option:selected');
                var noSelectedOptions = $selectedOptions.length;
                $selectedOptions.each(function (index) {
                    if (index === noSelectedOptions - 1) {
                        multipleSelectValue += $(this).text();
                    } else {
                        multipleSelectValue += $(this).text() + ", ";
                    }
                });

                // If there is no value, use the placeholder
                if (multipleSelectValue.trim() === "" || multipleSelectValue === undefined) {
                    multipleSelectValue = $mulSelectCtrl.data('placeholder');
                    $(this).addClass('placeholder');
                } else {
                    $(this).removeClass('placeholder');
                }
                $(this).text(multipleSelectValue);
            }

            // Radio buttons
            else if (dataType === "radio") {
                var radioVal = $(this).parents('.view-field').siblings('.edit-field').find('.edit-control:checked').data('text-value');
                if (!radioVal) {
                    radioVal = "";
                }
                $(this).text(radioVal);
            }
            // Manual
            else if (dataType === "manual") {
                return true;
                // do nothing
            }
            // Other cases: text input, text area
            else {
                var textEditCtrl = $(this).parents('.view-field').siblings('.edit-field').find('.edit-control').filter(function () {
                    if ($(this).data('control') === dataControl) {
                        return true;
                    }
                });
                var textVal = textEditCtrl.val();

                // If there is no value, use the placeholder
                if (textVal === undefined || textVal.trim() === "") {
                    textVal = textEditCtrl.attr('placeholder');
                    $(this).addClass('placeholder');
                } else {
                    $(this).removeClass('placeholder');
                }
                // Add html to field
                $(this).html(textVal);
            }
        });

    };

    // Reset the form function
    var resetForm = function (element) {
        $(element).find('input.edit-control, textarea.edit-control').each(function () {
            $(this).val($(this).data('current_data'));

            if ($(this).is(':checkbox') || $(this).is(':radio')) {
                $(this).prop('checked', $(this).data('current_data')).change();
            } else {
                $(this).val($(this).data('current_data'));
            }
        });
        $(element).find('select.edit-control').each(function () {
            $(this).select2('val', $(this).data('current_data'));
        });
    };

    // Check latest education
    var checkLatestEducationLevel = function ($formWrapper) {

        var $qualification = $formWrapper.closest('.section-education').find('select.qualification option:selected');
        var baseWeight = 0;
        var highestLevel = '';

        $('html').attr('lang') === "en" ? highestLevel = "Add Education" : highestLevel = "Thêm Học Vấn Bằng Cấp";
        $qualification.each(function () {
            if ($(this).data('weight') > baseWeight) {
                baseWeight = $(this).data('weight');
                highestLevel = $(this).text()
            }
        });

        if (!!highestLevel) {
            $('.current-highest-education').text(highestLevel);
        }
    };

    // Save Content: some misc/special features & Turn to View Mode
    var saveContent = function (thisFormWrapper) {
        
        // Get content from input and save to the "view controls" function
        getContent(thisFormWrapper);

        // Language Section Case: validate language
        var languageIsValid = true;

        if ($(thisFormWrapper).closest('.section-languages').length) {
            var thisVal = $(thisFormWrapper).find('select').val();
            $(thisFormWrapper).siblings('.form-wrapper').not('.sample').each(function () {
                if ($(this).find('select').val() === thisVal) {
                    $('.form-wrapper.edit-mode .lang-chosen-already').fadeIn('fast');
                    languageIsValid = false;
                    return false;
                } else {
                    $('.form-wrapper.edit-mode .lang-chosen-already').fadeOut('fast');
                    languageIsValid = true;
                }
            })
        }

        // Check if language is valid or not
        if (!languageIsValid) {
            return;
        }

        if ($(thisFormWrapper).hasClass('opened-section')) {
            $(thisFormWrapper).removeClass('opened-section');
        }
        $(thisFormWrapper).removeClass('edit-mode new-section').find('.edit-field').fadeOut('fast').promise().done(function () {
            $(thisFormWrapper).find('.view-field').fadeIn('fast');
        });
        $(thisFormWrapper).find('.edit-control').each(function () {
            if ($(this).is(':checkbox') || $(this).is(':radio')) {
                $(this).data('current_data', $(this).prop('checked')).change();
            } else {
                $(this).data('current_data', $(this).val());
            }

        });
        // Show the add section button
        thisFormWrapper.parents('fieldset').find('.add-one-more-section').fadeIn('fast');

        // Summary Case
        if (thisFormWrapper.is('.form-summary')) {
            var $addSectionBtn = thisFormWrapper.closest('fieldset').find('.add-one-more-section');
            if ($.trim($('#profile-objective').val()) !== "") {
                $addSectionBtn.addClass('hide');
            } else {
                $addSectionBtn.removeClass('hide');
            }
        }

        // Working Preference Case
        if (thisFormWrapper.is('.form-working-preference')) {

            var hasContent = false;
            $addSectionBtn = thisFormWrapper.closest('fieldset').find('.add-one-more-section');
            thisFormWrapper.find('select, #specific-salary-input').each(function () {
                if (!!$(this).val()) {
                    hasContent = true;
                }
            });
            if (thisFormWrapper.find('input:checked').length > 0) {
                hasContent = true;
            }

            if (hasContent) {
                $addSectionBtn.addClass('hide');
            } else {
                $addSectionBtn.removeClass('hide');
            }
        }


        // Contact Information Case
        if (thisFormWrapper.find('.btn-primary').is('#save-contact-information')) {
            if ($('#district').is(':disabled')) {
                var $districtViewControl = $('#district-view-control');
                $districtViewControl.text($districtViewControl.data('na'));
            }
        }
        // Key Skills
        if (thisFormWrapper.find('.btn-primary').is('#save-skills')) {
            //updateTags($tagView);
            $('.skill-old').remove();
            $('#alert-old-skill').remove();

        }

        // Education case
        if (thisFormWrapper.closest('.section-education').length) {
            checkLatestEducationLevel(thisFormWrapper)
        }
        // Misc
        calculateToDate(thisFormWrapper);
        //checkChooseLanguage();
        calculateNumberYearMonth(thisFormWrapper.find('.total-time'));
        thisFormWrapper.parents('fieldset').removeClass('edit-mode');
    };


    // Set latest work experience field & position
    var setLatestFields = function (company, position) {
        $('.current-company').text(company);
        $('.profile-pos .view-field span').text(position);
        $('#position').val(position)
    };
    

    // Check/uncheck Current Job; checkbox should be the "to-present-checkbox"
    var checkToDateDatePickerIsPresent = function (checkbox) {
        var $toDateDatePicker = $(checkbox).parents('.form-wrapper').find('input.datepicker[data-control="to-date"]');
        if ($(checkbox).is(':checked')) {
            $toDateDatePicker.attr('disabled', 'disabled').val("");
        } else {
            if ($toDateDatePicker.is(":disabled")) {
                $toDateDatePicker.removeAttr('disabled').trigger('focus');
            }
        }
    };

    // Calculate the number of years and months: To Date - From Date. e.g "5 years 2 months"
    var calculateNumberYearMonth = function (totalTime) {
        var fromDate = $(totalTime).siblings('[data-control="from-date"]').text();
        var toDate = $(totalTime).siblings('[data-control="to-date"]').text();
        var $thisToPresentCheckBox = $(totalTime).parents('.view-field').siblings('.edit-field').find('.to-present-checkbox');
        if ($thisToPresentCheckBox.length > 0) {
            if ($thisToPresentCheckBox.is(':checked')) {
                var d = new Date();
                var thisMonth = d.getMonth() + 1;
                if (thisMonth < 10) {
                    toDate = "0" + (d.getMonth() + 1).toString() + "/" + d.getFullYear().toString();
                } else {
                    toDate = (d.getMonth() + 1).toString() + "/" + d.getFullYear().toString();
                }
            }
        }

        var calculatedFromDate = fromDate.substr(0, 3) + "01/" + fromDate.substr(3, 4);
        var calculatedToDate = toDate.substr(0, 3) + "01/" + toDate.substr(3, 4);

        if (( isNaN(Date.parse(calculatedToDate)) || isNaN(Date.parse(calculatedFromDate)) )) {
            calculatedFromDate = "09/01/2008";
            calculatedToDate = "12/01/2014";
            $(totalTime).addClass('placeholder');
        } else {
            $(totalTime).removeClass('placeholder');
        }

        var calculatedMillisecond = Date.parse(calculatedToDate) - Date.parse(calculatedFromDate) + 2592000000;
        var numberOfYear = Math.floor(calculatedMillisecond / 31536000000);
        var numberOfMonth = Math.floor((calculatedMillisecond - numberOfYear * 31536000000) / 2592000000);

        if (numberOfYear < 0) {
            numberOfYear = 0;
        }

        if (numberOfMonth == 12){
            numberOfYear += 1;
            numberOfMonth = 0;
        }

        numberOfYear === 0 ? $(totalTime).find('.years').hide() : $(totalTime).find('.years').show();
        numberOfMonth === 0 ? $(totalTime).find('.months').hide() : $(totalTime).find('.months').show();

        if (numberOfYear === 0 && numberOfMonth === 0) {
            $(totalTime).hide();
        } else {
            $(totalTime).show();
        }

        if ($.trim($('html').attr('lang')) === "en") {
            numberOfYear === 1 ? $(totalTime).find('.years .lbl').text('year') : $(totalTime).find('.years .lbl').text('years');
            numberOfMonth === 1 ? $(totalTime).find('.months .lbl').text('month') : $(totalTime).find('.months .lbl').text('months');
        }

        $(totalTime).find('.number-of-years').text(numberOfYear);
        $(totalTime).find('.number-of-months').text(numberOfMonth);
    };

    // Add/Remove from samples
    var removeAddedForm = function (thisFormWrapper) {
        var $thisFieldset = $(thisFormWrapper).parents('fieldset');

        $(thisFormWrapper).slideUp('fast').promise().done(function () {
            $(this).remove();
            checkContentSection($thisFieldset);
            if ($thisFieldset.closest('.section-education').length) {
                checkLatestEducationLevel($thisFieldset)
            }
        });


        // Show the add section button
        $thisFieldset.find('.add-one-more-section').fadeIn('fast');
        //If the number of sections is 0, then remove the whole fieldset
        //$thisFieldset.data('numberOfSection', $thisFieldset.data('numberOfSection') - 1);
        //if ($thisFieldset.data('numberOfSection') == 0) {
        //    $(thisFormWrapper).parents('div[class^="section-"]').slideUp('fast');
        //}

        $(thisFormWrapper).parents('fieldset').removeClass('edit-mode');
    };

    // If there is content, show content; if there is no content, show the placeholder section; if the number of section reach the max number of section, remove the add button
    var checkContentSection = function (thisFieldset) {
        var numberOfSection = $(thisFieldset).find('.content-section').clone().find('.sample').remove().end().find('.form-wrapper, .tag-xs:not([style])').length;
        var maxNumberOfField = $(thisFieldset).data('max-number-of-field');

        if (numberOfSection === 0) {
            $(thisFieldset).find('.content-section-wrapper').fadeOut('fast').promise().done(function () {
                $(thisFieldset).find('.placeholder-section').fadeIn('fast');
            });
        } else {
            $(thisFieldset).find('.placeholder-section').fadeOut('fast').promise().done(function () {
                $(thisFieldset).find('.content-section-wrapper').fadeIn('fast');
            });


        }

        if (maxNumberOfField && maxNumberOfField === numberOfSection) {
            $(thisFieldset).find('.add-one-more-section').fadeOut('fast');
        } else {
            $(thisFieldset).find('.add-one-more-section').fadeIn('fast');
        }
    };

    var initModule = function () {
        // Save data for resetting form later
        $('input.edit-control,select.edit-control, textarea.edit-control').each(function () {
            if ($(this).is(':checkbox') || $(this).is(':radio')) {
                $(this).data('current_data', $(this).prop('checked')).change();
            } else {
                $(this).data('current_data', $(this).val());
            }
        });

        // Bind data for edit/saving forms
        $('.form-wrapper').click(function () {
            var $thisFieldset = $(this).parents('fieldset');
            var $self = $(this);
            var $thisAutoFocusEl = $self.find('[autofocus]');

            if (!$thisFieldset.hasClass('edit-mode')) {
                $thisFieldset.addClass('edit-mode');

                if (!/edit-mode|no-edit-mode/.test($self[0].className)) {
                    $self.addClass('edit-mode');
                    $self.find('.view-field').fadeOut('fast').promise().done(function () {
                        $self.find('.edit-field').fadeIn('fast');
                        // Check if this element is a tinyMCE instance, then focus that tinyMCE instance
                        if ($thisAutoFocusEl.siblings('.mce-container').length > 0) {
                            tinyMCE.get($thisAutoFocusEl[0].id).focus();
                        } else {
                            $thisAutoFocusEl.focus();
                        }
                        goToPosition($self.closest('.form-view'), 50);
                    });

                }

                // FadeOut the Add Section Button
                $self.parents('fieldset').find('.add-one-more-section').fadeOut('fast');
                if ($(this).is('.skill-section')) {
                    //mdlKeySkill.updateTags($tagEdit);
                }

            }
        });

        // Showing Edit Avatar Dropdown
        $('.user-pic').closest('.no-edit-mode').click(function (e) {
            e.stopPropagation();
            $(this).find('.dropdown').toggleClass('open');
        }).end().find('.form-btn-edit').click(function (e) {
            e.stopPropagation();
            $(this).siblings('.dropdown').toggleClass('open');
        });

        // Cancel Edit
        $('.btn-cancel').click(function (e) {
            e.stopPropagation();
            var $formWrapper = $(this).parents('.form-wrapper');
            if ($formWrapper.hasClass('new-section')) {
                removeAddedForm($formWrapper);
            } /*else if ($formWrapper.hasClass('opened-section')) {
             $formWrapper.removeClass('opened-section').parents('[class^="section"]').slideUp('fast');
             }*/
            else {
                $formWrapper.removeClass('edit-mode').find('.edit-field').fadeOut('fast').promise().done(function () {
                    $formWrapper.find('.view-field').fadeIn('fast');
                });
                resetForm($formWrapper);
                //checkChooseLanguage();
                //$formWrapper.parents('fieldset').find('.add-one-more-section').fadeIn('fast');
                checkContentSection($formWrapper.parents('fieldset'));
                $formWrapper.parents('fieldset').removeClass('edit-mode');
            }
            $formWrapper.find('.error-message').fadeOut('fast');
            $formWrapper.find('.has-error').removeClass('has-error');
        });

        $('.form-wrapper').each(function () {
            saveContent($(this));
        });

        // Check the to-present-checkbox
        var $toPresentCheckbox = $('.to-present-checkbox');
        $toPresentCheckbox.each(function () {
            checkToDateDatePickerIsPresent(this);
        });

        $toPresentCheckbox.change(function () {
            checkToDateDatePickerIsPresent(this);
        });

        // Tooltips for achievement text area, this is a dynamic one.
        $('body').tooltip({
            selector: '.achievement-textarea',
            title: 'Summarize your formal education you received, including field of study and special interests. Describe what kind non-academic classes you chose or even the continuing education program you took after graduated.',
            placement: 'top',
            delay: 200
        });

        // Get content for the "toDate" field
        $('#form-employment').find('.view-control[data-control="to-date"]').each(function () {
            var thisViewCtrl = $(this);
            var thisEditCtrl = $(this).parents('.view-field').siblings('.edit-field').find('[data-control="to-date"]');
            var thisToPresentCheckBox = thisEditCtrl.parents('.edit-field').find('.to-present-checkbox');
            if ((thisToPresentCheckBox).is(':checked')) {
                var toDateText = thisToPresentCheckBox.data('text-value');
                thisViewCtrl.text(toDateText).removeClass('placeholder');
            } else if (thisEditCtrl.val().trim() === "") {
                thisViewCtrl.text(thisEditCtrl.attr('placeholder')).addClass('placeholder');

            } else {
                thisViewCtrl.text(thisEditCtrl.val()).removeClass('placeholder');

            }

        });

        // Add one more section

        $('.add-one-more-section').click(function (e) {
            // Protect from clicking on the header to prevent collapsing/expanding
            e.stopPropagation();

            // If this section type is only edit/save (not clone and add)
            if ($(this).data('type') === "edit-only") {
                $(this).closest('fieldset').find('.form-wrapper').click();
                return;
            }

            // If this section type is "clone and add"
            $(this).closest('fieldset').find('.collapse').collapse('show');
            var $thisFieldset = $(this).closest('fieldset');
            if (!$thisFieldset.hasClass('edit-mode')) {
                var $thisNewSection = $thisFieldset.find('.sample').clone(true, true).removeClass('sample').addClass('new-section').appendTo($thisFieldset.find('.sortable'));
                $thisNewSection.slideDown('fast').trigger('click');
                // Update the datepicker for these new section
                $thisNewSection.find('.sample-datepicker').toggleClass('sample-datepicker datepicker').each(function () {
                    if (languageID === 2) {
                        $(this).datepicker({format: 'mm/yyyy', startDate: '-50y', minViewMode: 'months', language: 'en', autoclose: true});
                    } else {
                        $(this).datepicker({format: 'mm/yyyy', startDate: '-50y', minViewMode: 'months', language: 'vi', autoclose: true});
                    }
                }).blur(function () {
                    if (!$('.datepicker').hasClass('hover')) {
                        $(this).datepicker('hide');
                    }
                });

                // Check the datepicker checkbox
                checkToDateDatePickerIsPresent($thisNewSection.find('.to-present-checkbox'));

                // Scroll to this new section
                // scrollToThisSection('.new-section');

                // Update Select2 Components
                $thisNewSection.find('.qualification').select2({width: "100%"});

                ($thisNewSection.find('.language-select').length > 0) && $thisNewSection.find('.language-select').select2({width: "100%"});


                // Update textarea
                $thisNewSection.find('textarea').tinymce({
                    theme: 'modern',
                    toolbar: false,
                    menubar: false,
                    statusbar: false,
                    setup: function (editor) {
                        var numberOfCharacterAllowed = 5000;
                        editor.on('init', function () {
                            updateCountdown(editor, $(editor.getContainer()).siblings('.countdown'), numberOfCharacterAllowed, true);
                        });
                        editor.on('keyup', function () {
                            updateCountdown(editor, $(editor.getContainer()).siblings('.countdown'), numberOfCharacterAllowed, true);
                        }).on('keydown', function (event) {
                            // Stop enter text when reaching its limit
                            var key = event.which;
                            if ($(editor.getContent()).text().length >= numberOfCharacterAllowed && !(key == 46 || key == 8 || key == 37 || key == 39 )) {
                                event.preventDefault();
                                event.stopPropagation();
                                return false;
                            }
                        })
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


                // Sortable
                // $thisFieldset.find('.sortable').sortable();

                // Save data of these inputs
                $thisNewSection.find('input.edit-control,select.edit-control, textarea.edit-control').each(function () {
                    if ($(this).is(':checkbox') || $(this).is(':radio')) {
                        $(this).data('current_data', $(this).prop('checked')).change();
                    } else {
                        $(this).data('current_data', $(this).val());
                    }
                });

                // Hide this button & show the whole panel
                $(this).fadeOut('fast');
                $thisFieldset.data('numberOfSection', $thisFieldset.data('numberOfSection') + 1);
            }
        });

        // Count the number of sections when the page is loaded
        $('#form-employment,#form-education,#form-reference').each(function () {
            var $thisFieldset = $(this).find('fieldset');
            $thisFieldset.data('numberOfSection', $thisFieldset.find('form:not(".sample")').length);
        });


        // Get content when the page is loaded

        getContent('.section-profile');
        getContent('.section-skills');
        getContent('.section-employment');
        getContent('.section-education');
        getContent('.section-reference');

        var $noExcheckbox = $('#no-experience-check-box');
        if ($noExcheckbox.is(':checked')) {
            var $thisViewControl = $noExcheckbox.parents('.edit-field').siblings('.view-field').find('.view-control');
            $thisViewControl.text($noExcheckbox.data('text')).removeClass('placeholder');
        }

        // Important to put this after getContent, because this function use the content of the view controls to calculate years and months
        $('.total-time').each(function () {
            calculateNumberYearMonth(this);
        });

        // Check all fieldset for the number of sections
        $('fieldset').each(function () {
            checkContentSection($(this));
        });

    };

    // Public API
    return {
        init: initModule,
        saveContent: saveContent,
        getContent: getContent,
        checkContentSection: checkContentSection,
        removeAddedForm: removeAddedForm,
        setLatestFields: setLatestFields
    };

})();

var mdlAttachment = (function () {
    "use strict";

    var updateFileName = function (input, target) {
        var fileName = "";
        if (!!$(input)[0].files) {
            fileName = $(input)[0].files[0].name;
        } else {
            fileName = $(input)[0].value.split('\\').pop();
        }

        $(target).text(fileName);
    };

    var updateFileDate = function (input, target) {
        var d = new Date();
        var fileDate = moment().format('DD/MM/YYYY');
        $(target).text(fileDate);
    };

    var initModule = function (input, nameTarget, dateTarget) {
        $(input).change(function () {
            updateFileName(input, nameTarget);
            updateFileDate(input, dateTarget);
        });
    };

    // Public API
    return {
        updateFileName: updateFileName,
        updateFileDate: updateFileDate,
        init: initModule
    };
})();

var addSectionContent = function (section, objEmptyField) {

    if (typeof objEmptyField !== "undefined" && objEmptyField) {


        if (typeof objEmptyField.ruleValue !== "undefined" && typeof objEmptyField.fieldName !== "undefined") {
            // Phone case
            if (objEmptyField.ruleValue === "phone" && objEmptyField.fieldName === "references") {
                $('.section-reference').find('.form-wrapper').not('.sample')
                    .find('input').filter('[data-control="ref-phone"]').each(function () {
                    if ($.trim($(this).val()) === "") {
                        var $thisFormWrapper = $(this).closest('.form-wrapper');
                        if ($thisFormWrapper.hasClass('edit-mode')) {
                            goToPosition($thisFormWrapper, 50);
                        } else {
                            $thisFormWrapper.click();
                        }
                        $thisFormWrapper.find('.msg-description-suggestion').stop().fadeIn('fast').delay('5000').fadeOut('fast');
                        var self = this;
                        setTimeout(function () {
                            $(self).focus();
                        }, 500);
                        return false;
                    }
                })
            }else if (objEmptyField.ruleValue != "description" && (objEmptyField.fieldName == "experiences" || objEmptyField.fieldName == "educations")) {
                $(section).find('.form-wrapper').not('.sample')
                    .find('input').filter('[data-control="from-date"],[data-control="to-date"]').each(function () {
                        if ($.trim($(this).val()) === "") {
                            var $thisFormWrapper = $(this).closest('.form-wrapper');
                            if ($thisFormWrapper.hasClass('edit-mode')) {
                                goToPosition($thisFormWrapper, 50);
                            } else {
                                $thisFormWrapper.click();
                            }
                            $thisFormWrapper.find('.msg-description-suggestion').stop().fadeIn('fast').delay('5000').fadeOut('fast');
                            var self = this;
                            setTimeout(function () {
                                $(self).focus();
                            }, 500);
                            return false;
                        }
                    })
            } else {
                // Other cases
                $(section).find('.form-wrapper').not('.sample').find('textarea').each(function () {
                    if ($.trim($(this).val()) === "") {
                        var $thisFormWrapper = $(this).closest('.form-wrapper');
                        if ($thisFormWrapper.hasClass('edit-mode')) {
                            goToPosition($thisFormWrapper, 50);
                        } else {
                            $thisFormWrapper.click();
                        }
                        $thisFormWrapper.find('.msg-description-suggestion').stop().fadeIn('fast').delay('5000').fadeOut('fast');
                        var self = this;
                        setTimeout(function () {
                            tinyMCE.get(self.id).focus()
                        }, 500);
                        return false;
                    }
                })
            }
        }
    }

    else {

        if (!$(section).is('.hidden-section')) {
            $(section).find('.add-one-more-section').click();
        } else {
            $(section).fadeIn('fast').promise().done(function () {
                $(section).find('.form-wrapper').click();
            })
        }

        if ($(section).find('fieldset').hasClass('edit-mode')) {
            goToPosition($(section).find('.form-wrapper.edit-mode'), 50);
        }
    }

};

// DOM Loaded
$(function () {
    "use strict";
    mdlBenefit.init();
    mdlKeySkill.init();
    mdlLiveEdit.init();
    mdlAttachment.init('#profileAttachFile', '.resume-name', '.resume-date');
});

$(function () {
    "use strict";

    var checkGraduateCheckBox = function () {
        if ($('.new-graduate-checkbox').is(':checked')) {
            $('.year-of-ex-input,.lbl-year-ex').attr('disabled', 'disabled').fadeOut('fast');
            $('.year-of-ex-view-field').addClass('hide');
        } else {
            $('.year-of-ex-input,.lbl-year-ex').removeAttr('disabled').fadeIn('fast');
            $('.year-of-ex-view-field').removeClass('hide');
        }
    };

    checkGraduateCheckBox();

    $('.new-graduate-checkbox').change(function () {
        checkGraduateCheckBox()
    });


    // Visible to Employer
    function checkInvisibleToEmployer() {
        var $invisibleToEmployerStatic = $('#invisible-to-employer-static');
        if ($('#visible-to-employer').is(':checked')) {
            $('#visible-to-employer-view-control').find('.fa').removeClass('fa-eye').addClass('fa-eye-slash');
            $invisibleToEmployerStatic.text($invisibleToEmployerStatic.data('invisible'));
        } else {
            $('#visible-to-employer-view-control').find('.fa').removeClass('fa-eye-slash').addClass('fa-eye');
            $invisibleToEmployerStatic.text($invisibleToEmployerStatic.data('visible'));
        }
    }

    checkInvisibleToEmployer();
    $('#visible-to-employer').change(function () {
        checkInvisibleToEmployer();
    });

});

// Back to top
$(function () {
    "use strict";
    var backToTopConfig = {
        duration: 500,
        startTop: 300,
        btn: $('.go-top:first')
    };

    $(document).scroll(function () {
        var winTop = $(window).scrollTop();
        if (winTop > backToTopConfig.startTop) {
            backToTopConfig.btn.removeClass('hide').fadeIn(backToTopConfig.duration);
        } else {
            backToTopConfig.btn.fadeOut(backToTopConfig.duration);
        }
    });

    $('.blurry-text').click(function () {
        $(this).removeClass('blurry-text')
    });
    $('.close').click(function () {
        $('.blurry-text').removeClass('blurry-text')
    })

});