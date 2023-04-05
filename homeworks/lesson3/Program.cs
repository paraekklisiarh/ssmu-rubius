/*
 * Спроектируем онлайн больницу. 
   Создайте классы врачей и пациентов. 
   У каждого врача и пациента должны быть набор свойств и методов (минимум 3 свойства и 2 метода) 
   У врача должен быть метод взаимодействия с пациентом. 
   В классе Program продемонстрируйте взаимодействие моделей врачей и пациентов. Проявите креатив) 
   При проектировании учитывайте принципы SOLID и DRY.  
 */

/*
 * Извините, пожалуйста. :)
 */

using lesson3.Doctors;

namespace lesson3;

public static class Program
{
    // Общий список всех докторов
    static readonly List<Doctor> Doctors = new List<Doctor>(15);

    // Создаём докторов (они должны храниться где-то в БД, но ее нет)
    static void NewDoctor(string specialityString, string name, byte workExperience)
    {
        if (!Enum.TryParse(specialityString, out Doctor.Specialities speciality))
        {
            throw new ArgumentException($"Врач специальности {specialityString} не может работать в нашей больнице");
        }
        
        switch (speciality)
        {
            case Doctor.Specialities.Невролог:
                Doctors.Add(new NeurologyDoc(name: name, speciality: speciality, workExperience));
                break;
            case Doctor.Specialities.Кардиолог:
                Doctors.Add(new CardioDoc(name, speciality, workExperience));
                break;
            case Doctor.Specialities.Терапевт:
            default:
                Doctors.Add(new Doctor(speciality, name, workExperience));
                break;
        }
    }

    // Общий список всех пациентов больницы
    static readonly List<Patient> Patients = new List<Patient>();

    // Создаём пациентов: у нас же нет регистратуры.
    private static void NewPatient(byte age, string name)
    {
        Patients.Add(new Patient(age, name));
    }

    static void DoctorAppointment(Patient patient, Doctor doctor)
        // Метод приёма у врача
    {
        // Пациенты не ходят к плохим врачам!
        if (patient.HatingDoctors.Count == Doctors.Count)
        {
            Console.WriteLine($"Пациент {patient.Name} решил не посещать нашу клинику.");
            Patients.Remove(patient);
            return;
        }

        while (patient.HatingDoctors.Contains(doctor.Name)) doctor = Doctors[new Random().Next(Doctors.Count)];

        // Пациенты предпочитают любимых врачей
        if (patient.LovingDoctors.Count > 0)
        {
            // Эту строку написал ChatGPT, я ещё не умею в лямбды, зато я придумал логику и написал второй кусок!
            // Получение списка объектов любимых докторов той же специальности
            /*
             * Assuming you want to find the doctors from the `doctors` list whose names are contained
             * in the `LovingDoctors` list, you can use LINQ to achieve this. Example:
             * 
             * var lovingDoctors = doctors.Where(d => LovingDoctors.Contains(d.Name)).ToList();
             * 
             * This code uses the `Where` extension method to filter the `doctors` list
             * based on the condition that the `Name` of each doctor is contained in the `LovingDoctors` list.
             * The result is a new list of doctors who match the condition,
             * which is stored in the `lovingDoctors` variable.
             */
            var lovingDoctors = Doctors.Where(d => patient.LovingDoctors.Contains(d.Name))
                .Where(d => d.Speciality == doctor.Speciality) // но могут предпочесть только внутри специальности
                .ToList();

            if (lovingDoctors.Count > 0 && !lovingDoctors.Contains(doctor))
            {
                Console.WriteLine($"Пациент {patient.Name} заменил врача {doctor.Name} на {lovingDoctors[0].Name}");
                doctor = lovingDoctors[0];
            }
        }

        // Приём у врача
        doctor.Cure(patient);

        if (patient.IsHealth)
        {
            // Оцениваем успешный приём
            patient.EvaluateDoctor(doctor);
        }
        else
        {
            // Необходимо продолжить лечение, если приём у врача неуспешен.
            int randomDoctor = new Random().Next(Doctors.Count);
            doctor.RecordToDoctor(patient, Doctors[randomDoctor].Name);
        }
    }

    public static void Main()
    {
        // Заполняем список докторов
        NewDoctor("Невролог", "Ау И.И.", 5);
        NewDoctor("Кардиолог", "Иванов В.И.", 5);
        NewDoctor("Кардиолог", "Иванов А.И.", 3);
        NewDoctor("Терапевт", "Коновалов Г.В.", 0);
        try
        {
            NewDoctor("Мясник", "Dr.Hannibal", 25);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        NewPatient(25, "Цы А.И.");
        NewPatient(79, "Цы И.А.");
        NewPatient(50, "J.R.R. Tolkien");

        Console.WriteLine("Пациенты больницы:");
        foreach (var item in Patients) Console.WriteLine($"{item.Name} : {item.Age} : Здоров: {item.IsHealth}");

        // Симуляция приёма пациентов
        var appointmentIndex = 2; // Магическое число итераций сиумуляции
        do
        {
            {
                // Список пациентов для сегодняшего приёма
                // Отдельный список создаётся в т.ч. потому, что внутри цикла удаляется элемент
                var currentDayPatients = Patients.ToList();

                foreach (var patient in currentDayPatients)
                {
                    var randomDoc = new Random().Next(Doctors.Count);
                    DoctorAppointment(patient, Doctors[randomDoc]);
                }
                Console.WriteLine("День окончен");
            }
            appointmentIndex--;
        } while (appointmentIndex != 0);


        Console.WriteLine("Ежедневный отчёт о пациентах:");
        foreach (var patient in Patients)
        {
            Console.WriteLine($"{patient.Name} : {patient.Age} : Здоров: {patient.IsHealth}");
            if (patient.LovingDoctors.Count > 0)
            {
                Console.Write($"    Любимые доктора: ");
                foreach (string docName in patient.LovingDoctors) Console.Write($" {docName}");
                Console.WriteLine();
            }
        }

        Console.WriteLine("Ежедневный отчёт о врачах:");
        foreach (var doctor in Doctors)
            Console.WriteLine(
                $"{doctor.Speciality} {doctor.Name}, стаж {doctor.WorkExperience}, " +
                $"рейтинг удовлетворенности {doctor.Rating}%, " +
                $"пациентов принято {doctor.PatientCounter}");
    }
}