using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_FirstTask.Data.Interfaces;
using WebApplication_FirstTask.Models;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication_FirstTask.Controllers
{
    public class FormatController : Controller
    {
        private readonly IFormatTaskInterface formatTask;
        private readonly IHostingEnvironment hostingEnvironment;

        public FormatController(IFormatTaskInterface formatTask, IHostingEnvironment hostingEnvironment)
        {
            this.formatTask = formatTask;
            this.hostingEnvironment = hostingEnvironment;
        }
        // GET: /<controller>/

        public IActionResult Index()
        {
            var getAll = formatTask.GetAll();
            return View(getAll);
        }

        public IActionResult Details(int Id)
        {
            var getById = formatTask.GetById(Id);

            return View(getById);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskFormat task, IFormFile fileName)
        {
            string filename = "Format" + Path.GetExtension(fileName.FileName);

            var path = Path.GetFullPath(Path.Combine(hostingEnvironment.WebRootPath, "file"));

            if(path != null)
            {
                GetFiles(path);
            }

          

            if (fileName != null)
            {
                using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    fileName.CopyTo(stream);
                }

                string[] files = System.IO.Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories);
                var txtFiles = Directory.EnumerateFiles(path, "*.txt");

                foreach (string currentFile in txtFiles)
                {
                    var lines = System.IO.File.ReadAllLines(currentFile).ToList();

                    foreach (var line in lines)
                    {
                        if (line != null)
                        {
                            if (lines.First() == line)
                            {
                                continue;
                            }

                           
                            string[] entries = line.Split(",");

                            if (!string.IsNullOrWhiteSpace(line))
                            {
                                task.Color = entries[0].Trim();
                                task.Number = Convert.ToInt32(entries[1].Trim());
                                task.Label = entries[2].Trim();

                                TaskFormat formatT = new();

                                formatT.Color = task.Color;

                                formatT.Number = task.Number;

                                formatT.Label = task.Label;

                                formatTask.Add(formatT);

                            }


                          
                        }
                    }

                    formatTask.Commit();

                    return RedirectToAction("Index");
                }

            }

            return View();
        }


        private static string GetFiles(string path)
        {
            var file = new DirectoryInfo(path).GetFiles().OrderByDescending(o => o.LastWriteTime).FirstOrDefault();

            return file?.Name;
        }


        public IActionResult Update(int id)
        {
            var getbyId = formatTask.GetById(id);
            return View(getbyId);
        }

        [HttpPost]
        public IActionResult Update(TaskFormat task)
        {
            if (task != null)
            {
                formatTask.Update(task);

                formatTask.Commit();

                return RedirectToAction("Index");
            }

            return NoContent();
        }


        public IActionResult Delete(int Id)
        {
            var getbyId = formatTask.GetById(Id);
            return View(getbyId);
        }

        [HttpPost]
        public IActionResult Delete(TaskFormat task)
        {
            if (task != null)
            {
                formatTask.Delete(task.Id);

                formatTask.Commit();

                return RedirectToAction("Index");
            }

            return NoContent();
        }



        [HttpPost]
        public List<object> GetFormatData()
        {
            List<object> data = new();

            var color = formatTask.GetAll().Select(x => x.Color).ToList();

            data.Add(color);

            var label = formatTask.GetAll().Select(x => x.Label).ToList();

            data.Add(label);

            var number = formatTask.GetAll().Select(x => x.Number).ToList();

            data.Add(number);


            return data;
        }
    }
}

