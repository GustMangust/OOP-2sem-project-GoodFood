﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace GoodFood.Model
{
    public class Picture
    {
        public Picture()
        {

        }
        public Picture(Bitmap source)
        {
            this.Source = source;
        }
        Bitmap source;

        // Set serialization to IGNORE this property
        // because the 'PictureByteArray' property
        // is used instead to serialize
        // the 'Picture' Bitmap as an array of bytes.
        public Bitmap Source
        {
            get { return source; }
            set { source = value; }
        }
        public string PictureString
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                source.Save(ms, ImageFormat.Bmp);
                byte[] byteImage = ms.ToArray();
                return Convert.ToBase64String(byteImage); // Get Base64
            }
            set
            {
                Bitmap bmpReturn;
                byte[] byteBuffer = Convert.FromBase64String(value);
                MemoryStream memoryStream = new MemoryStream(byteBuffer);
                memoryStream.Position = 0;
                bmpReturn = (Bitmap)Image.FromStream(memoryStream);
                memoryStream.Close();
                source = bmpReturn;
            }
        }
        // Set PictureByteArray Property 
        // to be an attribute of the Picture node 
        public byte[] PictureByteArray
        {
            get
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
                return (byte[])converter.ConvertTo(source, typeof(byte[]));
            }
            set
            {
                source = new Bitmap(new MemoryStream(value));
            }
        }
    }
}
