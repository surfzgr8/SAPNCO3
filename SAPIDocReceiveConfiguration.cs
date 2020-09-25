using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using SAP.Middleware;
using SAP.Middleware.Connector;


namespace Clarks.SAP.Connector
{
    /// <summary>
    /// Encapsulates connection functionality using NCO 3.
    /// </summary>
    public  class SAPIDocReceiveConfiguration : IServerConfiguration
    {

        // holds all server configurations
        Dictionary<string, RfcConfigParameters> availableDestinations = null;
        RfcConfigParameters _parameters = new RfcConfigParameters();

        // event fired when configuration is changed
        public event RfcServerManager.ConfigurationChangeHandler ConfigurationChanged;



        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="locationConfigDom">Configuration connection parameters</param>
        public SAPIDocReceiveConfiguration(string serverName,string destinationName,XmlDocument locationConfigDom)
        {
            if (availableDestinations==null)
                availableDestinations = new Dictionary<string, RfcConfigParameters>();

            UpdateParameters(serverName, destinationName,locationConfigDom);
        }


        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="locationConfigDom">Configuration connection parameters</param>
        public SAPIDocReceiveConfiguration(string serverName,string destinationName,string progId)
        {
            if (availableDestinations == null)
                availableDestinations = new Dictionary<string, RfcConfigParameters>();

            AddParameters(serverName, progId);
        }


        /// <summary>
        /// Implemented from IDestinationConfiguration,returns populated 
        /// Rfc parameters collection to RFC destination manager
        /// </summary>
        /// <param name="destinationName">The destination name used in repositiry</param>
        /// <returns>The collection of RFC parameters</returns>
        public RfcConfigParameters GetParameters(string serverName)
        {
            RfcConfigParameters foundDestination;
           
            if (availableDestinations != null)
            {
                availableDestinations.TryGetValue(serverName, out foundDestination);

                if (foundDestination==null)
                    throw new ApplicationException(string.Format("Server:{0} parameters do not exist in repository", serverName));
            }
            else
            {
                throw new ApplicationException(string.Format("Server:{0} parameters do not exist in repository",serverName));
            }

            //return foundDestination;
            RfcConfigParameters rfcConfigParameters = availableDestinations[serverName];

            return rfcConfigParameters;
        }

        /// <summary>
        /// Called from constructor
        /// </summary>
        /// <param name="locationConfigDom">Xml containing parameters, passed by BizTalk</param>
        public void UpdateParameters(string serverName, string destinationName, XmlDocument configDOM)
        {
     

            RfcConfigParameters parameters = new RfcConfigParameters();

            parameters.Add(RfcConfigParameters.RepositoryDestination, destinationName);

            parameters.Add(RfcConfigParameters.Name, serverName);

            parameters.Add(RfcConfigParameters.GatewayHost, SAPIDocUtils.Extract(configDOM, "/Config/gatewayHost").ToString());
            parameters.Add(RfcConfigParameters.GatewayService, SAPIDocUtils.Extract(configDOM, "/Config/gatewayService").ToString());
            parameters.Add(RfcConfigParameters.ProgramID, SAPIDocUtils.Extract(configDOM, "/Config/programID").ToString());
            parameters.Add(RfcConfigParameters.RegistrationCount, SAPIDocUtils.Extract(configDOM, "/Config/registrationCount").ToString());
            parameters.Add(RfcConfigParameters.Trace,SAPIDocUtils.ExtractUInt(configDOM, "/Config/rfcTrace").ToString());
         

            RfcConfigParameters existingConfiguration;

            //if a destination of that name existed before, we need to fire a change event
            if (availableDestinations.TryGetValue(serverName, out existingConfiguration))
            {
                availableDestinations[serverName] = parameters;

                RfcConfigurationEventArgs eventArgs = new RfcConfigurationEventArgs(RfcConfigParameters.EventType.CHANGED, parameters);

                System.Diagnostics.Trace.WriteLine("Fire change event " + eventArgs.ToString() + " for destination " + serverName);

                // fire changed event
                if (ConfigurationChanged != null)
                    ConfigurationChanged(serverName, eventArgs);
            }
            else
            {
                availableDestinations[serverName] = parameters;
            }
            
           
        }

        /// <summary>
        /// Called by constructor
        /// </summary>
        /// <param name="serverName"></param>
        public  void AddParameters(string serverName,string progId)//, string destinationName)
        {

            // temp hard-coded for prototyping
            string destinationName = "SAP_IDOC_DESTINATION_CONFIG";

  
            RfcConfigParameters parameters = new RfcConfigParameters();
            parameters.Add(RfcConfigParameters.Name, serverName);
            parameters.Add(RfcConfigParameters.RepositoryDestination, destinationName);
            parameters.Add(RfcConfigParameters.GatewayHost, "10.2.17.78");
            parameters.Add(RfcConfigParameters.GatewayService, "3300");
            parameters.Add(RfcConfigParameters.ProgramID, progId);
            parameters.Add(RfcConfigParameters.RegistrationCount, "1");

            RfcConfigParameters existingConfiguration;


            //if a destination of that name existed before, we need to fire a change event
            if (availableDestinations.TryGetValue(serverName, out existingConfiguration))
            {
                availableDestinations[serverName] = parameters;

                RfcConfigurationEventArgs eventArgs= new RfcConfigurationEventArgs(RfcConfigParameters.EventType.CHANGED, _parameters);

                System.Diagnostics.Trace.WriteLine("Fire change event " + eventArgs.ToString() + " for destination " + serverName);


                // fire changed event
                if (ConfigurationChanged != null)
                    ConfigurationChanged(serverName, eventArgs);
            }
            else
            {
                availableDestinations[serverName] = parameters;
            }

           
        }

        // The following code is not used in this example
        public bool ChangeEventsSupported()
        {
            return true;
        }

    }
}
