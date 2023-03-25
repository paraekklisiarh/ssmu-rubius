namespace lesson3.Doctors;

public class CardioDoc : Doctor
{
    public CardioDoc(string name, string speciality, byte workExperience) : base(name, speciality, workExperience)
    {
    }

    private bool CardioExam()
        // Собственая функция оценки здоровья врачом-специалистом
    {
        return new Random().Next(5) == 1;
    }
    public override void Cure(Patient curingPatient)
    {
        Console.WriteLine($"{Speciality} - Приём у врача {Name}: стаж {WorkExperience}, рейтинг {Rating}");
        PatientCounter++;
        
        curingPatient.IsHealth = CardioExam();
        Console.WriteLine(curingPatient.IsHealth
            ? $"Пациент {curingPatient.Name} кардиологической патологии не имеет."
            : $"Пациент {curingPatient.Name} болен!");
    }
}