﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using ModLibsUI.Classes.UI.Theme;
using ModLibsCore.Libraries.Debug;


namespace ModLibsUI.Classes.UI.Elements {
	/// <summary>
	/// Defines a common text panel button element.
	/// </summary>
	public class UITextPanelButton : UIThemedTextPanel, IToggleable {
		/// @private
		[Obsolete( "use IsInteractive", true )]
		public bool IsEnabled {
			get => this.IsInteractive;
			private set => this.IsInteractive = value;
		}

		/// <summary>
		/// Indicates element is able to be edited.
		/// </summary>
		public bool IsInteractive { get; private set; } = true;

		//public string HoverText = "";



		////////////////

		/// <param name="theme">Appearance style.</param>
		/// <param name="label">Button's label.</param>
		/// <param name="scale">Multiplier of label text size.</param>
		/// <param name="large">Sets label text 'large'.</param>
		public UITextPanelButton( UITheme theme, string label, float scale = 1f, bool large = false )
				: base( theme, true, label, scale, large ) {
			this.Theme = theme;
			this.IsInteractive = true;

			this.SetPadding( 5f );

			theme.ApplyButton( this );

			var self = this;
			this.OnMouseOver += delegate ( UIMouseEvent evt, UIElement fromElem ) {
				if( !self.IsInteractive ) { return; }
				theme.ApplyButtonLit( self );
			};
			this.OnMouseOut += delegate ( UIMouseEvent evt, UIElement fromElem ) {
				if( !self.IsInteractive ) { return; }
				theme.ApplyButton( self );
			};

			this.RefreshTheme();
		}


		////////////////

		/// <summary>
		/// Enables the button.
		/// </summary>
		public virtual void Enable() {
			this.IsInteractive = true;
			this.RefreshTheme();
		}

		/// <summary>
		/// Disables the button.
		/// </summary>
		public virtual void Disable() {
			this.IsInteractive = false;
			this.RefreshTheme();
		}


		////////////////

		/// <summary>
		/// Refreshes visual theming.
		/// </summary>
		public override void RefreshTheme() {
			if( this.IsInteractive ) {
				this.Theme.ApplyButton( this );
			} else {
				this.Theme.ApplyButtonDisable( this );
			}
		}

		////////////////

		/// @private
		public override void Draw( SpriteBatch spriteBatch ) {
			if( !this.IsHidden ) {
				base.Draw( spriteBatch );
			}
		}


		////////////////

		/*private void DrawHoverTextIfHover( GameTime gameTime ) {
			if( string.IsNullOrEmpty( this.HoverText ) ) { return; }

			if( this.GetOuterDimensions().ToRectangle().Contains( Main.mouseX, Main.mouseY ) ) {
				var pos = new Vector2( Main.mouseX, Main.mouseY );

				Main.spriteBatch.DrawString( Main.fontMouseText, this.HoverText, pos, Color.White );
			}
		}*/
	}
}
