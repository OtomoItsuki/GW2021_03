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
    /// HaveMoneyPayPage.xaml の相互作用ロジック
    /// </summary>
    public partial class HaveMoneyPayPage : BasePayPage {
        private List<Label> hLabels = null;
        protected int[] haveMoney = setMoney();

        private static int[] setMoney() {
            int[] money = new int[Calculator.MONEYTYPE.Length];
            return money;
        }




        public HaveMoneyPayPage() {
            InitializeComponent();
            nextAccounting.Visibility = Visibility.Hidden;
            hLabels = new List<Label>() { lbH10000, lbH5000, lbH1000, lbH500, lbH100, lbH50, lbH10, lbH5, lbH1 };
            pLabels = new List<Label>() { lbP10000, lbP5000, lbP1000, lbP500, lbP100, lbP50, lbP10, lbP5, lbP1 };
        }

        private void payCalc_Click(object sender, RoutedEventArgs e) {
            if (tbPayMoney.Text =="") {
                return;
            }   
            int checkedNum = CheackRB(spRadiobutton.Children);
            int pMcalc = int.Parse(tbPayMoney.Text);
            int[] hMCalc = haveMoney;
            //int haveMoneyCalc = ;

            if (pMcalc > int.Parse(lbhaveMoney.Content.ToString())) {
                MessageBox.Show("支払う額より所持金が少ないです","",MessageBoxButton.OK);
                return;
            }
            base.SetLb(hLabels, hMCalc);
            int[] pMResult = Calculator.PayMoneyCalc(pMcalc, hMCalc);
            base.PayCalc_Click(sender, e, pLabels, pMResult);
            rRemainMoney.Content = Calculator.ArrayToNum(hMCalc);
            base.ButtonVisibleOn(nextAccounting);
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
            base.NextAccounting_Click(sender, e,nextAccounting);
        }
        //所持金を入力する
        private void BtHMInput_Click(object sender, RoutedEventArgs e) {
            haveMoney =inputMoneyWindow.ShowWindow(Calculator.limits,haveMoney);
            SetLb(hLabels, haveMoney);
            lbhaveMoney.Content = Calculator.ArrayToNum(haveMoney);
        }
        //自分で支払い枚数を入力する
        private void BtPayInput_Click(object sender, RoutedEventArgs e) {
            if (tbPayMoney.Text == "" || int.Parse(tbPayMoney.Text) < Calculator.ArrayToNum(haveMoney)) {
                MessageBox.Show("支払額が正しくありません");
                return;
            }
            int[] rMoney = base.PayInputShow(sender, e,pLabels,haveMoney);
            for (int i = 0; i < haveMoney.Length; i++) {
                haveMoney[i] -= rMoney[i];
            }
            rPayMoney.Content = Calculator.ArrayToNum(rMoney);
            rChange.Content = Calculator.ArrayToNum(rMoney) - int.Parse(tbPayMoney.Text);
            rRemainMoney.Content = Calculator.ArrayToNum(haveMoney);
        }
    }
}
