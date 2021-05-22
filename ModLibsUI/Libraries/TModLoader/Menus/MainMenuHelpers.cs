﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.Config;
using Terraria.UI;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Reflection;
using ModLibsCore.Services.Timers;
using ModLibsUI.Services.UI.Menus;


namespace ModLibsUI.Libraries.TModLoader.Menus {
	/// <summary>
	/// Assorted static "helper" functions pertaining to the main menu.
	/// </summary>
	public class MainMenuLibraries {
		/// <summary>
		/// Gets a menu UI corresponding to a given menu definition.
		/// </summary>
		/// <param name="menuDef"></param>
		/// <returns></returns>
		public static UIState GetMenuUI( MenuUIDefinition menuDef ) {
			string menuUiName = Enum.GetName( typeof(MenuUIDefinition), menuDef );

			UIState menuUi;
			ReflectionLibraries.Get( typeof(MenuUIs), null, menuUiName, out menuUi );

			return menuUi;
		}


		/// <summary>
		/// Switches to the list of mod configs menu.
		/// </summary>
		public static void OpenModConfigListUI() {
			Type interfaceType = ReflectionLibraries.GetMainAssembly()
				.GetType( "Terraria.ModLoader.UI.Interface" );

			if( !Main.gameMenu ) {
				IngameOptions.Close();
				IngameFancyUI.CoverNextFrame();
				Main.playerInventory = false;
				Main.editChest = false;
				Main.npcChatText = "";
				Main.inFancyUI = true;
			} else {
				if( !ReflectionLibraries.Get(interfaceType, null, "modConfigID", out Main.menuMode) ) {
					LogLibraries.Warn( "Could not get Interface.modConfigID" );
					return;
				}
			}

			UIState modConfigListInterfaceObj;
			if( !ReflectionLibraries.Get(interfaceType, null, "modConfigList", out modConfigListInterfaceObj)
					|| modConfigListInterfaceObj == null ) {
				LogLibraries.Warn( "Could not get Interface.modConfigList" );
				return;
			}

			Main.InGameUI.SetState( modConfigListInterfaceObj );

			Main.PlaySound( SoundID.MenuTick );
		}


		/// <summary>
		/// Loads the UI for mod configs.
		/// </summary>
		/// <param name="config"></param>
		public static void OpenModConfigUI( ModConfig config ) {
			Type interfaceType = ReflectionLibraries.GetMainAssembly()
				.GetType( "Terraria.ModLoader.UI.Interface" );

			if( !Main.gameMenu ) {
				IngameOptions.Close();
				IngameFancyUI.CoverNextFrame();
				Main.playerInventory = false;
				Main.editChest = false;
				Main.npcChatText = "";
				Main.inFancyUI = true;
			} else {
				if( !ReflectionLibraries.Get( interfaceType, null, "modConfigID", out Main.menuMode ) ) {
					LogLibraries.Warn( "Could not get Interface.modConfigID" );
					return;
				}
			}

			UIState modConfigInterfaceObj;
			if( !ReflectionLibraries.Get(interfaceType, null, "modConfig", out modConfigInterfaceObj) || modConfigInterfaceObj == null ) {
				LogLibraries.Warn( "Could not get Interface.modConfig" );
				return;
			}

			object _;
			if( !ReflectionLibraries.RunMethod(modConfigInterfaceObj, "SetMod", new object[] { config.mod, config }, out _) ) {
				LogLibraries.Warn( "Could not run Interface.modConfig.SetMod" );
				return;
			}

			Main.InGameUI.SetState( modConfigInterfaceObj );

			Main.PlaySound( SoundID.MenuTick );
		}


