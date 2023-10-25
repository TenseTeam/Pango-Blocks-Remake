namespace ProjectPBR.Level.PathSystem
{
    using System.Collections.Generic;

    public class Path
    {
        public bool IsGoingToCollide;
        public bool IsGoingToFall;
        public List<Node> Nodes;
        public bool HasReached => !IsGoingToCollide && !IsGoingToFall;

        public Path()
        {
            Nodes = new List<Node>();
            IsGoingToCollide = false;
            IsGoingToFall = false;
        }
    }
}
