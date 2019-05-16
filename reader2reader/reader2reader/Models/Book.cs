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

        [Display(Name = "Precio")]
        public decimal Price { get; set; }
    }
}