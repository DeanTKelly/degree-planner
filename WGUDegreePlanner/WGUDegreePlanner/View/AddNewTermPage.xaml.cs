using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGUDegreePlanner.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGUDegreePlanner.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewTermPage : ContentPage
	{
		public AddNewTermPage ()
		{
			InitializeComponent ();
            ViewModelAddNewTermPage viewModel;
            BindingContext = viewModel = new ViewModelAddNewTermPage(); 
		}
	}
}