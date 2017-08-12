// コレジャナイｗｗｗ

#pragma comment(lib, "user32.lib")

#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	for(; ; )
	{
		uint val;

		SystemParametersInfo(SPI_GETWHEELSCROLLLINES, 0, &val, 0);

		cout("%u\n", val);

		sleep(100);
	}
}
