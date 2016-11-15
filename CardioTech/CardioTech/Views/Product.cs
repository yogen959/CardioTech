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

        Products Producto;
        private Button btn = new Button();
        private Label description = new Label
        {
            Text = "Busca nuestros Productos",
            VerticalOptions = LayoutOptions.FillAndExpand,
            FontSize = 18
        };
        private Label name = new Label
        {
            Text = "Titulo: ",
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
        }

        public Product()
        { 
            btn.Text = "Volver";
            btn.TextColor = Color.Red;
            btn.BackgroundColor = Color.White;
            btn.Clicked += btnClick;
            Content = new StackLayout
            {
                Children =
                    {
                        name,
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
