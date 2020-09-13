Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports Xarial.XCad.SolidWorks.Documents.Services

Public Class DocHandler
    Inherits SwDocumentHandler

    Protected Overrides Sub AttachPartEvents(part As PartDoc)
        AddHandler part.ConfigurationChangeNotify, AddressOf OnConfigurationChange
    End Sub

    Protected Overrides Sub AttachAssemblyEvents(assm As AssemblyDoc)
        AddHandler assm.ConfigurationChangeNotify, AddressOf OnConfigurationChange
    End Sub

    Protected Overrides Sub AttachDrawingEvents(drw As DrawingDoc)
        AddHandler drw.ActivateSheetPostNotify, AddressOf OnActivateSheet
    End Sub

    Protected Overrides Sub DetachPartEvents(part As PartDoc)
        RemoveHandler part.ConfigurationChangeNotify, AddressOf OnConfigurationChange
    End Sub

    Protected Overrides Sub DetachAssemblyEvents(assm As AssemblyDoc)
        RemoveHandler assm.ConfigurationChangeNotify, AddressOf OnConfigurationChange
    End Sub

    Protected Overrides Sub DetachDrawingEvents(drw As DrawingDoc)
        RemoveHandler drw.ActivateSheetPostNotify, AddressOf OnActivateSheet
    End Sub

    Function OnActivateSheet(SheetName As String) As Integer
        Application.SendMsgToUser($"'{SheetName}' sheet is activated in {Model.GetTitle()}")
        Return 0
    End Function

    Function OnConfigurationChange(ConfigurationName As String, [Object] As Object, ObjectType As Integer, changeType As Integer) As Integer
        Application.SendMsgToUser($"'{ConfigurationName}' configuration is activated in {Model.GetTitle()} [{CType(changeType, swConfigurationChangeTypes_e)}]")
        Return 0
    End Function

End Class
