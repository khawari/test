using System.Collections.Generic;

namespace Routing
{
    /// <summary>
    /// Provides routing analyzing features.
    /// </summary>
    interface IRouteAnalyzer
    {
        /// <summary>
        /// Given a list of route, it reduces it down to eliminate dependencies.
        /// </summary>
        /// <param name="routes"></param>
        /// <returns></returns>
        IEnumerable<string> Process(IEnumerable<string> routes);
    }
}
