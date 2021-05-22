using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace ModLibsUI {
	/// @private
	partial class ModLibsUIMod : Mod {
		public static ModLibsUIMod Instance { get; private set; }



		////////////////

		public override void Load() {
			ModLibsUIMod.Instance = this;
		}

		////

		public override void Unload() {
			try {
				LogLibraries.Alert( "Unloading mod..." );
			} catch { }

			ModLibsUIMod.Instance = null;
		}


		////////////////

		//public override void UpdateMusic( ref int music ) { //, ref MusicPriority priority
		//	this.MusicHelpers.UpdateMusic();
		//}
	}
}
