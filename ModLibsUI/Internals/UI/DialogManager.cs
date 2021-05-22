using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsUI.Classes.UI.Elements;


namespace ModLibsUI.Internals.UI {
	class DialogManager : ILoadable {
		public static DialogManager Instance => ModContent.GetInstance<DialogManager>();


		////////////////

		public bool ForcedPersistenceDialog { get; private set; }
		public UIDialog CurrentDialog { get; private set; }



		////////////////

		public DialogManager() {
			this.ForcedPersistenceDialog = false;
			this.CurrentDialog = null;
		}

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }

		////////////////

		internal void SetForcedPersistence() {
			if( this.CurrentDialog != null ) {
				this.ForcedPersistenceDialog = true;
			}
		}

		internal void UnsetForcedModality() {
			this.ForcedPersistenceDialog = false;
		}


		internal void SetCurrentDialog( UIDialog dlg ) {
			if( this.CurrentDialog != null && this.CurrentDialog != dlg ) {
				this.CurrentDialog.Close();
			}
			this.CurrentDialog = dlg;
		}

		////////////////

		internal void Update() {
			if( this.CurrentDialog == null ) {
				return;
			}

			if( Main.InGameUI.CurrentState != this.CurrentDialog ) {
				this.CurrentDialog.Close();
			}
			
			if( !this.CurrentDialog.IsOpen ) {
				if( this.ForcedPersistenceDialog ) {
					this.CurrentDialog.Open();
				} else {
					this.CurrentDialog = null;
				}
			}
		}
	}
}
