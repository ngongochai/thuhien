if (typeof setCookie !== 'function') {
	function setCookie(name, value, days) {
		if (days) {
				var date = new Date();
				date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
				var expires = "; expires=" + date.toGMTString();
		}
		else var expires = "";
		document.cookie = name + "=" + value + expires + "; path=/";
	}
}

if (typeof getCookie !== 'function') {
	function getCookie(name) {
		var nameEQ = name + "=";
		var ca = document.cookie.split(';');
		for (var i = 0; i < ca.length; i++) {
				var c = ca[i];
				while (c.charAt(0) == ' ') c = c.substring(1, c.length);
				if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
		}
		return null;
	}
}

$(function () {
	var jwt = getCookie('FIREBASE_JWT');
	FirebaseMessage.init(firebaseApiKey, firebaseAuthDomain, firebaseDatabaseURL);

	if (jwt != null) {
		connectFirebase(jwt);
	} else {
		FirebaseMessage.refreshJWT('', function(jwt) {
			connectFirebase(jwt);
		});
	}
});

function connectFirebase(jwt) {
	FirebaseMessage.connect(jwt, function() {
		if (typeof renderNotificationCenter === 'function') {
			renderNotificationCenter();
		}

		if (typeof renderMessageRootComponent === 'function') {
			renderMessageRootComponent();
		}
	});
}

function renderNotificationCenter() {
	FirebaseMessage.onReceiveNotification(function(unreadNo) {
		var noElement = jQuery('#unread-message');
		if (unreadNo > 0) {
            if (unreadNo > 99) {
                unreadNo = "99+";
            }
			noElement.html(unreadNo);
			noElement.show();
		} else {
			noElement.hide();
		}
	});
}
