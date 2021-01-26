using CsvHelper;
using DAL.ModelsEntities;
using DAL.ParseMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class ParseService
    {
        public List<OrderEntity> ReadCSVFile(string location)
        {
            try
            {
                using (var reader = new StreamReader(@location, Encoding.Default))
                using (var csv = new CsvReader(reader))
                {
                    var res = csv.Configuration.RegisterClassMap<OrderMap>();

                    var records = csv.GetRecords<OrderEntity>().ToList();

                    return records;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void WriteCSVFile(string path, List<OrderEntity> orders)
        {
            using (StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(true)))
            using (CsvWriter cw = new CsvWriter(sw))
            {
                cw.WriteHeader<OrderEntity>();

                cw.NextRecord();

                foreach (OrderEntity order in orders)
                {
                    cw.WriteRecord(order);

                    cw.NextRecord();
                }
            }
        }
    }
}
