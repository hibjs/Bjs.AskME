/*
 * Login.js
 * 
 */


$(function () {
    $('#login-btn').on('click', function () {
        console.log($('#email').val());
        console.log($('#password').val())
        $.post({
            url: 'auth',
            data: { UserName: $('#email').val(), Password: $('#password').val() },
            success: function (data) {
                console.log(data);
                location.href = '/'
            }, error: function () {
                console.log('Eror in Ajax');
            }

        })
    })

})