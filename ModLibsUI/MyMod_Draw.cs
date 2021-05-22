using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.TModLoader;
using ModLibsUI.Services.UI.FreeHUD;
using ModLibsUI.Services.UI.LayerDisable;


namespace ModLibsUI {
	/// @private
	partial class ModLibsUIMod : Mod {
		public override void UpdateUI( GameTime gameTime ) {
			FreeHUD.Instance?.UIContext?.Update( gameTime );
		}


		////////////////

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			var loadLibs = ModContent.GetInstance<LoadLibraries>();
			if( loadLibs == null ) { return; }

			//

			var layerDisable = LayerDisable.Instance;

			foreach( GameInterfaceLayer layer in layers ) {
				if( layerDisable.DisabledLayers.Contains(layer.Name) ) {
					layer.Active = false;
				}
			}

			//

			int idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Mouse Text" ) );
			if( idx == -1 ) { return; }

			//

			GameInterfaceDrawMethod hudDrawCallback = () => {
				FreeHUD.Instance?.UIContext.Draw( Main.spriteBatch, Main._drawInterfaceGameTime );
				return true;
			};

			//

			if( LoadLibraries.IsCurrentPlayerInGame() ) {
				var hudLayer = new LegacyGameInterfaceLayer( "ModLibsUI: HUD UI",
					hudDrawCallback,
					InterfaceScaleType.UI );
				layers.Insert( idx, hudLayer );
			}
		}
	}
}
