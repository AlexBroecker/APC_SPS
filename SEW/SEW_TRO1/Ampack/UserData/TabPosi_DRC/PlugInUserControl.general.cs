
// ------------------------------------------------------------------------
// Filename    : C:\Users\DEWALKAT\Documents\SEW\MotionStudio\AppBuilderProjects\TabellenPosi\PlugInUserControl.general.cs
// ------------------------------------------------------------------------
// Project     : ApplicationBuilder.NET Project
// Description : Autogenerated ApplicationBuilder event handler
// ------------------------------------------------------------------------
// Author      : autogenerated
// Date        : Dienstag, 19. Juni 2018
// ------------------------------------------------------------------------
// Verweise
namespace AppBuilder.NET
{
  public partial class PlugInUserControl
  {
    
    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      CloseEvent();

      if (disposing && (components != null))
      {
        components.Dispose();
      }

      if (mmsFunctions != null)
      {
        mmsFunctions.CloseCommunication();
      }

      base.Dispose(disposing);
    }


    private AppBuilderDeviceAdapter.MotionStudioProjectFunctions mmsFunctions = null;
    private SEW.SEWControls.ControlRe.AppBuilderSplashScreen splaceScreen = null;

    /// <summary>
    /// Initialisierung der Sew-Komponenten
    /// </summary>
    protected void SewComponentInit()
    {
      if(showSplashScreen)
      {
        splaceScreen = new SEW.SEWControls.ControlRe.AppBuilderSplashScreen();
        splaceScreen.TopMost = true;
        splaceScreen.AutoClose = true;
        splaceScreen.AutoCloseTime = 1000;
        splaceScreen.Show();
        System.Windows.Forms.Application.DoEvents();
      }

      TextDE = "TabellenPosiDRC";
      TextEN = "TabellenPosiDRC";
      TextFR = "TabellenPosiDRC";

      // Textmanager Path
      SEW.XMLDataServer.XMLDataManager.PathXMLLanguage = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, @"LanguageFiles");
      SEW.XMLDataServer.XMLDataManager.PathXMLParameter = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, @"Configuration\Parameter");

      // DataCollection for communication
      ApplicationBuilderExtender.DataCollection dataCollection = null;

      // List of MoviLinkbaseAdapter for communication
      System.Collections.Generic.List<AppBuilderDeviceAdapter.MoviLinkBaseAdapter> movilinkBaseAdapters = new System.Collections.Generic.List<AppBuilderDeviceAdapter.MoviLinkBaseAdapter>();

      // Find DataCollection and all MovilinkBaseAdapter
      // and support ISupportInitialize
      if (components != null && components.Components != null)
      {
        foreach (System.ComponentModel.Component c in this.components.Components)
        {
          if (c is AppBuilderDeviceAdapter.MoviLinkBaseAdapter)
          {
            movilinkBaseAdapters.Add(c as AppBuilderDeviceAdapter.MoviLinkBaseAdapter);
          }
          else if (c is ApplicationBuilderExtender.DataCollection)
          {
            dataCollection = c as ApplicationBuilderExtender.DataCollection;
            if(devicePath == null)
            {
              dataCollection.MMSDevicePath = dataCollection.MotionStudioProjectFile;
              if(dataCollection.MMSDevicePath != null && dataCollection.MMSDevicePath != "")
              {
                dataCollection.MMSDevicePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dataCollection.MMSDevicePath), "UserData");
              }
            }
            else
            {
              dataCollection.MMSDevicePath = devicePath;
            }
          }
          else if (c is ApplicationBuilderExtender.AppBuilderExtender)
          {
            ((ApplicationBuilderExtender.AppBuilderExtender)c).CheckLogIn(UserInfo);
          }

          // SupportInitialize ausführen
          if (c is System.ComponentModel.ISupportInitialize)
          {
            ((System.ComponentModel.ISupportInitialize)c).EndInit();
          }
        }
      }

      if (dataCollection != null)
      {
        if ((dataCollection.MotionStudioProjectFile == "" || dataCollection.MotionStudioProjectFile == null) && PlugInNodeInfo != null)
        {
          dataCollection.SetCultureInfo(PlugInCultureInfo);
          if(movilinkBaseAdapters != null)
          {
            foreach (AppBuilderDeviceAdapter.MoviLinkBaseAdapter mvlAdapt in movilinkBaseAdapters)
            {
              mvlAdapt.SewDataClient = PlugInNodeInfo.Client;
              mvlAdapt.InitializeCommunication(PlugInNodeInfo, secosIPAddress, devicePath);
            }
          }
        }
        else
        {
          System.Collections.Generic.List<string> DeviceNames = new System.Collections.Generic.List<string>();

          if(movilinkBaseAdapters != null)
          {
            // Collect all DeviceNames from Adapters
            foreach (AppBuilderDeviceAdapter.MoviLinkBaseAdapter mvlAdapt in movilinkBaseAdapters)
            {
              DeviceNames.Add(mvlAdapt.DeviceSignature);
            }

            // Create and initialize DataClients
            mmsFunctions = new AppBuilderDeviceAdapter.MotionStudioProjectFunctions(dataCollection.MotionStudioProjectFile);
            mmsFunctions.InitializeCommunication(DeviceNames.ToArray());

            // Initialize Adapters
            foreach (AppBuilderDeviceAdapter.MoviLinkBaseAdapter mvlAdapt in movilinkBaseAdapters)
            {
              mvlAdapt.InitializeCommunication(mmsFunctions.DataClients);
            }
          }
        }
      }
      if (splaceScreen != null)
      {
        splaceScreen.CloseWindowWithDelay();
      }
    }




  }
}