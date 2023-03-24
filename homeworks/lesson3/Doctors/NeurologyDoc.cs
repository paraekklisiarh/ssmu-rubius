namespace lesson3.Doctors;

public class NeurologyDoc : Doctor
{
    public NeurologyDoc(string name, string speciality, byte workExperience) : base(name, speciality, workExperience)
    {
    }

    public override void Cure(Patient curingPatient)
    {
        bool isHealth = new Random().Next(3) == 1;
        Console.WriteLine(isHealth
            ? $"Нервная система пациента {curingPatient.Name} здорова." // Жаль, что не моя
            : $"Пациент {curingPatient.Name} болен!");
    }
}