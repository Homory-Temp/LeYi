$(document).ready(function () {
	$(".mini.button").each(function (i, obj) {
		$(obj).click(function () {
			if ($(this).attr("data-url")) {
				top.location.href = $(this).attr("data-url");
			} else {
				notify($(this), '无权限进行相关操作', 'warn');
			}
		});
	});

	$("#allItems").transition("pulse");
});
