var timer;

function ticking() {
	timer = setInterval("calcTicks()", 1000);
}

function calcTicks() {
	var now = parseInt($("#tick").html());
	now--;
	if (now > 0) {
		$("#tick").html(now);
	} else {
		clearInterval(timer);
		reVerify();
	}
}
