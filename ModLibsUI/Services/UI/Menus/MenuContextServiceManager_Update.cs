﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsUI.Classes.UI.Menus;
using ModLibsUI.Libraries.TModLoader.Menus;


namespace ModLibsUI.Services.UI.Menus {
	partial class MenuContextServiceManager {
		private static void _Update( GameTime gametime ) {   // <- Just in case references are doing something funky...
			var menuCtxMngr = ModContent.GetInstance<MenuContextServiceManager>();

			menuCtxMngr?.Update();
		}

		private void Update() {
			UIState ui = Main.MenuUI.CurrentState;
			string prevUiName, currUiName;

			if( this.CurrentMenuUI == 0 ) {
				prevUiName = null;
			} else {
				MenuUIDefinition prevUiDef = this.CurrentMenuUI;
				prevUiName = Enum.GetName( typeof(MenuUIDefinition), prevUiDef );
			}

			currUiName = ui?.GetType().Name;

			if( prevUiName == currUiName ) {
				return;
			}
			
			this.SwitchToUI( ui );
		}


		////////////////

		private void SwitchToUI( UIState ui ) {
			MenuUIDefinition openingUiDef = 0;
			MenuUIDefinition closingUiDef = this.CurrentMenuUI;

			// Out with the old
			if( closingUiDef != 0 && this.Contexts.ContainsKey(closingUiDef) ) {
				foreach( (string ctxName, MenuContext ctx) in this.Contexts[closingUiDef] ) {
					ctx.Hide( MainMenuLibraries.GetMenuUI(closingUiDef) );
				}
			}

			// Validate
			if( ui != null ) {
				if( !Enum.TryParse( ui.GetType().Name, out openingUiDef ) ) {
					if( ModLibsUIConfig.Instance.DebugModeMenuInfo ) {
						LogLibraries.WarnOnce( "Could not get MenuUIDefinition " + ui.GetType().Name );
					}
					this.CurrentMenuUI = 0;
					return;
				}
			} else {
				this.PreviousMenuUI = this.CurrentMenuUI;
				this.CurrentMenuUI = 0;
			}
			
			// In with the new
			if( this.Contexts.ContainsKey( openingUiDef ) ) {
				foreach( MenuContext ctx in this.Contexts[openingUiDef].Values.ToArray() ) {
					ctx.ActivateIfInactive( ui );
					ctx.Show( ui );
				}
			}

			this.PreviousMenuUI = this.CurrentMenuUI;
			this.CurrentMenuUI = openingUiDef;
		}
	}
}
