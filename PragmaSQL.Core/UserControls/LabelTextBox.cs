using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
  public partial class LabelTextBox : UserControl
  {
    public LabelTextBox( )
    {
      InitializeComponent();
    }

    public TextBox TextBox
    {
      get { return this.textBox; }
    }

    public Label Label
    {
      get { return this.lbl; }
    }

    public string LabelText
    {
      get { return lbl.Text; }
      set { lbl.Text = value; }
    }

    public string TextBoxText
    {
      get { return textBox.Text; }
      set { textBox.Text = value; }
    }

    public bool ReadOnly
    {
      get { return textBox.ReadOnly; }
      set { textBox.ReadOnly = value; }
    }
  }

  public enum LabelOrientation
  {
    Top,
    Left,
    Bottom,
    Right
  }
}
