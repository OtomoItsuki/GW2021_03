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
        public List<Label> hLabels = null;
        public int[] money = {1,0,0,0,0,0,0,0,0 };




        public HaveMoneyPayPage() {
            InitializeComponent();
            nextAccounting.Visibility = Visibility.Hidden;
            hLabels = new List<Label>() { lbH10000, lbH5000, lbH1000, lbH500, lbH100, lbH50, lbH10, lbH5, lbH1 };
            pLabels = new List<Label>() { lbP10000, lbP5000, lbP1000, lbP500, lbP100, lbP50, lbP10, lbP5, lbP1 };
            rLabels = new List<Label>() { rPayMoney, rChange };
        }

        private void payCalc_Click(object sender, RoutedEventArgs e) {
            if (tbPayMoney.Text =="") {
                return;
            }   
            int checkedNum = CheackRB(spRadiobutton.Children);
            int pMcalc = int.Parse(tbPayMoney.Text);
            int[] hMCalc = money;
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
        
        private void BtHMInput_Click(object sender, RoutedEventArgs e) {
            base.BtInput_Click(sender, e,hLabels);
        }

        private void BtSelfInput_Click(object sender, RoutedEventArgs e) {
            base.SelfInputShow(sender, e);
        }
    }
}
