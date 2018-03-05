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
        private ITextMiningDAL textMining;

        public TextMiningBLL()
        {
            textMining = new TextMiningDAL();
        }

        public List<List<string>> tokenizeTitles(List<string> titles)
        {
            return textMining.tokenizeTitles(titles);
        }

        public void addStopsWordsDB(List<string> stopWords)
        {
            textMining.addStopsWordsDB(stopWords);
        }

        public bool isEnglish(List<string> tokenizedTitle, Spelling spelling)
        {
            return textMining.isEnglish(tokenizedTitle, spelling);
        }

        public bool isStopWord(string token, List<string> stopWords)
        {
            return textMining.isStopWord(token, stopWords);
        }

        public List<string> getCristinID()
        {
            return textMining.getCristinID();
        }

        public List<string> getTitles(string cristinID)
        {
            return textMining.getTitles(cristinID);
        }

        public IOrderedEnumerable<IGrouping<string, string>> groupTitles(List<List<string>> tokenizedTitles)
        {
            return textMining.groupTitles(tokenizedTitles);
        }

        public List<List<string>> removeLanguages(List<List<string>> tokenizedTitles, Spelling spelling)
        {
            return textMining.removeLanguages(tokenizedTitles, spelling);
        }

        public void removeStopsWordsDB(List<string> stopWords)
        {
            textMining.removeStopsWordsDB(stopWords);
        }

        public List<List<string>> removeStopWords(List<List<string>> tokenizedTitles, List<string> stopWords)
        {
            return textMining.removeStopWords(tokenizedTitles, stopWords);
        }

        public List<List<string>> stemTitles(List<List<string>> tokenizedTitles, EnglishStemmer stemmerObj)
        {
            return textMining.stemTitles(tokenizedTitles, stemmerObj);
        }

        public List<string> getTopCloudWords()
        {
            return textMining.getTopCloudWords();
        }

        public List<string> getStopWords()
        {
            return textMining.getStopWords();
        }

        public string removeSpecialCharacters(string str)
        {
            return textMining.removeSpecialCharacters(str);
        }

        public bool saveWordCloud(IOrderedEnumerable<IGrouping<string, string>> groupedWords)
        {
            return textMining.saveWordCloud(groupedWords);
        }
    }
}
