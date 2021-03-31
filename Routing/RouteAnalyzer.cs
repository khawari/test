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
            var parsedRoutes = routes.Select(i => new Route(i.Split(" -> ")));
            List<Route> processedRoutes = new List<Route>();
            parsedRoutes.ToList().ForEach(parsedRoute =>
            {
                var processedRoute = processedRoutes.FirstOrDefault(i => i.Contains(parsedRoute[0]));
                if (processedRoute != null)
                {
                    processedRoute.Replace(parsedRoute[0], parsedRoute);
                    processedRoute.CheckForCircularReference();
                }
                else if (parsedRoute.Count() == 2)
                {
                    processedRoute = processedRoutes.FirstOrDefault(i => i.Contains(parsedRoute[1]));
                    if (processedRoute != null)
                    {
                        processedRoute.Replace(parsedRoute[1], parsedRoute);
                        processedRoute.CheckForCircularReference();
                    }
                    else processedRoutes.Add(parsedRoute);
                }
                else processedRoutes.Add(parsedRoute);
            });

            return null;
        }
    }
}
