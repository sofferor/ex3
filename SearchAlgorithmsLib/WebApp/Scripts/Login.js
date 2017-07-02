function LoginUser() {
    console.log("LoginUser()");
    var userName = $("#userName").val();
    var pwd = $("#inputPassword").val();

    if (userName.length <= 0) {
        alert("Please enter a valid user name.");
    }
    if (pwd.length <= 0) {
        alert("Please enter a valid password.");
    }

    var uri = "/api/Users/" + userName;
    $.getJSON(uri).done(function (data) {
        console.log("try to get user");
        if (pwd != data.Password) {
            alert("Wrong password.");
            return;
        }
        sessionStorage.on = "true";
        console.log("sessionStorage.on: " + sessionStorage.on);
        sessionStorage.userName = userName;
        alert("You login successfully");
        location.reload();

    }).fail(function(status) {
        alert("User not exist, please register.");
    });
}