<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="flvPlayer.aspx.cs" Inherits="VideoService.flvPlayer" %>

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
    <title>mp4Player</title>
    <% 
        var videoUrl = "";
        if (!String.IsNullOrEmpty(Request["videoUrl"]))
        {
            videoUrl = Request["videoUrl"].ToString();
        }
    %>
</head>
<body>
    <div>
       
    </div>
    <object classid='clsid:D27CDB6E-AE6D-11cf-96B8-4445535411111'
        codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0'
        width="500" height="400">
        <param name='movie' value='assets/flvplayer/Flvplayer.swf' />
        <param name='quality' value='high' />
        <param name='allowFullScreen' value='true' />
        <param name='FlashVars'
          value="vcastr_file=<%=videoUrl%>&IsAutoPlay=1&IsContinue=1" />
        <embed src='assets/flvplayer/Flvplayer.swf' allowfullscreen='true'
            flashvars='vcastr_file=<%=videoUrl%>&IsAutoPlay=1&IsContinue=1'
            quality='high'
            pluginspage='http://www.macromedia.com/go/getflashplayer'
            type='application/x-shockwave-flash' width="500" height="400" />
    </object>
</body>
</html>
