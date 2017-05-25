
function get-shit {
$ass = Get-ItemProperty "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\LastVisitedPidlMRU"
$ass.PSObject.Properties | ForEach-Object{
try{
$v=$_.Value
$x = ([System.Text.Encoding]::Unicode.GetString($v))
$x -replace '[^\u0020-\u007E]+', ''''
}
catch{}
}}
get-shit
