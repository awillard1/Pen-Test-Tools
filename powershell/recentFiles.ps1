function GetRecentlyUsedFiles(){
    foreach($user in (Get-ChildItem "$env:SystemDrive\Users")) {
        $recentpath = "C:\Users\$user\AppData\Roaming\Microsoft\Windows\Recent\"
        if (Test-Path($recentpath))
        {
            $recentdocs = Get-childItem $recentpath
            foreach($doc in $recentdocs){
                try{
                    $filepath = $recentpath + $doc
                    $sh = New-Object -ComObject WScript.Shell
                    $target = $sh.CreateShortcut($filepath).TargetPath
                    if (![string]::IsNullOrEmpty($target)){
                        $target
                        }
                    }
                catch {}
            }
        }
    }
}