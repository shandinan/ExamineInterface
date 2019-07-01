<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamineVideo.aspx.cs" Inherits="VideoService.ExamineVideo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>视频播放区</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="Content-type" content="text/html; charset=utf-8">
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN THEME STYLES -->
    <link href="../assets/global/css/components.css" id="style_components" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout/css/layout.css" rel="stylesheet" type="text/css" />
    <link id="style_color" href="../assets/admin/layout/css/themes/blue.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-table/bootstrap-table.css" rel="stylesheet" />
    <link href="../assets/global/plugins/Thickbox/jquery.thickbox.css" rel="stylesheet" />
</head>
<body>
   
     <!-- BEGIN CONTAINER -->
    <div class="page-container">
      
        <!-- BEGIN CONTENT -->
        <div class="page-content">
        <div class="" style="padding-top: 1px;">
            <div class="page-header">
                 <div style="padding-left:50px;"><h3>流水号：<%=LSH%>,车辆识别代号：<%=CLSBDH %></h3> </div>
              <%-- <div style="padding-left:50px;"><h3>号牌号码：<%=HPHM %>,号牌种类：<%=HPZL %></h3> </div>--%>
           </div>
        <div class="col-sm-2">
            <div class="list-group" id="sdnPlayList">
                <%if(dicVideoPath !=null && dicVideoPath.Count>0){ if(dicVideoPath.ContainsKey("leftfront")){ %>
                <a href="#" class="list-group-item active" id="Leftfront" name="<%=dicVideoPath["leftfront"] %>">
                    <h4 class="list-group-item-heading">车前斜视45度</h4>
                    <p class="list-group-item-text" id="txtLeftfront">正在播放</p>
                </a>
                <%} if(dicVideoPath.ContainsKey("rightback")){  %>
                <a href="#" class="list-group-item" id="Rightback" name="<%=dicVideoPath["rightback"] %>">
                    <h4 class="list-group-item-heading">车后斜视45度</h4>
                    <p class="list-group-item-text" id="txtRightback">点击播放</p>
                </a>
                <%} if(dicVideoPath.ContainsKey("bottom")){  %>
                 <a href="#" class="list-group-item" id="bottom" name="<%=dicVideoPath["bottom"] %>">
                    <h4 class="list-group-item-heading">底盘工位</h4>
                    <p class="list-group-item-text" id="txtbottom">点击播放</p>
                </a>
                 <%} if(dicVideoPath.ContainsKey("speed")){  %>
                 <a href="#" class="list-group-item" id="speed" name="<%=dicVideoPath["speed"] %>">
                    <h4 class="list-group-item-heading">车速工位</h4>
                    <p class="list-group-item-text" id="txtspeed">点击播放</p>
                </a>
                <%} if(dicVideoPath.ContainsKey("brake1")){  %>
                 <a href="#" class="list-group-item" id="brake1" name="<%=dicVideoPath["brake1"] %>" >
                    <h4 class="list-group-item-heading">制动工位1轴</h4>
                    <p class="list-group-item-text" id="txtbrake1">点击播放</p>
                </a>
                 <%} if(dicVideoPath.ContainsKey("brake2")){  %>
                  <a href="#" class="list-group-item" id="brake2" name="<%=dicVideoPath["brake2"] %>">
                    <h4 class="list-group-item-heading">制动工位2轴</h4>
                    <p class="list-group-item-text" id="txtbrake2">点击播放</p>
                </a>
                 <%} if(dicVideoPath.ContainsKey("brake3")){  %>
                  <a href="#" class="list-group-item" id="brake3" name="<%=dicVideoPath["brake3"] %>">
                    <h4 class="list-group-item-heading">制动工位3轴</h4>
                    <p class="list-group-item-text" id="txtbrake3">点击播放</p>
                </a>
                 <%} if(dicVideoPath.ContainsKey("brake4")){  %>
                  <a href="#" class="list-group-item" id="brake4" name="<%=dicVideoPath["brake4"] %>">
                    <h4 class="list-group-item-heading">制动工位4轴</h4>
                    <p class="list-group-item-text" id="txtbrake4">点击播放</p>
                </a>
                  <%} if(dicVideoPath.ContainsKey("brake5")){  %>
                  <a href="#" class="list-group-item" id="brake5" name="<%=dicVideoPath["brake5"] %>">
                    <h4 class="list-group-item-heading">制动工位5轴</h4>
                    <p class="list-group-item-text" id="txtbrake5">点击播放</p>
                </a>
                  <%} if(dicVideoPath.ContainsKey("brake6")){  %>
                  <a href="#" class="list-group-item" id="brake6" name="<%=dicVideoPath["brake6"] %>">
                    <h4 class="list-group-item-heading">制动工位6轴</h4>
                    <p class="list-group-item-text" id="txtbrake6">点击播放</p>
                </a>
                 <%} if(dicVideoPath.ContainsKey("brakeh")){  %>
                 <a href="#" class="list-group-item" id="brakeh" name="<%=dicVideoPath["brakeh"] %>">
                    <h4 class="list-group-item-heading">制动工位驻车</h4>
                    <p class="list-group-item-text" id="txtbrakeh">点击播放</p>
                </a>
                 <%} if(dicVideoPath.ContainsKey("lightlfar")){  %>
                 <a href="#" class="list-group-item" id="lightlfar" name="<%=dicVideoPath["lightlfar"] %>">
                    <h4 class="list-group-item-heading">左灯光工位</h4>
                    <p class="list-group-item-text" id="txtlightlfar">点击播放</p>
                </a>
                  <%} if(dicVideoPath.ContainsKey("lightrfar")){  %>
                  <a href="#" class="list-group-item" id="lightrfar" name="<%=dicVideoPath["lightrfar"] %>">
                    <h4 class="list-group-item-heading">右灯光工位</h4>
                    <p class="list-group-item-text" id="txtlightrfar">点击播放</p>
                </a>
                <%} if(dicVideoPath.ContainsKey("slip")){  %>
                  <a href="#" class="list-group-item" id="slip" name="<%=dicVideoPath["slip"] %>">
                    <h4 class="list-group-item-heading">侧滑工位</h4>
                    <p class="list-group-item-text" id="txtslip">点击播放</p>
                </a>
                 <%} if(dicVideoPath.ContainsKey("roadfront")){  %>
                 <a href="#" class="list-group-item" id="roadfront" name="<%=dicVideoPath["roadfront"] %>">
                    <h4 class="list-group-item-heading">动态/路试工位1</h4>
                    <p class="list-group-item-text" id="txtroadfront">点击播放</p>
                </a>
                 <%} if(dicVideoPath.ContainsKey("roadback")){  %>
                 <a href="#" class="list-group-item" id="roadback" name="<%=dicVideoPath["roadback"] %>">
                    <h4 class="list-group-item-heading">动态/路试工位2</h4>
                    <p class="list-group-item-text" id="txtroadback">点击播放</p>
                </a>
                <%} } %>
            </div>
        </div>
        <div class="col-sm-10" >
          
            <div class="embed-responsive embed-responsive-4by3" >
                <iframe id="videoField" class="embed-responsive-item" src="PlayVideo.aspx" width="1700px" height="1000px" frameborder="0" scrolling="no"></iframe>
            </div>
            
        </div>
            <input type="hidden" id="dbtype" value="1" runat="server" />
    </div>
        </div>
        <!-- END CONTENT -->
    </div>
    <!-- END CONTAINER -->
     <!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
    <!-- BEGIN CORE PLUGINS -->
    <!--[if lt IE 9]>
        <script src="../assets/global/plugins/respond.min.js"></script>
        <script src="../assets/global/plugins/excanvas.min.js"></script> 
        <![endif]-->
    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
    <!-- IMPORTANT! Load jquery-ui-1.10.3.custom.min.js before bootstrap.min.js to fix bootstrap tooltip conflict with jquery ui tooltip -->
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../assets/global/plugins/bootstrap-table/bootstrap-table.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/bootstrap-table/bootstrap-table-zh-CN.min.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/Thickbox/jquery.thickbox.js"></script>
    <!-- END CORE PLUGINS -->

    <script>
        jQuery(document).ready(function () {
            // initiate layout and plugins
          //  Metronic.init(); // init metronic core components
           // Layout.init(); // init current layout
         //   QuickSidebar.init(); // init quick sidebar
           // Demo.init(); // init demo features
            //UIBlockUI.init();
        });
    </script>

    <!--- FOOT SCRIPT-->
     <script type="text/javascript">
        $(document).ready(function () {
          
            $("#sdnPlayList a").click(function () {
                //alert($(this).attr("name"));
                $("#sdnPlayList a").removeClass("active");
                $(this).addClass("active");
                $("#sdnPlayList a p").text("点击播放");
                $(this).find("p").text("正在播放");
               
                var dbtype=$("#dbtype").val();
                if (dbtype == "1") {
                    $("#videoField").attr("src", "PlayVideo.aspx?videoUrl=\\\\192.168.0.248\\" + $(this).attr("name"));
                } else if (dbtype == "2") {
                    $("#videoField").attr("src", "PlayVideo.aspx?videoUrl=\\\\192.168.0.253\\" + $(this).attr("name"));
                } else {
                    $("#videoField").attr("src", "PlayVideo.aspx?videoUrl=\\\\192.168.0.248\\" + $(this).attr("name"));
                }

                 

                //var sstest = $(this).attr("name").replace("Video", "");
                //var ssdddd = "ftp://192.168.0.245" + sstest;
                //alert(ssdddd);
                //$("#videoField").attr("src", ssdddd);
               // alert("PlayVideo.aspx?videoUrl=\\\\192.1.4.102\\" + $(this).attr("name"));
              //  $("#videoField").attr("src", "PlayVideo.aspx?videoUrl=\\\\192.168.21.210\\Videoszjg\\" + $(this).attr("name")); //\\192.168.36.252\Videozjg_new
            });

           // $("#Leftfront").click();
        });
    </script>
    <!-- END FOOT SCRIPT -->

</body>
</html>
