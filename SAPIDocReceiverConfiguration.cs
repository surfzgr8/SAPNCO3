using System;
using System.Collections.Generic;
using System.Text;

using SAP.Middleware;
using SAP.Middleware.Connector;

namespace Clarks.SAP.Connector
{
    /// <summary>
    /// Encapsulates connection functionality using NCO 3.
    /// </summary>
    public class SAPIDocReceiveConfiguration : IServerConfiguration
    {

        public RfcConfigParameters GetParameters(String serverName)
        {
            if ("SQLCONDA_SERVER_CONFIG".Equals(serverName))
            {
                RfcConfigParameters parms = new RfcConfigParameters();
                parms.Add(RfcConfigParameters.RepositoryDestination, "SAP_CLIENT_CONFIG");
                parms.Add(RfcConfigParameters.GatewayHost, "10.2.17.78");
                parms.Add(RfcConfigParameters.GatewayService, "3300");
                parms.Add(RfcConfigParameters.ProgramID, "SQLARTMAS");
                parms.Add(RfcConfigParameters.RegistrationCount, "5");

                return parms;
            }
            else return null;
        }

        // The following code is not used in this example
        public bool ChangeEventsSupported()
        {
            return false;
        }

        public event RfcServerManager.ConfigurationChangeHandler ConfigurationChanged;
    }
}
