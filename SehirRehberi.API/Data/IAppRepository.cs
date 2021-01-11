using SehirRehberi.API.Dtos;
using SehirRehberi.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Data
{
  public  interface IAppRepository
    {
        void Add<T>(T entity) where T:class;
        void Delete<T>(T entity) where T : class;
        bool SaveAll();
        List<City> GetCities();
        List<Photo> GetPhotosByCity(int id);
        City GetCityById(int cityId);
        Photo GetPhoto(int id);
        List<Photo> GetPhotosByUserId(int userId);
        List<Comments> GetCommentsByPhoto(int photoId);
        Rating GetCityRating(int cityId);
        void UpdateRating(int cityId,RatingDto ratingDto);
        void CommentUpvote(int commentId,int upDown);
    }
}
