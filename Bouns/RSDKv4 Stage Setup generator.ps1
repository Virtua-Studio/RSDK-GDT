#region Vars
    #region Description
        $ProjectName = Read-Host -Prompt 'Input your RSDK Project name'
        $StageName = Read-Host -Prompt 'Input the stage name'
        $User = Read-Host -Prompt 'Input your user name'
		$AliasesInput = Read-Host -Prompt 'How many aliases? (Max is 47)'
    #endregion Description
	
    #region Music
        $MusNameInput   = Read-Host -Prompt 'What is the music file'
        $MusLoop1       = Read-Host -Prompt 'Input music loop point'
        $FMusNameInput  = Read-Host -Prompt 'What is the music for speed shoes file'
        $MusLoop2       = Read-Host -Prompt 'Input music speed up loop point'
        $SpeedMus       = "$StageName" + "Setup_SpeedUpMusic"
        $StageName_F    = "$StageName" + "_F"
        $SlowMus        = "$StageName" + "Setup_SlowDownMusic"
        $MusName        = "$MusNameInput" + ".ogg"
        $Mus_FName      = "$FMusNameInput" + "_F.ogg"
    #endregion Music

    #region setup   
        $SpriteSheet = "Global/Display.gif"
        $EditorAtribute = "unused"
        $Aliases = 0
        $FileName = ".\$StageName" + "Setup.txt"
    #endregion setup
#endregion Vars

$FileExists = Test-Path -Path $FileName -PathType Leaf

