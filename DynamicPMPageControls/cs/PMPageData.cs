using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Base;
using Xarial.XCad.UI.PropertyPage.Enums;
using Xarial.XCad.UI.PropertyPage.Services;

namespace DynamicPMPageControls
{
    public class VisibilityHandler : IDependencyHandler
    {
        public VisibilityHandler() 
        {
        }

        public void UpdateState(IXApplication app, IControl source, IControl[] dependencies)
        {
            var ctrlsCount = (int)dependencies.First().GetValue();
            var ctrlIndex = (int)source.Metadata.First().Value;
            source.Visible = ctrlIndex <= ctrlsCount;
        }
    }

    [ComVisible(true)]
    public class PMPageData : SwPropertyManagerPageHandler//, INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        public PseudoDynamic Pseudo { get; }

        private int m_ControlsCount;

        [ControlTag(nameof(ControlsCount))]
        [NumberBoxOptions(NumberBoxUnitType_e.UnitlessInteger, 1, 10, 1, true, 1, 1)]
        public int ControlsCount
        {
            get => m_ControlsCount;
            set 
            {
                m_ControlsCount = value;
                //this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ControlsCount)));
            }
        }

        public PMPageData()
        {
            Pseudo = new PseudoDynamic();
            m_ControlsCount = 3;
        }
    }

    public class PseudoDynamic
    {
        [Label("Text Box 1")]
        [Title("Text Box 1")]
        [AttachMetadata(StaticValue = 1)]
        [DependentOn(typeof(VisibilityHandler), nameof(PMPageData.ControlsCount))]
        public string Text1 { get; set; }

        [Label("Text Box 2")]
        [Title("Text Box 2")]
        [AttachMetadata(StaticValue = 2)]
        [DependentOn(typeof(VisibilityHandler), nameof(PMPageData.ControlsCount))]
        public string Text2 { get; set; }

        [Label("Text Box 3")]
        [Title("Text Box 3")]
        [AttachMetadata(StaticValue = 3)]
        [DependentOn(typeof(VisibilityHandler), nameof(PMPageData.ControlsCount))]
        public string Text3 { get; set; }

        [Label("Text Box 4")]
        [Title("Text Box 4")]
        [AttachMetadata(StaticValue = 4)]
        [DependentOn(typeof(VisibilityHandler), nameof(PMPageData.ControlsCount))]
        public string Text4 { get; set; }

        [Label("Text Box 5")]
        [Title("Text Box 5")]
        [AttachMetadata(StaticValue = 5)]
        [DependentOn(typeof(VisibilityHandler), nameof(PMPageData.ControlsCount))]
        public string Text5 { get; set; }

        [Label("Text Box 6")]
        [Title("Text Box 6")]
        [AttachMetadata(StaticValue = 6)]
        [DependentOn(typeof(VisibilityHandler), nameof(PMPageData.ControlsCount))]
        public string Text6 { get; set; }

        [Label("Text Box 7")]
        [Title("Text Box 7")]
        [AttachMetadata(StaticValue = 7)]
        [DependentOn(typeof(VisibilityHandler), nameof(PMPageData.ControlsCount))]
        public string Text7 { get; set; }

        [Label("Text Box 8")]
        [Title("Text Box 8")]
        [AttachMetadata(StaticValue = 8)]
        [DependentOn(typeof(VisibilityHandler), nameof(PMPageData.ControlsCount))]
        public string Text8 { get; set; }

        [Label("Text Box 9")]
        [Title("Text Box 9")]
        [AttachMetadata(StaticValue = 9)]
        [DependentOn(typeof(VisibilityHandler), nameof(PMPageData.ControlsCount))]
        public string Text9 { get; set; }

        [Label("Text Box 10")]
        [Title("Text Box 10")]
        [AttachMetadata(StaticValue = 10)]
        [DependentOn(typeof(VisibilityHandler), nameof(PMPageData.ControlsCount))]
        public string Text10 { get; set; }
    }
}
