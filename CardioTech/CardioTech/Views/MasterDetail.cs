using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CardioTech.Models;
using System.Threading.Tasks;

namespace CardioTech.Views
{
    class MasterDetailPageDemoPage : MasterDetailPage
    {
        public Label header = new Label
        {
            Text = "CardioTech",
            FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            HorizontalOptions = LayoutOptions.Center
        };
        public MasterDetailPageDemoPage()
        {
            this.Title = header.Text;
            this.Master = new ProductList(this);
            this.Detail = new Product();
            
            // For Windows Phone, provide a way to get back to the master page.
            if (Device.OS == TargetPlatform.WinPhone)
            {
                (this.Detail as ContentPage).Content.GestureRecognizers.Add(
                    new TapGestureRecognizer((view) =>
                    {
                        this.IsPresented = true;
                    }));
            }
            this.IsPresented = true;


        }

    }
}
