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

namespace PayControl {
    /// <summary>
    /// StartWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class StartWindow : Page {
        public StartWindow() {
            InitializeComponent();
        }


        private void NMPButton_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(MainWindow.noMoneyPayPage);
        }
        private void HMPButton_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(MainWindow.haveMoneyPayPage);
            
        }
        
    }
}
