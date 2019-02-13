using System;
using System.Collections.Generic;
using System.Reflection;

namespace Smod2.Piping
{
	public class PluginPipes
	{
		private readonly Dictionary<string, FieldPipe> fields;
		private readonly Dictionary<string, PropertyPipe> properties;
		private readonly Dictionary<string, MethodPipe> methods;
		private readonly Dictionary<string, EventPipe> events;

		public PluginPipes(Plugin plugin)
		{
			Type pluginType = plugin.GetType();
			const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

			fields = new Dictionary<string, FieldPipe>();
			foreach (FieldInfo field in pluginType.GetFields(flags))
			{
				FieldPipe pipe = field.GetCustomAttribute<FieldPipe>();
				if (pipe != null)
				{
					pipe.Init(plugin, field);
					fields.Add(field.Name, pipe);
				}
			}

			properties = new Dictionary<string, PropertyPipe>();
			foreach (PropertyInfo property in pluginType.GetProperties(flags))
			{
				PropertyPipe pipe = property.GetCustomAttribute<PropertyPipe>();
				if (pipe != null)
				{
					pipe.Init(plugin, property);
					properties.Add(property.Name, pipe);
				}
			}

			methods = new Dictionary<string, MethodPipe>();
			events = new Dictionary<string, EventPipe>();
			foreach (MethodInfo method in pluginType.GetMethods(flags | BindingFlags.NonPublic))
			{
				if (method.IsPublic)
				{
					MethodPipe methodPipe = method.GetCustomAttribute<MethodPipe>();
					if (methodPipe != null)
					{
						methodPipe.Init(plugin, method);
						methods.Add(method.Name, methodPipe);

						continue;
					}
				}

				EventPipe eventPipe = method.GetCustomAttribute<EventPipe>();
				if (eventPipe != null)
				{
					eventPipe.Init(plugin, method);
					events.Add(method.Name, eventPipe);
				}
			}
		}

		private static T[] DuplicateCollection<T>(ICollection<T> collection)
		{
			T[] result = new T[collection.Count];
			int i = 0;

			foreach (T element in collection)
			{
				result[i] = element;

				i++;
			}

			return result;
		}

		public bool HasField(string name) => fields.ContainsKey(name);
		public FieldPipe GetField(string name) => fields[name];
		public FieldPipe[] GetFields() => DuplicateCollection(fields.Values);

		public bool HasProperty(string name) => properties.ContainsKey(name);
		public PropertyPipe GetProperty(string name) => properties[name];
		public PropertyPipe[] GetProperties() => DuplicateCollection(properties.Values);

		public bool HasMethod(string name) => methods.ContainsKey(name);
		public MethodPipe GetMethod(string name) => methods[name];
		public MethodPipe[] GetMethods() => DuplicateCollection(methods.Values);

		public bool HasEvent(string name) => events.ContainsKey(name);
		public EventPipe GetEvent(string name) => events[name];
		public EventPipe[] GetEvents() => DuplicateCollection(events.Values);
	}
}
