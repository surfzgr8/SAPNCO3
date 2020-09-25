using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

using SAP.Middleware;
using SAP.Middleware.Connector;

namespace Clarks.SAP.Connector
{
    /// <summary>
    /// Emulates SAP Adapter Transmitter
    /// </summary>
    class SAPAdapterWorkItem
    {
        private RfcDestination _rfcDestination = null;

        public void RegisterDestination(string destinationName)
        {

            bool destinationIsInialised = ((_rfcDestination != null) && string.Equals(_rfcDestination.Name, destinationName));

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


            System.Diagnostics.Trace.WriteLine(
                String.Format("Destination Confgured to:{0}", _rfcDestination.Monitor.OriginDestinationID));
        }
        public  void SubmitIDOc()
        {


            IRfcFunction function = null;

            StreamReader IdocStream = new StreamReader(@"C:\ClarksBiztalkSystem\SAP Test Data\SFS\mapOrders05_output.txt");

            try
            {

                function = _rfcDestination.Repository.CreateFunction("IDOC_INBOUND_ASYNCHRONOUS");
                function.SetParameterActive(0, true);

                // build the DC40 header segment
                BuildIdoc(IdocStream, function);

                function.Invoke(_rfcDestination);
            }
            catch (RfcBaseException e)
            {
                Console.WriteLine(e.ToString());
                return;
            }


        }

        private void BuildIdoc(StreamReader txtStream, IRfcFunction function)
        {


            IRfcTable tableIdocControlRec40 = null;
            IRfcStructure structureIdocControlRec40;

            IRfcTable tableIdocDataRec40;
            IRfcStructure structureIdocDataRec40;

            String iDocText = txtStream.ReadToEnd();
            int pos = 0;

            tableIdocControlRec40 = function.GetTable("IDOC_CONTROL_REC_40");
            structureIdocControlRec40 = tableIdocControlRec40.Metadata.LineType.CreateStructure();

            //  need one row for DC40 control record
            tableIdocControlRec40.Insert();

            // iterate thru fields in structure
            foreach (IRfcField field in structureIdocControlRec40)
            {
                // get value from text
                String fieldValue = iDocText.Substring(pos, field.Metadata.NucLength);

                // insert value into table
                tableIdocControlRec40.SetValue(field.Metadata.Name, fieldValue);
                pos += field.Metadata.NucLength;
            }

            String iDocDataline = string.Empty;

            txtStream.BaseStream.Seek(0, SeekOrigin.Begin);
            // skip DC40 control row
            txtStream.ReadLine();

            // now build Data Table
            tableIdocDataRec40 = function.GetTable("IDOC_DATA_REC_40");

            int lineCount = 0;

            do
            {
                iDocDataline = txtStream.ReadLine();

                if (!string.IsNullOrEmpty(iDocDataline))
                {

                    structureIdocDataRec40 = tableIdocDataRec40.Metadata.LineType.CreateStructure();
                    tableIdocDataRec40.Append(iDocDataline.Length);

                    pos = 0;

                    // iterate thru fields in structure
                    foreach (IRfcField field in structureIdocDataRec40)
                    {
                        // data segment header record
                        if (!string.Equals(field.Metadata.Name, "SDATA"))
                        {
                            // get value from text
                            String fieldValue = iDocDataline.Substring(pos, field.Metadata.NucLength);

                            tableIdocDataRec40[lineCount].SetValue(field.Metadata.Name, fieldValue);
                            pos += field.Metadata.NucLength;
                        }
                        else
                        {
                            // rest of data
                            String fieldValue = iDocDataline.Substring(pos).PadRight(field.Metadata.NucLength);
                            tableIdocDataRec40[lineCount].SetValue(field.Metadata.Name, fieldValue);
                        }
                        // insert value into table

                    }
                }


                lineCount += 1;
            }
            while (!string.IsNullOrEmpty(iDocDataline));

        }
    }


}
