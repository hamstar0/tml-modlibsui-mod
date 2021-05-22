using System;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using Terraria;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Hooks.LoadHooks;
using ModLibsCore.Libraries.XNA;


namespace ModLibsUI.Internals.Menus {
	/// @private
	class Menus : ILoadable {
		void ILoadable.OnModsLoad() {
			throw new NotImplementedException();
		}

		void ILoadable.OnPostModsLoad() {
			if( Main.dedServ ) { return; }

			LoadHooks.AddPostModLoadHook( () => {
				Menus.InitializeDebugModeMenuInfo();
			} );
		}

		void ILoadable.OnModsUnload() {
			throw new NotImplementedException();
		}


		////////////////

		private static bool DebugModeMenuInfoLoaded = false;

		private static void InitializeDebugModeMenuInfo() {
			if( !ModHelpersConfig.Instance.DebugModeMenuInfo ) { return; }

			Main.OnPostDraw += Menus.DebugModeMenuInfo;
			Menus.DebugModeMenuInfoLoaded = true;

			LoadHooks.AddModUnloadHook( () => {
				try {
					if( Menus.DebugModeMenuInfoLoaded ) {
						Main.OnPostDraw -= Menus.DebugModeMenuInfo;
					}
				} catch { }

				Menus.DebugModeMenuInfoLoaded = false;
			} );
		}


		private static void DebugModeMenuInfo( GameTime _ ) {
			bool __;
			XNASpritebatchLibraries.DrawBatch( ( sb ) => {
				sb.DrawString(
					Main.fontMouseText,
					Main.menuMode + "",
					new Vector2( Main.screenWidth - 32, Main.screenHeight - 32 ),
					Color.White
				);
			}, out __ );
		}
	}
}
