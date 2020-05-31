using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Manga
{
    class ImageManagement
    {
        public BitmapImage colorTint(BitmapImage sourceBitmap, 
                                    byte blueTint = 70,
                                    byte greenTint = 70, 
                                    byte redTint = 70)
        {
            float blue = blueTint / 255;
            float red = redTint / 255;
            float green = greenTint / 255;

            int height = sourceBitmap.PixelHeight;
            int width = sourceBitmap.PixelWidth;
            
            return sourceBitmap;
            
        }
    }
}
