﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using ModLibsCore.Services.Timers;


namespace ModLibsUI.Classes.UI.Elements {
	/// <summary>
	/// Defines a hidden UI element. Used for internal logical or interface-related functions.
	/// </summary>
	public class UIHiddenPanel : UIPanel {
		/// <summary>
		/// Indicates if mouse is hovering the element.
		/// </summary>
		public bool IsHovering { get; private set; }

		private Func<Rectangle> Zone;
		private Action OnHover;
		private Action OnExit;



		////////////////

		/// <param name="zone">Indicates the space the element pretends to occupy for interactivity purposes
		/// (in truth, it is 0 sized).</param>
		/// <param name="onHover">Action to run on mouse hover</param>
		/// <param name="onExit">Action to run on mouse hover exit</param>
		public UIHiddenPanel( Func<Rectangle> zone, Action onHover, Action onExit ) : base() {
			this.Zone = zone;
			this.OnHover = onHover;
			this.OnExit = onExit;

			this.Left.Set( 0f, 0f );
			this.Top.Set( 0f, 0f );
			this.Width.Set( 0f, 0f );
			this.Height.Set( 0f, 0f );
		}


		////////////////

		/// <summary>
		/// Handles mouse hover detection; nothing to draw.
		/// </summary>
		/// <param name="spriteBatch">Unused.</param>
		public override void Draw( SpriteBatch spriteBatch ) {
			if( this.Zone().Contains(Main.mouseX, Main.mouseY) ) {
				if( !this.IsHovering ) {
					Timers.RunNow( () => {
						this.OnHover();
					} );
				}
				this.IsHovering = true;
			} else {
				if( this.IsHovering ) {
					Timers.RunNow( () => {
						this.OnExit();
					} );
				}
				this.IsHovering = false;
			}
		}
	}
}
