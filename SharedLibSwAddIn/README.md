# Resolving Shared Dlls Conflict In SOLIDWORKS Add-Ins

This exampel demonstrates various conflicts when shared library with different versions is used in different SOLIDWORKS add-ins loaded into the same session of SOLIDWORKS.

## Test Case 1 - Add-in are using the same version of shared library

* Compile as is
* Load in SOLIDWORKS
* Both add-ins are loaded correctly

## Test Case 2 - Add-ins are using different versions of a shared library

* Replace reference in **SecondSwAddIn** to **Lib\1.1.0.0\SharedLib.dll**
* Uncomment the line

~~~ cs
Application.ShowMessageBox($"2nd AddIn: {new SharedUtil().GetMessage("User1")}");
~~~

* Compile both projects
* Load SOLIDWORKS
* Both add-ins are loaded correctly

## Test Case 3 - Add-ins are using different versions of a shared library with missing version in second add-in

* Replace the **SharedLib.dll** in the **SecondSwAddIn** bin folder with the **Lib\2.0.0.0\SharedLib.dll**
* Do not recomile (otherwise output library will be overwritten)
* Start SOLIDWORKS
* First add-in displays the message box
* Second add-in fails with the error

~~~
Could not load file or assembly 'SharedLib, Version=1.1.0.0, Culture=neutral, PublicKeyToken=edd202a4e66b8d76' or one of its dependencies. The system cannot find the file specified.
~~~

## Test Case 4 - Add-ins are using different versions of a shared library with missing version in second add-in(AssemblyResolve->LoadFrom fix)

* Uncomment the static constructor in both add-ins
* Recompile the code
* Replace the **SharedLib.dll** in the **SecondSwAddIn** bin folder with the **Lib\2.0.0.0\SharedLib.dll**
* Start SOLIDWORKS
* First add-in displays the message box
* Second add-in fails with the error

~~~
Method not found: 'System.String Xarial.XCad.Examples.SharedUtil.GetMessage(System.String)'.
~~~

* Unckeck the first add-in from the startup in **Tools->AddIns...** menu and restart
* Message displayed successfully

## Test Case 5 - Add-ins are using different versions of a shared library with missing version in second add-in(AssemblyResolve->LoadFrom fix and version check)

* Uncomment the following code in both add-ins

~~~ cs
var candAssmName = AssemblyName.GetAssemblyName(assmFilePath);

if (candAssmName.Version >= assmName.Version)
{
    return Assembly.LoadFrom(assmFilePath);
}
~~~

* Recompile the code
* Replace the **SharedLib.dll** in the **SecondSwAddIn** bin folder with the **Lib\2.0.0.0\SharedLib.dll**
* Start SOLIDWORKS
* First add-in displays the message box
* Second add-in displays the message box

## Test Case 5 - Final Solution

* Restore the original code
* Replace reference in **SecondSwAddIn** to **Lib\1.1.0.0\SharedLib.dll**
* Uncomment the line

~~~ cs
Application.ShowMessageBox($"2nd AddIn: {new SharedUtil().GetMessage("User1")}");
~~~

* Download the [AssemblyReferenceResolver.cs](https://github.com/xarial/xtoolkit/blob/dev/src/Utils/Reflection/AssemblyReferenceResolver.cs) file and add to both projects
* Uncomment the following code in both add-ins:

~~~ cs
private readonly Xarial.XToolkit.Reflection.AssemblyReferenceResolver m_AssmRefsResolver;

public FirstSwAddIn() 
{
    m_AssmRefsResolver = new Xarial.XToolkit.Reflection.AssemblyReferenceResolver(
        AppDomain.CurrentDomain, Path.GetDirectoryName(typeof(FirstSwAddIn).Assembly.Location), "1st xCAD AddIn");
}

public override void OnDisconnect()
{
    m_AssmRefsResolver.Dispose();
}
~~~

* Compile both projects
* Replace the **SharedLib.dll** in the **SecondSwAddIn** bin folder with the **Lib\2.0.0.0\SharedLib.dll**
* Do not recomile (otherwise output library will be overwritten)
* Optionally open the [DebugView](https://learn.microsoft.com/en-us/sysinternals/downloads/debugview) or any otehr trace listener and specify the **\*xCAD\*** filter
* Add-ins are loaded successfully and the following message is output to the trace

~~~
1st xCAD AddIn: Resolving ambiguity for 'SharedLib, Version=1.1.0.0, Culture=neutral, PublicKeyToken=edd202a4e66b8d76' 
1st xCAD AddIn: Ambiguity for 'SharedLib, Version=1.1.0.0, Culture=neutral, PublicKeyToken=edd202a4e66b8d76' is not resolved 
2nd xCAD AddIn: Resolving ambiguity for 'SharedLib, Version=1.1.0.0, Culture=neutral, PublicKeyToken=edd202a4e66b8d76' 
2nd xCAD AddIn: Ambiguity for 'SharedLib, Version=1.1.0.0, Culture=neutral, PublicKeyToken=edd202a4e66b8d76' is resolved by nearest available version of the assembly 
2nd xCAD AddIn: Loading assembly 'SharedLib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=edd202a4e66b8d76' 
~~~