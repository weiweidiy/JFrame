using System;

namespace JFrame
{

    public class CommonRouteManager :  BaseRunable , IRouter
    {

        public RouteResponse GetHotFixAddress(RouteRequest routeInfo)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnRun(RunableExtraData extraData)
        {
            var routeRequest = (RouteRequest)extraData.Data;

            var routeReponse = GetHotFixAddress(routeRequest);

            NotifyComplete(this);
        }
    }

}

