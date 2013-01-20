using System;

namespace Slippor.Rent.Common
{
    /// <summary>
    /// 过滤设置
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 租房模式
        /// </summary>
        public RentMode RentMode { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public String City { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public String Range { get; set; }

        /// <summary>
        /// 最高租金
        /// </summary>
        public int? MaxFee { get; set; }

        /// <summary>
        /// 最低租金
        /// </summary>
        public int? MinFee { get; set; }

        /// <summary>
        /// 几室
        /// </summary>
        public int? Room { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public String Keyword { get; set; }

        /// <summary>
        /// 否定关键词
        /// </summary>
        public String NegativeKeyword { get; set; }

        /// <summary>
        /// 房间类型
        /// </summary>
        public RoomMode RoomMode { get; set; }

        /// <summary>
        /// 装修类型
        /// </summary>
        public FitmentMode FitmentMode { get; set; }
    }

    /// <summary>
    /// 租房模式
    /// </summary>
    public enum RentMode
    {
        /// <summary>
        /// 未设置
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// 合租
        /// </summary>
        Shared = 1,

        /// <summary>
        /// 整租
        /// </summary>
        Alone = 2,

        /// <summary>
        /// 床位
        /// </summary>
        Bed = 3,
    }

    /// <summary>
    /// 租房类型
    /// </summary>
    public enum RoomMode
    {
        NotSet = 0,

        /// <summary>
        /// 主卧
        /// </summary>
        Master = 1,

        /// <summary>
        /// 次卧
        /// </summary>
        Sub = 2,

        /// <summary>
        /// 隔断间
        /// </summary>
        Seperate = 3,
    }

    /// <summary>
    /// 装修模式
    /// </summary>
    public enum FitmentMode
    {
        NotSet = 0,

        /// <summary>
        /// 豪华装修
        /// </summary>
        Luxury = 1,

        /// <summary>
        /// 精装修
        /// </summary>
        Fine = 2,

        /// <summary>
        /// 中等装修
        /// </summary>
        Mid = 3,

        /// <summary>
        /// 简单装修
        /// </summary>
        Simple = 4,

        /// <summary>
        /// 毛坯
        /// </summary>
        Blank = 5,
    }
}