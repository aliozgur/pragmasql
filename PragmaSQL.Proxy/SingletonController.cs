using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Configuration;


namespace PragmaSQL.Proxy
{
  [Serializable]
  public class SingletonController : MarshalByRefObject
  {
    private static int _portNo = 1234;
    private static IpcChannel _ipcChannel = null;
    private static Mutex _mutex = null;

    public delegate void ReceiveDelegate( string[] args );

    static private ReceiveDelegate _receiver = null;
    static public ReceiveDelegate Receiver
    {
      get
      {
        return _receiver;
      }
      set
      {
        _receiver = value;
      }
    }

    
    static SingletonController()
    {   
      string p = ConfigurationManager.AppSettings["SingletonControllerIPCPort"] as string;
      if(p != null)
      {
        Int32.TryParse(p, out _portNo );
      }
    }

    public static bool IamFirst( ReceiveDelegate r )
    {
      if (IamFirst())
      {
        Receiver += r;
        return true;
      }
      else
      {
        return false;
      }
    }

    public static bool IamFirst( )
    {
      //string m_UniqueIdentifier;
      //string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).CodeBase;
      //m_UniqueIdentifier = assemblyName.Replace("\\", "_").ToLowerInvariant();
      string uniqueIdentifier = "PragmaSQL_35307BC2_A238_4afc_B3B9_60E3D5396F4E";
      _mutex = new Mutex(false, uniqueIdentifier);

      if (_mutex.WaitOne(1, true))
      {
        //We locked it! We are the first instance!!!    
        CreateInstanceChannel();
        return true;
      }
      else
      {
        //Not the first instance!!!
        _mutex.Close();
        _mutex = null;
        return false;
      }
    }

    public static void CreateInstanceChannel( )
    {
      if(_ipcChannel != null)
      {
        return;
      }

      _ipcChannel = new IpcChannel("localhost:" + _portNo.ToString());
      ChannelServices.RegisterChannel(_ipcChannel, false);
      RemotingConfiguration.RegisterWellKnownServiceType(
          Type.GetType("PragmaSQL.Proxy.SingletonController"),
          "SingletonController",
          WellKnownObjectMode.SingleCall);
    }


    public static void Cleanup( )
    {
      if (_mutex != null)
      {
        _mutex.Close();
      }

      if (_ipcChannel != null)
      {
        _ipcChannel.StopListening(null);
        ChannelServices.UnregisterChannel(_ipcChannel);
      }

      _mutex = null;
      _ipcChannel = null;
    }

    public static void Send( string[] s )
    {
      SingletonController ctrl;
      IpcChannel channel = new IpcChannel();
      ChannelServices.RegisterChannel(channel, false);
      try
      {
        ctrl = (SingletonController)Activator.GetObject(typeof(SingletonController), String.Format("ipc://localhost:{0}/SingletonController",_portNo));
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception: " + e.Message);
        throw;
      }
      ctrl.Receive(s);
    }

    public void Receive( string[] s )
    {
      if (_receiver != null)
      {
        _receiver(s);
      }
    }
  }
}
