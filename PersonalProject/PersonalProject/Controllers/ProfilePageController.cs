﻿using Microsoft.AspNet.Identity;
using PersonalProject.AdditionalClasses;
using PersonalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersonalProject.Controllers
{
    [RoutePrefix("Personal_Project/Main_Page_Personal_Project")]
    public class ProfilePageController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
      

        [Route("Profile/getProfilePageInfo")]
        [HttpGet]
        [Authorize]
        public Object getProfilePageInfo()
        {
            var LoggedUser = User.GetLoggedInUserInDB();

            return new {
                avatarPicture = LoggedUser.AvatarPicture,
                username = LoggedUser.UserName,
                level = LoggedUser.UserLevel,
                experiance = LoggedUser.Experience,
                reputation = LoggedUser.Reputation,
                carGameBestScore = LoggedUser.CarGame.BestScore,
                pacmanGameBestScore = LoggedUser.PackManGame.BestScore,


            };
        }

        [Route("Profile/Avatar")]
        [HttpPost]
        [Authorize]
        public void Avatar([FromBody] string _newAvatarPicture)
        {
            //var x = _newAvatarPicture;

            string userId = User.Identity.GetUserId();

            db.Users.Find(userId).AvatarPicture = _newAvatarPicture;

            db.SaveChanges();
            
        }

        [Route("Profile/DeleteUser")]
        [HttpDelete]
        [Authorize]
        public void DeleteUser()
        {
            ApplicationUser LoggedUser = User.GetLoggedInUserInDB();
            db.Users.Remove(LoggedUser);
            db.SaveChanges();
        }

    }
}
