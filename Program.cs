using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ClipboardImageSave
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Image clipboardFile = null;
            bool clipboardIsImage = false;
            Exception exceptionV = null;
            Thread threadSta = new Thread(
                delegate ()
                {
                    try
                    {
                        if (Clipboard.ContainsImage())
                        {
                            clipboardIsImage = true;
                            clipboardFile = Clipboard.GetImage();
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptionV = ex;
                    }
                });
            threadSta.SetApartmentState(ApartmentState.STA);
            threadSta.Start();
            threadSta.Join();

            if (!object.Equals(clipboardFile, null) && clipboardIsImage)
            {
                string fileNameTemp = string.Format(@"{0}.png", DateTime.Now.Ticks);
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                clipboardFile.Save(Path.Combine(desktopPath, fileNameTemp), ImageFormat.Png);
            }
        }
    }
}