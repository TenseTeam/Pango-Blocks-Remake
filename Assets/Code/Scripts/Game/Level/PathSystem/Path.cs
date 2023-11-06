namespace ProjectPBR.Level.PathSystem
{
    using System.Collections.Generic;

    public class Path
    {
        public bool IsGoingToCollide;
        public bool IsGoingToFall;
        public List<Node> Nodes = new List<Node>();
        public bool HasReached => !IsGoingToCollide && !IsGoingToFall;

        public Path()
        {
            IsGoingToCollide = false;
            IsGoingToFall = false;
        }
    }
}
