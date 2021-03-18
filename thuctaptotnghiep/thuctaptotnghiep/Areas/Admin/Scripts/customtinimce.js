$(function () {
        tinyMCE.init({
            selector: '#textarea', plugins: ['link image', 'advlist autolink lists link image charmap print preview anchor',
            'searchreplace visualblocks code ,textcolor',
            'insertdatetime media table contextmenu paste code' ,'textcolor colorpicker'],
            setup: function (editor) {
                editor.on('change', function () {
                    editor.save();
                });
            },
            height: 400,
            toolbar: "insertfile undo redo | styleselect | fontselect | fontsizeselect | bold italic | forecolor backcolor |alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link | image | media", file_browser_callback: RoxyFileBrowser
        });
    });
function RoxyFileBrowser(field_name, url, type, win) {
    var roxyFileman = '/plugin/fileman/index.html';
    if (roxyFileman.indexOf("?") < 0) {
        roxyFileman += "?type=" + type;
    }
    else {
        roxyFileman += "&type=" + type;
    }
    roxyFileman += '&input=' + field_name + '&value=' + win.document.getElementById(field_name).value;
    if (tinyMCE.activeEditor.settings.language) {
        roxyFileman += '&langCode=' + tinyMCE.activeEditor.settings.language;
    }
    tinyMCE.activeEditor.windowManager.open({
        file: roxyFileman,
        title: 'Roxy Fileman',
        width: 850,
        height: 650,
        resizable: "yes",
        plugins: "media",
        inline: "yes",
        close_previous: "no"
    }, { window: win, input: field_name });
    return false;
}
function openCustomRoxy2() {
    $('#roxyCustomPanel2').dialog({ modal: true, width: 800, height: 500 });
    $('.ui-dialog-titlebar-close').html("X");
}
function closeCustomRoxy2() {
    $('#roxyCustomPanel2').dialog('close');
}