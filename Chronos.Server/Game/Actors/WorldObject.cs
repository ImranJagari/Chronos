using Chronos.Protocol.Enums;
using Chronos.Protocol.Types.ObjectsType;
using Chronos.Server.Game.World;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Server.Game.Actors
{
    public abstract class WorldObject : IDisposable
    {
        public const int DEFAULT_SCALE = 100;
        protected WorldObject()
        {
            CreationTime = DateTime.Now;
        }

        public abstract ObjectTypeEnum ObjectType { get; }

        public abstract int Id
        {
            get;
            protected set;
        }

        public DateTime CreationTime
        {
            get;
            protected set;
        }

        public Map Map
        {
            get
            {
                return Position.Map;
            }
            protected set
            {
                Position.Map = value;
            }
        }
        public ObjectPosition Position { get; set; }

        public bool IsDeleted
        {
            get;
            protected set;
        }

        public bool IsDisposed
        {
            get;
            protected set;
        }
        public virtual bool CanBeSee(WorldObject byObj) => byObj != null && !IsDeleted && !IsDisposed && byObj.Map != null && byObj.Map == Map && Position.IsInCircle(byObj.Position, 100);

        public virtual bool CanSee(WorldObject obj)
        {
            if (obj == null || obj.IsDeleted || obj.IsDisposed)
                return false;

            return obj.CanBeSee(this);
        }
        public virtual ObjectType GetObjectType(bool me = false)
        {
            return new ObjectType(ObjectType, (uint)Id, 11, 0xFFFFFFFF, Position.X, Position.Y, Position.Z, 0, 0, DEFAULT_SCALE);
        }
        #region IDisposable Members

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            OnDisposed();
        }

        protected virtual void OnDisposed()
        {
            //Position = null;
        }

        #endregion
    }
}