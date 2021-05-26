using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace ModLibsUI {
	/// <summary>
	/// Defines config settings.
	/// </summary>
	[Label( "Mod Libs - UI - Settings" )]
	public class ModLibsUIConfig : ModConfig {
		/// <summary>
		/// Gets the stack-merged singleton instance of this config file.
		/// </summary>
		public static ModLibsUIConfig Instance => ModContent.GetInstance<ModLibsUIConfig>();



		////////////////

		/// @private
		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		/// <summary>
		/// Displays the current menu's ID in bottom right.
		/// </summary>
		[Label( "Debug Mode - Show Menu ID" )]
		[Tooltip( "Displays the current menu's ID in bottom right." )]
		public bool DebugModeMenuInfo { get; set; } = false;
	}
}
