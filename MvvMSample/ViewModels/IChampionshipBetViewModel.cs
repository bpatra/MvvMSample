using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvMSample.Models;

namespace MvvMSample.ViewModels
{
    public interface IChampionshipBetViewModel
    {
        ObservableCollection<IFootballClub> FootballClubs { get; }
    }
}
