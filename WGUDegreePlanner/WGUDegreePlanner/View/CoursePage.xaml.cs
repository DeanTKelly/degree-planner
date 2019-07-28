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
	public partial class CoursePage : ContentPage
	{
        public CoursePage (Course course)
		{
			InitializeComponent ();
            Course = course;
            BindingContext = viewModel = new ViewModelCoursePage(course);
        }
        ViewModelCoursePage viewModel;
        private Course courseValue;
        public Course Course
        {
            get { return courseValue; }
            set { courseValue = value; OnPropertyChanged(); }
        }

        async void AssessmentListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedAssessment = e.SelectedItem as Assessment;
            if (selectedAssessment == null)
                return;
            await Navigation.PushAsync(new AssessmentPage(selectedAssessment));

            AssessmentListView.SelectedItem = null;
        }
    }
}