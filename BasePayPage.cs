using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PayControl {
    public class BasePayPage : Page{
        readonly int[] MONEYTYPE = new int[] { 10000, 5000, 1000, 500, 100, 50, 10, 5, 1 };
        public BasePayPage() {
            
        }

        protected void BackButton_Click(object sender, RoutedEventArgs e) {
            NavigationService.GoBack();
        }
        protected void PayCalc_Click(object sender, RoutedEventArgs e) {

        }
        
        protected int[] PayCaliculate(int payMoney, int checkedNum) {
            int[] resultPay = new int[2];
            //支払いに出す総額
            resultPay[0] = payMoney;
            //総額から支払額を引いた数(お釣り)今は確定で0
            resultPay[1] = payMoney-payMoney;
            return resultPay;
        }
        protected void SetLb(int[] hLabels ,Label[] labels) {
            for (int i = 0; i < labels.Length; i++) {

            }
        }
    }
}
