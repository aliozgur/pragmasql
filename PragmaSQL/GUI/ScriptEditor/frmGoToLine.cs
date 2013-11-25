/********************************************************************
  Class      : frmGoToLine
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Istanbul Bilgi University
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.GUI
{
  public partial class frmGoToLine : Form
  {
    private int maxLineCnt = 0;
    public int MaxLineCnt
    {
      get 
      { 
        return maxLineCnt; 
      }
      set 
      { 
        maxLineCnt = value; 
        if( value <= 0 )
        {
          label1.Text = "Line (No lines available)";
          textBox1.Enabled = false;
          btnGo.Enabled = false;
        }
        else
        {
          textBox1.Enabled = true;
          btnGo.Enabled = true;        
          btnGo.Enabled = true;
          label1.Text = "Line ( 1 - " + value.ToString() + " )";
        }
      }
    }

    private GoToLineEventHandler _goToLineRequested;
    public event GoToLineEventHandler GoToLineRequested
    {
      add
      {
        _goToLineRequested += value;
      }
      remove
      {
        _goToLineRequested -= value;
      }
    }

    public frmGoToLine( )
    {
      InitializeComponent();
    }

    private void btnGo_Click( object sender, EventArgs e )
    {
      int lineNo = 0;
      if( !Int32.TryParse(textBox1.Text,out lineNo))
      {
        return;
      }

      if( _goToLineRequested != null)
      {
        _goToLineRequested(this,lineNo);
      }

    }

    private void btnClose_Click( object sender, EventArgs e )
    {
      Close();
    }

  }
  public delegate void GoToLineEventHandler(object sender, int lineNo);

}