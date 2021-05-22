using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace ModLibsUI {
	/// <summary>
	/// Defines Mod Libs config settings.
	/// </summary>
	[Label( "Mod Libs - UI Settings" )]
	public class ModHelpersConfig : ModConfig {
		/// <summary>
		/// Gets the stack-merged singleton instance of this config file.
		/// </summary>
		public static ModHelpersConfig Instance => ModContent.GetInstance<ModHelpersConfig>();



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
