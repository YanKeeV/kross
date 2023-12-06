using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SampleMvcApp.Controllers
{
    public class Labs : Controller
    {

        [Authorize]
        public IActionResult Lab1()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public void Lab1(string input_file, string output_file)
            => webLabs.Lab1.task1(input_file, output_file);

        [Authorize]
        public IActionResult Lab2()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public void Lab2(string input_file, string output_file)
            => webLabs.Lab2.task2(input_file, output_file);

        [Authorize]
        public IActionResult Lab3()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public void Lab3(string input_file, string output_file)
             => webLabs.Lab3.task3(input_file, output_file);
    }
}
