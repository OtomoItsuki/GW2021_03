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
        public readonly static int[] MoneyLimits = setLimits();
        public readonly static int[] PAYMONEYLIMITS = new int[] {127,127,127,COINILIMIT,COINILIMIT,COINILIMIT,COINILIMIT,COINILIMIT,COINILIMIT, };
        public readonly static int[] limits = Enumerable.Repeat<int>(127, Calculator.MONEYTYPE.Length).ToArray();

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

        /// <summary>
        /// 配列を返すための支払うときの種類ごとに数値を格納する配列
        /// </summary>
        private static int[] resultPayMoney = new int[MONEYTYPE.Length];
        private static int valueRemain; 


    public static int[] PayMoneyCalc(int payMoney) {
            int[] i = NumToArray(payMoney);
            return i;
        }
        public static int[] PayMoneyCalc(int payMoney,int[] haveMoney) {
            resultPayMoney =NumToArray(payMoney);
            
            for (int i = 0; i < resultPayMoney.Length; i++) {
                haveMoney[i] -= resultPayMoney[i];
                haveMoney[i] += resultPayMoney[i];
            }
            return resultPayMoney;
        }

        private static int[] NumToArray(int reduceNum) {

            
            for (int i = 0; i < MONEYTYPE.Length; i++) {
                
                resultPayMoney[i] = reduceNum / MONEYTYPE[i];
                valueRemain = reduceNum % MONEYTYPE[i];
                if (valueRemain == 0) {
                    break;
                }
            }
            return resultPayMoney;
        }
        private static int[] NumToArrayReduceRemain(int reduceNum) {
            return NumToArrayReduceRemain(reduceNum,MoneyLimits);
        }
        //紙幣から一種類、硬貨から一種類以下で少なくするもしくは硬貨から二種類以下

        //一番最初に紙幣のみで計算、次に紙幣と硬貨の組み合わせ、最後に硬貨のみ
        private static int[] NumToArrayReduceRemain(int reduceNum,int[] Limits) {
            int[] rNums = null;
            int remainNum = reduceNum;
            int rCount = MONEYTYPE.Length;

            int valueRCount = 0;

            for (int i = 0; i < COININDEX; i++) {
                //このif文の中の処理を実行したときお釣りは出ない
                if (remainNum % MONEYTYPE[i] == 0) {
                    resultPayMoney[i] = remainNum / MONEYTYPE[i];
                    return resultPayMoney;
                }
                //限界値まで１ずつ増やして計算
                for (int j = 0; j < Limits[i]; j++) {
                    resultPayMoney[i] = j;
                    remainNum -= MONEYTYPE[i] * j;

                    //お釣りとして出てくる額が今計算している桁を越したらtrue
                    if (remainNum / MONEYTYPE[i] <= 1) {
                        if (remainNum < 0) {
                            VerifyMostNum(resultPayMoney, ref rNums, ref rCount, valueRCount);
                        }
                        for (int k = i + 1; k < MONEYTYPE.Length; k++) {
                            if (remainNum % MONEYTYPE[k] == 0) {
                                resultPayMoney[k] = remainNum / MONEYTYPE[k];
                                return resultPayMoney;
                            }
                            for (int l = 0; l < Limits[k]; l++) {
                                if (MONEYTYPE[k] * Limits[k] < remainNum) {
                                    resultPayMoney[k] = l;
                                    remainNum -= MONEYTYPE[k] *l;
                                    VerifyMostNum(resultPayMoney, ref rNums, ref rCount, valueRCount);
                                }
                            }
                        }

                    }
                }
                valueRCount = NumToArray(-remainNum).Count(x => x > 0);
                //現在の最高のおつり枚数が現在のおつりと比べて多いとき、支払いに用いる枚数、お釣りの枚数上書き
                VerifyMostNum(resultPayMoney, ref rNums, ref rCount, valueRCount);
                resultPayMoney.Initialize();

            }


            //１００００円(要素[0]と組み合わせた要素[3]以降(硬貨)のなかで一番お釣りの枚数が少なくなる)

            if (resultPayMoney.Count(i => i != 0) == 2) {
                valueRCount = NumToArray(-remainNum).Count(x => x > 0);
                //現在の最高のおつり枚数が現在のおつりと比べて多いとき、支払いに用いる枚数、お釣りの合計金額、お釣りの枚数上書き
                if (rCount > valueRCount) {
                    rNums = resultPayMoney;
                    rCount = valueRCount;
                }
                else if (rCount == valueRCount && resultPayMoney.Sum() < rNums.Sum()) {
                    rNums = resultPayMoney;
                    rCount = valueRCount;
                }
                resultPayMoney.Initialize();

            }
            return resultPayMoney;
        }
        //支払い枚数を自分で入力して計算する(帰ってくるのはお釣り)
        public static int[] InputMoneyToReduce(int payMoney,int[] payMoneys) {
            return payMoneys;
        }

        private static void VerifyMostNum(int[] mNums, ref int[] rNums, ref int rCount, int valueRCount) {
            if (rCount > valueRCount) {
                rNums = mNums;
                rCount = valueRCount;
            }
            else if (rCount == valueRCount && mNums.Sum() < rNums.Sum()) {
                rNums = mNums;
                rCount = valueRCount;
            }
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
