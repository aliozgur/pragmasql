using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using PragmaSQL.Core;

using WeifenLuo.WinFormsUI.Docking;

namespace PragmaSQL
{
  public partial class frmMessages : DockContent
  {
    public frmMessages( )
    {
      InitializeComponent();
    }

    public ListView ListView
    {
      get { return lv; }
    }

    private void ClearMessages( )
    {
      lv.Items.Clear();
    }

    private void SaveMessagesToFile()
    {
      if (saveFileDialog1.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      StringBuilder sb = new StringBuilder();
      sb.AppendLine("Type ; " + "Date Time ; " + "Message ; " + "Method ; " + "Class ; " + "Assembly");
      sb.AppendLine(String.Empty);
      foreach (ListViewItem item in lv.Items)
      {
        sb.Append(((MessageType)item.ImageIndex).ToString()+ " ; ");
        int i = 0;
        foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
        {
          if (i == 0)
          {
            i++;
            continue;
          }
          sb.Append(subItem.Text + ";");        
        }
        sb.Append("\r\n");
      }

      StreamWriter writer = new StreamWriter(saveFileDialog1.FileName);
      writer.Write(sb.ToString());
      writer.Close();
    }


    private void btnClear_Click( object sender, EventArgs e )
    {
      ClearMessages();
    }

    private void btnSaveAs_Click(object sender, EventArgs e)
    {
      SaveMessagesToFile();
    }

    private void clearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ClearMessages();
    }

    private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SaveMessagesToFile();
    }
  }
}