using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanProjectManagement.Business
{
    public static class Helpers
    {
        public static string TestTextBox<T>(this T textbox, string val) where T : TextBox, new()
        {
            string ret = string.Empty;
            if (null == textbox)
                ret = "Unknown Error";
            else if (string.IsNullOrEmpty(textbox.Text))
                ret = "Please enter data for " + val;
            return ret;
        }
        public static string TestComboBox<T>(this T comboBox, string val) where T : ComboBox, new()
        {
            string ret = string.Empty;
            if (null == comboBox)
                ret = "Unknown Error";
            else
            {
                if (comboBox.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    if (null == comboBox.SelectedItem || string.IsNullOrEmpty(comboBox.SelectedItem.ToString()))
                        ret = "Please enter data for " + val;
                }
                else
                {
                    if (string.IsNullOrEmpty(comboBox.Text))
                        ret = "Please enter data for " + val;
                }
            }
            return ret;
        }
    }
}
