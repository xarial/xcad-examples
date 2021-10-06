using System.ComponentModel.DataAnnotations.Schema;

namespace XCad.Examples.FurnitureConfigurator.DAL
{
    public enum PanelType_e
    {
        PanelBase, 
        PanelEndLH,
        PanelEndRH,
        PanelTop,
        PanelRear,
        PanelInternal
    }

    [Table("Panels")]
    public class DbPanel
    {
        public int Id { get; set; }
        public PanelType_e Type { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool InStock { get; set; }
    }
}
