using Sat.Recruitment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Core.Strategies
{
    public interface IBonusStrategy
    {
        decimal CalculateBonus(User u);
    }
}
