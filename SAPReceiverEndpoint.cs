using System;
using System.Collections.Generic;
using System.Text;


using SAP.Middleware;
using SAP.Middleware.Connector;

namespace Clarks.SAP.Connector
{
    /// <summary>
    /// Emulates the SAP Receive Endpoint in SAP Adapter
    /// </summary>
    class SAPReceiverEndpoint
    {
        private RfcDestination _rfcDestination = null;

        public void RegisterDestination()
        {
            RfcDestinationManager.RegisterDestinationConfiguration(new SAPIDocDestinationConfiguration());//1
            _rfcDestination = RfcDestinationManager.GetDestination("SAP_IDOC_CONFIG");
        }

        /// <summary>
        /// Emuulates SAP Adapter Receive Endpoint
        /// </summary>
        public void Initialise()
        {

            //RfcDestinationManager.RegisterDestinationConfiguration(new MyClientConfig());//1
            RfcServerManager.RegisterServerConfiguration(new SAPIDocReceiveConfiguration());//2

            Type[] handlers = new Type[1] { typeof(SAPIDocReceiveHandler) };//3
            RfcServer server = RfcServerManager.GetServer("SQLCONDA_SERVER_CONFIG", handlers);//3

            server.RfcServerError += OnRfcServerError;
            server.RfcServerApplicationError += OnRfcServerError;

            //register TID specific handler
            server.TransactionIDHandler = new SAPTIDHandler();

            //server.TransactionIDHandler = new MyServerHandler();
            server.Start();//4

            Console.WriteLine("Server has been started. Press X to exit.\n");

            while (true)
            {
                if (Console.ReadLine().Equals("X")) break;
            }
            server.Shutdown(true); //Shuts down 

        }


        public void OnRfcServerError(Object server, RfcServerErrorEventArgs errorEventData)
        {
            RfcServer rfcServer = server as RfcServer;
            RfcServerApplicationException appEx = errorEventData.Error as RfcServerApplicationException;

            if (appEx != null)
            {
                Console.WriteLine("RfcServerApplicationError occured in RFC server {0} :", rfcServer.Name);
            }
            else
                Console.WriteLine("RfcServerError occured in RFC server {0} :", rfcServer.Name);


            if (errorEventData.ServerContextInfo != null)
            {
                Console.WriteLine("RFC Caller System ID: {0} ", errorEventData.ServerContextInfo.SystemAttributes.SystemID);
                Console.WriteLine("RFC function Name: {0} ", errorEventData.ServerContextInfo.FunctionName);
            }

            Console.WriteLine("Error type: {0}", errorEventData.Error.GetType().Name);
            Console.WriteLine("Error message: {0}", errorEventData.Error.Message);

            if (appEx != null)
            {
                Console.WriteLine("Inner exception type: {0}", appEx.InnerException.GetType().Name);
                Console.WriteLine("Inner exception message: {0}", appEx.InnerException.Message);
            }


        }
    }
}
