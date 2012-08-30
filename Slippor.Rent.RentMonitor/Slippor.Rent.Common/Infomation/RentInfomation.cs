using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Slippor.Rent.Common
{
	/// <summary>
	/// 租赁信息
	/// </summary>
	public class RentInfomation
	{
		/// <summary>
		/// 出租人
		/// </summary>
		public String Lessor
		{
			get;
			set;
		}

		/// <summary>
		/// 电话号码
		/// </summary>
		public String PhoneNumber
		{
			get;
			set;
		}

		/// <summary>
		/// 标题
		/// </summary>
		public String Title
		{
			get;
			set;
		}

		/// <summary>
		/// 描述
		/// </summary>
		public String Description
		{
			get;
			set;
		}

		/// <summary>
		/// 花费
		/// </summary>
		public double Cost
		{
			get;
			set;
		}

		/// <summary>
		/// 地址
		/// </summary>
		public String Address
		{
			get;
			set;
		}

		/// <summary>
		/// 链接
		/// </summary>
		public String Url
		{
			get;
			set;
		}

		/// <summary>
		/// 发布时间
		/// </summary>
		public DateTime PublishDate
		{
			get;
			set;
		}
	}
}
