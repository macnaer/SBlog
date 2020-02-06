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

        [Route("Blog/Post/{id}")]
        public IActionResult Post(int id)
        {
            var query = _context.Blog.AsQueryable();

            var posts = query.Where(post => post.id == id).Select(p => new BlogModel
            {
                id = p.id,
                title = p.title,
                author = p.author,
                fullPost = p.fullPost,
                img = p.img,
            }).SingleOrDefault();

            return View(posts);
        }

        public IActionResult Blog()
        {
            List<BlogModel> posts = _context.Blog.ToList();
            return View(posts);
        }
    }
}