using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoIt;
using System.Drawing;

namespace EntropiaBot.EntropiaWindow
{
    public struct Area
    {
        public String Name;
        public int x1, y1, x2, y2;
        public int Width {get{ return x2 - x1;}}
        public int Height { get { return y2 - y1; } }

        

        public Area(int px1, int py1, int px2, int py2, String name)
        {
            x1 = px1; y1 = py1; x2 = px2; y2 = py2;
            this.Name = name;
        }
    }
    
    
    public class EntropiaHandler
    {
        private static EntropiaHandler instance;
        private IntPtr _winHandle;
        public  IntPtr WinHandle
        {
            set { _winHandle = value; }
            get{return  AutoItX.WinGetHandle("Entropia Universe Client"); }
        
        }
 
        private EntropiaHandler()
        {
            WinHandle = AutoItX.WinGetHandle("Entropia Universe Client");
        }

        public static EntropiaHandler getInstance()
            {
                if (instance == null)
                    instance = new EntropiaHandler();

                    return instance;
                
            }

        // is pixel with color exist in the area
        public bool IsPixelExist(Area area, Color color)
        {
            bool result = false;
            Bitmap bmp = GetBitmapFromScreen(area);
            byte[] rgbArray = bitmapToByteArr(bmp);
            for (int i = 0; i < rgbArray.Length - 3; i += 4)
            {

                if (color.B == rgbArray[i] &&
                    color.G == rgbArray[i + 1] &&
                    color.R == rgbArray[i + 2])
                {
                    result = true;
                    break;
                }

            }
            
            return result;
        }

        //convert Bitmap to Byte Array
        public byte[] bitmapToByteArr(Bitmap bmp)
        {

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap. 
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            return rgbValues;
        }
        
        
        // image from Screen
        // x1, y, x2, y2 left top and right bottom coord relatively window
         public Bitmap GetBitmapFromScreen(Area area)
        {
            int x = 0, y = 0;

            x = AutoItX.WinGetPos(WinHandle).Left;
            y = AutoItX.WinGetPos(WinHandle).Top;

            //AutoItX.W
            Bitmap bmp = new Bitmap(area.x2 - area.x1,area.y2 - area.y1);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(x + area.x1, y + area.y1, 0, 0, new System.Drawing.Size((int)bmp.Width, (int)bmp.Height));
            }
           return bmp;
        }
        

    }
}
