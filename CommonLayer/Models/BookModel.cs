﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class BookModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string BookDescription { get; set; }
        public string BookImage { get; set; }
        public float Rating { get; set; }
        public int Totalview { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Quantity { get; set; }

    }
}
