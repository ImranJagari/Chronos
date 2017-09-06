using Chronos.Server.Game.Actors;
using Chronos.Server.Game.Actors.Context.Characters;
using Chronos.Server.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Server.Game.World
{
    public class Map
    {
        public SimpleClientCollection Clients { get; }
        public List<WorldObject> Objects { get; set; }
        public int SceneId { get; set; }

        public void Enter(WorldObject obj)
        {
            if(obj is Character)
            {
                Clients.Add((obj as Character).Client);
            }
            Objects.Add(obj);
        }
        public void Leave(WorldObject obj)
        {
            if (!Objects.Contains(obj))
                return;

            if (obj is Character)
            {
                Clients.Remove((obj as Character).Client);
            }
            Objects.Add(obj);

        }
    }
}