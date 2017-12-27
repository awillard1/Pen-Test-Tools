using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace DirectoryTestWebApp.Business
{
    public class SitemapHelper
    {
        public List<string> Urls(Stream stream)
        {
            List<string> urls = new List<string>();
            using (StreamReader reader = new StreamReader(stream))
            {
                XmlTextReader txtReader = new XmlTextReader(reader);
                string prevName = string.Empty;
                while (txtReader.Read())
                {
                    switch (txtReader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an Element.
                            if ("loc" == txtReader.Name)
                                prevName = txtReader.Name;
                            else
                                prevName = string.Empty;
                            break;
                        case XmlNodeType.Text: //Display the text in each element.
                            if ("loc" == prevName)
                                urls.Add(txtReader.Value);
                            break;
                        case XmlNodeType.EndElement: //Display end of element.
                            break;
                    }
                }
                stream.Dispose();
            }
            return urls;
        }
    }
}
