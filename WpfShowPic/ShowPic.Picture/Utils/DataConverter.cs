using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShowPic.Picture.DataConverter
{

    [ValueConversion(typeof(byte[]), typeof(BitmapImage))]
    public class DataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte[] img = null;
            if (value == null) return null;
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                img = (byte[])value;
            }
            if (img == null)
            {
                return null;
            }
            return ShowSelectedIMG(img);                //以流的方式显示图片的方法
        }


        //转换器中二进制转化为BitmapImage  datagrid绑定UI
        private BitmapImage ShowSelectedIMG(byte[] img)
        {
            if (img != null)
            {
                //img是从数据库中读取出来的字节数组
                System.IO.MemoryStream ms = new System.IO.MemoryStream(img);

                ms.Seek(0, System.IO.SeekOrigin.Begin);
                BitmapImage newBitmapImage = new BitmapImage();
                newBitmapImage.BeginInit();
                newBitmapImage.StreamSource = ms;
                newBitmapImage.EndInit();
                return newBitmapImage;
            }
            return null;

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

}

