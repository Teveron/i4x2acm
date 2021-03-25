rem @echo off
FOR %%g in ("%1") DO (
 echo %%g
 Pushd %%g
 for %%f in (*.acm) do acm2wav.exe %%f
 Popd )

Pause
