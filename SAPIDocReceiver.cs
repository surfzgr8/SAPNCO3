using System;
using System.Collections.Generic;
using System.Text;

using SAP.Middleware;
using SAP.Middleware.Connector;

namespace Clarks.SAP.Connector
{
    /// <summary>
    /// Encapsulates NCO 3 IDoc receive functionality
    /// </summary>
    public class SAPIDocReceiveHandler
    {
        [RfcServerFunction(Name = "IDOC_INBOUND_ASYNCHRONOUS", Default = false)]
        public void StfcConnection(RfcServerContext context, IRfcFunction function)
        {

            IRfcTable IdocControlRec40;
            IRfcStructure structureIdocControlRec40;
            IRfcTable IdocDataRec40;
            IRfcStructure structureIdocDataRec40;

            string field = string.Empty;

            try
            {
                IdocControlRec40 = function.GetTable("IDOC_CONTROL_REC_40");

                for (int i = 0; i < IdocControlRec40.RowCount; i++)
                {
                    IdocControlRec40.CurrentIndex = i;
                    structureIdocControlRec40 = IdocControlRec40.Metadata.LineType.CreateStructure();

                    for (int j = 0; j < structureIdocControlRec40.ElementCount; j++)
                    {
                        switch (structureIdocControlRec40[j].Metadata.DataType)
                        {
                            case RfcDataType.DATE:

                                string dateField = IdocControlRec40.GetValue(structureIdocControlRec40[j].Metadata.Name).ToString().PadRight(structureIdocControlRec40[j].Metadata.NucLength, ' ');
                                dateField = dateField.Replace("-", "");

                                field = field + dateField;

                                break;

                            case RfcDataType.TIME:

                                string timeField = IdocControlRec40.GetValue(structureIdocControlRec40[j].Metadata.Name).ToString().PadRight(structureIdocControlRec40[j].Metadata.NucLength, ' ');
                                timeField = timeField.Replace(":", "");

                                field = field + timeField;

                                break;

                            default:

                                field = field + IdocControlRec40.GetValue(structureIdocControlRec40[j].Metadata.Name).ToString().PadRight(structureIdocControlRec40[j].Metadata.NucLength, ' ');

                                break;
                        }
                    }

                    field += "\r\n";

                }
                //field = ;

                IdocDataRec40 = function.GetTable("IDOC_DATA_REC_40");

                for (int i = 0; i < IdocDataRec40.RowCount; i++)
                {
                    IdocDataRec40.CurrentIndex = i;
                    structureIdocDataRec40 = IdocDataRec40.Metadata.LineType.CreateStructure();


                    for (int j = 0; j < structureIdocDataRec40.ElementCount; j++)
                    {
                        Type dataType = structureIdocDataRec40[j].GetType();

                        field = field + IdocDataRec40.GetValue(structureIdocDataRec40[j].Metadata.Name).ToString().PadRight(structureIdocDataRec40[j].Metadata.NucLength, ' ');

                    }

                    field += "\r\n";

                    //Console.WriteLine(field);
                }

                File.WriteAllText(@"c:\testIDocBytes.txt", field);


            }
            catch (Exception e)
            {

            }
        }

        //[RfcServerFunction(Name = "STFC_CONNECTION")]
        //public static void StfcConnection(RfcServerContext context,
        //       IRfcFunction function)
        //{
        //    Console.WriteLine("Received function call {0} from system {1}.",
        //        function.Metadata.Name, context.SystemAttributes.SystemID);
        //    String reqtext = function.GetString("REQUTEXT");
        //    Console.WriteLine("REQUTEXT = {0}\n", reqtext);
        //    function.SetValue("ECHOTEXT", reqtext);
        //    function.SetValue("RESPTEXT", "Hello from NCo 3.0!");
        //}
    }
}
