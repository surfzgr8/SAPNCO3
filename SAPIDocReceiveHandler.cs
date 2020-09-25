using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

using SAP.Middleware;
using SAP.Middleware.Connector;

namespace Clarks.SAP.Connector
{
    /// <summary>
    /// Encapsulates NCO 3 IDoc receive functionality
    /// </summary>
    public class SAPIDocReceiveHandler
    {

        public delegate void IDocReceiveEventHandler(SAPIDocReceiveEventArgs e);

        public static event IDocReceiveEventHandler IDocEndReceiveCompleteEvent;
        public static event IDocReceiveEventHandler IDocBeginReceiveCompleteEvent;

        /// <summary>
        /// Call back handler method,invoked by RFC Server.
        /// </summary>
        /// <param name="context">Session context</param>
        /// <param name="function">Rfc Function passed by Rfc Server manager</param>
        [RfcServerFunction(Name = "IDOC_INBOUND_ASYNCHRONOUS", Default = false)]
        public void IDOC_INBOUND_ASYNCHRONOUS(RfcServerContext context, IRfcFunction function)
        {


            List<string> IdocNumberList = new List<string>();

            string IDocNumber = string.Empty;
            string field = string.Empty;

            IRfcTable IdocControlRec40;
            IRfcStructure structureIdocControlRec40;
            IRfcTable IdocDataRec40;
            IRfcStructure structureIdocDataRec40;

            SAPIDocStream sapIDocWriter = new SAPIDocStream();

            // notify any subscribed clients that we are about to receive an IDoc
            SAPIDocReceiveEventArgs e = new SAPIDocReceiveEventArgs();
            e.SessionId = context.SessionID;

            if (IDocBeginReceiveCompleteEvent != null)
                IDocBeginReceiveCompleteEvent(e);



            try
            {

                // Process the IDoc DC40 header segment
                IdocControlRec40 = function.GetTable("IDOC_CONTROL_REC_40");

                // Build DC40 Control record string
                foreach (IRfcStructure rfcControlRecord in IdocControlRec40)
                {


                    structureIdocControlRec40 = IdocControlRec40.Metadata.LineType.CreateStructure();

                    sapIDocWriter.Write(rfcControlRecord.GetValue("TABNAM").ToString().PadRight(structureIdocControlRec40["TABNAM"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("MANDT").ToString().PadRight(structureIdocControlRec40["MANDT"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);

                    IDocNumber = rfcControlRecord.GetValue("DOCNUM").ToString().PadRight(structureIdocControlRec40["DOCNUM"].Metadata.NucLength, ' ');
                    sapIDocWriter.Write(IDocNumber, SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    IdocNumberList.Add(IDocNumber);


                    sapIDocWriter.Write(rfcControlRecord.GetValue("DOCREL").ToString().PadRight(structureIdocControlRec40["DOCREL"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("STATUS").ToString().PadRight(structureIdocControlRec40["STATUS"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("DIRECT").ToString().PadRight(structureIdocControlRec40["DIRECT"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("OUTMOD").ToString().PadRight(structureIdocControlRec40["OUTMOD"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("EXPRSS").ToString().PadRight(structureIdocControlRec40["EXPRSS"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);

                    sapIDocWriter.Write(rfcControlRecord.GetValue("TEST").ToString().PadRight(structureIdocControlRec40["TEST"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("IDOCTYP").ToString().PadRight(structureIdocControlRec40["IDOCTYP"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("CIMTYP").ToString().PadRight(structureIdocControlRec40["CIMTYP"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("MESTYP").ToString().PadRight(structureIdocControlRec40["MESTYP"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("MESCOD").ToString().PadRight(structureIdocControlRec40["MESCOD"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("MESFCT").ToString().PadRight(structureIdocControlRec40["MESFCT"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);

                    sapIDocWriter.Write(rfcControlRecord.GetValue("STD").ToString().PadRight(structureIdocControlRec40["STD"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("STDVRS").ToString().PadRight(structureIdocControlRec40["STDVRS"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("STDMES").ToString().PadRight(structureIdocControlRec40["STDMES"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);

                    sapIDocWriter.Write(rfcControlRecord.GetValue("SNDPOR").ToString().PadRight(structureIdocControlRec40["SNDPOR"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("SNDPRT").ToString().PadRight(structureIdocControlRec40["SNDPRT"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("SNDPFC").ToString().PadRight(structureIdocControlRec40["SNDPFC"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("SNDPRN").ToString().PadRight(structureIdocControlRec40["SNDPRN"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("SNDSAD").ToString().PadRight(structureIdocControlRec40["SNDSAD"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("SNDLAD").ToString().PadRight(structureIdocControlRec40["SNDLAD"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);

                    sapIDocWriter.Write(rfcControlRecord.GetValue("RCVPOR").ToString().PadRight(structureIdocControlRec40["RCVPOR"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("RCVPRT").ToString().PadRight(structureIdocControlRec40["RCVPRT"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("RCVPFC").ToString().PadRight(structureIdocControlRec40["RCVPFC"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("RCVPRN").ToString().PadRight(structureIdocControlRec40["RCVPRN"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("RCVSAD").ToString().PadRight(structureIdocControlRec40["RCVSAD"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("RCVLAD").ToString().PadRight(structureIdocControlRec40["RCVLAD"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);

                    string dateField = rfcControlRecord.GetValue("CREDAT").ToString().PadRight(structureIdocControlRec40["CREDAT"].Metadata.NucLength, ' ');
                    dateField = dateField.Replace("-", "");
                    sapIDocWriter.Write(dateField, SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);

                    string timeField = rfcControlRecord.GetValue("CRETIM").ToString().PadRight(structureIdocControlRec40["CRETIM"].Metadata.NucLength, ' ');
                    timeField = timeField.Replace(":", "");
                    sapIDocWriter.Write(timeField, SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);

                    sapIDocWriter.Write(rfcControlRecord.GetValue("REFINT").ToString().PadRight(structureIdocControlRec40["REFINT"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("REFGRP").ToString().PadRight(structureIdocControlRec40["REFGRP"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("REFMES").ToString().PadRight(structureIdocControlRec40["REFMES"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("ARCKEY").ToString().PadRight(structureIdocControlRec40["ARCKEY"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write(rfcControlRecord.GetValue("SERIAL").ToString().PadRight(structureIdocControlRec40["SERIAL"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    sapIDocWriter.Write("\r\n", SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                }


                // Process the IDoc DDR40 data segment
                IdocDataRec40 = function.GetTable("IDOC_DATA_REC_40");

              
                //for (int i = 0; i < IdocDataRec40.RowCount; i++)
                foreach (IRfcStructure rfcDataRecord in IdocDataRec40)
                {
                    // Get the record
                    structureIdocDataRec40 = IdocDataRec40.Metadata.LineType.CreateStructure();

                    // Build the data segment
                    sapIDocWriter.Write(rfcDataRecord.GetValue("SEGNAM").ToString().PadRight(structureIdocDataRec40["SEGNAM"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);                    
                    sapIDocWriter.Write(rfcDataRecord.GetValue("MANDT").ToString().PadRight(structureIdocDataRec40["MANDT"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);                    
                    sapIDocWriter.Write(rfcDataRecord.GetValue("DOCNUM").ToString().PadRight(structureIdocDataRec40["DOCNUM"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);                    
                    sapIDocWriter.Write(rfcDataRecord.GetValue("SEGNUM").ToString().PadRight(structureIdocDataRec40["SEGNUM"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);                    
                    sapIDocWriter.Write(rfcDataRecord.GetValue("PSGNUM").ToString().PadRight(structureIdocDataRec40["PSGNUM"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);                    
                    sapIDocWriter.Write(rfcDataRecord.GetValue("HLEVEL").ToString().PadRight(structureIdocDataRec40["HLEVEL"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);                    
                    sapIDocWriter.Write(rfcDataRecord.GetValue("SDATA").ToString().PadRight(structureIdocDataRec40["SDATA"].Metadata.NucLength, ' '), SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);
                    
                    sapIDocWriter.Write("\r\n", SAPIDocStream.SAPEncodingTypeEnum.SAPEncodingUnicode);

                }


                sapIDocWriter.UnderlyingStream.Seek(0, SeekOrigin.Begin);

                StreamReader iDocReader = new StreamReader(sapIDocWriter.UnderlyingStream);
                string Idoc = iDocReader.ReadToEnd();

                byte[] buffer = Encoding.UTF8.GetBytes(Idoc);
               
                // Pass the IDoc back in the EventArgs parameter
                e.IDocBuffer = buffer;
                e.IDocStream = sapIDocWriter.UnderlyingStream;


                // Pass back current session Id
                e.SessionId = context.SessionID;

                // Pas back IDOcnumbers
                e.IDocNumberList = IdocNumberList;

                // Set IDocs are batched flag
                e.IsBatched = (IdocControlRec40.RowCount > 1);

                if (IDocEndReceiveCompleteEvent != null)
                    IDocEndReceiveCompleteEvent(e);



            }
            catch (Exception ex)
            {
                // raise exception up
                System.Diagnostics.Trace.WriteLine(string.Format("Exception raised in IDOC_INBOUND_ASYNCHRONOUS of type:{0}", ex.Message));
            }
        }
    }

    /// <summary>
    /// Summary description for SAPAdapterErrorArgs.
    /// </summary>
    public class SAPIDocReceiveEventArgs : EventArgs
    {

        private List<string> _iDocNumberList = null;
        private byte[] _iDocBuffer = null;
        private string _sessionId = string.Empty;
        private string _data = string.Empty;
        private bool _isBatched = false;
        private Stream _iDocStream = null;


        /// <summary>
        /// IDoc returned Byte Array
        /// </summary>
        public byte[] IDocBuffer
        {
            get { return _iDocBuffer; }
            set { _iDocBuffer = value; }
        }

        public Stream IDocStream
        {
            get { return _iDocStream; }
            set { _iDocStream = value; }
        }

        public List<string> IDocNumberList
        {
            get { return _iDocNumberList; }
            set { _iDocNumberList = value; }
        }

        /// <summary>
        /// IDoc returned in string
        /// </summary>
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }


        public string SessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; }
        }

        public bool IsBatched
        {
            get { return _isBatched; }
            set { _isBatched = value; }
        }
    }
}
