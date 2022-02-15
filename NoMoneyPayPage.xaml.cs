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
            int[] pMResult = null;
            switch (CheackRB(spRadiobutton.Children)) {
                case 0:
                    pMResult = Calculator.NumToArrayReduceRemain(int.Parse(tbPayMoney.Text));
                    break;
                case 1:
                    pMResult = Calculator.PayMoneyCalc(int.Parse(tbPayMoney.Text));
                    break;
                default:
                    break;
            }
            ResultSet(int.Parse(tbPayMoney.Text),rPayMoney, rChange, pMResult);
            base.ButtonVisibleOn(nextAccounting);
            base.PayCalc_Click(sender, e, pLabels, pMResult);
        }

        private void ResultSet(int v, Label rPayMoney, Label rChange, int[] pMResult) {
            int result = Calculator.ArrayToNum(pMResult);
            rPayMoney.Content = result;
            rChange.Content = result - v;
        }

        //手動入力
        private void BtPayInput_Click(object sender, RoutedEventArgs e) {
            if (tbPayMoney.Text == "") {
                MessageBox.Show("支払額が正しくありません");
                return;
            }
            inputMoneyWindow.lbPayMoney.Content = tbPayMoney.Text;
            int[] rMoney;
            rMoney = base.PayInputShow(int.Parse(tbPayMoney.Text), pLabels, Calculator.PAYMONEYLIMITS);
            if (inputMoneyWindow.Result) {

                rPayMoney.Content = Calculator.ArrayToNum(rMoney);
                rChange.Content = Calculator.ArrayToNum(rMoney) - int.Parse(tbPayMoney.Text);

                base.ButtonVisibleOn(nextAccounting);
            }
        }
        private void TbPayMoneyChenged(object sender, TextChangedEventArgs e) {
            TextBox box = (TextBox)sender;
            int d;
            if (!int.TryParse(box.Text, out d)) {
                box.Text = Regex.Replace(box.Text, "[^0-9-]", "");
            }
        }
        private new void BackButton_Click(object sender, RoutedEventArgs e) {
            base.BackButton_Click(sender, e);

        }

        private void nextAccounting_Click(object sender, RoutedEventArgs e) {
            base.NextAccounting_Click(sender, e, nextAccounting,tbPayMoney,pLabels, rPayMoney,rChange);
        }

        
    }
}
