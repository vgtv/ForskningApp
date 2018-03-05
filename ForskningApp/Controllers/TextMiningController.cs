using BLL;
using Iveonik.Stemmers;
using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForskningApp.Controllers
{
    public class TextMiningController : Controller
    {
        private ITextMiningBLL textMining;

        public TextMiningController()
        {
            textMining = new TextMiningBLL();
        }

        public ActionResult Index()
        {

            WordDictionary englishDictionary = new WordDictionary { DictionaryFile = "en-US.dic" };
     
            Spelling englishSpeller = new Spelling { Dictionary = englishDictionary };
            EnglishStemmer englishStemmer = new EnglishStemmer();

            var scientistsList = textMining.getCristinID();
            var stopWords = textMining.getStopWords();

            // tokenizer legger til noen "ord" som tomme mellomrom
            // fant ikke ut hva årsaken er, sletter disse bare om de forekommer
            // i stopword tabellen
            textMining.addStopsWordsDB(new List<string> { "" });

            foreach (var scientistID in scientistsList)
            {
                Debug.WriteLine("-------- " + scientistID + " -----------");


                var titles = textMining.getTitles(scientistID);


                if (titles == null) continue;

                var tokenizedTitles = textMining.tokenizeTitles(titles);

                textMining.removeLanguages(tokenizedTitles, englishSpeller);

                textMining.removeStopWords(tokenizedTitles, stopWords);
                textMining.stemTitles(tokenizedTitles, englishStemmer);

                var wordCloud = textMining.groupTitles(tokenizedTitles);

                foreach (var w in wordCloud)
                {
                Debug.WriteLine("word: " + w.Key + ", count: " + w.Count());
                }
            }

            return View();
        }
    }
}