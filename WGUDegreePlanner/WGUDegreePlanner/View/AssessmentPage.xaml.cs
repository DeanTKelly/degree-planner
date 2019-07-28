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
	public partial class AssessmentPage : ContentPage
	{
		public AssessmentPage (Assessment assessment)
		{
			InitializeComponent ();
            assessmentValue = assessment;
            BindingContext = viewModel = new ViewModelAssessmentPage(assessment);
		}

        private Assessment assessmentValue;
        public Assessment Assessment
        {
            set
            {
                if(assessmentValue != value)
                {
                    assessmentValue = value;
                    OnPropertyChanged();
                }
            }
            get
            {
                return assessmentValue;
            }
            
        }
        ViewModelAssessmentPage viewModel;

        
    }
}