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

namespace Revit2022Api.CreateBeam
{
    /// <summary>
    /// Interaction logic for CreateBeamWpf.xaml
    /// </summary>
    public partial class CreateBeamWpf : Window
    {

        private readonly ExternalEvent _createBeamEvent;
        private readonly ExternalEvent _createGetTypeBeamEvent;
        public CreateBeamWpf(ExternalEvent createBeamEvent , ExternalEvent createGetTypeBeamEvent)
        {
            InitializeComponent();
            _createBeamEvent = createBeamEvent;
            _createGetTypeBeamEvent = createGetTypeBeamEvent;
        }

        private void btnCreateBeam(object sender, RoutedEventArgs e)
        {
            _createBeamEvent.Raise();
        }

        private void comboboxFamilychange(object sender, SelectionChangedEventArgs e)
        {
            _createGetTypeBeamEvent.Raise();
        }
    }
}
