using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettlementPlanApp
{
    // this code is mostly copied from online examples
    class CashFlowCalculator
    {
        int participantCount;

        int getMinIndex(double[] arr)
        {
            int minInd = 0;
            for (int i = 1; i < participantCount; i++)
                if (arr[i] < arr[minInd])
                    minInd = i;
            return minInd;
        }

        int getMaxIndex(double[] arr)
        {
            int maxInd = 0;
            for (int i = 1; i < participantCount; i++)
                if (arr[i] > arr[maxInd])
                    maxInd = i;
            return maxInd;
        }

        void CalculateCashFlowImpl(double[] amount, double[,] result)
        {
            int mxCreditIndex = getMaxIndex(amount), mxDebitIndex = getMinIndex(amount);

            if (amount[mxCreditIndex] == 0 &&
                amount[mxDebitIndex] == 0)
                return;

            double min = Math.Min(-amount[mxDebitIndex], amount[mxCreditIndex]);
            amount[mxCreditIndex] -= min;
            amount[mxDebitIndex] += min;

            result[mxDebitIndex, mxCreditIndex] = min; //person mxDebitIndex pays person mxCreditIndex amount min
            CalculateCashFlowImpl(amount, result);
        }

        // debts array represents participants and debts
        // debts[i][j] so that person in index i owns money to person in index j
        public double[,] CalculateCashFlow(double[,] debts, int count)
        {
            participantCount = count;
            var amount = new double[participantCount];

            for (int p = 0; p < participantCount; p++)
                for (int i = 0; i < participantCount; i++)
                    amount[p] += (debts[i, p] - debts[p, i]);

            var result = new double[count,count];
            CalculateCashFlowImpl(amount, result);
            return result;
        }
    }
}
