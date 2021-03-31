using System.Linq;
using System.Collections.Generic;

namespace Routing
{
    /// <summary>
    /// Represents a route
    /// </summary>
    public class Route : List<string>
    {
        /// <summary>
        /// Constructs an instance of a route.
        /// </summary>
        /// <param name="collection"></param>
        public Route(IEnumerable<string> collection) : base(collection)
        {
        }

        /// <summary>
        /// Replaces node with a sub route.
        /// </summary>
        /// <param name="node">node to search for.</param>
        /// <param name="route">route to replace the node with.</param>
        public void Replace(string node, Route route)
        {
            var index = IndexOf(node);
            Remove(node);
            InsertRange(index, route);
        }

        /// <summary>
        /// Checks for circular dependency.
        /// </summary>
        public void CheckForCircularReference()
        {
            if (this.GroupBy(i => i).Any(g => g.Count() > 1))
                throw new System.Exception("Circular dependency detected");
        }
    }
}
