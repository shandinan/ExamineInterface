using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VideoService
{
    public partial class ExamineVideo : System.Web.UI.Page
    {

        public string HPHM = string.Empty; //号牌号码
        public string HPZL = string.Empty; //号牌种类
        public string CLSBDH = string.Empty;//车辆识别代号
        public string LSH = string.Empty;//流水号
        public string JYLSH = string.Empty;//检验流水号
        public string CYQTD = string.Empty;//查验区通道
        string strOldPath = "Video-old"; //老视频路径
        string strNewPath = "Video";//新视频路径
        int iDay = -7;//视频转存天数
        // public string jylsh = string.Empty;//检测流水号
        //   public string strRes = string.Empty;//
        public Dictionary<string, string> dicVideoPath = new Dictionary<string, string>();//视频种类和路径
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RWWebconfig rwPath = (RWWebconfig)ConfigurationManager.GetSection("filepath");
                RWWebconfig rwIday = (RWWebconfig)ConfigurationManager.GetSection("iday");
                strOldPath = rwPath.SdnName;
                strNewPath = rwPath.SdnValue;
                iDay = Convert.ToInt32(rwIday.SdnValue);

                HPHM = Request["hphm"]; //从URL中解析出号牌号码
                HPZL = Request["hpzl"]; //从URL中解析出好怕种类
                CLSBDH = Request["clsbdh"];//车辆识别代号
                LSH = Request["lsh"]; //流水号
                JYLSH = Request["jylsh"];//检验流水号
                CYQTD = Request["cyqtd"];//查验区通道


                //jylsh = Request.QueryString["jylsh"];
                if (string.IsNullOrEmpty(HPHM) || string.IsNullOrEmpty(HPZL))
                {
                    //号牌号码 和号牌种类其中一个为空
                }
                else
                {
                    //string test = Server.UrlDecode(HPHM);

                    HPHM = HPHM.ToUpper();
                    HPZL = HPZL.ToUpper();
                    // HPHM = HttpUtility.UrlDecode(HPHM).ToUpper();//转换给大写
                    //   HPZL = HttpUtility.UrlDecode(HPZL).ToUpper();//号牌种类转换成大写
                }
                LoadVideoType();//加载视频

            }
        }

        /// <summary>
        /// 动态加载视频类型
        /// select  * from (
        /// select t.*,
        // row_number() OVER(PARTITION BY t.cameraname   ORDER BY t.begintime desc) as row_flg   
        //from REC_VIDEOPATHTEMP t where   t.begintime >= '2015/06/13 12:00:00'  and  t.hphm = '苏E206BT' and t.hpzl='02'
        /// 
        /// </summary>
        private void LoadVideoType()
        {
            try
            {
            //    string strSql = "select * from REC_VIDEOPATHTEMP t where t.vin ='{0}' order by id desc";
                string strSql = "select * from (select * from REC_VIDEOPATHTEMP t where t.endtime is not null and t.jclsh ='{0}'  order by id desc) where rownum <=2  order by rownum desc";
                strSql = string.Format(strSql, LSH); //根据流水号获取录像表中的路径
                DataTable dt = DBUtility.DbHelperOra.Query(strSql).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DateTime dtBeginTime = Convert.ToDateTime(dr["begintime"].ToString()); //得到时间格式的录像开始时间
                        if (dtBeginTime.DayOfYear < (DateTime.Now.DayOfYear + iDay)) //三天之前
                        {
                            dicVideoPath.Add(dr["cameraname"].ToString(),strOldPath+"\\"+ dr["path"].ToString()); //添加对于的视频种类和路径
                        }
                        else //三天之后
                        {
                            dicVideoPath.Add(dr["cameraname"].ToString(), strNewPath + "\\" + dr["path"].ToString()); //添加对于的视频种类和路径
                        }
                      //  dicVideoPath.Add(dr["cameraname"].ToString(), dr["path"].ToString()); //添加对于的视频种类和路径
                    }
                    dbtype.Value = "1";//245 新数据库
                }
                //以上是从207数据库获取视频信息
            }
            catch
            { }
        }
    }
}