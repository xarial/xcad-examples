Imports FormsAndWpfControls.My.Resources
Imports Xarial.XCad.Base.Attributes
Imports Xarial.XCad.UI.PropertyPage

<Title("WinForms Control")>
<Icon(GetType(Resources), NameOf(Resources.winforms_icon))>
Public Class WinFormsUserControl
    Implements IXCustomControl

    Dim m_Value As Object

    Public Property Value As Object Implements IXCustomControl.Value
        Get
            Return m_Value
        End Get
        Set(value As Object)
            m_Value = value
            RaiseEvent ValueChanged(Me, value)
        End Set
    End Property

    Public Event ValueChanged As CustomControlValueChangedDelegate Implements IXCustomControl.ValueChanged

End Class
