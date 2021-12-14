using System.Collections.Generic;
using System.Linq;

namespace AoC12
{
    public class Path
    {
        private List<Cave> _path;

        public Path(Cave newNode)
        {
            _path = new List<Cave> {newNode};
        }

        public Path(List<Cave> oldPath, Cave newNode)
        {
            _path = oldPath.ToList();
            _path.Add(newNode);
        }

        public Cave Last()
        {
            return _path.Last();
        }

        public bool IsFinished()
        {
            return "end".Equals(_path.Last().Name);
        }

        public bool CanBeAddedEasy(Cave newCave)
        {
            return newCave.IsBig || !_path.Contains(newCave);
        }

        public bool CanBeAddedComplicated(Cave newCave)
        {
            return newCave.IsBig || !_path.Contains(newCave) || canBeAddedSpecial(newCave);
        }

        private bool canBeAddedSpecial(Cave newCave)
        {
            if ("start".Equals(newCave.Name) || "end".Equals(newCave.Name))
            {
                return false;
            }

            if (_path.Where(cave => !cave.IsBig)
                .GroupBy(cave => cave.Name)
                .Any(group => group.Count() > 1))
            {
                return false;
            }

            return true;
        }

        public Path GetNewPath(Cave newNode)
        {
            return new Path(_path, newNode);
        }
    }
}