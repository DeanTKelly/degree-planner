using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WGUDegreePlanner.Model;
using Xamarin.Forms;

namespace WGUDegreePlanner.ViewModel
{
    public class ViewModelEditAssessmentsPage : ViewModelBase
    {
        private Assessment assessmentValue;
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
        public Assessment Assessment
        {
            set
            {
                if (assessmentValue != value)
                {
                    assessmentValue = value;
                    OnPropertyChanged("Assessment");
                }
            }
            get
            {
                return assessmentValue;
            }
        }
        public string AssessmentName
        {
            set
            {
                if (assessmentValue.AssessmentName != value)
                {
                    assessmentValue.AssessmentName = value;
                    OnPropertyChanged("AssessmentName");
                }
            }
            get
            {
                return assessmentValue.AssessmentName;
            }
        }
        public string AssessmentType
        {
            set
            {
                if (assessmentValue.AssessmentType != value)
                {
                    assessmentValue.AssessmentType = value;
                    OnPropertyChanged("AssessmentType");
                }
            }
            get
            {
                return assessmentValue.AssessmentType;
            }
        }
        public List<string> AssessmentTypeList { get; } = new List<string> { "Objective Assessment", "Performance Assessment" };
        public string AssessmentStartDate
        {
            set
            {
                if (assessmentValue.AssessmentStart != DateTime.Parse(value))
                {
                    assessmentValue.AssessmentStart = DateTime.Parse(value);
                    OnPropertyChanged("AssessmentStart");
                }
            }
            get
            {
                return assessmentValue.AssessmentStart.ToShortDateString();
            }
        }
        public string AssessmentEndDate
        {
            set
            {
                if (assessmentValue.AssessmentEnd != DateTime.Parse(value))
                {
                    assessmentValue.AssessmentEnd = DateTime.Parse(value);
                    OnPropertyChanged("AssessmentEnd");
                }
            }
            get
            {
                return assessmentValue.AssessmentEnd.ToShortDateString();
            }
        }
        public bool AssessmentNotifications
        {
            set
            {
                if (assessmentValue.AssessmentNotifications != value)
                {
                    assessmentValue.AssessmentNotifications = value;
                    OnPropertyChanged("AssessmentNotifications");
                }
            }
            get
            {
                return assessmentValue.AssessmentNotifications;
            }
        }

        public int CourseID
        {
            set
            {
                if (assessmentValue.CourseID != value)
                {
                    assessmentValue.CourseID = value;
                    OnPropertyChanged("CourseID");
                }
            }
            get
            {
                return assessmentValue.CourseID;
            }
        }
        public ViewModelEditAssessmentsPage (Assessment assessment)
        {
            assessmentValue = assessment;
            SaveButtonCommand = new Command(async () => await ExecuteSaveButtonCommand());
            CancelButtonCommand = new Command(async () => await App.Current.MainPage.Navigation.PopAsync());
        }
        public bool preventNullValues(Assessment assessment)
        {
            return assessment.AssessmentName != "" && assessment.AssessmentType != "";
        }
        public Command SaveButtonCommand { get; set; }
        async Task ExecuteSaveButtonCommand()
        {
            if (preventNullValues(Assessment))
            {
                await App.DB.SaveAssessment(Assessment);
                SetNotify(AssessmentNotifications, "Reminder", $"{AssessmentName} preparations begin on {AssessmentStartDate}",
                        3, DateTime.Parse(AssessmentStartDate).AddDays(-7));
                SetNotify(AssessmentNotifications, "Reminder", $"{AssessmentName} is due on {AssessmentEndDate}",
                    4, DateTime.Parse(AssessmentEndDate).AddDays(-7));
                await App.Current.MainPage.Navigation.PopToRootAsync();
                MessagingCenter.Send<ViewModelEditAssessmentsPage, Assessment>(this, "EditAssessment", Assessment);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(" ", "Please complete all fields.", "Continue");
                return;
            }
        }
        public Command CancelButtonCommand { get; set; }        
    }
}
