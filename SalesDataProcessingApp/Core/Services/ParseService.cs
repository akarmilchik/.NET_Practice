using Core.Interfaces;
using CsvHelper;
using DAL.ModelsEntities;
using DAL.ParseMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class ParseService : IParseService
    {
        private object lockObject = new object();

        public List<OrderEntity> ReadCSVFile(string location)
        {
            List<OrderEntity> records;

            try
            {
                lock (lockObject)
                {
                    Console.WriteLine($"Lock file {location}");
                    using (var reader = new StreamReader(@location, Encoding.Default))
                    using (var csv = new CsvReader(reader, new CultureInfo("en-US")))
                    {
                        //csv.Configuration.CultureInfo = new CultureInfo("en-US");

                        //csv.Configuration.RegisterClassMap<OrderMap>();

                        records = csv.GetRecords<OrderEntity>().ToList();
                    }
                }

                Console.WriteLine($"Unlock file {location}");

                return records;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
