using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace delivery
{
    public partial class MainPage : ContentPage
    {

        public class MyItem
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public ImageSource PicPath { get; set; }
            public bool Flag { get; set; } = true;

        }
        public static List<MyItem> list { get; set; }



        public MainPage()
        {
            InitializeComponent();
            list = new List<MyItem>
            {

                new MyItem
                {
                    Name = "Item 1",
                    Description = "Какое то описание первого айтема",
                    PicPath = "shrek.jpg"
                },

                new MyItem
                {
                    Name = "Item 2",
                    Description = "А тут типа описание второго ну",
                    PicPath = "rickardo.jpg"
                },

                new MyItem
                {
                    Name = "Item 3",
                    Description = "Не сложно догадаться что тут третьего",
                    PicPath = "shrek.jpg"
                },

                new MyItem
                {
                    Name = "Item 4",
                    Description = "Ну и последний, можно еще добавить",
                    PicPath = "shrek.jpg"
                },


                new MyItem
                {
                    Name = "Item 4",
                    Description = "Ну и последний, можно еще добавить",
                    PicPath = "shrek.jpg"
                },



                new MyItem
                {
                    Name = "Item 4",
                    Description = "Ну и последний, можно еще добавить",
                    PicPath = "shrek.jpg"
                },


                new MyItem
                {
                    Name = "Item 4",
                    Description = "Ну и последний, можно еще добавить",
                    PicPath = "shrek.jpg"
                },


                new MyItem
                {
                    Name = "Item 4",
                    Description = "Ну и последний, можно еще добавить",
                    PicPath = "shrek.jpg"
                },


                new MyItem
                {
                    Name = "Item 4",
                    Description = "Ну и последний, можно еще добавить",
                    PicPath = "shrek.jpg"
                },



                new MyItem
                {
                    Name = "Item 4",
                    Description = "Ну и последний, можно еще добавить",
                    PicPath = "shrek.jpg"
                },


                new MyItem
                {
                    Name = "Item 4",
                    Description = "Ну и последний, можно еще добавить",
                    PicPath = "shrek.jpg"
                },



                new MyItem
                {
                    Name = "Item 4",
                    Description = "Ну и последний, можно еще добавить",
                    PicPath = "shrek.jpg"
                },



                new MyItem
                {
                    Name = "Item 4",
                    Description = "Ну и последний, можно еще добавитьeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee",
                    PicPath = "shrek.jpg"
                },



                new MyItem
                {
                    Name = "Item 4",
                    Description = "Ну и последний, можно еще добавить",
                    PicPath = "shrek.jpg"
                },

                 new MyItem
                 {
                    Name = "Item 5",
                    Description = "Ну и последний, можно ",
                    PicPath = "shrek.jpg"
                 },

                 new MyItem
                 {
                    Name = "Item 6",
                    Description = "Ну и последний, можно еще ",
                    PicPath = "shrek.jpg"
                 }

            };
            itemList.ItemsSource = list;


        }




        private void clickPush(object sender, EventArgs e)
        {
                
            var button = sender as Button;

            button.IsEnabled = false;
            list.Find(x => x.Name.Contains(button.CommandParameter.ToString())).Flag = false;
            Cart.cart[button.CommandParameter.ToString()] = new Cart.itemCart
            {
                Name = button.CommandParameter.ToString(),
                Description = list.Find(x => x.Name.Contains(button.CommandParameter.ToString())).Description.ToString(),
                PicPath = list.Find(x => x.Name.Contains(button.CommandParameter.ToString())).PicPath,
                count = 1
            };
        }


        private void goCart(object sender, EventArgs e)
        {

            var button = sender as Button;
            button.IsEnabled = false;
     
            Page cart = new Cart();

            Navigation.PushAsync(cart);

            cart.Disappearing += (send, ev) => button.IsEnabled = true;
            cart.Disappearing += (send, ev) => itemList.ItemsSource = null; 
            cart.Disappearing += (send, ev) => itemList.ItemsSource = list;

        }

    }
}
