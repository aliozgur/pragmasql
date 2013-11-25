using System;
using System.Collections.Generic;
using System.Text;
using PragmaSQL.Core;
using ICSharpCode.Core;

namespace SQLManagement
{
  public  class CommandBase: ICommand
  {
    object owner = null;

    /// <summary>
    /// Returns the owner of the command.
    /// </summary>
    public virtual object Owner
    {
      get
      {
        return owner;
      }
      set
      {
        owner = value;
        OnOwnerChanged(EventArgs.Empty);
      }
    }

    /// <summary>
    /// Invokes the command.
    /// </summary>
    public virtual void Run()
    {
      if (HostServicesSingleton.HostServices.HostEdition == HostEditionType.Personal)
        throw new PersonalEditionLimitation();
    }


    protected virtual void OnOwnerChanged(EventArgs e)
    {
      if (OwnerChanged != null)
      {
        OwnerChanged(this, e);
      }
    }

    public event EventHandler OwnerChanged;
  }

  public abstract class MenuCommandBase : CommandBase, IMenuCommand
  {
    bool isEnabled = true;

    public virtual bool IsEnabled
    {
      get
      {
        return isEnabled;
      }
      set
      {
        isEnabled = value;
      }
    }
  }
}
