
using Sat.Recruitment.Core.Entities;
using System;

namespace Sat.Recruitment.Core.Strategies
{
    public class PremiumBonusStrategy : IBonusStrategy
    {
        const decimal BaseMoney = 100;

        public decimal CalculateBonus(User u)
        {
            decimal gif = 0;
            if (u.Money > BaseMoney)
            {
                gif = u.Money * 2;
            }

            return gif;
        }
    }
}
