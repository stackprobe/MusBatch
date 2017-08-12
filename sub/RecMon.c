#pragma comment(lib, "user32.lib")

#include "C:\Factory\Common\all.h"

static uchar *LastStates;
static uchar *States;
static uchar States_1[0x100];
static uchar States_2[0x100];

static void DoRecMon(void)
{
	uint vk;

	LastStates = States_1;
	States = States_2;

	for(; ; )
	{
		{
			POINT pos;

			GetCursorPos(&pos);

			cout("M %d %d", pos.x, pos.y);
		}

		for(vk = 0; vk <= 0xff; vk++)
			States[vk] = GetAsyncKeyState(vk) ? 1 : 0;

		for(vk = 0; vk <= 0xff; vk++)
			if(LastStates[vk] ? !States[vk] : States[vk])
				cout(" %c %u", States[vk] ? 'D' : 'U', vk);

		m_swap(LastStates, States, uint);

		cout("\n");

		sleep(500);
//		sleep(10);
//		sleep(1);
	}
	LOGPOS();
}
int main(int argc, char **argv)
{
	DoRecMon();
}
