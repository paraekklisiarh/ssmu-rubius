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

namespace lesson3;

public static class Program
{
    public static void Main()
    {
        // Общий список всех докторов
        var doctors = new List<Doctor>(15);

        // Создаём докторов (они должны храниться где-то в БД, но ее нет)
        void NewDoctor(string speciality, string name, byte workExperience)
        {
            // Не на каждого доктора у нас есть лицензия
            if (speciality is "Терапевт" or "Невролог" or "Кардиолог")
                doctors.Add(new Doctor(speciality, name, workExperience));
            else
                throw new ArgumentException(
                    $"Врачи специальности '{speciality}' не могут работать в нашей больнице");
        }
        
        // Ну, допустим, из БД построчно получаем
        try
        {
            NewDoctor("Невролог", "Ау И.И.", 5);
            NewDoctor("Кардиолог", "Иванов В.И.", 5);
            NewDoctor("Кардиолог", "Иванов А.И.", 3);
            NewDoctor("Терапевт", "Коновалов Г.В.", 0);
            NewDoctor("Мясник", "Коновалов В.В.", 0);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        // Все пациенты больницы
        var patients = new List<Patient>();

        // Создаём пациентов: у нас же нет регистратуры.
        void NewPatient(byte age, string name)
        {
            patients.Add(new Patient(age, name));
        }
        
        NewPatient(25, "Цы А.И.");
        NewPatient(79, "Цы И.А.");
        NewPatient(50, "J.R.R. Tolkien");

        Console.WriteLine("Пациенты больницы:");
        foreach (var item in patients) Console.WriteLine($"{item.Name} : {item.Age} : Здоров: {item.IsHealth}");

        void DoctorAppointment(Patient patient, Doctor doctor)
            // Метод приёма у врача
        {
            // Пациенты не ходят к плохим врачам!
            if (patient.HatingDoctors.Count == doctors.Count) 
            {
                Console.WriteLine($"Пациент {patient.Name} решил не посещать нашу клинику.");
                patients.Remove(patient);
                return;
            }
            while (patient.HatingDoctors.Contains(doctor.Name)) doctor = doctors[new Random().Next(doctors.Count)];

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
                var lovingDoctors = doctors.Where(d => patient.LovingDoctors.Contains(d.Name))
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
                int randomDoctor = new Random().Next(Doctor.NumberOfDoctors);
                doctor.RecordToDoctor(patient, doctors[randomDoctor].Name);
            }
        }

        // Все пациенты должны быть приняты!
        var appointmentIndex = 0;
        do
        {
            {
                // Список пациентов для сегодняшего приёма
                // Отдельный список создаётся в т.ч. потому, что внутри цикла удаляется элемент
                var currentDayPatients = patients.ToList();

                foreach (var patient in currentDayPatients)
                {
                    int randomDoc = new Random().Next(doctors.Count);
                    DoctorAppointment(patient, doctors[randomDoc]);
                }
            }
            appointmentIndex++;
        } while (appointmentIndex < 2);


        Console.WriteLine("Ежедневный отчёт о пациентах:");
        foreach (var patient in patients)
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
        foreach (var doctor in doctors)
            Console.WriteLine(
                $"{doctor.Speciality} {doctor.Name}, стаж {doctor.WorkExperience}, " +
                $"рейтинг удовлетворенности {doctor.Rating}%, " +
                $"пациентов принято {doctor.PatientCounter}");
    }
}