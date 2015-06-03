using System.Text.RegularExpressions;
using NUnit.Framework;

namespace RomanNumeralKata.UnitTests
{
	[TestFixture]
	public class RomanToIntTests
	{

		private static readonly object[] RomanNumeralCases =
		{
			new object[] {"", 0},
			new object[] {"I", 1},
			new object[] {"IV", 4},
			new object[] {"IX", 9},
			new object[] {"XIII", 13},
			new object[] {"XXX", 30},
			new object[] {"CXIII", 113},
			new object[] {"X", 10},
			new object[] {"XL", 40},
			new object[] {"MCMLXXXIV", 1984},
		};

		private static readonly object[] HistoricCases =
		{
			new object[] {"IIII", 4},
			new object[] {"MDCCCCX", 1910}
		};

		[Test, TestCaseSource("RomanNumeralCases"), TestCaseSource("HistoricCases")]
		public void CanTranslateRomanToInt(string input, int result)
		{
			int answer = input.FromRomanToInt();
			Assert.AreEqual(result, answer);
		}

		[Test, TestCaseSource("RomanNumeralCases"), TestCaseSource("HistoricCases")]
		public void CanTranslateRomanToIntV2(string input, int result)
		{
			int answer = input.FromRomanToIntV2();
			Assert.AreEqual(result, answer);
		}

		[Test, TestCaseSource("RomanNumeralCases"), TestCaseSource("HistoricCases")]
		public void CanTranslateRomanToIntV3(string input, int result)
		{
			int answer = input.FromRomanToIntV3();
			Assert.AreEqual(result, answer);
		}
	}

	public static class StringExtension
	{
		public static int FromRomanToInt(this string input)
		{
			int value = Ones(ref input);
			value += Tens(ref input);
			value += Hundreds(ref input);
			value += Thousands(ref input);
			return value;

		}

		public static int Ones(ref string input)
		{
			string[] oneStrings = new[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };

			return GetNumberFromArray(ref input, oneStrings, 1);
		}

		private static int GetNumberFromArray(ref string input, string[] numerals, int multiplier)
		{
			if (numerals.Length > 4 && input.EndsWith(numerals[4]))
			{
				input = input.Substring(0, input.Length - numerals[4].Length);
				return 4 * multiplier;
			}

			for (int i = numerals.Length - 1; i >= 0; i--)
			{
				if (input.EndsWith(numerals[i]))
				{
					input = input.Substring(0, input.Length - numerals[i].Length);
					return i * multiplier;
				}
			}
			return 0;
		}

		public static int Tens(ref string input)
		{
			string[] tenStrings = new[] { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
			return GetNumberFromArray(ref input, tenStrings, 10);
			;
		}

		public static int Hundreds(ref string input)
		{
			string[] hundredStrings = new[] { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
			return GetNumberFromArray(ref input, hundredStrings, 100);

		}

		public static int Thousands(ref string input)
		{
			string[] thousandStrings = new[] { "", "M", "MM", "MMM" };
			return GetNumberFromArray(ref input, thousandStrings, 1000);
		}


		public static int FromRomanToIntV2(this string input)
		{
			int value = 0;

			for (var i = input.Length - 1; i >= 0; i--)
			{
				if (input[i] == 'I')
				{
					if (value >= 5) value -= 1;
					else value += 1;
				}
				if (input[i] == 'V')
				{
					value += 5;
				}

				if (input[i] == 'X')
				{
					if (value >= 50) value -= 10;
					else value += 10;
				}
				if (input[i] == 'L')
				{
					value += 50;
				}

				if (input[i] == 'C')
				{
					if (value >= 500) value -= 100;
					else value += 100;
				}
				if (input[i] == 'D')
				{
					value += 500;
				}

				if (input[i] == 'M')
				{
					value += 1000;
				}
			}

			return value;
		}

		public static int FromRomanToIntV3(this string input)
		{
			if (input.Length == 1)
				return input[0].FromRomanToInt();

			var sum = 0;
			var subtractive = new Regex("(IV|IX|XL|XC|CD|CM)");


			if (subtractive.IsMatch(input))
			{
				var matches = subtractive.Matches(input);

				for (int i = 0; i < matches.Count; i++)
				{
					var subtract = matches[i].Value[0].FromRomanToInt();
					var number = matches[i].Value[1].FromRomanToInt();
					sum += (number - subtract);
				}

				input = subtractive.Replace(input, "");
			}

			for (int i = 0; i < input.Length; i++)
			{
				sum += input[i].FromRomanToInt();
			}


			return sum;

		}

		public static int FromRomanToInt(this char input)
		{
			if (input == 'I') return 1;
			if (input == 'V') return 5;
			if (input == 'X') return 10;
			if (input == 'L') return 50;
			if (input == 'C') return 100;
			if (input == 'D') return 500;
			if (input == 'M') return 1000;
			return 0;
		}
	}
}
