using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MovieService : IService<Movie>
    {
        private readonly IRepository<MovieEntity> _repository;

        public MovieService(string connectionString)
        {
            _repository = new SqlRepository(connectionString);
        }

        public void Add(Movie item)
        {
            _repository.Add(MapToMovieEntity(item));
        }

        public IEnumerable<Movie> GetAll()
        {
            return _repository.GetAll()
                .Select(MapToMovie);
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }

        public void Update(int id, Movie item)
        {
            _repository.Update(id, MapToMovieEntity(item));
        }

        public string GetById(int id)
        {
            return _repository.GetById(id);
        }

        private Movie MapToMovie(MovieEntity movieEntity)
        {
            return new Movie()
            {
                Id = movieEntity.Id,
                Name = movieEntity.Name,
                Genre = movieEntity.Genre,
                Time = movieEntity.Time,
                Year = movieEntity.Year,
            };
        }

        private MovieEntity MapToMovieEntity(Movie movie)
        {
            return new MovieEntity()
            {
                Id = movie.Id,
                Name = movie.Name,
                Genre = movie.Genre,
                Time = movie.Time,
                Year = movie.Year,
            };
        }
    }
}
