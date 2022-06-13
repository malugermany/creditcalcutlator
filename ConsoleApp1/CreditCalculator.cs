using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
	//---------------------------------------------------------------
	/// <summary>
	/// Klasse zur Berechnung eines Kredits 
	/// </summary>
	public class CreditCalculator
	{
		//---------------------------------------------------------------
		/// <summary>
		/// Zinssatz in Prozent
		/// </summary>
		public decimal InterestRate { get; set; } = decimal.Zero;

		//---------------------------------------------------------------
		/// <summary>
		/// Aufgenommener Kredit
		/// </summary>
		public decimal Credit { get; set; } = decimal.Zero;


		//---------------------------------------------------------------
		/// <summary>
		/// Rate des Kredits 
		/// </summary>
		public decimal Ratio { get; set; } = decimal.Zero;

		//---------------------------------------------------------------
		/// <summary>
		/// Gesamt aufgelaufene Zinsen 
		/// </summary>
		public decimal TaxSum { get; private set; } = decimal.Zero;

		//---------------------------------------------------------------
		/// <summary>
		/// Startzeitpunkt der Kreditname, Defaul ist die aktuelle Zeit 
		/// </summary>
		public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

		//---------------------------------------------------------------
		/// <summary>
		/// Monatlicher Tag, an dem das Geld eingezogen wird 
		/// </summary>
		public int PayDayMonthly { get; set; } = 1; 



		//---------------------------------------------------------------
		/// <summary>
		/// Berechnung des Kredits 
		/// </summary>
		/// <returns></returns>
		public List<string> Calculate()
		{
			List<string> ret = new List<string>();

			ret.Add(string.Format("{0,6}{1,15}{2,15}{3,15}{4,15}{5,15}\n", "Rate","Datum","Tage", "Kredit", "Zinsen","Tilgung"));

			ret.Add(string.Format("{0,6}{1,15}{2,15}{3,15:f}", 0, StartDate.ToShortDateString(),"", Credit)); 
			//Kredithöhe an einem Zeitpunkt
			decimal creditstage = Credit;

			int i = 0;

			DateOnly fromDate = StartDate;
			DateOnly toDate = CalculateFirstPayDay(this.StartDate, this.PayDayMonthly); 

			while (creditstage > Ratio)
			{
				if(i > 0)
				{
					toDate = toDate.AddMonths(1);
				}

				//Zinsen die fällig werden
				decimal tax = creditstage * (InterestRate / 100m) * GetMontlyFactor(fromDate, toDate);

				int days = toDate.DayNumber - fromDate.DayNumber;

				TaxSum = TaxSum + tax; 

				//Tilgung
				decimal repayment = Ratio - tax;

				creditstage = creditstage - repayment;

				i++;

				//ret.Add($"Rate: {string.Format("{0,5}", i)} "
				ret.Add($"{string.Format("{0,6}", i)}"
					+ $"{string.Format("{0,15}", toDate.ToShortDateString())}"
					+ $"{string.Format("{0,15}", days)}"
					+ $"{string.Format("{0,15:f}",creditstage)}"
					+ $"{string.Format("{0,15:f}", tax)}"
					+ $"{string.Format("{0,15:f}", repayment)}"
				
					);

				fromDate = toDate;
			}

			return ret;
		}

		//---------------------------------------------------------------
		/// <summary>
		/// Ersten Zahltag berechnen
		/// </summary>
		/// <param name="pStartDate">StartDate von dem begonnen werden soll</param>
		/// <param name="pPayDayMonthly">monatlicher Zahltag</param>
		/// <returns></returns>
		public DateOnly CalculateFirstPayDay(DateOnly pStartDate, int pPayDayMonthly)
		{
			int day = pPayDayMonthly;
			int month = 0;
			int year = 0; 
			
			
			//Berechnung des Monats
			if(pStartDate.Day < pPayDayMonthly)
			{
				month = pStartDate.Month; 
			}
			else if (pStartDate.Day == pPayDayMonthly)
			{
				month = pStartDate.Month; 
			}
			else
			{
				month = pStartDate.AddMonths(1).Month; 
			}

			//Berechnung des Jahres
			if(month == 1)
			{
				year = pStartDate.AddYears(1).Year; 
			}
			else
			{
				year = pStartDate.Year; 
			}


			//folgendes Muster muss gefüllt werden 
			return new DateOnly(year, month, day); 
		}

		//---------------------------------------------------------------
		/// <summary>
		/// Monatlichen Quotienten ermitteln, Anhand des Tags 
		/// </summary>
		/// <param name="pFromDate">Zeitraum der Berechnung von</param>
		/// <param name="pToDate">Zeitraum der Berechnung bis</param>
		/// <returns>monatlicher Quotient</returns>
		private decimal GetMontlyFactor(DateOnly pFromDate, DateOnly pToDate)
		{
			int days = pToDate.DayNumber - pFromDate.DayNumber; 

			decimal ret =  days / 365m; 
			
			return ret;	
		}

		
	}
}
