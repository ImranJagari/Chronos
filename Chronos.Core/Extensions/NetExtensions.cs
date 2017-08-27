using System.Net;

namespace Chronos.Core.Extensions
{
    public static class NetExtensions
    {
        public static string RequestMD5(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Method = "HEAD";
            string result;
            using (WebResponse response = webRequest.GetResponse())
            {
                result = response.Headers.Get("Content-MD5");
            }
            return result;
        }

        public static long RequestContentLenght(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Method = "HEAD";
            long contentLength;
            using (WebResponse response = webRequest.GetResponse())
            {
                webRequest.Abort();
                contentLength = response.ContentLength;
            }
            return contentLength;
        }
    }
}