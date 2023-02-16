using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TISLR2.Models;
using Users.Models;

namespace TISLR2.Controllers
{
    public class HomeController : Controller
    {

        // 3 - Интеграционное тестирование в ASP.NET Core MVC 
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {

            if (db.Users.Any(el => el.Email == user.Email))
            {
                return Content("Пользователь с таким email уже существует");
            }
            if (user.Phone == null || user.Age == 0 || user.Name == null || user.Email ==
            null)
            {
                return Content("Введены не все данные");
            }
            else if (!user.IsValid(user.Phone))
            {
                return Content("Номер телефона должен содержать 11 цифр");
            }
            else if (!Regex.IsMatch(user.Phone, @"^\d+$"))
            {
                return Content("Номер телефона должен состоять только из цифр");
            }
            else
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            if (user.Phone == null || user.Age == 0 || user.Name == null || user.Email ==
            null)
            {
                return Content("Введены не все данные");
            }
            else if (!user.IsValid(user.Phone))
            {
                return Content("Номер телефона должен содержать 11 цифр");
            }
            else
            {
                bool msg = user.IsValid(user.Phone);
                if (msg == true)
                {
                    db.Users.Update(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else return Content("Номер телефона должен содержать 11 цифр");
            }
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                User user = new User { Id = id.Value };
                db.Entry(user).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }


        // New methods
        [HttpDelete("{id}")]
        [ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User  user = db.Users.FirstOrDefault(el => el.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        [ActionName("UpdateUser")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            if (user.Phone == null || user.Age == 0 || user.Name == null || user.Email ==
            null)
            {
                return Content("Введены не все данные");
            }
            else
            {
                bool msg = user.IsValid(user.Phone);
                if (msg == true)
                {
                    db.Users.Update(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else return Content("Номер телефона должен содержать 11 цифр");
            }
        }
    }

    // 2 - Тестирование контроллеров MVC в ASP.NET Core
    /*public class HomeController : Controller
    {
        IRepository _repo;
        public HomeController(IRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public IActionResult GetUser(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            User user = _repo.Get(id.Value);
            if (user == null)
                return NotFound();
            return View(user);
        }

        public IActionResult CreateUser() => View();

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                _repo.Create(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

    }*/


    // 1 - Создание стартового и тестового проектов, модульное тестирование
    /*public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (user.Phone == null || user.Age == 0 || user.Name == null || user.Email ==
            null)
            {
                return Content("Введены не все данные");
            }
            else
            {
                bool msg = user.IsValid(user.Phone);
                if (msg == true)
                {
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else return Content("Номер телефона должен содержать 11 цифр");
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            if (user.Phone == null || user.Age == 0 || user.Name == null || user.Email ==
            null)
            {
                return Content("Введены не все данные");
            }
            else
            {
                bool result = user.IsValid(user.Phone);
                if (result == true)
                {
                    db.Users.Update(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else return Content("Номер телефона должен содержать 11 цифр");
            }
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                User user = new User { Id = id.Value };
                db.Entry(user).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }


    }*/
}
