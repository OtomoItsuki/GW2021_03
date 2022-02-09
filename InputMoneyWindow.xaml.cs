using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        int[] cancelNums;
        List<TextBox> textBoxes;
        List<ScrollBar> scrollBars;
        public InputMoneyWindow() {
            InitializeComponent();
            textBoxes = new List<TextBox> { tb10000, tb5000, tb1000, tb500, tb100, tb50, tb10, tb5, tb1 };
            scrollBars = new List<ScrollBar> { sb10000, sb5000, sb1000, sb500, sb100, sb50, sb10, sb5, sb1 };
        }
        public int[] ShowWindow(int[] limits) {
            cancelNums = new int[] {0,0,0,0,0,0,0,0,0,0 };
            for (int i = 0; i < textBoxes.Count; i++) {
                scrollBars[i].Maximum = limits[i];
                scrollBars[i].Value = 0;
            }
            this.ShowDialog();
            
            return rMoney;
        }
        

        private void btClose_Click(object sender, RoutedEventArgs e) {
            btDecision_Click(sender, e);
            rMoney = cancelNums;
        }

        private void btDecision_Click(object sender, RoutedEventArgs e) {
            Visibility = Visibility.Hidden;

        }

        private void Tb_Changed(Object sender, TextChangedEventArgs e) {
            var textb = (TextBox)sender;
            int d;
            if (!int.TryParse(textb.Text, out d)) {
                textb.Text = Regex.Replace(textb.Text, "[^0-9-]", "");
            }
            rMoney[textb.TabIndex] = int.Parse(textb.Text);
            lbHaveMoney.Content = Calculator.ArrayToNum(rMoney);
        }

        public int[] ShowWindow(int[] limits, int[] haveMoney) {
            cancelNums = haveMoney;
            for (int i = 0; i < textBoxes.Count; i++) {

                scrollBars[i].Maximum = limits[i];
                scrollBars[i].Value = haveMoney[i];
            }
            this.ShowDialog();
            return rMoney;
        }
    }
}
