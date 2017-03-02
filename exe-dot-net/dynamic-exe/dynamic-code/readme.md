Warning
==============

All the information provided on this site is for educational purposes only.

The site or the authors are not responsible for any misuse of the information.

You shall not misuse the information to gain unauthorized access and/or write malicious programs.

These information shall only be used to expand knowledge and not for causing malicious or damaging attacks.

Example usage
==============
1. just run it with exploit.xml in the directory of the exe
2. or run it by passing the location of the file with how many iteration the code has been base64 encoded 

It is important that the XML have the correct usings and correct version of the .net framework.

The contents of the exploit.xml file has a single iteration of base64 of the following basic code.

namespace BadMojo{

public class BiteMe{

public static void Main(string[] args)

{DoWork();}

public BiteMe(){}

public static void DoWork()

{System.Diagnostics.Process.Start("https://www.google.com");}}}
