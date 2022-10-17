using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Schema
{
    public class Rates
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public DateTime Id { get; set; }

        [Required]
        [Column(TypeName = "Float")]
        public decimal RUB { get; set; }

        [Required]
        [Column(TypeName = "Float")]
        public decimal EUR { get; set; }

        [Required]
        [Column(TypeName = "Float")]
        public decimal GBP { get; set; }

        [Required]
        [Column(TypeName = "Float")]
        public decimal JPY { get; set; }

        [Required]
        [Column(TypeName = "Text")]
        public string Response { get; set; }

    }
}
