﻿using System;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria.UI;
using ModLibsUI.Classes.UI.Theme;
using ModLibsCore.Libraries.Debug;


namespace ModLibsUI.Classes.UI.Elements.Slider {
	/// <summary>
	/// Implements a UI slider bar element.
	/// </summary>
	public partial class UISlider : UIThemedElement, IToggleable {
		private void InitializeMe() {
			bool ProcessInput( StringBuilder fullInput ) {
				return !this.IsNowSettingValue
					&& this.IsInteractive
					&& this.SetValueFromInput( fullInput.ToString() );
			}

			//

			this.Width.Set( UISlider.DefaultSliderWidth, 0f );
			this.Height.Set( UISlider.DefaultSliderHeight, 0f );

			this.NumericInput = new UITextInputElement( "" );
			this.NumericInput.Enable( !this.IsTextInputHidden );
			this.NumericInput.Hide( this.IsTextInputHidden );
			this.NumericInput.Top.Set( -2f, 0f );
			this.NumericInput.Left.Set( 20f, 0f );
			this.NumericInput.Width.Set( 64f, 0f );
			this.NumericInput.Height.Set( 10f, 0f );
			this.NumericInput.OnTextChange += ProcessInput;
			this.Append( this.NumericInput );

			this.LeftArrowElem = new UIThemedText( this.Theme, true, "< ", true );
			this.LeftArrowElem.TextColor = Color.Gray;
			this.LeftArrowElem.Top.Set( -2f, 0f );
			this.LeftArrowElem.Left.Set( -UISlider.DefaultArrowsWidth - 4f, 1f );
			this.LeftArrowElem.Height.Set( UISlider.DefaultSliderHeight, 0f );
			this.LeftArrowElem.OnMouseOver += ( _, __ ) => {
				this.LeftArrowElem.TextColor = Color.Yellow;
				this.LeftArrowElem.SetText( this.LeftArrowElem.Text, 1.1f, false );
			};
			this.LeftArrowElem.OnMouseOut += ( _, __ ) => {
				this.LeftArrowElem.TextColor = Color.Gray;
				this.LeftArrowElem.SetText( this.LeftArrowElem.Text, 1.0f, false );
			};
			this.LeftArrowElem.OnClick += ( _, __ ) => {
				if( this.IsInteractive ) {
					this.ScrollLeft();
				}
			};
			this.Append( (UIElement)this.LeftArrowElem );

			this.RightArrowElem = new UIThemedText( this.Theme, true, " >  ", true );
			this.RightArrowElem.TextColor = Color.Gray;
			this.RightArrowElem.Top.Set( -2f, 0f );
			this.RightArrowElem.Left.Set( -(UISlider.DefaultArrowsWidth * 0.5f) - 12f, 1f );
			this.RightArrowElem.Height.Set( UISlider.DefaultSliderHeight, 0f );
			this.RightArrowElem.OnMouseOver += ( _, __ ) => {
				this.RightArrowElem.TextColor = Color.Yellow;
				this.RightArrowElem.SetText( this.RightArrowElem.Text, 1.1f, false );
			};
			this.RightArrowElem.OnMouseOut += ( _, __ ) => {
				this.RightArrowElem.TextColor = Color.Gray;
				this.RightArrowElem.SetText( this.RightArrowElem.Text, 1.0f, false );
			};
			this.RightArrowElem.OnClick += ( _, __ ) => {
				if( this.IsInteractive ) {
					this.ScrollRight();
				}
			};
			this.Append( (UIElement)this.RightArrowElem );
		}
	}
}
