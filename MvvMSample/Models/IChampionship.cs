using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvMSample.Models
{
    public interface IChampionship
    {
        IList<IFootballClub> CurrentChampionShipRanking { get; }

        List<IFootballClub> UserBet { get; set; }

        int GetGoodBetCount();
    }
}
