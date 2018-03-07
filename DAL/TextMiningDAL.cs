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

        /*
         * Finner alle personer sine cristinID'er
         */ 
        public List<string> getCristinID()
        {
            using (var db = new dbEntities())
            {
                return db.person.Select(p => p.cristinID).Take(15).ToList();
            }
        }

        /* Henter alle tittlene til en forsker
         * Når vi først spør: a.cristinID == cristinID finner vi alle radene personen forekommer i tabellen 'author'
         * Hver rad i 'author' har en forskningsID. Disse forskningsID'ene bruker vi videre i 'research' tabellen for
         * å så finne alle titlene i 'research' tabellen.
         * 
         */ 
        public List<string> getTitles(string cristinID)
        {
            using (var db = new dbEntities())
            {
                return db.author.Where(a => a.cristinID == cristinID)
                     .Select(a => (db.research.Where(r => r.cristinID == a.forskningsID)
                     .Select(r => r.tittel).FirstOrDefault()).ToLower())
                     .ToList();
            }
        }

       /*
        * Henter alle stoppord fra database tabellen 'stopwords'
        */ 
        public List<string> getStopWords()
        {
            using (var db = new dbEntities())
            {
                return db.stopwords.Select(sw => sw.Word).ToList();
            }
        }

        // Kan slettes
        public List<string> getTopCloudWords()
        {
            throw new NotImplementedException();
        }

        /**************************
         * Text Modifying methods
         **************************/

        /*
         * Stemmer alle ord i en eller flere titler
         */ 
        public List<List<string>> stemTitles(List<List<string>> tokenizedTitles, EnglishStemmer stemmerObj)
        {
            tokenizedTitles.ForEach(title => title.ForEach(word => stemmerObj.Stem(word)));
            return tokenizedTitles;
        }

        /*
         *  Hakker opp tittlene til ord og velger kun unike titler (Distinct)
         *  
         *  @Implements removeSpecialCharacters(string str)
         */
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

        /*
         * Trimmer en tittel og returnerer denne
         */ 
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


        /*
         * Noramliserer tokenizedTitles listen til en liste med strenger. Det vil si at alle ord er i en lang liste.
         * Grupperer og sorterer ordene (størst først) og returnerer denne listen.
         * 
         * !Hvor mange ord skal vi ha per person? Her tar jeg ut Take(10)
         */
        public List<IGrouping<string, string>> groupTitles(List<List<string>> tokenizedTitles)
        {
            var wordList = new List<string>();
            tokenizedTitles.ForEach(title => title.ForEach(word => wordList.Add(word)));
            return wordList.GroupBy(i => i).OrderByDescending(g => g.Count()).Take(10).ToList();
        }

        /*
         * Sletter en tittel dersom tittelen ikke er engelsk
         * 
         * @Implements isEnglish(List<string> tokenizedTitle, Spelling spelling)
         */
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


        /*
         * Returner true om det er 45% sjanse for at tittelen er engelsk. 
         * Dette kan justeres, men må testes grundigere.
         */
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

        /* 
         * Sletter ord som er stoppord
         * 
         * @Implements isStopWord(string token, List<string> stopWords)
         */

        public List<List<string>> removeStopWords(List<List<string>> tokenizedTitles, List<string> stopWords)
        {
            tokenizedTitles.ForEach(title => title.ToList().ForEach(word =>
            {
                if (isStopWord(word, stopWords)) title.Remove(word);
            }));

            return tokenizedTitles;
        }

        /*
         * Returner true om et ord er et stoppord
         */
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

        /* Denne metoden skal finne ut om vi skal lagre ordene eller ikke og gjøre en person inaktiv:
         * Vi kan f.eks. si at en bruker er innaktiv dersom han har:
         * mindre enn 10 ord og antall forekomster av ordene ikke er mer enn 30.
         *
         * !Dette er noe som bør testes for å finne en balanse. Hva tenker dere?
         */
        public bool isActive(List<IGrouping<string, string>> groupedWords, int titleCount)
        {
            // Eksempel
            Int32 count = 0;
            Int32 listSize = groupedWords.Count();

            if (listSize < 10)
            {
                return false;
            }

            foreach (var words in groupedWords)
            {
                count += words.Count();
            }

            if (count < 30)
            {
                return false; 
            }

            return true;
        }

        /***********************
         * Saving/Adding/Removing from Database
         ***********************/

        
        /*
         * Lagrer nye - eller oppdaterer antall forekomster av et ord i tabellen 'word'
         * 
         */ 
        public bool saveWords(List<IGrouping<string, string>> groupedWords)
        {
            using (var db = new dbEntities())
            {
                try
                {
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
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message.ToString());
                    return false;
                }
                return true;
            }
        }

        /*
         * Lagrer ordskyen til en person i tabellen 'wordsky'
         * Key er et ord fra 'word' tabellen
         * 
         * cristinID | key | count
         * 10050     | 2   | 20
         * 10050     | 100 | 3
         */
        public bool saveWordCloud(List<IGrouping<string, string>> groupedWords, string cristinID)
        {
            using (var db = new dbEntities())
            {
                try
                {
                    foreach (var word in groupedWords)
                    {
                        Int32 foundWord = db.words.Where(w => word.Key == w.word).Select(w => w.key).FirstOrDefault();
                        db.wordsky.Add(new wordsky { cristinID = cristinID, key = foundWord, count = (short)word.Count() });
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message.ToString());
                    return false;
                }
                return true;
            }
        }

        /*
         * Legger til et eller flere stopp ord fra databasen
         * -    Sjekker om stoppordet eksisterer først
         */
        public bool addStopsWordsDB(List<string> stopWords)
        {
            using (var db = new dbEntities())
            {
                try
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
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message.ToString());
                    return false;
                }
                return true;
            }
        }

        /*
         * Sletter et eller flere stopp ord fra databasen
         * -    Sjekker om stoppordet eksisterer først
         */
        public bool removeStopsWordsDB(List<string> stopWords)
        {
            using (var db = new dbEntities())
            {
                try
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
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message.ToString());
                    return false;
                }
                return true;
            }
        }
    }
}
