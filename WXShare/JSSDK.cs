using System;
using System.Text;
using System.IO;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace WXShare
{
    public class JSSDK
    {
        private string appId;
        private string appSecret;

        private string[] ASPX_HEAD = {
            "<%@ Page Language=\"C#\" AutoEventWireup=\"true\" CodeBehind=\"jsapi_ticket.aspx.cs\" Inherits=\"WXShare.jsapi_ticket\" %>",
            "<%@ Page Language=\"C#\" AutoEventWireup=\"true\" CodeBehind=\"access_token.aspx.cs\" Inherits=\"WXShare.access_token\" %>"
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        public JSSDK(string appId, string appSecret)
        {
            this.appId = appId;
            this.appSecret = appSecret;
        }

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public SignPackage getSignPackage(HttpContext context)
        {
            string jsApiTicket = getJsApiTicket();
            string url = context.Request.Url.AbsoluteUri;
            long time = Utils.ConvertTimeStamp(DateTime.Now);
            string nonceStr = createNonceStr();
            string raw = "jsapi_ticket=" + jsApiTicket + "&noncestr=" + nonceStr + "&timestamp=" + time + "&url=" + url;
            string signature = Utils.Sha1Sign(raw);
            return new SignPackage()
            {
                appId = this.appId,
                nonceStr = nonceStr,
                timestamp = time.ToString(),
                url = url,
                signature = signature,
                rawString = raw
            };
        }

        /// <summary>
        /// 生成随机Nonce字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string createNonceStr(int length = 16)
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder sb = new StringBuilder();
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(chars.Substring(rd.Next(0, chars.Length), 1));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取ticket
        /// </summary>
        /// <returns></returns>
        private string getJsApiTicket()
        {
            string ticket = string.Empty;
            var data = JObject.Parse(getAspxFile("jsapi_ticket.aspx", ASPX_HEAD[0]));
            if (data != null && long.Parse(data["expire_time"].ToString()) < Utils.ConvertTimeStamp(DateTime.Now))
            {
                string accessToken = getAccessToken();
                string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?type=jsapi&access_token="
                    + accessToken;
                var jRes = JObject.Parse(httpGet(url));
                ticket = jRes["ticket"].ToString();
                if (!string.IsNullOrEmpty(ticket))
                {
                    data["expire_time"] = Utils.ConvertTimeStamp(new DateTime()) + 7000;
                    data["jsapi_ticket"] = ticket;
                    setAspxFile("jsapi_ticket.aspx", data.ToString(), ASPX_HEAD[0]);
                }
            }
            else
                ticket = data["jsapi_ticket"].ToString();
            return ticket;
        }

        /// <summary>
        /// 获取access token
        /// </summary>
        /// <returns></returns>
        private string getAccessToken()
        {
            string accessToken = string.Empty;
            var data = JObject.Parse(getAspxFile("access_token.aspx", ASPX_HEAD[1]));
            if (data != null && long.Parse(data["expire_time"].ToString()) < Utils.ConvertTimeStamp(DateTime.Now))
            {
                string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid="
                    + this.appId + "&secret=" + this.appSecret;
                var jRes = JObject.Parse(httpGet(url));
                accessToken = jRes["access_token"].ToString();
                if (!string.IsNullOrEmpty(accessToken))
                {
                    data["expire_time"] = Utils.ConvertTimeStamp(new DateTime()) + 7000;
                    data["access_token"] = accessToken;
                    setAspxFile("access_token.aspx", data.ToString(), ASPX_HEAD[1]);
                }
            }
            else
                accessToken = data["access_token"].ToString();
            return accessToken;
        }

        /// <summary>
        /// Http Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string httpGet(string url)
        {
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }

        /// <summary>
        /// 加载安全aspx文件内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="headString"></param>
        /// <returns></returns>
        private string getAspxFile(string fileName, string headString)
        {
            string content = string.Empty;
            StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + fileName);
            content = sr.ReadToEnd();
            sr.Close();
            return content.Substring(headString.Length);
        }

        /// <summary>
        /// 写入安全aspx文件内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        /// <param name="headString"></param>
        private void setAspxFile(string fileName, string content, string headString)
        {
            FileStream fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + fileName, FileMode.Create);
            byte[] data = Encoding.Default.GetBytes(headString + content);
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
        }
    }
}