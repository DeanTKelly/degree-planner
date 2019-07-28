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
    public class ViewModelAssessmentPage : ViewModelBase
    {
        private Assessment assessmentValue;
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
        public ViewModelAssessmentPage(Assessment assessment)
        {
            assessmentValue = assessment;
            EditButtonCommand = new Command(async () => await ExecuteEditButtonCommand());
            DeleteButtonCommand = new Command(async () => await ExecuteDeleteButtonCommand());
        }
        public Command EditButtonCommand { get; set; }
        async Task ExecuteEditButtonCommand()
        {
            await App.Current.MainPage.Navigation.PushAsync(new EditAssessmentsPage(this));
        }
        public Command DeleteButtonCommand { get; set; }
        async Task ExecuteDeleteButtonCommand()
        {
            await App.DB.DeleteAssessment(Assessment);
            MessagingCenter.Send<ViewModelAssessmentPage, Assessment>(this, "DeleteAssessment", Assessment);
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}