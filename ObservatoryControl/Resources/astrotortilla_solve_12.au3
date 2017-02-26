#include <Constants.au3>
#include <GuiComboBox.au3>
#include <WinAPIFiles.au3>
#include <Date.au3>
;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
; AstroTortilla sovler
; 1.2c [02/01/2017]
;   - return coordinates in file
; 1.1 [08/12/2016]
;   - path to astrotortilla can be passed as argument
; 1.0 [07/12/2016]
;   - initial working release
;
;///////////////////////////////////////////////////////////////////////////////////////////////////////////////


;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
$wintitle="AstroTortilla"
$wintitleASCOM="ASCOM Telescope Chooser"
$ascomWaitTimeout=1
$programPath = "c:\Program Files (x86)\AstroTortilla\AstroTortilla.exe"
$tempFileName = "astrotortilla_result.txt" ; Path - System Temp Dir
;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
$handleWindow=0;

if ($CmdLine[0]>0) then
    $programPath = $CmdLine[1]
endif

Global $res, $Solution_RA, $Solution_Dec, $Current_RA, $Current_Dec, $Target_RA, $Target_Dec; //return result of solution



; 0 - Start program if not running
StartProgram()

; 1 - Switch combobox to point 2 (ASCOM telescope)
; 2 - Press OK on ASCOM dialog
ConnectTelescope()

; 3 - Check "Sync Scope"
pressSyncScope()

; 4 - Press "Capture and Solve"
pressStartButton()

WinActivate ($winTitle)

; 5 - Wait for solution
$res = waitForResult() ; =0 solved, =1 not solved

; 6 - get result detailes
if ($res=0) Then
   getResultDetails();
EndIf

; 7 - save result detailes
SaveResult();

exit $res

;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
; THE END
;///////////////////////////////////////////////////////////////////////////////////////////////////////////////



;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
; Запуск программы, если не запущена, и активация окна
;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
func StartProgram()
	; Run program
	if (not WinExists ($winTitle)) Then
		Run($programPath)
		$handleWindow=WinWaitActive($winTitle)
	Else
		WinActivate ($winTitle)
	EndIf
EndFunc



;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
; Подключить телескоп в программе
;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
func ConnectTelescope()
	; 1 - Switch combobox to point 2 (ASCOM telescope)
	$hanTelescopeControl = ControlGetHandle($wintitle,"","[CLASSNN:ComboBox2]")
	;MsgBox(1,"AstroTortilla TelescopeControl",$hanTelescopeControl)

	ControlSend ( $wintitle, "", $hanTelescopeControl, "{DOWN}" )
	;$Send("{DOWN}")

	;_GUICtrlComboBox_SetCurSel($hanTelescopeControl,1) //работает, но переключение не вызывает обработчик

	; 2 - Press ok in ascom dialog

	$successflag = WinWaitActive($wintitleASCOM,"",$ascomWaitTimeout) ; wait 5 sec for ASCOM window

	if ($successflag<>0) then

		$hanASCOM_cbDriverSelector = ControlGetHandle($wintitleASCOM,"","[NAME:cbDriverSelector]")
		;MsgBox(1,"ASCOM cbDriverSelector",$hanASCOM_cbDriverSelector)

		$hanASCOM_cmdOK = ControlGetHandle($wintitleASCOM,"","[NAME:cmdOK]")
		;MsgBox(1,"ASCOM cmdOK",$hanASCOM_cmdOK)

		$successflag=ControlClick ($wintitleASCOM,"","[NAME:cmdOK]")
		;MsgBox(1,"ASCOM cmdOK",$successflag)
	else
		;MsgBox(1,"ASCOM cmdOK","Not found window ((")
	endif

EndFunc

;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
; Включить галочку "синхронизировать"
;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
func pressSyncScope()

	$state= ControlCommand($handleWindow, "","[CLASSNN:Button6]", "IsChecked")
	;MsgBox(1,"AstroTortilla sync_checkbox",$state)

	$state2= ControlCommand($handleWindow, "","[CLASSNN:Button6]", "Check")
	;MsgBox(1,"AstroTortilla sync_checkbox",$state2)

EndFunc


