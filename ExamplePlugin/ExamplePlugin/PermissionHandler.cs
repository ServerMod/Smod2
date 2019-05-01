using System;
using System.Collections.Generic;
using System.Linq;
using Smod2.API;
using Smod2.Permissions;

namespace ExamplePlugin
{
	// Note: This is only used if you are making a permission plugin, you do NOT need any of this to check player permissions.
	internal class PermissionHandler : IPermissionsHandler
	{
		// This will be called whenever any plugin checks if a user has a permission node
		public short CheckPermission(Player player, string permissionName)
		{
			// Imagine this as a list of server ranks and their permissions which we have read from a file or database.
			Dictionary<string, string[]> ranks = new Dictionary<string, string[]>
			{
				{ "admin", new[]
					{
						"exampleplugin.lotto",
						"exampleplugin.admin",
						"exampleplugin.mod",
						"exampleplugin.helloworld"
					}
				},
				{ "moderator", new[]
					{
						"exampleplugin.lotto",
						"exampleplugin.mod",
						"exampleplugin.helloworld"
					}
				}
			};

			// Imagine this as a list of all users who have special ranks, also read from a file or database.
			// Each user only has a single rank in this example but you could of course allow for several ranks per user in your plugin.
			Dictionary<string, string> users = new Dictionary<string, string>
			{
				{ "00000000000000000", "admin" },
				{ "11111111111111111", "moderator" },
				{ "22222222222222222", "datboi" }
			};

			// Check if the player has a rank.
			if (users.ContainsKey(player.SteamId))
			{
				// Check if the rank exists, steam id 2 above would fail this check as their rank does not exist.
				if (ranks.ContainsKey(users[player.SteamId]))
				{
					// Check if the user's rank has this permission listed.
					if (ranks[users[player.SteamId]].Contains(permissionName))
					{
						// Returns positive to indicate they have the permission.
						return 1;
					}

					// Permission plugins can also support negative permissions in order to negate permissions which are given by default or by other permission handlers
					if (ranks[users[player.SteamId]].Contains("-" + permissionName))
					{
						// Returns negative to forbid all permission handlers from allowing this permission
						return -1;
					}
				}
			}

			// Returns neutral to indicate this player doesn't have this permission node
			return 0;
		}
	}
}
