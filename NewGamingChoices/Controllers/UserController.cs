using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewGamingChoices.Data;
using NewGamingChoices.Models;
using NewGamingChoices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NewGamingChoices.Models.ApplicationUser;

namespace NewGamingChoices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private ApplicationDbContext _db;
        private CustomUserService _userService;

        public UserController(ILogger<UserController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
            _userService = new CustomUserService(_db);
        }

        private ApplicationUser getCurrentUser()
        {
            return _userService.GetCurrentUser(User);
        }

        [HttpGet("[action]")]
        public ApplicationUser getUser(string username)
        {
            return _userService.GetUser(username);
        }

        [HttpPost("[action]")]
        public void updateUser(ApplicationUser user)
        {
            _userService.UpdateUser(user);
        }

        [Produces("application/json")]
        [HttpGet("searchfriend")]
        public IActionResult SearchFriend(string term)
        {
            try
            {
                var cu = getCurrentUser();
                var names = _db.Users.Where(p => p.UserName.Contains(term)).ToList().Where(p => !p.FriendsList.Any(f => f.Id == cu.Id) && !p.AskedFriendsList.Any(f => f.Id == cu.Id)).Select(p => p.UserName);
                return Ok(names);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("sendfr")]
        public IActionResult SendFriendRequest([FromBody] FriendData fd)
        {
            try
            {
                var asker = getCurrentUser();
                var askedfriend = _db.Users.Where(p => p.Id == fd.id).FirstOrDefault();
                if(askedfriend != null)
                {
                    if(!askedfriend.AskedFriendsList.Any(fr => fr.Id == asker.Id)) // Only one request per person
                    {
                        if (!askedfriend.FriendsList.Any(f => f.Id == asker.Id)) // Already friends
                        {
                            List<Friend> askedfriendlist = askedfriend.AskedFriendsList;
                            askedfriendlist.Add(new Friend(asker.Id, asker.UserName));
                            askedfriend.UpdateAskedFriendsList(askedfriendlist);
                        }
                        else
                        {
                            return BadRequest("Vous êtes déjà amis !");
                        }
                    }
                    else
                    {
                        return BadRequest("Une demande d'ami a déjà été envoyée à cet utilisateur, c'est maintenant à lui de la valider ou de la refuser.");
                    }
                }

                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("addfriend")]
        public IActionResult AddFriend([FromBody] FriendData fd)
        {
            try
            {
                var user = getCurrentUser();
                var friend = _db.Users.Where(p => p.Id == fd.id).FirstOrDefault();
                if (friend != null)
                {
                    if (user.AskedFriendsList.Any(fr => fr.Id == fd.id)) // Security check, can't add a friend without validating a request
                    {
                        List<Friend> userfl = user.FriendsList;
                        List<Friend> friendfl = friend.FriendsList;

                        // New friendship <3
                        userfl.Add(new Friend(friend.Id, friend.UserName));
                        friendfl.Add(new Friend(user.Id, user.UserName));

                        user.UpdateFriendsList(userfl);
                        friend.UpdateFriendsList(friendfl);
                    }
                    else
                    {
                        return BadRequest("Il n'est pas possible d'ajouter un ami sans qu'il ait au préalable envoyé une demande.");
                    }
                }

                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("deleteFR")]
        public IActionResult DeleteFriendRequest([FromBody] FriendData fd)
        {
            try
            {
                var user = getCurrentUser();
                var friend = _db.Users.Where(p => p.Id == fd.id).FirstOrDefault();
                if (friend != null)
                {
                    List<Friend> askedfl = user.AskedFriendsList;

                    var friendtoremove = askedfl.First(f => f.Id == friend.Id);
                    askedfl.Remove(friendtoremove);

                    user.UpdateAskedFriendsList(askedfl);
                }

                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("deleteFriend")]
        public IActionResult DeleteFriend([FromBody] FriendData fd)
        {
            try
            {
                var user = getCurrentUser();
                var friend = _db.Users.Where(p => p.Id == fd.id).FirstOrDefault();
                if (friend != null)
                {
                    List<Friend> userfl = user.FriendsList;
                    List<Friend> friendfl = friend.FriendsList;

                    var friendtoremove = userfl.First(f => f.Id == friend.Id);
                    var usertoremove = friendfl.First(f => f.Id == user.Id);

                    // Friendship ended </3
                    userfl.Remove(friendtoremove);
                    friendfl.Remove(usertoremove);

                    user.UpdateFriendsList(userfl);
                    friend.UpdateFriendsList(friendfl);
                }

                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        public class FriendData
        {
            public string id;
        }
    }
}
