var userAccount = (function () {
    var btnSignUp = $("#btnSignUp");

    function initialize() {
        btnSignUp.bind("click", SignUp);
    }

    var SignUp = function() {
        var signUpUrl = window.ToDoUrl + "Account/Signup";
        var signUpData =
        {
            Name: $("#txtName").val(),
            EmailId: $("#txtEmailId").val(),
            ContactNo: $("#txtContactNo").val(),
            UserName: $("#txtUserName").val(),
            Password: $("#txtPassword").val()
        };

        $.ajax({
            type: "POST",
            url: signUpUrl,
            contentType: "application/json",
            data: JSON.stringify(signUpData),
            success: function (data) {
                alert("success");
            },
            error: function () {
                alert("error");
            }
        });
    }

    return {
        initialize: initialize
    }
})();

$(userAccount.initialize);
