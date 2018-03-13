using System;
using System.Collections.Generic;
using System.Linq;

namespace CopQuiz
{
    class Program
    {
        static void Main(string[] args)
        {
            var trials = (int)Math.Pow(4, 10);
            foreach (var i in Enumerable.Range(0, trials))
            {
                var answer = GetQuaternary(i);
                if (IsValid(answer))
                {
                    foreach (var selection in answer)
                    {
                        Console.Write(selection);
                    }
                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }

        static char[] GetQuaternary(int decimalNumber)
        {
            var answers = new char[10];
            foreach (var i in Enumerable.Range(0, 10))
            {
                var remainder = decimalNumber % 4;
                decimalNumber = decimalNumber / 4;
                answers[i] = Convert.ToChar(65 + remainder);
            }

            return answers;
        }

        static bool IsValid(char[] answers)
        {
            var questions = new List<IValidable>()
            {
                // Q2
                new Question<AbsoluteChecking>(2)
                    .AddOption('A', new AbsoluteChecking(5, 'C'))
                    .AddOption('B', new AbsoluteChecking(5, 'D'))
                    .AddOption('C', new AbsoluteChecking(5, 'A'))
                    .AddOption('D', new AbsoluteChecking(5, 'B')),

                //Q3
                new Question<OnlyYouChecking>(3)
                    .AddOption('A', new OnlyYouChecking(3, 6, 2, 4))
                    .AddOption('B', new OnlyYouChecking(6, 2, 4, 3))
                    .AddOption('C', new OnlyYouChecking(2, 4, 3, 6))
                    .AddOption('D', new OnlyYouChecking(4, 3, 6, 2)),

                //Q4
                new Question<RelativeChecking>(4)
                    .AddOption('A', new RelativeChecking(1, 5))
                    .AddOption('B', new RelativeChecking(2, 7))
                    .AddOption('C', new RelativeChecking(1, 9))
                    .AddOption('D', new RelativeChecking(6, 10)),

                //Q5
                new Question<RelativeChecking>(5)
                    .AddOption('A', new RelativeChecking(5, 8))
                    .AddOption('B', new RelativeChecking(5, 4))
                    .AddOption('C', new RelativeChecking(5, 9))
                    .AddOption('D', new RelativeChecking(5, 7)),

                //Q6
                new Question<RelativeChecking>(6)
                    .AddOption('A', new RelativeChecking(8, 2))
                    .AddOption('A', new RelativeChecking(8, 4))
                    .AddOption('B', new RelativeChecking(8, 1))
                    .AddOption('B', new RelativeChecking(8, 6))
                    .AddOption('C', new RelativeChecking(8, 3))
                    .AddOption('C', new RelativeChecking(8, 10))
                    .AddOption('D', new RelativeChecking(8, 5))
                    .AddOption('D', new RelativeChecking(8, 9)),

                //Q7
                new Question<LeastSelectedChecking>(7)
                    .AddOption('A', new LeastSelectedChecking('C'))
                    .AddOption('B', new LeastSelectedChecking('B'))
                    .AddOption('C', new LeastSelectedChecking('A'))
                    .AddOption('D', new LeastSelectedChecking('D')),

                //Q8
                new Question<OptionAdjacentChecking>(8)
                    .AddOption('A', new OptionAdjacentChecking(1, 7))
                    .AddOption('B', new OptionAdjacentChecking(1, 5))
                    .AddOption('C', new OptionAdjacentChecking(1, 2))
                    .AddOption('D', new OptionAdjacentChecking(1, 10)),

                //Q9
                new Question<XChecking>(9)
                    .AddOption('A', new XChecking(6))
                    .AddOption('B', new XChecking(10))
                    .AddOption('C', new XChecking(2))
                    .AddOption('D', new XChecking(9)),

                //Q10
                new Question<CountDifferenceChecking>(10)
                    .AddOption('A', new CountDifferenceChecking(3))
                    .AddOption('B', new CountDifferenceChecking(2))
                    .AddOption('C', new CountDifferenceChecking(4))
                    .AddOption('D', new CountDifferenceChecking(1))
            };

            foreach (var question in questions)
            {
                if (!question.CheckAnswer(answers))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public static class CharArrayDistributionHelper
    {
        public static IOrderedEnumerable<KeyValuePair<char, int>> GetDistribution(this char[] charArray)
        {
            var countEach = new Dictionary<char, int>();

            foreach (var val in charArray)
            {
                if (!countEach.ContainsKey(val))
                {
                    countEach[val] = 0;
                }

                countEach[val]++;
            }

            return countEach.OrderBy(x => x.Value);
        }
    }

    public interface IValidable
    {
        bool CheckAnswer(char[] answers);
    }

    public class Question<T> : IValidable where T : IValidable
    {
        public int QuestionIndex { get; set; }
        public Dictionary<char, List<T>> Options
            = new Dictionary<char, List<T>>();

        public Question(int questionIndex)
        {
            QuestionIndex = questionIndex;
        }

        public Question<T> AddOption(char option, T condition)
        {
            if (Options.ContainsKey(option))
            {
                Options[option].Add(condition);
            }
            else
            {
                Options[option] = new List<T>() { condition };
            }

            return this;
        }

        public bool CheckAnswer(char[] answers)
        {
            var selectedOption = Options[answers[QuestionIndex - 1]];
            foreach (var condition in selectedOption)
            {
                if (!condition.CheckAnswer(answers)) return false;
            }

            return true;
        }
    }

    public class AbsoluteChecking : IValidable
    {
        private readonly int _QuestionIndex;
        private readonly char _Answer;

        public AbsoluteChecking(int questionIndex, char answer)
        {
            _QuestionIndex = questionIndex;
            _Answer = answer;
        }

        public bool CheckAnswer(char[] answers)
        {
            return answers[_QuestionIndex - 1] == _Answer;
        }
    }

    public class RelativeChecking : IValidable
    {
        private readonly int _QuestionIndex;
        private readonly int _TheOtherQuestionIndex;

        public RelativeChecking(int questionIndex, int theOtherQuestionIndex)
        {
            _QuestionIndex = questionIndex;
            _TheOtherQuestionIndex = theOtherQuestionIndex;
        }

        public bool CheckAnswer(char[] answers)
        {
            return answers[_QuestionIndex - 1] == answers[_TheOtherQuestionIndex - 1];
        }
    }

    public class OnlyYouChecking : IValidable
    {
        private readonly int _You;
        private readonly int _Others1;
        private readonly int _Others2;
        private readonly int _Others3;

        public OnlyYouChecking(int you, int others1, int others2, int others3)
        {
            _You = you;
            _Others1 = others1;
            _Others2 = others2;
            _Others3 = others3;
        }

        public bool CheckAnswer(char[] answers)
        {
            return answers[_You - 1] != answers[_Others1 - 1]
                   && answers[_Others1 - 1] == answers[_Others2 - 1]
                   && answers[_Others2 - 1] == answers[_Others3 - 1];
        }
    }

    public class LeastSelectedChecking : IValidable
    {
        private readonly char _LeastSelectedOption;

        public LeastSelectedChecking(char leastSelectedOption)
        {
            _LeastSelectedOption = leastSelectedOption;
        }

        public bool CheckAnswer(char[] answers)
        {
            var distribution = answers.GetDistribution();
            return (distribution.Count() == 4 && distribution.First().Key == _LeastSelectedOption
                || !distribution.Any(d => d.Key == _LeastSelectedOption));
        }
    }

    public class OptionAdjacentChecking : IValidable
    {
        private readonly int _QuestionIndex;
        private readonly int _TheOtherQuestionIndex;

        public OptionAdjacentChecking(int questionIndex, int theOtherQuestionIndex)
        {
            _QuestionIndex = questionIndex;
            _TheOtherQuestionIndex = theOtherQuestionIndex;
        }

        public bool CheckAnswer(char[] answers)
        {
            return Math.Abs(answers[_QuestionIndex - 1] - answers[_TheOtherQuestionIndex - 1]) != 1;
        }
    }

    public class XChecking : IValidable
    {
        private readonly int _X;

        public XChecking(int x)
        {
            _X = x;
        }

        public bool CheckAnswer(char[] answers)
        {
            return (answers[_X - 1] == answers[4]) != (answers[0] == answers[5]);
        }
    }

    public class CountDifferenceChecking : IValidable
    {
        private readonly int _CountDifference;

        public CountDifferenceChecking(int difference)
        {
            _CountDifference = difference;
        }

        public bool CheckAnswer(char[] answers)
        {
            return answers.GetDistribution().Last().Value - answers.GetDistribution().First().Value == _CountDifference;
        }
    }
}
