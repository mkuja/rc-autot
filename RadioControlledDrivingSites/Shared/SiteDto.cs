namespace RadioControlledDrivingSites.Shared;

using System.ComponentModel.DataAnnotations;

public class SiteDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nimi on pakollinen.")]
    [StringLength(40, ErrorMessage = "Nimi voi olla enintään 40 merkkiä.")]
    public string Name { get; set; } = "";

    [StringLength(50, ErrorMessage = "Alusta voi olla enintään 40 merkkiä.")]
    public string Environment { get; set; } = "";

    [StringLength(600, ErrorMessage = "Kuvaus on liian pitkä (max. 600 merkkiä).")]
    public string Description { get; set; } = "";

    [Required(ErrorMessage = "Paikka on pakollinen.")]
    [StringLength(300, ErrorMessage = "Liian pitkä paikkatieto.")]
    public string Location { get; set; } = "";

    [Required(ErrorMessage = "Paikan pituus- ja leveysasteet ovat pakollisia.")]
    [StringLength(60, ErrorMessage = "Liian pitkä merkkijono tarkalle sijaintitiedolle.")]
    [RegularExpression(@"^\d+\.\d+, ?\d+\.\d+$", ErrorMessage = "Anna pituus- ja leveysasteet kopioituna Bing- tai Google-kartasta.")]
    public string LongitudeLattitude { get; set; } = String.Empty;
}