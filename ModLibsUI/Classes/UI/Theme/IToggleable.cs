﻿using System;


namespace ModLibsUI.Classes.UI.Theme {
	/// <summary>
	/// Interface for all elements that support toggleable interactivity.
	/// </summary>
	public interface IToggleable {
		/// <summary></summary>
		void Enable();

		/// <summary></summary>
		void Disable();
	}
}
