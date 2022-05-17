
using Sat.Recruitment.Core.Entities;
using System;

namespace Sat.Recruitment.Core.Strategies
{
    public class NormalBonusStrategy : IBonusStrategy
    {
        const decimal minimumThreshold = 10;
        const decimal BaseMoney = 100;

        public decimal CalculateBonus(User u)
        {
            decimal gif = 0;
            if (u.Money > BaseMoney)
            {
                var percentage = Convert.ToDecimal(0.12);
                //If new user is normal and has more than USD100
                gif = u.Money * percentage;
            }
            if (u.Money > minimumThreshold && u.Money < BaseMoney) {
                var percentage = Convert.ToDecimal(0.8);
                gif = u.Money * percentage;
            }

            return gif;
        }
    }
}
