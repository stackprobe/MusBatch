#pragma comment(lib, "user32.lib")

#include "C:\Factory\Common\all.h"
#include "libs\Random.h"

#define SLEEP_ONCE_MILLIS 500

/*
	構造体参考

		[StructLayout(LayoutKind.Sequential)]
		private struct MOUSEINPUT
		{
			public int dx;
			public int dy;
			public int mouseData;
			public int dwFlags;
			public int time;
			public int dwExtraInfo;
		};

		[StructLayout(LayoutKind.Sequential)]
		private struct KEYBDINPUT
		{
			public short wVk;
			public short wScan;
			public int dwFlags;
			public int time;
			public int dwExtraInfo;
		};

		[StructLayout(LayoutKind.Sequential)]
		private struct HARDWAREINPUT
		{
			public int uMsg;
			public short wParamL;
			public short wParamH;
		};

		[StructLayout(LayoutKind.Explicit)]
		private struct INPUT
		{
			[FieldOffset(0)]
			public int type;
			[FieldOffset(4)]
			public MOUSEINPUT mi;
			[FieldOffset(4)]
			public KEYBDINPUT ki;
			[FieldOffset(4)]
			public HARDWAREINPUT hi;
		};
*/

static void DoMouseCursor(sint x, sint y)
{
	INPUT i;

	zeroclear(i);

	i.type = INPUT_MOUSE;
	i.mi.dx = d2i(x * (65536.0 / GetSystemMetrics(SM_CXSCREEN)));
	i.mi.dy = d2i(y * (65536.0 / GetSystemMetrics(SM_CYSCREEN)));
	i.mi.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
	i.mi.dwExtraInfo = GetMessageExtraInfo();

	SendInput(1, &i, sizeof(INPUT));
}
static void DoMouseButton(uint kind, int downFlag)
{
	INPUT i;
	int flag;

	switch(kind)
	{
	case 1: flag = downFlag ? MOUSEEVENTF_LEFTDOWN   : MOUSEEVENTF_LEFTUP;   break;
	case 2: flag = downFlag ? MOUSEEVENTF_MIDDLEDOWN : MOUSEEVENTF_MIDDLEUP; break;
	case 3: flag = downFlag ? MOUSEEVENTF_RIGHTDOWN  : MOUSEEVENTF_RIGHTUP;  break;

	default:
		error();
	}

	zeroclear(i);

	i.type = INPUT_MOUSE;
	i.mi.dwFlags = flag;
	i.mi.dwExtraInfo = GetMessageExtraInfo();

	SendInput(1, &i, sizeof(INPUT));
}
static void DoMouseWheel(sint level) // level: -1 == 手前に1コロ, +1 == 奥へ1コロ
{
	INPUT i;

	zeroclear(i);

	i.type = INPUT_MOUSE;
	i.mi.mouseData = level * WHEEL_DELTA;
	i.mi.dwFlags = MOUSEEVENTF_WHEEL;
	i.mi.dwExtraInfo = GetMessageExtraInfo();

	SendInput(1, &i, sizeof(INPUT));
}
static void DoKeyboard(uint16 vk, int downFlag)
{
	INPUT i;

	zeroclear(i);

	i.type = INPUT_KEYBOARD;
	i.ki.wVk = vk;
	i.ki.wScan = MapVirtualKey(vk, 0);
	i.ki.dwFlags = KEYEVENTF_EXTENDEDKEY | (downFlag ? 0 : KEYEVENTF_KEYUP); // KEYEVENTF_EXTENDEDKEY を指定しないとシフトが押せない？？？
	i.mi.dwExtraInfo = GetMessageExtraInfo();

	SendInput(1, &i, sizeof(INPUT));
}
static void CheckBreak(void)
{
#if 0
	if(GetAsyncKeyState(VK_SCROLL) != 0)
	{
		cout("VK_SCROLL PRESSED !!!\n");
		termination(1);
	}
#endif
}
int main(int argc, char **argv)
{
	uint xRndRng = 0;
	uint yRndRng = 0;

	while(hasArgs(1))
	{
		CheckBreak();

		if(argIs("RR"))
		{
			xRndRng = toValue(nextArg());
			yRndRng = toValue(nextArg());

			m_minim(xRndRng, IMAX);
			m_minim(yRndRng, IMAX);

			InitRandom();
			continue;
		}
		if(argIs("MC"))
		{
			sint x;
			sint y;

			x = atoi(nextArg());
			y = atoi(nextArg());

			if(xRndRng)
				x += (sint)(GetRandom() % (xRndRng * 2 + 1)) - (sint)xRndRng;

			if(yRndRng)
				y += (sint)(GetRandom() % (yRndRng * 2 + 1)) - (sint)yRndRng;

//			cout("MOUSE_CURSOR: %d, %d\n", x, y);

			DoMouseCursor(x, y);
			continue;
		}
		if(argIs("MB"))
		{
			uint kind;
			int downFlag;

			kind = toValue(nextArg());
			downFlag = atoi(nextArg());

//			cout("MOUSE_BUTTON: %u, %d\n", kind, downFlag);

			DoMouseButton(kind, downFlag);
			continue;
		}
		if(argIs("MW"))
		{
			sint level = atoi(nextArg());

//			cout("MOUSE_WHEEL: %d\n", level);

			DoMouseWheel(level);
			continue;
		}
		if(argIs("KB"))
		{
			uint vk;
			int downFlag;

			vk = toValue(nextArg());
			downFlag = atoi(nextArg());

//			cout("KEYBOARD: %02x (%u), %d\n", vk, vk, downFlag);

			DoKeyboard(vk, downFlag);
			continue;
		}
		if(argIs("T"))
		{
			uint millis = toValue(nextArg());

//			cout("SLEEP %u\n", millis);

			for(; ; )
			{
				uint t = m_min(SLEEP_ONCE_MILLIS, millis);

				sleep(t);
				millis -= t;

				if(!millis)
					break;

				CheckBreak();
			}
			continue;
		}
		error_m("不明な引数が指定されました。");
	}
}
