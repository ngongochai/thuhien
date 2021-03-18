// Back To Top Button
var backToTopConfig = {
	duration: 500,
	start_top: 200,
	btn: $('.go-top:first')
};

$(document).scroll(function () {
	var win_top = $(window).scrollTop();
	if (win_top > backToTopConfig.start_top) {
		backToTopConfig.btn.removeClass('hide').fadeIn(backToTopConfig.duration);
	} else {
		backToTopConfig.btn.fadeOut(backToTopConfig.duration);
	}
});

//Scroll Top
$("a[href='#top']").click(function () {
	$("html, body").animate({scrollTop: 0}, "slow");
	return false;
});

$(function () {
	
	goToPosition = function (elementId, offset) {
		"use strict";

		if (typeof offset === "undefined") {
			offset = 0;
		}

		if (typeof $(elementId).offset() !== "undefined") {
			$('body,html').animate({
				scrollTop: $(elementId).offset().top - offset
			}, 1000);
		}

	};
});
