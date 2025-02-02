﻿using Emreraslan.Core.Entities.BaseEntity;

namespace Emreraslan.Core.Entities
{
    public class Category : IEntity
    {
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
