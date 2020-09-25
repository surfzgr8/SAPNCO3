using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using SAP.Middleware;
using SAP.Middleware.Connector;

namespace Clarks.SAP.Connector
{

    public class SAPTIDHandler : ITransactionIDHandler
    {

        public delegate void IDocsSessionEventHandler(SAPSessionEventArgs e);

        public event IDocsSessionEventHandler IDocSessionStart;

        //ONLY for tests. Use a database to store the TID state!
        List<string> tids = new List<string>();


        //If DB is down, throw an exception at this point. .Net Connector will then abort 
        //the tRFC and the R/3 backend will try again later.
        public bool CheckTransactionID(RfcServerContextInfo serverContext, RfcTID tid)
        {
            if (string.IsNullOrEmpty(serverContext.TransactionID.TID.ToString()))
                EventLog.WriteEntry(
                    "Clarks SAP Adapter", string.Format("serverContext.TransactionID.TID in CheckTransactionID is null"), EventLogEntryType.Error);

            if (tid == null)
            {
                
                EventLog.WriteEntry(
                      "Clarks SAP Adapter", string.Format("tid.TID in CheckTransactionID is null")  , EventLogEntryType.Error);
               
            }

            if (tid.TID == null)
            {
                
                EventLog.WriteEntry(
                      "Clarks SAP Adapter", string.Format("tid.TID in CheckTransactionID is null")  , EventLogEntryType.Error);
                
            }

            if (tid.TID != null)
                if (string.IsNullOrEmpty(tid.TID.ToString()))
                {
                    

                     EventLog.WriteEntry(
                      "Clarks SAP Adapter", string.Format("tid.TID.ToString in CheckTransactionID is null or Empty")  , EventLogEntryType.Error);

                   

                }
                else
                {
                    EventLog.WriteEntry(
                        "Clarks SAP Adapter", string.Format("Clarks SAP Adapter TID set to:{0} in CheckTransactionID callback", tid.TID.ToString()), EventLogEntryType.Information);
                    

                }

            // Notify any registered client with sessionId
            if (IDocSessionStart !=null)
            {
                SAPSessionEventArgs sapSessionEvents=new SAPSessionEventArgs();
                sapSessionEvents.SessionId = serverContext.SessionID;
                IDocSessionStart(sapSessionEvents);
            }

            System.Diagnostics.Trace.WriteLine(string.Format("Rfc Function Activated with SessionId:{0}", serverContext.SessionID));

            lock (tids)
            {
                if (tids.Contains(tid.TID))
                    return false;
                else
                {
                    tids.Add(tid.TID);
                    return true;
                }
            }
            // "true" means that NCo will now execute the transaction, "false" means
            // that we have already executed this transaction previously, so NCo will
            // skip the function execution step and will immediately return an OK code to R/3.
        }


        // clean up the resources
        public void ConfirmTransactionID(RfcServerContextInfo serverContext, RfcTID tid)
        {

            if (serverContext.TransactionID == null)
            {
                EventLog.WriteEntry(
                        "Clarks SAP Adapter", string.Format("serverContext.TransactionID in ConfirmTransactionID is null"), EventLogEntryType.Error);
            }
            else
            {
                if (string.IsNullOrEmpty(serverContext.TransactionID.TID.ToString()))
                    EventLog.WriteEntry(
                        "Clarks SAP Adapter", string.Format("serverContext.TransactionID.TID in ConfirmTransactionID is null"), EventLogEntryType.Error);
            }

            if (tid == null)
            {

                EventLog.WriteEntry(
                      "Clarks SAP Adapter", string.Format("tid.TID in ConfirmTransactionID is null"), EventLogEntryType.Error);
               
            }

            if (tid.TID == null)
            {

                EventLog.WriteEntry(
                      "Clarks SAP Adapter", string.Format("tid.TID in ConfirmTransactionID is null"), EventLogEntryType.Error);
               
            }

            if (tid.TID != null)
                if (string.IsNullOrEmpty(tid.TID.ToString()))
                {


                    EventLog.WriteEntry(
                     "Clarks SAP Adapter", string.Format("tid.TID.ToString in ConfirmTransactionID is null or Empty"), EventLogEntryType.Error);

                  
                }
                else
                {
                    EventLog.WriteEntry(
                        "Clarks SAP Adapter", string.Format("Clarks SAP Adapter TID set to:{0} in ConfirmTransactionID callback", tid.TID.ToString()), EventLogEntryType.Information);


                }

            
            
            try
            {
                //clean up the resources
                //partner won't react on an exception at this point
            }
            finally
            {
                lock (tids)
                {
                    tids.Remove(tid.TID);
                }
            }
        }



