using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ParseJSONDataForDataKeys
{
    public partial class DataKeyParse : Form
    {
        public DataKeyParse()
        {
            InitializeComponent();
        }

        private IList<ObjectData> ProcessText(string d)
        {
            IList<ObjectData> data = new List<ObjectData>();
            try
            {
                if (jsonData.Checked)
                {
                    JsonTextReader reader = new JsonTextReader(new StringReader(d));
                    while (reader.Read())
                    {
                        if (reader.Value != null)
                            if (reader.TokenType == JsonToken.PropertyName)
                            {
                                ObjectData kd = new ObjectData();
                                if (null != reader.Value)
                                {
                                    kd.Key = reader.Value.ToString();
                                    try
                                    {
                                        kd.Value = reader.ReadAsString();
                                    }
                                    catch { }
                                    finally
                                    {
                                        data.Add(kd);
                                    }
                                }
                            }
                            else { }
                    }
                }
                else
                {
                    XElement doc = XElement.Parse(d);
                    foreach (var name in doc.DescendantNodes().OfType<XElement>())
                    {
                        ObjectData kd = new ObjectData
                        {
                            Key = name.Name.LocalName,
                            Value = name.Value.ToString()
                        };
                        data.Add(kd);

                        foreach (XAttribute attrName in name.Attributes().ToList())
                        {
                            ObjectData kd1 = new ObjectData
                            {
                                Key = kd.Key.ToString() + " : " + attrName.Name.ToString(),
                                Value = attrName.Value.ToString()
                            };
                            data.Add(kd1);

                        }
                    }
                }
            }
            catch
            {
                //Sloppy exception handling
                MessageBox.Show("Unable to Parse Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return data;
        }

        private void process_Click(object sender, EventArgs e)
        {
            string d = inputText.Text;
            IList<ObjectData> data = ProcessText(d);
            if (null != data)
            {
                var list = new BindingList<ObjectData>(data);
                dataGridView1.DataSource = list;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
    public class ObjectData
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}