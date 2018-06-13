/*

 Question.js

 */

$(function () {
    $('#answerBtn').click(function () {
        var answer = $('#answer').val().trim();
        if (answer.length > 0) {
            $.post({
                url: 'answer',
                data: { 'qId': $('#qId').val(), 'answerContent': answer },
                success: function (data) {
                    if (data.status === 200) {
                        location.reload();
                    } else {
                        alert(data.msg)
                    }
                }
            })
        } else {
            alert('请填写你的回答，你的有效回答会帮助到很多人！');
        }
    })
})


function preView(obj) {
    var html = $(obj).val();
    $('#preview').html(marked(html));
}

