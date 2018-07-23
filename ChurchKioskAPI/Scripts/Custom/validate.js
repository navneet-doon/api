$(function () { });

var Login = {

    ValidateForm: function () {
        let _userName = $('#txtUserName').val().trim();
        let _password = $('#txtPassword').val().trim();
        if (_userName === '' || _password != 'admin') {
            alert('Please enter username and password value should be - admin');
            return false;
        }
        else {
            let data = { "Username": _userName, "Password": _password };
            Login.SubmitData(data);
        }
    },

    SubmitData: function (data) {
        debugger;
        $.ajax({
            url: location.protocol + "//" + location.hostname + ":" + location.port + "/api/auth",
            type: 'POST',
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (xhr, status, error) {
                debugger;
            },
            success: function (result) {
                debugger;
                sessionStorage.AuthToken = result;
                Login.GoToSecureResources();
            }
        });
    },

    GoToSecureResources: function () {
        $.ajax({
            url: location.protocol + "//" + location.hostname + ":" + location.port + "/api/home",
            type: 'GET',
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", sessionStorage.AuthToken);
            },
            error: function (xhr, status, error) {
                debugger;
                location.href = $_UnauthorizedUser;
            },
            success: function (result) {
                debugger;
            }
        });
    }
}