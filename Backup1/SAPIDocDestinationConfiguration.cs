using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using SAP.Middleware;
using SAP.Middleware.Connector;

//using Microsoft.Samples.BizTalk.Adapters.BaseAdapter;

namespace Clarks.SAP.Connector
{
    /// <summary>
    /// Encapsulates SAP Destination Configuration management. This is a singleton process.
    /// </summary>
    public class SAPIDocDestinationConfiguration :  IDestinationConfiguration
    {
        private string _destinationName = string.Empty;

        // holds all configured destinations
        static Dictionary<string, RfcConfigParameters> availableDestinations=null;

        // event fired when configuration is changed
        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="locationConfigDom">Configuration connection parameters</param>
        public SAPIDocDestinationConfiguration(String destinationName,XmlDocument locationConfigDom)
        {
            if (availableDestinations==null)
                availableDestinations = new Dictionary<string, RfcConfigParameters>();
            
            _destinationName = destinationName;
            UpdateParameters(locationConfigDom);
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="locationConfigDom">Configuration connection parameters</param>
        public SAPIDocDestinationConfiguration(String destinationName)
        {
            availableDestinations = new Dictionary<string, RfcConfigParameters>();
            AddParameters(destinationName);
        }

        /// <summary>
        /// Overloaded method called by constructor to add
        /// hard0-coded values to RfcConfigparams
        /// </summary>
        /// <param name="destinationName">The destination name used in repositiry</param>
        /// <returns>The collection of RFC parameters</returns>
        private void AddParameters(String destinationName)
        {
            if ("SAP_IDOC_DESTINATION_CONFIG".Equals(destinationName))
            {
                RfcConfigParameters parameters = new RfcConfigParameters();
                parameters.Add(RfcConfigParameters.AppServerHost, "10.2.17.78");
                parameters.Add(RfcConfigParameters.Client, "120");
                parameters.Add(RfcConfigParameters.Language, "EN");
                parameters.Add(RfcConfigParameters.MaxPoolSize, "10");
                parameters.Add(RfcConfigParameters.MaxPoolWaitTime, "300");
                parameters.Add(RfcConfigParameters.MessageServerHost, "10.2.17.78");
                parameters.Add(RfcConfigParameters.SystemID, "DR3");
                parameters.Add(RfcConfigParameters.SystemNumber, "00");

                parameters.Add(RfcConfigParameters.User, "biztalk");
                parameters.Add(RfcConfigParameters.Password, "klatzib1");
                        
                RfcConfigParameters existingConfiguration;

                //if a destination of that name existed before, we need to fire a change event
                if (availableDestinations.TryGetValue(destinationName, out existingConfiguration))
                {
                    availableDestinations[destinationName] = parameters;
                    RfcConfigurationEventArgs eventArgs = new RfcConfigurationEventArgs(RfcConfigParameters.EventType.CHANGED, parameters);
                    
                    System.Diagnostics.Trace.WriteLine("Fire change event " + eventArgs.ToString() + " for destination " + destinationName);

                    // fire changed event
                    if (ConfigurationChanged != null)
                        ConfigurationChanged(destinationName, eventArgs);
                }
                else
                {
                    availableDestinations[destinationName] = parameters;
                }
             
            }
           
        }


        /// <summary>
        /// Called by constructor, adds Rfc params contained in
        /// Xml document.
        /// </summary>
        /// <param name="destinationName">The destination name used in repositiry</param>
        /// <returns>The collection of RFC parameters</returns>
        public void UpdateParameters(XmlDocument configDOM)
        {
           
            RfcConfigParameters parameters = new RfcConfigParameters();

            parameters.Add(RfcConfigParameters.AppServerHost, SAPIDocUtils.Extract(configDOM, "/Config/appServerHost").ToString());
            parameters.Add(RfcConfigParameters.Client, SAPIDocUtils.Extract(configDOM, "/Config/client").ToString());
            parameters.Add(RfcConfigParameters.Language, SAPIDocUtils.Extract(configDOM, "/Config/language").ToString());
            parameters.Add(RfcConfigParameters.MaxPoolSize, SAPIDocUtils.Extract(configDOM, "/Config/maxPoolSize").ToString());
            parameters.Add(RfcConfigParameters.MaxPoolWaitTime, SAPIDocUtils.Extract(configDOM, "/Config/maxPoolSizeWaitTime").ToString());
            parameters.Add(RfcConfigParameters.MessageServerHost, SAPIDocUtils.Extract(configDOM, "/Config/messageServerHost").ToString());
            //parameters.Add(RfcConfigParameters.PoolSize, SAPIDocUtils.Extract(configDOM, "/Config/poolSize"));
            parameters.Add(RfcConfigParameters.SystemID, SAPIDocUtils.Extract(configDOM, "/Config/systemID").ToString());
            parameters.Add(RfcConfigParameters.SystemNumber, SAPIDocUtils.Extract(configDOM, "/Config/systemNumber").ToString());

            parameters.Add(RfcConfigParameters.User, SAPIDocUtils.Extract(configDOM, "/Config/username"));
            parameters.Add(RfcConfigParameters.Password, SAPIDocUtils.Extract(configDOM, "/Config/password"));
            
            parameters.Add(RfcConfigParameters.RepositoryDestination,_destinationName);

            RfcConfigParameters existingConfiguration;
      
            //if a destination of that name existed before, we need to fire a change event
            if (availableDestinations.TryGetValue(_destinationName, out existingConfiguration))
            {
                availableDestinations[_destinationName] = parameters;
                RfcConfigurationEventArgs eventArgs = new RfcConfigurationEventArgs(RfcConfigParameters.EventType.CHANGED, parameters);
                
                System.Diagnostics.Trace.WriteLine("Fire change event " + eventArgs.ToString() + " for destination " + _destinationName);
                
                // fire changed event
                if (ConfigurationChanged != null)
                    ConfigurationChanged(_destinationName, eventArgs);
            }
            else
            {
                availableDestinations[_destinationName] = parameters;
            }

        }
        /// <summary>
        /// Implemented from IDestinationConfiguration,returns populated 
        /// Rfc parameters collection to RFC destination manager
        /// </summary>
        /// <param name="destinationName">The destination name used in repositiry</param>
        /// <returns>The collection of RFC parameters</returns>
        public RfcConfigParameters GetParameters(string destinationName)
        {
            RfcConfigParameters foundDestination;

            if (availableDestinations != null)
            {
                availableDestinations.TryGetValue(destinationName, out foundDestination);

                if (foundDestination == null)
                    throw new ApplicationException(string.Format("Destination:{0} do not exist in Repository", destinationName));
            }
            else
            {
                throw new ApplicationException(string.Format("Destination:{0} do not exist in Repository",destinationName));
            }
            
            return foundDestination;
        }


        /// <summary>
        /// Informs RFC Destination Manager that class supprts changes
        /// to Rfc configuration parameters 
        /// </summary>
        /// <returns></returns>
        public bool ChangeEventsSupported()
        {
            return true;
        }


    }
   
}
