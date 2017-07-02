$.get("../Html/NavBar.html",
    function (data) {
        var start = data.indexOf("<nav class");
        var end = data.indexOf("</nav>");
        var res = data.substring(start, end);
        $("#navbar").replaceWith(res);

        var contentPlacement =  $(".navbar-fixed-top").height();
        $("#spacer").css("margin-top", contentPlacement);

        console.log("in navbar.js: checkig if connected");
        var isConnected = sessionStorage.on;
        console.log("isConnected: " + isConnected);
        if (isConnected == "true") {
            console.log("in navbar.js: is connected");
            $("#register").text(sessionStorage.userName);
            $("#login").text("Logout");
        }
    });

function Logout() {
    console.log("in onClick to check if logout.");
    var isLogout = $("#login").text();
    if (isLogout == "Logout") {
        sessionStorage.on = "false";
        $("#register").text("Register");
        $("#login").text("Login");
        alert("You logged out successfully.");
        location.reload();
        return false;
    } else {
        return true;
    }
};

function ApplyMulti() {
    console.log("in onClick to check if login.");
    var isLogin = $("#login").text();
    if (isLogin == "Login") {
        alert("You must log in before playing in multi player.");
        event.returnValue = false;
        return false;
    } else {
        return true;
    }
};