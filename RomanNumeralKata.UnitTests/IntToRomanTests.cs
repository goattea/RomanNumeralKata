using System.IO;
using NUnit.Framework;

namespace RomanNumeralKata.UnitTests
{
	[TestFixture]
	public class IntToRomanTests
	{


		[Test]
		[TestCase(1, "I")]
		[TestCase(3, "III")]
		[TestCase(4, "IV")]
		[TestCase(5, "V")]
		[TestCase(6, "VI")]
		[TestCase(10, "X")]
		[TestCase(1984,"MCMLXXXIV")]
		[TestCase(2984, "MMCMLXXXIV")]
		public void ConvertInputToRoman(int input, string conversion)
		{
			Assert.AreEqual(conversion, ConvertNumberToRoman(input));
		}


		[Test, Category("Unit"), ExpectedException(typeof(InvalidDataException))]
		public void NumberAbove300ThrowsException()
		{
			ConvertNumberToRoman(3001);
			Assert.IsTrue(false);
		}

		private string ConvertNumberToRoman(int num)
		{
			if(num > 3000)
				throw new InvalidDataException(string.Format("Cannot convert {0} to roman", num));

			return ConvertThousandsToRoman(num)
			       + ConvertHundredsToRoman(num)
			       + ConvertTensToRoman(num)
			       + ConvertOnesToRoman(num);
		}

		private string ConvertThousandsToRoman(int num)
		{
			int thousands = num/1000;

			var thousandStrings = new[] { "", "M", "MM", "MMM" };
			return thousandStrings[thousands];
		}

		private string ConvertHundredsToRoman(int num)
		{
			int hundreds = num % 1000 / 100;
			var hundredStrings = new[] { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
			return hundredStrings[hundreds];
		}

		private string ConvertTensToRoman(int num)
		{
			var tens = num%100/10;
			var tenStrings = new [] { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
			return tenStrings[tens];
		}

		private string ConvertOnesToRoman(int num)
		{
			var ones = num%10;
			var oneStrings = new [] {"", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"};
			return oneStrings[ones];
		}
	}
}
