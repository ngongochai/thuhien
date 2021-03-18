var TopCompanies = {
    
    exec_request : function () {
        var requestURL = 'vclick/index.php?group=common&zone=3&limit=14';
        var wrapper = jQuery('#ads_TOP_COMPANIES');
        TopCompanies.display_animation(wrapper, true);

        jQuery.ajax({
            url: requestURL,
            success: function(result)
            {
                TopCompanies.display_animation(wrapper, false);

                if(result.code != 200) {
                    console.log(result.message);
                    return;
                }

                TopCompanies.render_ads(wrapper, result.data.result);
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

        var adswrapper = '<div id="adc_TOP_COMPANIES">';

        for(var index = 0; index < length; index++) {
            var ads = data[index];
            var logowrapper = '';

            logowrapper += '<a class="cusLogo" target="_blank" href="' + ads['destinationURL'] + '">';
            logowrapper += '<img class="salesLogoImage img-responsive" src="' +  ads['imageURL'] + '" width="88" height="43">';

            logowrapper += '</a>';
            adswrapper += logowrapper;
        }

        adswrapper += '</div>';
        wrapper.html(adswrapper);
    }
};

jQuery(function() {
    TopCompanies.exec_request();
});