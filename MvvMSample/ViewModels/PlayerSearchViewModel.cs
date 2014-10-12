using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BonenLawyer;
using MvvMSample.Models;

namespace MvvMSample.ViewModels
{
    public class PlayerSearchViewModel : IPlayerSearchViewModel
    {
        private readonly ICollectionView<IPlayer> _view;
        private string _textsearch;

        public PlayerSearchViewModel(IPlayerProvider playerProvider)
        {
            _view = new MyCollectionViewGeneric<IPlayer>(CollectionViewSource.GetDefaultView(playerProvider.GetAllWorldCupPlayer()));
            _view.Filter += (object item) =>
                {
                    if (_textsearch == null) return true;
                    var itemPl = (IPlayer) item;     
                    return itemPl.Name.Contains(_textsearch) ||
                               itemPl.NationalTeam.Contains(_textsearch) ||
                               itemPl.Club.Contains(_textsearch) ||
                               itemPl.Championship.Contains(_textsearch);
                };
        }

        public string SearchPlayerText 
        {
            get { return _textsearch; }
            set
            {
                _textsearch = value;
                _view.Refresh();
            }
        }

        public ICollectionView<IPlayer> DisplayedPlayers { get { return _view; } }
    }
}
