﻿using ATS.DAL.Constants;

namespace ATS.DAL.ModelsEntities
{
    public class RequestEntity
    {
        public int Id { get; set; }

        public string SourcePhoneNumber { get; set; }

        public RequestRespondState State { get; set; }
    }
}