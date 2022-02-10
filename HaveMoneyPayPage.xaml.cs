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
        protected int[] haveMoney = new int[Calculator.MONEYTYPE.Length];



        public HaveMoneyPayPage() {
            InitializeComponent();
            nextAccounting.Visibility = Visibility.Hidden;
            hLabels = new List<Label>() { lbH10000, lbH5000, lbH1000, lbH500, lbH100, lbH50, lbH10, lbH5, lbH1 };
            pLabels = new List<Label>() { lbP10000, lbP5000, lbP1000, lbP500, lbP100, lbP50, lbP10, lbP5, lbP1 };
            base.SetLb(hLabels, haveMoney);
        }

        private void payCalc_Click(object sender, RoutedEventArgs e) {
            if (tbPayMoney.Text =="") {
                return;
            }   
            int checkedNum = CheackRB(spRadiobutton.Children);
            int pMCalc = int.Parse(tbPayMoney.Text);
            int[] hMCalc = haveMoney.ToArray();
            //int haveMoneyCalc = ;

            if (pMCalc > int.Parse(lbhaveMoney.Content.ToString())) {
                MessageBox.Show("支払う額より所持金が少ないです","",MessageBoxButton.OK);
                return;
            }
            int[] pMResult = Calculator.NumToArrayReduceRemain(pMCalc,hMCalc);
            SetLb(pLabels, pMResult);
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

        private new void BackButton_Click(object sender, RoutedEventArgs e) {
            base.BackButton_Click(sender, e);
            
        }

        private void nextAccounting_Click(object sender, RoutedEventArgs e) {
            base.NextAccounting_Click(sender, e,nextAccounting,tbPayMoney,pLabels,rPayMoney,rChange);
            SetLb(hLabels, new int[9]);
            lbhaveMoney.Content = Calculator.ArrayToNum(haveMoney);
            rRemainMoney.Content = 0.ToString();
        }
        //所持金を入力する
        private void BtHMInput_Click(object sender, RoutedEventArgs e) {
            if (inputMoneyWindow.lbPayMoney.Visibility == Visibility.Visible) {
                inputMoneyWindow.lbPayMoney.Visibility = Visibility.Hidden;
            }
            Array.Copy(inputMoneyWindow.ShowWindow(haveMoney, Calculator.INPUTLIMITS), haveMoney, Calculator.MONEYTYPE.Length);
            SetLb(hLabels, haveMoney);
            lbhaveMoney.Content = Calculator.ArrayToNum(haveMoney);
        }
        //自分で支払い枚数を入力する
        private void BtPayInput_Click(object sender, RoutedEventArgs e) {
            if (tbPayMoney.Text == "" || int.Parse(tbPayMoney.Text) > Calculator.ArrayToNum(haveMoney)) {
                MessageBox.Show("支払い額が正しくありません");
                return;
            }
            inputMoneyWindow.lbPayMoney.Content = tbPayMoney.Text;
            int[] rMoney;
            rMoney = base.PayInputShow(int.Parse(tbPayMoney.Text), pLabels, haveMoney);
            if (inputMoneyWindow.Result) {

                for (int i = 0; i < haveMoney.Length; i++) {
                    haveMoney[i] -= rMoney[i];
                }
                rPayMoney.Content = Calculator.ArrayToNum(rMoney);
                rChange.Content = Calculator.ArrayToNum(rMoney) - int.Parse(tbPayMoney.Text);
                int[] ReduceCalc = Calculator.NumToArray(int.Parse(rChange.Content.ToString()));
                for (int i = 0; i < haveMoney.Length; i++) {
                    haveMoney[i] += ReduceCalc[i];
                }
                rRemainMoney.Content = Calculator.ArrayToNum(haveMoney);
            }
            base.ButtonVisibleOn(nextAccounting);
        }
    }
}
