using System;

namespace Slippor.Utility.WebWorker
{
	public static class UserAgents
	{
		public static String IE9 { get { return "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C)"; } }
		public static String IE8 { get { return "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C)"; } }
		public static String IE7 { get { return "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C)"; } }
		public static String IE6 { get { return "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1)"; } }

		public static String Firefox8 { get { return "Mozilla/5.0 (Windows NT 6.1; rv:8.0) Gecko/20111108 Firefox/8.0"; } }

		public static String Chrome16 { get { return "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.3 (KHTML, like Gecko) Chrome/16.0.877.0 Safari/535.3"; } }

		public static String BaiduSpider { get { return "Baiduspider"; } }
	}
}