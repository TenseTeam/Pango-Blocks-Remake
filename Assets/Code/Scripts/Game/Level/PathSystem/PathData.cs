namespace ProjectPBR.Level.PathSystem.Data
{
    using System.Collections.Generic;

    public struct PathData
    {
        public bool IsGoingToCollide;
        public bool IsGoingToFall;
        public List<Node> Nodes;
        public bool HasReached => !IsGoingToCollide && !IsGoingToFall;
    }
}
