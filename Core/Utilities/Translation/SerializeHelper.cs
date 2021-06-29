using Core.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Utilities.Translation
{
    public class SerializeHelper<TEntity> where TEntity: class, IDto, new()
    {
		public static string Serialize(List<TEntity> entities, Language language)
		{
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new MultiLanguageResolver(language),
				Formatting = Formatting.Indented
			};
			
			var json = JsonConvert.SerializeObject(entities, settings);
			return json;
		}

		public static List<TEntity> DeSerialize(string json, Language language)
		{
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new MultiLanguageResolver(language),
				Formatting = Formatting.Indented
			};
			
			List<TEntity> list = JsonConvert.DeserializeObject<List<TEntity>>(json, settings);
			return list;
		}
	}
}