
/* 
 * Ask.js
 * write by xiaoshan
 * 
 */

$(function () {

    /*
     * 提问
     */
    $('#question-btn').on('click', function () {
        var tagIds = new Array();
        for (var i in tags) {
            tagIds.push(tags[i].id)
        }
        console.log(tagIds.toString())
        $.post({
            url: './',
            data: { 'title': $('#title').val(), 'description': $('#description').val(), 'ids': tagIds.toString() },
            traditional: true,
            success: function (data) {
                if (data.status === 200) {
                    location.href = '/'
                } else {
                    alert(data.msg)
                }
            }
        })
    })

    /*
     * 保存文章
     */
    $('#addBlog').on('click', function () {
        var tagIds = new Array();
        for (var i in tags) {
            tagIds.push(tags[i].id)
        }
        console.log(tagIds.toString())
        $.post({
            url: 'add/post',
            data: { 'title': $('#title').val(), 'content': $('#description').val(), 'ids': tagIds.toString() },
            traditional: true,
            success: function (data) {
                console.log(data)
                if (data.status === 200) {
                    alert('发布成功')
                } else {
                    alert(data.msg)
                }
            }
        })
    })
    $(document).click(function () {
        $('#tag-suggestions').hide()
    })

    $('button.modal-closebutton').click(function () {
        $('div.modal-wrapper').hide()
    })
})

function preView(obj) {
    var html = $(obj).val();
    $('#preview').html(marked(html));
}

/*
 * 请求数据 
 * @param {any} obj
 */
function getData(obj) {
    var key = $(obj).val()
    if (key.length > 0 && key.substring(key.length - 1, key.length) === ' ' && key !== '') {
        setTimeout(function () {
            $.get({
                url: '../tags/' + key,
                success: function (data) {
                    renderTagSuggestions(data.data)
                }
            })
        }, 300)
    }
}

/*
 * 渲染 标签提示框 
 * @param {any} data Ajax 请求到的数据
 */
function renderTagSuggestions(data) {
    var tagSuggestions = $('#tag-suggestions');
    if (data.length > 0) {
        tagSuggestions.empty();
        for (var i in data) {
            var div = document.createElement('div')
            div.addEventListener('click', function () { addTag(data[i]); tagSuggestions.hide(); $('#tag').val('') })
            var des = '';
            if (data[i].description.length > 60) {
                des = data[i].description.substring(0, 60) + '...'
            } else {
                des = data[i].description
            }
            $(div).append("<span class='post-tag'>" + data[i].tagName + "</span ><span class='pull-right'><a href='~/question/tagged/" + data[i].tagName + "'>@</a></span><p class='des'>" + des + "</p>")
            tagSuggestions.append(div)
        }
        tagSuggestions.show()
    } else {
        var a = document.createElement('a');
        a.href = 'javascript:void(0)'
        a.innerText = '创建标签'
        a.className = 'btn btn-ask'
        a.addEventListener('click', function () {
            openDialog($('#tag').val())
        })
        tagSuggestions.empty().append(a).show()
    }
}

/*
 * 渲染 Tags 
 * @param {any} tags 标签
 */
function renderTags(tags) {
    var tagHoler = $('#tags')
    tagHoler.empty();
    if (tags.length > 0) {
        for (var i in tags) {
            var span = document.createElement('span');
            span.className = 'post-tag'
            span.innerHTML = tags[i].tagName;
            var remove = document.createElement('span');
            remove.className = 'iconfont icon-close';
            remove.addEventListener('click', function () { removeTag(tags[i]) });
            tagHoler.append($(span).append(remove));
        }
    } else {
        tagHoler.hide();
    }
}



var tags = new Array();
/*
 * 添加标签 
 * @param {any} item 数据
 */
function addTag(item) {
    tags.push(item);
    renderTags(tags);
}

/**
 * 删除标签
 * @param {any} item 数据
 */
function removeTag(item) {
    tags.splice($.inArray(item, tags), 1);
    renderTags(tags)
}

/*
 * 添加Tag 
 * @param {any} tagName 标签名
 */
function openDialog(tagName) {
    $('#tagName').val(tagName)
    $('#addTagModal').show()
}

/*
 * 保存 标签
 */
function saveTag() {
    $.post({
        url: '/user/add/tag',
        data: { 'tagName': $('#tagName').val(), 'des': $('#des').val() },
        success: function (data) {
            if (data.status == 200) {
                $('#addTagModal').hide();
                $('#des').val('')
            } else {
                alert(data.msg)
            }
        },
        error: function (xmlrequest) {
            console.log(xmlrequest.status)
        }
    })

}