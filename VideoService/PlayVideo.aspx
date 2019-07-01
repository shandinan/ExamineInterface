<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayVideo.aspx.cs" Inherits="VideoService.PlayVideo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        body {
            margin: 0px;
            padding: 0px;
        }
    </style>
    <title>mp4Player</title>
    <% 
        var videoUrl = "";
        if (!String.IsNullOrEmpty(Request["videoUrl"]))
        {
            videoUrl = Request["videoUrl"].ToString();

            //url=url.Substring(6);}

            //url = @"\\192.168.0.207\" + url;
            //videoUrl = @"\\192.1.6.89\video\HK_leftfront.mp4";
            //videoUrl = @"\\192.1.6.143\Video\jinfu\2015\05\17\1\E11111\HK_leftfront.mp4";
            //url = url;
        }
        //videoUrl = @"\\192.1.6.143\Video\jinfu\2015\05\17\1\E11111\HK_leftfront.mp4";
    %>
</head>
<body style="width: auto; height: auto">
    <div id="sdnMedia">
        <object id="sdnPlayer" classid="CLSID:2CE620BF-AD0E-41C3-80D4-2734C27D2366" width="1200px" height="800px">
            <param name="url" value="<%=videoUrl %>">
        </object>
        <object style="display:none;" id="Player" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6" width="1400" height="800">
            <param name="autoStart" value="False">
            <param name="url" value="<%=videoUrl %>">
            <param name="autoSize" value="True">
            <param name="fullScreen" value="0">
        </object>
    </div>
    <!--
    <object id="Player" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6" width="1400" height="800">
        <param name="autoStart" value="True">
        <param name="url" value="<%=videoUrl %>">
        <param name="autoSize" value="True">
        <param name="fullScreen" value="0">
    </object>
    -->
</body>
</html>
<script type="text/javascript">
    // window.onload = checkOCX;
    function checkOCX() {
        var sdnPlayer = document.getElementById("sdnPlayer");
        if (sdnPlayer.object == null) {
            //  alert("插件注册失败");
            <%-- var varHtml = "<object id='Player' classid='CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6' width='1400' height='800'>";
            varHtml += " <param name='autoStart' value='True'>";
            varHtml += "<param name='url' value='<%=videoUrl %>'>";
            varHtml += "<param name='autoSize' value='True'>";
            varHtml += "<param name='fullScreen' value='0'></object>";
            alert(varHtml);
            document.getElementById("sdnMedia").innerHTML = ""; //清空原有插件
            document.getElementById("sdnMedia").innerHTML = varHtml;--%> //加载windowMediaPlayer
            document.getElementById("sdnPlayer").style.display = "none";
            var playObj = document.getElementById("Player");
            document.getElementById("Player").style.display = "";
            playObj.controls.play();//Windows Media Player 播放视频
           
        }
        else {
         
            sdnPlayer.sdnPlay();
            // sdnPlayer.sdnOpen("\\\\192.168.0.245\\Video_old\\jinfu\\2016\\03\\01\\1\\苏E3DA50\\1\\YW_brake1162425.mp4");
    }
    }
    window.onload = checkOCX;
</script>

