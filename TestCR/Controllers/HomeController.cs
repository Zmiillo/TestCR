using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestCR.Models;

namespace TestCR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        UsersContext db;

        public HomeController(ILogger<HomeController> logger, UsersContext context)
        {
            _logger = logger;
            this.db = context;
            if (db.Users.Count() == 0)
            {
                User user1 = new User { Login= "Zver", Name= "Игорь", Surname= "Павлов", Email = "PavlovIE@mail.ru" };
                User user2 = new User { Login = "Devastator3000", Name ="Варечка", Surname = "Суслова", Email = "Konffetka@mail.ru" };
                User user3 = new User { Login = "PetrenkoAI", Name = "Александр Иванович", Surname = "Сидоров", Email = "PetrenkoAI@rambler.ru" };
                User user4 = new User { Login = "Zmv", Name = "Мария", Surname = "Завьялова", Email = "zmv@mail.ru" };

                db.Users.AddRange(user1, user2, user3, user4);
                db.SaveChanges();
            }
        }

        public async Task<IActionResult> Index(int? id, int? deleteid, string name, SortState sortOrder = SortState.LoginAsc)
        {
            IQueryable<User> users = db.Users;
            
            if (deleteid != null && deleteid != 0)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == deleteid);
                if (user != null)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                    users = db.Users;
                }
            }

            if (id != null && id != 0)
            {
                users = users.Where(p => p.Id == id);
            }

            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name) || p.Login.Contains(name)  || p.Surname.Contains(name)  || p.Email.Contains(name) );
            }
            
            switch (sortOrder)
            {
                case SortState.LoginDesc:
                    users = users.OrderByDescending(s => s.Login);
                    break;
                case SortState.NameAsc:
                    users = users.OrderBy(s => s.Name);
                    break;
                case SortState.NameDesc:
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case SortState.SurnameAsc:
                    users = users.OrderBy(s => s.Surname);
                    break;
                case SortState.SurnameDesc:
                    users = users.OrderByDescending(s => s.Surname);
                    break;
                case SortState.EmailAsc:
                    users = users.OrderBy(s => s.Email);
                    break;
                case SortState.EmailDesc:
                    users = users.OrderByDescending(s => s.Email);
                    break;
                default:
                    users = users.OrderBy(s => s.Login);
                    break;
            }

            var items = await users.ToListAsync();
            
            IndexView viewModel = new IndexView
            {
                SortView = new SortView(sortOrder),
                FilterView = new FilterView(db.Users.ToList(), id, name),
                Users = items
            };
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if(id!=null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p=>p.Id==id);
                if (user != null) return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}