Imports FormsAndWpfControls.My.Resources
Imports Xarial.XCad.Base.Attributes
Imports Xarial.XCad.UI.PropertyPage

<Title("WinForms Control")>
<Icon(GetType(Resources), NameOf(Resources.winforms_icon))>
Public Class WinFormsUserControl
    Implements IXCustomControl

    Dim m_DataContext As Object

    Public Property DataContext As Object Implements IXCustomControl.DataContext
        Get
            Return m_DataContext
        End Get
        Set(value As Object)
            m_DataContext = value
            RaiseEvent DataContextChanged(Me, value)
        End Set
    End Property

    Public Event DataContextChanged As Action(Of IXCustomControl, Object) Implements IXCustomControl.DataContextChanged

End Class
