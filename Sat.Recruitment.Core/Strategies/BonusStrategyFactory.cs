using Sat.Recruitment.Core.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Core.Strategies
{
    public class BonusStrategyFactory
    {
        private static IDictionary<string, Func<IBonusStrategy>>
              _strategies = new Dictionary<string, Func<IBonusStrategy>>()
              {
                  { DomainConstants.UserType_Normal, () => new NormalBonusStrategy() },
                  { DomainConstants.UserType_SuperUser, () => new SuperUserBonusStrategy() },
                  { DomainConstants.UserType_Premium, () => new PremiumBonusStrategy() }
              };

        public static IBonusStrategy GetStrategy(string name)
        {
            return _strategies[name]();
        }
    }
}
