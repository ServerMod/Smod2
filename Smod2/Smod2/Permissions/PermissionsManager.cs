using Smod2.API;
using System.Collections.Generic;

namespace Smod2.Permissions
{
    public class PermissionsManager
    {
        // TODO: Add plugin to remove handlers on plugin disable
        private List<IPermissionsHandler> permissionHandlers;

        bool RegisterHandler(IPermissionsHandler handler)
        {
            if(handler == null)
            {
                PluginManager.Manager.Logger.Error("PERMISSIONS_MANAGER","Failed to add Permissions Handler as it was null.");
                return false;
            }
            permissionHandlers.Add(handler);
            PluginManager.Manager.Logger.Debug("PERMISSIONS_MANAGER", "Added new permissions handler.");
            return true;
        }

        void UnregisterHandler(IPermissionsHandler handler)
        {
            permissionHandlers.Remove(handler);
        }

        bool CheckPermission(Player player, string permissionName)
        {
            bool allowed = false;
            foreach(IPermissionsHandler handler in permissionHandlers)
            {
                // Checks each permission handler, aborts if this permission node is negative
                switch(handler.CheckPermission(player, permissionName))
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