Dim swApp As SldWorks.SldWorks

Sub main()

    Set swApp = Application.SldWorks
    
    Dim swAddInApiExample As Object

    Set swAddInApiExample = swApp.GetAddInObject("{557BB880-4F74-43C3-8244-60AEF26CB5F2}")
    
    swAddInApiExample.SayHello "VBA Macro"
    
End Sub