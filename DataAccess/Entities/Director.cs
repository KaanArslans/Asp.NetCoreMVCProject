﻿#nullable disable

// since Statuses enum is under different folder therefore different namespace,
// we need to include the namespace with using (similar to import in Java)

using DataAccess.Base;

namespace DataAccess.Entities;

public class Director:Record
{
    
    
    public string UserName { get; set; }
    public string Surname { get; set; }
	public DateTime? BirthDate { get; set; }

	public bool IsRetired { get; set; } // boolean data type which can have the value of true or false

    // Way 1: we can use the Statuses enum type through its namespace
    // public DataAccess.Enums.Statuses Status { get; set; }
    // Way 2: we can use the Statuses enum type directly after adding the using line by its namespace on top of the file
    

    // class has-a relationship (Roles table is the one side)
    
    
    // tables one to many relationship 
    

    // class has a relationship for many to many tables relationship (UserResources relational table is the many side)
    public List<Movie> Movies { get; set; }
}
