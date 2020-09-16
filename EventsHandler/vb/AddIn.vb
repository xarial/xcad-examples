Imports System.ComponentModel

<Runtime.InteropServices.ComVisible(True)>
<DisplayName("Event Handlers Example")>
Public Class AddIn
    Inherits Xarial.XCad.SolidWorks.SwAddInEx

    Public Overrides Sub OnConnect()
        Application.Documents.RegisterHandler(Of DocHandler)()
    End Sub
End Class