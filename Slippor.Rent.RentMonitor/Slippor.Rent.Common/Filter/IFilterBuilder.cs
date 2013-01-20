using System;

namespace Slippor.Rent.Common
{
    /// <summary>
    /// 过滤构造器
    /// </summary>
    public interface IFilterBuilder
    {
        /// <summary>
        /// 构造过滤Url
        /// </summary>
        /// <param name="filterConfig"></param>
        /// <returns></returns>
        String BuildFilterUrl(FilterConfig filterConfig);
    }
}