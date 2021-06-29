using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IHistoryDal : IEntityRepository<History>
    { // IHistoryDal implements IEntityRepository<History> for History CRUD Operations 
        // Special Methods Area for History Data Access
        void ImportJson(string importLanguage);
    }
}