#region Code
If($FileExists) # Is there a file with the same name in the same folder?
{
    Write-Error "$FileName already exists"
    # Exit # Stops the script.

}
Else # It doesn't exist yet.
{
#region RSDKv4            
    Generating Script
    # make a file
    New-Item -ItemType File -Name $FileName -ErrorAction SilentlyContinue | Out-Null
    # Put all of this in it
    Add-Content $FileName -Value "// ----------------------------------"
    Add-Content $FileName -Value "// RSDK Project:  $ProjectName"
    Add-Content $FileName -Value "// Script Description:    $StageName Setup Object "
    Add-Content $FileName -Value "// Script Author: $User"
    Add-Content $FileName -Value "// ----------------------------------"
    Add-Content $FileName -Value "" 
    Add-Content $FileName -Value "// ========================"
    Add-Content $FileName -Value "// Aliases"
    Add-Content $FileName -Value "// ========================"
    Add-Content $FileName -Value ""
    while($Aliases -le $AliasesInput)
    {
        Add-Content $FileName -Value "private alias object.value$Aliases 	:"
        $Aliases++
    }
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "// Tracks"
    Add-Content $FileName -Value "private alias 0 : TRACK_STAGE"
    Add-Content $FileName -Value "private alias 1 : TRACK_ACTFINISH"
    Add-Content $FileName -Value "private alias 2 : TRACK_INVINCIBLE"
    Add-Content $FileName -Value "private alias 3 : TRACK_CONTINUE"
    Add-Content $FileName -Value "private alias 4 : TRACK_BOSS"
    Add-Content $FileName -Value "private alias 5 : TRACK_GAMEOVER"
    Add-Content $FileName -Value "private alias 6 : TRACK_DROWNING"
    Add-Content $FileName -Value "private alias 7 : TRACK_SUPER"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "// Reserved Object Slots Aliases"
    Add-Content $FileName -Value "private alias 10 : SLOT_ZONESETUP"
    Add-Content $FileName -Value "private alias 25 : SLOT_MUSICEVENT_CHANGE"
    Add-Content $FileName -Value "private alias 26 : SLOT_MUSICEVENT_BOSS"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "// Music Events"
    Add-Content $FileName -Value "private alias 0 : MUSICEVENT_FADETOBOSS"
    Add-Content $FileName -Value "private alias 1 : MUSICEVENT_FADETOSTAGE"
    Add-Content $FileName -Value "private alias 2 : MUSICEVENT_TRANSITION"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "private alias 0 : MUSICEVENT_FLAG_NOCHANGE"
    Add-Content $FileName -Value "private alias 1 : MUSICEVENT_FLAG_SPEEDUP"
    Add-Content $FileName -Value "private alias 2 : MUSICEVENT_FLAG_SLOWDOWN"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "// Music Loops"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "private alias $MusLoop1 : MUSIC_LOOP_$StageName"
    Add-Content $FileName -Value "private alias $MusLoop2 : MUSIC_LOOP_$StageName_F"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "private alias 38679  : MUSIC_LOOP_INV"
    Add-Content $FileName -Value "private alias 30897  : MUSIC_LOOP_INV_F"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "// ========================"
    Add-Content $FileName -Value "// Function Declarations"
    Add-Content $FileName -Value "// ========================"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "reserve function $SpeedMus"
    Add-Content $FileName -Value "reserve function $SlowMus"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "// ========================"
    Add-Content $FileName -Value "// Static Values"
    Add-Content $FileName -Value "// ========================"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "// ========================"
    Add-Content $FileName -Value "// Tables"
    Add-Content $FileName -Value "// ========================"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "// ========================"
    Add-Content $FileName -Value "// Function Definitions"
    Add-Content $FileName -Value "// ========================"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "private function $SpeedMus"
    Add-Content $FileName -Value "	CheckEqual(object[SLOT_MUSICEVENT_CHANGE].type, TypeName[Music Event])"
    Add-Content $FileName -Value "	temp0 = checkResult"
    Add-Content $FileName -Value "	CheckEqual(object[SLOT_MUSICEVENT_CHANGE].propertyValue, MUSICEVENT_TRANSITION)"
    Add-Content $FileName -Value "	temp0 &= checkResult"
    Add-Content $FileName -Value "	CheckEqual(stage.musicFlag, MUSICEVENT_FLAG_NOCHANGE)"
    Add-Content $FileName -Value "	temp0 &= checkResult"
    Add-Content $FileName -Value "	if temp0 == 0"
    Add-Content $FileName -Value "		switch music.currentTrack"
    Add-Content $FileName -Value "		case TRACK_STAGE"
    Add-Content $FileName -Value "			SetMusicTrack(`"Invincibility_F.ogg`", TRACK_INVINCIBLE, MUSIC_LOOP_INV_F)"   
    Add-Content $FileName -Value "			SwapMusicTrack(`"$Mus_FName`", TRACK_STAGE, MUSIC_LOOP_$StageName_F, 8000)"
    Add-Content $FileName -Value "			break"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "		case TRACK_INVINCIBLE"
    Add-Content $FileName -Value "			SetMusicTrack(`"$Mus_FName`", TRACK_STAGE, MUSIC_LOOP_$StageName_F)"
    Add-Content $FileName -Value "			SwapMusicTrack(`"Invincibility_F.ogg`", TRACK_INVINCIBLE, MUSIC_LOOP_INV_F, 8000)"
    Add-Content $FileName -Value "			break"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "		case TRACK_BOSS"
    Add-Content $FileName -Value "		case TRACK_DROWNING"
    Add-Content $FileName -Value "		case TRACK_SUPER"
    Add-Content $FileName -Value "			SetMusicTrack(`"$Mus_FName`", TRACK_STAGE, MUSIC_LOOP_$StageName_F)"
    Add-Content $FileName -Value "			SetMusicTrack(`"Invincibility_F.ogg`", TRACK_INVINCIBLE, MUSIC_LOOP_INV_F)"
    Add-Content $FileName -Value "			break"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "		end switch"
    Add-Content $FileName -Value "	else"
    Add-Content $FileName -Value "		stage.musicFlag = MUSICEVENT_FLAG_SPEEDUP"
    Add-Content $FileName -Value "	end if"
    Add-Content $FileName -Value "end function"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "private function $SlowMus"
    Add-Content $FileName -Value "	CheckEqual(object[SLOT_MUSICEVENT_CHANGE].type, TypeName[Music Event])"
    Add-Content $FileName -Value "	temp0 = checkResult"
    Add-Content $FileName -Value "	CheckEqual(object[SLOT_MUSICEVENT_CHANGE].propertyValue, MUSICEVENT_TRANSITION)"
    Add-Content $FileName -Value "	temp0 &= checkResult"
    Add-Content $FileName -Value "	CheckEqual(stage.musicFlag, MUSICEVENT_FLAG_NOCHANGE)"
    Add-Content $FileName -Value "	temp0 &= checkResult"
    Add-Content $FileName -Value "	if temp0 == 0"
    Add-Content $FileName -Value "		switch music.currentTrack"
    Add-Content $FileName -Value "		case TRACK_STAGE"
    Add-Content $FileName -Value "			SetMusicTrack(`"Invincibility.ogg`", TRACK_INVINCIBLE, MUSIC_LOOP_INV)"   
    Add-Content $FileName -Value "			SwapMusicTrack(`"$MusName`", TRACK_STAGE, MUSIC_LOOP_$StageName, 12500)"
    Add-Content $FileName -Value "			break"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "		case TRACK_INVINCIBLE"
    Add-Content $FileName -Value "			SetMusicTrack(`"$MusName`", TRACK_STAGE, MUSIC_LOOP_$StageName)"
    Add-Content $FileName -Value "			SwapMusicTrack(`"Invincibility.ogg`", TRACK_INVINCIBLE, MUSIC_LOOP_INV, 12500)"
    Add-Content $FileName -Value "			break"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "		case TRACK_BOSS"
    Add-Content $FileName -Value "		case TRACK_DROWNING"
    Add-Content $FileName -Value "		case TRACK_SUPER"
    Add-Content $FileName -Value "			SetMusicTrack(`"$MusName`", TRACK_STAGE, MUSIC_LOOP_$StageName)"
    Add-Content $FileName -Value "			SetMusicTrack(`"Invincibility.ogg`", TRACK_INVINCIBLE, MUSIC_LOOP_INV)"
    Add-Content $FileName -Value "			break"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "		end switch"
    Add-Content $FileName -Value "	else"
    Add-Content $FileName -Value "		stage.musicFlag = MUSICEVENT_FLAG_SLOWDOWN"
    Add-Content $FileName -Value "	end if"
    Add-Content $FileName -Value "end function"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "// ========================"
    Add-Content $FileName -Value "// Events"
    Add-Content $FileName -Value "// ========================"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "event ObjectUpdate"
    Add-Content $FileName -Value "end event"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "event ObjectDraw"
    Add-Content $FileName -Value "end event"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "event ObjectStartup"
    Add-Content $FileName -Value "	SetMusicTrack(`"$MusName`", 0, MUSIC_LOOP_$StageName)"
    Add-Content $FileName -Value "	SpeedUpMusic = $SpeedMus"
    Add-Content $FileName -Value "	SlowDownMusic = $SlowMus"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "	animalType1 = TypeName[Flicky]"
    Add-Content $FileName -Value "	animalType2 = TypeName[Ricky]"
    Add-Content $FileName -Value "	object[SLOT_ZONESETUP].type = TypeName[$StageName Setup]"
    Add-Content $FileName -Value "	object[SLOT_ZONESETUP].priority = PRIORITY_ACTIVE"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "end event"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "// ========================"
    Add-Content $FileName -Value "// Editor Events"
    Add-Content $FileName -Value "// ========================"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "event RSDKDraw"
    Add-Content $FileName -Value "  DrawSprite(0)"
    Add-Content $FileName -Value "end event"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "event RSDKLoad"
    Add-Content $FileName -Value "  LoadSpriteSheet($SpriteSheet)"
    Add-Content $FileName -Value "  SpriteFrame(-16, -16, 32, 32, 1, 143)"
    Add-Content $FileName -Value ""
    Add-Content $FileName -Value "  SetVariableAlias(ALIAS_VAR_PROPVAL, $EditorAtribute)"
    Add-Content $FileName -Value "end event"
#endregion RSDKv4
   
}

Code $FileName # Open in Visual Studio Code
#notepad++ $FileName # Open in notepad++