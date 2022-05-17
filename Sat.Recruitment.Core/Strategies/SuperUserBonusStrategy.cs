
using Sat.Recruitment.Core.Entities;
using System;

namespace Sat.Recruitment.Core.Strategies
{
    public class SuperUserBonusStrategy : IBonusStrategy
    {
        const decimal BaseMoney = 100;

        public decimal CalculateBonus(User u)
        {
            decimal gif = 0;
            if (u.Money > BaseMoney)
            {
                var percentage = Convert.ToDecimal(0.20);
                gif = u.Money * percentage;
            }

            return gif;
        }
    }
}
