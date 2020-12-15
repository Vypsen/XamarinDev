using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace delivery
{
   
    public partial class Cart : ContentPage
    {
        public static Dictionary<string, itemCart> cart = new Dictionary<string, itemCart>();


        public class itemCart
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public ImageSource PicPath { get; set; }
            public int count { get; set; } = 0;

        }

       
        

        public Cart()
        {
            
            InitializeComponent();
            cartList.ItemsSource = cart.Select((a) => { return a.Value; }).ToList();

        }


        
        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
           
                count.Text = e.NewValue.ToString();
        }
    }
}