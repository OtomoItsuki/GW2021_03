using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayControl {
    public static class Calculator {
        readonly public static int[] MONEYTYPE = new int[] { 10000, 5000, 1000, 500, 100, 50, 10, 5, 1 };
        readonly public static int COININDEX = 3;
        public static int[] PayMoneyCalc(int payMoney) {
            int[] i = NumToArray(payMoney);
            return i;
        }
        public static int[] PayMoneyCalc(int payMoney,int[] haveMoney) {
            int[] i = { 0, 0 };
            return i;
        }

        private static int[] NumToArray(int reduceNum) {
            int[] mNums = new int[MONEYTYPE.Length];
            
            for (int i = 0; i < MONEYTYPE.Length; i++) {
                
                
                mNums[i] = reduceNum / MONEYTYPE[i];
                reduceNum = reduceNum % MONEYTYPE[i];

            }
            return mNums;
        }
        //紙幣から一種類、硬貨から一種類以下で少なくするもしくは硬貨から二種類以下
        private static int[] NumToArrayReduce(int reduceNum) {
            int[] mNums = new int[MONEYTYPE.Length];
            int[] rNums = null;
            int remainNum = reduceNum;
            int rSum = 10 * MONEYTYPE.Length;
            int rCount = MONEYTYPE.Length;
            
            int paperIndex =0;
            int valueRSum = 0;
            int valueRCount = 0;
            int index = 0;
            do {
                //このif文の中の処理を実行したときお釣りは出ない
                if (remainNum % MONEYTYPE[index] == 0) {
                    mNums[index] = remainNum / MONEYTYPE[index];
                    return mNums;
                }
                //このif文は紙幣は使わない、もしくは紙幣の入力がすんでいるときに処理部分に入る　ここ以降の処理では必ずお釣りが出る
                if (paperIndex >= COININDEX || mNums.Count(x=> x > 0) > 0) {
                    if (remainNum / MONEYTYPE[index] <= 2 && index % 2 == 0) {
                        if (remainNum > 0) {
                            mNums[index] = (remainNum / MONEYTYPE[index]) + 1;
                            index++;
                        }
                        else {

                        }
                    }
                    else if (true) {

                    }
                }
                else {
                    if (remainNum > MONEYTYPE[index]) {
                        mNums[index]++;
                        remainNum -= MONEYTYPE[index];
                    }
                    else {
                        mNums[index] = (remainNum / MONEYTYPE[index]) + 1;
                        remainNum -= MONEYTYPE[index] * mNums[index];
                        
                    }
                    index++;
                }
                //１００００円(要素[0]と組み合わせた要素[3]以降(硬貨)のなかで一番お釣りの枚数が少なくなる)

                

                if (mNums.Count(i => i != 0) == 2) {
                    valueRSum = NumToArray(-remainNum).Sum();
                    valueRCount = NumToArray(-remainNum).Sum();
                    //現在の最高のおつり枚数が現在のおつりと比べて多いとき上書き
                    if (rSum > valueRSum) {
                        rSum = valueRSum;
                        rNums = mNums;
                        rCount = valueRCount;
                    }
                    else if (rSum == valueRSum && rCount > valueRCount) {
                        rNums = mNums;
                        rCount = valueRCount;
                    }
                    mNums.Initialize();
                    
                }
                if (index == MONEYTYPE.Length && paperIndex <= 3) {
                    index = 0;
                    paperIndex++;
                }
                if (true) {

                }
            } while (index == MONEYTYPE.Length);
            return rNums;
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
