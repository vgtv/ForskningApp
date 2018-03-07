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
        /*
        private ITextMiningBLL textMining;

        public TextMiningController()
        {
            textMining = new TextMiningBLL();
        }

        public ActionResult Index()
        {
            /*
            WordDictionary englishDictionary = new WordDictionary { DictionaryFile = "en-US.dic" };
            englishDictionary.Initialize();
            Spelling englishSpeller = new Spelling { Dictionary = englishDictionary };
            EnglishStemmer englishStemmer = new EnglishStemmer();

            var cristinIDList = textMining.getCristinID();
            var stopWords = textMining.getStopWords();

            // textMining.addStopsWordsDB(new List<string> { "" });

            foreach (var cristinID in cristinIDList)
            {
                Debug.WriteLine("-------- " + cristinID + " -----------");

                var titles = textMining.getTitles(cristinID);

                if (titles == null) continue;

                var tokenizedTitles = textMining.tokenizeTitles(titles);

                textMining.removeLanguages(tokenizedTitles, englishSpeller);

                if(textMining.isActive(tokenizedTitles))
                {
                    textMining.removeStopWords(tokenizedTitles, stopWords);
                    textMining.stemTitles(tokenizedTitles, englishStemmer);

                    var groupedWords = textMining.groupTitles(tokenizedTitles);

                    var saved = textMining.saveWords(groupedWords);

                    if (saved)
                    {
                        Debug.WriteLine("-----------Save operation has succed-----------");
                    }
                    else
                    {
                        Debug.WriteLine("----------Error while svaing----------------");
                    }

                }

                /*foreach (var w in wordCloud)
                {
                    Debug.WriteLine("TEXT MINER | word: " + w.Key + ", count: " + w.Count());
                }*/

                /*if (textMining.isActive(groupeWords))
                {
                    textMining.saveWords(groupeWords);
                    textMining.saveWordCloud(groupeWords, cristinID);
                }*/
                /*
            }

            
        }
*/
    }
}