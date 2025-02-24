﻿using BackEnd.Model;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Dtos.Comment;
using System.ComponentModel.DataAnnotations;
namespace BackEnd.Dtos.Stock
{
    public class StockDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "Company cannot be over 10 characters")]
        public string CompanyName { get; set; } = string.Empty;


        [Required]
        [Range(1, 100000000)]
        public decimal Purchase { get; set; }


        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Industry cannot be over 10 characters")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 5000000000000)]
        public long MarketCap { get; set; }
        
        public List<CommentDto> Comments { get; set; }
    }
}
