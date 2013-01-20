using System;
using System.Collections.Generic;
using Slippor.Rent.Common.Spider;

namespace Slippor.Rent.BackTester
{
    public class SpiderTest
    {
        public String TestGanjiSpider()
        {
            return new GanjiSpider().GetContent("http://bj.ganji.com/fang1/shijingshan/");
        }

        public String TestBaiduSpider()
        {
            var dic = new Dictionary<String, String>();
            dic.Add("keyword", "电子商务");
            dic.Add("area", "1");
            dic.Add("pageNo", "0");
            dic.Add("device", "1");
            return new BaiduSpider().PostContent("http://fengchao.baidu.com/nirvana/request.ajax?path=GET/Preview", dic);
        }
    }
}
