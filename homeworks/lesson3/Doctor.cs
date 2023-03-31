namespace lesson3;

public class Doctor
{
    // Счетчик принятых пациентов
    public int PatientCounter { get; protected set; }

    // Список, в котором хранятся все оценки доктора
    // Rider сказал сделать его readonly :|
    private readonly List<byte> _rating = new();

    public byte Rating
    {
        get
        {
            if (_rating.Count == 0) return 0;
            int score = _rating.Sum(Convert.ToInt32);
            return Convert.ToByte(score / _rating.Count);
        }
        set => _rating.Add(value);
    }

    public string Name { get; init; }

    public enum Specialities
    {
        Терапевт,
        Невролог,
        Кардиолог
    }

    public Specialities Speciality { get; set; }
    public byte WorkExperience { get; set; }


    public Doctor(Specialities speciality, string name, byte workExperience)
    {
        Name = name;
        Speciality = speciality;
        WorkExperience = workExperience;
    }

    public virtual void Cure(Patient curingPatient)
        // Метод оценки здоровья пациента
    {
        Console.WriteLine($"{Speciality} - Приём у врача {Name}: стаж {WorkExperience}, рейтинг {Rating}");
        PatientCounter++;

        curingPatient.IsHealth = new Random().Next(2) == 1;

        Console.WriteLine(curingPatient.IsHealth
            ? $"Пациент {curingPatient.Name} здоров!"
            : $"Пациент {curingPatient.Name} болен!");
    }

    public void RecordToDoctor(Patient patient, string otherDocName)
        // Метод направления к другому специалисту
    {
        // Тут должны быть какие-нибудь взаимодействия с БД, но их нет :)
        Console.WriteLine($"{Speciality} {Name}: Пациент {patient.Name} записан к врачу {otherDocName}");
    }
}