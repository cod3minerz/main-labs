public delegate void TestDelegate();

class Program
{
    static void Main(string[] args)
    {
        Test test = new Test("Основы C#", 120, 10, 5);
        Exam exam = new Exam("Основы C#", 120, "Ivan Ivanov", "Medium");
        FinalExam finalExam = new FinalExam("Основы C#", 120, "Ivan Ivanov", "Medium", true, 30);

        TestDelegate testMethods = null;
        testMethods += test.Start;
        testMethods += exam.Start;
        testMethods += exam.Message;
        testMethods += finalExam.Start;
        testMethods += finalExam.Message;
        testMethods += finalExam.PartingWords;

        testMethods();

    }
}

public interface IStart
{
    void Start();
}

public interface INotification
{
    void Message();
}

public interface IPartingWords
{
    void PartingWords();
}


public abstract class Trial : IStart
{
    protected string Name;
    protected int Duration;

    public Trial(string testObject, int duration)
    {
        Name = testObject;
        Duration = duration;
    }

    public abstract void PrintDate();

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

    public override void PrintDate()
    {
        Console.WriteLine("Тест завтра");
    }

    public void FinishTest()
    {
        Console.WriteLine("Написание теста подходит к концу");
    }

    public override void Start()
    {
        base.Start();
        Console.WriteLine($"Всего вопросов: {_countQuestions}");
        Console.WriteLine($"Минимальное количество баллов: {_minPoints}");
    }
}

public class Exam : Trial, INotification
{
    private string _examinerName;
    private string _complexity;

    public Exam(string testObject, int duration, string examinerName, string complexity) : base(testObject, duration)
    {
        _examinerName = examinerName;
        _complexity = complexity;
    }

    public virtual void Message()
    {
        Console.WriteLine("Экзамен скоро начнется!");
    }
    
    public override void PrintDate()
    {
        Console.WriteLine("Экзамен будет через неделю");
    }

    
    public override void Start()
    {
        Console.WriteLine($"Имя экзаменатора: {_examinerName}");
        Console.WriteLine($"Сложность экзамена: {_complexity}");
    }
}

public class FinalExam : Exam, IPartingWords
{
    private bool _isReady;
    private int _daysLearning;

    public FinalExam(string testObject, int duration, string examinerName, string complexity, bool isReady, int daysLearning)
        : base(testObject, duration, examinerName, complexity)
    {
        _isReady = isReady;
        _daysLearning = daysLearning;
    }
    
    public override void Message()
    {
        Console.WriteLine("Выпускной экзамен скоро начнется!");
    }
    
    public override void PrintDate()
    {
        Console.WriteLine("Выпускной экзамен будет через месяц");
    }

    public void PartingWords()
    {
        Console.WriteLine("Напутствие: после выпускного экзамена ты свободен");
    }

    public override void Start()
    {
        string message;
        message = _isReady ? "готов" : "не готов";
        base.Start();
        Console.WriteLine($"Студент к выпускному экзамену {message}, он готовился {_daysLearning} дней");
    }
}