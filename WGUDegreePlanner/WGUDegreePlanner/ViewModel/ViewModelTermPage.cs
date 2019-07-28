using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WGUDegreePlanner.Model;
using WGUDegreePlanner.View;
using Xamarin.Forms;

namespace WGUDegreePlanner.ViewModel
{
    public class ViewModelTermPage : ViewModelBase
    {
        public Term Term
        {
            set
            {
                if (termValue != value)
                {
                    termValue = value;
                    OnPropertyChanged();
                }
            }
            get
            {
                return termValue;
            }
        }
        private ObservableCollection<Term> terms;
        public ViewModelTermPage(Term term)
        {
            termValue = term;
            CourseList = new ObservableCollection<Course>();
            terms = new ObservableCollection<Term>();
            LoadCoursesCommand = new Command(async () => await ExecuteLoadCourses());
            AddCourseCommand = new Command(async () => await ExecuteAddCourse());
            BackButtonCommand = new Command(async () => await App.Current.MainPage.Navigation.PopAsync());
            DeleteTermCommand = new Command(async () => await ExecuteDeleteTerm());
            EditTermCommand = new Command(async () => await ExecuteEditTerm());
            ShowCourses();
            MessagingCenter.Subscribe<ViewModelMainPage, Course>(this, "AddNewCourse", (sender, obj) =>
            {
                CourseList.Add(obj);
            });
            MessagingCenter.Subscribe<ViewModelAddNewCoursePage, Course>(this, "AddNewCourse", (sender, obj) =>
            {
                CourseList.Add(obj);
                ButtonEnabled = CourseList.Count >= 6 ? false : true;
            });
            MessagingCenter.Subscribe<ViewModelEditTermPage, Term>(this, "EditTerm", (sender, obj) =>
            {
                Term = obj;
            });
            MessagingCenter.Subscribe<ViewModelCoursePage, Course>(this, "DeleteCourse", (sender, obj) =>
            {
                CourseList.Remove(obj);
                ButtonEnabled = CourseList.Count >= 6 ? false : true;
            });
        }
        public Command BackButtonCommand { get; set; }
        private Term termValue;

        
        public ObservableCollection<Course> CourseList { get; set; }
        
        private bool buttonEnabledValue = true;
        public bool ButtonEnabled
        {
            set
            {
                if(buttonEnabledValue != value)
                {
                    buttonEnabledValue = value;
                    OnPropertyChanged("ButtonEnabled");
                }
            }
            get
            {
                return buttonEnabledValue;
            }
        }

        private bool courseLabelValue = false;
        public bool CourseLabel
        {
            set
            {
                if(courseLabelValue != value)
                {
                    courseLabelValue = value;
                    OnPropertyChanged();
                }
            }
            get
            {
                return courseLabelValue;
            }
        }

        public Command AddCourseCommand { get; set; }
        async Task ExecuteAddCourse()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddNewCoursePage(Term));
        }
        
        public Command DeleteTermCommand { get; set; }
        async Task ExecuteDeleteTerm()
        {
            await App.DB.DeleteTerm(termValue);
            MessagingCenter.Send<ViewModelTermPage, Term>(this, "DeleteTerm", termValue);
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

        public Command EditTermCommand { get; set; }
        async Task ExecuteEditTerm()
        {
            await App.Current.MainPage.Navigation.PushAsync(new EditTermPage(Term));
        }

        public Command LoadCoursesCommand { get; set; }
        async Task ExecuteLoadCourses()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                CourseList.Clear();
                var courses = await App.DB.ShowCourses(Term);
                foreach(var course in courses)
                {
                    CourseList.Add(course);
                }
            }
            catch(Exception exception) { }
            finally { IsBusy = false; }
        }
        public async void ShowCourses()
        {
            List<Course> courses = await App.DB.ShowCourses(termValue);
            foreach(Course course in courses)
            {
                CourseList.Add(course);
            }
            if(CourseList.Count > 0)
            {
                CourseLabel = true;
            }
            ButtonEnabled = CourseList.Count >= 6 ? false : true;
        }
        public string TermName
        {
            set
            {
                if (termValue.TermName != value)
                {
                    termValue.TermName = value;
                    OnPropertyChanged("TermName");
                }
            }
            get
            {
                return termValue.TermName;
            }
        }
        public string TermStartDate
        {
            set
            {
                if (termValue.TermStart != DateTime.Parse(value))
                {
                    termValue.TermStart = DateTime.Parse(value);
                    OnPropertyChanged("TermStart");
                }
            }
            get
            {
                return termValue.TermStart.ToShortDateString();
            }
        }
        public string TermEndDate
        {
            set
            {
                if (termValue.TermEnd != DateTime.Parse(value))
                {
                    termValue.TermEnd = DateTime.Parse(value);
                    OnPropertyChanged("TermEnd");
                }
            }
            get
            {
                return termValue.TermEnd.ToShortDateString();
            }
        }
    }
}
