using Essensgeldkasse.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensgeldkasse.Classes
{
    public class IndexToColorConverter  //: IValueConverter
    {
        public Color EvenColor { get; set; }

        public Color OddColor { get; set; }

   //     public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
   //     {
     //       var collectionView = parameter as CollectionView;

          //  return (List<Client>)(collectionView.ItemsSource as List<Client>).IndexOf(value) % 2 == 0 ? EvenColor : OddColor;
    //    }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
