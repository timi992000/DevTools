using DevTools.Core.Exceptions;
using DevTools.Core.Extender;
using DevTools.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Config = System.Configuration.Configuration;

namespace DevTools.Core.Configuration;
public class DevToolsConfiguration
{
	#region - needs -
	private static readonly string USER_CONFIG_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DevTools", "User.config");
	#endregion

	#region - ctor -
	private DevToolsConfiguration()
	{
		m_DefaultConfig = ConfigurationManager.OpenExeConfiguration("App.config");
		m_UserConfig = ConfigurationManager.OpenExeConfiguration(USER_CONFIG_PATH);
	}
	#endregion

	#region - Instance -
	private static DevToolsConfiguration? m_Configuration { get; set; }
	public static DevToolsConfiguration Instance => m_Configuration ??= new DevToolsConfiguration();
	#endregion

	#region - properties -

	#region - private properties -
	private Config? m_DefaultConfig { get; set; }
	private Config? m_UserConfig { get; set; }
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

		if(m_UserConfig is null)
			throw new NullReferenceException(nameof(m_UserConfig));

		if (m_UserConfig.AppSettings.Settings.AllKeys.Any(x => x.Equals(key)))
			throw new KeyAlreadyExistsException(key);

		m_UserConfig.AppSettings.Settings.Add(key, value);
    }
	#endregion

	#region [Get]
	public DevOperationResult<string> Get(string key)
	{
        ArgumentNullException.ThrowIfNull(key, nameof(key));

        if (key.IsNullEmptyOrWhitespace())
            throw new ArgumentException("String can't be empty or only containing whitespaces.", nameof(key));

		if(m_UserConfig is null)
		{
			if (m_DefaultConfig is null)
				throw new Exception("No configuration is present.");

			if (!m_DefaultConfig.AppSettings.Settings.AllKeys.Any(x => x.IsEquals(key)))
				return DevOperationResult<string>.Fail($"The key '{key}' doesn't exists.");

			return DevOperationResult<string>.Ok(m_DefaultConfig.AppSettings.Settings[key].Value);
		}

        if (!m_UserConfig.AppSettings.Settings.AllKeys.Any(x => x.IsEquals(key)))
            return DevOperationResult<string>.Fail($"The key '{key}' doesn't exists.");

        return DevOperationResult<string>.Ok(m_UserConfig.AppSettings.Settings[key].Value);
    }
    #endregion

    #endregion

    #endregion

}
