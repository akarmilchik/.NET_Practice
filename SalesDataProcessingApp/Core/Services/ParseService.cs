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
        private object _lockObject = new object();

        private readonly Serilog.Core.Logger _logger; 

        public List<OrderEntity> ReadCSVFile(string location, Serilog.Core.Logger logger)
        {
            List<OrderEntity> records;

            try
            {
                lock (_lockObject)
                {
                    logger.Information($"Lock file {location}.");

                    using (var reader = new StreamReader(@location, Encoding.Default))
                    using (var csv = new CsvReader(reader))
                    {

                        csv.Configuration.CultureInfo = new CultureInfo("en-US");

                        csv.Configuration.RegisterClassMap<OrderMap>();
                        
                        records = csv.GetRecords<OrderEntity>().ToList();
                    }
                }

                logger.Information($"Unlock file {location}.");

                return records;
            }
            catch (Exception e)
            {
                logger.Error($"File open error: {e}.");

                throw;
            }
        }
    }
}
