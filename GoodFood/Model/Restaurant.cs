using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace GoodFood.Model
{
    public class Restaurant
    {
        [Key]
        public int Rest_ID { get; set; }
        public string Name { get; set; }
        public int Number_of_tables { get; set; }
        public int Start_time { get; set; }
        public int End_time { get; set; }
        public byte[] Image { get; set; }
        public string Type_of_cuisine { get; set; }
        [NotMapped]
        public Picture Restaurant_Picture { get; set; }
        [NotMapped]
        public ImageSource Restaurant_ImageSource { get; set; }
        public Restaurant() 
        {
        }
        public Restaurant(int id,string name,int num,int start,int end,byte[] im,string type) 
        {
            Rest_ID = id;
            Name = name;
            Number_of_tables = num;
            Start_time = start;
            End_time = end;
            Image = im;
            Type_of_cuisine = type;
        }
        public Restaurant(string name,int num,int start,int end,byte[] im,string type) 
        {
            Name = name;
            Number_of_tables = num;
            Start_time = start;
            End_time = end;
            Image = im;
            Type_of_cuisine = type;
        }
        /*public int Rest_ID
        {
            get 
            {
                return rest_ID;
            }
            set 
            {
                rest_ID = value;
            }
        }
        public int Start_time
        {
            get
            {
                return start_time;
            }
            set
            {
                start_time = value;
            }
        }
        public int End_time
        {
            get
            {
                return end_time;
            }
            set
            {
                end_time = value;
            }
        }*/
    }
}
