using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVC_Intro.Data.Interfaces;
using MVC_Intro.Models;
using MVC_Intro.ViewModel;

namespace MVC_Intro.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public BlogController(DBContext context, IPostRepository postRepository, IHostingEnvironment hostingEnvironment)
        {
            _postRepository = postRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [Route("Blog/Post/{id}")]
        public IActionResult Post(int id)
        {
            var post = _postRepository.GetPostById(id);

            if (post == null)
            {
                Response.StatusCode = 404;
                return View("PostNotFound", id);
            }
                 
            return View(post);
        }

        public IActionResult Blog()
        {
            var posts = _postRepository.GetAllPosts().ToList(); ;
            return View(posts);
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            BlogModel post = _postRepository.GetPostById(id);
            PostEditViewModel postEditViewModel = new PostEditViewModel
            {
                Id = post.id,
                title = post.title,
                author = post.author,
                preview = post.preview,
                fullPost = post.fullPost,
                ExistImgPath = post.img
            };
            return View(postEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PostEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                BlogModel post = _postRepository.GetPostById(model.Id);
                post.author = model.author;
                post.title = model.title;
                post.preview = model.preview;
                post.fullPost = model.fullPost;

                if (model.img != null){
                    if (model.ExistImgPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "img", model.ExistImgPath);
                        System.IO.File.Delete(filePath);
                    }
                    post.img = UploadedFile(model);
                }
               
                _postRepository.UpdatePost(post);
                return RedirectToAction("Blog");
            }
            return View();
        }

        private  string UploadedFile(PostEditViewModel model)
        {
            string uniqFileName = null;
            if (model.img != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "img");
                uniqFileName = Guid.NewGuid().ToString() + "_" + model.img.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.img.CopyTo(fileStream);
                }
            }

            return uniqFileName;
        }
    }
}