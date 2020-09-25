using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.BizTalk.Streaming;

namespace Clarks.SAP.Connector
{
    public class SAPIDocStream : VirtualStream, IDisposable
    {
        private const int DefaultBufferSize = 10240; //10 KB
        private const int DefaultThresholdSize = 1048576; //1 MB

        private bool _disposed = false;

        public enum SAPEncodingTypeEnum { SAPEncodingAscii = 1, SAPEncodingUnicode };

        public SAPIDocStream() : base(DefaultBufferSize,MemoryFlag.AutoOverFlowToDisk) { }

      

        public void Write(string bufferedString, SAPEncodingTypeEnum encodingType)
        {
            byte[] buffer;
            //MemoryStream memStream;
            ASCIIEncoding encoderAscii = new ASCIIEncoding();
            UTF8Encoding encoderUTF = new UTF8Encoding();

            try
            {

                switch (encodingType)
                {
                    case SAPEncodingTypeEnum.SAPEncodingAscii:

                        buffer = encoderAscii.GetBytes(bufferedString);
                        break;

                    case SAPEncodingTypeEnum.SAPEncodingUnicode:

                        buffer = encoderUTF.GetBytes(bufferedString);
                        
         
                        break;

                    default:

                        buffer = encoderUTF.GetBytes(bufferedString);
                        break;

                }


                base.Write(buffer, 0, buffer.Length);
                base.Flush();
               


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                //return (null);

            }

        }


        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue 
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
           
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this._disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
             

          
            }
            _disposed = true;
        }


    }
}
