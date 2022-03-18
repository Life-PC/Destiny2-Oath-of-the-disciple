using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny2_Oath_of_the_disciple.lib.hook
{
	internal struct KeyboardStateFlag
	{
		private int flag;

		public bool IsExtended
		{
			get
			{
				return IsFlagging(1);
			}
			set
			{
				Flag(value, 1);
			}
		}

		public bool IsInjected
		{
			get
			{
				return IsFlagging(16);
			}
			set
			{
				Flag(value, 16);
			}
		}

		public bool AltDown
		{
			get
			{
				return IsFlagging(32);
			}
			set
			{
				Flag(value, 32);
			}
		}

		public bool IsUp
		{
			get
			{
				return IsFlagging(128);
			}
			set
			{
				Flag(value, 128);
			}
		}

		private bool IsFlagging(int value)
		{
			return (flag & value) != 0;
		}

		private void Flag(bool value, int digit)
		{
			flag = (value ? (flag | digit) : (flag & ~digit));
		}
	}
}
