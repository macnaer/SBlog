using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_Intro.Models;

namespace MVC_Intro.Controllers
{
    public class BlogController : Controller
    {
        private readonly DBContext _context;
        public BlogController(DBContext context)
        {
            _context = context;
        }
        public IActionResult Blog()
        {
            List<BlogModel> posts = _context.Blog.ToList();
            return View(posts);
        }
    }
}