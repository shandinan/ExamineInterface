using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace VideoService
{
    public partial class VideoXml : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // string strXml = Request.QueryString["xmldata"];
            byte[] byts = new byte[Request.InputStream.Length];
            Request.InputStream.Read(byts, 0, byts.Length);
            string req = HttpUtility.UrlDecode(System.Text.Encoding.Default.GetString(byts));
            string strTest = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><soapenv:Body><ns1:setVideoPara soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:ns1=\"http://webServices.tmri.com\"><para xsi:type=\"xsd:string\">&lt;?xml version=&quot;1.0&quot; encoding=&quot;GBK&quot; ?&gt;&lt;root&gt;&lt;videopara&gt;&lt;cylsh&gt;1190605983558&lt;/cylsh&gt;&lt;cyqxh&gt;320500CY0001&lt;/cyqxh&gt;&lt;cyqtd&gt;3&lt;/cyqtd&gt;&lt;cllx&gt;0&lt;/cllx&gt;&lt;cysj&gt;2019/06/10 13:37:51&lt;/cysj&gt;&lt;cycs&gt;1&lt;/cycs&gt;&lt;sfzmhm&gt;320504198404070034&lt;/sfzmhm&gt;&lt;clsbdh&gt;LC0C34DG6K1017004&lt;/clsbdh&gt;&lt;hphm&gt;&#x82CF;&lt;/hphm&gt;&lt;hpzl&gt;02&lt;/hpzl&gt;&lt;/videopara&gt;&lt;/root&gt;</para></ns1:setVideoPara></soapenv:Body></soapenv:Envelope>";
            string strResponse = DealXML(req);
            // DealXML(req);
            //string strXml = HttpUtility.UrlDecode(Request.QueryString["xmldata"]);//得到对应的xml并解码
            //  string strResponse = "<string xmlns=\"http://webServices.tmri.com\"><?xml version=\"1.0\" encoding=\"GBK\"?><root><head><code>1</code> <message></message> <keystr>3</keystr> </head> <body> </body> </root></string>";
            Response.Write(strResponse);
        }
        /// <summary>
        /// 处理请求的xml数据
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        private string DealXML(string strXml)
        {
            sysLog.WriteOptDisk(strXml);
            string strResponse = "<string xmlns=\"http://webServices.tmri.com\"><?xml version=\"1.0\" encoding=\"GBK\"?><root><head><code>{0}</code> <message>{1}</message> <keystr>{2}</keystr> </head> <body> </body> </root></string>";
            string strKey = "";
            try
            {
                //1 先得到para参数
                XmlDocument allXml = new XmlDocument();
                allXml.LoadXml(strXml);//加载xml

                XmlNamespaceManager man = new XmlNamespaceManager(allXml.NameTable);
                man.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");

                //Querying
                XmlNode titlenode = allXml.SelectSingleNode("/soapenv:Envelope/soapenv:Body", man);
                string strPara = titlenode.InnerText; //得到para参数值
                strPara = HttpUtility.HtmlDecode(strPara);//html解码

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strPara);
                XmlNodeList nodes = xmlDoc.SelectNodes("/root/videopara");
                DataTable dtPara = new DataTable();
                string strcyqxh = "";//查验区序号
                string strcyqtd = "1";//查验区通道
                foreach (XmlNode xnode in nodes)
                {
                    List<object> objList = new List<object>();
                    XmlNodeList listParam = xnode.ChildNodes;
                    foreach (XmlNode nd in listParam) //遍历得到具体参数
                    {
                        string strCol = nd.Name.ToLower();
                        if (strCol == "cyqtd") //查验区通道
                        {
                            strcyqtd = nd.InnerText;
                        }
                        else if (strCol == "cyqxh") //查验区序号
                        {
                            strcyqxh = nd.InnerText;
                        }
                        dtPara.Columns.Add(new DataColumn(strCol));
                        objList.Add(nd.InnerText);
                    }
                    dtPara.Rows.Add(objList.ToArray());
                }

                string strJsonParam = Newtonsoft.Json.JsonConvert.SerializeObject(dtPara);//把dt 转换成json字符串

                //从数据库获取对应的客户端录像IP地址
                string strUrl = "http://192.168.1.221:9999";
                //if (strcyqxh == "320500CY0001") //苏州车管所的查验区通道
                //{
                //    strUrl = "http://192.168.1.221:9999";
                //}
                //string strRedisKey = strcyqxh + "_" + strcyqtd;//查验区序号_查验区通道  组成唯一key
                //try
                //{
                //    if (StackExchangeRedisHelper.Exists(strRedisKey)) //缓存中存在
                //    {
                //        strUrl = StackExchangeRedisHelper.Get(strRedisKey) + "";
                //    }
                //    else //不存在重数据库中读取得到结果并存放在redis中
                //    {
                //        string str_redis_sql = $"SELECT T.PC_IP FROM DEV_DEPART T WHERE T.CYQXH='{strcyqxh}' AND T.CYQTD='{strcyqtd}'";
                //        string str_req_ip = DBUtility.DbHelperOra.GetSingle(str_redis_sql) + "";//得到对于的控制电脑IP
                //        StackExchangeRedisHelper.Set(strRedisKey, str_req_ip);
                //        strUrl = str_req_ip;
                //    }
                //}
                //catch(Exception ex)
                //{
                //    sysLog.WriteOptDisk(ex.Message);
                //    string str_redis_sql = $"SELECT T.PC_IP FROM DEV_DEPART T WHERE T.CYQXH='{strcyqxh}' AND T.CYQTD='{strcyqtd}'";
                //    string str_req_ip = DBUtility.DbHelperOra.GetSingle(str_redis_sql) + "";//得到对于的控制电脑IP
                //    //StackExchangeRedisHelper.Set(strRedisKey, str_req_ip);
                //    strUrl = str_req_ip;
                //}

                switch (strcyqxh)
                {
                    case "320500CY0001": //苏州车管所
                        //  strUrl = "http://192.168.1.221:9999";
                        if (strcyqtd == "1") //苏州车管所
                        {
                            strUrl = "http://192.168.1.221:9999";
                        }else if (strcyqtd == "3") //城北检测线
                        {
                            strUrl = "http://192.167.177.100:9999";
                        }
                        else
                        {
                            strUrl = "http://192.167.177.100:9999";
                        }
                        break;
                    case "320500CY0002": //元素
                        strUrl = "http://192.167.128.129:9999";
                        break;
                    case "320500CY0003": //192.167.105.100 金昌众辉
                        strUrl = "http://192.167.105.100:9999";
                        break;
                    case "320100CY0001"://南京车管所
                         strUrl= "http://11.1.0.243:9999";
                      //  strUrl = "http://192.168.1.51:9999";
                        break;
                    case "320500CY0004": //元通    ================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0008": //甪直东昌
                        strUrl = "http://192.167.123.120:9999";
                        break;
                    case "320500CY0009": //建国
                        strUrl = "http://192.167.111.180:9999";
                        break;
                    case "320500CY0010": //富骏
                        strUrl = "http://192.167.114.100:9999";
                        break;
                    case "320500CY0011": //苏天
                        strUrl = "http://192.167.102.100:9999";
                        break;
                    case "320500CY0012": //欧亚伟业
                        strUrl = "http://192.167.120.103:9999";
                        break;
                    case "320500CY0013": //建通
                        strUrl = "http://192.167.104.100:9999";
                        break;
                    case "320500CY0014": //苏丰
                        strUrl = "http://192.167.127.187:9999";
                        break;
                    case "320500CY0015": //海星   ===================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0017": //浚和   ====================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0018": //苏新   ====================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0019": //中耀    ====================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0020": //天丰    ====================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0021": //国懋    ====================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0022": //联众    ====================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0023": //久久    ====================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0024": //小康    ====================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0025": //汽工贸    ====================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0026": //华意    ====================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0027": //丰丰    ====================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0028": //元星    ====================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0029": //瀚明  
                        strUrl = "http://192.167.158.100:9999";
                        break;
                    case "320500CY0030": //广沣    ====================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0031": //国兴     
                        strUrl = "http://192.167.165.100:9999";
                        break;
                    case "320500CY0032": //众和    ====================
                        strUrl = "http://11.1.0.243:9999";
                        break;
                    case "320500CY0033": //华成吴中    
                        strUrl = "http://192.167.112.100:9999";
                        break;
                    case "320500CY0034": //华成木渎   
                        strUrl = "http://192.167.108.100:9999";
                        break;
                    case "320500CY0035": //华成集团   
                        strUrl = "http://192.167.113.100:9999";
                        break;
                    //case "320500CY0004": //城北检测线
                    //    strUrl = "http://192.167.166.100:9999";
                    //    break;
                    default:
                        strUrl = "http://192.168.1.221:9999";
                        break;

                }

                // string strResData = new sdnHttpWebRequest().sdnDoPost(strUrl, strJsonParam);
                string[] arrObj = new string[] { strUrl, strJsonParam };
                //  new sdnHttpWebRequest().sdnDoPost(strUrl, strJsonParam);
                new Thread(StarPost).Start(arrObj); //异步开启线程 防止录像客户端无响应 2019年9月12日 08点57分
                //通过后台post请求各个网点客户端
                return string.Format(strResponse, "1", "", strKey); ;
            }
            catch (Exception ex)
            {
                sysLog.WriteOptDisk(ex.Message);
                 return string.Format(strResponse, "$E", ex.Message, strKey);
              //  return string.Format(strResponse, "1", "", strKey);
            }
        }
        /// <summary>
        /// 开启post
        /// </summary>
        private void StarPost(object obj)
        {
            string[] arrObj = (string[])obj;
            new sdnHttpWebRequest().sdnDoPost(arrObj[0], arrObj[1]);
        }
    }
}