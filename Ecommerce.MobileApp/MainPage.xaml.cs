using Ecommerce.MobileApp.DataServices;
using System.Diagnostics;

namespace Ecommerce.MobileApp
{
    public partial class MainPage : ContentPage
    {

        private readonly IRestDataService _service;
        public MainPage(IRestDataService service)
        {
            _service = service;
            InitializeComponent();
        }

        protected  async Task onAppearingAsync()
        {
            base.OnAppearing();
            //collectionview.ItemSource = await _service.GetAllTodosAsync();
        }

        async void onAddToDoClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("--- Add Button Clicked ----");
        }

        async void OnSelectionChanged(object sender , SelectedItemChangedEventArgs e)
        {
            Debug.WriteLine("==>");
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}