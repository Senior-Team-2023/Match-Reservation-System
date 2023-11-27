// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
	// var seatPosition = $("#SeatPosition").val();
	// $(".grid-btn").each(function () {
	// 	if ($(this).text() == seatPosition) {
	// 		$(this).addClass("active");
	// 	}
	// });
	$(".grid-btn").on("click",function () {
		$(".grid-btn").removeClass("grid-btn-selected");
		if (!$(this).hasClass("grid-btn-rsrv")) {
			$(this).addClass("grid-btn-selected");
		}
		console.log("Clicked");
		$("#SeatPosInput").val($(this).text());
	});
});