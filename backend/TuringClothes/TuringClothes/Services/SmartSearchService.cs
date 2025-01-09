using F23.StringSimilarity.Interfaces;
using System.Globalization;
using System.Text;
using F23.StringSimilarity;
using TuringClothes.Database;

namespace TuringClothes.Services
{
    public class SmartSearchService
    {
        private const double THRESHOLD = 0.75;
        private readonly INormalizedStringSimilarity _stringSimilarityComparer;
        private readonly MyDatabase _myDatabase;
        public SmartSearchService(MyDatabase myDatabase)
        {
            _stringSimilarityComparer = new JaroWinkler();
            _myDatabase = myDatabase;
        }

        public IEnumerable<Product> Search(string query)
        {
            IEnumerable<Product> result;
            if (string.IsNullOrWhiteSpace(query))
            {
                result = _myDatabase.Products.ToList();
            }
            else
            {
                string[] queryKeys = GetKeys(ClearText(query));
                List<Product> matches = new List<Product>();
                foreach (Product product in _myDatabase.Products)
                {
                    string[] itemKeys = GetKeys(ClearText(product.Name));
                    if (IsMatch(queryKeys, itemKeys))
                    {
                        matches.Add(product);
                    }
                }
                result = matches;
            }

            return result;
        }

        private string[] GetKeys(string query)
        {
            return query.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        private string RemoveDiacritics(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder(normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        private string ClearText(string text)
        {
            return RemoveDiacritics(text.ToLower());
        }

        private bool IsMatch(string itemKey, string queryKey)
        {
            return itemKey == queryKey
                || itemKey.Contains(queryKey)
                || _stringSimilarityComparer.Similarity(itemKey, queryKey) >= THRESHOLD;
        }

        private bool IsMatch(string[] queryKeys, string[] itemKeys)
        {
            bool isMatch = false;

            for (int i = 0; !isMatch && i < itemKeys.Length; i++)
            {
                string itemKey = itemKeys[i];

                for (int j = 0; !isMatch && j < queryKeys.Length; j++)
                {
                    string queryKey = queryKeys[j];

                    isMatch = IsMatch(itemKey, queryKey);
                }
            }

            return isMatch;
        }

    }
}
