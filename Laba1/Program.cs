
class Program
{
    static void Main(string[] args)
    {
        Trial trials1 = new Trial("Основы C#", 120);

        Trial test = new Test("Основы C#", 120, 10, 5);
        //test.Start();

        Trial exam = new Exam("Основы C#", 120, "Ivan Ivanov", "Medium");
        //exam.Start();

        Trial finalExam = new FinalExam("Основы C#", 120, "Ivan Ivanov", "Medium", true, 30);
        //finalExam.Start();
    }
}


public class Trial
{
    protected string Name;
    protected int Duration;

    public Trial(string testObject, int duration)
    {
        Name = testObject;
        Duration = duration;
    }

    public virtual void Start()
    {
        Console.WriteLine($"Испытание по дисциплине {Name} началось");
        Console.WriteLine($"Продолжительность испытания: {Duration}");
    }
}

public class Test : Trial
{
    private int _countQuestions;
    private int _minPoints;

    public Test(string testObject, int duration, int countQuestions, int minPoints) : base(testObject, duration)
    {
        _countQuestions = countQuestions;
        _minPoints = minPoints;
    }

    public override void Start()
    {
        Console.WriteLine($"Всего вопросов: {_countQuestions}");
        Console.WriteLine($"Минимальное количество баллов: {_minPoints}");
    }
}

public class Exam : Trial
{
    private string _examinerName;
    private string _complexity;

    public Exam(string testObject, int duration, string examinerName, string complexity) : base(testObject, duration)
    {
        _examinerName = examinerName;
        _complexity = complexity;
    }

    public override void Start()
    {
        Console.WriteLine($"Имя экзаменатора: {_examinerName}");
        Console.WriteLine($"Сложность экзамена: {_complexity}");
    }
}

public class FinalExam : Exam
{
    private bool _isReady;
    private int _daysLearning;

    public FinalExam(string testObject, int duration, string examinerName, string complexity, bool isReady, int daysLearning)
        : base(testObject, duration, examinerName, complexity)
    {
        _isReady = isReady;
        _daysLearning = daysLearning;
    }

    public override void Start()
    {
        string message;
        message = _isReady ? "готов" : "не готов";
        base.Start();
        Console.WriteLine($"Студент к выпускному экзамену {message}, он готовился {_daysLearning} дней");
    }
}