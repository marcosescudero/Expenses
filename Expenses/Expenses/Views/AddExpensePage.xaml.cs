﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Expenses.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddExpensePage : ContentPage
	{
		public AddExpensePage ()
		{
			InitializeComponent ();
		}
	}
}