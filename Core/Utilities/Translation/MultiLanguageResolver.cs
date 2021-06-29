using Core.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Translation
{
    public class MultiLanguageResolver : DefaultContractResolver
    {
        public Language _language { get; set; }

        public MultiLanguageResolver(Language language)
        {
            _language = language;
        }

        protected override JsonProperty CreateProperty(MemberInfo memberInfo, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(memberInfo, memberSerialization);

            // See if there is a [TranslatedFieldName] attribute applied to the property for the requested language
            var attribute = property.AttributeProvider.GetAttributes(true)
                                            .OfType<TranslatedFieldNameAttribute>()
                                            .FirstOrDefault(a => a.Language == _language);

            // if so, change the property name to the one from the attribute
            if (attribute != null)
            {
                property.PropertyName = attribute.Name;
            }

            return property;
        }
    }
}
