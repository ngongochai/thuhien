function updateVipJobCarousel() {
    'use strict';
    var $vipJobs = $('#vipJobs'),
        maxHeight = 46;
    $vipJobs.show();
    // Calculate the width of item
    $vipJobs.on('jcarousel:createend', function () {
        var numberOfItem = $vipJobs.find('li').length, itemWidth;
        if (numberOfItem <= 6) {
            $('.jcarousel-control-prev, .jcarousel-control-next').hide();
            itemWidth = ( $vipJobs.width() - 5 * numberOfItem ) / numberOfItem;
            $vipJobs.find('li').width(itemWidth);
        }
        $vipJobs.addClass('in');
    });

    // Init VIP jobs carousel
    $vipJobs.jcarousel({
        wrap: 'both'
    });

    $('.jcarousel-control-prev').jcarouselControl({
        target: '-=1'
    });

    $('.jcarousel-control-next').jcarouselControl({
        target: '+=1'
    });


    // Limit company names to 2 lines
    $vipJobs.find('.caption').dotdotdot({
        height: maxHeight
    });
}