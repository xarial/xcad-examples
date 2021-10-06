using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Documents;
using Xarial.XCad.Documents.Services;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Documents;
using Xarial.XCad.SolidWorks.Documents.Services;
using Xarial.XCad.SolidWorks.UI;

namespace Xarial.XCad.Examples.DocumentFeatureTabs
{
    public class SelectionsDocumentHandler : SwDocumentHandler
    {
        private readonly ISwAddInEx m_AddIn;
        private ISwFeatureMgrTab<SelectionsControl> m_Tab;
        private readonly SelectionsVM m_ViewModel;
        private ISwDocument m_Doc;

        public SelectionsDocumentHandler(ISwAddInEx addIn) 
        {
            m_AddIn = addIn;
            m_ViewModel = new SelectionsVM();
        }

        protected override void OnInit(ISwApplication app, ISwDocument doc)
        {
            m_Doc = doc;
            UpdateSelectionsViewModel();

            m_Doc.Selections.ClearSelection += OnClearSelection;
            m_Doc.Selections.NewSelection += OnNewSelection;

            m_Tab = m_AddIn.CreateFeatureManagerTabWpf<SelectionsControl>(doc);

            if (m_Tab.IsControlCreated)
            {
                m_Tab.Control.DataContext = m_ViewModel;
            }

            m_Tab.ControlCreated += OnControlCreated;
        }

        private void OnNewSelection(IXDocument doc, IXSelObject selObject)
            => UpdateSelectionsViewModel();

        private void OnClearSelection(IXDocument doc)
            => UpdateSelectionsViewModel();

        private void UpdateSelectionsViewModel()
            => m_ViewModel.SelectionsCount = m_Doc.Selections.Count;

        private void OnControlCreated(SelectionsControl ctrl)
        {
            m_Tab.Control.DataContext = m_ViewModel;
        }

        protected override void Dispose(bool disposing)
        {
            m_Doc.Selections.ClearSelection -= OnClearSelection;
            m_Doc.Selections.NewSelection -= OnNewSelection;
        }
    }
}
