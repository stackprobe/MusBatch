#pragma comment(lib, "user32.lib")

#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	while(!waitKey(0))
	{
		uint val;
		uint ret;

		ret = SystemParametersInfo(SPI_GETWHEELSCROLLLINES, 0, &val, 0);

		// ホイール動かしても、常に val == 3, ret == 1 になる。

		cout("%u %u\n", val, ret);

		sleep(100);
	}
}
