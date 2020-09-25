using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

using SAP.Middleware;
using SAP.Middleware.Connector;

using Microsoft.BizTalk.Streaming;

namespace Clarks.SAP.Connector
{
    public class SAPIDocTransmitHandler
    {

        /// <summary>
        /// 
        /// </summary>
        public void SubmitIDoc(Stream IDocStream, RfcDestination rfcDestination)
        {
            IRfcFunction function = null;

            StreamReader IdocStream = new StreamReader(@"C:\ClarksBiztalkSystem\SAP Test Data\SFS\mapOrders05_output.txt");

            try
            {

                function = rfcDestination.Repository.CreateFunction("IDOC_INBOUND_ASYNCHRONOUS");
                function.SetParameterActive(0, true);

                // build the DC40 header segment
                BuildIdoc(IdocStream, function);

                function.Invoke(rfcDestination);
            }
            catch (RfcBaseException e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
        }

        private  void BuildIdoc(StreamReader txtStream, IRfcFunction function)
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
                    tableIdocDataRec40.Append(structureIdocDataRec40);

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

        private  void BuildEdiDc40FromXml(Stream xmlStream, IRfcTable tableIdocControlRec40, IRfcStructure structureIdocControlRec40)
        {
            bool endOfEDI_DC40Segment = false;
            string nodeName = string.Empty;

            XmlTextReader xmlNodeReader = new XmlTextReader(xmlStream);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;

            XmlReader xmlReader = XmlReader.Create(xmlNodeReader, settings);

            xmlReader.Read();

            while (xmlReader.Read() && !endOfEDI_DC40Segment)
            {
                // Move to fist element
                xmlReader.MoveToElement();

                //Create the DC40 Control Record

                // current node EDI_DC40 
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "EDI_DC40")
                {

                    int indexIdocControlRec40 = 0;

                    // loop round the EDI_DC40 Table
                    while (xmlReader.Read())
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element)
                        {
                            nodeName = xmlReader.LocalName;

                            // xml node name matches rfcStructure field name 
                            if (indexIdocControlRec40 < structureIdocControlRec40.Metadata.FieldCount)
                            {

                                // goto to the text element
                                xmlReader.Read();

                                // empty element so node will by an EndType
                                if (xmlReader.NodeType == XmlNodeType.Text)
                                {
                                    tableIdocControlRec40.SetValue(nodeName, xmlReader.Value);
                                }

                                indexIdocControlRec40 += 1;
                            }
                            else
                            {
                                endOfEDI_DC40Segment = true;
                                break;
                            }
                        }
                    }

                } //outer while

            }
        }


    }
}
