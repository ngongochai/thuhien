function handleAttachError(xhr) {
    $(".attached-cv-update #upload-progress").hide();
    if (xhr.status == 413) {
        $('#profile-attach-upload-error').show();
    } else {
        $('#errorMessage').slideDown();
        setTimeout(function () {
            $('#errorMessage').slideUp();
        }, 5000);
    }
}
function handleAttachSuccess(data) {
    $(".attached-cv-update #upload-progress").hide();
    clearErrorMessage()
    if (data.status == 'REDIRECT') {
        window.location.href = data.redirectUrl;
    } else if (data.status == 'VALIDATION_ERROR') {
        $.each(data.fields, function (field, message) {
            if (field == 'resumeFile') {
                $('#profile-attach-upload-error').show();
            }
        });
    }
    else if (data.status == 'UPLOAD_ERROR') {
        $('#profile-attach-upload-error').html("<small>" + data.error + "</small>").show();
    } else if (data.status == 'ERROR') {
        $('#errorMessage').show();
    } else if (data.status == 'SUCCESS') {
        if($("#profileUploadAttachForm .no-resume").is(":visible")){
            $("#profileUploadAttachForm .no-resume").hide();
            $("#profileUploadAttachForm .have-resume").show();
        }
        $(".attached-cv-update #upload-success").slideDown();
        setTimeout(function () {
            $(".attached-cv-update #upload-success").slideUp();
        }, 5000);

    }
}

function clearErrorMessage() {
    $('#errorMessage').hide();
    $('#profile-attach-upload-error').hide();
}

function selectSearchable(item) {
    //call ajax to update this option
    $.post("/jobseekers/resume_manage.php?m=changeSearchable",
        {resumeid: $(item).val(), searchable: 1},
        function () {

        });
}
(function ($) {

    var options = {
        className: 'switchery',
        disabledOpacity: .5,
        speed: '0.5s',
        size: 'small'
    };

    $('input[type=checkbox]').filter('.js-switch').filter(
        'input:not([data-switchery=true])').each(function () {
            var n = new Switchery(this, options);
        });

    $('#chb-searchable').change(function () {
        //$('.note-searchable').slideToggle();
        //$('.note-unsearchable').slideToggle();
        $('.searchable-choice').slideToggle();

        if (this.checked) {
            //enable note for seachable
            $("#profileSettings .note-searchable").show();
            $("#profileSettings .note-unsearchable").hide();
            var listChoices = [];
            var listIndex = []
            var flagHaveSearchAble = 0;
            $("#profileSettings .searchable-choice .radio input[type=radio]").each(function () {
                var index = $(this).data('last-update');
                if (this.checked) {
                    flagHaveSearchAble = 1;
                }
                listChoices[index] = this;
                listIndex.push(index);
            });

            if (flagHaveSearchAble == 0) {
                //get max item last update
                var maxItem =  Math.max.apply(null, listIndex);

                //call ajax to update this option
                $.post("/jobseekers/resume_manage.php?m=changeSearchable",
                    {resumeid: $(listChoices[maxItem]).val(), searchable: 1},
                    function () {
                        $(listChoices[maxItem]).get(0).checked=true;
                    });
            }
        } else {
            $("#profileSettings .note-searchable").hide();
            $("#profileSettings .note-unsearchable").show();
            var item=$("#profileSettings .searchable-choice .radio input[type=radio]:checked");
            if(item.length){
                $.post("/jobseekers/resume_manage.php?m=changeSearchable",
                    {resumeid: $(item).val(), searchable: 0},
                    function () {
                        $(item).removeAttr("checked");
                    });
            }

        }
    });


    $("#profileAttachFile").change(function () {
        $(".attached-cv-update #upload-progress").show();
        var options = {
            beforeSubmit: clearErrorMessage,
            success: handleAttachSuccess,  // post-submit callback
            error: function (xhr, status, error) {
                handleAttachError(xhr);
            },
            type: 'post',        // 'get' or 'post', override for form's 'method' attribute
            dataType: 'json'        // 'xml', 'script', or 'json' (expected server response type)
        };
        $(this).closest('form').ajaxSubmit(options);
    })
    $("#btnAlternativeUpload").click(function(){
       $("#profileAttachFile").trigger('click');
    });
}(jQuery));

