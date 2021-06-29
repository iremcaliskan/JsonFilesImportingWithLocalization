using AutoMapper;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entities;
using Core.Utilities.Translation;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class HistoryManager : IHistoryService
    {
        private readonly IHistoryDal _historyDal;
        private readonly IMapper _mapper;

        public HistoryManager(IHistoryDal historyDal, IMapper mapper)
        { // It needs a data access technique at creation time of class, newing time
            _historyDal = historyDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(HistoryValidator))] // Validate structure of the entity
        [CacheRemoveAspect("IHistory.Get")] // Clear datas in cache
        [TransactionScopeAspect] // Transaction(operation) recovery, rollback
        public void Add(History history)
        {
            _historyDal.Add(history);
        }

        [ValidationAspect(typeof(HistoryValidator))]
        [CacheRemoveAspect("IHistory.Get")]
        [TransactionScopeAspect]
        public void Update(History history)
        {
            _historyDal.Update(history);
        }

        [CacheRemoveAspect("IHistoryService.Get")]
        [TransactionScopeAspect]
        public void Delete(History history)
        {
            _historyDal.Delete(history);
        }

        [CacheAspect] // If the operation is not corrupted with CRUD, fetch the data from the cache instead of making more requests but if the request has been made before
        [PerformanceAspect(15)] // For Performance issue, if process time takes time longer than 15 seconds, warn 
        public History GetByIdInTurkishList(int id)
        {
            return _historyDal.Get(x => x.Id == id && x.LanguageCode == 0);
        }

        [CacheAspect]
        [PerformanceAspect(15)]
        public History GetByIdInItalianList(int id)
        {
            return _historyDal.Get(x => x.Id == id && x.LanguageCode == 1);
        }

        [CacheAspect]
        [PerformanceAspect(15)]
        public List<History> GetAllByTurkish()
        {
            var turkishList = _historyDal.GetAll(x => x.LanguageCode == 0);
            var turkishListMapToGet = _mapper.Map<List<HistoryGetDto>>(turkishList);
            var turkishListMapFromGetToAdd = _mapper.Map<List<HistoryAddDto>>(turkishListMapToGet);
            var json = SerializeHelper<HistoryAddDto>.Serialize(turkishListMapFromGetToAdd, Language.TR);
            var list = SerializeHelper<HistoryAddDto>.DeSerialize(json, Language.TR);
            var turkishListMapReverse = _mapper.Map<List<History>>(list);
            return turkishListMapReverse.OrderBy(x => x.Id).ToList();
        }

        //[CacheAspect]
        //[PerformanceAspect(5)]
        //public List<History> GetAllByEnglish()
        //{
        //    var json = SerializeHelper<History>.Serialize(_historyDal.GetAll(), Language.EN);
        //    var list = SerializeHelper<History>.DeSerialize(json, Language.EN);
        //    return list;
        //}

        [CacheAspect]
        [PerformanceAspect(15)]
        public List<History> GetAllByItalian()
        {
            var italianList = _historyDal.GetAll(x => x.LanguageCode == 1);
            var italianListMapToGet = _mapper.Map<List<HistoryGetDto>>(italianList);
            var italianListMapFromGetToAdd = _mapper.Map<List<HistoryAddDto>>(italianListMapToGet);
            var json = SerializeHelper<HistoryAddDto>.Serialize(italianListMapFromGetToAdd, Language.IT);
            var list = SerializeHelper<HistoryAddDto>.DeSerialize(json, Language.IT);
            var italianListMapReverse = _mapper.Map<List<History>>(list);
            return italianListMapReverse.OrderBy(x => x.Id).ToList();
        }

        //[ValidationAspect(typeof(HistoryValidator))]
        [CacheRemoveAspect("IHistory.Get")]
        [TransactionScopeAspect]
        public void ImportJson(string importLanguage)
        {
            _historyDal.ImportJson(importLanguage);
        }
    }
}