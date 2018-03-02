﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class TextMiningDAL : ITextMiningDAL
    {
        // Tenker å lage en tabell for stop ordene, så vent med denne
        // Tabellen må lages fra databasen (appdata/db.mdf), så greit at vi gjør det i plenum
        public bool addStopsWordsDB(List<string> stopWords)
        {
            throw new NotImplementedException();
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

        // Trenger tabellen
        public bool removeStopsWordsDB(List<string> stopWords)
        {
            throw new NotImplementedException();
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
