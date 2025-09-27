using System;

public delegate void TestDelegate();

class Program
{
    static void Main(string[] args)
    {
        Test test = new Test("Основы C#", 120, 10, 5);
        Exam exam = new Exam("Основы C#", 120, "Ivan Ivanov", "Medium");
        FinalExam finalExam = new FinalExam("Основы C#", 120, "Ivan Ivanov", "Medium", true, 30);

        EventHandler<Exception> handler = (sender, ex) =>
        {
            Console.WriteLine($"[{sender.GetType().Name}] {ex.GetType().Name}: {ex.Message}");
        };

        test.ErrorOccurred += handler;
        exam.ErrorOccurred += handler;
        finalExam.ErrorOccurred += handler;

        TestDelegate testMethods = null;
        testMethods += test.Start;
        testMethods += exam.Start;
        testMethods += exam.Message;
        testMethods += finalExam.Start;
        testMethods += finalExam.Message;
        testMethods += finalExam.PartingWords;
        testMethods();

        test.SimulateStackOverflow();
        test.CauseArrayTypeMismatch();
        exam.CauseDivideByZero();
        exam.CauseIndexOutOfRange();
        finalExam.CauseInvalidCast();
        finalExam.SimulateOutOfMemory();
        test.CauseOverflowChecked();
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

    private event EventHandler<Exception> _errorOccurredCore;
    public virtual event EventHandler<Exception> ErrorOccurred
    {
        add { _errorOccurredCore += value; }
        remove { _errorOccurredCore -= value; }
    }

    protected virtual void OnError(Exception ex)
    {
        _errorOccurredCore?.Invoke(this, ex);
    }

    protected void SafeRun(Action action)
    {
        try { action(); }
        catch (Exception ex) { OnError(ex); }
    }

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

    public void SimulateStackOverflow()
    {
        SafeRun(() => { throw new StackOverflowException("Эмуляция переполнения стека"); });
    }

    public void CauseArrayTypeMismatch()
    {
        SafeRun(() =>
        {
            object[] arr = new string[1];
            arr[0] = new object();
        });
    }

    public void CauseOverflowChecked()
    {
        SafeRun(() =>
        {
            checked
            {
                int x = int.MaxValue;
                x = x + 1;
                Console.WriteLine(x);
            }
        });
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

    public override event EventHandler<Exception> ErrorOccurred
    {
        add { base.ErrorOccurred += value; }
        remove { base.ErrorOccurred -= value; }
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

    public void CauseDivideByZero()
    {
        SafeRun(() =>
        {
            int z = 0;
            var r = 1 / z;
            Console.WriteLine(r);
        });
    }

    public void CauseIndexOutOfRange()
    {
        SafeRun(() =>
        {
            int[] a = { 1, 2, 3 };
            Console.WriteLine(a[5]);
        });
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

    public override event EventHandler<Exception> ErrorOccurred
    {
        add { base.ErrorOccurred += value; }
        remove { base.ErrorOccurred -= value; }
    }

    protected override void OnError(Exception ex)
    {
        base.OnError(new ApplicationException("FinalExam", ex));
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
        string message = _isReady ? "готов" : "не готов";
        base.Start();
        Console.WriteLine($"Студент к выпускному экзамену {message}, он готовился {_daysLearning} дней");
    }

    public void CauseInvalidCast()
    {
        SafeRun(() =>
        {
            object o = "строка";
            int n = (int)o;
            Console.WriteLine(n);
        });
    }

    public void SimulateOutOfMemory()
    {
        SafeRun(() => { throw new OutOfMemoryException("Эмуляция нехватки памяти"); });
    }
}