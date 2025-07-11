# ========================== #
# WinSysTunerZ Velopack Build Script mit Auto-Push nach RELEASES/
# ========================== #

$projectRoot = "D:\000_Projekte\Windows_System_Tuner"
$buildDir    = "$projectRoot\Release"
$outDir      = "$projectRoot\RELEASES"
$appExe      = "WinSysTunerZ.exe"
$themesDir   = "$projectRoot\WinSysTunerZ\Themes"
$destThemes  = "$buildDir\Themes"

# Prüfe, ob vpk global installiert ist
if (-not (Get-Command vpk -ErrorAction SilentlyContinue)) {
    Write-Host "Velopack (vpk) ist nicht global installiert! Bitte mit 'dotnet tool install --global vpk' installieren."
    exit 1
}

# RELEASES-Ordner neu erstellen
if (Test-Path $outDir) { Remove-Item $outDir -Recurse -Force }
New-Item -ItemType Directory -Path $outDir | Out-Null

# Themes ins Release kopieren
if (Test-Path $destThemes) { Remove-Item $destThemes -Recurse -Force }
Copy-Item "$themesDir\*" -Destination $destThemes -Recurse -Force

# Versionsnummer holen (aus .exe)
$appVersion = (Get-Command "$buildDir\$appExe").FileVersionInfo.ProductVersion
# ODER: Alternativ direkt aus csproj parsen, falls Version nicht in exe eingebrannt!
# $csproj = Get-Content "$projectRoot\WinSysTunerZ\WinSysTunerZ.csproj"
# $appVersion = ($csproj | Select-String -Pattern "<Version>(.*)</Version>").Matches[0].Groups[1].Value

$appId = "WinSysTunerZ"

# Velopack pack (erstellt Update/Installer-Dateien im $outDir)
vpk pack --packId $appId --packVersion $appVersion --packDir "$buildDir" --mainExe "$appExe" --outputDir "$outDir"

# ----> NUR die Inhalte aus RELEASES nach Git pushen <----
Set-Location $outDir

# Nur geänderte/neue Dateien in Git aufnehmen und committen!
git add -A
git commit -m "Neues WinSysTunerZ Release $appVersion - $(Get-Date -Format yyyy-MM-dd_HH-mm)"
git push

Write-Host "`nFertig: Installer & Update-Dateien liegen im Ordner RELEASES und sind im Git-Repo gepusht."

Pause
