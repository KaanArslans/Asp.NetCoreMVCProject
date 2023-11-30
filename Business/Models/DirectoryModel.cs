#nullable disable 
// For preventing the usage of ? (nullable) for reference types
// such as strings, arrays, classes, interfaces, etc.
// Should only be used with entity and model classes.

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DataAccess.Entities;


namespace Business.Models;

public class DirectoryModel
{
   
    public int Id { get; set; }

    [DisplayName("Director")] 

	
	[Required(ErrorMessage = "{0} is required!")] 
   
    [StringLength(10, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
    // Way 5:
    [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
    [MaxLength(10, ErrorMessage = "{0} must be maximum {1} characters!")]
    public string UserName { get; set; }



    [DisplayName("Movies")]
    public List<Movie> Movies { get; set; }


    [DisplayName("Surname")]
    public string Surname { get; set; }
	public DateTime? BirthDate { get; set; }
    [DisplayName("Movie number that belongs to director")]
    public int MovieCountOutput { get; set; }

    [DisplayName("Retired or not?")]
    public string IsRetiredOutput { get; set; }
    public bool IsRetired { get; set; }

    

}
