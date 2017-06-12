$.get("../Html/NavBar.html",
    function (data) {
        var start = data.indexOf("<nav class");
        var end = data.indexOf("</nav>");
        var res = data.substring(start, end);
        $("#navbar").replaceWith(res);
    });

$(document).ready(function () {
    var contentPlacement = $(".navbar-fixed-top").position().top + $(".navbar-fixed-top").height();
    $("#spacer").css("margin-top", contentPlacement);
});