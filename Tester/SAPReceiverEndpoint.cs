using System;

using SAP.Middleware.Connector;

namespace Clarks.SAP.Connector
{
    /// <summary>
    /// Emulates the SAP Receive Endpoint in the SAP Adapter
    /// </summary>
    class SAPReceiverEndpoint
    {
        private RfcServer _server = null;
        private RfcDestination _rfcDestination = null;
        private static SAPIDocReceiveConfiguration _sapIDocReceiveConfiguration = null;

        private string _destinationName = "SAP_IDOC_DESTINATION_CONFIG";
        private string _IDocText = string.Empty;
        private string _sessionId = string.Empty;

        public delegate void IDocReadyHandler(EventArgs e);

        public event IDocReadyHandler IDocReady;

        public void Shutdown()
        {
            RfcSessionManager.EndContext(_rfcDestination);
            _server.Shutdown(true);
        }

        public void RegisterDestination(string destinationName)
        {
      
            bool destinationIsInialised=((_rfcDestination !=null) && string.Equals(_rfcDestination.Name, destinationName));
            
            // Only register if not already initialised
            try
            {
                // ? destinantion already configured and initialised
                if (!destinationIsInialised)
                {
                    
                    RfcDestinationManager.RegisterDestinationConfiguration(new SAPIDocDestinationConfiguration(destinationName));//1

                    _rfcDestination = RfcDestinationManager.GetDestination(destinationName);
                    
                }
            }
            // ignore as destination already configured
            catch (RfcInvalidStateException rfcEx)
            {   
                // cascade up callstack
                throw rfcEx;   
            }
 

            //System.Diagnostics.Trace.WriteLine(
            //    String.Format("Destination Confgured to:{0}", _rfcDestination.Monitor.OriginDestinationID));
        }

