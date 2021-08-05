using System.ComponentModel.DataAnnotations.Schema;

namespace XCad.Examples.FurnitureConfigurator.DAL
{
    [Table("Frames")]
    public class DbFrame
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public bool InStock { get; set; }
    }
}
