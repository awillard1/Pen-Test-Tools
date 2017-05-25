function zipFiles($targets,$fileToZipTo){
    Add-Type -assembly  System.IO.Compression.FileSystem
    [System.IO.Compression.CompressionLevel]$Compression = "Optimal"
    [System.IO.Compression.ZipArchive]$zip


    if (!(Test-Path $fileToZipTo)){
        $zip = [System.IO.Compression.ZipFile]::Open($fileToZipTo,[System.IO.Compression.ZipArchiveMode]::Create)
        $zip.Dispose()
    }
    ForEach($target in $targets){
        if (![string]::IsNullOrEmpty($target)){
            $target
            $zip = [io.compression.zipfile]::Open($fileToZipTo,[System.IO.Compression.ZipArchiveMode]::Update)
            [System.io.compression.ZipFileExtensions]::CreateEntryFromFile($zip, $target, $target)
            $zip.Dispose();
        }
    }
}