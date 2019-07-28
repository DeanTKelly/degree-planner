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
	public partial class EditCoursePage : ContentPage
	{
		public EditCoursePage (ViewModelCoursePage viewModel)
		{
			InitializeComponent ();
            Course = viewModel.Course;
            BindingContext = vm = new ViewModelEditCoursePage(Course);
		}
        public Course Course { get; set; }
        ViewModelEditCoursePage vm;
	}
}