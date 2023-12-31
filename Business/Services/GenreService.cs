using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;
using Business.Results;
using Business.Results.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IGenreService
    {
        IQueryable<GenreModel> Query();
        Result Add(GenreModel model);
        Result Update(GenreModel model);
        Result Delete(int id);

        // extra method definitions can be added to use in the related controller and
        // to implement to the related classes
        List<GenreModel> GetList();
        GenreModel GetItem(int id);
    }

    public class GenreService : IGenreService
    {
        private readonly Db _db;

        public   GenreService(Db db)
        {
            _db = db;
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.Genre.Include(r => r.MovieGenres).Select(r => new GenreModel()
            {
                Id = r.id,
                
                
                
                Name = r.Name,

                //ScoreOutput = r.Score.ToString("N1", new CultureInfo("en-US"))
                // no need to use CultureInfo anywhere in our project anymore,
                // since we made the culture info configuration under the
                // Localization section in MVC project's Program.cs file
                
                // N: number format, 1: one decimal after decimal point

                
                // MM: 2 digits month, dd: 2 digits day, yyyy: 4 digits year,
                // hh: 12 hour with AM and PM, mm: 2 digits minute, ss: 2 digits second

                MovieCountOutput = r.MovieGenres.Count,

                // querying over many to many relationship
                MovieNamesOutput = string.Join("<br />", r.MovieGenres.Select(ur => ur.Movie.Name)), // to show user names in details operation
                MovieIdsInput = r.MovieGenres.Select(ur => ur.MovieId).ToList() // to set selected UserIds in edit operation
            });
        }

        public Result Add(GenreModel model)
        {
            // we want to check whether a resource with the same title exists for the same date without time
            // therefore we use the Date property of DateTime type,
            // for entity's delegate of type Resource (r), we create a new DateTime with default value
            // "01/01/0001 00:00:00.000" if its value is null
            // and check the delegate's Date property value is equal to the model's Date property value
            if (
                _db.Genre.Any(r =>
                r.Name.ToUpper() == model.Name.ToUpper().Trim()))
                return new ErrorResult("Genre with same name cant exist!");

            var entity = new Genre()
            {
                // ? can be used if the value of a property can be null,
                // if model.Content is null, Content is set to null, else Content is set to
                // model.Content's trimmed value
                Name = model.Name,

                 // since Title is required in the model, therefore can't be null,
                                            // we don't need to use ?

                // inserting many to many relational entity,
                // ? must be used with UserIdsInput if there is a possibility that it can be null
                MovieGenres = model.MovieIdsInput?.Select(MovieId => new MovieGenre()
                {
                    MovieId = MovieId
                }).ToList()
            };

            _db.Genre.Add(entity);
            _db.SaveChanges();

            return new SuccessResult("Resource added successfully.");
        }

        public Result Update(GenreModel model)
        {
            if (
                 _db.Genre.Any(r =>
                 r.Name.ToUpper() == model.Name.ToUpper().Trim() && r.id != model.Id))
                return new ErrorResult("Genre with same name cant exist!");

            // deleting many to many relational entity
            var existingEntity = _db.Genre.Include(r => r.MovieGenres).SingleOrDefault(r => r.id == model.Id);
            if (existingEntity is not null && existingEntity.MovieGenres is not null)
                _db.MovieGenre.RemoveRange(existingEntity.MovieGenres);

            // existingEntity queried from the database must be updated since we got the existingEntity
            // first as above, therefore changes of the existing entity are being tracked by Entity Framework,
            // if disabling of change tracking is required, AsNoTracking method must be used after the DbSet,
            // for example _db.Resources.AsNoTracking()
            existingEntity.Name = model.Name;
            

            // inserting many to many relational entity
            existingEntity.MovieGenres = model.MovieIdsInput?.Select(MovieId => new MovieGenre()
            {
                MovieId= MovieId
            }).ToList();

            _db.Genre.Update(existingEntity);
            _db.SaveChanges(); // changes in all DbSets are commited to the database by Unit of Work

            return new SuccessResult("Resource updated successfully.");
        }

        public Result Delete(int id)
        {
            var entity = _db.Genre.Include(r => r.MovieGenres).SingleOrDefault(r => r.id == id);
            if (entity is null)
                return new ErrorResult("Resource not found!");

            // deleting many to many relational entity:
            // deleting relational UserResource entities of the resource entity first
            _db.MovieGenre.RemoveRange(entity.MovieGenres);

            // then deleting the Resource entity
            _db.Genre.Remove(entity);

            _db.SaveChanges();

            return new SuccessResult("Resource deleted successfully.");
        }

        public List<GenreModel> GetList()
        {
            // since we wrote the Query method above, we should call it
            // and return the result as a list by calling ToList method
            return Query().ToList();
        }

        // Way 1:
        //public ResourceModel GetItem(int id)
        //{
        //    return Query().SingleOrDefault(r => r.Id == id);
        //}
        // Way 2:
        public GenreModel GetItem(int id) => Query().SingleOrDefault(r => r.Id == id);
    }
}
