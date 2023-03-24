namespace lesson3;

public class Doctor
{
    public static int NumberOfDoctors { get; private set; }
    public int PatientCounter { get; private set; }

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
    public string Speciality { get; set; }
    public byte WorkExperience { get; set; }

    public Doctor(string speciality, string name, byte workExperience)
    {
        Name = name;
        Speciality = speciality;
        WorkExperience = workExperience;
        NumberOfDoctors++;
    }

    public virtual void Cure(Patient curingPatient)
        // Метод оценки здоровья пациента
    {
        Console.WriteLine($"{Speciality} - Приём у врача {Name}: стаж {WorkExperience}, рейтинг {Rating}");
        PatientCounter++;

        bool isHealth = new Random().Next(2) == 1;

        curingPatient.IsHealth = isHealth;

        Console.WriteLine(isHealth ? $"Пациент {curingPatient.Name} здоров!" : $"Пациент {curingPatient.Name} болен!");
    }

    public void RecordToDoctor(Patient patient, string otherDocName)
        // Метод направления к другому специалисту
    {
        if (true)
            // Тут должны быть какие-нибудь взаимодействия с БД, но их нет :)
            Console.WriteLine($"Пациент {patient.Name} записан к врачу {otherDocName}");
    }
}