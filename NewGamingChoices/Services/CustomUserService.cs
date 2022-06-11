using Microsoft.EntityFrameworkCore;
using NewGamingChoices.Data;
using NewGamingChoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewGamingChoices.Services
{
    public class CustomUserService
    {
        private ApplicationDbContext _db { get; set; }
        public CustomUserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ApplicationUser GetUser(string userEmail)
        {
            var user = _db.Users.Include(u => u.GamingMoods).FirstOrDefault(user => user.Email == userEmail);
            return user;
        }

        public ApplicationUser GetUserById(string userId)
        {
            var user = _db.Users.Include(u => u.GamingMoods).ThenInclude(gm => gm.Game).FirstOrDefault(user => user.Id == userId);
            return user;
        }

        public void UpdateUser(ApplicationUser user)
        {
            //var consolesdb = _db.Consoles.Include(c => c.Users);
            //foreach (var cdb in consolesdb)
            //{
            //    if (cdb.Users.Any(u => u.Id == user.Id))
            //    {
            //        _db.Consoles.Remove(cdb);
            //    }
            //}

            //_db.SaveChanges();


            //var userconsoles = _db.Consoles.Include(c => c.Users).Where(c => c.Users.Any(u => u.Id == user.Id)).ToList();
            //var newconsoles = user.Consoles.Where(c => !userconsoles.Any(con => con.ID == c.ID)).ToList();

            //user.Consoles.Clear();
            //user.Consoles = newconsoles;


            //foreach (GameConsole console in userconsoles)
            //{
            //    if (!user.Consoles.Any(c => c.ID == console.ID))
            //    {
            //        var usertodelete = console.Users.FirstOrDefault(u => u.Id == user.Id);
            //        console.Users.Remove(usertodelete);
            //    }
            //}
            //foreach (var userconsole in user.Consoles)
            //{
            //    if (!userconsoles.Any(c => c.ID == userconsole.ID))
            //    {
            //        var consoletoupdate = _db.Consoles.Include(c => c.Users).FirstOrDefault(c => c.ID == userconsole.ID);
            //        if (!consoletoupdate.Users.Any(u => u.Id == user.Id))
            //        {
            //            consoletoupdate.Users.Add(user);
            //        }
            //    }
            //}

            //List<GameConsole> newconsolelist = new List<GameConsole>();
            //foreach(var con in user.Consoles)
            //{
            //    newconsolelist.Add(con);
            //}

            //user.Consoles.Clear();

            _db.Users.Update(user);
            _db.SaveChanges();
        }
    }
}
