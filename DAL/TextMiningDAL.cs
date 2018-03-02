using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class TextMiningDAL : ITextMiningDAL
    {
        public void addStopsWordsDB(List<string> stopWords)
        {
            using(var db = new dbEntities())
            {
                foreach(var newStopWord in stopWords)
                {
                   // Debug.WriteLine("Inserting stop word: " + newStopWord);
                   // db.stopwords.Add(new stopwords { word = newStopWord });
                }
                db.SaveChanges();
            }
        }

        public bool checkLanguage(List<string> title)
        {
            throw new NotImplementedException();
        }

        // Vanskelig å lage før vi har en tabell
        public bool checkStopWord(string token)
        {
            throw new NotImplementedException();
        }

        public List<string> getCristinID()
        {
            using(var db = new dbEntities())
            {
                return db.person.Select(p => p.cristinID).Take(5).ToList();
            }
            // Take(5) tar kun 5 stykker til å starte med           
        }

        public List<string> getTitles(string inCristinID)
        {
            using(var db = new dbEntities())
            {                
                return db.authors.Where(f => f.cristinID == inCristinID)
                    .Select(f => (db.research.Where(fo => fo.cristinID == f.forskningsID)
                    .Select(s => s.tittel).FirstOrDefault().ToLower())).ToList();
            }
        }

        // trenger CloudWord tabellen før vi kan gjøre denne
        public List<string> getTopCloudWords()
        {
            throw new NotImplementedException();
        }

        public List<string> groupTitles(List<List<string>> tokenizedTitles)
        {
            throw new NotImplementedException();
        }

        public List<List<string>> removeLanguages(List<List<string>> tokenizedTitles)
        {
            throw new NotImplementedException();
        }

        public void removeStopsWordsDB(List<string> stopWords)
        {
            using (var db = new dbEntities())
            {
                foreach (var oldStopWord in stopWords)
                {
                   // Debug.WriteLine("Removing stop word: " + oldStopWord);

                 //   var contains = db.stopwords.Where(sw => sw.Word == oldStopWord).FirstOrDefault();
                 //   if(contains != null) { db.stopwords.Remove(contains); }
                }
                db.SaveChanges();
            }
        }
     
        public List<List<string>> removeStopWords(List<List<string>> tokenizedTitles)
        {
            foreach(var title in tokenizedTitles)
            {
                foreach(var word in title)
                {
                    if (checkStopWord(word))
                    {
                        title.Remove(word);
                    }
                }
            }

            return tokenizedTitles;
            throw new NotImplementedException();
        }

        // trenger tabellen
        public bool savePersonWordCloud(List<string> groupedTitles)
        {
            throw new NotImplementedException();
        }

        // trenger tabellen
        public bool saveWordCloud(List<string> groupedTitles)
        {
            throw new NotImplementedException();
        }

        public List<List<string>> stemTitles(List<List<string>> tokenizedTitles)
        {
            throw new NotImplementedException();
        }

        public List<List<string>> tokenizeTitles(List<string> titles)
        {
            var tokenizedTitles = new List<List<string>>();
            foreach(var title in titles)
            {
                tokenizedTitles.Add(title.Split().ToList());
            }

            // usikker på om split gjør nok, kanskje nødvendig
            // med regex for å lete etter punktum, binnestrek osv.

            return tokenizedTitles;
            throw new NotImplementedException();
        }

        // trenger tabellen
        public bool updateWordCloud(List<string> groupedTitles)
        {
            throw new NotImplementedException();
        }
    }
}
