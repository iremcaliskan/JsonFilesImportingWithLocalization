using Core.Entities;
using System;

namespace Core.Utilities.Translation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    // Attribute to add different "translations"
    public class TranslatedFieldNameAttribute : Attribute
    {
        public string Name { get; }
        public Language Language { get; }

        public TranslatedFieldNameAttribute(string translatedName, Language lang)
        {
            Name = translatedName;
            Language = lang;
        }
    }
}