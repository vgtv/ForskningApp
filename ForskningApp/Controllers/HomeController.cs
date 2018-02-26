﻿using ForskningApp;
using Iveonik.Stemmers;
using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Console = System.Console;


namespace ForskningApp.Controllers
{
    public class HomeController : Controller
    {

        private dbEntities fdbe = new dbEntities();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                fdbe.Dispose();
            }
            base.Dispose(disposing);
        }


        public List<string> getStopWords()
        {
            return new List<string>()
            {
                "a","able", "about", "above", "according", "accordingly", "across", "actually", "after",
                "afterwards", "again", "against", "ain't", "all", "allow", "allows", "almost", "alone",
                "along", "already", "also", "although", "always","am", "among", "amongst", "an", "and",
                "another", "any", "anybody", "anyhow", "anyone", "anything", "anyway", "anyways", "anywhere",
                "apart", "appear", "appreciate", "appropriate", "are", "aren't", "around", "as", "a's",
                "aside", "ask", "asking", "associated", "at", "available", "away", "awfully", "be",
                "became", "because", "become", "becomes", "becoming", "been", "before", "beforehand",
                "behind", "being", "believe", "below", "beside", "besides", "best", "better", "between",
                "beyond", "both", "brief", "but", "by", "came", "can", "cannot", "cant", "can't", "cause",
                "causes", "certain", "certainly", "changes", "clearly", "c'mon", "co", "com", "come", "comes",
                "concerning", "consequently", "consider", "considering", "contain", "containing", "contains",
                "corresponding", "could", "couldn't", "course", "c's", "currently", "definitely", "described",
                "despite", "did", "didn't", "different", "do", "does", "doesn't", "doing", "done", "don't",
                "down", "downwards", "during", "each", "edu", "eg", "eight", "either", "else", "elsewhere",
                "enough", "entirely", "especially", "et", "etc", "even", "ever", "every", "everybody",
                "everyone", "everything", "everywhere", "ex", "exactly", "example", "except", "far", "few",
                "fifth", "first", "five", "followed", "following", "follows", "for", "former", "formerly",
                "forth", "four", "from", "further", "furthermore", "get", "gets", "getting", "given",
                "gives", "go", "goes", "going", "gone", "got", "gotten", "greetings", "had", "hadn't",
                "happens", "hardly", "has", "hasn't", "have", "haven't", "having", "he", "he'd", "he'll",
                "hello", "help", "hence", "her", "here", "hereafter", "hereby", "herein", "here's",
                "hereupon", "hers", "herself", "he's", "hi", "him", "himself", "his", "hither", "hopefully",
                "how", "howbeit", "however", "how's", "i", "i'd", "ie", "if", "ignored", "i'll", "i'm",
                "immediate", "in", "inasmuch", "inc", "indeed", "indicate", "indicated", "indicates",
                "inner", "insofar", "instead", "into", "inward", "is", "isn't", "it", "it'd", "it'll",
                "its", "it's", "itself", "i've", "just", "keep", "keeps", "kept", "know", "known", "knows",
                "last", "lately", "later", "latter", "latterly", "least", "less", "lest", "let", "let's",
                "like", "liked", "likely", "little", "look", "looking", "looks", "ltd", "mainly", "many",
                "may", "maybe", "me", "mean", "meanwhile", "merely", "might", "more", "moreover", "most",
                "mostly", "much", "must", "mustn't", "my", "myself", "name", "namely", "nd", "near",
                "nearly", "necessary", "need", "needs", "neither", "never", "nevertheless", "new",
                "next", "nine", "no", "nobody", "non", "none", "noone", "nor", "normally", "not",
                "nothing", "novel", "now", "nowhere", "obviously", "of", "off", "often", "oh", "ok",
                "okay", "old", "on", "once", "one", "ones", "only", "onto", "or", "other", "others",
                "otherwise", "ought", "our", "ours", "ourselves", "out", "outside", "over", "overall",
                "own", "particular", "particularly", "per", "perhaps", "placed", "please", "plus",
                "possible", "presumably", "probably", "provides", "que", "quite", "qv", "rather",
                "rd", "re", "really", "reasonably", "regarding", "regardless", "regards", "relatively",
                "respectively", "right", "said", "same", "saw", "say", "saying", "says", "second",
                "secondly", "see", "seeing", "seem", "seemed", "seeming", "seems", "seen", "self",
                "selves", "sensible", "sent", "serious", "seriously", "seven", "several", "shall",
                "shan't", "she", "she'd", "she'll", "she's", "should", "shouldn't", "since", "six", "so",
                "some", "somebody", "somehow", "someone", "something", "sometime", "sometimes", "somewhat",
                "somewhere", "soon", "sorry", "specified", "specify", "specifying", "still", "sub", "such",
                "sup", "sure", "take", "taken", "tell", "tends", "th", "than", "thank", "thanks", "thanx",
                "that", "thats", "that's", "the", "their", "theirs", "them", "themselves", "then", "thence",
                "there", "thereafter", "thereby", "therefore", "therein", "theres", "there's", "thereupon",
                "these", "they", "they'd", "they'll", "they're", "they've", "think", "third", "this",
                "thorough", "thoroughly", "those", "though", "three", "through", "throughout", "thru",
                "thus", "to", "together", "too", "took", "toward", "towards", "tried", "tries", "truly",
                "try", "trying", "t's", "twice", "two", "un", "under", "unfortunately", "unless", "unlikely",
                "until", "unto", "up", "upon", "us", "use", "used", "useful", "uses", "using", "usually",
                "value", "various", "very", "via", "viz", "vs", "want", "wants", "was", "wasn't", "way",
                "we", "we'd", "welcome", "well", "we'll", "went", "were", "we're", "weren't", "we've",
                "what", "whatever", "what's", "when", "whence", "whenever", "when's", "where", "whereafter",
                "whereas", "whereby", "wherein", "where's", "whereupon", "wherever", "whether", "which",
                "while", "whither", "who", "whoever", "whole", "whom", "who's", "whose", "why", "why's",
                "will", "willing", "wish", "with", "within", "without", "wonder", "won't", "would",
                "wouldn't", "yes", "yet", "you", "you'd", "you'll", "your", "you're", "yours", "yourself",
                "yourselves", "you've", "zero", "based", "base"
            };
        }

        [HttpPost]
        public ActionResult Index(person innPerson)
        {
            person funnetPerson = fdbe.person.Where(p => p.fornavn == innPerson.fornavn && p.etternavn == innPerson.etternavn)
                .FirstOrDefault();

            if(funnetPerson == null)
            {
                return View();
            }

            List<string> forskninger = fdbe.forfattere.Where(f => f.cristinID == funnetPerson.cristinID)
                                       .Select(e => e.forskningsID).ToList();
            var stopWords = getStopWords();

            List<string> listeMedTitler = new List<string>();
            foreach (var fID in forskninger)
            {
                string t = fdbe.forskning.Where(f => f.cristinID == fID)
                            .Select(f => f.tittel).FirstOrDefault().ToLower();

                var output = string.Join(" ", t.Split().Where(ord => !stopWords.Contains(ord)));
                listeMedTitler.Add(output);
            }


            WordDictionary oDict = new WordDictionary { DictionaryFile = "en-US.dic" };
            oDict.Initialize();
            Spelling oSpell = new Spelling { Dictionary = oDict };
            EnglishStemmer stemmer = new EnglishStemmer();

            foreach (var tittel in listeMedTitler.ToArray())
            {
                var tmp = tittel.Split();

                var antallEngelskeOrd = tmp.Count(ord => oSpell.TestWord(ord));

                if (antallEngelskeOrd > 4)
                {
                    //ok.

                    tmp.ToList().ForEach(t => stemmer.Stem(t));
                    tittel.Replace(tmp.ToString(), "");
                }
                else
                {
                    listeMedTitler.Remove(tittel);
                }
            }

            List<string> gruppert = new List<string>();
            foreach (var tittel in listeMedTitler.ToArray())
            {
                var tmp = tittel.Split();
                foreach (var t in tmp)
                {
                    gruppert.Add(t);
                }
            }


            var group = gruppert.GroupBy(i => i).OrderByDescending(g => g.Count());

            ViewBag.Liste = "<table class='table table-hover'><tr><th>Ord</th><th>Antall</th></tr>";
            foreach (var g in group)
            {
                ViewBag.Liste += "<tr><td>" + g.Key + "</td>" + "<td>" + g.Count() + "</td></tr>";
            }

            ViewBag.Liste += "</table>";

            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
