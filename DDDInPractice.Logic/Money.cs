using System;
using System.Collections.Generic;
using System.Text;

namespace DDDInPractice.Logic
{
    public sealed class Money : ValueObject<Money>
    {
        public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);
        public static readonly Money Cent = new Money(1, 0, 0, 0, 0, 0);
        public static readonly Money TenCent = new Money(0, 1, 0, 0, 0, 0);
        public static readonly Money Quarter = new Money(0, 0, 1, 0, 0, 0);
        public static readonly Money Dollar = new Money(0, 0, 0, 1, 0, 0);
        public static readonly Money FiveDollar = new Money(0, 0, 0, 0, 1, 0);
        public static readonly Money TwentyDollar = new Money(0, 0, 0, 0, 0, 1);





        public int OneCentCount { get;  }
        public int TenCentCount { get;  }
        public int QuarterCount { get;  }
        public int OneDollarCount { get;  }
        public int FiveDollarCount { get; }
        public int TwentyDollarCount { get; }

        public decimal Amount =>
                OneCentCount * 0.01m +
                TenCentCount * 0.1m +
                QuarterCount * 0.25m +
                OneDollarCount + 
                FiveDollarCount * 5 +
                TwentyDollarCount * 20;


        public Money(
           int oneCentCount,
           int tenCentCount,
           int quarterCount,
           int oneDollarCount,
           int fiveDollarCount,
           int twentyDollarCount)
        {
            if (oneCentCount < 0 || tenCentCount < 0 || quarterCount < 0 
                || oneDollarCount < 0 || fiveDollarCount < 0 || twentyDollarCount < 0)
            {
                throw new InvalidOperationException();
            }

            OneCentCount += oneCentCount;
            TenCentCount += tenCentCount;
            QuarterCount += quarterCount;
            OneDollarCount += oneDollarCount;
            FiveDollarCount += fiveDollarCount;
            TwentyDollarCount += twentyDollarCount;
        }

        public static Money operator +(Money money1, Money money2)
        {
            Money sum = new Money(
                money1.OneCentCount + money2.OneCentCount,
                money1.TenCentCount + money2.TenCentCount,
                money1.QuarterCount + money2.QuarterCount,
                money1.OneDollarCount + money2.OneDollarCount,
                money1.FiveDollarCount + money2.FiveDollarCount,
                money1.TwentyDollarCount + money2.TwentyDollarCount);

            return sum;
        }

        public static Money operator -(Money money1, Money money2)
        {
            if (money2.Amount > money1.Amount)
            {
                throw new InvalidOperationException();
            }

            return new Money(
                money1.OneCentCount - money2.OneCentCount,
                 money1.TenCentCount - money2.TenCentCount,
                 money1.QuarterCount - money2.QuarterCount,
                 money1.OneDollarCount - money2.OneDollarCount,
                 money1.FiveDollarCount - money2.FiveDollarCount,
                 money1.TwentyDollarCount - money2.TwentyDollarCount);
        }

        protected override bool EqualsCore(Money other)
        {
            return OneCentCount == other.OneCentCount
                && TenCentCount == other.TenCentCount
                && QuarterCount == other.QuarterCount
                && OneDollarCount == other.OneDollarCount
                && FiveDollarCount == other.FiveDollarCount
                && TwentyDollarCount == other.TwentyDollarCount;
        }

        protected override int GetHashCodeCore()
        {
            int hashCode = OneCentCount;
            hashCode = (hashCode * 397) ^ TenCentCount;
            hashCode = (hashCode * 397) ^ QuarterCount;
            hashCode = (hashCode * 397) ^ OneDollarCount;
            hashCode = (hashCode * 397) ^ FiveDollarCount;
            hashCode = (hashCode * 397) ^ TwentyDollarCount;

            return hashCode;
        }
    }
}
