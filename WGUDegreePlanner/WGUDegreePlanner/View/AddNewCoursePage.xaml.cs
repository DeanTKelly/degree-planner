using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGUDegreePlanner.Model;
using WGUDegreePlanner.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGUDegreePlanner.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewCoursePage : ContentPage
	{
		public AddNewCoursePage (Term term)
		{
			InitializeComponent ();
            termValue = term;
            BindingContext = viewModel = new ViewModelAddNewCoursePage(Term);
		}
        private Term termValue;
        public Term Term
        {
            get { return termValue; }
            set { termValue = value; OnPropertyChanged(); }
        }
        ViewModelAddNewCoursePage viewModel;        
    }
}