/// <reference path="notify.js" />

function notify(id, message, type) {
	if (id == null || id == "") {
		$.notify(message, type);
	} else {
		$(id).notify(message, type);
		$(id).focus();
	}
}
