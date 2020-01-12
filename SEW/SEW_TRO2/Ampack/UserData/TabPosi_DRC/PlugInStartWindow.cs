using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
using SEW.Defines;
using SEW.TechEditor.TechEditorBasis;
using SEW.Framework.PlugInDefines;
using AppBuilderDeviceAdapter;
using SEW.SEWControls.ControlRe.UserAccess;

namespace AppBuilder.NET
{
  public partial class Monitor_TabellenPositionierung_Bauer_PlugInStartWindow : PlugInBaseFrame, ISEWPlugIn
  {
    #region Definitionen

    /// <summary>
    /// Zugriff auf alle �bergebenen PlugIn Argumente
    /// </summary>
    public Dictionary<string, object> PlugInArgs;

    private PlugInUserControl plugInWindow = null;

    #endregion

    #region Konstruktor

    /// <summary>
    /// Konstruktor
    /// </summary>
    /// <param name="plugArgs"></param>
    public Monitor_TabellenPositionierung_Bauer_PlugInStartWindow(Dictionary<string, object> plugArgs)
    {
      string secosIPAddress = "";
      NodeInfo nodeInfo = null;
      CultureInfo cultureInfo = null;
      string devicePath = "";

      PlugInArgs = plugArgs;

      if (PlugInArgs.ContainsKey("NodeInfo"))
      {
        nodeInfo = (NodeInfo)PlugInArgs["NodeInfo"];
      }

      if (PlugInArgs.ContainsKey("Culture"))
      {
        cultureInfo = (CultureInfo)PlugInArgs["Culture"];
      }

      if (PlugInArgs.ContainsKey("SecosIP"))
      {
        secosIPAddress = (string)PlugInArgs["SecosIP"];
      }

      if (PlugInArgs.ContainsKey("DevicePath"))
      {
        devicePath = (string)PlugInArgs["DevicePath"];
      }

      UserInformation userInfo = MotionStudioProjectFunctions.LogIn(Assembly.GetExecutingAssembly().Location);

      if (userInfo == null)
      {
        this.Caption = "No access rights for this plugin!";
        return;
      }

      plugInWindow = new PlugInUserControl(nodeInfo, cultureInfo, secosIPAddress, devicePath, userInfo);
      plugInWindow.Dock = DockStyle.Fill;
      this.AddComponent(plugInWindow);
      
      if (cultureInfo != null)
      {
        if(cultureInfo.TwoLetterISOLanguageName.ToUpper() == "DE")
          this.Caption = plugInWindow.TextDE;
        else if (cultureInfo.TwoLetterISOLanguageName.ToUpper() == "FR")
          this.Caption = plugInWindow.TextFR;
        else
          this.Caption = plugInWindow.TextEN;
      }
    }

    #endregion

    #region Dispose

    /// <summary> 
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gel�scht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      OnWriteStatus = null;
      OnGenericRequest = null;
      OnActivated = null;
      OnFinished = null;
      OnWindowToTab = null;
      OnTabToWindow = null;
      OnDeactivated = null;

      ChangeToTabEvent = null;
      ChangeToTabPerformedEvent = null;
      ChangeToWindowEvent = null;
      ChangeToWindowPerformedEvent = null;
      FinishEvent = null;
      DeactivateEvent = null;
      ActivateEvent = null;
      GenericCallEvent = null;

      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #endregion

    #region Vom Komponenten-Designer generierter Code

    /// <summary> 
    /// Erforderliche Methode f�r die Designerunterst�tzung. 
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
    /// </summary>
    private void InitializeComponent()
    {
      this.SuspendLayout();
      // 
      // PlugInMainWindow
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "PlugInMainWindow";
      this.Size = new System.Drawing.Size(691, 430);
      this.ResumeLayout(false);

    }

    #endregion

    #region ISEWPlugIn Member

    public event SEW.Framework.PlugInDefines.WriteStatusEventHandler OnWriteStatus;
    public event SEW.Framework.PlugInDefines.GenericRequestEventHandler OnGenericRequest;
    public event SEW.Framework.PlugInDefines.ActivatedEventHandler OnActivated;
    public event SEW.Framework.PlugInDefines.FinishedEventHandler OnFinished;
    public event SEW.Framework.PlugInDefines.WindowToTabEventHandler OnWindowToTab;
    public event SEW.Framework.PlugInDefines.TabToWindowEventHandler OnTabToWindow;
    public event SEW.Framework.PlugInDefines.DeactivatedEventHandler OnDeactivated;

