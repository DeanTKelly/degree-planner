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
	public partial class AddAssessmentsPage : ContentPage
	{
        private Course courseValue;
        ViewModelAddAssessmentsPage viewModel;
		public AddAssessmentsPage (Course course)
		{
			InitializeComponent ();
            courseValue = course;
            BindingContext = viewModel = new ViewModelAddAssessmentsPage(Course);
		}
        public Course Course
        {
            set
            {
                if (courseValue != value)
                {
                    courseValue = value;
                    OnPropertyChanged("Course");
                }
            }
            get
            {
                return courseValue;
            }

        }
    }
}