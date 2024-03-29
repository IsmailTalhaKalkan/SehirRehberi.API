﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using SehirRehberi.API.Data;
using SehirRehberi.API.Dtos;
using SehirRehberi.API.Helpers;
using SehirRehberi.API.Models;

namespace SehirRehberi.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Cities/{cityId}/Photos")]
  //[ApiController]
    public class PhotosController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        IOptions<CloudinarySettings> _cloudinaryconfig;
        private Cloudinary _cloudinary;

        public PhotosController(IAppRepository appRepository,IMapper mapper/*,IOptions <CloudinarySettings> cloudinaryconfig*/)
        {
            _appRepository = appRepository;
            _mapper = mapper;
           // _cloudinaryconfig = cloudinaryconfig;

            Account account = new Account("dv8jvkhjz", "635418244493727", "kz-hipH3xHPYXn4LV9Jq8rgMLwM");
            _cloudinary = new Cloudinary(account);
        }

        [HttpPost]
        public ActionResult AddPhotoForCity(int cityId,[FromForm] PhotoForCreationDto photoForCreationDto)
        {
            var city = _appRepository.GetCityById(cityId);
            if (city == null)
            {
                return BadRequest("Could not find the city");
            }

            var currentUserId = int.Parse( User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (currentUserId!=city.UserId)
            {
                return Unauthorized();
            }

            var file = photoForCreationDto.File;
         var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream=file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };
                    
                     uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            photoForCreationDto.Url = uploadResult.Url.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);
            photo.City = city;
            photo.UserId = currentUserId;

            if (!city.Photos.Any(p => p.IsMain))
            {
                photo.IsMain = true;
            }
            city.Photos.Add(photo);
            if (_appRepository.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);
            }
            return BadRequest("Could not add the photo");
        }

        [HttpGet("{id}",Name ="GetPhoto")]
        public ActionResult GetPhoto(int id)
        {
            var photoFromfDb = _appRepository.GetPhoto(id);
            var photo = _mapper.Map<PhotoForReturnDto>(photoFromfDb);
            return Ok(photo);
        }





    }
}
