#pragma comment(lib, "user32.lib")

#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	while(!waitKey(0))
	{
		uint val;
		uint ret;

		ret = SystemParametersInfo(SPI_GETWHEELSCROLLLINES, 0, &val, 0);

		// �z�C�[���������Ă��A��� val == 3, ret == 1 �ɂȂ�B

		cout("%u %u\n", val, ret);

		sleep(100);
	}
}
