using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// NoMoneyPayWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class NoMoneyPayPage :BasePayPage{

        public int[] money = { 1, 2, 3, 4, 5, 1, 2, 3, 4 };
        public NoMoneyPayPage() {
            InitializeComponent();
            nextAccounting.Visibility = Visibility.Hidden;
            pLabels = new List<Label>() { lbP10000, lbP5000, lbP1000, lbP500, lbP100, lbP50, lbP10, lbP5, lbP1 };
        }

        private void payCalc_Click(object sender, RoutedEventArgs e) {
            if (tbPayMoney.Text == "") {
                return;
            }
            int checkedNum = CheackRB(spRadiobutton.Children);
            int[] pMResult = Calculator.PayMoneyCalc(int.Parse(tbPayMoney.Text));
            base.PayCalc_Click(sender,e, pLabels, pMResult);
           
            base.ButtonVisibleOn(nextAccounting);
        }
        private void BtPayInput_Click(object sender, RoutedEventArgs e) {
            if (tbPayMoney.Text == "") {
                MessageBox.Show("支払額が正しくありません");
                return;
            }
            int rNum = Calculator.ArrayToNum(base.PayInputShow(sender, e, pLabels, Calculator.limits));
            rPayMoney.Content = rNum;
            rChange.Content = rNum- int.Parse(tbPayMoney.Text);
        }
        private void TbPayMoneyChenged(object sender, TextChangedEventArgs e) {
            TextBox box = (TextBox)sender;
            int d;
            if (!int.TryParse(box.Text, out d)) {
                box.Text = Regex.Replace(box.Text, "[^0-9-]", "");
            }
        }
        private void backButton_Click(object sender, RoutedEventArgs e) {
            base.BackButton_Click(sender, e);

        }

        private void nextAccounting_Click(object sender, RoutedEventArgs e) {
            base.NextAccounting_Click(sender, e, nextAccounting);
        }

        
    }
}
