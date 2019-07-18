#include "C:\Factory\Common\all.h"
#include "..\Random.h"

int main(int argc, char **argv)
{
	InitRandom();

	cout("%08x\n", GetRandom());
	cout("%08x\n", GetRandom());
	cout("%08x\n", GetRandom());
}
