namespace lesson3.Doctors;

public class NeurologyDoc : Doctor
{
    public NeurologyDoc(string name, Specialities speciality, byte workExperience) : base(speciality, name,
        workExperience)
    {
    }

    private bool NeurologyExam()
        // Собственая функция оценки здоровья врачом-специалистом
    {
        return new Random().Next(3) == 1;
    }

    public override void Cure(Patient curingPatient)
    {
        Console.WriteLine($"{Speciality} - Приём у врача {Name}: стаж {WorkExperience}, рейтинг {Rating}");
        PatientCounter++;

        curingPatient.IsHealth = NeurologyExam();
        Console.WriteLine(curingPatient.IsHealth
            ? $"Нервная система пациента {curingPatient.Name} здорова." // Жаль, что не моя
            : $"Пациент {curingPatient.Name} болен!");
    }
}