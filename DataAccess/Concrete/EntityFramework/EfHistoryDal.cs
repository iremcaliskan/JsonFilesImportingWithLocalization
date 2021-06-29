using AutoMapper;
using Core.DataAccess;
using Core.Utilities.Translation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Dtos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfHistoryDal : EfEntityRepositoryBase<History, TestCaseContext>, IHistoryDal
    { //  EfHistoryDal inherits EfEntityRepositoryBase<History, TestCaseContext> due to History CRUD operation over DB(TestCaseContext)
      //  Also inherits IHistoryDal to fill the empty methods with EfEntityRepositoryBase
        private readonly IMapper _mapper; // Mapper for Json Import without LanguageCode

        public EfHistoryDal(IMapper mapper)
        { // Constructure injection, less dependency
            _mapper = mapper;
        }

        public void ImportJson(string importLanguage)
        {
            if (importLanguage == "tr")
            {
                string file = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\JsonDatas\data-tr.json";
                StreamReader reader = new StreamReader(file);
                string jsonTurkish = reader.ReadToEnd();

                List<HistoryAddDto> historiesTurkish = SerializeHelper<HistoryAddDto>.DeSerialize(jsonTurkish, Core.Entities.Language.TR);
                var turkishToEnglishJson = SerializeHelper<HistoryAddDto>.Serialize(historiesTurkish, Core.Entities.Language.EN);

                List<HistoryAddDto> historiesTurkishToEnglish = JsonConvert.DeserializeObject<List<HistoryAddDto>>(turkishToEnglishJson);
                var turkishListMapReverse = _mapper.Map<List<History>>(historiesTurkishToEnglish);

                using (var context = new TestCaseContext())
                {
                    foreach (var item in turkishListMapReverse)
                    {
                        item.LanguageCode = 0; // tr code
                        context.Set<History>().Add(item);
                    }
                    context.SaveChanges();
                }
            }
            else if (importLanguage == "it")
            {
                string file = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\JsonDatas\data-it.json";
                StreamReader reader = new StreamReader(file);
                string jsonItalian = reader.ReadToEnd();

                List<HistoryAddDto> historiesItalian = SerializeHelper<HistoryAddDto>.DeSerialize(jsonItalian, Core.Entities.Language.IT);
                var italianToEnglishJson = SerializeHelper<HistoryAddDto>.Serialize(historiesItalian, Core.Entities.Language.EN);

                List<HistoryAddDto> historiesItalianToEnglish = JsonConvert.DeserializeObject<List<HistoryAddDto>>(italianToEnglishJson);
                var italianListMapReverse = _mapper.Map<List<History>>(historiesItalianToEnglish);

                using (var context = new TestCaseContext())
                {
                    foreach (var item in italianListMapReverse)
                    {
                        item.LanguageCode = 1; // it code
                        context.Set<History>().Add(item);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}