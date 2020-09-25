using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

using SAP.Middleware;
using SAP.Middleware.Connector;

namespace Clarks.SAP.Connector
{
    /// <summary>
    /// Test harness to emulate BizTalk
    /// </summary>
    class Program
    {
        

        static void Main(string[] args)
        {



            //ConnectRfcServer();
            
            //SubmitIDOc();
           
            //SanityTest();
        }





        public static void SubmitIDOc()
        {
            
            
            //IRfcFunction function = null;

            //StreamReader IdocStream = new StreamReader(@"C:\ClarksBiztalkSystem\SAP Test Data\SFS\mapOrders05_output.txt");

            //try
            //{

            //    function = _rfcDestination.Repository.CreateFunction("IDOC_INBOUND_ASYNCHRONOUS");
            //    function.SetParameterActive(0, true);
      
            //    // build the DC40 header segment
            //    BuildIdoc(IdocStream,function);
                
            //    function.Invoke(_rfcDestination);
            //}
            //catch (RfcBaseException e)
            //{
            //    Console.WriteLine(e.ToString());
            //    return;
            //}

  
        }














    }


}
