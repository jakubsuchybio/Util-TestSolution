using System;
using FSharpLibraryTutorial;

namespace CSharpUsingFSharpLibrary
{
	internal static class Program
	{
		private static void Main(string[] args) {
			var tickers = new[] { "msft", "orcl", "ebay" };
			var analyzers = MathLibrary.StockAnalyzer.GetAnalyzers( tickers, 365 );

			foreach( var a in analyzers ) {
				Console.WriteLine( $"Ret:{a.Return}\tStdDev:{a.StdDev}" );
			}
		}
	}
}