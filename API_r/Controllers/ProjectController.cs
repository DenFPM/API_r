using API_r.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_r.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProjectController : Controller
    {
        DbContext_Model db = new DbContext_Model(DbConnectionOption());

        [HttpPost]
        [Route("project")]
        public IActionResult CreateProject()
        {
            Project project = new Project() { Title = Request.Form["Title"], Img = Request.Form["Img"], Technologies = Request.Form["Technologies"], WebSite = Request.Form["WebSite"] };

            if (project.Title != null && project.Technologies != null)
            {
                db.projects.Add(project);
                db.SaveChanges();

                return Ok();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        [HttpGet]
        [Route("projects")]
        public DbSet<Project> GetProjects()
        {
            return db.projects;
        }

        [HttpPut]
        [Route("project")]
        public IActionResult UpdateProject(int id)
        {
            Project projectObj = new Project() { Title = Request.Form["Title"], Img = Request.Form["Img"], Technologies = Request.Form["Technologies"], WebSite = Request.Form["WebSite"] };
            var project = db.projects.FirstOrDefault(item => item.Id == id);

            if (project != null)
            {
                if (projectObj.Title != "")
                {
                    project.Title = projectObj.Title;
                }
                if (projectObj.Img != "")
                {
                    project.Img = projectObj.Img;
                }
                if (projectObj.Technologies != "")
                {
                    project.Technologies = projectObj.Technologies;
                }
                if (projectObj.WebSite != "")
                {
                    project.WebSite = projectObj.WebSite;
                }
                else
                {
                    return new BadRequestObjectResult("nothing to change");
                }
            }
            else
            {
                return new BadRequestObjectResult("not founds project");
            }
            db.projects.Update(project);
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("project")]
        public void DeleteProject(int id)
        {
            
            foreach(Project pr in db.projects)
            {
                if (pr.Id == id)
                {
                    db.projects.Remove(pr);
                }
            }
            db.SaveChanges();
        }
        static public DbContextOptions<DbContext_Model> DbConnectionOption()
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();

            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DbContext_Model>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            return options;
        }
    }
}
