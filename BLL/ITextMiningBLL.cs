using Iveonik.Stemmers;
using NetSpell.SpellChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ITextMiningBLL
    {
        List<string> getCristinID();

        List<string> getTitles(string cristinID);

        List<List<string>> tokenizeTitles(List<string> titles);

        List<List<string>> removeLanguages(List<List<string>> tokenizedTitles, Spelling spelling);

        bool isEnglish(List<string> tokenizedTitle, Spelling spelling);

        List<string> getStopWords();

        string removeSpecialCharacters(string str);

        List<List<string>> removeStopWords(List<List<string>> tokenizedTitles, List<string> stopWords);

        bool checkStopWord(string token, List<string> stopWords);

        List<List<string>> stemTitles(List<List<string>> tokenizedTitles, EnglishStemmer stemmerObj);
    
        IOrderedEnumerable<IGrouping<string, string>> groupTitles(List<List<string>> tokenizedTitles);

        bool updateWordCloud(List<string> groupedTitles);

        bool savePersonWordCloud(List<string> groupedTitles);

        List<string> getTopCloudWords();

        void addStopsWordsDB(List<string> stopWords);

        void removeStopsWordsDB(List<string> stopWords);
    }
}

