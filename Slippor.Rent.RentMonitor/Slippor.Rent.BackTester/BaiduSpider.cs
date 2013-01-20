using Slippor.Common.WebWorker;
using Slippor.Rent.Common;
using Slippor.Rent.Common.Spider;

namespace Slippor.Rent.BackTester
{
	public class BaiduSpider : AbstractSpider
	{
		#region Overrides of AbstractSpider

		protected override IWebWorker WebWorker
		{
			get { return new GanjiWebWorker(); }
		}

		#endregion
	}
}