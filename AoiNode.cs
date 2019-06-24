using System.Collections.Generic;
using System.Numerics;

namespace AOI
{
    public class AoiNode
    {
        public long Id;

        public Vector3 Position;

        public AoiInfo AoiInfo;

        public AoiLink Link;
        
        public AoiNode(){}

        public AoiNode Init(long id, float x, float y,float z)
        {
            Id = id;

            Position = new Vector3(x, y,z);

            if (AoiInfo.MovesSet == null) AoiInfo.MovesSet = new HashSet<long>();

            if (AoiInfo.MoveOnlySet == null) AoiInfo.MoveOnlySet = new HashSet<long>();

            if (AoiInfo.EntersSet == null) AoiInfo.EntersSet = new HashSet<long>();
            
            if (AoiInfo.LeavesSet == null) AoiInfo.LeavesSet = new HashSet<long>();

            return this;
        }

        public void SetPosition(float x, float y,float z)
        {
            Position.X = x;

            Position.Y = y;
            
            Position.Z = z;
        }

        public void Dispose()
        {
            Id = 0;
            
            AoiPool.Instance.Recycle(Link.XNode);
            
            AoiPool.Instance.Recycle(Link.YNode);
            
            AoiPool.Instance.Recycle(Link.ZNode);
            
            Link.XNode = null;

            Link.YNode = null;
            
            Link.ZNode = null;
            
            AoiInfo.MovesSet.Clear();
            
            AoiInfo.MoveOnlySet.Clear();
            
            AoiPool.Instance.Recycle(this);
        }
    }
}