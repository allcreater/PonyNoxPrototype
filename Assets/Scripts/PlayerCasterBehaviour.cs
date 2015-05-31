using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class SpellPattern
{
    public class Word 
    {
        public string Name;
        public float Cost;
        
        public int AmountMin;
        public int AmountMax;

        public Word (string name, int min, int max, float cost)
        {
            Name = name;
            AmountMin = min;
            AmountMax = max;
            Cost = cost;
        }
    };

    public Word[] Words {get; private set;}

    //Ge Ho 2Lo La[1-3] Ma
    public SpellPattern(string textPattern)
    {
        Regex wordRegexp = new Regex(@"(\d*)?(\w+)(?:\[(\d+)-(\d+)\])?");
        
        var matches = wordRegexp.Matches(textPattern);
        Words = new Word [matches.Count];
        for (int i = 0; i < matches.Count; ++i)
        {
            var match = matches[i];

            int min, max;
            float cost;
            if (!int.TryParse(match.Groups[3].Value, out min)) min = 1;
            if (!int.TryParse(match.Groups[4].Value, out max)) max = 1;
            if (!float.TryParse(match.Groups[1].Value, out cost)) cost = 1;

            Words[i] = new Word( match.Groups[2].Value, min, max, cost);
        }
    }

    public enum MatchResult
    {
        NotMatch,
        Incomplete,
        Match
    }

    public MatchResult Match(IEnumerable<string> words)
    {
        var enumerator = words.GetEnumerator();

		if (!enumerator.MoveNext())
			return MatchResult.Incomplete;
        
        foreach (var wordTemplate in Words)
        {
            if (enumerator.Current != wordTemplate.Name)
                return MatchResult.NotMatch;
            else
            {
                int numMatches = 0;
				bool isNotTail = true;
				while (isNotTail && numMatches < wordTemplate.AmountMax && wordTemplate.Name == enumerator.Current)
				{
					isNotTail &= enumerator.MoveNext();
					if (isNotTail)
						++numMatches;
				}

                if (numMatches < wordTemplate.AmountMin-1)
					return (isNotTail) ? MatchResult.NotMatch : MatchResult.Incomplete;
            }
        }

        return MatchResult.Match;
    }
}

public class PlayerCasterBehaviour : CasterBehaviour
{
    public GameObject m_StatsContainer;

    private List<string> m_activeWords = new List<string>();

    public void ApplySpellWord(string word)
    {
		var sp = new SpellPattern("3La[1-3] Ge Ho 2Lo La[1-3] Ma");
        
		m_activeWords.Add(word);

        if (word == "Ma")
        {
            //SpellPattern.MatchResult a = sp.Match(m_activeWords);
            SpellPattern.MatchResult a = sp.Match(new List<string> { "La", "Ge", "Ho", "Lo", "La", "La", "Ma"}); //match
            SpellPattern.MatchResult b = sp.Match(new List<string> { "La", "Ge", "Ho", "Lo", "La", "La" }); //incomplete
            SpellPattern.MatchResult c = sp.Match(new List<string> { "La", "Ge", "Ho", "Lo", "La", "La", "Ma", "Ma" }); //not match
            SpellPattern.MatchResult d = sp.Match(new List<string> { "La", "Ge", "Ho", "Lo", "La", "La", "La", "La", "Ma" }); //not match
            SpellPattern.MatchResult e = sp.Match(new List<string> { "La", "Ge", "Ho", "Lo" });  //incomplete
            m_activeWords.Clear();
        }
    }
}
