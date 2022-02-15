using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayControl {
    public static class Calculator {
        /// <summary>
        /// コインが始まるインデックス
        /// </summary>
        private static readonly int COININDEX = 3;
        /// <summary>
        /// 硬貨の限度数 現在の日本の法律から
        /// </summary>
        private static readonly int COINILIMIT = 20;
        /// <summary>
        /// 使用するお金の種類を集めた配列
        /// </summary>
        public static readonly int[] MONEYTYPE = new int[] { 10000, 5000, 1000, 500, 100, 50, 10, 5, 1 };
        /// <summary>
        /// 使用するお金の限度数を種類ごとにした配列
        /// </summary>
        public static readonly int[] MoneyLimits = setLimits();
        public static readonly int[] PAYMONEYLIMITS = new int[] {127,127,127,COINILIMIT,COINILIMIT,COINILIMIT,COINILIMIT,COINILIMIT,COINILIMIT, };
        public static readonly int[] INPUTLIMITS = Enumerable.Repeat<int>(127, MONEYTYPE.Length).ToArray();

        private static int[] setLimits() {
            int[] Limits = new int[MONEYTYPE.Length];
            for (int i = 0; i < MONEYTYPE.Length; i++) {
                //10000はほぼ制限なし(127まで)
                if (i == 0) {
                    Limits[i] = 127;
                }
                //1000,100,10,1は2枚
                else if (i % 2 != 0) {
                    Limits[i] = 2;
                }
                //500,50,5は1枚
                else if (i < COININDEX) {
                    Limits[i] = 1;
                }
                //5000は1枚
                else {
                    Limits[i] = 1;
                }
            }
            return Limits;
        }
        

        public static int[] PayMoneyCalc(int payMoney) {
            int[] i = NumToArray(payMoney);
            return i;
        }
        public static int[] PayMoneyCalc(int payMoney,int[] haveMoney) {
        int[] resultPayMoney = new int[MONEYTYPE.Length];
        resultPayMoney =NumToArray(payMoney);
            
            for (int i = 0; i < resultPayMoney.Length; i++) {
                haveMoney[i] -= resultPayMoney[i];
                haveMoney[i] += resultPayMoney[i];
            }
            return resultPayMoney;
        }

        public static int[] NumToArray(int PayMoney) {
            int[] resultPayMoney = new int[MONEYTYPE.Length];
            int reduceNum = PayMoney;

            for (int i = 0; i < MONEYTYPE.Length; i++) {
                
                resultPayMoney[i] = reduceNum / MONEYTYPE[i];
                reduceNum -= resultPayMoney[i] * MONEYTYPE[i];

                if (reduceNum % MONEYTYPE[i] == 0) {
                    break;
                }
            }
            return resultPayMoney;
        }
        public static int[] NumToArrayReduceRemain(int PayMoney) {
            return NumToArrayReduceRemain(PayMoney,MoneyLimits);
        }
        //紙幣から一種類、硬貨から一種類以下で少なくするもしくは硬貨から二種類以下

        //一番最初に紙幣のみで計算、次に紙幣と硬貨の組み合わせ、最後に硬貨のみ
        public static int[] NumToArrayReduceRemain(int PayMoney, int[] Limits) {
            int[] resultPayMoney = new int[MONEYTYPE.Length];
            int[] rNums = new int[MONEYTYPE.Length];
            int remainNum = -PayMoney;
            int rCount = MONEYTYPE.Length;
            int rSum = MONEYTYPE.Length * Limits.Sum();

            int index2nd;
            int valueRCount;
            int valueRSum;

            //何の種類かのindex
            for (int i = 0; i < MONEYTYPE.Length; i++) {//限界値まで１ずつ増やして計算
                //何枚かのindex
                for (int j = 1; j < Limits[i]; j++) {

                    resultPayMoney[i] = j;
                    //支払額に今出す額を足す(残りの金額がRemainNumに入る)
                    remainNum += MONEYTYPE[i] * j;

                    //お釣りは出ない場合の判定
                    if (remainNum == 0) {
                        resultPayMoney.CopyTo(rNums, 0);
                        return rNums;
                    }
                    
                    if (remainNum < MONEYTYPE[i]) {
                        //1種類目でお釣りが出る場合の判定
                        if (remainNum > 0) {
                            valueRCount = RemainCount(remainNum);
                            valueRSum = Calculator.NumToArray(remainNum).Sum();
                            //出たお釣りが今までで一番少ない場合の判定
                            if (valueRCount <= rCount) {
                                if (valueRCount < rCount || (valueRCount == rCount && valueRSum < rSum)) {
                                    resultPayMoney.CopyTo(rNums, 0);
                                    rCount = valueRCount;
                                    rSum = valueRSum;
                                }
                            }
                        }
                        if (COININDEX < i) {
                            index2nd = i;
                        }
                        else {
                            index2nd = COININDEX;
                        }
                        for (int k = index2nd; k < MONEYTYPE.Length; k++) {
                            for (int l = 1; l < Limits[k]; l++) {
                                
                                resultPayMoney[k] = l;
                                //支払額に今出す額を足す(残りの金額がRemainNumに入る)
                                remainNum += MONEYTYPE[k] * l;

                                //お釣りは出ない場合の判定
                                if (remainNum == 0) {
                                    
                                    resultPayMoney.CopyTo(rNums, 0);
                                    return rNums;

                                }

                                if (remainNum < MONEYTYPE[k]) {
                                    //お釣りが出る場合の判定
                                    if (remainNum > 0) {
                                        valueRCount = RemainCount(remainNum);
                                        valueRSum = Calculator.NumToArray(remainNum).Sum();
                                        //出たお釣りが今までで一番少ない場合の判定
                                        if (valueRCount <= rCount) {
                                            if (valueRCount < rCount || (valueRCount == rCount && valueRSum < rSum)) {
                                                resultPayMoney.CopyTo(rNums, 0);
                                                rCount = valueRCount;
                                                rSum = valueRSum;
                                            }
                                        }
                                    }

                                }
                                if (remainNum > MONEYTYPE[k]) {
                                    remainNum = -PayMoney + MONEYTYPE[i] * j;
                                    resultPayMoney[k] = 0;
                                    break;
                                }
                                remainNum = -PayMoney + MONEYTYPE[i] * j;
                                resultPayMoney[k] = 0;
                            }
                        }

                    }
                    if (remainNum > MONEYTYPE[i]) {
                        remainNum = -PayMoney;
                        resultPayMoney[i] = 0;
                        break;
                    }
                    remainNum = -PayMoney;
                    resultPayMoney[i] = 0;
                }
            }
            return rNums;
        }
        /// <summary>
        /// 数字(お釣り)を受け取って金銭の種類の数を返す
        /// </summary>
        /// <param name="remainNum"></param>
        /// <returns></returns>
        private static int RemainCount(int remainNum) {
            return (Calculator.NumToArray(remainNum)).Count(x => x != 0);
        }

        //支払い枚数を自分で入力して計算する(帰ってくるのはお釣り)
        public static int[] InputMoneyToReduce(int payMoney,int[] payMoneys) {
            return payMoneys;
        }

        public static int ArrayToNum(int[] moneyNums) {
            int sum = 0;
            for (int i = 0; i < MONEYTYPE.Length; i++) {
                sum += moneyNums[i] * MONEYTYPE[i];
            }
            return sum;
        }
    }
}
