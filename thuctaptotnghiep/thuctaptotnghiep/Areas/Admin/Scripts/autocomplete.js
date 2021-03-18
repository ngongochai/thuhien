jQuery(function () {
    $("#download-master").click(function () {
        _trackEvent("Downloads", "master")
    })
}), jQuery(document).ready(function (a) {
    a("body").scrollspy({
        target: ".bs-sidebar",
        offset: 0
    }),
    a(window).on("load", function () {
        a("body").scrollspy("refresh")
    }),
    setTimeout(function () {
        var b = a(".bs-sidebar");
        b.affix({
            offset: {
                top: function () {
                    var a = b.offset().top,
                    c = parseInt(b.children(0).css("margin-top"), 10);
                    return this.top = a - c
                },
                bottom: function () {
                    return this.bottom = a(".bs-footer").outerHeight(!0)
                }
            }
        })
    }, 100),

    a(".token-example-field").tokenfield(),
        a("#tokenfield-1").tokenfield({
            autocomplete: {
                source: function (request, response) {
                    jQuery.post("/Admin/ManagerJobAdmin/AutoCompleteSkill", {
                        key: request.term
                    }, function (data) {
                        response(data);
                    });
                },
                limit: 3,
                minLength: 1,
                delay: 100
            },
            showAutocompleteOnFocus: !0,
            delimiter: [",", " ", "-", "_"]
        });




})