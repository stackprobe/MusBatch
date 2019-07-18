#include "Random.h"

static uint X = 1;
static uint Y;
static uint Z;
static uint A;

static uint Xorshift128(void)
{
	uint t;

	t = X;
	t ^= X << 11;
	t ^= t >> 8;
	t ^= A;
	t ^= A >> 19;
	X = Y;
	Y = Z;
	Z = A;
	A = t;

	return t;
}
void InitRandom(void)
{
	uint64 seed = nowTick();
	uint i;

	X = (uint)(seed % 65521) << 16 | (uint)(seed % 65519);
	Y = (uint)(seed % 65497) << 16 | (uint)(seed % 65479);
	Z = (uint)(seed % 65449) << 16 | (uint)(seed % 65447);
	A = getSelfProcessId();

	for(i = 16; i; i--)
		Xorshift128();
}
uint GetRandom(void)
{
	return Xorshift128();
}
