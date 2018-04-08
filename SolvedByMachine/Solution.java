import java.util.*; 

public class Solution
{
    private static ArrayList<IVerifiable> _Questions = new ArrayList<IVerifiable>(
        Arrays.asList(
            // Q2
            new Question(2)
                .AddOption('A', new AbsoluteChecking(5, 'C'))
                .AddOption('B', new AbsoluteChecking(5, 'D'))
                .AddOption('C', new AbsoluteChecking(5, 'A'))
                .AddOption('D', new AbsoluteChecking(5, 'B')),

            //Q3
            new Question(3)
                .AddOption('A', new OnlyYouChecking(3, 6, 2, 4))
                .AddOption('B', new OnlyYouChecking(6, 2, 4, 3))
                .AddOption('C', new OnlyYouChecking(2, 4, 3, 6))
                .AddOption('D', new OnlyYouChecking(4, 3, 6, 2)),

            //Q4
            new Question(4)
                .AddOption('A', new RelativeChecking(1, 5))
                .AddOption('B', new RelativeChecking(2, 7))
                .AddOption('C', new RelativeChecking(1, 9))
                .AddOption('D', new RelativeChecking(6, 10)),

            //Q5
            new Question(5)
                .AddOption('A', new RelativeChecking(5, 8))
                .AddOption('B', new RelativeChecking(5, 4))
                .AddOption('C', new RelativeChecking(5, 9))
                .AddOption('D', new RelativeChecking(5, 7)),

            //Q6
            new Question(6)
                .AddOption('A', new RelativeChecking(8, 2))
                .AddOption('A', new RelativeChecking(8, 4))
                .AddOption('B', new RelativeChecking(8, 1))
                .AddOption('B', new RelativeChecking(8, 6))
                .AddOption('C', new RelativeChecking(8, 3))
                .AddOption('C', new RelativeChecking(8, 10))
                .AddOption('D', new RelativeChecking(8, 5))
                .AddOption('D', new RelativeChecking(8, 9)),

            //Q7
            new Question(7)
                .AddOption('A', new LeastSelectedChecking('C'))
                .AddOption('B', new LeastSelectedChecking('B'))
                .AddOption('C', new LeastSelectedChecking('A'))
                .AddOption('D', new LeastSelectedChecking('D')),

            //Q8
            new Question(8)
                .AddOption('A', new OptionAdjacentChecking(1, 7))
                .AddOption('B', new OptionAdjacentChecking(1, 5))
                .AddOption('C', new OptionAdjacentChecking(1, 2))
                .AddOption('D', new OptionAdjacentChecking(1, 10)),

            //Q9
            new Question(9)
                .AddOption('A', new XChecking(6))
                .AddOption('B', new XChecking(10))
                .AddOption('C', new XChecking(2))
                .AddOption('D', new XChecking(9)),

            //Q10
            new Question(10)
                .AddOption('A', new CountDifferenceChecking(3))
                .AddOption('B', new CountDifferenceChecking(2))
                .AddOption('C', new CountDifferenceChecking(4))
                .AddOption('D', new CountDifferenceChecking(1))
        )
    );

    public static void main(String []args)
    {
        var trials = Math.pow(4, 10);
        for (var i = 0; i < trials; i++)
        {
            var answer = GetQuaternary(i);
            if (IsValid(answer))
            {
                for (var selection : answer)
                {
                    System.out.print(selection);
                }
                System.out.println();
            }
        }
    }

    static char[] GetQuaternary(int decimalNumber)
    {
        var answers = new char[10];
        for (int i = 0; i < 10; i++)
        {
            var remainder = decimalNumber % 4;
            decimalNumber = decimalNumber / 4;
            answers[i] = (char)(65 + remainder);
        }

        return answers;
    }

    static boolean IsValid(char[] answers)
    {
        for (var question : _Questions)
        {
            if (!question.IsAnswerCorrect(answers))
            {
                return false;
            }
        }

        return true;
    }
}


