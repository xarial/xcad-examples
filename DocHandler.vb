Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports Xarial.XCad
Imports Xarial.XCad.Documents
Imports Xarial.XCad.Documents.Services
Imports Xarial.XCad.SolidWorks.Documents

Public Class DocHandler
    Implements IDocumentHandler

    Dim m_App As IXApplication
    Dim m_Model As SwDocument

    Public Sub Init(app As IXApplication, model As IXDocument) Implements IDocumentHandler.Init

        m_App = app
        m_Model = model

        If TypeOf model Is SwDrawing Then
            AddHandler CType(CType(model, SwDrawing).Drawing, DrawingDoc).ActivateSheetPostNotify, AddressOf OnActivateSheet
        ElseIf TypeOf model Is SwPart Then
            AddHandler CType(CType(model, SwPart).Part, PartDoc).ConfigurationChangeNotify, AddressOf OnConfigurationChange
        ElseIf TypeOf model Is SwAssembly Then
            AddHandler CType(CType(model, SwAssembly).Assembly, AssemblyDoc).ConfigurationChangeNotify, AddressOf OnConfigurationChange
        End If
    End Sub

    Function OnActivateSheet(SheetName As String) As Integer
        m_App.ShowMessageBox($"'{SheetName}' sheet is activated in {m_Model.Title}")
        Return 0
    End Function

    Function OnConfigurationChange(ConfigurationName As String, [Object] As Object, ObjectType As Integer, changeType As Integer) As Integer
        m_App.ShowMessageBox($"'{ConfigurationName}' configuration is activated in {m_Model.Title} [{CType(changeType, swConfigurationChangeTypes_e)}]")
        Return 0
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        If TypeOf m_Model Is SwDrawing Then
            RemoveHandler CType(CType(m_Model, SwDrawing).Drawing, DrawingDoc).ActivateSheetPostNotify, AddressOf OnActivateSheet
        ElseIf TypeOf m_Model Is SwPart Then
            RemoveHandler CType(CType(m_Model, SwPart).Part, PartDoc).ConfigurationChangeNotify, AddressOf OnConfigurationChange
        ElseIf TypeOf m_Model Is SwAssembly Then
            RemoveHandler CType(CType(m_Model, SwAssembly).Assembly, AssemblyDoc).ConfigurationChangeNotify, AddressOf OnConfigurationChange
        End If
    End Sub

End Class
