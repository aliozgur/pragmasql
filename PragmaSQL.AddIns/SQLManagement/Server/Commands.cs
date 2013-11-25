using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public class NewConnectionFromListCommand : CommandBase
  {
    public override void Run( )
    {
      HostServicesSingleton.HostServices.ObjectExplorerService.ExecuteCommand(ObjectExplorerCommand.NewConnectionFromList);
    }
  }

  public class NewConnectionCommand : CommandBase
  {
    public override void Run( )
    {
      base.Run();
      HostServicesSingleton.HostServices.ObjectExplorerService.ExecuteCommand(ObjectExplorerCommand.NewConnection);
    }
  }

  public class ShowServerInfoCommand : CommandBase
  {
    public override void Run()
    {
      base.Run();
      frmServerInfo.ShowServerInfo();
    }
  }

  public class ShowServerProcessesCommand : CommandBase
  {
    public override void Run()
    {
      base.Run();
      frmServerProcesses.ShowServerProcesses();
    }
  }

  public class ShowServerLocksCommand : CommandBase
  {
    public override void Run()
    {
      base.Run();
      frmServerLocks.ShowServerLocks();
    }
  }

  public class ShowBlockingProcessesCommand : CommandBase
  {
    public override void Run( )
    {
      base.Run();
      frmBlockingProcesses.ShowBlockingProcessesLocks();
    }
  }

  public class ShowLoginsCommand : CommandBase
  {
    public override void Run( )
    {
      base.Run();
      frmLogins.ShowLogins();
    }
  }
}
