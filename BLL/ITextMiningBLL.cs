using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    interface ITextMiningBLL
    {
        List<string> getCristinID();

        List<string> getTitles(string cristinID);

        List<List<string>> tokenizeTitles(List<string> titles);

        List<List<string>> removeStopWords(List<List<string>> tokenizedTitles);
        bool checkStopWords(List<string> tokenizedTitle);

        /*
         * Husk: Debug.WriteLine(slettet tittel)         
         */
        List<List<string>> removeLanguages(List<List<string>> tokenizedTitles);
        bool checkLanguage(List<string> title);

        List<List<string>> stemTitles(List<List<string>> tokenizedTitles);

        List<string> groupTitles(List<List<string>> tokenizedTitles);

        /*
         * Skal lagre alle nye ord i ordsky tabellen og oppdatere antall
         * Eksempel: key | word | count
         */
        bool updateWordCloud(List<string> groupedTitles);

        /*
         * Skal lage en nye kolonner med personen sine ord og antall
         * Eksempel: cristinID | key1 | count1 | key2 | count2 | key3 | count3
         */
        bool savePersonWordCloud(List<string> groupedTitles);

        List<string> getTopCloudWords();

        bool addStopsWordsDB(List<string> stopWords);

        bool removeStopsWordsDB(List<string> stopWords);
    }
}

