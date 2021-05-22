﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsUI.Classes.UI.Menus;
using ModLibsUI.Libraries.TModLoader.Menus;


namespace ModLibsUI.Services.UI.Menus {
	/// <summary>
	/// Provides a way to interface with the main menu and its UI by way of "contexts".
	/// </summary>
	public partial class MenuContextService {
		/// <summary>
		/// Indicates if a menu "context" (menu page) has custom content.
		/// </summary>
		/// <param name="menuDefinition"></param>
		/// <returns></returns>
		public static bool ContainsMenuContexts( MenuUIDefinition menuDefinition ) {
			var menuCtxMngr = ModContent.GetInstance<MenuContextServiceManager>();
			if( menuCtxMngr == null ) { return false; }

			IDictionary<string, MenuContext> contexts = menuCtxMngr.GetContexts( menuDefinition );

			return contexts != null && contexts.Count > 0;
		}


		////////////////

		/// <summary>
		/// Gets a specific piece of added content for a menu "context" (menu page) by name.
		/// </summary>
		/// <param name="menuDefinition"></param>
		/// <param name="contextName"></param>
		/// <returns></returns>
		public static MenuContext GetMenuContext( MenuUIDefinition menuDefinition, string contextName ) {
			var menuCtxMngr = ModContent.GetInstance<MenuContextServiceManager>();
			if( menuCtxMngr == null ) { return null; }

			IDictionary<string, MenuContext> contexts = menuCtxMngr.GetContexts( menuDefinition );

			if( contexts.ContainsKey(contextName) ) {
				return contexts[ contextName ];
			}
			return null;
		}


		////////////////

		/// <summary>
		/// Adds a piece of menu content to a menu "context" (menu page) by name.
		/// </summary>
		/// <param name="context"></param>
		public static void AddMenuContext( MenuContext context ) {
			var menuCtxMngr = ModContent.GetInstance<MenuContextServiceManager>();
			if( menuCtxMngr == null ) { return; }

			MenuUIDefinition menuDef = context.MenuDefinitionOfContext;

			IDictionary<string, MenuContext> contexts = menuCtxMngr.GetContexts( menuDef );
			contexts[ context.ContextName ] = context;

			UIState ui = Main.MenuUI.CurrentState;
			string currUiContextName = ui?.GetType().Name;

			if( Enum.GetName(typeof(MenuUIDefinition), menuDef ) == currUiContextName ) {
				context.ActivateIfInactive( ui );
				context.Show( ui );
			}
		}
	}
}
