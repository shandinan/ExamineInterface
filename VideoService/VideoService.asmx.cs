using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;

namespace VideoService
{
    /// <summary>
    /// VideoService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://webServices.tmri.com", Name = "ns1")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)] 
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class VideoService : System.Web.Services.WebService
    {
        public VideoService()
        {
            HttpRequest request = HttpContext.Current.Request;
            byte[] byts = new byte[request.InputStream.Length];
            request.InputStream.Read(byts, 0, byts.Length);
            string req = HttpUtility.UrlDecode(System.Text.Encoding.Default.GetString(byts));
           // sysLog.WriteOptDisk(para);
        }
      //  [SoapRpcMethod(Use = SoapBindingUse.Literal, Action = "http://webServices.tmri.com/HelloWorld", RequestNamespace = "http://webServices.tmri.com/", ResponseNamespace = "http://webServices.tmri.com/")]
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
       // [SoapRpcMethod(Use = SoapBindingUse.Literal, Action = "http://webServices.tmri.com/setVideoPara1", RequestNamespace = "http://webServices.tmri.com/", ResponseNamespace = "http://webServices.tmri.com/")]
        [WebMethod]
        public string setVideoPara1()
        {
           // string str = @"<?xml version='1.0' encoding='GBK' ><root><videopara><cylsh>1190605983558</cylsh><cyqxh>320500CY0001</cyqxh><cyqtd>1</cyqtd><cllx>0</cllx><cysj>2019/06/10 13:37:51<cysj><cycs>1</cycs><sfzmhm>320504198404070034</sfzmhm><clsbdh>LC0C34DG6K1017004</clsbdh><hphm>&#x82CF;</hphm><hpzl>02</hpzl></videopara></root>";
            return "";
        }
        /// <summary>
        /// 无锡所PDA 视频监管系统对接
        /// </summary>
        /// <param name="WriteXmlDoc"></param>
        /// <returns></returns>
       // [SoapRpcMethod(Use = SoapBindingUse.Default, Action = "http://webServices.tmri.com/setVideoPara", RequestNamespace = "http://webServices.tmri.com/", ResponseNamespace = "http://webServices.tmri.com/")]        
        [WebMethod]
        public string setVideoPara(string para)
        {
            HttpRequest request = HttpContext.Current.Request;
            byte[] byts = new byte[request.InputStream.Length];
            request.InputStream.Read(byts, 0, byts.Length);
            string req = HttpUtility.UrlDecode(System.Text.Encoding.Default.GetString(byts));
            sysLog.WriteOptDisk(para);

            return "<?xml version=\"1.0\" encoding=\"GBK\"?><root><head><code>1</code> <message></message> <keystr>3</keystr> </head> <body> </body> </root>";

        }
        /// <summary>
        /// 处理PDA发送过来的视频监管数据
        /// </summary>
        /// <param name="strXML"></param>
        /// <returns></returns>
        private string deal_XMLInfo(string strXML)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strXML);
                return "";

            }
            catch (Exception e)
            {
                return "";
            }
        }

    }
}
