using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class TextMiningDAL : ITextMiningDAL
    {
        public bool addStopsWordsDB(List<string> stopWords)
        {
            throw new NotImplementedException();
        }

        public bool checkLanguage(List<string> title)
        {
            throw new NotImplementedException();
        }

        public bool checkStopWords(List<string> tokenizedTitle)
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
                return db.forfattere.Where(f => f.cristinID == inCristinID).
                    Select(f => (db.forskning.Where(fo => fo.cristinID == f.forskningsID).Select(s => s.tittel).FirstOrDefault())).ToList();
            }
        }

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

        public bool removeStopsWordsDB(List<string> stopWords)
        {
            throw new NotImplementedException();
        }

        public List<List<string>> removeStopWords(List<List<string>> tokenizedTitles)
        {
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

            throw new NotImplementedException();
        }

        public bool updateWordCloud(List<string> groupedTitles)
        {
            throw new NotImplementedException();
        }
    }
}
