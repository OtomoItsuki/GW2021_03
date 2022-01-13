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
        public List<Label> rLabels = null;
        public int[] money = {1,2,3,4,5,1,2,3,4 };

        public HaveMoneyPayPage() {
            InitializeComponent();
            hLabels = new List<Label>() { lbH10000, lbH5000, lbH1000, lbH500, lbH100, lbH50, lbH10, lbH5, lbH1 };
            hLabels = new List<Label>() { lbP10000, lbP5000, lbP1000, lbP500, lbP100, lbP50, lbP10, lbP5, lbP1 };
        }

        private void payCalc_Click(object sender, RoutedEventArgs e) {
            var s = spRadiobutton.Children[0];
            int checkedNum = CheackRB(spRadiobutton.Children);
            int[] payResult = payCaliculate(int.Parse(tbPayMoney.Text), checkedNum);
            rPayMoney.Content = payResult[0];
            rChange.Content = payResult[1];
            rHaveMoney.Content = (int.Parse(lbhaveMoney.Content.ToString()) + payResult[1]) -payResult[0];
        }


        private int CheackRB(UIElementCollection children) {
            int count = 0;
            foreach (RadioButton rb in children) {
                if ((bool)rb.IsChecked) {
                    return count;
                }
                count++;
            }
            return children.Count;
        }
        private void tbPayMoneyChenged(object sender, TextChangedEventArgs e) {
            TextBox box = (TextBox)sender;
            int d;
            if (!int.TryParse(box.Text, out d)) {
                box.Text = Regex.Replace(box.Text, "[^0-9-]", "");
            }
        }
    }
}
