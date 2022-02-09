using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PayControl {
    public class BasePayPage : Page {
        public List<Label> pLabels = null;
        public static InputMoneyWindow inputMoneyWindow = new InputMoneyWindow();


        public BasePayPage() {

        }

        protected void BackButton_Click(object sender, RoutedEventArgs e) {
            NavigationService.GoBack();
        }


        protected void SetLb(List<Label> labels, int[] pastNums) {
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

        protected void PayCalc_Click(object sender, RoutedEventArgs e, List<Label> pLabels, int[] pMResult) {

            SetLb(pLabels, pMResult);
        }
        //支払いの枚数入力
        protected int[] PayInputShow(object sender, RoutedEventArgs e, List<Label> pLabels, int[] Limits) {
            
            int[] rNums = inputMoneyWindow.ShowWindow(Limits);
            SetLb(pLabels, rNums);
            return rNums;

        }
        

    }
    }
