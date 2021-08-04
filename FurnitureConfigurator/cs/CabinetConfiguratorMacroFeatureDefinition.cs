using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Documents;
using Xarial.XCad.Features.CustomFeature;
using Xarial.XCad.Features.CustomFeature.Delegates;
using Xarial.XCad.Geometry.Structures;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Documents;
using Xarial.XCad.SolidWorks.Features.CustomFeature;
using Xarial.XCad.SolidWorks.Features.CustomFeature.Attributes;
using Xarial.XCad.SolidWorks.Geometry;
using XCad.Examples.FurnitureConfigurator.Properties;
using static XCad.Examples.FurnitureConfigurator.CabinetConfiguratorPage;

namespace XCad.Examples.FurnitureConfigurator
{
    [ComVisible(true)]
    [HandlePostRebuild]
    [Icon(typeof(Resources), nameof(Resources.cabinet_icon))]
    [Title("Cabinet")]
    public class CabinetConfiguratorMacroFeatureDefinition : SwMacroFeatureDefinition<CabinetSizeData, CabinetConfiguratorPage>
    {
        private readonly CabinetConfiguratorService m_Svc;

        public CabinetConfiguratorMacroFeatureDefinition() 
        {
            m_Svc = new CabinetConfiguratorService();
        }

        public override ISwBody[] CreateGeometry(ISwApplication app, ISwDocument model, CabinetSizeData data, bool isPreview, out AlignDimensionDelegate<CabinetSizeData> alignDim)
        {
            alignDim = new AlignDimensionDelegate<CabinetSizeData>((p, d) => 
            {
                switch (p) 
                {
                    case nameof(CabinetSizeData.Width):
                        this.AlignLinearDimension(d, new Point(0, 0, 0), new Vector(1, 0, 0));
                        break;
                    case nameof(CabinetSizeData.Height):
                        this.AlignLinearDimension(d, new Point(0, 0, 0), new Vector(0, 1, 0));
                        break;
                    case nameof(CabinetSizeData.Depth):
                        this.AlignLinearDimension(d, new Point(0, 0, 0), new Vector(0, 0, -1));
                        break;
                }
            });

            return new ISwBody[0];
        }

        public override void OnPostRebuild(ISwApplication app, ISwDocument model, ISwMacroFeature<CabinetSizeData> feature, CabinetSizeData parameters)
        {
            m_Svc.Configure((IXAssembly)model, parameters.Width, parameters.Height, parameters.Depth,
                parameters.NumberOfDrawers, parameters.DrawerWidth, parameters.DrawerHandleType);
        }

        public override CabinetSizeData ConvertPageToParams(CabinetConfiguratorPage par)
            => new CabinetSizeData()
            {
                Width = par.Size.Width,
                Height = par.Size.Height,
                Depth = par.Size.Depth,
                DrawerWidth = par.Size.DrawerWidth,
                NumberOfDrawers = par.Size.NumberOfDrawers,
                DrawerHandleType = par.Size.DrawerHandleType
            };

        public override CabinetConfiguratorPage ConvertParamsToPage(CabinetSizeData par)
            => new CabinetConfiguratorPage() 
            {
                Size = new CabinetSizeGroup() 
                {
                    Width = par.Width,
                    Height = par.Height,
                    Depth = par.Depth,
                    DrawerWidth = par.DrawerWidth,
                    NumberOfDrawers = par.NumberOfDrawers,
                    DrawerHandleType = par.DrawerHandleType
                }
            };
    }
}
