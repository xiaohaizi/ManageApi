using ManageApi.Model;
using ManageApi.Model.ModelBll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ManageApi.Common
{
    public class Business
    {
        private static char[] constant =
        {
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        };
        //默认密钥向量
        private static byte[] Keys = { 0xEF, 0xAB, 0x56, 0x78, 0x90, 0x34, 0xCD, 0x12 };
        ///   <summary>
        ///   给一个字符串进行MD5加密
        ///   </summary>
        ///   <param   name="strText">待加密字符串</param>
        ///   <returns>加密后的字符串</returns>
        public static string MD5Encrypt(string strText, string encrypt = "")
        {
            strText = strText + encrypt;
            string returnStr = "";
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(strText));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                returnStr = sBuilder.ToString().ToLower();

            }


            return returnStr;
        }

        /// <summary>
        /// 验证密码是否正确
        /// </summary>
        /// <param name="_checkpwd"></param>
        /// <param name="pwd"></param>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        public static bool CheckMD5(string _checkpwd, string pwd, string encrypt = "")
        {
            string strText = _checkpwd + encrypt;
            string check_pwd = MD5Encrypt(_checkpwd, encrypt);
            bool check = false;
            check = check_pwd == pwd;
            return check;
        }



        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string GetTimestamp(DateTime dtime)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            var span = dtime - startTime; // 相差毫秒数
            return Convert.ToUInt64(span.TotalSeconds).ToString();
        }


        /// <summary>  
        /// 时间戳转为C#格式时间  
        /// </summary>  
        /// <param name="timeStamp">Unix时间戳格式</param>  
        /// <returns>C#格式时间</returns>  
        public static DateTime GetDateTime(string timeStamp)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区

            DateTime newDateTime = startTime.AddSeconds(Convert.ToUInt64(timeStamp));
            return newDateTime;
        }


        /// <summary>
        /// 随机 生成字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandom(int length = 8)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(26);
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(constant[rd.Next(26)]);
            }
            string randstr = newRandom.ToString();
            return randstr;
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ReadFile(string filename)
        {
            string restOfStream = "";
            filename = Config.AppPath + filename;
            try
            {
                StreamReader sr = File.OpenText(filename);
                string nextLine;
                restOfStream = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {

            }
            return restOfStream;
        }


        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="str"></param>
        /// <param name="filename"></param>
        /// <param name="append">true 是 追加, false 为覆盖原文件 </param>
        public static void WritFile(string str, string filename, bool append = false)
        {
            try
            {
                filename = Config.AppPath + filename;
                // true 是 append text, false 为覆盖原文件 
                StreamWriter sw = new StreamWriter(filename, append, Encoding.UTF8);
                sw.Write(str);
                sw.Close();
            }
            catch (Exception ex)
            {

            }
        }


        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        /// <summary>
        /// 递归获取菜单
        /// </summary>
        /// <param name="_menuList"></param>
        /// /// <param name="_count_level">菜单层级</param>
        /// <returns></returns>
        public static List<ReturnMenu> RecursionMenu(List<ReturnMenu> _menuList,int _count_level=2)
        {
            var p_list = _menuList.Where(x => x.Pid == 0).ToList();
            foreach (var _menu in p_list)
            {
                ChildMenu(_menuList, _menu ,1, _count_level);
            }
            return p_list;


        }
        /// <summary>
        /// 递归获取子菜单
        /// </summary>
        /// <param name="_menuList"></param>
        /// <param name="c_menu"></param>
        /// <param name="level">层级</param>
        public static void ChildMenu(List<ReturnMenu> _menuList, ReturnMenu c_menu,int level,int _count_level)
        {
            foreach (var item in _menuList)
            {
                var child = _menuList.Where(x => x.Pid == c_menu.ID).ToList();
                if (child.Count > 0&& level< _count_level)
                {
                    level = level + 1;
                    foreach (var item_1 in child)
                    {
                        c_menu.Child = child;                        
                        ChildMenu(_menuList, item_1, level, _count_level);
                        
                    }
                }

            }
        }


    }
}
