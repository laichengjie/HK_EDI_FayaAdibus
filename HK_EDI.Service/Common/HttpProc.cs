using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.Common
{
    /// <summary> 
    /// 上传数据参数 
    /// </summary> 
    public class UploadEventArgs : EventArgs
    {
        int bytesSent;
        int totalBytes;
        /// <summary> 
        /// 已发送的字节数 
        /// </summary> 
        public int BytesSent
        {
            get { return bytesSent; }
            set { bytesSent = value; }
        }
        /// <summary> 
        /// 总字节数 
        /// </summary> 
        public int TotalBytes
        {
            get { return totalBytes; }
            set { totalBytes = value; }
        }
    }

    /// <summary> 
    /// 下载数据参数 
    /// </summary> 
    public class DownloadEventArgs : EventArgs
    {
        int bytesReceived;
        int totalBytes;
        byte[] receivedData;
        /// <summary> 
        /// 已接收的字节数 
        /// </summary> 
        public int BytesReceived
        {
            get { return bytesReceived; }
            set { bytesReceived = value; }
        }
        /// <summary> 
        /// 总字节数 
        /// </summary> 
        public int TotalBytes
        {
            get { return totalBytes; }
            set { totalBytes = value; }
        }
        /// <summary> 
        /// 当前缓冲区接收的数据 
        /// </summary> 
        public byte[] ReceivedData
        {
            get { return receivedData; }
            set { receivedData = value; }
        }
    }

    public class WebClient
    {
        Encoding encoding = Encoding.UTF8;
        string respHtml = "";
        WebProxy proxy;
        static CookieContainer cc;
        WebHeaderCollection requestHeaders;
        WebHeaderCollection responseHeaders;
        int bufferSize = 152400;
        string userAgent= "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        public event EventHandler<UploadEventArgs> UploadProgressChanged;
        public event EventHandler<DownloadEventArgs> DownloadProgressChanged;
        static WebClient()
        {
            LoadCookiesFromDisk();
        }
        /// <summary> 
        /// 创建WebClient的实例 
        /// </summary> 
        public WebClient()
        {
            requestHeaders = new WebHeaderCollection();
            responseHeaders = new WebHeaderCollection();
        }
        /// <summary> 
        /// 设置发送和接收的数据缓冲大小 
        /// </summary> 
        public int BufferSize
        {
            get { return bufferSize; }
            set { bufferSize = value; }
        }
        /// <summary> 
        /// 获取响应头集合 
        /// </summary> 
        public WebHeaderCollection ResponseHeaders
        {
            get { return responseHeaders; }
        }
        /// <summary> 
        /// 获取请求头集合 
        /// </summary> 
        public WebHeaderCollection RequestHeaders
        {
            get { return requestHeaders; }
        }
        /// <summary> 
        /// 获取或设置代理 
        /// </summary> 
        public WebProxy Proxy
        {
            get { return proxy; }
            set { proxy = value; }
        }
        /// <summary> 
        /// 获取或设置请求与响应的文本编码方式 
        /// </summary> 
        public Encoding Encoding
        {
            get { return encoding; }
            set { encoding = value; }
        }
        /// <summary> 
        /// 获取或设置响应的html代码 
        /// </summary> 
        public string RespHtml
        {
            get { return respHtml; }
            set { respHtml = value; }
        }
        /// <summary> 
        /// 获取或设置与请求关联的Cookie容器 
        /// </summary> 
        public CookieContainer CookieContainer
        {
            get { return cc; }
            set { cc = value; }
        }
        /// <summary> 
        ///  获取网页源代码 
        /// </summary> 
        /// <param name="url">网址</param> 
        /// <returns></returns> 
        public string GetHtml(string url)
        {
            HttpWebRequest request = CreateRequest(url, "GET");
            respHtml = encoding.GetString(GetData(request));
            return respHtml;
        }

        public CredentialCache CredentialCache
        {
            get;
            set;
        }
        /// <summary> 
        /// 下载文件 
        /// </summary> 
        /// <param name="url">文件URL地址</param> 
        /// <param name="filename">文件保存完整路径</param> 
        public void DownloadFile(string url, string filename)
        {
            FileStream fs = null;
            try
            {
                HttpWebRequest request = CreateRequest(url, "GET");
                byte[] data = GetData(request);
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
                fs.Write(data, 0, data.Length);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
        /// <summary> 
        /// 从指定URL下载数据 
        /// </summary> 
        /// <param name="url">网址</param> 
        /// <returns></returns> 
        public byte[] GetData(string url)
        {
            HttpWebRequest request = CreateRequest(url, "GET");
            return GetData(request);
        }
        /// <summary> 
        /// 向指定URL发送文本数据 
        /// </summary> 
        /// <param name="url">网址</param> 
        /// <param name="postData">urlencode编码的文本数据</param> 
        /// <returns></returns> 
        public string Post(string url, string postData)
        {
            byte[] data = encoding.GetBytes(postData);
            return Post(url, data);
        }
        /// <summary> 
        /// 向指定URL发送文本数据 
        /// </summary> 
        /// <param name="url">网址</param> 
        /// <param name="postData">urlencode编码的文本数据</param> 
        /// <returns></returns> 
        public string Post(string url, string postData, string contentType)
        {
            byte[] data = encoding.GetBytes(postData);
            return Post(url, data, contentType);
        }

        /// <summary> 
        /// 向指定URL发送文本数据 
        /// </summary> 
        /// <param name="url">网址</param> 
        /// <param name="postData">urlencode编码的文本数据</param> 
        /// <returns></returns> 
        public string PostEx(string url, string postData, string contentType)
        {
            byte[] data = encoding.GetBytes(postData);
            return PostEx(url, data, contentType);
        }


        /// <summary>  
        /// 
        /// </summary>  
        /// <param name="url">请求后台地址</param>  
        /// <param name="content">Post提交数据内容(utf-8编码的)</param>  
        /// <returns></returns>  
        public  string HttpPost(string url, string HeaderValue)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            //req.Headers.Add("HEADER", HeaderValue[0]);
            //req.Headers.Add("PASSWORD", HeaderValue[1]);
            //req.Headers.Add("DATA", HeaderValue[2]);
            #region 添加Post 参数  
            byte[] data = Encoding.UTF8.GetBytes(HeaderValue);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容  
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        /// <summary>
        /// Post提交数据
        /// </summary>
        /// <param name="postUrl">URL</param>
        /// <param name="paramData">参数</param>
        /// <returns></returns>
        public string PostWebRequest(string postUrl, string paramData)
        {
            string ret = string.Empty;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
                                                    | SecurityProtocolType.Tls
                                                    | (SecurityProtocolType)0x300 //Tls11
                                                    | (SecurityProtocolType)0xC00; //Tls12

                if (!postUrl.StartsWith("https://"))
                    return "";

                byte[] byteArray = Encoding.Default.GetBytes(paramData); //转化 /
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ret;
        }
        /// <summary> 
        /// 向指定URL发送文本数据 
        /// </summary> 
        /// <param name="url">网址</param> 
        /// <param name="postData">urlencode编码的文本数据</param> 
        /// <returns></returns> 
        public string PostEx2(string url, string postData, string contentType)
        {
            byte[] data = encoding.GetBytes(postData);
            return PostEx2(url, data, contentType);
        }

        /// <summary> 
        /// 向指定URL发送字节数据 
        /// </summary> 
        /// <param name="url">网址</param> 
        /// <param name="postData">发送的字节数组</param> 
        /// <returns></returns> 
        public string PostEx(string url, byte[] postData, string contentType = "application/x-www-form-urlencoded")
        {
            HttpWebRequest request = CreateRequestEx(url, "POST");
            request.ContentType = contentType;
            request.ContentLength = postData.Length;
            request.KeepAlive = true;
            PostData(request, postData);
            //respHtml = encoding.GetString(GetDataEx(request));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            HttpStatusCode httpStatusCode = response.StatusCode;
            return httpStatusCode.ToString(); ;
        }
        /// <summary> 
        /// 向指定URL发送字节数据 
        /// </summary> 
        /// <param name="url">网址</param> 
        /// <param name="postData">发送的字节数组</param> 
        /// <returns></returns> 
        public string Post(string url, byte[] postData ,string contentType = "application/x-www-form-urlencoded")
        {
            HttpWebRequest request = CreateRequest(url, "POST");
            request.ContentType = contentType;
            request.ContentLength = postData.Length;
            request.KeepAlive = true;
            PostData(request, postData);
            respHtml = encoding.GetString(GetData(request));
            return respHtml;
        }

        /// <summary> 
        /// 向指定URL发送字节数据 
        /// </summary> 
        /// <param name="url">网址</param> 
        /// <param name="postData">发送的字节数组</param> 
        /// <returns></returns> 
        public string PostEx2(string url, byte[] postData, string contentType = "application/x-www-form-urlencoded")
        {
            HttpWebRequest request = CreateRequest(url, "POST");
            request.ContentType = contentType;
            request.ContentLength = postData.Length;
            request.KeepAlive = true;
            PostData(request, postData);
            respHtml = encoding.GetString(GetDataEx2(request));
            return respHtml;
        }

        /// <summary> 
        /// 向指定URL发送字节数据 
        /// </summary> 
        /// <param name="url">网址</param> 
        /// <param name="postData">发送的字节数组</param> 
        /// <returns></returns> 
        public string Post(string url, string data,WebHeaderCollection header, string contentType = "application/x-www-form-urlencoded")
        {
            byte[] postData = encoding.GetBytes(data);
            HttpWebRequest request = CreateRequest(url, "POST");
            request.ContentType = contentType;
            request.ContentLength = postData.Length;
            request.KeepAlive = true;
            foreach (string key in header.Keys)
            {
                request.Headers.Add(key, header[key]);
            }
            PostData(request, postData);
            respHtml = encoding.GetString(GetData(request));
            return respHtml;
        }
        /// <summary> 
        /// 向指定网址发送mulitpart编码的数据 
        /// </summary> 
        /// <param name="url">网址</param> 
        /// <param name="mulitpartForm">mulitpart form data</param> 
        /// <returns></returns> 
        public string Post(string url, MultipartForm mulitpartForm)
        {
            HttpWebRequest request = CreateRequest(url, "POST");
            request.ContentType = mulitpartForm.ContentType;
            request.ContentLength = mulitpartForm.FormData.Length;
            request.KeepAlive = true;
            PostData(request, mulitpartForm.FormData);
            var stream = GetData(request);
            respHtml = encoding.GetString(stream);
            return respHtml;
        }
        /// <summary> 
        /// 读取请求返回的数据 
        /// </summary> 
        /// <param name="request">请求对象</param> 
        /// <returns></returns> 
        private byte[] GetData(HttpWebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            responseHeaders = response.Headers;
            //SaveCookiesToDisk();

            DownloadEventArgs args = new DownloadEventArgs();
            if (responseHeaders[HttpResponseHeader.ContentLength] != null)
                args.TotalBytes = Convert.ToInt32(responseHeaders[HttpResponseHeader.ContentLength]);

            MemoryStream ms = new MemoryStream();
            int count = 0;
            byte[] buf = new byte[bufferSize];
            while ((count = stream.Read(buf, 0, buf.Length)) > 0)
            {
                ms.Write(buf, 0, count);
                if (this.DownloadProgressChanged != null)
                {
                    args.BytesReceived += count;
                    args.ReceivedData = new byte[count];
                    Array.Copy(buf, args.ReceivedData, count);
                    this.DownloadProgressChanged(this, args);
                }
            }
            stream.Close();
            //解压 
            if (ResponseHeaders[HttpResponseHeader.ContentEncoding] != null)
            {
                MemoryStream msTemp = new MemoryStream();
                count = 0;
                buf = new byte[100];
                switch (ResponseHeaders[HttpResponseHeader.ContentEncoding].ToLower())
                {
                    case "gzip":
                        GZipStream gzip = new GZipStream(ms, CompressionMode.Decompress);
                        while ((count = gzip.Read(buf, 0, buf.Length)) > 0)
                        {
                            msTemp.Write(buf, 0, count);
                        }
                        return msTemp.ToArray();
                    case "deflate":
                        DeflateStream deflate = new DeflateStream(ms, CompressionMode.Decompress);
                        while ((count = deflate.Read(buf, 0, buf.Length)) > 0)
                        {
                            msTemp.Write(buf, 0, count);
                        }
                        return msTemp.ToArray();
                    default:
                        break;
                }
            }
            return ms.ToArray();
        }

        /// <summary> 
        /// 读取请求返回的数据 
        /// </summary> 
        /// <param name="request">请求对象</param> 
        /// <returns></returns> 
        private byte[] GetDataEx2(HttpWebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            responseHeaders = response.Headers;
            //SaveCookiesToDisk();

            DownloadEventArgs args = new DownloadEventArgs();
            var nContentLength = 0;
            if (responseHeaders.AllKeys.Contains("Content-Length"))
            {
               
                for (var i = 0; i < responseHeaders.AllKeys.Length; i++)
                {
                    if ("Content-Length" == responseHeaders.AllKeys[i])
                    {
                        nContentLength = i;
                    }
                }
            }
            if (responseHeaders[nContentLength] != null)
                args.TotalBytes = Convert.ToInt32(responseHeaders[nContentLength]);

            MemoryStream ms = new MemoryStream();
            int count = 0;
            byte[] buf = new byte[bufferSize];
            while ((count = stream.Read(buf, 0, buf.Length)) > 0)
            {
                ms.Write(buf, 0, count);
                if (this.DownloadProgressChanged != null)
                {
                    args.BytesReceived += count;
                    args.ReceivedData = new byte[count];
                    Array.Copy(buf, args.ReceivedData, count);
                    this.DownloadProgressChanged(this, args);
                }
            }
            stream.Close();

            return ms.ToArray();
        }


        /// <summary> 
        /// 读取请求返回的数据 
        /// </summary> 
        /// <param name="request">请求对象</param> 
        /// <returns></returns> 
        private byte[] GetDataEx(HttpWebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            responseHeaders = response.Headers;
            //SaveCookiesToDisk();

            DownloadEventArgs args = new DownloadEventArgs();
            if (responseHeaders[HttpResponseHeader.ContentLength] != null)
                args.TotalBytes = Convert.ToInt32(responseHeaders[HttpResponseHeader.ContentLength]);

            MemoryStream ms = new MemoryStream();
            int count = 0;
            byte[] buf = new byte[bufferSize];
            while ((count = stream.Read(buf, 0, buf.Length)) > 0)
            {
                ms.Write(buf, 0, count);
                if (this.DownloadProgressChanged != null)
                {
                    args.BytesReceived += count;
                    args.ReceivedData = new byte[count];
                    Array.Copy(buf, args.ReceivedData, count);
                    this.DownloadProgressChanged(this, args);
                }
            }
            stream.Close();
            return ms.ToArray();
        }
        /// <summary> 
        /// 发送请求数据 
        /// </summary> 
        /// <param name="request">请求对象</param> 
        /// <param name="postData">请求发送的字节数组</param> 
        private void PostData(HttpWebRequest request, byte[] postData)
        {
            int offset = 0;
            int sendBufferSize = bufferSize;
            int remainBytes = 0;
            Stream stream = request.GetRequestStream();
            UploadEventArgs args = new UploadEventArgs();
            args.TotalBytes = postData.Length;
            while ((remainBytes = postData.Length - offset) > 0)
            {
                if (sendBufferSize > remainBytes) sendBufferSize = remainBytes;
                stream.Write(postData, offset, sendBufferSize);
                offset += sendBufferSize;
                if (this.UploadProgressChanged != null)
                {
                    args.BytesSent = offset;
                    this.UploadProgressChanged(this, args);
                }
            }
            stream.Close();
        }
        /// <summary> 
        /// 创建HTTP请求 
        /// </summary> 
        /// <param name="url">URL地址</param> 
        /// <returns></returns> 
        private HttpWebRequest CreateRequest(string url, string method)
        {
            Uri uri = new Uri(url);

            if (uri.Scheme == "https")
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);

            // Set a default policy level for the "http:" and "https" schemes. 
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
            HttpWebRequest.DefaultCachePolicy = policy;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AllowAutoRedirect = false;
            request.AllowWriteStreamBuffering =false;
            request.Method = method;
            if (CredentialCache != null)
            {
                request.Credentials = CredentialCache;
                //string authByte ="NTLM " + Convert.ToBase64String(new ASCIIEncoding().GetBytes
                //    (
                //        CredentialCache.DefaultNetworkCredentials.UserName + ":" + 
                //        CredentialCache.DefaultNetworkCredentials.Password
                //    ));
                //request.Headers.Add("Authorization", authByte);
            }

            if (proxy != null) request.Proxy = proxy;
            request.CookieContainer = cc;
            foreach (string key in requestHeaders.Keys)
            {
                request.Headers.Add(key, requestHeaders[key]);
            }
            requestHeaders.Clear();
            return request;
        }

        /// <summary> 
        /// 创建HTTP请求 
        /// </summary> 
        /// <param name="url">URL地址</param> 
        /// <returns></returns> 
        private HttpWebRequest CreateRequestEx(string url, string method)
        {
            Uri uri = new Uri(url);

            if (uri.Scheme == "https")
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);

            // Set a default policy level for the "http:" and "https" schemes. 
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
            HttpWebRequest.DefaultCachePolicy = policy;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AllowAutoRedirect = false;
            request.AllowWriteStreamBuffering = true;
            request.Method = method;
            request.UserAgent = userAgent;
            if (CredentialCache != null)
            {
                request.Credentials = CredentialCache;
                //string authByte ="NTLM " + Convert.ToBase64String(new ASCIIEncoding().GetBytes
                //    (
                //        CredentialCache.DefaultNetworkCredentials.UserName + ":" + 
                //        CredentialCache.DefaultNetworkCredentials.Password
                //    ));
                //request.Headers.Add("Authorization", authByte);
            }
      

            if (proxy != null) request.Proxy = proxy;
            request.CookieContainer = cc;
            foreach (string key in requestHeaders.Keys)
            {
                request.Headers.Add(key, requestHeaders[key]);
            }
            requestHeaders.Clear();
            return request;
        }
        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
        /// <summary> 
        /// 将Cookie保存到磁盘 
        /// </summary> 
        private static void SaveCookiesToDisk()
        {
            string cookieFile = System.Environment.GetFolderPath(Environment.SpecialFolder.Cookies) + "\\webclient.cookie";
            FileStream fs = null;
            try
            {
                fs = new FileStream(cookieFile, FileMode.Create);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formater.Serialize(fs, cc);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
        /// <summary> 
        /// 从磁盘加载Cookie 
        /// </summary> 
        private static void LoadCookiesFromDisk()
        {
            cc = new CookieContainer();
            string cookieFile = System.Environment.GetFolderPath(Environment.SpecialFolder.Cookies) + "\\webclient.cookie";
            if (!File.Exists(cookieFile))
                return;
            FileStream fs = null;
            try
            {
                fs = new FileStream(cookieFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                cc = (CookieContainer)formater.Deserialize(fs);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
    }

    /// <summary> 
    /// 对文件和文本数据进行Multipart形式的编码 
    /// </summary> 
    public class MultipartForm
    {
        private Encoding encoding;
        private string boundary;
        private MemoryStream ms;
        private Dictionary<string, object> keys;
        private Dictionary<string, object> files;

        /// <summary> 
        /// 获取编码后的字节数组 
        /// </summary> 
        public byte[] FormData
        {
            get
            {
                if (ms == null)
                {
                    ms = new MemoryStream();
                    StringBuilder sb = new StringBuilder();
                    if (keys != null)
                    {
                        foreach (var item in keys.Keys)
                        {
                            var values = getFileValues(keys, item);
                            foreach (var val in values)
                            {
                                if (files != null)
                                {
                                    sb.AppendFormat("--{0}\r\n", this.boundary);
                                    sb.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n", item);
                                    sb.Append("Content-Type: text/plain\r\n\r\n");
                                    sb.AppendFormat("{0}\r\n", val);
                                }
                                else
                                {
                                    if (sb.Length > 0)
                                        sb.Append("&");
                                    sb.Append(item + "=" + val);
                                }
                            }
                        }
                        var keyBuf = encoding.GetBytes(sb.ToString());
                        ms.Write(keyBuf, 0, keyBuf.Length);
                    }

                    if (files != null)
                    {
                        foreach (var item in files.Keys)
                        {
                            ArrayList values = getFileValues(files, item);
                            foreach (var val in values)
                            {
                                sb.Remove(0, sb.Length);
                                FileStream fs = null;
                                byte[] fileData = { };
                                try
                                {
                                    fs = new FileStream(val.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
                                    fileData = new byte[fs.Length];
                                    fs.Read(fileData, 0, fileData.Length);
                                    sb.AppendFormat("--{0}\r\n", this.boundary);
                                    sb.AppendFormat("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n", item, Path.GetFileName(val.ToString()));
                                    sb.AppendFormat("Content-Type: {0}\r\n\r\n", this.GetContentType(val.ToString()));
                                    byte[] buf = encoding.GetBytes(sb.ToString());
                                    ms.Write(buf, 0, buf.Length);
                                    ms.Write(fileData, 0, fileData.Length);
                                    byte[] crlf = encoding.GetBytes("\r\n");
                                    ms.Write(crlf, 0, crlf.Length);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    if (fs != null) fs.Close();
                                }
                            }
                        }
                        byte[] buffer = encoding.GetBytes("--" + this.boundary + "--\r\n");
                        ms.Write(buffer, 0, buffer.Length);
                    }
                }

                return ms.ToArray();
            }
        }

        /// <summary> 
        /// 获取此编码内容的类型 
        /// </summary> 
        public string ContentType
        {
            get { return files != null ? string.Format("multipart/form-data; boundary={0}", this.boundary) : "application/x-www-form-urlencoded"; }
        }
        /// <summary> 
        /// 获取或设置对字符串采用的编码类型 
        /// </summary> 
        public Encoding StringEncoding
        {
            set { encoding = value; }
            get { return encoding; }
        }
        /// <summary> 
        /// 实例化 
        /// </summary> 
        public MultipartForm()
        {
            boundary = string.Format("--{0}--", Guid.NewGuid());
            encoding = Encoding.UTF8;
        }
        /// <summary> 
        /// 添加一个文件 
        /// </summary> 
        /// <param name="name">文件域名称</param> 
        /// <param name="filename">文件的完整路径</param> 
        public void AddFlie(string name, string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException("尝试添加不存在的文件。", filename);

            if (files == null)
                files = new Dictionary<string, object>();
            AddField(name, filename, files);
            ms = null;
        }

        /// <summary> 
        /// 添加字符串 
        /// </summary> 
        /// <param name="name">文本域名称</param> 
        /// <param name="value">文本值</param> 
        public void AddString(string name, string value)
        {
            if (keys == null)
                keys = new Dictionary<string, object>();
            AddField(name, value, keys);
            ms = null;
        }
        /// <summary> 
        /// 从注册表获取文件类型 
        /// </summary> 
        /// <param name="filename">包含扩展名的文件名</param> 
        /// <returns>如：application/stream</returns> 
        private string GetContentType(string filename)
        {
            Microsoft.Win32.RegistryKey fileExtKey = null; ;
            string contentType = "application/stream";
            try
            {
                fileExtKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(Path.GetExtension(filename));
                contentType = fileExtKey.GetValue("Content Type", contentType).ToString();
            }
            finally
            {
                if (fileExtKey != null) fileExtKey.Close();
            }
            return contentType;
        }

        private void AddField(string name, string value, Dictionary<string, object> dic)
        {
            if (dic.ContainsKey(name))
            {
                ArrayList valList = getFileValues(dic, name);
                valList.Add(value);
                dic[name] = valList.ToArray(typeof(string));
            }
            else
            {
                dic.Add(name, value);
            }
        }

        private ArrayList getFileValues(Dictionary<string, object> dic, string key)
        {
            ArrayList valList = new ArrayList();
            object valObj = dic[key];
            if (valObj.GetType() == typeof(string))
            {
                valList.Add(valObj.ToString());
            }
            else if (valObj.GetType() == typeof(string[]))
            {
                foreach (string strval in (string[])valObj)
                {
                    valList.Add(strval);
                }
            }
            return valList;
        }
    }
}
