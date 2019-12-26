<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ckPlayer.aspx.cs" Inherits="VideoService.ckPlayer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <style type="text/css">
        body {
            margin: 0px;
            padding: 0px;
        }
    </style>
    <title></title>
    <% 
        var videoUrl = "";
        if (!String.IsNullOrEmpty(Request["videoUrl"]))
        {
            videoUrl = Request["videoUrl"].ToString();
        }
    %>
</head>
<body>
    <div id="video" style="width: 600px; height: 400px;"></div>
    <script src="assets/global/plugins/jquery.min.js"></script>
    <script type="text/javascript" src="ckplayer/ckplayer/ckplayer.js"></script>
    <script type="text/javascript">
        function getQueryString() {
            var qs = location.search.substr(1), // 获取url中"?"符后的字串  
                args = {}, // 保存参数数据的对象
                items = qs.length ? qs.split("&") : [], // 取得每一个参数项,
                item = null,
                len = items.length;

            for (var i = 0; i < len; i++) {
                item = items[i].split("=");
                var name = decodeURIComponent(item[0]),
                    value = decodeURIComponent(item[1]);
                if (name) {
                    args[name] = value;
                }
            }
            return args;
        };
       // var videoUrl = getQueryString()['videoUrl']+"";
        alert('<%=videoUrl %>');
        var videoObject = {
            container: '#video', //容器的ID或className
            variable: 'player',//播放函数名称
            autoplay: true,//视频默认0是暂停，1是播放
            flashplayer: true,//强制使用flashplayer播放
            poster: 'ckplayer/material/poster.jpg',//封面图片
            video: [//视频地址列表形式sss
                ['<%=videoUrl %>', 'video/flv', '英文超清', 0],
            ]
        };
        var player = new ckplayer(videoObject);
		</script>
</body>
</html>
