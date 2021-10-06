using System.ComponentModel.DataAnnotations.Schema;

namespace XCad.Examples.FurnitureConfigurator.DAL
{
    [Table("Doors")]
    public class DbDoor 
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool InStock { get; set; }
    }
}
