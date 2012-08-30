using Slippor.Utility.WebWorker;

namespace Slippor.Rent.Common
{
	public class GanjiWebWorker : RentWebWorker
	{
		public GanjiWebWorker()
		{
			this.Encoding = System.Text.Encoding.UTF8;
			this.UserAgent = UserAgents.IE9;
			this.Referrer = "http://www.ganji.com";
		}
	}
}