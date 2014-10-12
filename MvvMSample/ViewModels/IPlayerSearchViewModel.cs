using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BonenLawyer;

namespace MvvMSample.ViewModels
{
    public interface IPlayerSearchViewModel
    {
        string SearchPlayerText { get; set; }

        ICollectionView<IPlayer> DisplayedPlayers { get; } 
    }
}
