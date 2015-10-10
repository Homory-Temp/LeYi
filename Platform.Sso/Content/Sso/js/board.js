$(document).ready(function () {
	$(".signLink").each(function (i, obj) {
		$(obj).click(function () {
		    window.open($(this).attr('data-url'));
		});
	});
});
