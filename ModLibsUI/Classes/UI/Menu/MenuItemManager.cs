using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Hooks.LoadHooks;


namespace ModLibsUI.Classes.UI.Menu {
	class MenuItemManager {
		internal IDictionary<string, MenuItem> Items = new Dictionary<string, MenuItem>();

		

		////////////////

		public MenuItemManager() {
			if( !Main.dedServ ) {
				Main.OnPostDraw += MenuItemManager._Draw;
			}

			LoadHooks.AddModUnloadHook( () => {
				try {
					if( !Main.dedServ ) {
						Main.OnPostDraw -= MenuItemManager._Draw;
					}
				} catch { }
			} );
		}

		////////////////

		private static void _Draw( GameTime gameTime ) {	// <- Just in case references are doing something funky...
			ModContent.GetInstance<MenuItemManager>()?.Draw( gameTime );
		}

		private void Draw( GameTime gameTime ) {
			foreach( MenuItem item in this.Items.Values.ToArray() ) {
				if( item.MenuContext == Main.menuMode ) {
					item.Draw();
				}
			}
		}
	}
}
