var Square2 = {
    rotateSquareBanner : function (parentEl, rotationTime) {
        parentEl.each(function () {
            var $self = $(this);
            var firstItem = $(this).find('a:first');

            setInterval(function () {
                var currentShownItem = $self.find('a:visible');
                var nextItem = currentShownItem.next('a');

                currentShownItem.hide().promise().done(function () {
                    if (nextItem.length > 0) {
                        nextItem.css('display','block');
                    } else {
                        firstItem.css('display','block');
                    }
                });
            }, rotationTime);
        })
    },

    
    exec_request : function () {
        var requestURL = 'vclick/index.php?group=common&zone=6';
        var wrapper = jQuery('#ads_ADV_HOME_2');
        Square2.display_animation(wrapper, true);

        jQuery.ajax({
            url: requestURL,
            success: function(result)
            {
                Square2.display_animation(wrapper, false);

                if(result.code != 200) {
                    console.log(result.message);
                    return;
                }

                Square2.render_ads(wrapper, result.data.result);
                Square2.rotateSquareBanner(wrapper, result.data.rotate_time);
            }
        });
    },

    display_animation : function (wrapper, flag) {
        if(!flag) {
            wrapper.empty();
            return;
        }

        var subwrapper = '<div class="text-center m-t-md m-b-md"><i class="fa fa-spinner fa-pulse fa-3x fa-fw margin-bottom"></i></div>';
        wrapper.html(subwrapper);
    },

    render_ads : function(wrapper, data) {
        var length = data.length;

        if(length <= 0) {
            return;
        }

        var adswrapper = '<div id="adc_ADV_HOME_2" pan_timer="15" align="center" border="0" cellpadding="0" cellspacing="0">';
        for(var index = 0; index < length; index++) {
            var hidden = (index == 0) ? '' : 'hidden';

            var ads = data[index];
            var logowrapper = '';

            logowrapper += '<a ' + hidden + ' class="cusLogo" target="_blank" href="' + ads['destinationURL'] + '">';
            logowrapper += '<img class="salesLogoImage img-responsive" src="' +  ads['imageURL'] + '" width="234" height="234">';

            logowrapper += '</a>';
            adswrapper += logowrapper;
        }

        adswrapper += '</div>';
        wrapper.html(adswrapper);
    }
};

jQuery(function() {
    Square2.exec_request();
});