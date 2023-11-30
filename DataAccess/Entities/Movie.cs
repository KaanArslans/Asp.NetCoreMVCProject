#nullable disable

// Way 1:
// namespace DataAccess.Entities;
// Way 2:
using DataAccess.Base;

namespace DataAccess.Entities // namespace DataAccess.Entities; can also be written
                              // therefore we don't need to use curly braces
{
    public class Movie:Record
    {
        // data member and member method usage example from Java:
        // private int id; // a class variable is called as a field in C#

        // public void setId(int id) // a class method is called as a behavior in C#
        // {
        //     this.id = id;
        // }

        // public int getId()
        // {
        //     return id;
        // }



        // this is called a property in C# which contains getters and setters 
        
        public short? Year { get; set; }
		public string Name { get; set; } // "String" class type can also be used, general usage "string" data type

        // class has a relationship for one to many tables relationship (Users table is the many side)
        // Way 1:
        //public IEnumerable<User> Users { get; set; }
        // Way 2:
        //public ICollection<User> Users { get; set; }
        // Way 3: since List class implements IEnumerable and ICollection interfaces, we can use List class as the type

        public int? DirectorId { get; set; }

        public double Revenue { get; set; }
        public Director Director { get; set; }

		public List<MovieGenre> MovieGenres { get; set; }


	}
}