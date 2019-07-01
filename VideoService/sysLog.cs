using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace VideoService
{
    public class sysLog
    {
        private static Exception EX = null;//错误消息
        private static string Leve = null;//错误级别
        private static string Position = null;//错误位置
        private static string SavePath = null; //保存位置

        /// <summary>
        /// 文件写入硬盘 操作日志
        /// </summary>
        /// <param name="str"></param>
        public static void WriteOptDisk(string carnum, string state, string msg, string m, string mm)
        {
            string SavePath = AppDomain.CurrentDomain.BaseDirectory;
            FileStream fs = null;
            try
            {
                string str = "时间:" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ms") + "\t";

                str += "操作信息:窗口：" + carnum + "卡地址：" + state + "端口：" + msg + "波特率：" + m + "票号：" + mm + "\t";
                str += "\r\n\r\n\r\n";
                string path = SavePath + "/Logs/OptLogs/" + DateTime.Now.ToString("yyyyMM") + "/" + carnum + "/" + DateTime.Now.ToString("yyyyMMdd") + "Log.db";

                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(SavePath + "/Logs/OptLogs/" + DateTime.Now.ToString("yyyyMM") + "/" + carnum + "/");
                    File.Create(path).Close();
                }

                //字符串写入硬盘
                byte[] bytData = System.Text.Encoding.Default.GetBytes(str);
                fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 2048);
                fs.Write(bytData, 0, bytData.Length);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }


        /// <summary>
        /// 文件写入硬盘 操作日志
        /// </summary>
        /// <param name="str"></param>
        public static void WriteOptDisk(string msg)
        {
            string SavePath = AppDomain.CurrentDomain.BaseDirectory;
            FileStream fs = null;
            try
            {
                string str = "时间:" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ms") + "\t";

                str += "操作信息:" + msg + "\t";
                str += "\r\n\r\n\r\n";
                string path = SavePath + "/Logs/OptLogs/" + DateTime.Now.ToString("yyyyMM") + "/888/" + DateTime.Now.ToString("yyyyMMdd") + "Log.db";

                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(SavePath + "/Logs/OptLogs/" + DateTime.Now.ToString("yyyyMM") + "/888/");
                    File.Create(path).Close();
                }

                //字符串写入硬盘
                byte[] bytData = System.Text.Encoding.Default.GetBytes(str);
                fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 2048);
                fs.Write(bytData, 0, bytData.Length);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

    }
}