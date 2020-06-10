using Smod2.API;

namespace Smod2.Permissions
{
	public interface IPermissionsHandler
	{
		/// <summary>
		/// Queries a plugin for player permissions.
		/// </summary>
		/// <param name="player">The player to check the permission of.</param>
		/// <param name="permissionName">The name of the permission to check.</param>
		/// <returns>
		/// -1 for negative permission. (Stops other handlers from allowing it)
		/// 0 for no permission.
		/// 1 for positive permission.
		/// </returns>
		short CheckPermission(Player player, string permissionName);
	}
}
