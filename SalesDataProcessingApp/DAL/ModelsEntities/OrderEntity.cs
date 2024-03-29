﻿using System;

namespace DAL.ModelsEntities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public virtual ClientEntity Client { get; set; }
        public int ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }
        public DateTime Date { get; set; }
    }
}