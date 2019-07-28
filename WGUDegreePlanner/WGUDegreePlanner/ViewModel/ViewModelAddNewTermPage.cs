using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WGUDegreePlanner.Model;
using Xamarin.Forms;

namespace WGUDegreePlanner.ViewModel
{
    public class ViewModelAddNewTermPage : ViewModelBase
    {
        public ViewModelAddNewTermPage()
        {
            termValue = new Term();
            TermStartDate = DateTime.Now.ToShortDateString();
            TermEndDate = DateTime.Now.AddMonths(6).ToShortDateString();
            SaveNewTermCommand = new Command(async () => await ExecuteSaveNewTermCommand());
            CancelButtonCommand = new Command(async () => await ExecuteCancelButtonCommand());
        }        
        private Term termValue;
        public Term Term
        {
            set
            {
                if (termValue != value)
                {
                    termValue = value;
                    OnPropertyChanged("Term");
                }
            }
            get
            {
                return termValue;
            }
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
        public Command SaveNewTermCommand { get; set; }
        async Task ExecuteSaveNewTermCommand()
        {
            if (preventNullValues(Term))
            {
                await App.DB.SaveTerm(Term);
            MessagingCenter.Send<ViewModelAddNewTermPage, Term>(this, "AddNewTerm", Term);
            await App.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(" ", "Please complete all fields.", "Continue");
                return;
            }
        }
        public bool preventNullValues(Term term)
        {
            return term.TermName != null; 
        }
        public Command CancelButtonCommand { get; set; }
        async Task ExecuteCancelButtonCommand()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
