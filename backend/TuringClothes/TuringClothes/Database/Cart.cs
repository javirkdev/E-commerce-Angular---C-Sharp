﻿namespace TuringClothes.Database
{
    public class Cart
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public ICollection<CartDetail> Details { get; set; }
    }
}
