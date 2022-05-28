
using System.Collections.Generic;

namespace MyApp
{
    public class WordCloudCalculator
    {
        public IReadOnlyDictionary<string, int> Calculate(IEnumerable<Person> persons)
        {
            var words = new Dictionary<string, int>();

            foreach (var person in persons)
            {
                UpdateWord(words, person.FirstName);
                if (person.SecondName != null)
                {
                    UpdateWord(words, person.SecondName);
                }
                UpdateWord(words, person.LastName);
            }

            return words;
        }

        private void UpdateWord(Dictionary<string, int> words, string name)
        {
            var normalizedName = name.ToLower();
            if (words.ContainsKey(normalizedName))
            {
                words[normalizedName] += 1;
            }
            else
            {
                words[normalizedName] = 1;
            }
        }
    }
}