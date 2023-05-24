using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Text;
using NuGet.ProjectModel;
using SQL_SanitizeWordsMVC_Client.Models;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

            var checkWord =  GetWords();


            var result = checkWord.Where(x => x.FileValues.Any(y => y.Contains(word.value))).ToList();
            if (result.Count == 0)
            {
                apiGateway.CreateWord(word);
            }
            else
            {
                string mess = "Dangerous input!!"+" "+"Sensitive no inserted in the database !!";
                ViewData["message"] = mess;
            }

            
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

        // create list 

        public class FileValue
        {
            public List<string> FileValues { get; set; }

            public FileValue()
            {
                FileValues = new List<string>();
            }
        }
        // reading a file
        private static List<FileValue> GetWords()
        {
            string path = "C:\\Users\\Lungelo Mbalane\\Documents\\Visual Studio 2022\\SQL_Words_Application\\SQL_SanitizeWordsMVC_Client\\SQL_SanitizeWordsMVC_Client\\Data\\sql_sensitive_list.txt";

            var fileValues = new List<FileValue>();
            var fileValue = new FileValue();
           
            List<String[]> arrayList = new List<String[]>();
            //string [] words = File.ReadAllLines(path).ToString();
            using (StreamReader file = new StreamReader(path))
            {
                //int counter = 0;
                string line = String.Empty;

                while (!String.IsNullOrEmpty(line = file.ReadLine()))
                {
                    fileValue.FileValues.Add(line);
                    fileValues.Add(fileValue);

                   
                }

    
                file.Close();


            }


            return fileValues;
      }
}
}
