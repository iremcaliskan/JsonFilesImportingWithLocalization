using Core.Entities;

namespace Entities.Concrete
{
    public class History : IEntity
    { // Entity - Db Table
        public int Id { get; set; }
        
        public string Dc_Date { get; set; }

        public string Dc_Category { get; set; }

        public string Dc_Event { get; set; }

        public short LanguageCode { get; set; }
    }
}