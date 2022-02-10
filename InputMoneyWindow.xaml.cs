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
        private int[] rHaveMoney = new int[Calculator.MONEYTYPE.Length];
        private int[] rMoney = new int[Calculator.MONEYTYPE.Length];
        public bool Result;
        int[] cancelNums;
        private List<TextBox> textBoxes;
        private List<ScrollBar> scrollBars;
        public InputMoneyWindow() {
            InitializeComponent();
            textBoxes = new List<TextBox> { tb10000, tb5000, tb1000, tb500, tb100, tb50, tb10, tb5, tb1 };
            scrollBars = new List<ScrollBar> { sb10000, sb5000, sb1000, sb500, sb100, sb50, sb10, sb5, sb1 };
        }
        //所持金、もしくは既定の値を上限とする支払い
        public int[] ShowWindow(int pMoney, int[] limits) {
            cancelNums = limits.ToArray();
            for (int i = 0; i < textBoxes.Count; i++) {
                scrollBars[i].Maximum = cancelNums[i];
                scrollBars[i].Value = 0;
                
            }

            //なぜかShowDialogのみだとエラーが発生するので「==true || Result」を書く
            switch (ShowDialog() == true || Result) {
                case true:
                    if (pMoney > Calculator.ArrayToNum(rMoney)) {
                        MessageBox.Show("支払いに必要な額が入力された値より多いです");
                        return this.ShowWindowCancel(pMoney);
                    }
                    break;
                case false:
                    return new int[] {0,0,0,0,0,0,0,0,0, };
                default:

                    break;
            }

            return this.rMoney;

        }
        public int[] ShowWindowCancel(int pMoney) {
            switch (ShowDialog() == true || Result) {
                case true:
                    if (pMoney > Calculator.ArrayToNum(rMoney)) {
                        MessageBox.Show("支払いに必要な額が入力された値より多いです");
                        return this.ShowWindowCancel(pMoney);
                    }
                    break;
                case false:
                    break;
                default:
                    break;
            } return rMoney; 
        }

        private void btClose_Click(object sender, RoutedEventArgs e) {
            Result = false;
            Visibility = Visibility.Hidden;
        }

        private void btDecision_Click(object sender, RoutedEventArgs e) {
            Result = true;
            Visibility = Visibility.Hidden;

        }

        private void Tb_Changed(Object sender, TextChangedEventArgs e) {
            var textb = (TextBox)sender;
            int d;
            if (!int.TryParse(textb.Text, out d)) {
                textb.Text = Regex.Replace(textb.Text, "[^0-9-]", "");
            }
            this.rMoney[textb.TabIndex] = int.Parse(textb.Text);
            lbAllPayMoney.Content = Calculator.ArrayToNum(rMoney);
        }
        //所持金入力メソッド
        public int[] ShowWindow(int[] haveMoney ,int[] limits) {
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
