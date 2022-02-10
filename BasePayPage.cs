using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        protected void NextAccounting_Click(object sender, RoutedEventArgs e, Button button, TextBox tbPayMoney, List<Label> pLabels, Label rPayMoney, Label rChange) {

            ButtonVisibleOff(button);
            SetLb(pLabels, new int[9]);
            tbPayMoney.Text = 0.ToString();
            rPayMoney.Content = 0.ToString();
            rChange.Content = 0.ToString();
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
            for (int i = 0; i < pMResult.Length; i++) {
                if (pMResult[i] != 0) {
                    pLabels[i].Background = new SolidColorBrush(Colors.BurlyWood);
                }
                else {
                    pLabels[i].Background = null;
                }
            }
            SetLb(pLabels, pMResult);
        }
        //支払いの枚数入力
        protected int[] PayInputShow(int pMoney, List<Label> pLabels, int[] Limits) {
            if (inputMoneyWindow.lbPayMoney.Visibility == Visibility.Hidden) {
                inputMoneyWindow.lbPayMoney.Visibility = Visibility.Visible;
            }
            inputMoneyWindow.lbPayMoney.Content = pMoney;
            int[] rNums = inputMoneyWindow.ShowWindow(pMoney,Limits);
            if (inputMoneyWindow.Result) {
                SetLb(pLabels, rNums);
                return rNums;
            }
            return rNums;
        }
        

    }
    }
