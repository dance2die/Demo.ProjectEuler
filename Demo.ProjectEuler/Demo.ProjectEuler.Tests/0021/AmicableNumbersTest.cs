﻿using System.Collections.Generic;
using System.Linq;
using Demo.ProjectEuler.Core;
using Demo.ProjectEuler.Tests.Core;
using Xunit;
using Xunit.Abstractions;

namespace Demo.ProjectEuler.Tests._0021
{
	public class AmicableNumbersTest : BaseTest
	{
		private readonly AmicableNumbers _sut = new AmicableNumbers();
		public static IEnumerable<object[]> DivisorNumberSumLookupData => new[]
		{
			new object[] { 220, new KeyValuePair<int, int>(220, 284)  },
			new object[] { 284, new KeyValuePair<int, int>(284, 220)  }
		};

		public AmicableNumbersTest(ITestOutputHelper output) : base(output)
		{
		}

		[Theory]
		[InlineData(220, 284)]
		[InlineData(284, 220)]
		public void TestDivisorNumberCounts(int value, int expected)
		{
			int actual = _sut.GetDivisorSum(value);

			Assert.Equal(expected, actual);
		}

		[Theory]
		[MemberData("DivisorNumberSumLookupData")]
		public void TestBuildingDivisorNumberCountLookup(int value, KeyValuePair<int, int> expected)
		{
			KeyValuePair<int, int> actual = _sut.GetPairOfNumberSum(value);

			Assert.True(expected.Key == actual.Key && expected.Value == actual.Value);
			Assert.Equal(expected, actual);
		}
	}

	public class AmicableNumbers
	{
		private readonly Factors _factors = new Factors();

		public int GetDivisorSum(int value)
		{
			return _factors.GetProperDivisors(value).Sum();
		}

		public KeyValuePair<int, int> GetPairOfNumberSum(int value)
		{
			var divisorSum = GetDivisorSum(value);
			return new KeyValuePair<int, int>(value, divisorSum);
		}
	}
}
