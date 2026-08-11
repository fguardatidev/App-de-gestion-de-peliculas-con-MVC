using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class Movie : IValidatableObject
{
    public int Id { get; set; }
    [StringLength(60,MinimumLength = 3)]
    [Required]
    public string? Title { get; set; }

    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    /*
    [StringLength(30)]
    public string? Genre { get; set; }
    */
    
    [Required]
    public Genre? Genre { get; set; }
    
    public int GenreId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    [Range(1,100)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
    [StringLength(5)]
    [Required]
    public string? Rating { get; set; }

    [StringLength(40)]
    public string? Director { get; set; }

    [DataType(DataType.Text)]
    public int Duration { get; set; } //int no necesita tampoco Required ni ? ya que por defecto asigna 0
    public bool Seen { get; set; } //no se necesita [Required] ya que por defecto se asigna false
    [Range(1,10)]
    [Display(Name = "Personal Rating")]
    [DataType(DataType.Text)]
    public int? PersonalRating { get; set; }


    //metodo que sirve para validar que la puntuacion sea completada, en caso de que la pelicula haya sido vista
    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext )
    {
        if(Seen && (PersonalRating == null))
        {
            yield return new ValidationResult("Al haber visto la película, debe completar su puntuación.",
                new[] { nameof(PersonalRating) }
                );
        }
        else if (!Seen && (PersonalRating != null))
        {
            yield return new ValidationResult("No puede puntuar una película no vista.",
                new[] { nameof(PersonalRating) }
                );
        }
    }
}