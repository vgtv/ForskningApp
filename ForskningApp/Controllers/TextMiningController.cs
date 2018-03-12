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
            return View();
        }

        public ActionResult StartTextMining()
        {
            WordDictionary englishDictionary = new WordDictionary { DictionaryFile = "en-US.dic" };
            englishDictionary.Initialize();
            Spelling englishSpeller = new Spelling { Dictionary = englishDictionary };
            EnglishStemmer englishStemmer = new EnglishStemmer();

            var cristinIDList = textMining.getCristinID();
            var stopWords = textMining.getStopWords();

            var addedStopWords = new List<string> { "studi", "base", "analysi", "effect", "activ", "high",
            "factor", "year", "express", "cross", "experi", "level", "assess", "case", "cohort", "impact",
            "term", "low", "process", "long", "depend", "perform", "type", "increas", "outcom", "evalu",
            "top", "larg", "comparison", "section", "central", "person", "problem","rate", "general" };

            Int32 counter = 0;
            Int32 total = cristinIDList.Count();

            foreach (var cristinID in cristinIDList)
            {
                var titles = textMining.getTitles(cristinID);
                if (titles == null) continue;

                var tokenizedTitles = textMining.tokenizeTitles(titles);
                textMining.removeLanguages(tokenizedTitles, englishSpeller);

                if (textMining.isActive(tokenizedTitles))
                {
                    textMining.removeStopWords(tokenizedTitles, stopWords);
                    textMining.stemTitles(tokenizedTitles, englishStemmer);
                    textMining.removeStopWords(tokenizedTitles, addedStopWords); // sikrer at de stemmete ordene også fjernes

                    short totalTitles = (short)tokenizedTitles.Count();
                    var groupedWords = textMining.groupTitles(tokenizedTitles);

                    if (textMining.saveWordCloud(groupedWords, cristinID, totalTitles))
                    {
                        Debug.WriteLine("Saving: (" + (++counter) + "/" + total + ")");
                    }
                    else
                    {
                        Debug.WriteLine("An unexpected error has occured at cristinId: " + cristinID);
                        return View();
                    }
                }
                else
                {
                    Debug.WriteLine("Saving: (" + (++counter) + "/" + total + ")");
                }
            }
            Debug.WriteLine("Text Mining Complete");
            return View();
        }
    }
}