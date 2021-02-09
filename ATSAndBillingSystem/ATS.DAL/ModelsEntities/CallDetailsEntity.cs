using System;

namespace ATS.DAL.ModelsEntities
{
    public class CallDetailsEntity
    {
        public int Id { get; set; }

        public DateTime StartedTime { get; set; }

        public TimeSpan DurationTime { get; set; }

        public string Source { get; set; }

        public string Target { get; set; }

        public decimal Cost { get; set; }
    }
}