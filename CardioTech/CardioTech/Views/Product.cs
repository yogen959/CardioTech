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
    class Product : ContentPage
    {

        Products Producto=new Products();
        private Button btn = new Button();
        Image image = new Image();
        private Label description = new Label
        {
            Text = "Busca nuestros Productos",
            TextColor = Color.Red,
            VerticalOptions = LayoutOptions.FillAndExpand,
            FontSize = 18
        };
        private Label name = new Label
        {
            Text = "Titulo: ",
            TextColor = Color.Red,
            VerticalOptions = LayoutOptions.FillAndExpand,
            FontSize = 18
        };

        private void btnClick(object sender, EventArgs ea)
        {
        }

        public void updateProduct(Products p)
        {
            name.Text = "Titulo: "+p.name;
            description.Text = p.description;
            if (p.id > 0)
            {
                UriImageSource source = new UriImageSource
                {
                    Uri = new Uri(p.medium),
                    CachingEnabled = false
                };
                image.Opacity = 0;
                image.Source = source;
                image.Opacity = 1;
            }
        }

        public Product()
        {
            image = new Image
            {
                Source = ImageSource.FromUri(new Uri("http://cardiotech.com.do/img/2016/10/14/logopng.png")),
                WidthRequest = 200,
                HeightRequest = 200
            };
            btn.Text = "Volver";
            btn.TextColor = Color.Red;
            btn.BackgroundColor = Color.White;
            btn.Clicked += btnClick;
            Content = new StackLayout
            {
                BackgroundColor = Color.White,
                Children =
                    {
                        name,
                        image,
                        new ScrollView
                        {
                            Content = description,
                            VerticalOptions=LayoutOptions.FillAndExpand
                            
                        },
                        btn
                    },
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5)
            };
        }
        
       
    }
}
