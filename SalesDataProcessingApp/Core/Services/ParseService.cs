using Core.Interfaces;
using CsvHelper;
using DAL.ModelsEntities;
using DAL.ParseMaps;
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
        public List<OrderEntity> ReadCSVFile(string location, Serilog.Core.Logger logger)
        {
            try
            {
                List<OrderEntity> records;

                using (var reader = new StreamReader(@location, Encoding.Default))
                using (var csv = new CsvReader(reader))
                {

                    csv.Configuration.CultureInfo = new CultureInfo("en-US");

                    csv.Configuration.RegisterClassMap<OrderMap>();

                    records = csv.GetRecords<OrderEntity>().ToList();
                }

                return records;
            }
            catch (Exception e)
            {
                logger.Error($"Erro reading file: {e}");
                throw;
            }
        }
    }
}
