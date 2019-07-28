using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WGUDegreePlanner.Model;
using WGUDegreePlanner.View;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WGUDegreePlanner.ViewModel
{
    public class ViewModelCoursePage : ViewModelBase
    {
        public ViewModelCoursePage(Course course)
        {
            courseValue = course;
            AssessmentList = new ObservableCollection<Assessment>();
            ShareCourseNotesCommand = new Command(async () => await ExecuteShareCourseNotesCommand());
            EditButtonCommand = new Command(async () => await ExecuteEditButtonCommand());
            DeleteButtonCommand = new Command(async () => await ExecuteDeleteButtonCommand());
            ScheduleAssessmentsCommand = new Command(async () => await ExecuteScheduleAssessmentsCommand());
            PopulateAssessmentList();

            MessagingCenter.Subscribe<ViewModelEditCoursePage, Course>(this, "EditCourse", (sender, info) =>
            {
                Course = info;
            });
            MessagingCenter.Subscribe<ViewModelAddAssessmentsPage, Assessment>
               (this, "AddAssessment", (sender, obj) =>
               {
                   AssessmentList.Add(obj);
               });
            MessagingCenter.Subscribe<ViewModelAssessmentPage, Assessment>
                (this, "DeleteAssessment", (sender, obj) =>
                {
                    AssessmentList.Remove(obj);
                });
            MessagingCenter.Subscribe<ViewModelEditAssessmentsPage>(this, "EditAssessment", async (obj) =>
            {
                await ExecuteLoadAssessmentsCommand();
            });

        }
        public Command ShareCourseNotesCommand { get; set; }
        async Task ExecuteShareCourseNotesCommand()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = CourseNotes,
                Title = $"{CourseName} Course Notes:"
            });

        }
        private ObservableCollection<Assessment> assessments;
        public ObservableCollection<Assessment> AssessmentList
        {
            set
            {
                if (assessments != value)
                {
                    assessments = value;
                    OnPropertyChanged("Assessment");
                }
            }
            get
            {
                return assessments;
            }
        }
        public Command LoadAssessmentsCommand { get; set; }
        async Task ExecuteLoadAssessmentsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                AssessmentList.Clear();
                var assessments = await App.DB.ShowAssessments(Course);
                foreach (var assessment in assessments)
                {
                    AssessmentList.Add(assessment);
                }
            }
            catch (Exception exception) { }
            finally { IsBusy = false; }
        }
        private async void PopulateAssessmentList()
        {
            List<Assessment> assessments = await App.DB.ShowAssessments(courseValue);
            AssessmentList.Clear();
            foreach (Assessment assessment in assessments)
            {
                AssessmentList.Add(assessment);
            }


        }
        public Command EditButtonCommand { get; set; }
        async Task ExecuteEditButtonCommand()
        {
            await App.Current.MainPage.Navigation.PushAsync(new EditCoursePage(this));
        }
        public Command DeleteButtonCommand { get; set; }
        async Task ExecuteDeleteButtonCommand()
        {
            await App.DB.DeleteCourse(Course);
            MessagingCenter.Send<ViewModelCoursePage, Course>(this, "DeleteCourse", Course);
            await App.Current.MainPage.Navigation.PopAsync();
        }
        public Command ScheduleAssessmentsCommand { get; set; }
        async Task ExecuteScheduleAssessmentsCommand()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddAssessmentsPage(Course));

        }
        private Course courseValue;
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
