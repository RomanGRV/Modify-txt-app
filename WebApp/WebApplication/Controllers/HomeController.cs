using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Sentence> sentences;

            using (var context = new WebAppContext())
            {
                sentences = context.Sentences.ToList();
            }

            var home = new HomeViewModel
            {
                Sentences = sentences
            };

            return View(home);
        }

        [HttpPost]
        public ActionResult Index(Upload upload)
        {
            var model = new SearchViewModel();

            if (!string.IsNullOrEmpty(upload.Search) && upload.File != null && upload.File.ContentLength > 0)
            {
                using (var reader = new StreamReader(upload.File.InputStream))
                {
                    var str = reader.ReadToEnd();

                    model.Search = upload.Search;
                    model.Text = str;

                    var sentences = GetSentences(str, upload.Search);

                    Save(sentences);
                }
            }

            return RedirectToAction("Index");
        }

        private static void Save(IEnumerable<Sentence> sentences)
        {
            var context = new WebAppContext();
            {
                foreach (var sentence in sentences)
                {
                    context.Sentences.Add(sentence);
                }

                context.SaveChanges();
            }
        }

        private static IEnumerable<Sentence> GetSentences(string text, string search)
        {
            var sentences = new List<Sentence>();
            // Умова (від крапки до крапки)
            foreach (var sentence in text.Split('.'))
            {
                if (sentence.ToLower().Contains(search.ToLower()))
                {
                    var count = CountStringOccurrences(sentence.ToLower(), search.ToLower());

                    var sentenceToSave = new Sentence
                    {
                        Text = new string(sentence.Reverse().ToArray()), // Умова (буквами у зворотньому порядку)
                        Word = search,
                        Count = count
                    };

                    sentences.Add(sentenceToSave);
                }
            }

            return sentences;
        }

        private static int CountStringOccurrences(string text, string pattern)
        {
            var count = 0;
            var i = 0;
            // Умова (зберігаючи к-ть входжень)
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }
    }
}