import java.util.*; 

public class Question implements IVerifiable
{
    public int QuestionIndex;
    public HashMap<Character, ArrayList<IVerifiable>> Options
        = new HashMap<Character, ArrayList<IVerifiable>>();

    public Question(int questionNumber)
    {
        QuestionIndex = questionNumber - 1;
    }

    public Question AddOption(char option, IVerifiable condition)
    {
        var conditions = new ArrayList<IVerifiable>();
        if (Options.containsKey(option))
        {
            conditions = Options.get(option);
        }
        else
        {
            Options.put(option, conditions);
        }

        conditions.add(condition);

        return this;
    }

    public boolean IsCorrectAnswer(char[] answers)
    {
        var selectedOption = Options.get(answers[QuestionIndex]);
        for (var condition : selectedOption)
        {
            if (!condition.IsCorrectAnswer(answers)) return false;
        }

        return true;
    }
}

class AbsoluteChecking implements IVerifiable
{
    private int _QuestionIndex;
    private char _Answer;

    public AbsoluteChecking(int questionNumber, char answer)
    {
        _QuestionIndex = questionNumber - 1;
        _Answer = answer;
    }

    public boolean IsCorrectAnswer(char[] answers)
    {
        return answers[_QuestionIndex] == _Answer;
    }
}

class RelativeChecking implements IVerifiable
{
    private int _QuestionIndex;
    private int _TheOtherQuestionIndex;

    public RelativeChecking(int questionNumber, int theOtherQuestionNumber)
    {
        _QuestionIndex = questionNumber - 1;
        _TheOtherQuestionIndex = theOtherQuestionNumber - 1;
    }

    public boolean IsCorrectAnswer(char[] answers)
    {
        return answers[_QuestionIndex] == answers[_TheOtherQuestionIndex];
    }
}

class OnlyYouChecking implements IVerifiable
{
    private int _You;
    private int _Others1;
    private int _Others2;
    private int _Others3;

    public OnlyYouChecking(int you, int others1, int others2, int others3)
    {
        _You = you - 1;
        _Others1 = others1 - 1;
        _Others2 = others2 - 1;
        _Others3 = others3 - 1;
    }

    public boolean IsCorrectAnswer(char[] answers)
    {
        return answers[_You] != answers[_Others1]
               && answers[_Others1] == answers[_Others2]
               && answers[_Others2] == answers[_Others3];
    }
}

class LeastSelectedChecking implements IVerifiable
{
    private char _LeastSelectedOption;

    public LeastSelectedChecking(char leastSelectedOption)
    {
        _LeastSelectedOption = leastSelectedOption;
    }

    public boolean IsCorrectAnswer(char[] answers)
    {
        var distribution = CharArrayHelper.GetHistogram(answers);

        if(distribution.size() == 4 && distribution.getFirst().getKey() == _LeastSelectedOption)
        {
            return true;
        }

        var iterator = distribution.iterator();
        while(iterator.hasNext())
        {
            if(iterator.next().getKey() == _LeastSelectedOption)
            {
                return false;
            }
        }

        return true;
    }
}

class OptionAdjacentChecking implements IVerifiable
{
    private int _QuestionIndex;
    private int _TheOtherQuestionIndex;

    public OptionAdjacentChecking(int questionNumber, int theOtherQuestionNumber)
    {
        _QuestionIndex = questionNumber - 1;
        _TheOtherQuestionIndex = theOtherQuestionNumber - 1;
    }

    public boolean IsCorrectAnswer(char[] answers)
    {
        return Math.abs(answers[_QuestionIndex] - answers[_TheOtherQuestionIndex]) != 1;
    }
}

class XChecking implements IVerifiable
{
    private int _XIndex;

    public XChecking(int x)
    {
        _XIndex = x - 1;
    }

    public boolean IsCorrectAnswer(char[] answers)
    {
        return (answers[_XIndex] == answers[4]) != (answers[0] == answers[5]);
    }
}

class CountDifferenceChecking implements IVerifiable
{
    private int _CountDifference;

    public CountDifferenceChecking(int difference)
    {
        _CountDifference = difference;
    }

    public boolean IsCorrectAnswer(char[] answers)
    {
        var distribution = CharArrayHelper.GetHistogram(answers);
        return distribution.getLast().getValue() - distribution.getFirst().getValue() == _CountDifference;
    }
}