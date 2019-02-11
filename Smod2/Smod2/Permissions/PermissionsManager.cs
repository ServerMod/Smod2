using Smod2.API;
using System.Collections.Generic;

namespace Smod2.Permissions
{
    public class PermissionsManager
    {
        // TODO: Switch to something with unique members
        private HashSet<IPermissionsHandler> permissionHandlers = new HashSet<IPermissionsHandler>();

        public bool RegisterHandler(IPermissionsHandler handler)
        {
            if (handler == null)
            {
                PluginManager.Manager.Logger.Error("PERMISSIONS_MANAGER", "Failed to add Permissions Handler as it was null.");
                return false;
            }

            if(permissionHandlers.Add(handler))
            {
                PluginManager.Manager.Logger.Debug("PERMISSIONS_MANAGER", "Added new permissions handler.");
                return true;
            }

            PluginManager.Manager.Logger.Warn("PERMISSIONS_MANAGER", "Attempted to add duplicate Permissions Handler.");
            return false;
        }

        public void UnregisterHandler(IPermissionsHandler handler)
        {
            permissionHandlers.Remove(handler);
        }

        public bool CheckPermission(Player player, string permissionName)
        {
            bool allowed = false;
            foreach (IPermissionsHandler handler in permissionHandlers)
            {
                // Checks each permission handler, aborts if this permission node is negative
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
}