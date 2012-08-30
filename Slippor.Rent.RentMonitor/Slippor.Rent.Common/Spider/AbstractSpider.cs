using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Slippor.Utility.WebWorker;

namespace Slippor.Rent.Common.Spider
{
	public abstract class AbstractSpider : ISpider
	{

		protected abstract IWebWorker WebWorker { get; }

		public String GetContent(String url)
		{
			return WebWorker.AchieveResponse(url, null, null, true, RequestMethod.Get);
		}
	}
}
