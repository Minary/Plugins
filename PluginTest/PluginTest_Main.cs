namespace PluginTest
{
  using MinaryLib;
  using MinaryLib.Plugin;
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Reflection;
  using System.Windows.Forms;


  public partial class PluginTest_MainForm : Form
  {

    #region DATATYPE

    private struct PluginRecord
    {
      public IPlugin IPlugin;
      public UserControl UserControl;
      public TabPage TabPage;
    }

    #endregion


    #region MEMBERS

    private IPlugin loadedPlugin;
    private Dictionary<string, PluginRecord> pluginDict = new Dictionary<string, PluginRecord>();

    #endregion


    #region PUBLIC

    public PluginTest_MainForm()
    {
      InitializeComponent();
    }


    #endregion


    #region PRIVATE

    private void LoadModule(string pluginPath)
    {
      Type objType;
      Assembly assemblyObj;
      var fileName = Path.GetFileNameWithoutExtension(pluginPath);

      if ((assemblyObj = Assembly.LoadFrom(pluginPath)) == null)
      {
        return;
      }    
      

      var pluginName = $"Minary.Plugin.Main.{fileName}";
      objType = assemblyObj.GetType(pluginName, false, false);

      if (objType == null)
      {
        return;
      }

      var pluginProperties = new PluginProperties()
      {
        ApplicationBaseDir = Directory.GetCurrentDirectory(),
        PluginBaseDir = pluginPath,
        PatternSubDir = string.Empty,
        HostApplication = (IPluginHost)this
      };

      /*
       * Add loaded plugin to ...
       * - the global "plugin list" (IPlugin)
       * - the "used plugins DGV" list
       * - the "plugin position" list (name + position)
       */
      object tmpPluginObj = Activator.CreateInstance(objType, pluginProperties);
      if ((tmpPluginObj is IPlugin) == false || 
          (tmpPluginObj is UserControl) == false)
      {
        return;
      }

      try
      {
        var tmpPluginRec = new PluginRecord();
        tmpPluginRec.IPlugin = (IPlugin)tmpPluginObj;
        tmpPluginRec.UserControl = (UserControl)tmpPluginObj;
        tmpPluginRec.TabPage = new TabPage(tmpPluginRec.IPlugin.Config.PluginName);
        this.pluginDict.Add(tmpPluginRec.IPlugin.Config.PluginName, tmpPluginRec);

        // Initialize new tab page ...
        tmpPluginRec.TabPage.Controls.Add(tmpPluginRec.IPlugin.PluginControl);

        tmpPluginRec.TabPage.ImageIndex = (int)Status.NotRunning;
        tmpPluginRec.TabPage.BorderStyle = BorderStyle.None;

        // Let the plugin user control adapt its size when parent control (the tab control) resizes.
        tmpPluginRec.UserControl.Dock = DockStyle.Fill;

        // 
        //this.tp_Plugin = newPluginTabPage;

        this.loadedPlugin = tmpPluginRec.IPlugin;
        this.LogMessage($"Plugin {loadedPlugin.Config.PluginName} loaded");
        this.tb_PluginPath.Text = pluginPath;
        
        this.TC_PluginTester.Controls.Add(tmpPluginRec.TabPage);
        //this.tp_Plugin.Refresh();
        //this.TC_PluginTester.Refresh();
        //this.Refresh();

        // Initialize and register the plugin.
        this.loadedPlugin.OnInit();
      }
      catch (ArgumentNullException ex)
      {
        MessageBox.Show($"ArgumentNullException {fileName}: {ex.Message} {ex.StackTrace}");
      }
      catch (ArgumentException ex)
      {
        MessageBox.Show($"ArgumentException {fileName}: {ex.Message} {ex.StackTrace}");
      }
      catch (NotSupportedException ex)
      {
        MessageBox.Show($"NotSupportedException {fileName}: {ex.Message} {ex.StackTrace}");
      }
      catch (TargetInvocationException ex)
      {
        if (ex.InnerException != null)
        {
          MessageBox.Show($"TargetInvocationException {fileName}: {ex.Message} - {ex.InnerException.Message}");
        }
        else
        {
          MessageBox.Show($"TargetInvocationException {fileName}: {ex.Message} {ex.StackTrace}");
        }
      }
      catch (MethodAccessException ex)
      {
        MessageBox.Show($"MethodAccessException {fileName}: {ex.Message} {ex.StackTrace}");
      }
      catch (MemberAccessException ex)
      {
        MessageBox.Show($"MemberAccessException {fileName}: {ex.Message} {ex.StackTrace}");
      }
      catch (TypeLoadException ex)
      {
        MessageBox.Show($"TypeLoadException {fileName}: {ex.Message} {ex.StackTrace}");
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Exception {fileName}: {ex.Message} {ex.StackTrace}");
      }
    }

    #endregion

  }
}
