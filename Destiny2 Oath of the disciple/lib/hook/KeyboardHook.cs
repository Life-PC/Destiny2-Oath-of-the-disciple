using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Destiny2_Oath_of_the_disciple.lib.hook
{
	[DefaultEvent("KeyboardHooked")]
	public class KeyboardHook : Component
	{
		private delegate int KeyboardHookDelegate(int code, KeyboardMessage message, ref KeyboardState state);

		private const int KeyboardHookType = 13;

		private GCHandle hookDelegate;

		private IntPtr hook;

		private static readonly object EventKeyboardHooked = new object();

		public event KeyboardHookedEventHandler KeyboardHooked
		{
			add
			{
				base.Events.AddHandler(EventKeyboardHooked, value);
			}
			remove
			{
				base.Events.RemoveHandler(EventKeyboardHooked, value);
			}
		}

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int hookType, KeyboardHookDelegate hookDelegate, IntPtr hInstance, uint threadId);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern int CallNextHookEx(IntPtr hook, int code, KeyboardMessage message, ref KeyboardState state);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool UnhookWindowsHookEx(IntPtr hook);

		protected virtual void OnKeyboardHooked(KeyboardHookedEventArgs e)
		{
			(base.Events[EventKeyboardHooked] as KeyboardHookedEventHandler)?.Invoke(this, e);
		}

		public KeyboardHook()
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException("Windows 98/Meではサポートされていません。");
			}
			KeyboardHookDelegate value = CallNextHook;
			hookDelegate = GCHandle.Alloc(value);
			IntPtr hINSTANCE = Marshal.GetHINSTANCE(typeof(KeyboardHook).Assembly.GetModules()[0]);
			hook = SetWindowsHookEx(13, value, hINSTANCE, 0u);
		}

		public KeyboardHook(KeyboardHookedEventHandler handler)
			: this()
		{
			KeyboardHooked += handler;
		}

		private int CallNextHook(int code, KeyboardMessage message, ref KeyboardState state)
		{
			if (code >= 0)
			{
				KeyboardHookedEventArgs keyboardHookedEventArgs = new KeyboardHookedEventArgs(message, ref state);
				OnKeyboardHooked(keyboardHookedEventArgs);
				if (keyboardHookedEventArgs.Cancel)
				{
					return -1;
				}
			}
			return CallNextHookEx(IntPtr.Zero, code, message, ref state);
		}

		protected override void Dispose(bool disposing)
		{
			if (hookDelegate.IsAllocated)
			{
				UnhookWindowsHookEx(hook);
				hook = IntPtr.Zero;
				hookDelegate.Free();
			}
			base.Dispose(disposing);
		}
	}
}
