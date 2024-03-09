using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using GalaSoft.MvvmLight.Command;
using MovieProject.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MovieProject.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly IService<Movie> _service;

        public MainViewModel()
        {
            _service = new MovieService(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            Movies = new ObservableCollection<Movie>(_service.GetAll());
            Movie = new Movie();
            SelectedMovie = new Movie();
            AddMovieCommand = new RelayCommand(AddMovie);
            DeleteMovieCommand = new RelayCommand(DeleteMovie);
            UpdateMovieCommand = new RelayCommand(UpdateMovie);
            GetByIdCommand = new RelayCommand(GetById);
        }

        public ObservableCollection<Movie> Movies { get; set; }
        public Movie Movie { get; set; }
        public Movie SelectedMovie { get; set; }

        public ICommand AddMovieCommand { get; }
        public ICommand DeleteMovieCommand { get; set; }
        public ICommand UpdateMovieCommand { get; set; }
        public ICommand GetByIdCommand { get; set; }

        private void AddMovie()
        {
            if (Movie == null) return;
            _service.Add(Movie);
            Movies.Add(Movie);
            Movie = new Movie();
        }

        private void DeleteMovie()
        {
            if (SelectedMovie == null) return;
            _service.Remove(SelectedMovie.Id);
            Movies.Remove(Movies.First(m => m.Id == SelectedMovie.Id));
        }

        private void UpdateMovie()
        {
            if (Movie == null || SelectedMovie == null) return;
            _service.Update(SelectedMovie.Id, Movie);
            int index = Array.IndexOf(Movies.ToArray(), Movies.First(m => m.Id == SelectedMovie.Id));
            Movies[index] = Movie;
            Movie = new Movie();
        }

        private void GetById()
        {
            if (Movie == null) return;
            string str = _service.GetById(Movie.Id);
            MessageBox.Show("Id: " + str);
        }
    }
}
