if ( $Args[0] -eq $null ) {
    Write-Error "First parameter must be output folder"
    return
}

if( !(Test-Path "assets/audio") )
{
    New-Item -ItemType Directory -Path "assets/audio"
}
if( !(Test-Path "assets/icons") )
{
    New-Item -ItemType Directory -Path "assets/icons"
}

foreach ($folder in Get-ChildItem ($Args[0] + "character voices")) {
    if ( $folder.Name -eq "_lastupdate.md" ) {
        continue
    }
    foreach ($file in Get-ChildItem $folder.FullName) {
        $targetFolder = "assets\\audio\\" + $folder.Name.replace(' ','')
        if( !(Test-Path $targetFolder) )
        {
            New-Item -ItemType Directory -Path $targetFolder
        }
        if ( $file.Name -match "lobby interactive voice ([0-9])\.mp3" ) {
            Copy-Item $file.FullName -Destination ($targetFolder + "\\lobby" + $matches[1] + ".mp3")
        }
        elseif ( $file.Name -match "bond level up voice ([0-9])\.mp3" ) {
            Copy-Item $file.FullName -Destination ($targetFolder + "\\bond" + $matches[1] + ".mp3")
        }
        elseif ( $file.Name -eq "login voice - normal.mp3" ) {
            Copy-Item $file.FullName -Destination ($targetFolder + "\\login_normal.mp3")
        }
        elseif ( $file.Name -eq "login voice - max bond.mp3" ) {
            Copy-Item $file.FullName -Destination ($targetFolder + "\\login_max.mp3")
        }
        elseif ( $file.Name -eq "receiving gift - normal.mp3" ) {
            Copy-Item $file.FullName -Destination ($targetFolder + "\\gift_normal.mp3")
        }
        elseif ( $file.Name -eq "receiving gift - preferred.mp3" ) {
            Copy-Item $file.FullName -Destination ($targetFolder + "\\gift_preferred.mp3")
        }
        elseif ( $file.Name -eq "voice when obtained.mp3" ) {
            Copy-Item $file.FullName -Destination ($targetFolder + "\\obtained.mp3")
        }
    }
    Copy-Item ($Args[0] + "portraits\\" + $folder.Name + "\\Default\\smallhead.png") -Destination ("assets\\icons\\" + $folder.Name.replace(' ','') + ".png")
}