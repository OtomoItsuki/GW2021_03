using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        int[] hMoney = new int[9];
        public InputMoneyWindow() {
            InitializeComponent();
        }
        public int[] ShowWindow() {
            this.ShowDialog();
            int[] money = {int.Parse(tb10000.Text), int.Parse(tb5000.Text), int.Parse(tb1000.Text),
                           int.Parse(tb500.Text),int.Parse(tb100.Text),int.Parse(tb50.Text),int.Parse(tb10.Text),int.Parse(tb5.Text),int.Parse(tb1.Text)};
            return money;
        }
        public int[] NHMShowWindow() {
            this.ShowDialog();
            int[] money = {int.Parse(tb10000.Text), int.Parse(tb5000.Text), int.Parse(tb1000.Text),
                           int.Parse(tb500.Text),int.Parse(tb100.Text),int.Parse(tb50.Text),int.Parse(tb10.Text),int.Parse(tb5.Text),int.Parse(tb1.Text)};
            lbHaveMoney1.Visibility = Visibility.Hidden;
            return money;
        }

        private void btClose_Click(object sender, RoutedEventArgs e) {
            Visibility = Visibility.Hidden;
        }

        private void btDecision_Click(object sender, RoutedEventArgs e) {
            
            btClose_Click(sender, e);
        }

        private void Tb_Changed(Object sender, TextChangedEventArgs e) {
            
        }
        
    }
}
