using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GongSolutions.Wpf.DragDrop;
using MvvMSample.Models;

namespace MvvMSample.ViewModels
{
    public interface IChampionshipBetViewModel : IDropTarget
    {
        ObservableCollection<IFootballClub> FootballClubs { get; }
    }
}
