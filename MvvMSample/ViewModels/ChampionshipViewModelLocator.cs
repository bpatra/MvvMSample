using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvMSample.Models;

namespace MvvMSample.ViewModels
{
    public class ChampionshipViewModelLocator
    {
        public static IChampionshipBetViewModel ChampionshipBetViewModel { get { return new ChampionshipBetViewModel(new MyChampionship()); } }

        private class MyChampionship : IChampionship
        {
            public List<IFootballClub> CurrentChampionShipRanking { get{throw  new NotImplementedException();}}
            public int GetGoodBetCount()
            {
                throw new NotImplementedException();
            }

            public List<IFootballClub> UserBet
            {
                get
                {
                    var clubs = new IFootballClub[]
                        {
                            new FootballClub("Fc Nantes", "Les Canaris", 1943),
                            new FootballClub("Paris Saint Germain", "Les Parisiens", 1970),
                            new FootballClub("Olympique de Marseille", "Les Olympiens", 1899),
                            new FootballClub("AS Saint-Étienne", "Les Verts", 1919),
                            new FootballClub("Olympique Lyonnais", "Les Gones", 1950),
                            new FootballClub("Girondins de Bordaux", "les Girondins", 1919),
                        };

                    return new List<IFootballClub>(clubs);

                }
                set { throw new NotImplementedException(); }
            }

            private class FootballClub : IFootballClub
            {
                public FootballClub(string name, string nickName, int creationYear)
                {
                    FullName = name;
                    NickName = nickName;
                    CreationYear = creationYear;
                }

                public string FullName { get; private set; }
                public string NickName { get; private set; }
                public int CreationYear { get; private set; }
            }
        }
    }
}
