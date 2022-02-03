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
        public List<Label> pLabels = null;
        public List<Label> rLabels = null;
        public static InputMoneyWindow inputMoneyWindow = new InputMoneyWindow();
        
        public BasePayPage() {

        }

        protected void BackButton_Click(object sender, RoutedEventArgs e) {
            NavigationService.GoBack();
        }
        
        
        protected void SetLb(List<Label> labels,int[] pastNums) {
            if (pastNums.Length != labels.Count) {
                return;
            }
            for (int i = 0; i < labels.Count; i++) {
                labels[i].Content = pastNums[i];
            }
        }
        protected void ButtonVisibleOn(Button button) {
            button.Visibility = Visibility.Visible;
        }
        protected void ButtonVisibleOff(Button button) {
            button.Visibility = Visibility.Hidden;
        }
        protected void NextAccounting_Click(object sender, RoutedEventArgs e, Button button) {
            ButtonVisibleOff(button);
        }

        protected void BtInput_Click(object sender, RoutedEventArgs e,List<Label> labels) {
            inputMoneyWindow.Show();

        }
        protected int CheackRB(UIElementCollection children) {
            int count = 0;
            foreach (RadioButton rb in children) {
                if ((bool)rb.IsChecked) {
                    return count;
                }
                count++;
            }
            return children.Count;
        }

        internal void PayCalc_Click(object sender, RoutedEventArgs e, List<Label> pLabels, int[] pMResult) {

            SetLb(pLabels, pMResult);
        }

        internal void SelfInputShow(object sender, RoutedEventArgs e) {
            
        }
    }
}
