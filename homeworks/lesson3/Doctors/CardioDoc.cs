namespace lesson3.Doctors;

public class CardioDoc : Doctor
{
    public CardioDoc(string name, string speciality, byte workExperience) : base(name, speciality, workExperience)
    {
    }

    public override void Cure(Patient curingPatient)
    {
        bool isHealth = new Random().Next(5) == 1;
        Console.WriteLine(isHealth
            ? $"Пациент {curingPatient.Name} кардиологической патологии не имеет."
            : $"Пациент {curingPatient.Name} болен!");
    }
}