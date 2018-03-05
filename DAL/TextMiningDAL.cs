﻿using Iveonik.Stemmers;
using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TextMiningDAL : ITextMiningDAL
    {
        /****************
         * Get Methods
         ****************/

        public List<string> getCristinID()
        {
            using (var db = new dbEntities())
            {
                return db.person.Select(p => p.cristinID).Take(250).ToList();
            }
            // Take(5) tar kun 5 stykker til å starte med           
        }

        public List<string> getTitles(string cristinID)
        {
            using (var db = new dbEntities())
            {
                // Frode sin id er 65073

                // author: cristinID | forskningsID
                //         65073     | 100
                //         65073     | 101
                //         65073     | 102
                // research tittel   | forskningsid
                //         blalbla   | 100
                //          sdfsdfds | 101

                /*
                var forskningsIDer = db.author.Where(a => a.cristinID == cristinID)
                    .Select(a => a.forskningsID).ToList();

                List<string> tittlerListe = new List<string>();
                foreach (var forskningID in forskningsIDer)
                {
                    tittlerListe.Add(db.research.Where(r => r.forskningsID == forskningID)
                        .Select(r => r.tittel).FirstOrDefault().ToLower());
                }
                */



                return db.author.Where(a => a.cristinID == cristinID)
                     .Select(a => (db.research.Where(r => r.forskningsID == a.forskningsID)
                     .Select(r => r.tittel)).FirstOrDefault().ToLower())
                     .ToList();
            }
        }


        public List<string> getStopWords()
        {
            using (var db = new dbEntities())
            {
                return db.stopwords.Select(sw => sw.Word).ToList();
            }
        }

        public List<string> getTopCloudWords()
        {
            throw new NotImplementedException();
        }  

        /**************************
         * Text Modifying methods
         **************************/

        public List<List<string>> stemTitles(List<List<string>> tokenizedTitles, EnglishStemmer stemmerObj)
        {
            tokenizedTitles.ForEach(title => title.ForEach(word => stemmerObj.Stem(word)));
            return tokenizedTitles;
        }

        public List<List<string>> tokenizeTitles(List<string> titles)
        {
            var tokenizedTitles = new List<List<string>>();
            foreach (var title in titles)
            {
                var tmp = removeSpecialCharacters(title);
                tokenizedTitles.Add(tmp.Split().ToList());
            }
            return tokenizedTitles;
        }

        public string removeSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsLetter(c) || c == '\'' || char.IsWhiteSpace(c))
                {
                    sb.Append(c);
                }
                else if (c == '-' || c == '_')
                {
                    sb.Append(' ');
                }
            }
            return sb.ToString();
        }


        public IOrderedEnumerable<IGrouping<string, string>> groupTitles(List<List<string>> tokenizedTitles)
        {
            var wordList = new List<string>();
            tokenizedTitles.ForEach(title => title.ForEach(word => wordList.Add(word)));

            var groupedList = wordList.GroupBy(i => i).OrderByDescending(g => g.Count());

            // var groupedList = wordList.GroupBy(i => i).ToList();
            // groupedList.Sort();

            return groupedList;
        }

        // deres filbane må endres her
        // @"C:\Users\an2n\fil.txt"
       
        public List<List<string>> removeLanguages(List<List<string>> tokenizedTitles, Spelling spelling)
        {
            using (StreamWriter writer = new StreamWriter(@"C:\Users\an2n\fil.txt", true))
                foreach (var titles in tokenizedTitles.ToList())
                {
                    if (!checkLanguage(titles, spelling))
                    {
                        foreach(var word in titles)
                        {
                            writer.Write(word + " ");
                        }
                       writer.WriteLine("");

                       tokenizedTitles.Remove(titles);
                    }
                }
            return tokenizedTitles;
        }


        public bool checkLanguage(List<string> tokenizedTitle, Spelling spelling)
        {
            var wordCount = 0;

            foreach (var words in tokenizedTitle)
            {
                if (spelling.TestWord(words))
                {
                    if (++wordCount > 2) return true;
                }
            }
            return false;
        }

        /**********************
         * Stop Words Methods
         **********************/

        public void addStopsWordsDB(List<string> stopWords)
        {
            using (var db = new dbEntities())
            {
                foreach (var newStopWord in stopWords)
                {
                    if (db.stopwords.Where(sw => sw.Word == newStopWord).FirstOrDefault() == null)
                    {
                        db.stopwords.Add(new stopwords { Word = newStopWord });
                    }
                }
                db.SaveChanges();
            }
        }

        public void removeStopsWordsDB(List<string> stopWords)
        {
            using (var db = new dbEntities())
            {
                foreach (var oldStopWord in stopWords)
                {
                    var containsStopWord = db.stopwords.Where(sw => sw.Word == oldStopWord).FirstOrDefault();
                    if (containsStopWord != null) {
                        db.stopwords.Remove(containsStopWord);
                    }
                }
                db.SaveChanges();
            }
        }

        public List<List<string>> removeStopWords(List<List<string>> tokenizedTitles, List<string> stopWords)
        {
            tokenizedTitles.ForEach(title => title.ToList().ForEach(word => {
                if (checkStopWord(word, stopWords)) title.Remove(word);
            }));

            return tokenizedTitles;
        }


        public bool checkStopWord(string token, List<string> stopWords)
        {
            foreach (var stopWord in stopWords)
            {
                if (token == stopWord)
                {
                    return true;
                }
            }
            return false;
        }

        /***********************
         * Word Cloud Methods
         ***********************/

        public bool savePersonWordCloud(List<string> groupedTitles)
        {
            throw new NotImplementedException();
        }

        public bool saveWordCloud(List<string> groupedTitles)
        {
            throw new NotImplementedException();
        }

        public bool updateWordCloud(List<string> groupedTitles)
        {
            throw new NotImplementedException();
        }
    }
}
