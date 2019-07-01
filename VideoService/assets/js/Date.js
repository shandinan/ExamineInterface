///日期格式化参数
/// <param name="name" type="String">Description</param>
Date.prototype.format = function (format) //author: meizz
{
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "h+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (/(y+)/.test(format))
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}
///将c#序列化后的时间字符串转化为js的日期
///val的值必须是c#序列化的时间值      /Date(1325520000000+0800)/
///比如“1990-11-11 12:12:23”
jQuery.extend({
    formatTime: function (val, format) {
        try {
            var re = /-?\d+/;
            var m = re.exec(val);
            var d = new Date(parseInt(m[0]));

            return d.format(format);
        }
        catch (ex) {
            return "";
        }
    }
});

