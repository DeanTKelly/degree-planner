using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGUDegreePlanner.Model;
using WGUDegreePlanner.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGUDegreePlanner.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditAssessmentsPage : ContentPage
	{
		public EditAssessmentsPage (ViewModelAssessmentPage viewModel)
		{
			InitializeComponent ();
            Assessment = viewModel.Assessment;
            BindingContext = vm = new ViewModelEditAssessmentsPage(Assessment);
		}
        public Assessment Assessment { get; set; }
        ViewModelEditAssessmentsPage vm;
	}
}