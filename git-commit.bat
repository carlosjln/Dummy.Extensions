@echo off
rem Scripted by: Carlos J. L�pez 
rem https://github.com/carlosjln/
rem https://twitter.com/carlosjln

echo.
echo What did you do?
set /p msg=I 

echo.
call git add -A

echo.
call git commit --verbose -m "%msg%"

echo.
set /p push=Do you want to push your changes? [y/n] 

echo.
IF "%push%"=="y" (
	echo Pushing changes...
	echo.
	call git push
)

echo.
echo My job here is done :)
echo.
pause