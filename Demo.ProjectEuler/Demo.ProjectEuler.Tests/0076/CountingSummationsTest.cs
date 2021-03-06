﻿using Demo.ProjectEuler.Tests.Core;
using Xunit;
using Xunit.Abstractions;

namespace Demo.ProjectEuler.Tests._0076
{
	public class CountingSummationsTest : BaseTest
	{
		private readonly CountingSummations _sut = new CountingSummations();

		public CountingSummationsTest(ITestOutputHelper output) : base(output)
		{
		}

		[Theory]
		[InlineData(2, 1)]
		[InlineData(3, 2)]
		[InlineData(4, 4)]
		[InlineData(5, 6)]
		[InlineData(100, 190569291)]
		public void TestSummationCounting(int n, int expected)
		{
			int actual = _sut.GetSummationCount(n);

			Assert.Equal(expected, actual);
		}
	}

	public class CountingSummations
	{
		/// <summary>
		/// Get summation count using Partition function algorithm
		/// </summary>
		/// <remarks>
		/// Refer to graph on <see cref="http://math.stackexchange.com/q/411901"/>
		/// 
		/// Math World <see cref="http://mathworld.wolfram.com/PartitionFunctionP.html"/>
		///  P(n,k) = P(n-1,k-1) + P(n-k,k) 
		/// </remarks>
		public int GetSummationCount(int n)
		{
			return GetParitionCount(n, n) - 1;
		}

		private int GetParitionCount(int n, int k)
		{
			if (k == 0) return 0;
			if (n == 0) return 1;
			if (n < 0) return 0;

			return GetParitionCount(n, k - 1) + GetParitionCount(n - k, k);
		}
	}
}
