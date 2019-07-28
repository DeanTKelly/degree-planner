using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WGUDegreePlanner.Model;
using Xamarin.Forms;

namespace WGUDegreePlanner.ViewModel
{
    public class ViewModelEditTermPage : ViewModelBase
    {
        private Term termValue;
        public Term Term
        {         
            set
            {
                if(termValue != value)
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
        public ViewModelEditTermPage(Term term)
        {            
            termValue = term;
            SaveButtonCommand = new Command(async () => await ExecuteSaveButtonCommand());
            CancelButtonCommand = new Command(async () => await ExecuteCancelButtonCommand());
        }
        public Command SaveButtonCommand { get; set; }
        async Task ExecuteSaveButtonCommand()
        {
            if (preventNullValues(Term))
            {           
                await App.DB.SaveTerm(Term);
                MessagingCenter.Send<ViewModelEditTermPage, Term>(this, "EditTerm", Term);
                await App.Current.MainPage.Navigation.PopToRootAsync();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(" ", "Please complete all fields.", "Continue");
                return;
            }
        }
        public bool preventNullValues(Term term)
        {
            return term.TermName != "";
        }
        public Command CancelButtonCommand { get; set; }
        async Task ExecuteCancelButtonCommand()
        {
            await App.Current.MainPage.Navigation.PopAsync();
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
