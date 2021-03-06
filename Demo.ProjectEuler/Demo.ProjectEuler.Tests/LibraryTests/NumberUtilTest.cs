﻿using System.Linq;
using System.Numerics;
using Demo.ProjectEuler.Core;
using Demo.ProjectEuler.Tests.Core;
using Xunit;
using Xunit.Abstractions;

namespace Demo.ProjectEuler.Tests.LibraryTests
{
	public class NumberUtilTest : BaseTest
	{
		private readonly NumberUtil _sut = new NumberUtil();

		public NumberUtilTest(ITestOutputHelper output) : base(output)
		{
		}

		[Theory]
		[InlineData(123, new [] {1,2,3})]
		[InlineData(1, new [] {1})]
		[InlineData(2, new [] {2})]
		[InlineData(2234, new [] {2,2,3,4})]
		[InlineData(1234567890, new [] {1,2,3,4,5,6,7,8,9,0})]
		[InlineData(9876, new [] {9,8,7,6})]
		public void TestNumberToSequence(long input, int[] expected)
		{
			int[] actual = _sut.ToReverseSequence(input).Reverse().ToArray();

			Assert.True(expected.SequenceEqual(actual));
		}
	}
}
