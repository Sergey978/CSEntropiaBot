using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoIt;
using System.Drawing;

namespace EntropiaBot.EntropiaWindow
{
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


        public bool IsPixelExist(int x1, int y1, int x2, int y2, int color)
        {
            Bitmap bmp = new Bitmap(x2-x1, y2-y1);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(x1, x1, 0, 0, new Size(x2 - x1, x2 - x1));
            }
            
            return false;
        }

        // Конверт bitmap в массив
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        

    }
}
