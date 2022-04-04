﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI;
using ModLibsGeneral.Services.AnimatedColor;


namespace ModLibsUI.Classes.UI.Elements {
	/// <summary>
	/// Defines a UI panel element specialized for rendering and displaying a mod's data (especially as a list item).
	/// </summary>
	public partial class UIModData : UIThemedPanel {
		/// <summary>
		/// Expands on base.Draw(...) to implement custom mouse hover effects (if applicable).
		/// </summary>
		/// <param name="sb">SpriteBatch to draw to. Typically given `Main.spriteBatch`.</param>
		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );

			if( this.IsMouseHovering && this.WillDrawOwnHoverElements ) {
				this.DrawHoverEffects( sb );
			}
		}

		/// <summary>
		/// Expands on base.Draw to add version overlay and render mod tags.
		/// </summary>
		/// <param name="sb">SpriteBatch to draw to. Typically given `Main.spriteBatch`.</param>
		protected override void DrawSelf( SpriteBatch sb ) {
			base.DrawSelf( sb );

			CalculatedStyle innerDim = base.GetInnerDimensions();
			Vector2 innerPos = innerDim.Position();

			if( this.LatestAvailableVersion > this.Mod.Version ) {
				Color color = AnimatedColors.Fire.CurrentColor;
				var pos = new Vector2( innerPos.X + 128f, innerPos.Y );
			
				sb.DrawString( Main.fontDeathText, this.LatestAvailableVersion.ToString()+" Available", pos, color, 0f, default( Vector2 ), 1f, SpriteEffects.None, 1f );
			}

			/*if( this.ModTags.Count > 0 ) {
				var startPos = new Vector2( innerPos.X, innerPos.Y + 56 );
				var pos = startPos;

				this.Height.Set( 64 + 12, 0f );

				int i = 0;
				foreach( string tag in this.ModTags ) {
					if( !ModTagDefinition.TagMap.ContainsKey(tag) ) {
						continue;
					}

					string tagStr = tag + ((i++ < this.ModTags.Count-1) ? "," : "");
					Color tagColor = ModTagDefinition.TagMap[tag].GetFlavorColor();

					Vector2 dim = Main.fontMouseText.MeasureString( tag ) * 0.75f;
					float addX = dim.X + 8;

					if( ((pos.X + addX) - innerDim.X) > innerDim.Width ) {
						pos.X = startPos.X;
						pos.Y += 12;

						this.Height.Set( this.Height.Pixels + 12, 0f );
					}

					Utils.DrawBorderString( sb, tagStr, pos, tagColor, 0.75f );
					
					pos.X += addX;
				}
			}*/
		}


		/// <summary>
		/// Draws mouse hover title.
		/// </summary>
		/// <param name="sb">SpriteBatch to draw to. Typically given `Main.spriteBatch`.</param>
		public void DrawHoverEffects( SpriteBatch sb ) {
			if( this.TitleElem.IsMouseHovering ) {
				if( this.TitleElem is UIWebUrl ) {
					var titleUrl = (UIWebUrl)this.TitleElem;

					if( !titleUrl.WillDrawOwnHoverUrl ) {
						titleUrl.DrawHoverEffects( sb );
					}
				}
			}
		}
	}
}
