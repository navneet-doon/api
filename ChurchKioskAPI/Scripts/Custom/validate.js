$(function () {
    Login.InIt();
});

var Login = {

    InIt:function(){
        if (sessionStorage.AuthToken == undefined) {
            $('#lblAuthToken').text('There is no auth-token available, you need to login first!');
        }
        else {
            $('#lblAuthToken').text(sessionStorage.AuthToken);
        }

        //****************************** Register secured link click events *******************************
        $('.lnkSecureResource').click(function () {
            Login.GoToSecureResources($(this).attr('action'));
        });
    },

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
        //debugger;
        $.ajax({
            url: location.protocol + "//" + location.hostname + ":" + location.port + "/api/auth",
            type: 'POST',
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (xhr, status, error) {
                //debugger;
            },
            success: function (result) {
                //debugger;
                sessionStorage.AuthToken = result;
                $('#lblAuthToken').text(sessionStorage.AuthToken);
                $(':password').val(''); $(':text').val('');
            }
        });
    },

    GoToSecureResources: function (_url) {
        //debugger;
        $.ajax({
            url: _url,
            type: 'GET',
            beforeSend: function (xhr) {
                if (sessionStorage.AuthToken != undefined)
                    xhr.setRequestHeader("Authorization", sessionStorage.AuthToken);
            },
            error: function (xhr, status, error) {
                //debugger;
                if (xhr.status == 401) {
                    alert(xhr.responseJSON.Message);
                }
            },
            success: function (result) {
                //debugger;
                console.log(result);
                alert(result);
            }
        });
    }
}