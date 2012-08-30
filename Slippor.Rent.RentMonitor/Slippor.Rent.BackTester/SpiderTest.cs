using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Slippor.Rent.Common.Spider;

namespace Slippor.Rent.BackTester
{
	public class SpiderTest
	{
		public String TestGanjiSpider()
		{
			return new GanjiSpider().GetContent("http://bj.ganji.com/fang1/shijingshan/");
		}
	}
}
