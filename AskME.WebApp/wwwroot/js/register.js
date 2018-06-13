
/*
 * Register.js
 * 
 */

$(function () {

    $('#regiter-btn').on('click', function () {

        $.post({
            url: 'register',
            data: { UserName: $('#username').val(), Email: $('#email').val(), Password: $('#password').val() },
            success: function (data) {          
                if (data.status = 200) {
                    location.href = '/user/login'
                } else {
                    alert(data.msg)
                }
            }
        })
    })
})