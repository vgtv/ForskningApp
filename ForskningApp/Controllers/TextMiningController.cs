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
            englishDictionary.Initialize();
            Spelling englishSpeller = new Spelling { Dictionary = englishDictionary };
            EnglishStemmer englishStemmer = new EnglishStemmer();

            var cristinIDList = textMining.getCristinID();
            var stopWords = textMining.getStopWords();

            // textMining.addStopsWordsDB(new List<string> { "" });

            Int32 counter = 0;
            Int32 total = cristinIDList.Count();

            foreach (var cristinID in cristinIDList)
            {
                var titles = textMining.getTitles(cristinID);
                if (titles == null) continue;
           
                var tokenizedTitles = textMining.tokenizeTitles(titles);
                textMining.removeLanguages(tokenizedTitles, englishSpeller);

                if(textMining.isActive(tokenizedTitles))
                {
                    textMining.removeStopWords(tokenizedTitles, stopWords);
                    textMining.stemTitles(tokenizedTitles, englishStemmer);

                    var groupedWords = textMining.groupTitles(tokenizedTitles);


                    if (textMining.saveWords(groupedWords)){                        
                        Debug.WriteLine("Progress "+(++counter)+"/"+total);
                    }
                    else
                    {
                        Debug.WriteLine("An unexpected error has occured" + cristinID);
                        return View();
                    }
                }
                else
                {
                    Debug.WriteLine("Progress "+(++counter) + "/" + total);
                }
            }
            return View();
        }
    }
}