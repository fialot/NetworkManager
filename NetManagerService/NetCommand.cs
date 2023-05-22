using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NetworkManager;

namespace NetManagerService;


internal class NetCommand
{

    readonly string commandDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + Path.DirectorySeparatorChar + "NetManager" + Path.DirectorySeparatorChar;

    public void CheckCommandFile()
    {
        var file = commandDir + "command.xml";


        //Console.WriteLine(file);

        if (File.Exists(file))
        {

            var XMLtext = "";
            // ----- Read File -----
            try
            {
                using (StreamReader reader = new StreamReader(file, true))
                {
                    XMLtext = reader.ReadToEnd();
                }
            }
            catch
            {
            }

            try
            {
                // ----- Parse XML to Structure -----
                var xml = XDocument.Parse(XMLtext);
                var settings = xml.Element("settings");
                if (settings != null)
                {
                    var net = settings.Element("network");
                    if (net != null)
                    {
                        IpSetting netSettings = new IpSetting(net);

                        try
                        {
                            IPv4.Set(netSettings);
                            Console.WriteLine("Change settings succesfully");

                            WriteReply("OK", "Succesfully changed");
                        } 
                        catch (Exception ex) 
                        {
                            Console.WriteLine("Change settings error: " + ex.Message);

                            WriteReply("Error", ex.Message);
                        }
                    }
                }
            }

            catch { }

            // ----- Delete command file -----
            try
            {
                File.Delete(file);
            }
            catch { }
            
        }
        else
        {
            /*IpSettings netSettings = new IpSettings();

            // ----- Parse XML to Structure -----
            var xml = new XDocument(new XDeclaration("1.0", "utf-8", null));
            var mainElement = new XElement("settings");

            mainElement.Add(netSettings.GetXmlElement());

            xml.Add(mainElement);

            // ---- Save to XML -----
            xml.Save(file);
            */
        }
    }

    private void WriteReply(string result, string message)
    {
        var replyFile = commandDir + "reply.xml";

        // ----- Parse XML to Structure -----
        var xml = new XDocument(new XDeclaration("1.0", "utf-8", null));
        var mainElement = new XElement("reply");

        mainElement.Add(new XElement("result", result));
        mainElement.Add(new XElement("message", message));

        xml.Add(mainElement);

        // ---- Save to XML -----
        xml.Save(replyFile);
    }

}
