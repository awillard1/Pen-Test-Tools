It is important that the XML have the correct usings and correct version of the .net framework.

The contents of the exploit.xml file has a single iteration of base64 of the following basic code.

namespace BadMojo{

public class BiteMe{

public static void Main(string[] args)

{DoWork();}

public BiteMe(){}

public static void DoWork()

{System.Diagnostics.Process.Start("https://www.google.com");}}}
