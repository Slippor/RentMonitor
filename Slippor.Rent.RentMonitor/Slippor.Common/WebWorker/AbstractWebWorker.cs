using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace Slippor.Utility.WebWorker
{
	/// <summary>
	/// 
	/// </summary>
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

		/// <summary>
		/// 超时时长，单位毫秒
		/// </summary>
		protected int TimeOut
		{
			get { return _timeOut; }
			set { _timeOut = value; }
		}

		protected String UserAgent
		{
			get { return _userAgent; }
			set { _userAgent = value; }
		}

		/// <summary>
		/// 对获取的文件内容解码
		/// </summary>
		protected Encoding Encoding
		{
			get { return _encoding; }
			set { _encoding = value; }
		}

		protected int RetryTime
		{
			get { return _retryTime; }
			set { _retryTime = value; }
		}

		private const int _retryPeriod = 1000;
		/// <summary>
		/// 超时时长，单位毫秒
		/// </summary>
		private int _timeOut = 5000;
		private String _userAgent;
		private int _retryTime = 1000;
		private Encoding _encoding = Encoding.UTF8;

		#region IDisposable Members

		public void Dispose()
		{
			_loginUrl = null;
			_referrer = null;
			UserAgent = null;
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
			return Login(loginData, RetryTime);
		}

		/// <summary>
		/// 请求Url，但不获取响应内容
		/// </summary>
		/// <param name="url"></param>
		/// <param name="postBytes"></param>
		/// <param name="cookie"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		public bool Ping(string url, byte[] postBytes, object cookie, RequestMethod method)
		{
			if (AchieveResponse(url, postBytes, cookie, false, method) == String.Empty)
			{
				return true;
			}
			return false;
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <param name="postBytes"></param>
		/// <param name="cookie"></param>
		/// <param name="getResponse"></param>
		/// <param name="method"></param>
		/// <returns>返回为String.Empty，说明访问成功；返回空，说明有错；其他情况返回的是请求URL的内容。</returns>
		public String AchieveResponse(String url, byte[] postBytes, object cookie, bool getResponse, RequestMethod method)
		{
			var request = (HttpWebRequest)WebRequest.Create(url);
			try
			{
				request.Credentials = CredentialCache.DefaultCredentials;
				request.KeepAlive = true;
				request.Accept = "*/*";
				request.UserAgent = UserAgent;
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
				request.Timeout = TimeOut;
				request.ReadWriteTimeout = TimeOut;
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
					String status = response.StatusDescription;
					if (status == "OK")
					{
						if (getResponse)
						{
							Stream dataStream = response.GetResponseStream();
							if (dataStream != null)
							{
								var reader = new StreamReader(dataStream, Encoding);
								try
								{
									var responseFromServer = reader.ReadToEnd();
									reader.Close();
									dataStream.Close();
									return responseFromServer;
								}
								finally
								{
									reader.Close();
									dataStream.Close();
								}
							}
						}
						else
						{
							return String.Empty;
						}
					}
					else
					{
						throw new WebException(String.Format("网站请求出错：状态为{0}。", status));
					}
				}
			}
			finally
			{
				request.Abort();
			}
			return null;
		}

		#endregion

		protected virtual byte[] SetLoginInfo(String username, String password)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 获取Cookie
		/// </summary>
		/// <param name="url"></param>
		/// <param name="postBytes"></param>
		/// <returns></returns>
		private String GetCookie(String url, byte[] postBytes)
		{
			String cookie = null;

			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Credentials = CredentialCache.DefaultCredentials;
			request.CookieContainer = new CookieContainer();
			request.KeepAlive = true;
			request.UserAgent = UserAgent;
			request.Method = "Post";
			request.Timeout = TimeOut;
			request.AllowAutoRedirect = false;
			request.ReadWriteTimeout = TimeOut;
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