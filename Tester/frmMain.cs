using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
//using System.Xml.Xsl;
using System.IO;
using System.Windows.Forms;
using System.Reflection;


using Clarks.SAP.Connector;


namespace Tester
{
    public partial class frmMain : Form
    {
        private SAPReceiverEndpoint _btsRxAdapter = new SAPReceiverEndpoint();
      

        public frmMain()
        {
            InitializeComponent();
            
        }

    

        private void frmMain_Load(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // uniquely id destination
            _btsRxAdapter.RegisterDestination("SAP_IDOC_DESTINATION_CONFIG");

            //_btsRxAdapter2.RegisterDestination();
        }

        public static void SanityTest()
        {
            //Get destination instance. The destination is configured in app.config.
            // RfcDestinationManager.RegisterDestinationConfiguration(new MyClientConfig());//1
            //// destination = RfcDestinationManager.GetDestination("SAP_IDOC_CONFIG");
            // IRfcFunction function = null;

            // try
            // {
            //     function = _rfcDestination.Repository.          //get metadata repository associated with the destination
            //     CreateFunction("STFC_CONNECTION");  //fetch or get cached function metadata and 
            //     //create a function container based on the function metadata

            //     //set the parameter REQUTEXT. The parameter is CHAR 255, but the .Net Connector runtime always trys to find
            //     //a suitable conversion between C# data types and ABAP data types.
            //     function.SetValue("REQUTEXT", "Hello SAP");

            //     function.Invoke(_rfcDestination); //make the remote call
            // }
            // catch (RfcBaseException e)
            // {
            //     Console.WriteLine(e.ToString());
            //     return;
            // }

            // Console.WriteLine("STFC_CONNECTION finished:");
            // //Read the function parameters ECHOTEXT and RESPTEXT
            // Console.WriteLine(" Echo: " + function.GetString("ECHOTEXT"));
            // Console.WriteLine(" Response: " + function.GetString("RESPTEXT"));
            // Console.WriteLine();
            // Console.ReadLine();
        }

        private void btnRxIDoc_Click(object sender, EventArgs e)
        {
            _btsRxAdapter.Initialise();

            _btsRxAdapter.IDocReady += new SAPReceiverEndpoint.IDocReadyHandler(IDocReady);

        }


        private void IDocReady(EventArgs e) 
        {
            

            this.Invoke((MethodInvoker)delegate
            {

                txtIDoc.Text = _btsRxAdapter.IDocText;
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtIDoc.Text = string.Empty;
        }

        private void btnStopRx_Click(object sender, EventArgs e)
        {
            
        }

        private void btsServer2_Click(object sender, EventArgs e)
        {
            SAPAdapterWorkItem sapSender = new SAPAdapterWorkItem();

            sapSender.RegisterDestination("SAP_IDOC_DESTINATION_CONFIG");
            sapSender.SubmitIDOc();
        }

    }
}