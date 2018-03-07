using Iveonik.Stemmers;
using NetSpell.SpellChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ITextMiningDAL
    {
        List<string> getCristinID();

        List<string> getTitles(string cristinID);

        List<List<string>> tokenizeTitles(List<string> titles);

        List<List<string>> removeLanguages(List<List<string>> tokenizedTitles, Spelling spelling);

        bool isEnglish(List<string> tokenizedTitle, Spelling spelling);

        List<string> getStopWords();

        string removeSpecialCharacters(string str);

        List<List<string>> removeStopWords(List<List<string>> tokenizedTitles, List<string> stopWords);

        bool isStopWord(string token, List<string> stopWords);

        List<List<string>> stemTitles(List<List<string>> tokenizedTitles, EnglishStemmer stemmerObj);

        bool isActive(List<IGrouping<string, string>> groupedWords, int countTitles);
        
        List<IGrouping<string, string>> groupTitles(List<List<string>> tokenizedTitles);

        bool saveWords(List<IGrouping<string, string>> groupedWords);

        bool saveWordCloud(List<IGrouping<string, string>> groupedWords, string cristinID);

        List<string> getTopCloudWords();

        bool addStopsWordsDB(List<string> stopWords);

        bool removeStopsWordsDB(List<string> stopWords);
    }
}
