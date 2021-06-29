using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IHistoryService
    {
        List<History> GetAllByTurkish();
        List<History> GetAllByItalian();
        History GetByIdInTurkishList(int id);
        History GetByIdInItalianList(int id);
        void Add(History history);
        void Update(History history);
        void Delete(History history);
        void ImportJson(string importLanguage);
    }
}