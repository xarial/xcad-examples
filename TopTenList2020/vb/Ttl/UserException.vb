Imports System
Imports Xarial.XCad.Exceptions

Public Class UserException
    Inherits Exception
    Implements IUserException
    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
End Class