		/// <summary>
		/// Loads the mod browser menu.
		/// </summary>
		public static void OpenModBrowserMenu() {
			Type interfaceType = ReflectionLibraries.GetMainAssembly()
				.GetType( "Terraria.ModLoader.UI.Interface" );

			int modBrowserMenuMode;
			if( !ReflectionLibraries.Get( interfaceType, null, "modBrowserID", out modBrowserMenuMode ) ) {
				LogLibraries.Warn( "Could not switch to mod browser menu context." );
				return;
			}

			Main.PlaySound( SoundID.MenuTick );
			Main.menuMode = modBrowserMenuMode;

			UIState modBrowserUi;
			if( !ReflectionLibraries.Get( interfaceType, null, "modBrowser", out modBrowserUi ) ) {
				LogLibraries.Warn( "Could not acquire mod browser UI." );
				return;
			}

			Timers.SetTimer( "ModHelpersModDownloadPrompt", 5, true, () => {
				if( MenuContextService.GetCurrentMenuUI()?.GetType().Name != "UIModBrowser" ) {
					return false;
				}

				bool isLoading;
				if( !ReflectionLibraries.Get( modBrowserUi, "loading", out isLoading ) ) {
					return false;
				}

				if( isLoading ) {
					return true;
				}

				ModMenuLibraries.ApplyModBrowserFilter( "", false, new List<string>() );
				return false;
			} );
		}


		/// <summary>
		/// Loads the mod browser menu with a given set of mods to one-click bulk download
		/// (via. `ModMenuHelpers.ApplyModBrowserFilter(...)`).
		/// </summary>
		/// <param name="packTitle">Name of the set.</param>
		/// <param name="modNames">Mod (internal) names of the set.</param>
		public static void OpenModBrowserWithDownloadList( string packTitle, List<string> modNames ) {
			Type interfaceType = ReflectionLibraries.GetMainAssembly()
				.GetType( "Terraria.ModLoader.UI.Interface" );

			int modBrowserMenuMode;
			if( !ReflectionLibraries.Get( interfaceType, null, "modBrowserID", out modBrowserMenuMode ) ) {
				LogLibraries.Warn( "Could not switch to mod browser menu context." );
				return;
			}

			Main.PlaySound( SoundID.MenuTick );
			Main.menuMode = modBrowserMenuMode;

			UIState modBrowserUi;
			if( !ReflectionLibraries.Get( interfaceType, null, "modBrowser", out modBrowserUi ) ) {
				LogLibraries.Warn( "Could not acquire mod browser UI." );
				return;
			}

			Timers.SetTimer( "ModHelpersModDownloadPrompt", 5, true, () => {
				if( MenuContextService.GetCurrentMenuUI()?.GetType().Name != "UIModBrowser" ) {
					return false;
				}

				bool isLoading;
				if( !ReflectionLibraries.Get( modBrowserUi, "loading", out isLoading ) ) {
					return false;
				}

				if( isLoading ) {
					return true;
				}

				ModMenuLibraries.ApplyModBrowserFilter( packTitle, true, modNames );
				return false;
			} );

			/*Assembly tmlAsm = typeof( ModLoader ).Assembly;
			Type interfaceType = tmlAsm.GetType( "Interface" );

			Type uiModDlType = tmlAsm.GetType( "UIModDownloadItem" );
			object uiModDl = Activator.CreateInstance( uiModDlType, "ModName", "0.0.0", "hamstar", "", ModSide.Both, "", "http://javid.ddns.net/tModLoader/download.php?Down=mods/ModLibsUI.tmod", 0, 0, "", false, false, null );
			//UIModDownloadItem modItem = new UIModDownloadItem( displayname, name, version, author, modreferences, modside, modIconURL, download, downloads, hot, timeStamp, update, updateIsDowngrade, installed );
			items.Add( modItem );
			
			Interface.downloadMods.SetDownloading( packTitle );
			Interface.downloadMods.SetModsToDownload( modFilter, items );
			Interface.modBrowser.updateNeeded = true;

			int menuMode;
			if( !ReflectionHelpers.GetField<int>( interfaceType, null, "downloadModsID", out menuMode ) ) {
				LogHelpers.Log( "Could not switch to downloads menu." );
				return;
			}
			Main.PlaySound( SoundID.MenuTick );
			Main.menuMode = menuMode;*/
		}
	}
}
