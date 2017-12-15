using System;
using System.Globalization;
using System.Windows.Forms;
public class NumericPortTextBox : TextBox
{
    protected override void InitLayout()
    {
        base.InitLayout();
      
    }
    bool allowSpace = false;

    public string previousValue { get; set; }

    public bool TestRange()
    {
        if (null != this)
        {
            if (!string.IsNullOrEmpty(this.Text))
            {
                int curval = int.Parse(this.Text);
                if (1 <= curval && 65535 >= curval)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }
        return false;
    }

    // Restricts the entry of characters to digits (including hex), the negative sign,
    // the decimal point, and editing keystrokes (backspace).
    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        base.OnKeyPress(e);
        previousValue = this.Text;
        NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
        string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
        string groupSeparator = numberFormatInfo.NumberGroupSeparator;
        string negativeSign = numberFormatInfo.NegativeSign;

        string keyInput = e.KeyChar.ToString();

        if (Char.IsDigit(e.KeyChar))
        {
            // Digits are OK
            //e.Handled = TestRange(keyInput);
        }
        else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
         keyInput.Equals(negativeSign))
        {
            e.Handled = true;
            // No Decimal separator is not OK
        }
        else if (e.KeyChar == '\b')
        {
            // Backspace key is OK
        }
        //    else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
        //    {
        //     // Let the edit control handle control and alt key combinations
        //    }
        else if (this.allowSpace && e.KeyChar == ' ')
        {
            e.Handled = true;
        }
        else
        {
            // Swallow this invalid key and beep
            e.Handled = true;
            //    MessageBeep();
        }
    }

   

    public int IntValue
    {
        get
        {
            return Int32.Parse(this.Text);
        }
    }

    public decimal DecimalValue
    {
        get
        {
            return Decimal.Parse(this.Text);
        }
    }

    public bool AllowSpace
    {
        set
        {
            this.allowSpace = value;
        }

        get
        {
            return this.allowSpace;
        }
    }
}
