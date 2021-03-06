﻿using System.Text;
using System.Threading.Tasks;
using DotnetSpider.Core;
using Xunit;

namespace DotnetSpider.Test
{
	public class Site
	{
		private string WName = "WebSite";
		private string WValue = "12580emall";
		private string WDomain = "www.12580emall.com";
		private string Url = @"http://www.12580emall.com/emall/mall/index.html";

		[Fact]
		public void SetEncoding()
		{
			var site = new Core.Site
			{
				EncodingName = "UTF-8"
			};
			Assert.Equal(Encoding.UTF8, site.Encoding);
		}

		[Fact]
		public void AddRequests()
		{
			Core.Site site = new Core.Site { Domain = WDomain, EncodingName = "UTF-8", Timeout = 3000, };
			site.ClearStartRequests();
			site.AddStartUrl(Url);
			site.AddStartRequest(new Request(Url, 1, null));
			Assert.Equal(site.Domain, WDomain);
			Assert.True(site.StartRequests.Contains(new Request(Url, 1, null)));
		}

		[Fact]
		public void AddRequestsAsync()
		{
			Core.Site site = new Core.Site { Domain = WDomain, EncodingName = "UTF-8", Timeout = 3000, };
			site.ClearStartRequests();

			Parallel.For(1, 10000, new ParallelOptions
			{
				MaxDegreeOfParallelism = 10
			}, i =>
			{
				site.AddStartUrl("http://a.com/" + i);
			});

			Assert.Equal(site.Domain, WDomain);
			Assert.True(site.StartRequests.Contains(new Request("http://a.com/1", 1, null)));
		}

		[Fact]
		public void AddHeaders()
		{
			Core.Site site = new Core.Site { Domain = WDomain, EncodingName = "UTF-8", Timeout = 3000, };
			site.AddHeader(WName, WValue);
			Assert.NotNull(site.Headers);
			Assert.True(site.Headers.Count > 0);
		}
	}
}
