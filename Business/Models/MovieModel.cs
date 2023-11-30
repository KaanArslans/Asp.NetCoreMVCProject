#nullable disable

using System.ComponentModel;
using DataAccess.Entities;


namespace Business.Models
{
	public class MovieModel
	{
		
		public int Id { get; set; } 

		public string Name { get; set; }
		
		public short? Year { get; set; }
		[DisplayName("Director")]
		public int? DirectorId { get; set; }

		[DisplayName("Director")]
		public string directorOutput { get; set; }


		public double Revenue { get; set; }




	}
}
