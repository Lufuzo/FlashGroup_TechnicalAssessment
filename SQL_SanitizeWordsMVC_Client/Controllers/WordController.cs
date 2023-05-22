using Microsoft.AspNetCore.Mvc;
using NuGet.ProjectModel;
using SQL_SanitizeWordsMVC_Client.Models;

namespace SQL_SanitizeWordsMVC_Client.Controllers
{
    public class WordController : Controller
    {
        private readonly APIGateway apiGateway;


        public WordController(APIGateway _apiGateway)
        {
            apiGateway = _apiGateway;
        }


        public IActionResult Index()
        {
            List<Word> words; 
            words = apiGateway.ListWords();
            return View(words);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Word words = new Word();
            return View(words);
        }
    
        [HttpPost]
        public IActionResult Create(Word word)
        {
            apiGateway.CreateWord(word);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
           Word words = new Word();

            words = apiGateway.GetWord(id);
            return View(words);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Word word; 

            word = apiGateway.GetWord(id);
            return View(word);
        }

        [HttpPost]
        public IActionResult Edit(Word word)
        {
            apiGateway.UpdateWord(word);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Word word;
            word = apiGateway.GetWord(id);
            return View(word);
        }

        [HttpPost]
        public IActionResult Delete(Word word)
        {
            apiGateway.DeleteWord(word.id);
            return RedirectToAction("Index");
        }
    }
}
