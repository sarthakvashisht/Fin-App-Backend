﻿using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Stocks")]
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; }=string.Empty;
        public string CompanyName { get; set; }=string.Empty;
        public int Purchase { get; set;}
        public int LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public List<Comment>Comments { get; set; }=new List<Comment>();

        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
