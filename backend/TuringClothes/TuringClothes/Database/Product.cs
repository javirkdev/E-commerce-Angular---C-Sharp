﻿namespace TuringClothes.Database
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public long Stock { get; set; }

        public string Image { get; set; }

        public ICollection<Review>? Reviews { get; set; }
    }
}
