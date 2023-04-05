namespace lesson3;

public class Patient
{
    public Patient(byte age, string name)
    {
        Age = age;
        Name = name;
    }

    public string Name { get; }

    public byte Age { get; }

    public bool IsHealth { get; set; }

    // Список нелюбимых докторов пациента
    public readonly List<string> HatingDoctors = new();

    private void HateDoctor(string doctorName)
        // Функция добвления нелюбимых докторов
    {
        if (HatingDoctors.Contains(doctorName)) return;
        HatingDoctors.Add(doctorName);
        Console.WriteLine($"Теперь пациент {Name} терпеть не может доктора {doctorName}");
    }

    // Список любимых докторов пациента
    public readonly List<string> LovingDoctors = new();

    private void LoveDoctor(string doctorName)
    {
        if (LovingDoctors.Contains(doctorName)) return;
        LovingDoctors.Add(doctorName);
        Console.WriteLine($"Теперь пациент {Name} предпочитает врача {doctorName}");
    }

    public void EvaluateDoctor(Doctor doctor)
        // Оцениваем врача после приёма
    {
        var score = Convert.ToByte(new Random().Next(100));
        Console.WriteLine($"Пациент {Name} доволен приёмом у врача {doctor.Name} на {score}%.");
        doctor.Rating = score;
        switch (score)
        {
            case < 50:
                HateDoctor(doctor.Name);
                break;
            case > 90:
                LoveDoctor(doctor.Name);
                break;
        }
    }
}