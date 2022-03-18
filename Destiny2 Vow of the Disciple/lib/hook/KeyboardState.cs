using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Destiny2_Vow_of_the_Disciple.lib.hook
{
	internal struct KeyboardState
	{
		public Keys KeyCode;

		public int ScanCode;

		public KeyboardStateFlag Flag;

		public int Time;

		public IntPtr ExtraInfo;
	}
}
