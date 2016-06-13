﻿using System;
using System.Collections.Generic;
using System.Linq;
using Demo.ProjectEuler.Core;
using Demo.ProjectEuler.Tests.Core;
using Xunit;
using Xunit.Abstractions;

namespace Demo.ProjectEuler.Tests._0032
{
	public class PandigitalProductsTest : BaseTest
	{
		private readonly PandigitalProducts _sut = new PandigitalProducts();

		public PandigitalProductsTest(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public void TestIsSpecialPandigital()
		{
			var specialPandigitalCandidate = new Tuple<int, int, int>(39, 186, 7254);
			bool isSpecialPandigital = _sut.IsSpecialPandigital(specialPandigitalCandidate);

			Assert.True(isSpecialPandigital);
		}

		[Fact]
		public void TestSlicingIndexes()
		{
			// Number of possible slicing of 9 digit number string
			var expected = new []{
				new Tuple<int, int, int>(1, 1, 7), new Tuple<int, int, int>(1, 2, 6), new Tuple<int, int, int>(1, 3, 5), new Tuple<int, int, int>(1, 4, 4),
				new Tuple<int, int, int>(1, 5, 3), new Tuple<int, int, int>(1, 6, 2), new Tuple<int, int, int>(1, 7, 1),

				new Tuple<int, int, int>(2, 1, 6), new Tuple<int, int, int>(2, 2, 5), new Tuple<int, int, int>(2, 3, 4),
				new Tuple<int, int, int>(2, 4, 3), new Tuple<int, int, int>(2, 5, 2), new Tuple<int, int, int>(2, 6, 1),

				new Tuple<int, int, int>(3, 1, 5), new Tuple<int, int, int>(3, 2, 4), new Tuple<int, int, int>(3, 3, 3),
				new Tuple<int, int, int>(3, 4, 2), new Tuple<int, int, int>(3, 5, 1),

				new Tuple<int, int, int>(4, 1, 4), new Tuple<int, int, int>(4, 2, 3),
				new Tuple<int, int, int>(4, 3, 2), new Tuple<int, int, int>(4, 4, 1),

				new Tuple<int, int, int>(5, 1, 3), new Tuple<int, int, int>(5, 2, 2), new Tuple<int, int, int>(5, 3, 1),

				new Tuple<int, int, int>(6, 1, 2), new Tuple<int, int, int>(6, 2, 1),

				new Tuple<int, int, int>(7, 1, 1),
			};

			const int length = 9;
			var actual = _sut.GetPandigitalSlicingIndexes(length);

			Assert.True(CompareTupleList(expected, actual));
		}

		///// <remarks>
		///// Takes about 0.5 seconds to permutate 9 digit number
		///// </remarks>
		//[Fact]
		//public void TestPermutations()
		//{
		//	Permutation permutation = new Permutation();
		//	IEnumerable<IEnumerable<char>> permutations = permutation.GetPermutations("123456789".ToCharArray().ToList());

		//	foreach (IEnumerable<char> chars in permutations)
		//	{
		//		string text = new string(chars.ToArray());
		//	}
		//}

		[Fact]
		public void ShowResult()
		{
			Permutation permutation = new Permutation();
			const string pandigital = "123456789";
			IEnumerable<IEnumerable<char>> permutations = permutation.GetPermutations(pandigital.ToCharArray().ToList());
			Tuple<int, int, int>[] pandigitalSlicingIndexes = _sut.GetPandigitalSlicingIndexes(pandigital.Length);

			List<int> products = new List<int>();
			Dictionary<Tuple<int, int>, int> distinctCombinations = new Dictionary<Tuple<int, int>, int>();

			int counter = 0;
			foreach (IEnumerable<char> chars in permutations)
			{
				string text = new string(chars.ToArray());
				foreach (Tuple<int, int, int> indexes in pandigitalSlicingIndexes)
				{
					int multiplicand = Convert.ToInt32(text.Substring(0, indexes.Item1));
					int multiplier = Convert.ToInt32(text.Substring(indexes.Item1, indexes.Item2));
					int product = Convert.ToInt32(text.Substring(indexes.Item2 + 1, indexes.Item3));

					//if (multiplicand == 39 && multiplier == 186 && product == 7254)
					if (counter == 116611 && indexes.Item1 == 2 && indexes.Item2 == 3 && indexes.Item3 == 4)
					{
						_output.WriteLine("here");
					}

					if (multiplicand * multiplier == product)
					{
						var key = new Tuple<int, int>(multiplicand, multiplier);
						if (!distinctCombinations.ContainsKey(key))
							distinctCombinations.Add(key, product);

						//_output.WriteLine("{0} x {1} = {2}", multiplicand, multiplier, product);
						//products.Add(product);
					}
				}

				counter++;
			}

			foreach (KeyValuePair<Tuple<int, int>, int> distinctCombination in distinctCombinations)
			{
				_output.WriteLine("{0} x {1} = {2}", distinctCombination.Key.Item1, distinctCombination.Key.Item2, distinctCombination.Value);
			}

			//_output.WriteLine("Product sum: {0}", products.Sum());
			_output.WriteLine("Product sum: {0}", distinctCombinations.Values.Sum());
		}

		private bool CompareTupleList(Tuple<int, int, int>[] expected, Tuple<int, int, int>[] actual)
		{
			for (int i = 0; i < expected.Length; i++)
			{
				if (!expected[i].Equals(actual[i]))
					return false;
			}

			return true;
		}
	}

	public class PandigitalProducts
	{
		public Tuple<int, int, int>[] GetPandigitalSlicingIndexes(int length)
		{
			var result = new List<Tuple<int, int, int>>();

			// leftIndex <= length - 2 because middle and right index must have at least length of 1.
			for (int leftIndex = 1; leftIndex <= length - 2; leftIndex++)
			{
				for (int middleIndex = 1; middleIndex <= length - leftIndex - 1; middleIndex++)
				{
					for (int rightIndex = length - leftIndex - middleIndex; rightIndex >= 1; rightIndex--)
					{
						var indexes = new Tuple<int, int, int>(leftIndex, middleIndex, rightIndex);
						if (leftIndex + middleIndex + rightIndex == 9)
							result.Add(indexes);
					}
				}
			}

			return result.ToArray();
		}

		public bool IsSpecialPandigital(Tuple<int, int, int> specialPandigitalCandidate)
		{
			int[] pandigitalSequence = {1, 2, 3, 4, 5, 6, 7, 8, 9};

			var candidateString = string.Format("{0}{1}{2}", 
				specialPandigitalCandidate.Item1, specialPandigitalCandidate.Item2, specialPandigitalCandidate.Item3);
			var candidateSequence = candidateString.Select(c => Convert.ToInt32(c.ToString())).ToList();
			candidateSequence.Sort();

			return candidateSequence.SequenceEqual(pandigitalSequence);
		}
	}
}
