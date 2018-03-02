﻿using System;
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

        bool checkStopWord(string token);

        List<List<string>> removeLanguages(List<List<string>> tokenizedTitles);

        bool checkLanguage(List<string> title);

        List<List<string>> stemTitles(List<List<string>> tokenizedTitles);

        List<string> groupTitles(List<List<string>> tokenizedTitles);

        bool updateWordCloud(List<string> groupedTitles);

        bool savePersonWordCloud(List<string> groupedTitles);

        List<string> getTopCloudWords();

        bool addStopsWordsDB(List<string> stopWords);

        bool removeStopsWordsDB(List<string> stopWords);
    }
}

