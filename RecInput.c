/*
	RecInput.exe /R REC-FILE SAMPLING-MILLIS R-CTRL-AND-END

		REC-FILE       ... 保存ファイル
		R-CTRL-AND-END ... 右コントロール押下で停止するか, 0 or not 0

	RecInput.exe /S

		停止する。
*/

#pragma comment(lib, "user32.lib")

#include "C:\Factory\Common\all.h"

#define REC_MAX 360000
#define REC_MARGIN 10 // HACK

static uint MtxProc;
static uint EvStop;
static char *RecFile;
static int RCtrlAndEnd;
static uint SamplingMillis;

typedef struct Record_st
{
	sint X;
	sint Y;
	uchar States[0x100];
}
Record_t;

static Record_t *Records;

static void DoRecInput(void)
{
	uint vk;
	uint recIndex;
	uint recCount;
	Record_t *record;
	Record_t *prevRecord;
	FILE *fp;

	for(vk = 0; vk <= 0xff; vk++)
		GetAsyncKeyState(vk);

	Records = memCalloc((REC_MAX + REC_MARGIN) * sizeof(Record_t));

	LOGPOS();

	for(recIndex = 0; ; recIndex++)
	{
		errorCase_m(REC_MAX <= recIndex, "記憶領域の上限を超えました。");

		record = Records + recIndex;

		{
			POINT pos;

			GetCursorPos(&pos);

			record->X = pos.x;
			record->Y = pos.y;
		}

		for(vk = 0; vk <= 0xff; vk++)
			record->States[vk] = GetAsyncKeyState(vk) ? 1 : 0;

		if(RCtrlAndEnd && record->States[VK_RCONTROL])
			break;

		if(handleWaitForMillis(EvStop, 10))
			break;
	}

	LOGPOS();

	recCount = recIndex + 1;
	fp = fileOpen(RecFile, "wb");

	if(recCount)
	{
		writeLine_x(fp, xcout("M %d %d", record->X, record->Y));

		for(recIndex = 1; recIndex < recCount; recIndex++)
		{
			record = Records + recIndex;
			prevRecord = Records + recIndex - 1;

			if(prevRecord->X != record->X || prevRecord->Y != record->Y)
				writeToken_x(fp, xcout("M %d %d", record->X, record->Y));

			for(vk = 0; vk <= 0xff; vk++)
				if(prevRecord->States[vk] ? !record->States[vk] : record->States[vk])
					writeToken_x(fp, xcout(" %c %u", record->States[vk] ? 'D' : 'U', vk));

			writeChar(fp, '\n');
		}
	}
	fileClose(fp);
	memFree(Records);

	LOGPOS();
}
int main(int argc, char **argv)
{
	MtxProc = mutexOpen("{2ccc9820-c8dd-4c43-b8ec-02c97a0fb31f}");
	EvStop  = eventOpen("{e2be109b-c282-40fc-86de-d86f383127a0}"); // shared_uuid

	if(argIs("/R"))
	{
		RecFile = nextArg();
		RCtrlAndEnd = atoi(nextArg());

		if(handleWaitForMillis(MtxProc, 0))
		{
			DoRecInput();
			mutexRelease(MtxProc);
		}
	}
	else if(argIs("/S"))
	{
		eventSet(EvStop);
	}
	handleClose(MtxProc);
	handleClose(EvStop);
}
