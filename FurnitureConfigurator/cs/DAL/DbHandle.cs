using System.ComponentModel.DataAnnotations.Schema;

namespace XCad.Examples.FurnitureConfigurator.DAL
{
    [Table("Handles")]
    public class DbHandle 
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public bool InStock { get; set; }
    }
}
