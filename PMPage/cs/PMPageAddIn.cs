using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Base;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.UI.PropertyPage.Enums;
using Xarial.XCad.UI.PropertyPage.Structures;
using System.Collections;
using System.Drawing;
using Xarial.XCad.Examples.PMPage.CSharp.Page;
using Xarial.XCad.Examples.PMPage.CSharp.Page.Groups;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.UI.Commands.Attributes;
using Xarial.XCad.UI.Commands.Enums;
using Xarial.XCad.Documents;

namespace Xarial.XCad.Examples.PMPage.CSharp
{
    [ComVisible(true)]
    public class PMPageAddIn : SwAddInEx
    {
        [Title("Property Manager Page Example")]
        private enum Commands_e 
        {
            [Title("Open Page")]
            [CommandItemInfo(WorkspaceTypes_e.AllDocuments)]
            OpenPMPage
        }

        private IXPropertyPage<PMPageDataModel> m_Page;

        private PMPageDataModel m_Data;

        public override void OnConnect()
        {
            m_Data = new PMPageDataModel()
            {
                ControlsTab = new Page.Tabs.ControlsTab() 
                {
                    DynamicControlsGroup = new DynamicControlsGroup()
                },
                SimpleControls = new SimpleControlsGroup()
                {
                    TextBox = "Hello xCAD.NET",
                    CheckBox = true,
                    ComboBox = Options_e.Option2
                }
            };

            m_Page = this.CreatePage<PMPageDataModel>(m_Data.ControlsTab.DynamicControlsGroup.CreateDynamicControls);
            
            m_Page.Closing += OnPageClosing;
            m_Page.Closed += OnPageClosed;
            
            this.CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnCommandClick;
        }

        private void OnPageClosing(PageCloseReasons_e reason, PageClosingArg arg)
        {
            if (reason == PageCloseReasons_e.Okay) 
            {
                if (m_Data.DisableClosing) 
                {
                    arg.Cancel = true;
                    arg.ErrorTitle = "xCAD.NET PMPage Example";
                    arg.ErrorMessage = m_Data.CloseErrorMessage;
                }
            }
        }

        private void OnPageClosed(PageCloseReasons_e reason)
        {
            if (reason == PageCloseReasons_e.Okay)
            {
                var res = new StringBuilder();

                ComposeResult(m_Data, res, 0);

                Application.ShowMessageBox(res.ToString());
            }
        }

        private void ComposeResult(object obj, StringBuilder res, int level)
        {
            if (obj != null) 
            {
                foreach (var prp in obj.GetType().GetProperties()) 
                {
                    var val = prp.GetValue(obj, null);
                    string dispVal;

                    if (val == null)
                    {
                        dispVal = "{null}";
                    }
                    else if (val is IEnumerable && !(val is string))
                    {
                        var enumVals = new List<string>();

                        foreach (var elem in val as IEnumerable)
                        {
                            if (elem == null)
                            {
                                enumVals.Add("{null}");
                            }
                            else
                            {
                                enumVals.Add(elem.ToString());
                            }
                        }

                        dispVal = $"[{string.Join(", ", enumVals)}]";
                    }
                    else 
                    {
                        dispVal = val.ToString().Replace("\r\n", "{newline}").Replace("\r", "{newline}").Replace("\n", "{newline}");
                    }

                    var offset = string.Concat(Enumerable.Repeat("    ", level));
                    res.AppendLine($"{offset}{prp.Name} = {dispVal}");

                    if (val != null 
                        && !val.GetType().IsPrimitive 
                        && !val.GetType().IsEnum 
                        && val.GetType() != typeof(string)
                        && !val.GetType().IsEnum
                        && !val.GetType().IsArray
                        && !typeof(Image).IsAssignableFrom(val.GetType())
                        && !typeof(Delegate).IsAssignableFrom(val.GetType())
                        && !typeof(IEnumerable).IsAssignableFrom(val.GetType())
                        && !typeof(IXObject).IsAssignableFrom(val.GetType()))
                    {
                        ComposeResult(val, res, level + 1);
                    }
                }
            }
        }

        private void OnCommandClick(Commands_e spec)
        {
            switch (spec) 
            {
                case Commands_e.OpenPMPage:
                    m_Data.SimpleControls.SelectionBox = (Application.Documents.Active as IXPart)?.Bodies.FirstOrDefault()?.Faces.First();
                    m_Page.Show(m_Data);
                    break;
            }
        }
    }
}
