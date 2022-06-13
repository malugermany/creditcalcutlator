using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit; 

namespace CalculatorLibraryTests
{
	public class CalculatorTestClass
	{
		public static IEnumerable<object[]> GetTestData()
		{
			yield return new object[]
			{
				new DateOnly(2020,1,1), 3, new DateOnly(2020,1,3)
			};
		}


		[Theory]
		[MemberData(nameof(CalculatorTestClass.GetTestData))]
		public void CalculateFirstPayDay_ShouldReturnCorrectDateOnly(DateOnly pStartDate
			, int pPayDayMonthly
			, DateOnly pExpectedResult)
		{
			//Arage
			CreditCalculator creditCalculator = new CreditCalculator();

			//Act 
			DateOnly result = creditCalculator.CalculateFirstPayDay(pStartDate, pPayDayMonthly);

			//Assert
			Assert.Equal<DateOnly>(pExpectedResult, result); 
		}

		[Fact]
		public void Test()
		{
			Assert.True(true); 
		}
	}
}
