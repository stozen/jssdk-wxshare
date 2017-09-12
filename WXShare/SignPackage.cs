namespace WXShare
{
    public class SignPackage
    {
        public string appId { get; set; }
        public string nonceStr { get; set; }
        public string timestamp { get; set; }
        public string url { get; set; }
        public string signature { get; set; }
        public string rawString { get; set; }
    }
}