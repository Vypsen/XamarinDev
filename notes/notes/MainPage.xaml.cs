using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.IO;

namespace notes
{
    public class Notes
    {
        public string Text { get; set; }
        public int NumLines = 0;
        public string Date { get; set; }
        public int Id { get; set; }
        private static int GlobalId = 0;

        public Notes()
        {
            Id = ++GlobalId;
        }
    }
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {

            InitializeComponent();

            var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "jsonResult.json");

            if (File.Exists(file))
            {
                JObject Notes = JObject.Parse(File.ReadAllText(file));
                foreach(JObject i in Notes["notes"])
                {
                    var info = i["note"];
                    Console.WriteLine(i);
                    Console.WriteLine(info);
                    Instance.pool.Add(new Notes()
                    {
                        Text = info["text"].ToString(),
                        Date = info["data"].ToString(),
                        Id = (int)info["id"]
                    }); ;   
                
                }

                rebild();
            }            


            BindableLayout.SetItemsSource(Col1, Instance.marks1);
            BindableLayout.SetItemsSource(Col2, Instance.marks2);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            button.IsEnabled = false;
            Instance.currentId = 0;

            var form = new Page();
            Navigation.PushAsync(form);
            form.Disappearing += (send, ev) =>
            {
                button.IsEnabled = true;
                rebild();  

            };
        }

        private void rebild()
        {


            JArray jArray = new JArray();

            Instance.l1 = 0;
            Instance.l2 = 0;
            Instance.marks1.Clear();
            Instance.marks2.Clear();
            foreach (Notes i in Instance.pool)
            {
                if (Instance.l1 <= Instance.l2)
                {
                    Instance.marks1.Add(i);
                    Instance.l1 += i.NumLines + 2;
                }
                else
                {
                    Instance.marks2.Add(i);
                    Instance.l2 += i.NumLines + 2;
                }


                JObject jObject = new JObject()
                {
                    {"text", i.Text },
                    {"data", i.Date },
                    { "id", i.Id }
                };

                jArray.Add(new JObject() {

                    { "note", jObject } 

                });



                JObject arrayJson = new JObject() { { "notes", jArray } };
                string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "jsonResult.json");
                File.WriteAllText(file, arrayJson.ToString());



            }
            

 
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            
            var smth = sender as Frame;
            Instance.currentId = int.Parse(smth.AutomationId.ToString());

            var form = new Page();
            Navigation.PushAsync(form);

            form.Disappearing += (send, ev) =>
            {
                rebild();
            };
        }

        private async void PanGestureRecognizer_PanUpdatedLeft(object sender, PanUpdatedEventArgs e)
        {

            var thisFrame = sender as Frame;

            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    {
                        
                        thisFrame.TranslationX = Math.Min(0, e.TotalX * 2);
                        if (Instance.flag && Math.Abs(thisFrame.TranslationX / (MainStack.Width / 6)) > 0.4)
                        {
                            Instance.flag = false;

                            if (await DisplayAlert("delete", "are you sure about that?", "yes", "cancel"))
                            {

                                Instance.pool.Remove(Instance.pool.Find(x => x.Id == int.Parse(thisFrame.AutomationId)));

                                rebild();
                                Instance.flag = true;
                            }
                            else
                            {
                                thisFrame.TranslationX = 0;
                                Instance.flag = true;
                            }
                        }

                        break;
                    }


                case GestureStatus.Completed:
                    {
                        thisFrame.TranslationX = 0;

                        break;
                    }
            }


        }

        private async void PanGestureRecognizer_PanUpdatedRight(object sender, PanUpdatedEventArgs e)
        {

            var thisFrame = sender as Frame;

            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    {
                        //thisFrame.TranslationX = Math.Abs(e.TotalX);
                        thisFrame.TranslationX = Math.Max(0, e.TotalX * 2);
                        //Math.Max(Math.Min(0, e.TotalX), -Math.Abs(Content.Width - 700));
                        if (Instance.flag && Math.Abs(thisFrame.TranslationX / (MainStack.Width / 6)) > 0.4)
                        {
                            Instance.flag = false;

                            if (await DisplayAlert("delete", "are you sure about that?", "yes", "cancel"))
                            {
                                Instance.pool.Remove(Instance.pool.Find(x => x.Id == int.Parse(thisFrame.AutomationId)));

                                rebild();

                                Instance.flag = true;
                            }
                            else
                            {
                                thisFrame.TranslationX = 0;
                                Instance.flag = true;
                            }
                        }

                        break;
                    }


                case GestureStatus.Completed:
                    {
                        thisFrame.TranslationX = 0;

                        break;
                    }
            }
        }
    }
}
