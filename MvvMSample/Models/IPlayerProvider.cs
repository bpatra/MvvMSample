using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvMSample.Models
{
    public interface IPlayerProvider
    {
        List<IPlayer> GetAllWorldCupPlayer();
    }
}
