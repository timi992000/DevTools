using DevTools.Core.Commands;
using DevTools.Core.Extender;
using DevTools.Core.OperationResult;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DevTools.Core.BaseClasses
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		private const string EXECUTE_PREFIX = "Execute_";
		private const string CANEXECUTE_PREFIX = "CanExecute_";

		public event PropertyChangedEventHandler PropertyChanged;
		private readonly ConcurrentDictionary<string, object> _properties;
		private readonly ConcurrentDictionary<string, object> _values;

		private readonly List<string> _commandNames;
		private IDictionary<String, MethodInfo> _MyMethods;

		public ViewModelBase()
		{
			_properties = new ConcurrentDictionary<string, object>();
			_commandNames = new List<string>();

			Type MyType = GetType();
			_values = new ConcurrentDictionary<string, object>();
			__GetMembersAndGenerateCommands(MyType);
		}

		public object this[string key]
		{
			get
			{
				if (_values.ContainsKey(key))
					return _values[key];
				return null;
			}
			set
			{
				_values[key] = value;
				OnPropertyChanged(key);
			}
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#region Get
		protected T Get<T>(Expression<Func<T>> expression)
		{
			return Get<T>(__GetPropertyName(expression));
		}
		protected T Get<T>(Expression<Func<T>> expression, T defaultValue)
		{
			return Get(__GetPropertyName(expression), defaultValue);
		}

		protected T Get<T>(T defaultValue, [CallerMemberName] string propertyName = null)
		{
			return Get(propertyName, defaultValue);
		}
		protected T Get<T>([CallerMemberName] string name = null)
		{
			return Get(name, default(T));
		}

		protected T Get<T>(string name, T defaultValue)
		{
			return GetValueByName<T>(name, defaultValue);
		}

		protected T GetValueByName<T>(String name, T defaultValue)
		{

			if (_properties.TryGetValue(name, out var val))
				return (T)val;

			return defaultValue;
		}
		#endregion

		#region [Set]

		protected void Set<T>(Expression<Func<T>> expression, T value)
		{
			Set(__GetPropertyName(expression), value);
		}

		protected void Set<T>(T value, [CallerMemberName] string propertyName = "")
		{
			Set(propertyName, value);
		}

		public void Set<T>(string name, T value)
		{
			if (_properties.TryGetValue(name, out var val))
			{
				if (val == null && value == null)
					return;

				if (val != null && val.Equals(value))
					return;
			}
			_properties[name] = value;
			this[name] = value;
			OnPropertyChanged(name);
		}
		#endregion

		public void ValidateResult(IDevOperationResult result, [CallerMemberName] string methodName = "")
		{
			if (result == null)
				throw new ArgumentNullException(methodName + " failed!");
			if (!result.Succeeded || result.Messages.Any())
				throw new Exception(string.Join("\n", result.Messages));
		}

		public static List<string> GetMessage(Exception ex)
		{
			var messages = new List<string>();
			messages.Add(ex.ToStringValue());
			messages.Add(ex.Message);
			return messages;
		}

		public static List<string> GetMessage(string message)
		{
			return new List<string> { message };
		}

		public void ShowErrorMessage(Exception ex, [CallerMemberName] string methodName = "")
			=> ShowErrorMessage(ex, null, methodName);
		public void ShowErrorMessage(Exception ex, MetroWindow metroWindow, [CallerMemberName] string methodName = "")
		{
			ShowMessage(ex.ToStringValue(), $"Operation {methodName} failed! {DateTime.Now}", metroWindow);
		}

		public void ShowMessage(string message, MetroWindow metroWindow)
			=> ShowMessage(message, "", metroWindow);
		public void ShowMessage(string message, string title, MetroWindow metroWindow)
		{
			if (metroWindow != null)
				metroWindow.ShowMessageAsync(title, message);
			else
				MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private string __GetPropertyName<T>(Expression<Func<T>> expression)
		{
			var memberExpression = expression.Body as MemberExpression;
			if (memberExpression == null)
				throw new ArgumentException($"{nameof(expression)} must be a property {nameof(expression)}");

			return memberExpression.Member.Name;
		}

		private void __GetMembersAndGenerateCommands(Type myType)
		{
			var MethodInfos = new Dictionary<String, MethodInfo>(StringComparer.InvariantCultureIgnoreCase);
			foreach (var method in myType.GetMethods())
			{
				if (method.Name.StartsWith(EXECUTE_PREFIX))
					_commandNames.Add(method.Name.Substring(EXECUTE_PREFIX.Length));
				MethodInfos[method.Name] = method;
			}
			foreach (var property in myType.GetProperties())
			{
				this[property.Name] = property;
			}
			_commandNames.ForEach(n => Set(n, new RelayCommand(p => __ExecuteCommand(n, p), p => __CanExecuteCommand(n, p))));
			_MyMethods = MethodInfos;
		}

		private void __ExecuteCommand(string name, object parameter)
		{
			MethodInfo methodInfo;
			_MyMethods.TryGetValue(EXECUTE_PREFIX + name, out methodInfo);
			if (methodInfo == null) return;

			methodInfo.Invoke(this, methodInfo.GetParameters().Length == 1 ? new[] { parameter } : null);
		}

		private bool __CanExecuteCommand(string name, object parameter)
		{

			MethodInfo methodInfo;
			_MyMethods.TryGetValue(CANEXECUTE_PREFIX + name, out methodInfo);
			if (methodInfo == null) return true;

			return (bool)methodInfo.Invoke(this, methodInfo.GetParameters().Length == 1 ? new[] { parameter } : null);
		}


	}
}
