using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvMSample.Models
{
    public class PlayerProvider : IPlayerProvider
    {
        private const string path = @"D:\MvvMSample\MvvMSample\DBPlayerFrenchWC2014.csv";
        private List<IPlayer> _list;
        public PlayerProvider()
        {
            
        }

        private class Player : IPlayer 
        {
            public Player(string[] line)
            {
                NationalTeam = line[0];
                Name = line[1];
                Age = line[2];
                Club = line[4];
                Championship = line[3];
            }

            public string NationalTeam { get; set; }
            public string Name { get; set; }
            public string Age { get; set; }
            public string Club { get; set; }
            public string Championship { get; set; }
        }
           

        public List<IPlayer> GetAllWorldCupPlayer()
        {
            if (_list != null) return _list;
            if(!File.Exists(path)) throw new FileNotFoundException();
            _list = new List<IPlayer>();
            string[] lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                _list.Add(new Player(line.Split(',')));
            }

            return _list;

        }
    }
}
