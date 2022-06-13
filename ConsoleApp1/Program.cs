﻿using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			
			
			Console.WriteLine("Berechnung Kredikosten");

			CreditCalculator calculator = new CreditCalculator();
			calculator.Credit = 130687m;
			calculator.Ratio = 850m;
			calculator.InterestRate = 1.73m;
			calculator.PayDayMonthly = 1; 

			List<string> calulated = calculator.Calculate();

			foreach (var item in calulated)
			{
				Console.WriteLine(item.ToString()); 
			}
			Console.WriteLine(); 
			Console.WriteLine($"Zinsen insgesamt: {string.Format("{0:f}", calculator.TaxSum)}"); 

			Console.ReadLine(); 
		}


	}
}