C:\Factory\Tools\RDMD.exe /RC out

COPY /B RecInput.exe out
COPY /B SndInput.exe out
COPY /B WRec\WRec\bin\Release\WRec.exe out

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled out\RecInput.exe
C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled out\SndInput.exe

C:\Factory\SubTools\zip.exe /O out MusBatch

IF NOT "%1" == "/-P" PAUSE
