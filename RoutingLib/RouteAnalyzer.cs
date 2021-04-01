using System.Linq;
using System.Collections.Generic;
using System;

namespace Routing
{
    /// <inheritdoc/>
    public class RouteAnalyzer : IRouteAnalyzer
    {
        /// <inheritdoc/>
        public IEnumerable<string> Process(IEnumerable<string> routes)
        {
            var parsedRoutes = routes.Select(i => new Route(i.Split(" -> "))).ToList();
            parsedRoutes.ToList().ForEach(parsedRoute =>
            {
                // route validation check:
                // if the input is "2", "1 -> 2", "2 -> 3" then "1->2" or "2 -> 3" is invalid as per the requirements.
                if (parsedRoute.Count == 1 && parsedRoutes.Any(i => i != parsedRoute && i.Contains(parsedRoute[0])))
                    throw new Exception("invalid route.");

                // if the input is "1->2", "2->"3, "1->3", then "1->3" is invalid per the requirements.
                if (parsedRoutes.Any(i => i != parsedRoute && i[0] == parsedRoute[0]))
                    throw new Exception("invalid route.");
            });

            var heads = FindHeads(parsedRoutes);

            return heads.Select(i => string.Join(" -> ", i.ToList())).ToList();
        }

        List<Route> FindHeads(List<Route> routes)
        {
            var heads = new List<Route>();
            var tails = new List<Route>();
            routes.ForEach(r =>
            {
                if (routes.Where(i => i.Count() == 2).Any(i => i[1] == r[0]))
                    tails.Add(new Route(r));
                else heads.Add(new Route(r));
            });
            if (heads.Any() == false)
                throw new Exception("Circular dependency detected");
            if (tails.Any())
            {
                var subHeads = FindHeads(tails);
                subHeads.ForEach(sh =>
                {
                    heads.Where(h => h.Count() > 1 && h[1] == sh[0]).ToList().ForEach(h =>
                    {
                        h.Replace(sh[0], sh);
                    });
                });
            }

            return heads;
        }
    }
}
