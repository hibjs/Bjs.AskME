

/*
  md.js  渲染markdown语法
 
 */
$(function () {
    var rendererMD = new marked.Renderer();
    marked.setOptions({
        renderer: rendererMD,
        gfm: true,
        tables: true,
        breaks: false,
        pedantic: false,
        sanitize: false,
        smartLists: true,
        smartypants: false
    });//基本设置
    // 渲染Markdown
    var objs = $('div[data-markdown=true]');
    for (var i = 0; i < objs.length; i++) {
        var html = $(objs[i]).html().trim();
        $(objs[i]).html(marked(html));
    }

    /*
     * 转换时间为几个小时前这种格式
     */
    var date = $('span.datetime');
    for (var i = 0; i < date.length; i++) {
        var html = $(date[i]).html().trim();
        $(date[i]).html(diaplayTime(html));
    }

})


/*
 * 转换时间为几个小时前这种格式
 * @param {any} data 日期
 */

function diaplayTime(data) {
    var str = data;
    //将字符串转换成时间格式
    var timePublish = new Date(str);
    var timeNow = new Date();
    var minute = 1000 * 60;
    var hour = minute * 60;
    var day = hour * 24;
    var month = day * 30;
    var diffValue = timeNow - timePublish;
    var diffMonth = diffValue / month;
    var diffWeek = diffValue / (7 * day);
    var diffDay = diffValue / day;
    var diffHour = diffValue / hour;
    var diffMinute = diffValue / minute;

    if (diffValue < 0) {
        console.log("错误时间");
    }
    else if (diffMonth > 3) {
        result = timePublish.getFullYear() + "-";
        result += timePublish.getMonth() + "-";
        result += timePublish.getDate();
        alert(result);
    }
    else if (diffMonth > 1) {
        result = parseInt(diffMonth) + "月前";
    }
    else if (diffWeek > 1) {
        result = parseInt(diffWeek) + "周前";
    }
    else if (diffDay > 1) {
        result = parseInt(diffDay) + "天前";
    }
    else if (diffHour > 1) {
        result = parseInt(diffHour) + "小时前";
    }
    else if (diffMinute > 1) {
        result = parseInt(diffMinute) + "分钟前";
    }
    else {
        result = "刚刚发表";
    }
    return result;
}

/*
 * 添加关注 
 * @param {any} obj 对象
 * @param {any} id tagId
 */
function addFollow(obj, id) {
    $.post({
        url: '/user/add/follow',
        data: { 'id': id },
        success: function (data) {
            if (data.status == 200) {
                // 赋值HTML，并且移除点击事件
                $(obj).html("<i class='iconfont icon-right'></i>&nbsp;已关注").unbind();
            } else {
                alert(data.msg)
            }
        }, error: function (XMLHttpRequest) {
            if (XMLHttpRequest.status === 401) {
                window.location.href="/user/login"
            }
        }
    })
}


