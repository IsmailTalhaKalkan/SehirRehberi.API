using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SehirRehberi.API.Dtos;
using SehirRehberi.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Data
{
    

    public class AppRepository : IAppRepository
    {
        private DataContext _context;
        public AppRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public List<City> GetCities()
        {
            var cities = _context.Cities.Include(c => c.Photos).ToList();
            return cities;
        }

        public City GetCityById(int cityId)
        {
            var city = _context.Cities.Include(c => c.Photos).FirstOrDefault(c => c.Id == cityId);
            return city;
        }

        public Photo GetPhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
            return photo;

        }

        public List<Photo> GetPhotosByCity(int cityId)
        {
            var photos = _context.Photos.Where(p => p.CityId == cityId).ToList();
            return photos;
        }

        public List<Photo> GetPhotosByUserId(int userId)
        {
            var photos = _context.Photos.Where(p => p.UserId == userId).ToList();
            return photos;
        }

        public List<Comments> GetCommentsByPhoto(int photoId)
        {
            var comments = _context.Comments.Where(p => p.PhotoId == photoId).ToList();
            return comments;
        }

        public Rating GetCityRating(int cityId)
        {
            var rating = _context.Rating.Where(p => p.CityId == cityId).FirstOrDefault();
       
            return rating;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateRating(int cityId,RatingDto ratingDto)
        {
            var tempRating = new Rating();
            var ratingToUpdate = _context.Rating.Where(p => p.CityId == cityId).FirstOrDefault();            
            if (ratingToUpdate!=null)
            {
                // Store Current Data In Temp
                tempRating.Food = ratingToUpdate.Food*ratingToUpdate.Counter;
                tempRating.Pricing = ratingToUpdate.Pricing * ratingToUpdate.Counter;
                tempRating.Transportation = ratingToUpdate.Transportation * ratingToUpdate.Counter;
                tempRating.View = ratingToUpdate.View * ratingToUpdate.Counter;
                tempRating.Counter = ratingToUpdate.Counter + 1;
                // Update data on temp
                tempRating.Food += ratingDto.Food;
                 tempRating.Pricing += ratingDto.Pricing;
                tempRating.Transportation += ratingDto.Transportation;
                tempRating.View += ratingDto.View;
                // Calculate and Declare new rating
                ratingToUpdate.Food = tempRating.Food / tempRating.Counter;
                ratingToUpdate.Pricing = tempRating.Pricing / tempRating.Counter;
                ratingToUpdate.Transportation = tempRating.Transportation / tempRating.Counter;
                ratingToUpdate.View = tempRating.View / tempRating.Counter;
                ratingToUpdate.Counter = tempRating.Counter;
            }
        }

        public void CommentUpvote(int commentId,int upDown)
        {
            var commentToUpvote = _context.Comments.Where(p => p.Id == commentId).FirstOrDefault();
            if (commentToUpvote != null)
            {
                if (upDown==1)
                {
                    commentToUpvote.UpVote++;
                }
                else
                {
                    commentToUpvote.UpVote--;
                }
            }
        }
    }
}
