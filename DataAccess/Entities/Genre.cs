#nullable disable 
// For preventing the usage of ? (nullable) for reference types
// such as strings, arrays, classes, interfaces, etc.
// Should only be used with entity and model classes.

using DataAccess.Base;

namespace DataAccess.Entities; // namespace is used for grouping the classes according the their similar purposes,
                               // similar to package usage in Java

public class Genre:Record

{
    // integer number data types: int, long, short, byte

    


     // value assignment required

    public string Name { get; set; } // value assignment is not required (can be assigned as null)

  

  
  

    // class has a relationship for many to many tables relationship (UserResources table is the many side)
    public List<MovieGenre>MovieGenres { get; set; } // value assignment is not required
}
