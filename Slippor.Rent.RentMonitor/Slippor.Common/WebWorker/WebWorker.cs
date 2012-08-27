using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Text;

namespace Slippor.Utility.WebWorker
{
    public abstract class AbstractWebWorker : IWebWorker, IDisposable
    {
        private String _loginUrl;
        protected String LoginUrl
        {
            get { return _loginUrl; }
            set { _loginUrl = value; }
        }

        private String _referrer;
        protected String Referrer
        {
            get { return _referrer; }
            set { _referrer = value; }
        }

        private WebProxy _proxy;
        public WebProxy Proxy
        {
            get { return _proxy; }
            set { _proxy = value; }
        }

        private const int _retryPeriod = 1000;
        protected int _timeOut = 5000;
        protected String _userAgent;
        protected int _retryTime = 1000;
        protected Encoding _encoding = Encoding.UTF8;

        #region IDisposable Members

        public void Dispose()
        {
            _loginUrl = null;
            _referrer = null;
            _userAgent = null;
        }

        #endregion

        #region IWebWorker Members

        public String Login(String username, String password)
        {
            byte[] byteArray = SetLoginInfo(username, password);
            return Login(byteArray);
        }

        public String Login(byte[] loginData)
        {
            return Login(loginData, _retryTime);
        }

        public String Login(byte[] loginData, int retryTime)
        {
            if (loginData != null)
            {
                try
                {
                    return GetCookie(_loginUrl, loginData);
                }
                catch
                {
                    retryTime--;
                    if (retryTime > 0)
                    {
                        Thread.Sleep(_retryPeriod);
                        return Login(loginData, retryTime);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                throw new Exception("No LoginInfo.");
            }
        }

        public String Get(String url)
        {
            return AchieveResponse(url, null, null, true, RequestMethod.Get);
        }

        public String Post(String url)
        {
            return AchieveResponse(url, null, null, true, RequestMethod.Post);
        }

        public String AchieveResponse(String url, byte[] postBytes, object cookie, bool getStream, RequestMethod method)
        {
            String responseFromServer;
            var request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.Credentials = CredentialCache.DefaultCredentials;
                request.KeepAlive = true;
                request.Accept = "*/*";
                request.UserAgent = _userAgent;
                request.Referer = _referrer;
                request.Method = method.ToString();
                if (cookie != null)
                {
                    if (cookie is CookieContainer)
                    {
                        request.CookieContainer = (CookieContainer)cookie;
                    }
                    else
                    {
                        request.Headers.Add("cookie:" + cookie);
                    }
                }
                request.Timeout = _timeOut;
                request.ReadWriteTimeout = _timeOut;
                request.Proxy = Proxy;

                switch (method)
                {
                    case RequestMethod.Post:
                        request.ContentType = "application/x-www-form-urlencoded";
                        if (postBytes != null)
                        {
                            request.ContentLength = postBytes.Length;
                            Stream newStream = request.GetRequestStream();
                            try
                            {
                                newStream.Write(postBytes, 0, postBytes.Length);
                            }
                            finally
                            {
                                newStream.Close();
                            }
                        }
                        break;
                    case RequestMethod.Get:
                        request.ContentType = "text/html";
                        break;
                }
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    responseFromServer = response.StatusDescription;
                    if (responseFromServer == "OK" && getStream)
                    {
                        Stream dataStream = response.GetResponseStream();
                        if (dataStream != null)
                        {
                            var reader = new StreamReader(dataStream, _encoding);
                            try
                            {
                                responseFromServer = reader.ReadToEnd();
                                reader.Close();
                                dataStream.Close();
                            }
                            finally
                            {
                                reader.Close();
                                dataStream.Close();
                            }
                        }
                    }
                }
            }
            finally
            {
                request.Abort();
            }
            return responseFromServer;
        }

        #endregion

        protected abstract byte[] SetLoginInfo(String username, String password);

        private String GetCookie(String url, byte[] postBytes)
        {
            String cookie = null;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.CookieContainer = new CookieContainer();
            request.KeepAlive = true;
            request.UserAgent = _userAgent;
            request.Method = "Post";
            request.Timeout = _timeOut;
            request.AllowAutoRedirect = false;
            request.ReadWriteTimeout = _timeOut;
            request.Proxy = Proxy;

            request.ContentType = "application/x-www-form-urlencoded";
            if (postBytes != null)
            {
                request.ContentLength = postBytes.Length;
                Stream newStream = request.GetRequestStream();
                newStream.Write(postBytes, 0, postBytes.Length);
                newStream.Close();
            }

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusDescription == "OK" || response.StatusDescription == "Found")
                {
                    cookie = request.CookieContainer.GetCookieHeader(new Uri(url));
                }
                response.Close();
            }
            finally
            {
                request.Abort();
            }
            return cookie;
        }
    }
}