        /// <summary>
        /// Emulates SAP Adapter Receive Endpoint
        /// </summary>
        public void Initialise()
        {
            //hard coded for testing but will pick up from ProgId in config
            string serverName = "SQLARTMAS_SERVER";
            string progId = "SQLARTMAS";

            SAPTIDHandler rfcTIDHandler=new SAPTIDHandler();

            try
            {

                if (_sapIDocReceiveConfiguration == null)
                {
                    _sapIDocReceiveConfiguration =
                       new SAPIDocReceiveConfiguration(serverName, _rfcDestination.Name, progId);
                }
                else
                {
                    _sapIDocReceiveConfiguration.AddParameters(serverName, progId);
                }
                
                RfcServerManager.RegisterServerConfiguration(_sapIDocReceiveConfiguration);

                Type[] handlers = new Type[1] { typeof(SAPIDocReceiveHandler) };//3
                _server = RfcServerManager.GetServer(serverName, handlers);//3

                _server.RfcServerError += OnRfcServerError;
                _server.RfcServerApplicationError += OnRfcServerError;

                SAPIDocReceiveHandler.IDocEndReceiveCompleteEvent +=
                    new SAPIDocReceiveHandler.IDocReceiveEventHandler(OnIDocEndReceiveComplete);

                SAPIDocReceiveHandler.IDocBeginReceiveCompleteEvent +=
                    new SAPIDocReceiveHandler.IDocReceiveEventHandler(OnIDocBeginReceiveComplete);

                // register for session startevent in order to capture SessionId
                rfcTIDHandler.IDocSessionStart += new SAPTIDHandler.IDocsSessionEventHandler(OnRfcSessionStart);

                //register TID specific handler
                _server.TransactionIDHandler = rfcTIDHandler;

                // Create a new session for this particular destination
               // RfcSessionManager.BeginContext(_rfcDestination);


                //server.TransactionIDHandler = new MyServerHandler();
                _server.Start();//4

            }

            catch (RfcInvalidStateException rfcEx)
            {

                // cascade up callstack
                throw rfcEx;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            //Type[] handlers = new Type[1] { typeof(SAPIDocReceiveHandler) };//3
            //RfcServer server = RfcServerManager.GetServer("SQLARTMAS", handlers);//3



            //Console.WriteLine("Server has been started. Press X to exit.\n");

            //while (true)
            //{
            //    if (Console.ReadLine().Equals("X")) break;
            //}
            //server.Shutdown(true); //Shuts down 

        }


        public void InitialiseServer2()
        {
             
            //hard coded for testing but will pick up from ProgId in config
            string serverName = "SQLCONDA_SERVER";
            string progId = "SQLCOND_A";

            SAPTIDHandler rfcTIDHandler = new SAPTIDHandler();

            try
            {

               
                if (_sapIDocReceiveConfiguration == null)
                {
                    SAPIDocReceiveConfiguration sapIDocReceiveConfiguration =
                       new SAPIDocReceiveConfiguration(serverName, _rfcDestination.Name, progId);

                    RfcServerManager.RegisterServerConfiguration(_sapIDocReceiveConfiguration);
                }
                else
                {
                    _sapIDocReceiveConfiguration.AddParameters(serverName, progId);
                }

                RfcServerManager.RegisterServerConfiguration(_sapIDocReceiveConfiguration);

                Type[] handlers = new Type[1] { typeof(SAPIDocReceiveHandler) };//3
                _server = RfcServerManager.GetServer(serverName, handlers);//3

                _server.RfcServerError += OnRfcServerError;
                _server.RfcServerApplicationError += OnRfcServerError;

                SAPIDocReceiveHandler.IDocEndReceiveCompleteEvent +=
                    new SAPIDocReceiveHandler.IDocReceiveEventHandler(OnIDocEndReceiveComplete);

                SAPIDocReceiveHandler.IDocBeginReceiveCompleteEvent +=
                    new SAPIDocReceiveHandler.IDocReceiveEventHandler(OnIDocBeginReceiveComplete);

                // register for session startevent in order to capture SessionId
                rfcTIDHandler.IDocSessionStart += new SAPTIDHandler.IDocsSessionEventHandler(OnRfcSessionStart);

                //register TID specific handler
                _server.TransactionIDHandler = rfcTIDHandler;

                // Create a new session for this particular destination
               // RfcSessionManager.BeginContext(_rfcDestination);


                //server.TransactionIDHandler = new MyServerHandler();
                _server.Start();//4

            }

            catch (RfcInvalidStateException rfcEx)
            {

                // cascade up callstack
                throw rfcEx;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            //Type[] handlers = new Type[1] { typeof(SAPIDocReceiveHandler) };//3
            //RfcServer server = RfcServerManager.GetServer("SQLARTMAS", handlers);//3



            //Console.WriteLine("Server has been started. Press X to exit.\n");

            //while (true)
            //{
            //    if (Console.ReadLine().Equals("X")) break;
            //}
            //server.Shutdown(true); //Shuts down 

        }
        public string IDocText
        {
            get { return _IDocText; }
            set { _IDocText = value; }
        }

        public void OnIDocBeginReceiveComplete(SAPIDocReceiveEventArgs e)
        {
        
            // static event so make sure callback is from the same session.
            if (string.Equals(e.SessionId, this._sessionId))
            {
                // start streaming in SAP Adapter
            }
        }

        public void OnIDocEndReceiveComplete(SAPIDocReceiveEventArgs e)
        {
            // static event so make sure callback is from the same session.
            if (string.Equals(e.SessionId, this._sessionId))
            {
              //  StreamReader sr = new StreamReader(e.IDocStream, Encoding.Unicode);

                this._IDocText = e.Data;

                System.Diagnostics.Trace.WriteLine("Completed receiving an IDoc");
                System.Diagnostics.Trace.WriteLine(this._IDocText);

                // bubble upto form
                IDocReady(new EventArgs());
            }
        }

        public void OnRfcServerStateChanged(object server, RfcServerStateChangedEventArgs stateEventData)
        {
            RfcServer serverStatus=server as RfcServer;

            System.Diagnostics.Trace.WriteLine(serverStatus.Name);
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

        // call back to capture the session Id
        public void OnRfcSessionStart(SAPSessionEventArgs sessionArgs)
        {
            this._sessionId = sessionArgs.SessionId;
        }
    }
}
