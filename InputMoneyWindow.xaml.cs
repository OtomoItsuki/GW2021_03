using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PayControl {
    /// <summary>
    /// InputMoneyWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class InputMoneyWindow : Window {
        int[] rMoney = new int[Calculator.MONEYTYPE.Length];
        List<TextBox> textBoxes;
        List<ScrollBar> scrollBars;
        public InputMoneyWindow() {
            InitializeComponent();
            textBoxes = new List<TextBox> { tb10000, tb5000, tb1000, tb500, tb100, tb50, tb10, tb5, tb1 };
            scrollBars = new List<ScrollBar> { sb10000, sb5000, sb1000, sb500, sb100, sb50, sb10, sb5, sb1 };
        }
        public int[] ShowWindow(int[] Limits) {
            for (int i = 0; i < textBoxes.Count; i++) {
                scrollBars[i].Maximum = Limits[i];
            }
            
            this.Show();
            for (int i = 0; i < textBoxes.Count; i++) {
                rMoney[i] = int.Parse(textBoxes[i].Text);
            }
            return rMoney;
        }
        

        private void btClose_Click(object sender, RoutedEventArgs e) {
            Visibility = Visibility.Hidden;
        }

        private void btDecision_Click(object sender, RoutedEventArgs e) {
            
            btClose_Click(sender, e);
        }

        private void Tb_Changed(Object sender, TextChangedEventArgs e) {
            
        }
        
    }
}
