# Define source and destination folders
$sourceFolder = "./bin/Debug/net10.0/";
$destinationFolder = "C://Users/cam33/AppData/Roaming/RetroForge/Plugins";

if ($args -contains "-clean"){
    if(Test-Path $destinationFolder){
    Remove-Item -Path $destinationFolder -Force
    echo "cleaned $destinationFolder"
}
}

# Create destination folder if it doesn't exist
if (-not (Test-Path -Path $destinationFolder)) {
    New-Item -ItemType Directory -Path $destinationFolder -Force | Out-Null
    Write-Host "Created destination folder: $destinationFolder" -ForegroundColor Green
}

# Get all DLL files from source folder
$dllFiles = Get-ChildItem -Path $sourceFolder -Filter "*-Game.dll" -File

# Check if any DLLs were found
if ($dllFiles.Count -eq 0) {
    Write-Host "No DLL files found in $sourceFolder" -ForegroundColor Yellow
    exit
}

# Copy each DLL to destination
Write-Host "Copying $($dllFiles.Count) DLL file(s)..." -ForegroundColor Cyan

foreach ($dll in $dllFiles) {
    try {
        Copy-Item -Path $dll.FullName -Destination $destinationFolder -Force
        Write-Host "Copied: $($dll.Name)" -ForegroundColor Green
    }
    catch {
        Write-Host "Failed to copy $($dll.Name): $_" -ForegroundColor Red
    }
}

Write-Host "`nCopy operation completed!" -ForegroundColor Green
