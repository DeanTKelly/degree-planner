using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WGUDegreePlanner.Model;
using WGUDegreePlanner.View;
using Xamarin.Forms;

namespace WGUDegreePlanner.ViewModel
{
    public class ViewModelEditCoursePage : ViewModelBase
    {
        public ViewModelEditCoursePage(Course course)
        {
            courseValue = course;
            SaveButtonCommand = new Command(async () => await ExecuteSaveButtonCommand());
            CancelButtonCommand = new Command(async () => await ExecuteCancelButtonCommand());            
        }
        private Course courseValue;
        public bool preventNullValues(Course course)
        {
            return course.CourseName != "" && course.CourseStatus != ""
                && course.InstructorName != "" && course.InstructorPhone != ""
                && course.InstructorEmail != "" && course.CourseNotes != "";
        }
        public Command SaveButtonCommand { get; set; }
        async Task ExecuteSaveButtonCommand()
        {
            if (preventNullValues(Course))
            {
                await App.DB.SaveCourse(Course);
                await App.Current.MainPage.Navigation.PopToRootAsync();
                SetNotify(CourseNotifications, "Reminder", $"{CourseName} begins on {CourseStartDate}", 1, DateTime.Parse(CourseStartDate).AddDays(-7));
                SetNotify(CourseNotifications, "Reminder", $"{CourseName} ends  on {CourseEndDate}", 2, DateTime.Parse(CourseEndDate).AddDays(-7));
                MessagingCenter.Send<ViewModelEditCoursePage, Course>(this, "EditCourse", Course);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(" ", "Please complete all fields.", "Continue");
                return;
            }
        }        
        public Command CancelButtonCommand { get; set; }
        async Task ExecuteCancelButtonCommand()
        {
            await App.Current.MainPage.Navigation.PopAsync();
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
        public string CourseName
        {
            set
            {
                if (courseValue.CourseName != value)
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
                    OnPropertyChanged("CourseStart");
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
                    OnPropertyChanged("CourseEnd");
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
    }
}
