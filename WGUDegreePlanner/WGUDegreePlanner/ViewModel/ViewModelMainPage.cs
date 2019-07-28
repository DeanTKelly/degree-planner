using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WGUDegreePlanner.Model;
using WGUDegreePlanner.View;
using Xamarin.Forms;

namespace WGUDegreePlanner.ViewModel
{
    public class ViewModelMainPage : ViewModelBase
    {
        
        private ObservableCollection<Term> terms;
        public ObservableCollection<Term> Terms
        {
            set
            {
                if(terms != value)
                {
                    terms = value;
                    OnPropertyChanged("Term");
                }
            }
            get
            {
                return terms;
            }
        }    
                
        public Command LoadTermsCommand { get; set; }
        public Command AddNewTermCommand { get; set; } 
        public ViewModelMainPage()
        {
            Title = "WGU Degree Planner";
            terms = new ObservableCollection<Term>();
            LoadTermsCommand = new Command(async () => await ExecuteLoadTermsCommand());
            AddNewTermCommand = new Command(async () => await ExecuteAddNewTermCommand());
            if(App.DB.ShowTerms().Result.Count == 0)
            {
                App.DB.EvaluationData();
            }
            PopulateTermList();

            MessagingCenter.Subscribe<ViewModelAddNewTermPage, Term>
                (this, "AddNewTerm", (sender, obj) =>
                {
                    terms.Add(obj);
                });
            MessagingCenter.Subscribe<ViewModelTermPage, Term>
                (this, "DeleteTerm", (sender, obj) =>
                {
                    terms.Remove(obj);
                });
            MessagingCenter.Subscribe<ViewModelEditTermPage>(this, "EditTerm", async (obj) =>
            {
                await ExecuteLoadTermsCommand();
            });
        }

        
        async Task ExecuteAddNewTermCommand()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddNewTermPage());
        }
        async Task ExecuteLoadTermsCommand()
        {
                Terms.Clear();
                var terms = await App.DB.ShowTerms();
                foreach (var term in terms)
                {
                    Terms.Add(term);
                }          
            
        }
        public async void PopulateTermList()
        {            
            List<Term> terms = await App.DB.ShowTerms();
            Terms.Clear();
            foreach(Term term in terms)
            {
                Terms.Add(term);
            }
        }        
    }
}
