using Iveonik.Stemmers;
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
                return db.person.Select(p => p.cristinID).Take(1000).ToList();
            }
            // Take(5) tar kun 5 stykker til å starte med           
        }

        public List<string> getTitles(string cristinID)
        {
            using (var db = new dbEntities())
            {
                // Frode sin id er 65073

                // Author:    cristinID | forskningsID
                //            65073     | 100
                //            65073     | 101
                //            65073     | 102

                // Research : tittel    | forskningsID
                //            blalbla   | 100
                //            sdfsdfds  | 101
                // List<string> titler = {blalbla],{sdfsdfds]


                return db.author.Where(a => a.cristinID == cristinID)
                     .Select(a => (db.research.Where(r => r.cristinID == a.forskningsID)
                     .Select(r => r.tittel).FirstOrDefault()).ToLower())
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

            for (int i = 0; i < titles.Count; i++)
            {
                titles[i] = removeSpecialCharacters(titles[i]);

            }
            var test = titles.Distinct().ToList();

            foreach (var title in test)
            {
                tokenizedTitles.Add(title.Split().ToList());
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
                else if (c == '-' || c == '_' || c == '–' || c == ':')
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

        public List<List<string>> removeLanguages(List<List<string>> tokenizedTitles, Spelling spelling)
        {
            foreach (var titles in tokenizedTitles.ToList())
            {
                if (!isEnglish(titles, spelling))
                {
                    tokenizedTitles.Remove(titles);
                }
            }
            return tokenizedTitles;
        }


        public bool isEnglish(List<string> tokenizedTitle, Spelling spelling)
        {
            Int32 wordCount = 0;
            Int32 length = tokenizedTitle.Count;
            Int32 percentage = 0;

            foreach (var words in tokenizedTitle)
            {
                if (spelling.TestWord(words))
                {
                    ++wordCount;
                    percentage = (int)(0.5f + ((100f * wordCount) / length));
                }
            }

            if (percentage > 45)
            {
                /*Debug.WriteLine(percentage + "% is english: ");

                foreach (var v in tokenizedTitle)
                {
                    Debug.Write(v + " ");
                }
                Debug.WriteLine(wordCount + ":" + length);*/
                return true;
            }
            else
            {
                /*Debug.WriteLine(percentage + "% is english: ");
                foreach (var v in tokenizedTitle)
                {
                    Debug.Write(v + " ");
                }
                Debug.WriteLine(wordCount + ":" + length);*/
                return false;
            }
        }

        public List<List<string>> removeStopWords(List<List<string>> tokenizedTitles, List<string> stopWords)
        {
            tokenizedTitles.ForEach(title => title.ToList().ForEach(word =>
            {
                if (isStopWord(word, stopWords)) title.Remove(word);
            }));

            return tokenizedTitles;
        }


        public bool isStopWord(string token, List<string> stopWords)
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
         * Saving/Adding/Removing from Database
         ***********************/


        public bool saveWordCloud(IOrderedEnumerable<IGrouping<string, string>> groupedWords)
        {
            using (var db = new dbEntities())
            {
                // dette er en test funksjon for å sjekke at det blir lagret til databasen
                // denne delen av koden er kun for å lagre ordene og antallet av dem
                // videre må det implementeres en funksjon for å lagre cristinid
                // og de tilhørende ordene og antall forekomster av dem i "wordsky" tabellen

                foreach (var word in groupedWords)
                {
                    var foundWord = db.words.Where(w => word.Key == w.word).FirstOrDefault();
                    if (foundWord == null)
                    {
                        db.words.Add(new words { word = word.Key, count = word.Count() });
                    }
                    else
                    {
                        foundWord.count += word.Count();
                    }
                }
                db.SaveChanges();
                return true;
            }
        }
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
                    if (containsStopWord != null)
                    {
                        db.stopwords.Remove(containsStopWord);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