;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
; Запустить поиск
;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
func pressStartButton()

	$successflag=ControlClick ($wintitle,"","[CLASSNN:Button8]")
    ;MsgBox(1,"AstroTortilla start button",$successflag)

EndFunc

;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
; Запустить цикл ожидания конца поиска решения
;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
func waitForResult()

    Local $timewaiting=0

    do
        Sleep(1000)

        $statusText = StatusbarGetText ($wintitle)

        $found1 = StringInStr ($statusText, "no solution")
        $found2 = StringInStr ($statusText, "solved in")
        $timewaiting = $timewaiting + 1

    until ($found1 >0 or $found2 >0 or $timewaiting > 60)

    ;MsgBox(1,"Statusbar",$statusText & "[" & $found1 & "_" & $found2  &"]")

    $solved = 1
    if ($found2 > 0) then $solved = 0

    return $solved

EndFunc

;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
; Считать детали решения
;///////////////////////////////////////////////////////////////////////////////////////////////////////////////
func getResultDetails()

   $Solution_RA = ControlGetText($wintitle,"","[CLASS:Static; INSTANCE:14]")
   $Solution_Dec = ControlGetText($wintitle,"","[CLASS:Static; INSTANCE:16]")

   $Current_RA = ControlGetText($wintitle,"","[CLASS:Static; INSTANCE:3]")
   $Current_Dec = ControlGetText($wintitle,"","[CLASS:Static; INSTANCE:4]")

   $Target_RA = ControlGetText($wintitle,"","[CLASS:Static; INSTANCE:25]")
   $Target_Dec = ControlGetText($wintitle,"","[CLASS:Static; INSTANCE:26]")

   ;MsgBox($MB_SYSTEMMODAL, "", "RA:" & $Solution_RA & ", Dec:" & $Solution_Dec)

EndFunc

;///////////////////////////////////////////////////////////////////////////////////////////////////////////////

func SaveResult()
   ; Create a constant variable in Local scope of the filepath that will be read/written to.
   ; Local Const $sFilePath = _WinAPI_GetTempFileName(@TempDir)

   ;But i use temp dir but specify name
   Local Const $sFilePath = @TempDir & "\" & $tempFileName

   MsgBox($MB_SYSTEMMODAL, "", "File: " & $sFilePath)

    ; Create a temporary file to write data to.
    If Not FileWrite($sFilePath, "Test FileWrite. " & @CRLF) Then
        MsgBox($MB_SYSTEMMODAL, "", "An error occurred whilst writing the temporary file.")
        Return False
    EndIf

    ; Open the file for writing (append to the end of a file) and store the handle to a variable.
    Local $hFileOpen = FileOpen($sFilePath, $FO_OVERWRITE )
    If $hFileOpen = -1 Then
        MsgBox($MB_SYSTEMMODAL, "", "An error occurred whilst writing the temporary file.")
        Return False
    EndIf

    ; Write data to the file using the handle returned by FileOpen.
    FileWrite($hFileOpen, "Time: " & _NowCalcDate ( ) & " "& _NowTime(5) & @CRLF)
    FileWrite($hFileOpen, "Result: " & $res & @CRLF)
    FileWrite($hFileOpen, "Solution RA:" & $Solution_RA & @CRLF)
    FileWrite($hFileOpen, "Solution Dec:" & $Solution_Dec & "" & @CRLF)
    FileWrite($hFileOpen, "Current RA:" & $Current_RA & "" & @CRLF)
    FileWrite($hFileOpen, "Current Dec:" & $Current_Dec & "" & @CRLF)
    FileWrite($hFileOpen, "Target RA:" & $Target_RA & "" & @CRLF)
    FileWrite($hFileOpen, "Target Dec:" & $Target_Dec & "" & @CRLF)
    ;FileWrite($hFileOpen, "Line 3" & @CRLF)
    ;FileWrite($hFileOpen, "Line 4")

    ; Close the handle returned by FileOpen.
    FileClose($hFileOpen)

    ; Display the contents of the file passing the filepath to FileRead instead of a handle returned by FileOpen.
    ;MsgBox($MB_SYSTEMMODAL, "", "Contents of the file (" & $sFilePath & "):" & @CRLF & FileRead($sFilePath))

    ; Delete the temporary file.
    ;FileDelete($sFilePath)
EndFunc   ;==>Example