        // react on commit e.g. commit on the database
        // if necessary throw an exception, if the commit was not possible
        public void Commit(RfcServerContextInfo serverContext, RfcTID tid)
        {

            if (serverContext.TransactionID == null)
            {
                EventLog.WriteEntry(
                        "Clarks SAP Adapter", string.Format("serverContext.TransactionID in Commit is null"), EventLogEntryType.Error);
            }
            else
            {
                if (string.IsNullOrEmpty(serverContext.TransactionID.TID.ToString()))
                    EventLog.WriteEntry(
                        "Clarks SAP Adapter", string.Format("serverContext.TransactionID.TID in Commit is null"), EventLogEntryType.Error);
            }
            if (tid == null)
            {

                EventLog.WriteEntry(
                      "Clarks SAP Adapter", string.Format("tid.TID in Commit is null"), EventLogEntryType.Error);
                throw new ApplicationException("tid in Commit is null");
            }

            if (tid.TID == null)
            {

                EventLog.WriteEntry(
                      "Clarks SAP Adapter", string.Format("tid.TID in Commit is null"), EventLogEntryType.Error);
                throw new ApplicationException("tid.TID in Commit is null");
            }

            if (tid.TID != null)
                if (string.IsNullOrEmpty(tid.TID.ToString()))
                {


                    EventLog.WriteEntry(
                     "Clarks SAP Adapter", string.Format("tid.TID.ToString in Commit is null or Empty"), EventLogEntryType.Error);

                    throw new ApplicationException("tid.TID.ToString in Commit is null or Empty");

                }
                else
                {
                    EventLog.WriteEntry(
                        "Clarks SAP Adapter", string.Format("Clarks SAP Adapter TID set to:{0} in Commit callback", tid.TID.ToString()), EventLogEntryType.Information);


                }

            lock (tids)
            {
                if (!tids.Contains(tid.TID))
                    throw new Exception("tid " + tid.TID + " doesn't exist, hence it cannot be committed");
            }
        }


        // react on rollback e.g. rollback on the database
        public void Rollback(RfcServerContextInfo serverContext, RfcTID tid)
        {
            if (serverContext.TransactionID == null)
            {
                EventLog.WriteEntry(
                        "Clarks SAP Adapter", string.Format("serverContext.TransactionID in Rollback is null"), EventLogEntryType.Error);
            }
            else
            {
                if (string.IsNullOrEmpty(serverContext.TransactionID.TID.ToString()))
                    EventLog.WriteEntry(
                        "Clarks SAP Adapter", string.Format("serverContext.TransactionID.TID in Rollback is null"), EventLogEntryType.Error);
            }

            if (tid == null)
            {

                EventLog.WriteEntry(
                      "Clarks SAP Adapter", string.Format("tid.TID in Rollback is null"), EventLogEntryType.Error);
                throw new ApplicationException("tid in Rollback is null");
            }

            if (tid.TID == null)
            {

                EventLog.WriteEntry(
                      "Clarks SAP Adapter", string.Format("tid.TID in Rollback is null"), EventLogEntryType.Error);
                throw new ApplicationException("tid.TID in Rollback is null");
            }

            if (tid.TID != null)
                if (string.IsNullOrEmpty(tid.TID.ToString()))
                {


                    EventLog.WriteEntry(
                     "Clarks SAP Adapter", string.Format("tid.TID.ToString in Rollback is null or Empty"), EventLogEntryType.Error);

                    throw new ApplicationException("tid.TID.ToString in Rollback is null or Empty");

                }
                else
                {
                    EventLog.WriteEntry(
                        "Clarks SAP Adapter", string.Format("Clarks SAP Adapter TID set to:{0}", tid.TID.ToString()), EventLogEntryType.Information);


                }
            Console.WriteLine("Rollback transaction ID " + tid);
            /* Make sure the TID is removed from the list, otherwise CheckTransactionID will
            * return false the next time the backend retries this failed transaction, and then
            * it will never be executed...
            */
            tids.Remove(tid.TID);
        }
    }
    /// <summary>
    /// Summary description for SAPAdapterErrorArgs.
    /// </summary>
    public class SAPSessionEventArgs : EventArgs
    {

      
        private string _sessionId = string.Empty;


        public string SessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; }
        }
    }
}
