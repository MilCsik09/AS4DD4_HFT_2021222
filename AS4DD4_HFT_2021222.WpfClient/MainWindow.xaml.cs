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

namespace AS4DD4_HFT_2021222.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_EditBrands(object sender, RoutedEventArgs e)
        {
            BrandEditor brandEditor = new BrandEditor();
            brandEditor.Show();
        }


        private void Button_Click_EditCpus(object sender, RoutedEventArgs e)
        {

            CpuEditor cpuEditor = new CpuEditor();
            cpuEditor.Show();

        }

        private void Button_Click_EditVgas(object sender, RoutedEventArgs e)
        {
            VgaEditor vgaEditor = new VgaEditor();
            vgaEditor.Show();
        }
    }
}
