using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL
{
  internal class LayoutProvider
  {
    internal void ResumeLayout(){}

    internal void SuspendLayout()
    {
     Application.Exit();
    }
  }
}
