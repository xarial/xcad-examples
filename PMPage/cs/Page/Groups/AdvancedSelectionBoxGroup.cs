using SolidWorks.Interop.sldworks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Base.Enums;
using Xarial.XCad.Examples.PMPage.CSharp.Properties;
using Xarial.XCad.Geometry;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Base;
using Xarial.XCad.UI.PropertyPage.Services;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    public class PlanarFaceCustomFilter : ISelectionCustomFilter
    {
        public bool Filter(IControl selBox, IXSelObject selection, SelectType_e selType, ref string itemText)
        {
            var face = ((ISwSelObject)selection).Dispatch as IFace2;

            if (face.IGetSurface().IsPlane())
            {
                itemText = "Planar Face";
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// This group contains some advanced ways to work with SelectionBox controls
    /// </summary>
    public class AdvancedSelectionBoxGroup 
    {
        /// <summary>
        /// This SelectionBox will contain a custom icon and allow selection of
        /// edges, faces and vertices as defined in the <see cref="SelectionBoxOptionsAttribute.Filters"/>
        /// Secondary color will be used for the SelectionBox
        /// </summary>
        [Icon(typeof(Resources), nameof(Resources.xarial))]
        [SelectionBoxOptions(SelectionColor = StandardSelectionColor_e.Secondary,
            Filters = new SelectType_e[] { SelectType_e.Edges, SelectType_e.Faces, SelectType_e.Vertices })]
        [Description("Standard filters and selection color")]
        public IXSelObject CustomIconSelectionBox { get; set; }

        /// <summary>
        /// Properties of type <see cref="List{IXSelObject}"/> will automatically enable the multi selections
        /// </summary>
        [ControlOptions(height: 50)]
        public List<IXEdge> MultiSelectionBox { get; set; }

        /// <summary>
        /// <see cref="SelectionBoxOptionsAttribute"/> allows to specify the custom filter to allow specific types of entities
        /// This SelectionBox will only allow selection of planar faces
        /// This behavior is defined in <see cref="PlanarFaceCustomFilter.Filter(IControl, IXSelObject, SelectType_e, ref string)"/> method
        /// </summary>
        [Description("Only selects planar face")]
        [SelectionBoxOptions(typeof(PlanarFaceCustomFilter), SelectType_e.Faces)]
        public IXFace CustomSelectionFilter { get; set; }
    }
}
