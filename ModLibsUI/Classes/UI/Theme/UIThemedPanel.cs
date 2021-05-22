﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using ModLibsUI.Classes.UI.Theme;


namespace ModLibsUI.Classes.UI.Elements {
	/// <summary>
	/// Theme-able UIPanel.
	/// </summary>
	public class UIThemedPanel : UIPanel, IThemeable, IToggleable {
		/// <summary>
		/// Appearance style.
		/// </summary>
		public UITheme Theme { get; protected set; }

		/// <summary></summary>
		public bool IsHidden { get; protected set; }

		/// <summary>Visibility percent.</summary>
		public float Opacity { get; set; } = 1f;



		////////////////

		/// <summary></summary>
		/// <param name="theme"></param>
		/// <param name="skipThemeRefreshNow"></param>
		public UIThemedPanel( UITheme theme, bool skipThemeRefreshNow ) : base() {
			this.Theme = theme;

			if( !skipThemeRefreshNow ) {
				theme.ApplyPanel( this );
			}
		}


		////////////////

		/// <summary>
		/// An alternative to the normal `Append` method to apply theming to appended element.
		/// </summary>
		/// <param name="element"></param>
		public void AppendThemed( UIElement element ) {
			base.Append( element );
			this.RefreshThemeForChild( element );
		}


		////////////////

		/// <summary>
		/// Re-applies the current theme styles (including child elements).
		/// </summary>
		public virtual void RefreshTheme() {
			this.Theme.ApplyPanel( this );

			foreach( UIElement elem in this.Elements ) {
				this.RefreshThemeForChild( elem );
			}
		}

		/// <summary>
		/// Applies the current theme's styles to a given element (presumably a child element).
		/// </summary>
		/// <param name="element"></param>
		public virtual void RefreshThemeForChild( UIElement element ) {
			if( !this.Theme.Apply( element ) ) {
				this.Theme.ApplyByType( element );
			}
		}

		////////////////

		/// <summary>
		/// Sets the current theme.
		/// </summary>
		/// <param name="theme"></param>
		public virtual void SetTheme( UITheme theme ) {
			this.Theme = theme;
			this.RefreshTheme();
		}


		////////////////

		/// <summary>
		/// Enables interactivity for all child elements of this element.
		/// </summary>
		public virtual void Enable() {
			foreach( var child in this.Elements ) {
				var toggleable = child as IToggleable;
				if( toggleable == null ) { continue; }

				toggleable.Enable();
			}
		}

		/// <summary>
		/// Enables interactivity for all child elements of this element.
		/// </summary>
		public virtual void Disable() {
			foreach( var child in this.Elements ) {
				var toggleable = child as IToggleable;
				if( toggleable == null ) { continue; }

				toggleable.Disable();
			}
		}


		////////////////

		/// <summary></summary>
		public virtual void Hide() {
			this.IsHidden = true;
		}

		/// <summary></summary>
		public virtual void Show() {
			this.IsHidden = false;
		}


		////////////////

		/// @private
		public override void Draw( SpriteBatch spriteBatch ) {
			if( !this.IsHidden ) {
				float opacity = this.ComputeCurrentOpacity();

				Color oldBg = this.BackgroundColor;
				Color oldBord = this.BorderColor;

				this.BackgroundColor *= opacity;
				this.BorderColor *= opacity;

				base.Draw( spriteBatch );

				this.BackgroundColor = oldBg;
				this.BorderColor = oldBord;
			}
		}


		////////////////

		/// <summary>
		/// Gets the opacity of the current panel, factoring all parent panels.
		/// </summary>
		/// <returns></returns>
		public float ComputeCurrentOpacity() {
			float opacity = 1f;
			UIElement parent = this;
			UIThemedPanel parentPanel;

			do {
				parentPanel = parent as UIThemedPanel;
				if( parentPanel != null ) {
					opacity *= parentPanel.Opacity;
				}

				parent = parent.Parent;
			} while( parent != null );

			return opacity;
		}
	}
}
