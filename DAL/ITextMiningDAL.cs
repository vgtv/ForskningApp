using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface ITextMiningDAL
    {

       
       
        List<string> getCristinID();
        List<string> getTitles(string cristinID);
        List<List<string>> tokenizeTitles(List<string> titles);

        List<List<string>> removeStopWords(List<List<string>> tokenizedTitles);
        bool checkStopWords(List<string> tokenizedTitle);

        // Debug.WriteLine( slettet tittel )
        List<List<string>> removeLanguages(List<List<string>> tokenizedTitles);
        bool checkLanguage(List<string> title);

        List<List<string>> stemTitles(List<List<string>> tokenizedTitles);


        // (
        // 1 (1 (frode) 2 (er) 3(best)
        // 2 (1 (ok) 2 (ja) 3 (nei))
        // 3 ( 1 () 2 () 3 ())
        // )



    }
}
