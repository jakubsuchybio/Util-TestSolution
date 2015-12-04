using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace TestProject
{
	class Program
	{
		static void Main(string[] args) {
			var pythonKey = Registry.ClassesRoot.OpenSubKey( @"Python.File\shell\open\command" );
		}
	}
}
