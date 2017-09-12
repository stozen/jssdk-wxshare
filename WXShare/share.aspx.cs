using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WXShare
{
    public class share : System.Web.UI.Page
    {
        private JSSDK sdk;
        private SignPackage signPackage;

        public string AppId;
        public string Timestamp;
        public string NonceStr;
        public string Signature;

        protected void Page_Load(object sender, EventArgs e)
        {
            string appId = ConfigurationManager.AppSettings["appId"];
            string appSecret = ConfigurationManager.AppSettings["appSecret"];
            sdk = new JSSDK(appId, appSecret);
            signPackage = sdk.getSignPackage(HttpContext.Current);

            AppId = signPackage.appId;
            Timestamp = signPackage.timestamp;
            NonceStr = signPackage.nonceStr;
            Signature = signPackage.signature;
        }
    }
}