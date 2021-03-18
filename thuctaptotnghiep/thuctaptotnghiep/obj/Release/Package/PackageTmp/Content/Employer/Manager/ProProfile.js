    $(function () {
        window.messages = {
            something_went_wrong: 'Xin lỗi, có sự cố đã xảy ra vui lòng thử lại hoặc làm mới trang!',
            general_error: 'Xin lỗi, có lỗi với các thông tin mà bạn cung cấp.',
            company_name_notblank: 'Vui lòng nhập tên công ty.',
            categories_notblank: 'Vui lòng chọn ít nhất một ngành nghề.',
            phone_number_notblank: 'Vui lòng nhập số điện thoại liên hệ.',
            profile_not_contains_email: 'Vui lòng không nhập địa chỉ email',
            profile_not_contains_phone: 'Vui lòng không nhập số điện thoại',
            logo_file_accept: 'Vui lòng chọn hình ảnh .jpg .gif .png',
            logo_file_filesize: 'Vui lòng chọn hình ảnh kích thước nhỏ hơn 1MB',
            save_success_title: 'Đã lưu!',
            save_success_message: 'Thay đổi của bạn đã được lưu.'
        };

        // Modal Crop Photo
        $('#logo_logo_cropper').on('shown.bs.modal', function () {
            $("#cropbox").cropper({
                aspectRatio: 170 / 102,
                preview: "#preview"
            });
        });
        window.formValidator = $("#profile_form").validate({
            onfocusin: false,
            onfocusout: false,
            onkeyup: false,
            ignore: [],
            errorLabelContainer: "",
            rules: {
                'tencongty': {
                    required: true
                },
                'listnghanh': {
                    required: true
                },
                'dienthoaiban': {
                    required: true
                },
                'thongtin': {
                    not_contains_email: true,
                    not_contains_phone: true
                },
                //'profile[company_benefit1][benefit_id]': {
                //    required: {
                //        depends: function(element) {
                //            return $("#profile_company_benefit1_benefit_desc").is(":filled");
                //        }
                //    }
                //},
                //'profile[company_benefit2][benefit_id]': {
                //    required: {
                //        depends: function(element) {
                //            return $("#profile_company_benefit2_benefit_desc").is(":filled");
                //        }
                //    }
                //},
                //'profile[company_benefit3][benefit_id]': {
                //    required: {
                //        depends: function(element) {
                //            return $("#profile_company_benefit3_benefit_desc").is(":filled");
                //        }
                //    }
                //},
                //'profile[company_benefit1][benefit_desc]': {
                //    require_from_group: [1, ".benefit"],
                //    not_contains_email: true,
                //    not_contains_phone: true,
                //    required: {
                //        depends: function(element) {
                //            return $("#profile_company_benefit1_benefit_id").is(":filled");
                //        }
                //    }
                //},
                //'profile[company_benefit2][benefit_desc]': {
                //    require_from_group: [1, ".benefit"],
                //    not_contains_email: true,
                //    not_contains_phone: true,
                //    required: {
                //        depends: function(element) {
                //            return $("#profile_company_benefit2_benefit_id").is(":filled");
                //        }
                //    }
                //},
                //'profile[company_benefit3][benefit_desc]': {
                //    require_from_group: [1, ".benefit"],
                //    not_contains_email: true,
                //    not_contains_phone: true,
                //    required: {
                //        depends: function(element) {
                //            return $("#profile_company_benefit3_benefit_id").is(":filled");
                //        }
                //    }
                //},

            },
            messages: {
                'tencongty': {
                    required: messages.company_name_notblank,
                },
                'listnghanh': {
                    required: messages.categories_notblank
                },
                'dienthoaiban': {
                    required: messages.phone_number_notblank
                },
                'thongtin': {
                    not_contains_email: messages.profile_not_contains_email,
                    not_contains_phone: messages.profile_not_contains_phone
                },
                //'profile[company_benefit1][benefit_id]': {
                //    required: 'Vui lòng chọn loại phúc lợi.'
                //},
                //'profile[company_benefit2][benefit_id]': {
                //    required: 'Vui lòng chọn loại phúc lợi.'
                //},
                //'profile[company_benefit3][benefit_id]': {
                //    required: 'Vui lòng chọn loại phúc lợi.'
                //},
                //'profile[company_benefit1][benefit_desc]':{
                //    require_from_group: 'Vui lòng nhập phúc lợi công ty',
                //    not_contains_email: 'Vui lòng không nhập địa chỉ email',
                //    not_contains_phone: 'Vui lòng không nhập số điện thoại',
                //    required: 'Vui lòng nhập phúc lợi công ty'
                //},
                //'profile[company_benefit2][benefit_desc]':{
                //    require_from_group: 'Vui lòng nhập phúc lợi công ty',
                //    not_contains_email: 'Vui lòng không nhập địa chỉ email',
                //    not_contains_phone: 'Vui lòng không nhập số điện thoại',
                //    required: 'Vui lòng nhập phúc lợi công ty'
                //},
                //'profile[company_benefit3][benefit_desc]':{
                //    require_from_group: 'Vui lòng nhập phúc lợi công ty',
                //    not_contains_email: 'Vui lòng không nhập địa chỉ email',
                //    not_contains_phone: 'Vui lòng không nhập số điện thoại',
                //    required: 'Vui lòng nhập phúc lợi công ty'
                //},

            },
            errorPlacement: function (error, element) {
                var insertAfter = $(element).data("show-error-after");
                var errorContainer = $(element).data("show-error-container");
                if (typeof insertAfter !== 'undefined') {
                    if (typeof errorContainer !== 'undefined') {
                        $(element).closest(errorContainer).find(insertAfter).after(error);
                    } else {
                        $(element).parent().find(insertAfter).after(error);
                    }
                }
                else {
                    error.insertAfter($(element));
                }
            }
        });

        $.validator.addMethod('youtubeUrl', function (url, element, param) {
            var p = /^(?:https?:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v=|watch\?.+&v=))((\w|-){11})(?:\S+)?$/;
            return (url.match(p)) || url=='' ? RegExp.$1 : false;
        },"");

        window.formLogoValidator = $("#companyLogo").validate({
            onfocusin: false,
            onfocusout: false,
            onkeyup: false,
            ignore: [],
            errorLabelContainer: "",
            rules: {
                'logo': {
                    accept: "image/jpg,image/jpeg,image/png,image/gif",
                    filesize: 1024 * 1024
                },
                'logo1': {
                    accept: "image/jpg,image/jpeg,image/png,image/gif",
                    filesize: 1024*1024
                },
                'logo2': {
                    accept: "image/jpg,image/jpeg,image/png,image/gif",
                    filesize: 1024*1024
                },
                'logo3': {
                    accept: "image/jpg,image/jpeg,image/png,image/gif",
                    filesize: 1024*1024
                },
                'video': {
                    url: true,
                },
            },
            messages: {
                'logo': {
                    accept: messages.logo_file_accept,
                    filesize: messages.logo_file_filesize
                },
                'logo1': {
                    accept: 'Vui lòng chọn hình ảnh .jpg .gif .png',
                    filesize: 'Vui lòng chọn hình ảnh kích thước nhỏ hơn 1MB'
                },
                'logo2': {
                    accept: 'Vui lòng chọn hình ảnh .jpg .gif .png',
                    filesize: 'Vui lòng chọn hình ảnh kích thước nhỏ hơn 1MB'
                },
                'logo3': {
                    accept: 'Vui lòng chọn hình ảnh .jpg .gif .png',
                    filesize: 'Vui lòng chọn hình ảnh kích thước nhỏ hơn 1MB'
                },
                'video': {
                    url: 'Vui lòng nhập đúng định dạng đường dẫn',
                }
            },
            errorPlacement: function (error, element) {
                var insertAfter = $(element).data("show-error-after");
                var errorContainer = $(element).data("show-error-container");
                if (typeof insertAfter !== 'undefined') {
                    if (typeof errorContainer !== 'undefined') {
                        $(element).closest(errorContainer).find(insertAfter).after(error);
                    } else {
                        $(element).parent().find(insertAfter).after(error);
                    }
                }
                else {
                    error.insertAfter($(element));
                }
            }
        });


        //Add rule for check email exist
        $.validator.addMethod('invalidExistEmail', function (value, element, param) {
            var result=false;
            $.ajax({
                type:'POST',
                url: '/v2/user-check-email',
                data: {email: value},
                success: function(data){
                    if (data.status == "ERROR") {
                        result= false;
                    } else {
                        result= true;
                    }
                },
                dataType: 'json',
                async:false
            });
            return result;

        }, "Email này đã được sử dụng.");

        var formChangeEmailValidator = $("#formAccountChangeEmail").validate({
            errorClass: "error",
            submit: false,
            focusInvalid: true,
            ignore: [],
            errorLabelContainer: "",
            rules: {
                'change_email[new_email][first]': {
                    required: true,
                    email: true,
                    invalidExistEmail: true
                },
                'change_email[new_email][second]': {
                    required: true,
                    email: true,
                    equalTo: "#change_email_new_email_first"
                },
                'change_email[password]': {
                    required: true
                }
            },
            messages: {
                'change_email[new_email][first]': {
                    required: "Vui lòng nhập địa chỉ email",
                    email: "Địa chỉ email không đúng định dạng",
                    invalidExistEmail: "Email này đã được sử dụng."
                },
                'change_email[new_email][second]': {
                    required: "Vui lòng nhập xác nhận địa chỉ email",
                    email: "Xác nhận địa chỉ email không đúng định dạng",
                    equalTo: "Địa chỉ email không trùng khớp",
                },
                'change_email[password]': {
                    required: "Vui lòng nhập mật khẩu truy cập",
                }

            },
            errorPlacement: function (error, element) {
                var insertAfter = $(element).data("show-error-after");
                var errorContainer = $(element).data("show-error-container");
                if (typeof insertAfter !== 'undefined') {
                    if (typeof errorContainer !== 'undefined') {
                        $(element).closest(errorContainer).find(insertAfter).after(error);
                    } else {
                        $(element).parent().find(insertAfter).after(error);
                    }
                }
                else {
                    error.insertAfter($(element));
                }
            }
        });

        $(".btn-save-email").click(function (e) {
            e.preventDefault();
            formChangeEmailValidator.form();
            if (formChangeEmailValidator.valid()) {
                $("#formAccountChangeEmail").submit();
            }
        });


        var formSubmitEmailOptions = {
            type: 'POST',
            dataType: 'json',
            success: function (data) {
                if (data.status == "SUCCESS") {
                    swal("", "Vui lòng kiểm tra email và làm theo hướng dẫn để kích hoạt lại tài khoản của bạn.", "success")
                    $('#formEmail').slideUp();
                    $('.buttons').slideDown();
                } else if (data.status == "ERROR_PASSWORD") {
                    swal("", "Sai mật khẩu. Vui lòng thử lại.", "error");
                } else if (data.status == "VALIDATE_ERROR") {
                    swal("", "Xin lỗi, có lỗi với các thông tin mà bạn cung cấp.", "error");
                } else {
                    swal("", "Xin lỗi, có lỗi với các thông tin mà bạn cung cấp.", "error");
                }

            },
            error: function () {

            }
        };
        $('#formAccountChangeEmail').on('submit', function (e) {
            e.preventDefault();
            $(this).ajaxSubmit(formSubmitEmailOptions);
            return false;
        });


        //Process for change password
        var formChangePasswordValidator = $("#formAccountChangePassword").validate({
            errorClass: "error",
            submit: false,
            focusInvalid: true,
            ignore: [],
            errorLabelContainer: "",
            rules: {
                'password_first': {
                    required: true,
                    minlength: 4,
                    maxlength: 20
                },
                'password_second': {
                    required: true,
                    minlength: 4,
                    maxlength: 20,
                    equalTo: "#change_password_password_first"
                },
                'current_password': {
                    required: true,
                }
            },
            messages: {
                'password_first': {
                    required: "Vui lòng cung cấp mật khẩu truy cập mới.",
                    minlength: "The password must contain 4-20 characters",
                    maxlength: "The password must contain 4-20 characters",
                },
                'password_second': {
                    required: "Vui lòng xác nhận lại mật khẩu mới.",
                    minlength: "The password must contain 4-20 characters",
                    maxlength: "The password must contain 4-20 characters",
                    equalTo: "Xác nhận lại mật khẩu mới không trùng khớp.",
                },
                'current_password': {
                    required: "Vui lòng cung cấp mật khẩu truy cập.",
                }

            },
            errorPlacement: function (error, element) {
                var insertAfter = $(element).data("show-error-after");
                var errorContainer = $(element).data("show-error-container");
                if (typeof insertAfter !== 'undefined') {
                    if (typeof errorContainer !== 'undefined') {
                        $(element).closest(errorContainer).find(insertAfter).after(error);
                    } else {
                        $(element).parent().find(insertAfter).after(error);
                    }
                }
                else {
                    error.insertAfter($(element));
                }
            }
        });

        $(".btn-save-password").click(function (e) {
            e.preventDefault();
            formChangePasswordValidator.form();
            if (formChangePasswordValidator.valid()) {
                $("#formAccountChangePassword").submit();
            }
        });

        var formSubmitPasswordOptions = {
            type: 'POST',
            dataType: 'json',
            success: function (data) {
                if (data.status == "SUCCESS") {
                    swal({
                            title: "Đổi mật khẩu thành công.",
                            text: "Mật khẩu mới đã được gởi tới email của bạn. Vui lòng đăng nhập với mật khẩu mới.",
                            type: "success"
                        }, function () {
                            location.reload();
                        });
                } else if (data.status == 'REDIRECT') {
                    location.replace(data.redirectUrl);
                    
                }else if (data.status == "ERROR_PASSWORD") {
                    swal({
                                title: "",
                                text: "Sai mật khẩu. Vui lòng thử lại.",
                                type: "error"
                            }, function () {

                                $("#change_password_current_password").focus();
                            }
                    );

                } else if (data.status == "VALIDATE_ERROR") {
                    swal("", "Xin lỗi, có lỗi với các thông tin mà bạn cung cấp.", "error");
                } else {
                    swal("", "Xin lỗi, có lỗi với các thông tin mà bạn cung cấp.", "error");
                }
            },
            error: function () {

            }
        };

        $('#formAccountChangePassword').on('submit', function (e) {
            e.preventDefault();
            $(this).ajaxSubmit(formSubmitPasswordOptions);
            return false;
        });


        // Company Benefit
        //$('.benefit-group .benefit').each(function () {
        //    var $thisCounter = $(this).parents('.benefit-wrapper').find('.num');
        //    $(this).on('keyup change focus', function () {
        //        var textLength = $(this).val().length;
        //        $thisCounter.text(100 - textLength);
        //    });
        //});

        //var numberOfItems = $('.benefit-wrapper:visible').length;
        //if (numberOfItems == 0) {
        //    var firstBenefit = $('.benefit-wrapper')[0];
        //    firstBenefit.removeAttribute('hidden');
        //};

        // Counter
        $('.char-counter').each(function () {
            var maxLength = $(this).data('max-lengh'),
                    $targetInput = $('[data-counter-target="' + $(this).data('counter-name') + '"]'),
                    $counterNum = $(this).find('.num');
            $targetInput.keyup(function () {
                $counterNum.text(maxLength - $targetInput.val().length);
            });
            $targetInput.keyup();
        });

        // Chosen Icon
        function checkChosenIcon() {
            var $comBfit = $('.company-benefits');
            $comBfit.find('.item-wrapper').removeClass('chosen-icon');
            $comBfit.find('.benefit-id').each(function () {
                var thisID = $(this).val();
                if ($.trim(thisID) === "") {
                    thisID = 0;
                }
                $comBfit.find('.item-wrapper[data-val="' + thisID + '"]').addClass('chosen-icon');
            });
        }

        checkChosenIcon();

        // Choose Icon
        $('.item-wrapper').each(function () {
            $(this).click(function (event) {
                event.preventDefault();
                if (!$(this).hasClass('chosen-icon')) {
                    var thisIcon = $(this).find('.fa').clone().attr('class'),
                            thisEg = $(this).data('eg'),
                            thisVal = $(this).data('val');
                    $(this).parents('.dropdown').find('.dropdown-toggle .fa:first').attr('class', thisIcon);
                    $(this).parents('.input-group').find('.benefit').attr('placeholder', thisEg).change().select();
                    $(this).parents('.input-group').find('.benefit-id').val(thisVal).change();
                    $(this).parents('.input-group-addon').siblings('input').focus();
                    checkChosenIcon();
                }
            }).keyup(function (e) {
                if (e.which === 13) {
                    $(this).click();
                }
            });
        })

        // Check add/remove benefit buttons
        function checkAddBenefitBtn() {
            var numberOfItems = $('.benefit-wrapper:visible').length;

            // Check add button
            if (numberOfItems === 3) {
                $('.btn-add-more-benefit').slideUp('fast');
            } else {
                $('.btn-add-more-benefit').fadeIn('fast');
                $('.btn-add-more-benefit.first-btn').removeAttr('disabled');
            }

            // Check remove button
            if (numberOfItems === 1) {
                $('.btn-remove-benefit').hide();
            } else {
                $('.btn-remove-benefit').show();
            }
        }
        checkAddBenefitBtn();

        // Benefit
        $('input.benefit').each(function () {
            $(this).focus(function () {
                if ($.trim($(this).val()) === "") {
                    $(this).parents('.input-group').find('.dropdown-toggle').dropdown('toggle');
                }
            }).keypress(function (e) {
                if (e.keyCode === 13) {
                    e.preventDefault();
                    $('.btn-add-more-benefit').click();
                }
            }).click(function (e) {
                // Clicking on these inputs would also click on the body, which will close the dropdown. We need to stop the event bubbling here to prevent this.
                e.stopPropagation();
            });
        });

        // Show/hide counter for benefit inputs.
        $('input.benefit').focusin(function () {
            $(this).closest('.benefit-wrapper').find('.countdown').stop().fadeTo(0, 0).removeClass('invisible').stop().fadeTo('slow', 1);
        }).focusout(function () {
            $(this).closest('.benefit-wrapper').find('.countdown').stop().fadeTo('fast', 0).promise().done(function () {
                $(this).addClass('invisible');
            });
        });

        // Click "add more benefit" button to add one more field
        $('.btn-add-more-benefit').click(function () {
            $(this).closest('.company-benefits').find('.benefit-wrapper:not(:visible):first').slideDown('fast').promise().done(function () {
                $(this).find('input.benefit').focus();
                checkAddBenefitBtn();
            });
        });

        // Remove Benefit
        $('.btn-remove-benefit').click(function (e) {
            e.preventDefault();

            // Hide the benefit field & clear the content
            $(this).closest('.benefit-wrapper').find('input').val("").end().slideUp('fast').promise().done(function () {
                checkAddBenefitBtn();
            });

            // Set the icon to question mark
            $(this).closest('.benefit-wrapper').next().find('.dropdown-toggle i:first-child').attr('class', 'fa fa-lg fa-fw fa-question');
            checkChosenIcon();
        });

        // Init tooltip
        $('.benefit-icon-list .benefit-icon .btn').tooltip();
        $('[data-toggle="tooltip"]').tooltip();

        // Keyboard navigation for the benefit dropdowns
        $('.benefit-wrapper').find('.item-wrapper').keydown(function (e) {
            console.log(e.keyCode);
            var currentIndex;
            // Down Arrow
            if (e.keyCode === 40) {
                $(this).next().focus();
                if ($(this).is(':last-child')) {
                    $(this).prevAll(':first-child').focus();
                }
            }

            // Up Arrow
            if (e.keyCode === 38) {
                $(this).prev().focus();
                if ($(this).is(':first-child')) {
                    $(this).nextAll(':last-child').focus();
                }
            }

            // Right Arrow
            if (e.keyCode === 39) {
                currentIndex = $(this).index();

                if ($(this).parent().is(':last-child')) {
                    $(this).parent().prevAll(':first-child').find('.item-wrapper').eq(currentIndex).focus();
                } else {
                    $(this).parent().next().find('.item-wrapper').eq(currentIndex).focus();
                }
            }

            // Left Arrow
            if (e.keyCode === 37) {
                currentIndex = $(this).index();
                if ($(this).parent().is(':first-child')) {
                    $(this).parent().nextAll(':last-child').find('.item-wrapper').eq(currentIndex).focus();
                } else {
                    $(this).parent().prev().find('.item-wrapper').eq(currentIndex).focus();
                }
            }
        }).end().find('.dropdown-toggle').keydown(function (e) {
            if (e.keyCode === 40) {
                $(this).siblings('.dropdown-menu').find('.item-wrapper:eq(0)').focus();
            }
        });

    });