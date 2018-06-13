

$(function () {
    /*
     * 保存用户信息
     */
    $('#save').click(function () {
        $.post({
            url: '/users/edit',
            data: {
                'userName': $('#userName').val(),
                'address': $('#address').val(),
                'bio': $('#bio').val(),
                'about': $('#about').val(),
                'telNumber': $('#telNumber').val(),
                'wechat': $('#wechat').val(),
                'qq': $('#qq').val()
            },
            success: function (data) {
                if (data.status === 200) {
                    alert("保存成功");
                } else {
                    alert(data.msg);
                }
            }, error: function () {

            }
        })
    })

    /*
     * 上传用户图片
     */
    $('#upLoadIcon').click(function () {
        if (formData.get('file')) {
            $.post({
                url: '/users/icon/upload',
                contentType: false,
                processData: false,
                data: formData,
                success: function (data) {
                    if (data.status === 200) {
                        console.log(data.data)
                        $('#userIcon').attr('src', data.data)
                        $('#uploadIconModel').hide()
                        $('#preView').attr('src', '')
                    }
                },
                erro: function () {
                    alert('图片上传失败')
                }
            })
        } else {
            alert('请选择图片')
        }
    })

    $('i.modal-closeicon').click(function () {
        $('div.modal-wrapper').hide()
    })
})


var formData = new FormData();
function handleFiles(obj, file) {
    var _obj = $(obj)
    var _file = file[0];
    if (_file.size < (1024 * 1024 * 2)) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#preView').attr('src', e.target.result)
        }
        reader.readAsDataURL(_file);
        formData.append('file', _file);
    } else {
        alert('图片文件不能超过2M')
    }
}