    public delegate void PlugInSimpleEventHandler();
    public delegate Dictionary<string, object> PlugInGenericCallEventHandler(string command, Dictionary<string, object> data);

    public event PlugInSimpleEventHandler ChangeToTabEvent = null;
    public event PlugInSimpleEventHandler ChangeToTabPerformedEvent = null;
    public event PlugInSimpleEventHandler ChangeToWindowEvent = null;
    public event PlugInSimpleEventHandler ChangeToWindowPerformedEvent = null;
    public event PlugInSimpleEventHandler FinishEvent = null;
    public event PlugInSimpleEventHandler DeactivateEvent = null;
    public event PlugInSimpleEventHandler ActivateEvent = null;
    public event PlugInGenericCallEventHandler GenericCallEvent = null;

    /// <summary>
    /// Wird nie aufgerufen (nur damit keine Warnungen kommen)
    /// </summary>
    public void Dummy()
    {
      OnWriteStatus(this, new StatusEventArgs(""));
      OnGenericRequest(this, new GenericRequestEventArgs("", new Dictionary<string, object>()));
    }

    public void ChangeToTab()
    {
      if (ChangeToTabEvent != null)
        ChangeToTabEvent();

      OnWindowToTab(this, new System.EventArgs());
    }

    public void ChangeToTabPerformed()
    {
      if (ChangeToTabPerformedEvent != null)
        ChangeToTabPerformedEvent();
    }

    public void ChangeToWindow()
    {
      if (ChangeToWindowEvent != null)
        ChangeToWindowEvent();

      OnTabToWindow(this, new System.EventArgs());
    }

    public void ChangeToWindowPerformed()
    {
      if (ChangeToWindowPerformedEvent != null)
        ChangeToWindowPerformedEvent();
    }

    public void Finish()
    {
      if (FinishEvent != null)
        FinishEvent();

      OnFinished(this, new System.EventArgs());
    }

    public Dictionary<string, object> GetPlugInData()
    {
      return null;
    }

    public void SetDataPool(IDataPoolBase dataPool)
    {
      // TODO:  Implementierung von SewCopy.SetDataPool hinzuf�gen
    }

    public void Deactivate()
    {
      if (DeactivateEvent != null)
        DeactivateEvent();

      OnDeactivated(this, new System.EventArgs());
    }

    public void Activate()
    {
      if (ActivateEvent != null)
        ActivateEvent();
      
      OnActivated(this, new System.EventArgs());
    }

    public void SetVisibleArea(Rectangle rect, List<Rectangle> rects)
    {
      
    }

    public Dictionary<string, object> GenericCall(string command, Dictionary<string, object> data)
    {
      if (GenericCallEvent != null)
        return GenericCallEvent(command, data);

      return null;
    }

    /// <summary>
    /// Verbieten/Zulassen des Schlie�ens des PlugIns
    /// </summary>
    public void SetCanClosePlugIn(string CantCloseMessage, bool CanClose)
    {
      if (!CanClose)
      {
        // Schliessen verbieten
        Dictionary<string, object> e_args = new Dictionary<string, object>();
        e_args.Add("Message", CantCloseMessage);
        GenericRequestEventArgs e = new GenericRequestEventArgs("NoFinishPossible", e_args);
        if (OnGenericRequest != null)
        {
          OnGenericRequest(this, e);
        }
      }
      else
      {
        // Schliessen zulassen
        if (OnGenericRequest != null)
        {
          OnGenericRequest(this, new GenericRequestEventArgs("FinishPossible", null));
        }
      }
    }

    /// <summary>
    /// Send GenericRequest
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void PlugInWizardOnGenericRequest(object sender, GenericRequestEventArgs e)
    {
      OnGenericRequest(this, e);
    }

    #endregion

    #region Functions

    /// <summary>
    /// Close the PlugIn
    /// </summary>
    public void ClosePlugIn()
    {
      OnGenericRequest(this, new GenericRequestEventArgs("FinishMe", null));
    }

    #endregion
  }
}