﻿using System;
using System.Net;

namespace Slippor.Utility.WebWorker
{
	/// <summary>
	/// 网站工具接口
	/// </summary>
	public interface IWebWorker
	{
		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="username">用户名</param>
		/// <param name="password">密码</param>
		/// <returns>返回的Cookie</returns>
		String Login(String username, String password);

		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="loginData">登录信息</param>
		/// <returns>返回的Cookie</returns>
		String Login(byte[] loginData);

		/// <summary>
		/// 请求Url，但不获取响应内容
		/// </summary>
		/// <param name="url"></param>
		/// <param name="postBytes"></param>
		/// <param name="cookie"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		bool Ping(String url, byte[] postBytes, object cookie, RequestMethod method);

		/// <summary>
		/// 获取响应
		/// </summary>
		/// <param name="url">目标Url</param>
		/// <param name="postBytes">发送数据</param>
		/// <param name="cookie">Cookie</param>
		/// <param name="getResponse">是否获取响应内容</param>
		/// <param name="method">获取方式</param>
		/// <returns>响应内容</returns>
		String AchieveResponse(String url, byte[] postBytes, object cookie, bool getResponse, RequestMethod method);
	}

	public enum RequestMethod
	{
		/// <summary>
		/// Get方法
		/// </summary>
		Get,

		/// <summary>
		/// Post方法
		/// </summary>
		Post
	}
}