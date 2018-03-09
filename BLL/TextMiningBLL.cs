using DAL;
using Iveonik.Stemmers;
using NetSpell.SpellChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TextMiningBLL : ITextMiningBLL
    {
        private ITextMiningDAL _textMiningDAL;

        public TextMiningBLL()
        {
            _textMiningDAL = new TextMiningDAL();
        }

        public List<List<string>> tokenizeTitles(List<string> titles)
        {
            return _textMiningDAL.tokenizeTitles(titles);
        }

        public bool addStopsWordsDB(List<string> stopWords)
        {
            return _textMiningDAL.addStopsWordsDB(stopWords);
        }

        public bool isEnglish(List<string> tokenizedTitle, Spelling spelling)
        {
            return _textMiningDAL.isEnglish(tokenizedTitle, spelling);
        }

        public bool isStopWord(string token, List<string> stopWords)
        {
            return _textMiningDAL.isStopWord(token, stopWords);
        }

        public List<string> getCristinID()
        {
            return _textMiningDAL.getCristinID();
        }

        public List<string> getTitles(string cristinID)
        {
            return _textMiningDAL.getTitles(cristinID);
        }

        public List<IGrouping<string, string>> groupTitles(List<List<string>> tokenizedTitles)
        {
            return _textMiningDAL.groupTitles(tokenizedTitles);
        }

        public List<List<string>> removeLanguages(List<List<string>> tokenizedTitles, Spelling spelling)
        {
            return _textMiningDAL.removeLanguages(tokenizedTitles, spelling);
        }

        public bool removeStopsWordsDB(List<string> stopWords)
        {
            return _textMiningDAL.removeStopsWordsDB(stopWords);
        }

        public List<List<string>> removeStopWords(List<List<string>> tokenizedTitles, List<string> stopWords)
        {
            return _textMiningDAL.removeStopWords(tokenizedTitles, stopWords);
        }

        public List<List<string>> stemTitles(List<List<string>> tokenizedTitles, EnglishStemmer stemmerObj)
        {
            return _textMiningDAL.stemTitles(tokenizedTitles, stemmerObj);
        }

        public List<string> getTopCloudWords()
        {
            return _textMiningDAL.getTopCloudWords();
        }

        public List<string> getStopWords()
        {
            return _textMiningDAL.getStopWords();
        }

        public string removeSpecialCharacters(string str)
        {
            return _textMiningDAL.removeSpecialCharacters(str);
        }

        public bool saveWords(List<IGrouping<string, string>> groupedWords)
        {
            return _textMiningDAL.saveWords(groupedWords);
        }

        public bool saveWordCloud(List<IGrouping<string, string>> groupedWords, string cristinID, short totalTitles)
        {
            return _textMiningDAL.saveWordCloud(groupedWords, cristinID, totalTitles);
        }
        public bool isActive(List<List<string>> tokenizedTitles)
        {
            return _textMiningDAL.isActive(tokenizedTitles);
        }
    }
}
