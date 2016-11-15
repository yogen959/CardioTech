using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using CardioTech.Models;
using CardioTech.ViewModels;
using System.Threading.Tasks;

namespace CardioTech.Views
{
    class ProductList : ContentPage
    {
        private int paginationLimit = 1;

        private int limitProduct = 4;

        private int limitAutocomplete = 3;

        private ActivityIndicator _activityIndicator = new ActivityIndicator();

        private Label resultsLabel = new Label
        {
            Text = "Resultados apareceran aquí",
            VerticalOptions = LayoutOptions.FillAndExpand,
            FontSize = 18
        };

        private SearchBar searchBar = new SearchBar
        {
            Placeholder = "Buscar por componentes o nombre de producto"
        };

        private ProductsViewModel ViewModel
        {
            get { return BindingContext as ProductsViewModel; }
            set { BindingContext = value; }
        }

        private async void btnClick(object sender, EventArgs ea)
        {
            if (paginationLimit == -1)
                return;
            paginationLimit++;
            await Paginate();
        }

        private ListView _list=new ListView();

        private ListView _autoComplete = new ListView();

        private Button btn = new Button();

        private async void textChanged(object sender, TextChangedEventArgs args)
        {
            if (args.NewTextValue != "")
            {
                _activityIndicator.IsRunning = true;
                IEnumerable<Products> result = await ViewModel.GetResult(searchBar.Text, 1, limitAutocomplete);
                if (result == null)
                {
                    await DisplayAlert("Hey?!", "No se pudo buscar la información", "Cancelar", "OK");
                    _autoComplete.IsVisible = false;
                }
                else if (result.Count() > 0)
                {
                    List<Products> prList = (List<Products>)result;
                    _autoComplete.IsVisible = true;
                    _autoComplete.ItemsSource = null;
                    _autoComplete.ItemsSource = prList;
                }
                else
                {
                    _autoComplete.IsVisible = false;
                } 
                _activityIndicator.IsRunning = false;
            }
            
        }

        private void itemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            // Set the BindingContext of the detail page.
            try
            {
                Products p = (Products)args.SelectedItem;
                Product detail = (Product)this.MasterPage.Detail;
                detail.updateProduct(p);
                // Show the detail page.
                this.MasterPage.IsPresented = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        MasterDetailPageDemoPage MasterPage;
        public ProductList(MasterDetailPageDemoPage Mp)
        {
            MasterPage = Mp;
            // Define a selected handler for the ListView.
            _list.ItemSelected += itemSelected;
            btn.Text = "Cargar más";
            btn.TextColor = Color.Red;
            btn.BackgroundColor = Color.White;
            btn.Clicked += btnClick;
            ViewModel = new ProductsViewModel();
            this.searchBar.TextChanged += new EventHandler<TextChangedEventArgs>(textChanged);
            this.searchBar.SearchCommand = new Command(new Action(searchProducts));
            //Image cell for search results
            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetValue(TextCell.TextColorProperty, Color.Red);
            cell.SetValue(TextCell.DetailColorProperty, Color.Red);
            cell.SetBinding(ImageCell.TextProperty, "name");
            cell.SetBinding(ImageCell.ImageSourceProperty, "thumb");
            cell.SetBinding(ImageCell.DetailProperty, "description");
            //Text cell for auto complete 
            var textCell = new DataTemplate(typeof(TextCell));
            textCell.SetValue(TextCell.TextColorProperty, Color.Red);
            textCell.SetValue(TextCell.DetailColorProperty, Color.Red);
            textCell.SetBinding(TextCell.TextProperty, "name");
            //product list configuration
            _list.BackgroundColor = Color.White;
            _list.SeparatorColor = Color.Red;
            _list.ItemTemplate = cell;
            //AutoComplete list configuration
            _autoComplete.BackgroundColor = Color.White;
            _autoComplete.SeparatorColor = Color.Red;
            _autoComplete.ItemTemplate = textCell;
            _autoComplete.IsVisible = false;
            //_autoComplete.
            Title = MasterPage.Title;
            Content = new StackLayout
            {
                Children =
                    {
                        new Label
                        {
                            Text = "Buscador",
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            FontSize = 18
                        },
                        searchBar,
                        _autoComplete,
                        new ScrollView
                        {
                            Content = resultsLabel,
                            VerticalOptions=LayoutOptions.FillAndExpand
                        },
                        _activityIndicator,
                        _list,
                        btn
                    },
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5)
            };
            this.searchProducts();
        }

        public async void searchProducts()
        {
            this.resultsLabel.Text = "Resultados: " + this.searchBar.Text;
            await GetData();
        }

        private async Task Paginate()
        {
            _activityIndicator.IsRunning = true;

            IEnumerable<Products> result = await ViewModel.GetResult(this.searchBar.Text, this.paginationLimit, this.limitProduct);
            if (result == null)
            {
                await DisplayAlert("Hey?!", "No se pudo buscar la información", "Cancelar", "OK");
            }
            else if (result.Count() > 0)
            {
                List<Products> prList = (List<Products>)_list.ItemsSource;
                prList.AddRange(result);
                _list.ItemsSource = null;
                _list.ItemsSource = prList;
            }
            else
            {
                paginationLimit = -1;
                btn.IsVisible = false;
            }

            _activityIndicator.IsRunning = false;
        }

        private async Task GetData()
        {
            _activityIndicator.IsRunning = true;

            IEnumerable<Products> result = await ViewModel.GetResult(this.searchBar.Text, this.paginationLimit, this.limitProduct);

            if (result == null)
            {
                await DisplayAlert("Hey?!", "No se pudo buscar la información", "Cancelar", "OK");
            }
            else if (result.Count() > 0)
            {
                _list.ItemsSource = result.ToList();
            }
            else
            {
                var emptyObject = new List<Products>();

                emptyObject.Add(new Products
                {
                    name = "No hubo resultados ...",
                    description = "Vacio"
                });

                _list.ItemsSource = emptyObject;
            }

            _activityIndicator.IsRunning = false;

        }
    }
}
