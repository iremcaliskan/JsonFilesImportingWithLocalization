using Business.Concrete;
using Core.Utilities.Translation;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ConsoleAppUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Some of tests are made in here");
            //            // For EN Import
            //            //string fileEnglish = @"C:\Users\XTANBUL-7\source\repos\JsonFilesImportingWithLocalization\ConsoleAppUI\json-demo-files\demo-en.json";
            //            //StreamReader reader = new StreamReader(file);
            //            //string json = reader.ReadToEnd();
            //            //List<History> histories = JsonConvert.DeserializeObject<List<History>>(json);

            //            //using (var context = new TestCaseContext())
            //            //{
            //            //    context.Set<History>().AddRange(histories);
            //            //    context.SaveChanges();
            //            //}

            //            // For EN Import - Way 2
            //            string fileEnglish = @"C:\Users\XTANBUL-7\source\repos\JsonFilesImportingWithLocalization\ConsoleAppUI\json-demo-files\demo-en.json";
            //            string outputDemoEnglish = @"C:\Users\XTANBUL-7\source\repos\JsonFilesImportingWithLocalization\ConsoleAppUI\json-demo-files\outputEN.json";

            //            StreamReader readerEn1 = new StreamReader(fileEnglish);
            //            string jsonFileEnglish = readerEn1.ReadToEnd();

            //            List<History> historiesEnglish = SerializeHelper<History>.DeSerialize(jsonFileEnglish, Core.Entities.Language.EN);
            //            var englishRevertedToEnglishJson = SerializeHelper<History>.Serialize(historiesEnglish, Core.Entities.Language.EN);

            //            File.WriteAllText(outputDemoEnglish, englishRevertedToEnglishJson);

            //            StreamReader readerEn2 = new StreamReader(outputDemoEnglish);
            //            string jsonFileEnglishToEnglish = readerEn2.ReadToEnd();

            //            List<History> historiesEnglishToEnglish = JsonConvert.DeserializeObject<List<History>>(jsonFileEnglishToEnglish);
            //            using (var context = new TestCaseContext())
            //            {
            //                context.Set<History>().AddRange(historiesEnglishToEnglish);
            //                context.SaveChanges();
            //            }

            //            // For TR Import
            //            string fileTurkish = @"C:\Users\XTANBUL-7\source\repos\JsonFilesImportingWithLocalization\ConsoleAppUI\json-demo-files\demo-tr.json";
            //            string outputDemoTurkish = @"C:\Users\XTANBUL-7\source\repos\JsonFilesImportingWithLocalization\ConsoleAppUI\json-demo-files\outputTR.json";
            //            StreamReader readerTr1 = new StreamReader(fileTurkish);
            //            string jsonFileTurkish = readerTr1.ReadToEnd();

            //            List<History> historiesTurkish = SerializeHelper<History>.DeSerialize(jsonFileTurkish, Core.Entities.Language.TR);
            //            var turkishRevertedToEnglishJson = SerializeHelper<History>.Serialize(historiesTurkish, Core.Entities.Language.EN);
            //            File.WriteAllText(outputDemoTurkish, turkishRevertedToEnglishJson);

            //            StreamReader readerTr2 = new StreamReader(outputDemoTurkish);
            //            string jsonFileTurkishToEnglish = readerTr2.ReadToEnd();
            //            List<History> historiesTurkishToEnglish = JsonConvert.DeserializeObject<List<History>>(jsonFileTurkishToEnglish);
            //            using (var context = new TestCaseContext())
            //            {
            //                context.Set<History>().AddRange(historiesTurkishToEnglish);
            //                context.SaveChanges();
            //            }

            //            // For IT Import
            //            string fileItalian = @"C:\Users\XTANBUL-7\source\repos\JsonFilesImportingWithLocalization\ConsoleAppUI\json-demo-files\demo-it.json";
            //            string outputDemoItalian = @"C:\Users\XTANBUL-7\source\repos\JsonFilesImportingWithLocalization\ConsoleAppUI\json-demo-files\outputIT.json";
            //            StreamReader readerIt1 = new StreamReader(fileItalian);
            //            string jsonFileItalian = readerIt1.ReadToEnd();

            //            List<History> historiesItalian = SerializeHelper<History>.DeSerialize(jsonFileItalian, Core.Entities.Language.IT);
            //            var italianRevertedToEnglishJson = SerializeHelper<History>.Serialize(historiesItalian, Core.Entities.Language.EN);
            //            File.WriteAllText(outputDemoItalian, italianRevertedToEnglishJson);

            //            StreamReader readerIt2 = new StreamReader(outputDemoItalian);
            //            string jsonFileItalianToEnglish = readerIt2.ReadToEnd();
            //            List<History> historiesItalianToEnglish = JsonConvert.DeserializeObject<List<History>>(jsonFileItalianToEnglish);
            //            using (var context = new TestCaseContext())
            //            {
            //                context.Set<History>().AddRange(historiesItalianToEnglish);
            //                context.SaveChanges();
            //            }

            //            //HistoryManager manager = new HistoryManager(new EfHistoryDal());

            //            //var json1 = SerializeHelper<History>.Serialize(manager.GetAll(), Core.Entities.Language.EN);
            //            //Console.WriteLine(json1);
            //            //var list1 = SerializeHelper<History>.DeSerialize(json1, Core.Entities.Language.EN);
            //            //foreach (var item1 in list1)
            //            //{
            //            //    Console.WriteLine(item1.Id + " " + item1.Dc_Date + " " + item1.Dc_Category + " " + item1.Dc_Event);
            //            //}

            //            //var json2 = SerializeHelper<History>.Serialize(manager.GetAll(), Core.Entities.Language.TR);
            //            //Console.WriteLine(json2);
            //            //var list2 = SerializeHelper<History>.DeSerialize(json2, Core.Entities.Language.TR);
            //            //foreach (var item2 in list2)
            //            //{
            //            //    Console.WriteLine(item2.Id + " " + item2.Dc_Date + " " + item2.Dc_Category + " " + item2.Dc_Event);
            //            //}

            //            //var json3 = SerializeHelper<History>.Serialize(manager.GetAll(), Core.Entities.Language.IT);
            //            //Console.WriteLine(json3);
            //            //var list3 = SerializeHelper<History>.DeSerialize(json3, Core.Entities.Language.IT);
            //            //foreach (var item3 in list3)
            //            //{
            //            //    Console.WriteLine(item3.Id + " " + item3.Dc_Date + " " + item3.Dc_Category + " " + item3.Dc_Event);
            //            //}
            //        }
        }
    }
}
