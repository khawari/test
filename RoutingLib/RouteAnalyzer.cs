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
            List<Route> processedRoutes = new List<Route>();
            parsedRoutes.ToList().ForEach(parsedRoute =>
            {
                // route validation check:
                // if the input is "2", "1 -> 2" then "1->2" is invalid as per the requirements.
                if (parsedRoute.Count == 1 && parsedRoutes.Any(i => i != parsedRoute && i.Contains(parsedRoute[0])))
                    throw new Exception("invalid route.");
                
                // if the input is "1->2", "2->"3, "1->3", then "1->3" is invalid per the requirements.
                if (parsedRoutes.Any(i => i != parsedRoute && i[0] == parsedRoute[0]))
                    throw new Exception("invalid route.");

                var processedRoute = processedRoutes.SingleOrDefault(i => i.Contains(parsedRoute[0]));
                if (processedRoute != null)
                {
                    processedRoute.Replace(parsedRoute[0], parsedRoute);
                    processedRoute.CheckForCircularReference();
                }
                //else if (parsedRoute.Count() == 2)
                //{
                //    processedRoute = processedRoutes.SingleOrDefault(i => i.Contains(parsedRoute[1]));
                //    if (processedRoute != null)
                //    {
                //        processedRoute.Replace(parsedRoute[1], parsedRoute);
                //        processedRoute.CheckForCircularReference();
                //    }
                //    else processedRoutes.Add(parsedRoute);
                //}
                else processedRoutes.Add(parsedRoute);
            });

            return processedRoutes.Select(i => string.Join(" -> ", i.ToList())).ToList();
        }
    }
}
