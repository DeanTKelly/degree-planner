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
	public partial class TermPage : ContentPage
	{
		public TermPage (Term term)
		{
			InitializeComponent ();
            termValue = term;
            ViewModelTermPage viewModel = new ViewModelTermPage(term);
            BindingContext = viewModel;

            CourseListView.IsPullToRefreshEnabled = true;
            CourseListView.SetBinding(ListView.IsRefreshingProperty, nameof(viewModel.IsBusy));
		}
        private Term termValue;
        public Term Term
        {
            get { return termValue; }
            set { termValue = value; OnPropertyChanged(); }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            termValue = Term;
            ViewModelTermPage viewModel = new ViewModelTermPage(termValue);
            viewModel.ShowCourses();
        }

        async void CourseListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var course = e.SelectedItem as Course;
            if (course == null)
                return;
            await Navigation.PushAsync(new CoursePage(course));

            CourseListView.SelectedItem = null;
        }
    }
}