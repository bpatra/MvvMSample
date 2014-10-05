using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvMSample.ViewModels
{
    public class PlayerSearchViewModel : IPlayerSearchViewModel
    {
        public string SearchPlayerText { get; set; }

        public ICollection<IPlayer> DisplayedPlayers { get; private set; }
    }
}
