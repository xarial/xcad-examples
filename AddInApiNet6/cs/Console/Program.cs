using System.Diagnostics;
using Xarial.XCad.SolidWorks;

var app = SwApplicationFactory.FromProcess(Process.GetProcessesByName("SLDWORKS").First());

var addInApiExample = (dynamic)app.Sw.GetAddInObject("{557BB880-4F74-43C3-8244-60AEF26CB5F2}");

addInApiExample.SayHello(".NET6 Console");
