using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Destiny2_Oath_of_the_disciple.lib.hook
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
