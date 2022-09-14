using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OpusTechInterview.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpusTechInterview.Controllers
{
    public class EmployeeController : Controller
    {
        readonly ApplicationDbContext _db = null;
        readonly IWebHostEnvironment _env;
        public EmployeeController(ApplicationDbContext db, IWebHostEnvironment env) 
        { 
            _db = db; 
            _env = env; 
        }
        public IActionResult Index()
        {
            return View(_db.Employees.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeVM employee)
        {
            if (ModelState.IsValid)
            {
                if (_db.Employees.Any(x => x.EmployeeCode.ToLower() == employee.EmployeeCode.ToLower()))
                {
                    ModelState.AddModelError("", $"{employee.EmployeeCode} already exist");
                    return View(employee);
                }
                var pNew = new Employee()
                {
                    EmployeeCode = employee.EmployeeCode,
                    Name = employee.Name,
                    Phone = employee.Phone,
                    Email = employee.Email,
                    Photo = "no-pic.png"
                };
                if (employee.Photo != null && employee.Photo.Length > 0)
                {
                    string dir = Path.Combine(_env.WebRootPath, "Uploads");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    string fileName = Guid.NewGuid() + Path.GetExtension(employee.Photo.FileName);
                    string fullPath = Path.Combine(dir, fileName);
                    FileStream fs = new FileStream(fullPath, FileMode.Create);
                    employee.Photo.CopyTo(fs);
                    fs.Flush();
                    fs.Close();
                    pNew.Photo = fileName;
                }
                _db.Employees.Add(pNew);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

    }
}
