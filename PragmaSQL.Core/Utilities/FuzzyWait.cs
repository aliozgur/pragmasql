using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL.Core
{
  public partial class FuzzyWait : KryptonForm
  {
    private static FuzzyWait _instance = null;
    public FuzzyWait()
    {
      InitializeComponent();
    }

    public static void ShowFuzzyWait(string message)
    {
      if (_instance == null)
      {
        _instance = new FuzzyWait();
        _instance.lblMsg.Text = message;
      }
      else
      {
        _instance.lblMsg.Text = message;
      }

      int w = _instance.pictureBox1.Left + _instance.pictureBox1.Width + 5 + _instance.lblMsg.Width + 10;
      if (w > 500)
        _instance.Width = 500;
      else if (w < 250)
        _instance.Width = 250;
      else
        _instance.Width = w;
      
      _instance.Show();
      Application.DoEvents();
    }

    public static void CloseFuzzyWait()
    {
      if (_instance == null)
        return;
      _instance.Close();
    }


    private void FuzzyWait_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (_instance != null)
        _instance = null;
    }
  }
}