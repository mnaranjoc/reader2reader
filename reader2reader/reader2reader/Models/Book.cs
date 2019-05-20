using System;
using System.ComponentModel.DataAnnotations;

namespace reader2reader.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Título")]
        public string Title { get; set; }

        [Display(Name = "Autor")]
        public string Author { get; set; }

        [Display(Name = "Genero")]
        public string Genre { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Precio")]
        public decimal Price { get; set; }

        [Display(Name = "Fecha creación")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Image URL")]
        public string ImageURL { get; set; }

        [Display(Name = "Usuario creación")]
        public string CreatedBy { get; set; }
    }
}