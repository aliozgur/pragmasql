/********************************************************************
  Class      : SystemFileAssociationOptions
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using PragmaSQL.Core;
using BrendanGrant.Helpers.FileAssociation;

namespace PragmaSQL.Core
{
  public class SystemFileAssociationOptions
  {
    
    private SerializableDictionary<string, string[]> _openWithBackup = new SerializableDictionary<string, string[]>();
    public SerializableDictionary<string, string[]> OpenWithBackup
    {
      get { return _openWithBackup; }
      set { _openWithBackup = value; }
    }

    private SerializableDictionary<string, ProgramVerb[]> _verbsBackup = new SerializableDictionary<string, ProgramVerb[]>();
    public SerializableDictionary<string, ProgramVerb[]> VerbsBackup
    {
      get { return _verbsBackup; }
      set { _verbsBackup = value; }
    }

    private SerializableDictionary<string, string> _associations = new SerializableDictionary<string, string>();
    public SerializableDictionary<string, string> Associations
    {
      get { return _associations; }
      set { _associations = value; }
    }

    private List<string> _sqlProjectOpenWithBackup = new List<string>();
    public List<string> SqlProjectOpenWithBackup
    {
      get { return _sqlProjectOpenWithBackup; }
      set { _sqlProjectOpenWithBackup = value; }
    }

    private List<ProgramVerb> _sqlProjectVerbsBackup = new List<ProgramVerb>();
    public List<ProgramVerb> SqlProjectVerbsBackup
    {
      get { return _sqlProjectVerbsBackup; }
      set { _sqlProjectVerbsBackup = value; }
    }

    private bool _sqlProjectFileAssociated = false;
    public bool SqlProjectFileAssociated
    {
      get { return _sqlProjectFileAssociated; }
      set { _sqlProjectFileAssociated = value; }
    }

    private string _sqlProjectFileProgramIDBackup = String.Empty;
    public string SqlProjectFileProgramIDBackup
    {
      get { return _sqlProjectFileProgramIDBackup; }
      set { _sqlProjectFileProgramIDBackup = value; }
    }

    public SystemFileAssociationOptions()
    {
    
    }

    public static string PragmaSQLOpenCommand
    {
      get
      {
        FileInfo fi = new FileInfo(Application.ExecutablePath);
        string tmp = fi.Directory + "\\PragmaSQL.exe";
        tmp = "\"" + tmp + "\"" + " \"%1\"";
        return tmp;
      }
    }

  }
}
