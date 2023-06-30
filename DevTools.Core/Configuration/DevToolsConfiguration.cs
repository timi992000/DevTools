using DevTools.Core.Extender;
using DevTools.Core.OperationResult;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Config = System.Configuration.Configuration;

namespace DevTools.Core.Configuration;
public class DevToolsConfiguration
{
  #region - needs -
  private static readonly string USER_CONFIG_DIR = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DevTools");

  private static readonly string USER_CONFIG_PATH = Path.Combine(USER_CONFIG_DIR, "User.config");
  private static readonly string DEFAULT_CONFIG_PATH = Path.Combine(Environment.CurrentDirectory, "App.config");
  private static readonly string DEFAULT_USER_CONFIG = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<configuration>\n\t<appSettings>\n\t</appSettings>\n</configuration>";
  #endregion

  #region - ctor -
  private DevToolsConfiguration()
  {
    ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
    fileMap.ExeConfigFilename = DEFAULT_CONFIG_PATH;

    m_DefaultConfig = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

    if (!File.Exists(USER_CONFIG_PATH))
      __CreateUserConfig();

    ExeConfigurationFileMap userFileMap = new ExeConfigurationFileMap();
    userFileMap.ExeConfigFilename = USER_CONFIG_PATH;
    m_UserConfig = ConfigurationManager.OpenMappedExeConfiguration(userFileMap, ConfigurationUserLevel.None);
  }
  #endregion

  #region - Instance -
  private static DevToolsConfiguration? m_Configuration { get; set; }
  public static DevToolsConfiguration Instance => m_Configuration ??= new DevToolsConfiguration();
  #endregion

  #region - properties -

  #region - private properties -
  private Config? m_DefaultConfig { get; set; } = null;
  private Config? m_UserConfig { get; set; } = null;
  #endregion

  #endregion

  #region - methods -

  #region - public methods -

  #region [Set]
  public void Set(string key, string value)
  {
    ArgumentNullException.ThrowIfNull(key, nameof(key));
    ArgumentNullException.ThrowIfNull(value, nameof(value));

    if (key.IsNullEmptyOrWhitespace())
      throw new ArgumentException("String can't be empty or only containing whitespaces.", nameof(key));

    if (value.IsNullEmptyOrWhitespace())
      throw new ArgumentException("String can't be empty or only containing whitespaces.", nameof(value));

    if (m_UserConfig.AppSettings.Settings.AllKeys.Any(x => x.Equals(key)))
      m_UserConfig.AppSettings.Settings.Remove(key);

    m_UserConfig.AppSettings.Settings.Add(key, value);
    m_UserConfig.Save();
  }
  #endregion

  #region [Get]
  public DevOperationResult<string> Get(string key)
  {
    ArgumentNullException.ThrowIfNull(key, nameof(key));

    if (key.IsNullEmptyOrWhitespace())
      throw new ArgumentException("String can't be empty or only containing whitespaces.", nameof(key));

    if (!m_UserConfig.AppSettings.Settings.AllKeys.Any(x => x.IsEquals(key)))
    {
      if (m_DefaultConfig is null)
        throw new Exception("No configuration is present.");

      if (!m_DefaultConfig.AppSettings.Settings.AllKeys.Any(x => x.IsEquals(key)))
        return DevOperationResult<string>.Fail($"The key '{key}' doesn't exists.");

      return DevOperationResult<string>.Ok(m_DefaultConfig.AppSettings.Settings[key].Value);
    }

    return DevOperationResult<string>.Ok(m_UserConfig.AppSettings.Settings[key].Value);
  }
  #endregion

  #endregion

  #region - private methods -

  #region [__CreateUserConfig]
  private void __CreateUserConfig()
  {
    if (!Directory.Exists(USER_CONFIG_DIR))
      Directory.CreateDirectory(USER_CONFIG_DIR);
    File.WriteAllText(USER_CONFIG_PATH, DEFAULT_USER_CONFIG);
  }
  #endregion

  #endregion

  #endregion

}
