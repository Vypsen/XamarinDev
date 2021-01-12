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

        

        public class itemCart
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public ImageSource PicPath { get; set; }
            public int count { get; set; }

        }

        public static Dictionary<string, itemCart> cart = new Dictionary<string, itemCart>();



        public Cart()
        {
            
            InitializeComponent();
            cartList.ItemsSource = cart.Select((a) => { return a.Value; }).ToList();

          

        }

       
        private async void delete_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            bool result = await DisplayAlert("Подтвердить действие", "Вы хотите удалить элемент из карзины?", "Да", "Нет");


            if (result)
            {
                MainPage.list.Find(x => x.Name.Contains(button?.CommandParameter.ToString())).Flag = true;
                cart.Remove(button?.CommandParameter.ToString());
                cartList.ItemsSource = cart.Select((a) => { return a.Value; }).ToList();


            }


        }

        private void count_Completed(object sender, EventArgs e)
        {

            
            
            var enrty = sender as Entry;
            if (enrty.Text == "")
            {
                enrty.Text = "1";
                cart[enrty.ReturnCommandParameter.ToString()].count = int.Parse(enrty.Text);
            }

            cart[enrty.ReturnCommandParameter.ToString()].count = int.Parse(enrty.Text); 

        }

        private async void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {

           
            var entry = sender as Entry;
            if (entry.Text != null){

                bool IsDigit = entry.Text.Length == entry.Text.Where(c => char.IsDigit(c)).Count();

                if (!IsDigit || entry.Text == "0")
                {
                    entry.Text = "1";
                }
 
            }

        }

        private async void order_Clicked(object sender, EventArgs e)
        {


            if (cart.Values.Count == 0)
            {
                await DisplayAlert("Ваш заказ", "Вы ничего не выбрали", "ОK");
            }
            else  await DisplayAlert("Подтвердить действие", "Вы хотите заказать выбранные товары?", "Да", "Нет");

        }
    }
}