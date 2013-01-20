using System;

namespace Slippor.Rent.Common
{
    public enum SiteRegion : int
    {
        Beijing = 1,
    }

    public class GanjiFilterBuilder : AbstractFilterBuilder
    {
        public SiteRegion SiteRegion { get; set; }

        private static String GetSiteRegionDomainPrefix(SiteRegion siteRegion)
        {
            switch (siteRegion)
            {
                case SiteRegion.Beijing:
                    return "bj";
                default:
                    throw new InvalidOperationException("不支持的地域。");
            }
        }

        /// <summary>
        /// 赶集网的基本Url
        /// </summary>
        protected override string QueryBaseUrl
        {
            get
            {
                //基本Url和地域相关
                return String.Format("http://{0}.ganji.com/fang1/", GetSiteRegionDomainPrefix(SiteRegion));
            }
        }

        protected override string BuildQueryString(FilterConfig filterConfig)
        {
            String queryString = String.Empty;
            var regionSegment = GetRegionSegment(filterConfig);
            var houseSegment = GetHouseSegment(filterConfig);
            var priceSegment = GetPriceSegment(filterConfig);
            var modeSegment = GetModeSegment(filterConfig);
            if (!String.IsNullOrEmpty(regionSegment))
            {
                queryString += regionSegment + "//";
            }
            var detailSegment = String.Empty;
            if (filterConfig.RentMode == RentMode.Alone)
            {
                if (String.IsNullOrEmpty(houseSegment))
                {
                    detailSegment += houseSegment;
                }
            }

            if (String.IsNullOrEmpty(priceSegment))
            {
                detailSegment += priceSegment;
            }
            if (String.IsNullOrEmpty(modeSegment))
            {
                detailSegment += modeSegment;
            }
            if (!String.IsNullOrEmpty(detailSegment))
            {
                queryString += detailSegment + "//";
            }
            return queryString;
        }

        /// <summary>
        /// 获取地域节信息
        /// </summary>
        /// <param name="filterConfig"></param>
        /// <returns></returns>
        private String GetRegionSegment(FilterConfig filterConfig)
        {
            var region = String.Empty;
            if (!String.IsNullOrEmpty(filterConfig.City))
            {
                region = filterConfig.City;
            }
            if (!String.IsNullOrEmpty(filterConfig.Range))
            {
                region = filterConfig.Range;
            }
            return region;
        }

        /// <summary>
        /// 获取房型信息
        /// </summary>
        /// <param name="filterConfig"></param>
        /// <returns></returns>
        private String GetHouseSegment(FilterConfig filterConfig)
        {
            var house = String.Empty;
            var room = filterConfig.Room;
            if (room.HasValue)
            {
                if (room <= 6)
                {
                    house = "h" + room;
                }
                else
                {
                    //赶集五室以上归为统一的五室以上。
                    house = "h6";
                }
            }
            return house;
        }

        /// <summary>
        /// 获取租金信息
        /// </summary>
        /// <param name="filterConfig"></param>
        /// <returns></returns>
        private String GetPriceSegment(FilterConfig filterConfig)
        {
            var price = String.Empty;
            var min = filterConfig.MinFee;
            var max = filterConfig.MaxFee;
            if (min.HasValue || max.HasValue)
            {
                string minExpression;
                if (!min.HasValue)
                {
                    minExpression = "b0";
                }
                else
                {
                    minExpression = "b" + min;
                }
                string maxExpression;
                if (!max.HasValue)
                {
                    maxExpression = "e0";
                }
                else
                {
                    maxExpression = "e" + max;
                }
                price = minExpression + maxExpression;
            }
            return price;
        }

        /// <summary>
        /// 获取方式（不限,整租,合租,合租床位）信息
        /// </summary>
        /// <param name="filterConfig"></param>
        /// <returns></returns>
        private String GetModeSegment(FilterConfig filterConfig)
        {
            String mode = String.Empty;
            switch (filterConfig.RentMode)
            {
                case RentMode.Alone:
                    mode = "m1";
                    break;
                case RentMode.Shared:
                    break;
                case RentMode.NotSet:
                    break;
                case RentMode.Bed:
                    break;
                default:
                    throw new InvalidOperationException("不支持的租房方式。");
            }
            return mode;
        }

    }
}