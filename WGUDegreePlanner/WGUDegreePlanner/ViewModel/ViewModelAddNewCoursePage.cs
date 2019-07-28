using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WGUDegreePlanner.Model;
using WGUDegreePlanner.View;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WGUDegreePlanner.ViewModel
{
    public class ViewModelAddNewCoursePage : ViewModelBase
    {
        public ViewModelAddNewCoursePage(Term term)
        {
            courseValue = new Course();
            TermID = term.TermID;
            CourseStartDate = DateTime.Now.ToShortDateString();
            CourseEndDate = DateTime.Now.AddMonths(1).ToShortDateString();
            SaveNewCourseButtonCommand = new Command(async () => await ExecuteSaveNewCourseButtonCommand());
            CancelNewCourseButtonCommand = new Command(async () => await App.Current.MainPage.Navigation.PopAsync());
        }
        public Command SaveNewCourseButtonCommand { get; set; }
        async Task ExecuteSaveNewCourseButtonCommand()
        {
            if(preventNullValues(Course))
            {
                await App.DB.SaveCourse(Course);
                SetNotify(CourseNotifications, "Reminder", $"{CourseName} begins on {CourseStartDate}", 1, DateTime.Parse(CourseStartDate).AddDays(-7));
                SetNotify(CourseNotifications, "Reminder", $"{CourseName} ends  on {CourseEndDate}", 2, DateTime.Parse(CourseEndDate).AddDays(-7));
                await App.Current.MainPage.Navigation.PopAsync();
                MessagingCenter.Send<ViewModelAddNewCoursePage, Course>(this, "AddNewCourse", Course);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(" ", "Please complete all fields.", "Continue");
                return;
            }
        }
        public Command CancelNewCourseButtonCommand { get; set; }
        
        public bool preventNullValues(Course course)
        {
            return course.CourseName != null && course.CourseStatus != null
                && course.InstructorName != null && course.InstructorPhone != null 
                && course.InstructorEmail != null && course.CourseNotes != null;
        }
        private Course courseValue;
        public Course Course
        {
            set
            {
                if(courseValue != value)
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
        public string CourseName
        {
            set
            {
                if(courseValue.CourseName != value)
                {
                    courseValue.CourseName = value;
                    OnPropertyChanged("CourseName");
                }
            }
            get
            {
                return courseValue.CourseName;
            }
        }
        public string CourseStartDate
        {
            set
            {
                if (courseValue.CourseStart != DateTime.Parse(value))
                {
                    courseValue.CourseStart = DateTime.Parse(value);
                    OnPropertyChanged("CourseStartDate");
                }
            }
            get
            {
                return courseValue.CourseStart.ToShortDateString();
            }
        }
        public string CourseEndDate
        {
            set
            {
                if (courseValue.CourseEnd != DateTime.Parse(value))
                {
                    courseValue.CourseEnd = DateTime.Parse(value);
                    OnPropertyChanged("CourseEndDate");
                }
            }
            get
            {
                return courseValue.CourseEnd.ToShortDateString();
            }
        }
        public string CourseStatus
        {
            set
            {
                if (courseValue.CourseStatus != value)
                {
                    courseValue.CourseStatus = value;
                    OnPropertyChanged("CourseStatus");
                }
            }
            get
            {
                return courseValue.CourseStatus;
            }
        }
        public List<string> CourseStatusList { get; } = new List<string> { "Upcoming", "Current", "Completed" };
        public string InstructorName
        {
            set
            {
                if (courseValue.InstructorName != value)
                {
                    courseValue.InstructorName = value;
                    OnPropertyChanged("InstructorName");
                }
            }
            get
            {
                return courseValue.InstructorName;
            }
        }
        public string InstructorPhone
        {
            set
            {
                if (courseValue.InstructorPhone != value)
                {
                    courseValue.InstructorPhone = value;
                    OnPropertyChanged("InstructorPhone");
                }
            }
            get
            {
                return courseValue.InstructorPhone;
            }
        }
        public string InstructorEmail
        {
            set
            {
                if (courseValue.InstructorEmail != value)
                {
                    courseValue.InstructorEmail = value;
                    OnPropertyChanged("InstructorEmail");
                }
            }
            get
            {
                return courseValue.InstructorEmail;
            }
        }
        public bool CourseNotifications
        {
            set
            {
                if (courseValue.CourseNotifications != value)
                {
                    courseValue.CourseNotifications = value;
                    OnPropertyChanged("CourseNotifications");
                }
            }
            get
            {
                return courseValue.CourseNotifications;
            }
        }
        public string CourseNotes
        {
            set
            {
                if (courseValue.CourseNotes != value)
                {
                    courseValue.CourseNotes = value;
                    OnPropertyChanged("CourseNotes");
                }
            }
            get
            {
                return courseValue.CourseNotes;
            }
        }
        public int TermID
        {
            set
            {
                if (courseValue.TermID != value)
                {
                    courseValue.TermID = value;
                    OnPropertyChanged("TermID");
                }
            }
            get
            {
                return courseValue.TermID;
            }
        }        
    }
}
