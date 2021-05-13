using DevExpress.Mvvm;
using GoodFood.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GoodFood.ViewModel
{
    class AddRestaurantViewModel:ViewModelBase, IDataErrorInfo
    {
        public MainViewModel Vm { get; set; }
        public string Error { get { return null; } }
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string this[string name]
        {
            get 
            {
                string result = null;
                switch (name) 
                {
                    case "Name":
                        if (string.IsNullOrWhiteSpace(Name))
                            result = "Не может быть пустым";
                        else if (Name.Length < 3)
                            result = "Не менее 3 символов";
                        else if (!Validation.IsRestaurantNameValid(Name))
                            result = "Неверные символы";
                        break;
                    case "Type_of_cuisine":
                        if (string.IsNullOrWhiteSpace(Type_of_cuisine))
                            result = "Не может быть пустым";
                        else if (Type_of_cuisine.Length < 3)
                            result = "Не менее 3 символов";
                        else if (!Validation.IsRestaurantNameValid(Type_of_cuisine))
                            result = "Неверные символы";
                        break;
                    case "Number_of_tables":
                        if (string.IsNullOrWhiteSpace(Number_of_tables))
                            result = "Не может быть пустым";
                        else if (!Validation.IsNumberOfTablesValid(Number_of_tables))
                            result = "Неверные символы или слишком большое число";
                        break;
                    case "Start_time":
                        if (string.IsNullOrWhiteSpace(Start_time))
                            result = "Не может быть пустым";
                        else if (Start_time == End_time)
                            result = "Введите другое время";
                        else if (!Validation.IsTimeValid(Start_time))
                            result = "Неверный формат или неверные символы";
                        break;
                    case "End_time":
                        if (string.IsNullOrWhiteSpace(End_time))
                            result = "Не может быть пустым";
                        else if (Start_time == End_time)
                            result = "Введите другое время";
                        else if (!Validation.IsTimeValid(End_time))
                            result = "Неверный формат или неверные символы";
                        break;
                }
                if (ErrorCollection.ContainsKey(name))
                    ErrorCollection[name] = result;
                else if (result != null)
                    ErrorCollection.Add(name, result);
                RaisePropertyChanged("ErrorCollection");
                return result;
            }
        }

        private string name;
        public string Name
        {
            get 
            {
                return name;
            }
            set 
            {
                name=value;
                RaisePropertyChanged("Name");
            }
        }
        private string number_of_tables;
        public string Number_of_tables
        {
            get 
            {
                return number_of_tables;
            }
            set 
            {
                number_of_tables=value;
                RaisePropertyChanged("Number_of_tables");
            }
        }
        private string start_time;
        public string Start_time
        {
            get 
            {
                return start_time;
            }
            set 
            {
                start_time=value;
                RaisePropertyChanged("Start_time");
            }
        }
        private string end_time;
        public string End_time
        {
            get 
            {
                return end_time;
            }
            set
            {
                end_time=value;
                RaisePropertyChanged("End_time");
            }
        }
        private Picture image;
        public Picture Image
        {
            get { return image; }
            set
            {
                image = value;
                RaisePropertyChanged("Image");
            }
        }
        private ImageSource imageSource;
        public ImageSource ImageSource
        {
            get 
            {
                return imageSource; 
            }
            set
            {
                imageSource = value;
                RaisePropertyChanged("ImageSource");
            }
        }

        private string type_of_cuisine;
        public string Type_of_cuisine
        {
            get 
            {
                return type_of_cuisine;
            }
            set 
            {
                type_of_cuisine = value;
                RaisePropertyChanged("Type_of_cuisine");
            }
        }
        public ICommand addImage => new DelegateCommand(AddImage);
        private void AddImage() 
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Картинки|*.jpg;*.jpeg;*.png",
                ValidateNames = true,
                CheckFileExists = false,
                CheckPathExists = true,
                Multiselect = false,
                Title = "Выберите файл"
            };
            if (dialog.ShowDialog() == true)
            {
                ImageSource = new BitmapImage(new Uri(dialog.FileName));
            }
        }
        public ICommand addRestaurant => new DelegateCommand(AddRestaurant);
        private void AddRestaurant() 
        {
            if (Validation.IsRestaurantNameValid(Name) && Start_time!=End_time && Validation.IsTimeValid(Start_time) && Validation.IsTimeValid(End_time) && Validation.IsRestaurantNameValid(Type_of_cuisine) && Validation.IsNumberOfTablesValid(Number_of_tables) && ImageSource != null) 
            {
                Image = new Picture(ImageConverter.ConvertToBitmap(ImageSource as BitmapImage));
                int start;
                int end;
                if (Start_time[0] == '0')
                    start = Convert.ToInt32(Start_time.Substring(1, 1));
                else start = Convert.ToInt32(Start_time);

                if (End_time[0] == '0')
                    end = Convert.ToInt32(End_time.Substring(1,1));
                else end = Convert.ToInt32(End_time);
                DB.AddRestaurant(Name, Convert.ToInt32(Number_of_tables),start , end, Image.PictureString, Type_of_cuisine);
                Vm.SelectedViewModel = new AllRestaurantsViewModel(Vm);
            }
        }
        public AddRestaurantViewModel(MainViewModel vm) 
        {
            Vm = vm;
        }
    }
}
