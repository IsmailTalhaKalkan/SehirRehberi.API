using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SehirRehberi.API.Data;
using SehirRehberi.API.Dtos;
using SehirRehberi.API.Models;

namespace SehirRehberi.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[ApiController]
    public class RatingsController : ControllerBase
    {

        private IAppRepository _appRepository;
        private IMapper _mapper;

        public RatingsController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        [HttpPost("CreateRating")]

        public ActionResult CreateRatingForCiyty(int cityId)
        {
            var newRating = new Rating
            {
                CityId = cityId,
                Transportation = 0,
                Food=0,
                View=0,
                Pricing=0,
                Counter=0
            };
            _appRepository.Add(newRating);
            _appRepository.SaveAll();
            return Ok(newRating);
        }

        [HttpPost("NewRating")]

        public ActionResult NewRating(int cityId,[FromBody] RatingDto ratingDto) {

            _appRepository.UpdateRating(cityId, ratingDto);
            _appRepository.SaveAll();
            return Ok();
        
        }


        [HttpGet("Rating")]
        public ActionResult GetRatingByCityId(int cityId)
        {
            var rating = _appRepository.GetCityRating(cityId);
            var ratingToReturn = _mapper.Map<RatingDto>(rating);
            return Ok(ratingToReturn);

        }

    }
}
