using System;

namespace Slippor.Rent.Common.Spider
{
    /// <summary>
    /// 蜘蛛
    /// </summary>
    public interface ISpider
    {
        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="url">要获取内容的Url</param>
        /// <returns></returns>
        String GetContent(String url);

    }
}