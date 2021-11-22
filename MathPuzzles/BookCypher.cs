using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MathPuzzles
{
    public class BookCypher
    {
        public IEnumerable<string> GetOptions(ICollection<string> pages, int[] numbers)
        {
            string val;

            try
            {
                val = LetterPositions(pages, numbers, true);
            }
            catch
            {
                val = null;
            }
            if (val != null)
                yield return val;

            try
            {
                val = LetterPositions(pages, numbers, false);
            }
            catch
            {
                val = null;
            }
            if (val != null)
                yield return val;

            try
            {
                val = WordPositions(pages, numbers);
            }
            catch
            {
                val = null;
            }
            if (val != null)
                yield return val;

            try
            {
                val = WordFirstLetterPositions(pages, numbers);
            }
            catch
            {
                val = null;
            }
            if (val != null)
                yield return val;

            try
            {
                val = WordLetterPositions(pages, numbers);
            }
            catch
            {
                val = null;
            }
            if (val != null)
                yield return val;

            try
            {
                val = PageWordPositions(pages, numbers);
            }
            catch
            {
                val = null;
            }
            if (val != null)
                yield return val;

            try
            {
                val = PageWordFirstLetterPositions(pages, numbers);
            }
            catch
            {
                val = null;
            }
            if (val != null)
                yield return val;

            try
            {
                val = PageLetterPositions(pages, numbers, true);
            }
            catch
            {
                val = null;
            }
            if (val != null)
                yield return val;

            try
            {
                val = PageLetterPositions(pages, numbers, false);
            }
            catch
            {
                val = null;
            }
            if (val != null)
                yield return val;

            try
            {
                val = PageWordLetterPositions(pages, numbers);
            }
            catch
            {
                val = null;
            }
            if (val != null)
                yield return val;
        }

        /// <summary>
        /// Numbers represent letter positions in the text.
        /// </summary>
        private string LetterPositions(ICollection<string> pages, int[] numbers, bool onlyLetters)
        {
            var convertedPages = onlyLetters
                ? pages
                    .Select(page => Regex.Replace(page, @"[^a-z9]", string.Empty, RegexOptions.IgnoreCase))
                    .ToArray()
                : pages.ToArray();
            var pageStarts = convertedPages
                .Select(page => page.Length)
                .Aggregate(new List<int>(new[] { 0 }), (agg, curr) => { agg.Add(agg.Last() + curr); return agg; });
            var chars = numbers
                .Select(letterNum =>
                {
                    var pageIdx = pageStarts.Where(start => start < letterNum).Count() - 1;
                    return convertedPages[pageIdx][letterNum - pageStarts[pageIdx] - 1];
                })
                .ToArray();
            return new string(chars);
        }

        /// <summary>
        /// Numbers represent word positions in the text.
        /// </summary>
        private string WordPositions(ICollection<string> pages, int[] numbers)
        {
            var convertedPages = pages
                .Select(page => page.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                .ToArray();
            var pageStarts = convertedPages
                .Select(page => page.Length)
                .Aggregate(new List<int>(new[] { 0 }), (agg, curr) => { agg.Add(agg.Last() + curr); return agg; });
            var words = numbers
                .Select(wordNum =>
                {
                    var pageIdx = pageStarts.Where(start => start < wordNum).Count() - 1;
                    return convertedPages[pageIdx][wordNum - pageStarts[pageIdx] - 1];
                })
                .ToArray();
            return string.Join(" ", words);
        }

        /// <summary>
        /// Numbers represent word positions in the text.
        /// </summary>
        private string WordFirstLetterPositions(ICollection<string> pages, int[] numbers)
        {
            var convertedPages = pages
                .Select(page => page.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                .ToArray();
            var pageStarts = convertedPages
                .Select(page => page.Length)
                .Aggregate(new List<int>(new[] { 0 }), (agg, curr) => { agg.Add(agg.Last() + curr); return agg; });
            var chars = numbers
                .Select(wordNum =>
                {
                    var pageIdx = pageStarts.Where(start => start < wordNum).Count() - 1;
                    return convertedPages[pageIdx][wordNum - pageStarts[pageIdx] - 1][0];
                })
                .ToArray();
            return new string(chars);
        }

        /// <summary>
        /// Numbers represent word, then letter positions in the text.
        /// </summary>
        private string WordLetterPositions(ICollection<string> pages, int[] numbers)
        {
            if (numbers.Length % 2 != 0)
                return null;

            var convertedPages = pages
                .Select(page => page.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                .ToArray();
            var pageStarts = convertedPages
                .Select(page => page.Length)
                .Aggregate(new List<int>(new[] { 0 }), (agg, curr) => { agg.Add(agg.Last() + curr); return agg; });
            var chars = Enumerable.Range(0, numbers.Length / 2)
                .Select(idx =>
                {
                    var wordNum = numbers[idx * 2];
                    var letterNum = numbers[idx * 2 + 1];
                    var pageIdx = pageStarts.Where(start => start < wordNum).Count() - 1;
                    var word = convertedPages[pageIdx][wordNum - pageStarts[pageIdx] - 1];
                    return word[letterNum - 1];
                })
                .ToArray();
            return new string(chars);
        }

        /// <summary>
        /// Numbers represent page/letter positions in the text.
        /// </summary>
        private string PageLetterPositions(ICollection<string> pages, int[] numbers, bool onlyLetters)
        {
            if (numbers.Length % 2 != 0)
                return null;

            var convertedPages = onlyLetters
                ? pages
                    .Select(page => Regex.Replace(page, @"[^a-z9]", string.Empty, RegexOptions.IgnoreCase))
                    .ToArray()
                : pages.ToArray();
            var chars = Enumerable.Range(0, numbers.Length / 2)
                .Select(idx =>
                {
                    var pageNum = numbers[idx * 2];
                    var letterNum = numbers[idx * 2 + 1];
                    return convertedPages[pageNum][letterNum];
                })
                .ToArray();
            return new string(chars);
        }

        /// <summary>
        /// Numbers represent page/word positions in the text.
        /// </summary>
        private string PageWordPositions(ICollection<string> pages, int[] numbers)
        {
            if (numbers.Length % 2 != 0)
                return null;

            var convertedPages = pages
                .Select(page => page.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                .ToArray();
            var words = Enumerable.Range(0, numbers.Length / 2)
                .Select(idx =>
                {
                    var pageNum = numbers[idx * 2];
                    var wordNum = numbers[idx * 2 + 1];
                    return convertedPages[pageNum][wordNum];
                })
                .ToArray();
            return string.Join(" ", words);
        }

        /// <summary>
        /// Numbers represent page/word positions in the text.
        /// </summary>
        private string PageWordFirstLetterPositions(ICollection<string> pages, int[] numbers)
        {
            if (numbers.Length % 2 != 0)
                return null;

            var convertedPages = pages
                .Select(page => page.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                .ToArray();
            var chars = Enumerable.Range(0, numbers.Length / 2)
                .Select(idx =>
                {
                    var pageNum = numbers[idx * 2];
                    var wordNum = numbers[idx * 2 + 1];
                    return convertedPages[pageNum][wordNum][0];
                })
                .ToArray();
            return new string(chars);
        }

        /// <summary>
        /// Numbers represent page/word/letter positions in the text.
        /// </summary>
        private string PageWordLetterPositions(ICollection<string> pages, int[] numbers)
        {
            if (numbers.Length % 3 != 0)
                return null;

            var convertedPages = pages
                .Select(page => page.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                .ToArray();
            var pageStarts = convertedPages
                .Select(page => page.Length)
                .Aggregate(new List<int>(new[] { 0 }), (agg, curr) => { agg.Add(agg.Last() + curr); return agg; });
            var chars = Enumerable.Range(0, numbers.Length / 3)
                .Select(idx =>
                {
                    var pageNum = numbers[idx * 2];
                    var wordNum = numbers[idx * 2 + 1];
                    var letterNum = numbers[idx * 2 + 2];
                    return convertedPages[pageNum][wordNum][letterNum];
                })
                .ToArray();
            return new string(chars);
        }

    }
}