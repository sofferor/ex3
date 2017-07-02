function checkAndSubmit() {
    console.log("in checkAndSubmit()");
    var userName = $("#userName").val();
    if (userName.length <= 0) {
        alert("Please enter a user name");
        return;
    }
    var pwd = $("#pwd").val();
    var confPwd = $("#confPwd").val();
    if (pwd !== confPwd) {
        alert("Password don't match.\nPlease enter the same password twice.");
        return;
    }

    var email = $("#email").val();
    var isContain = email.includes("@");
    if (!isContain) {
        alert("Please enter a valid email address.");
        return;
    }

    var uri = "/api/Users";
    var user = {
        UserName: userName,
        Password: pwd,
        Email: email,
        Wins: 0,
        Loses: 0,
        Rank: 2000
    };

    
    $.post(uri, user).done(function (data) {
        console.log("uri: " + uri);
        console.log(data);
        console.log("user added succefully");
        location.reload();
        alert("Registerd succesfully, please login.");
    }).fail(function (jqXHR, status, errorThrown) {
        console.log(user);
        console.log("error: " + errorThrown);
        console.log("uri: " + uri);
        console.log(jqXHR);
        if (jqXHR.responseText.includes("Email field is not a valid")) {
            alert("Please enter valid email address.");
        }
        console.log("post Failed");
    });
   
    

}