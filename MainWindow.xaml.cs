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
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        public static NoMoneyPayPage noMoneyPayPage = new NoMoneyPayPage();
        public static HaveMoneyPayPage haveMoneyPayPage = new HaveMoneyPayPage();

        public MainWindow() {
            InitializeComponent();
            Uri uri = new Uri("/StartWindow.xaml", UriKind.Relative);
            frame.Source = uri;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (BasePayPage.inputMoneyWindow != null) {
                BasePayPage.inputMoneyWindow.Close();
                
            }
        }
    }
}
