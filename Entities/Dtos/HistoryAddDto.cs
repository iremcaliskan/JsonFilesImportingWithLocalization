using Core.Entities;
using Core.Utilities.Translation;

namespace Entities.Dtos
{
    public class HistoryAddDto : IDto
    { // Complex Type - Data Transfer Object for Different Language Json Importings
        [TranslatedFieldName("ID", Language.EN)]
        [TranslatedFieldName("ID", Language.TR)]
        [TranslatedFieldName("ID", Language.IT)]
        public int Id { get; set; }

        [TranslatedFieldName("dc_Date", Language.EN)]
        [TranslatedFieldName("dc_Zaman", Language.TR)]
        [TranslatedFieldName("dc_Orario", Language.IT)]
        public string Dc_Date { get; set; }

        [TranslatedFieldName("dc_Category", Language.EN)]
        [TranslatedFieldName("dc_Kategori", Language.TR)]
        [TranslatedFieldName("dc_Categoria", Language.IT)]
        public string Dc_Category { get; set; }

        [TranslatedFieldName("dc_Event", Language.EN)]
        [TranslatedFieldName("dc_Olay", Language.TR)]
        [TranslatedFieldName("dc_Evento", Language.IT)]
        public string Dc_Event { get; set; }
    }
}