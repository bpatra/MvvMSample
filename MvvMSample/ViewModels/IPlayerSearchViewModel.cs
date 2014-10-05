using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvMSample.ViewModels
{
    public interface IPlayerSearchViewModel
    {
        string SearchPlayerText { get; set; }

        ICollection<IPlayer> DisplayedPlayers { get; } 
    }
}
