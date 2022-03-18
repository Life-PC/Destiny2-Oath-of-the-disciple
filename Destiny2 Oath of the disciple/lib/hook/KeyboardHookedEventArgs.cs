using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Destiny2_Oath_of_the_disciple.lib.hook
{
	public class KeyboardHookedEventArgs : CancelEventArgs
	{
		private KeyboardMessage message;

		private KeyboardState state;

		public KeyboardUpDown UpDown => (message != KeyboardMessage.KeyDown && message != KeyboardMessage.SysKeyDown) ? KeyboardUpDown.Up : KeyboardUpDown.Down;

		public Keys KeyCode => state.KeyCode;

		public int ScanCode => state.ScanCode;

		public bool IsExtendedKey => state.Flag.IsExtended;

		public bool AltDown => state.Flag.AltDown;

		internal KeyboardHookedEventArgs(KeyboardMessage message, ref KeyboardState state)
		{
			this.message = message;
			this.state = state;
		}
	}
}
