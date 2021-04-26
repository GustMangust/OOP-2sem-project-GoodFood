using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GoodFood
{
    public static class ImageConverter
    {
        public static Bitmap ConvertToBitmapFromInteropBitmap(ImageSource source)
        {
            BitmapSource bmpSource = source as BitmapSource;
            MemoryStream ms = new MemoryStream();
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmpSource));
            encoder.Save(ms);
            ms.Seek(0, SeekOrigin.Begin);
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(ms);
            return bitmap;
        }
        public static Bitmap ConvertToBitmap(BitmapSource bitmapSource)
        {
            var width = bitmapSource.PixelWidth;
            var height = bitmapSource.PixelHeight;
            var stride = width * ((bitmapSource.Format.BitsPerPixel + 7) / 8);
            var memoryBlockPointer = Marshal.AllocHGlobal(height * stride);
            bitmapSource.CopyPixels(new Int32Rect(0, 0, width, height), memoryBlockPointer, height * stride, stride);
            var bitmap = new Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format32bppPArgb, memoryBlockPointer);
            return bitmap;
        }

        public static ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { }
        }
    }
}
