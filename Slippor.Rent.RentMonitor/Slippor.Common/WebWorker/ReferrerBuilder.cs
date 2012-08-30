using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Slippor.Common.WebWorker
{
	public static class ReferrerBuilder
	{
		public static String GetBaiduReferrer(String keyword)
		{
			return String.Format("http://www.baidu.com/s?wd={0}", keyword);
		}

		public static String GetGoogleReferrer(String keyword)
		{
			return String.Format("http://www.google.com/search?q={0}", keyword);
		}
	}
}
