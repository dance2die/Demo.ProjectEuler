﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Demo.ProjectEuler.Core;
using Demo.ProjectEuler.Tests.Core;
using Xunit;
using Xunit.Abstractions;

namespace Demo.ProjectEuler.Tests._0037
{
	public class TruncatablePrimesTest : BaseTest
	{
		private readonly TruncatablePrimes _sut = new TruncatablePrimes();

		public TruncatablePrimesTest(ITestOutputHelper output) : base(output)
		{
		}

		[Theory]
		[InlineData(2, false)]
		[InlineData(3, false)]
		[InlineData(5, false)]
		[InlineData(7, false)]
		[InlineData(3797, true)]
		public void TestTruncablePrime(int prime, bool expected)
		{
			bool actual = _sut.IsTruncablePrime(prime);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ShowResult()
		{
			BigInteger result = _sut.GetTruncablePrimes();

			_output.WriteLine(result.ToString());
		}
	}

	public class TruncatablePrimes
	{
		private readonly Prime _prime = new Prime();
		private readonly List<int> _processedPrimes = new List<int>();

		public BigInteger GetTruncablePrimes()
		{
			const int primeCountTotal = 11;	// given on the problem page.

			int number = 22;
			int counter = 0;

			List<int> truncablePrimes = new List<int>();

			do
			{
				do
				{
					number++;
				} while (!IsTruncablePrime(number));

				_processedPrimes.Add(number);
				truncablePrimes.Add(number);

				counter++;
			} while (counter < primeCountTotal);

			return truncablePrimes.Sum();
		}

		public bool IsTruncablePrime(int prime)
		{
			if (prime < 10) return false;
			if (!_prime.IsPrimeNumber(prime)) return false;

			if (prime == 3797)
			{
				Console.WriteLine(prime);
			}

			string primeTextLeftToRight = prime.ToString();
			string primeTextRightToLeft = primeTextLeftToRight;
			// Remove number from left to right.
			// Remove number from right to left.
			for (int i = 0; i < prime.ToString().Length - 1; i++)
			{
				primeTextLeftToRight = primeTextLeftToRight.Substring(1);
				primeTextRightToLeft = primeTextRightToLeft.Substring(0, primeTextRightToLeft.Length - 1);

				double primeTextLeftToRightValue = Convert.ToDouble(primeTextLeftToRight);
				double primeTextRightToLeftValue = Convert.ToDouble(primeTextRightToLeft);

				if (_processedPrimes.Contains((int)primeTextLeftToRightValue) ||
					_processedPrimes.Contains((int)primeTextRightToLeftValue))
					return true;

				if (!_prime.IsPrimeNumber(primeTextLeftToRightValue) || 
					!_prime.IsPrimeNumber(primeTextRightToLeftValue))
					return false;

			}

			return true;
		}
	}
}
