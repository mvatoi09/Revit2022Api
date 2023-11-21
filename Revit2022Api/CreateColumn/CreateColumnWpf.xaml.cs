using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Revit2022Api.CreateColumn
{
    /// <summary>
    /// Interaction logic for CreateColumnWpf.xaml
    /// </summary>
    public partial class CreateColumnWpf : Window
    {

        private readonly ExternalEvent _createColumnEvent;  // tao bien toan cuc ra ngoai co the truy cap duoc
        public CreateColumnWpf(ExternalEvent createColumnEvent)
        {
            InitializeComponent();
            _createColumnEvent = createColumnEvent;   // gan gia tri tu createColumnEvent vao bien toan cuc
                                                      //_createColumnEvent                      
        }

        private void btnClickCreateColumn(object sender, RoutedEventArgs e)
        {
            _createColumnEvent.Raise();   // raise : xu ly su kien
        }
    }
}
