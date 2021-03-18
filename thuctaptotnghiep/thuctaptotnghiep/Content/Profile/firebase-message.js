/**
 * Maintain business logics of Firebase Messaging Platform.
 */
var FirebaseMessage = (function () {
	// Declare handler of Firebase Authentication and Realtime Database
	var auth = null,
		db = null,
		currentUser = null,
		authPromise = null,
		retry = 0,
		NO_OF_FRIENDS = 50,
		NO_OF_MESSAGES = 20; // Change here > Also change in the app.js; @TODO: Merge this var with the one in the app.js

	// Define a list of Firebase Database path
	var usersPath = '',
		userStatus = "users/{uid}/status";

	// Define a list of contants
	var ONLINE_PRESENCE = 'online',
		OFFLINE_PRESENCE = 'offline',
		INVALID_CUSTOM_TOKEN = 'auth/invalid-custom-token',
		CUSTOM_TOKEN_MISMATCH = 'auth/custom-token-mismatch',
		NO_UNREAD = 0,
		MAX_TOKEN_RETRY = 3;

	var init = function (firebaseApiKey, firebaseAuthDomain, firebaseDatabaseURL) {
		// Initialize Firebase
		var config = {
			apiKey: firebaseApiKey,
			authDomain: firebaseAuthDomain,
			databaseURL: firebaseDatabaseURL
		};
		firebase.initializeApp(config);

		auth = firebase.auth();
		db = firebase.database();
	};

	// Open connnection to Firebase using custom token authentication model
	var connect = function (token, callback) {
		if (auth.currentUser == null) {
            authPromise = auth.signInWithCustomToken(token);

			authPromise.then(function (user) {
				retry = 0; // Reset no of refresh token retry

				currentUser = user;

				managePresence();

				if (typeof callback === 'function') {
					callback();
				}
			}).catch(function (error) {
				// Refresh new token if token is invalid
				var errorCode = error.code;
				var errorMessage = error.message;

				// For more information, please refer at https://firebase.google.com/docs/reference/js/firebase.auth.Auth#signInWithCustomToken
				if (errorCode == INVALID_CUSTOM_TOKEN) { // Thrown if the custom token format is incorrect.
					refreshJWT(token, connect);
				}
				else if (errorCode == CUSTOM_TOKEN_MISMATCH) { // Thrown if the custom token is for a different Firebase App.
					console.log(errorMessage);
				}
			});
		}
		else {
			currentUser = auth.currentUser;
		}
	};

	var disconnect = function () {
		authPromise.then(function (user) {
			auth.signOut();
		});
	}

	/**
	 * Detecting online/offline user state
	 *
	 * @private
	 */
	var managePresence = function () {
		var presenceRef = db.ref("users/" + currentUser.uid + "/status");
		presenceRef.set(ONLINE_PRESENCE);
		presenceRef.onDisconnect().set(OFFLINE_PRESENCE);
	};

	/**
	 * @public
	 */
	var refreshJWT = function (legacyToken, callback) {
		if (retry++ < MAX_TOKEN_RETRY) {
			// Raise ajax call to get new JWT
			var request = new XMLHttpRequest();
			request.open('GET', FIREBASE_REFRESH_TOKEN_URL, true);
			request.onload = function () {
				if (request.status >= 200 && request.status < 400) {
					var data = JSON.parse(request.responseText);

					if (typeof callback === 'function') { // Retry to login Firebase with new JWT
						callback(data.jwt);
					}
				} else {
					// We reached our target server, but it returned an error

				}
			};
			request.onerror = function () {
				// There was a connection error of some sort
			};
			request.send();

			// Store into cookie for use later
		} else {

		}
	};

	var fetchFriendList = function (startAt, callback) {
		authPromise.then(function (user) {
			var friendRef = db.ref('friends/' + currentUser.uid)
				.orderByChild('weight');

			if (startAt > 0) {
				friendRef = friendRef.endAt(startAt).limitToLast(NO_OF_FRIENDS);
			} else {
				friendRef = friendRef.limitToLast(NO_OF_FRIENDS);
			}

			friendRef.once('value').then(function (snapshots) {
				var friends = snapshots.val(),
					friendList = [];

				if (snapshots.numChildren() > 0) {
					// Fetch link of friends
					var friendIds = new Array();
					snapshots.forEach(function (snapshot) {
						var friend = snapshot.val();
						friendIds.push(friend.friendId);
					});


					fetchFriendLink(friendIds, function (friendLinks) {
						snapshots.forEach(function (snapshot) {
							var friend = snapshot.val();

							var avatar = '';
							if (friend.hasOwnProperty('avatar')) {
								avatar = friend.avatar;

								// Fix for new staging domain
								avatar = avatar.replace(/http:\/\/images.staging.vietnamworks.com/i, 'http://staging-images.vietnamworks.com');

								// Support SSL
								avatar = avatar.replace(/http:\/\//i, '//');
							}

							var friendInfo = {
								id: friend.friendId,
								name: friend.name,
								avatar: avatar,
								jobTitle: friend.latestAppliedJob,
								unread: friend.unread,
								date: friend.lastReceivedDate,
								threadId: friend.threadId,
								latestAppliedDate: friend.latestAppliedDate,
								weight: friend.weight
							};

							if (typeof friendLinks !== "undefined" && friendLinks.hasOwnProperty(friend.friendId)) {
								friendInfo.profileLink = friendLinks[friend.friendId];
							}

							// Do not use 'push' regarding Firebase is always sorting ascending
							friendList.unshift(friendInfo);
						});

						if (typeof callback === 'function') {
							callback(friendList);
						}
					});
				} else {
					if (typeof callback === 'function') {
						callback(friendList);
					}
				}
			});
		});
	};

	var fetchFriendLink = function (friendIds, callback) {
		if (typeof FRIEND_LINK_FETCH_URL === 'string') {
			// Raise ajax call to get new JWT
			var request = new XMLHttpRequest();
			request.open('GET', FRIEND_LINK_FETCH_URL + friendIds.join(), true);
			request.onload = function () {
				if (request.status >= 200 && request.status < 400) {
					var data = JSON.parse(request.responseText);

					if (typeof callback === 'function') {
						callback(data.aliases);
					}
				} else { // We reached our target server, but it returned an error
					if (typeof callback === 'function') {
						callback();
					}
				}
			};
			request.onerror = function () {
				if (typeof callback === 'function') {
					callback();
				}
			};
			request.send();

			// Store into cookie for use later
		} else if (typeof callback === 'function') {
			callback();
		}
	};

	var fetchThread = function (friend, startAt, callback) {
		authPromise.then(function (user) {
			var threadRef = db.ref('threads/' + friend.threadId)
				.orderByChild('sentDate');

			if (startAt > 0) {
				threadRef = threadRef.endAt(startAt).limitToLast(NO_OF_MESSAGES);
			} else {
				threadRef = threadRef.limitToLast(NO_OF_MESSAGES);
			}

			threadRef.once('value', function (snapshot) {
				if (typeof callback === 'function') {
					var messages = snapshot.val();
					var messageArr = [];

					// Convert object to array
					for (var messageId in messages) {
						messages[messageId].date = messages[messageId].sentDate;
						messages[messageId].isMyMessage = messages[messageId].sender == currentUser.uid;
						messageArr.push(messages[messageId]);
					}

					// Remove duplicate message regarding less and equal condition
					if (startAt > 0) {
						messageArr.splice(messageArr.length - 1, 1);
					}

					callback(messageArr);
				}
			});
		});
	};

	var fetchFriendInfo = function (friendId, callback) {
		authPromise.then(function (user) {
			var friendRef = db.ref('friends/' + currentUser.uid + '/' + friendId);
			friendRef.once('value', function (friend) {
				if (typeof callback === 'function') {
					callback(friend.val());
				}
			});
		});
	};

	/**
	 * Store new message to Firebase Database and announce to friend
	 *
	 * @public
	 */
	var sendMessage = function (friend, message, callback) {
		authPromise.then(function (user) {
			var sentDate = Date.now();

			// Store new message to Firebase
			var threadRef = db.ref('threads/' + friend.threadId);
			threadRef.push({
				sender: currentUser.uid,
				receiver: friend.friendId,
				message: message,
				sentDate: sentDate
			});

			// Update my last received date
			var threadRef = db.ref('friends/' + currentUser.uid + '/' + friend.friendId);
			threadRef.update({
				lastReceivedDate: sentDate
			});

			// Send notification to friend
			notifyNewMessage(friend.friendId, sentDate);
		});
	};

	/**
	 * @private
	 */
	var notifyNewMessage = function (friendId, sentDate) {
		authPromise.then(function (user) {
			var eventRef = db.ref('events/' + friendId + '/newMessages' + '/' + currentUser.uid);

			eventRef.transaction(function (newMessageEvent) {
				if (newMessageEvent === null) {
					return {unread: 1, date: sentDate};
				}

				return {unread: newMessageEvent.unread + 1, date: sentDate};
			});
		});
	}

	var disconnect = function (callback) {
		auth.signOut().then(function () {
			if (typeof callback === 'function') {
				// Clear JWT on cookie
				callback();
			}
		});
	};

	// Listern any updates on notification center
	var onReceiveNotification = function (callback) {
		authPromise.then(function (user) {
			var userRef = db.ref('users/' + user.uid + '/unread');
			userRef.off('value');
			userRef.on('value', function (snapshot) {
				if (typeof callback === 'function') {
					callback(snapshot.val());
				}
			});
		});
	};

	// Emit event whenever have a new message has been sent
	var onFriendStateChanged = function (callback) {
		authPromise.then(function (user) {
			var friendRef = db.ref('friends/' + currentUser.uid);

            var newFriendRef = friendRef.orderByChild('weight')
                .startAt(Date.now());
			newFriendRef.off('child_added');
			newFriendRef.on('child_added', function (snapshot) {
				var friendId = snapshot.getKey();
				var friendStates = snapshot.val();

				// Update status of friend list
				if (typeof callback === 'function') {
					callback({
                        isNewFriend: true,
                        friend: friendStates
                    });
				}
			});

            friendRef.off('child_changed');
			friendRef.on('child_changed', function (snapshot) {
				var friendId = snapshot.getKey();
				var friendStates = snapshot.val();

				// Update status of friend list
				if (typeof callback === 'function') {
                    callback({
                        isNewFriend: false,
                        friend: friendStates
                    });
				}
			});
		});
	};

	var onNewThreadMessage = function (friend, callback) {
		authPromise.then(function (user) {
			var threadRef = db.ref('threads/' + friend.threadId)
				.orderByChild('sentDate')
				.startAt(Date.now());
			threadRef.off('child_added');
			threadRef.on('child_added', function (snapshot) {
				if (typeof callback === 'function') {
					var message = snapshot.val();

					var avatar = '';
					if (message.sender != currentUser.uid && friend.hasOwnProperty('avatar')) {
						avatar = friend.avatar;

						// Fix for new staging domain
						avatar = avatar.replace(/http:\/\/images.staging.vietnamworks.com/i, 'http://staging-images.vietnamworks.com');

						// Support SSL
						avatar = avatar.replace(/http:\/\//i, '//');
					}

					callback({
						avatar: avatar,
						message: message.message,
						date: message.sentDate,
						isMyMessage: message.sender == currentUser.uid
					});
				}
			});
		});
	};

	var offNewThreadMessage = function (friend) {
		authPromise.then(function (user) {
			var threadRef = db.ref('threads/' + friend.threadId).off();
		});
	}

	var updateFriendStates = function (friendStates, callback) {
		authPromise.then(function (user) {
			var friendRef = db.ref('friends/' + currentUser.uid + '/' + friendStates.friendId);
			friendRef.transaction(function (friend) { // Keep friend states persistent
				if (friend) {
					if (friend.unread == null) {
						friend.unread = friendStates.unread;
					} else {
						friend.unread += friendStates.unread;
					}
					friend.lastReceivedDate = friendStates.date;
					friend.weight = friendStates.date;
				}

				return friend;
			}, function (error, commit, snapshot) {
				if (commit && typeof callback === 'function') {
					callback(snapshot.val())
				}
			});
		});
	};

	var updateFriendReceivedDate = function (friend) {
		authPromise.then(function (user) {
			var friendRef = db.ref('friends/' + currentUser.uid + '/' + friend.friendId);
			friendRef.update({lastReceivedDate: friend.date, weight: friend.date});
		});
	};

	/**
	 * Recalculate no of unread messages of friend and current user notification
	 *
	 * @public
	 */
	var readFriendMessage = function (friendId, noOfUnread, callback) {
		authPromise.then(function (user) {
			var updates = {};

			// Reset Friend's unread message
			updates['friends/' + currentUser.uid + '/' + friendId + '/unread'] = NO_UNREAD;
			db.ref().update(updates, function () {
				if (typeof callback === 'function') {
					callback();
				}
			});

			// Decrease no of unread notification
			var userRef = db.ref('users/' + currentUser.uid + '/unread');
			userRef.transaction(function (unread) {
				if (unread) {
					unread -= noOfUnread;
					unread = unread >= 0 ? unread : 0;
				}

				return unread;
			});
		});
	};

	var updateNotification = function (notification) {
		authPromise.then(function (user) {
			var userRef = db.ref('users/' + currentUser.uid);
			userRef.transaction(function (userObject) {
				if (userObject && userObject.updated != notification.date) {
					if (isNaN(userObject.unread)) {
						userObject.unread = 0;
					}

					userObject.unread += notification.unread;
					userObject.updated = notification.date;
				}

				return userObject;
			});
		});
	}

	return {
		init: init,
		connect: connect,
		disconnect: disconnect,
		refreshJWT: refreshJWT,
		fetchFriendList: fetchFriendList,
		fetchThread: fetchThread,
		fetchFriendInfo: fetchFriendInfo,
		sendMessage: sendMessage,
		disconnect: disconnect,
		readFriendMessage: readFriendMessage,
		onReceiveNotification: onReceiveNotification,
		offNewThreadMessage: offNewThreadMessage,
		onFriendStateChanged: onFriendStateChanged,
		updateFriendStates: updateFriendStates,
		updateFriendReceivedDate: updateFriendReceivedDate,
		onNewThreadMessage: onNewThreadMessage,
		updateNotification: updateNotification
	}
})();
