//Slide Bar Function
//////////////////////////////////////
+(function ($) {
    $.fn.slideBar = function (options) {
        // Merge Options
        var opts = $.extend({}, $.fn.slideBar.defaults, options);
        var sb = $(this);
        var sbt = opts.toggle;

        //Function
        //---------------------------
        //Set basic css
        sb.css({
            width: opts.width,
            marginLeft: '-' + opts.width
        }).fadeTo('normal', 1);

        //Open Slidebar

        function openSlidebar(sb) {
            sb.animate({
                'margin-left': 0
            }, 'normal', 'easeInOutBack').addClass('in').css({
                'position': 'absolute',
                'top': sb.offset().top
            });

            $('body').append('<div class="slidebar-backdrop"></div>').find('.slidebar-backdrop').fadeTo('normal', 0.7);
        }

        function closeSlidebar(sb) {
            sb.animate({
                'margin-left': '-' + opts.width
            }, 'normal', 'easeInOutBack').removeClass('in').promise().done(function () {
                    var top_position = $(this).offset().top - $(window).scrollTop();
                    console.log(top_position)
                    $(this).css({
                        'position': 'fixed',
                        'top': top_position
                    }).animate({
                        'top': 0
                    });
                }

            )

            $('.slidebar-backdrop').fadeOut().promise().done(function () {
                $(this).remove();
            });
        }

        //Toggle Slidebar
        sbt.click(function (e) {
            e.stopPropagation;
            if (!sb.hasClass('in')) {
                openSlidebar(sb);
            } else {
                closeSlidebar(sb);
            }
        });
        $(document).on('click', '.slidebar-backdrop', function () {
            closeSlidebar(sb)
        })


    }
    // Default Options
    $.fn.slideBar.defaults = {
        toggle: $('.slide-sidebar-toggle'),
        width: '75%'
    };
}(jQuery));