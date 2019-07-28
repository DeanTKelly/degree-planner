using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WGUDegreePlanner.Model;
using WGUDegreePlanner.View;
using WGUDegreePlanner.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGUDegreePlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        
        ViewModelMainPage viewModel = new ViewModelMainPage();
        public MainPage()
        {
            InitializeComponent();
            
            BindingContext = viewModel;

            TermsListView.IsPullToRefreshEnabled = true;
            TermsListView.SetBinding(ListView.IsRefreshingProperty, nameof(viewModel.IsBusy));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.PopulateTermList();
        }
        async void TermsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var term = e.SelectedItem as Term;
            if (term == null)
                return;
            await Navigation.PushAsync(new TermPage(term));
            TermsListView.SelectedItem = null;
        }     
         
            }
}
