using Smod2.API;
using System.Collections.Generic;

namespace Smod2.Permissions
{
	public class PermissionsManager
	{
		private HashSet<IPermissionsHandler> permissionHandlers = new HashSet<IPermissionsHandler>();

		private readonly DefaultPermissionsHandler defaultPermissionsHandler = new DefaultPermissionsHandler();

		public PermissionsManager()
		{
			// Immediately registers the default permissions handler
			RegisterHandler(defaultPermissionsHandler);
		}

		// Used by permission plugins to register themselves as permission handlers
		public bool RegisterHandler(IPermissionsHandler handler)
		{
			if (handler == null)
			{
				PluginManager.Manager.Logger.Error("PERMISSIONS_MANAGER", "Failed to add permissions handler as it was null.");
				return false;
			}

			if(permissionHandlers.Add(handler))
			{
				return true;
			}

			PluginManager.Manager.Logger.Warn("PERMISSIONS_MANAGER", "Attempted to add duplicate permissions handler.");
			return false;
		}

		public void UnregisterHandler(IPermissionsHandler handler)
		{
			permissionHandlers.Remove(handler);
		}

		// Used by plugins to specicy permissions which should be given to everyone by default
		public bool RegisterDefaultPermission(string permissionName)
		{
			return defaultPermissionsHandler.AddPermission(permissionName);
		}

		public bool UnregisterDefaultPermission(string permissionName)
		{
			return defaultPermissionsHandler.AddPermission(permissionName);
		}

		public bool CheckPermission(Player player, string permissionName)
		{
			bool allowed = false;
			foreach (IPermissionsHandler handler in permissionHandlers)
			{
				// Checks each permission handler, aborts if this permission node is negative in any handler
				switch (handler.CheckPermission(player, permissionName))
				{
					case 1:
						allowed = true;
						break;

					case -1:
						return false;
				}
			}
			// True if any permission handler returns positively and none return negatively
			return allowed;
		}
	}

	// Plugins can register permissions which are given to everyone by default. Permission plugins can still counter these by returing a negative permission.
	public class DefaultPermissionsHandler : IPermissionsHandler
	{
		private HashSet<string> defaultPerms = new HashSet<string>();

		public short CheckPermission(Player player, string permissionName)
		{
			if (defaultPerms.Contains(permissionName))
			{
				return 1;
			}
			return 0;
		}

		public bool AddPermission(string permissionName)
		{
			if(permissionName == null || permissionName == "")
			{
				PluginManager.Manager.Logger.Warn("PERMISSIONS_MANAGER", "Attempted to add default permission but it was empty or null.");
				return false;
			}
			PluginManager.Manager.Logger.Debug("PERMISSIONS_MANAGER", "Added default permission '" + permissionName + "'.");
			return defaultPerms.Add(permissionName);
		}

		public bool RemovePermission(string permissionName)
		{
			if (permissionName == null || permissionName == "")
			{
				PluginManager.Manager.Logger.Warn("PERMISSIONS_MANAGER", "Attempted to remove default permission but string was empty or null.");
				return false;
			}
			PluginManager.Manager.Logger.Debug("PERMISSIONS_MANAGER", "Removed default permission '" + permissionName + "'.");
			return defaultPerms.Remove(permissionName);
		}
	}
}