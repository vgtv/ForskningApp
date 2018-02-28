using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    class TextMiningBLL : ITextMiningBLL
    {
        public bool addStopsWords()
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
            throw new NotImplementedException();
        }

        public List<string> getTitles(string cristinID)
        {
            throw new NotImplementedException();
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

        public List<List<string>> removeStopWords(List<List<string>> tokenizedTitles)
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
    }
}
