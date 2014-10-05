using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvMSample
{
    public interface IPlayer
    {
        string NationalTeam { get; }

        string Name { get; }

        string Age { get; }

        string Club { get;}

        string Championship { get; }
    }
}
 