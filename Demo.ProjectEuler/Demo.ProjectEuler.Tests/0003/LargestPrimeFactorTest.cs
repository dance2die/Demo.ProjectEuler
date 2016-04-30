﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Demo.ProjectEuler.Tests._0003
{
	public class LargestPrimeFactorTest
	{
		private readonly ITestOutputHelper _output;

		public LargestPrimeFactorTest(ITestOutputHelper output)
		{
			_output = output;
		}

		[Theory]
		[InlineData(-1000, false)]
		[InlineData(-1, false)]
		[InlineData(0, false)]
		[InlineData(1, false)]
		[InlineData(2, true)]
		[InlineData(3, true)]
		[InlineData(4, false)]
		[InlineData(5, true)]
		[InlineData(6, false)]
		[InlineData(7, true)]
		[InlineData(8, false)]
		[InlineData(17, true)]
		[InlineData(100, false)]
		public void TestIsPrimeNumber(int value, bool expectedResult)
		{
			var sut = new LargestPrimeFactor();

			bool actualResult = sut.IsPrimeNumber(value);

			Assert.Equal(expectedResult, actualResult);
		}

		[Theory]
		[InlineData(2, new[] {2})]
		[InlineData(3, new[] {3})]
		[InlineData(4, new[] {2, 2})]
		[InlineData(5, new[] {5})]
		[InlineData(6, new[] {2, 3})]
		[InlineData(7, new[] {7})]
		[InlineData(8, new[] {2, 2, 2})]
		[InlineData(9, new[] {3, 3})]
		[InlineData(10, new[] {2, 5})]
		//[InlineData(15, new[] { 3, 5 })]
		//[InlineData(31, new[] { 31 })]
		//[InlineData(13195, new[] { 5, 7, 13, 29 })]
		public void TestGetPrimeFactors(int value, IEnumerable<int> expectedResult)
		{
			var sut = new LargestPrimeFactor();

			List<int> actualResult = sut.GetPrimeFactors(value);

			Assert.True(actualResult.SequenceEqual(expectedResult));
		}
	}

	public class LargestPrimeFactor
	{
		public List<int> GetPrimeFactors(int value)
		{
			var result = new List<int>();

			if (IsPrimeNumber(value))
			{
				result.Add(value);
				return result;
			}



			return result;
		}

		public bool IsPrimeNumber(int value)
		{
			if (value <= 1) return false;	// by definion of Prime Number

			for (int i = 2; i < value; i++)
			{
				if (value % i == 0) return false;
			}

			return true;
		}
	}
}
