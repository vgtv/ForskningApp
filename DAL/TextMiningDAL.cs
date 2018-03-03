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
    class TextMiningDAL : ITextMiningDAL
    {
        public void addStopsWordsDB(List<string> stopWords)
        {
            using (var db = new dbEntities())
            {
                foreach (var newStopWord in stopWords)
                {
                    //Debug.WriteLine("Inserting stop word: " + newStopWord);
                    db.stopwords.Add(new stopwords { word = newStopWord });
                }
                db.SaveChanges();
            }
        }

        public bool checkLanguage(List<string> tokenizedTitle, Spelling spelling)
        {
            // Disse to bør også brukes i removeLanguage:
            WordDictionary oDict = new WordDictionary { DictionaryFile = "en-US.dic" };
            oDict.Initialize();

            var wordcount = 0;
            foreach (var words in tokenizedTitle)
            {
                if (spelling.TestWord(words))
                {
                    wordcount++;
                }
            }

            if (wordcount > 5)
            {
                return true;
            }

            else
            {
                return false;
            }
        }


        // Skal også finne ut hvem som blir slettet
        // Vi får ikke brukt Debug.WriteLine her så kom gjerne opp med noen 
        // Smart ideer: feks. filskriving?Okei
        public List<List<string>> removeLanguages(List<List<string>> tokenizedTitles, Spelling spelling)
        {

            using (StreamWriter writer = new StreamWriter("languagesRemoved.txt")) // Vet ikke om dette funker. Du får teste Antån.
                foreach (var titles in tokenizedTitles)
                {
                    if (checkLanguage(titles, spelling) == false)
                    {
                        writer.WriteLine(titles);
                        tokenizedTitles.Remove(titles);

                    }
                }
            return tokenizedTitles;
        }


        public bool checkStopWord(string token)
        {
            var test = false; 
            var stopWords = getStopwords();
            foreach (var words in stopWords)
            {
                if(token == words)
                {
                    test = true;     
                }
            }
            return test; 
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
                return db.author.Where(f => f.cristinID == inCristinID)
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

        public void removeStopsWordsDB(List<string> stopWords)
        {
            using (var db = new dbEntities())
            {
                foreach (var oldStopWord in stopWords)
                {
                 // Debug.WriteLine("Removing stop word: " + oldStopWord);
                 var containsStopWord = db.stopwords.Where(sw => sw.word == oldStopWord).FirstOrDefault();
                 if(containsStopWord != null) { db.stopwords.Remove(containsStopWord); }
                }
                db.SaveChanges();
            }
        }
     
        public List<List<string>> removeStopWords(List<List<string>> tokenizedTitles)
        {
            tokenizedTitles.ForEach(title => title.ForEach(word => 
            { if (checkStopWord(word)) title.Remove(word); }));

            // To forskjellige måter å gjøre det på, øverste er mer kompakt (lambda)

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

        public bool savePersonWordCloud(List<string> groupedTitles)
        {
            throw new NotImplementedException();
        }

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
            // med regex for å lete etter punktum, binnestrek osv?

            return tokenizedTitles;
            throw new NotImplementedException();
        }

        public bool updateWordCloud(List<string> groupedTitles)
        {
            throw new NotImplementedException();
        }
    }
}
