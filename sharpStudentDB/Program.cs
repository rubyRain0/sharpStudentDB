using System;
using System.Collections.Generic;
using ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        StudentDBStorage studentDBStorage = new StudentDBStorage(new StudentContext());

        // Init DB with some data.

        Group g1 = new Group { GroupName = "ФИИТ"};
        Group g2 = new Group { GroupName = "МОАИС"};
        Group g3 = new Group { GroupName = "ПМИ" };

        Student s1 = new Student() { StudentName = "Имя1", StudentSurname="Фамилия1", Age = 18, Group = g1 };
        Student s2 = new Student() { StudentName = "Имя2", StudentSurname = "Фамилия2", Age = 17, Group = g2 };
        Student s3 = new Student() { StudentName = "Имя3", StudentSurname = "Фамилия3", Age = 17, Group = g3 };

        studentDBStorage.AddStudent(s1);
        studentDBStorage.AddStudent(s2);
        studentDBStorage.AddStudent(s3);

        // Interface.
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("Списки студентов по группам:");
            List<Group> groups = studentDBStorage.GetGroups();
            foreach (Group group in groups)
            {
                Console.WriteLine($"{group.GroupName} ({group.Students.Count} студентов)");

                List<Student> sortedStudents = group.Students;
                sortedStudents.Sort((s1, s2) => s1.StudentSurname.CompareTo(s2.StudentSurname));

                foreach (Student student in sortedStudents)
                {
                    Console.WriteLine($"- {student.StudentName} {student.StudentSurname}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Добавить студента");
            Console.WriteLine("2 - Удалить студента");
            Console.WriteLine("0 - Выйти из приложения");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    AddStudent(studentDBStorage);
                    break;
                case "2":
                    RemoveStudent(studentDBStorage);
                    break;
                case "0":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    break;
            }

            Console.WriteLine();
        }
    }
    
    private static void AddStudent(StudentDBStorage storage)
    {
        Console.WriteLine("Добавление студента:");
        Console.WriteLine("Выберите номер группы:");
        List<Group> groups = storage.GetGroups();
        for (int i = 0; i < groups.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {groups[i].GroupName}");
        }

        int groupIndex;
        if (int.TryParse(Console.ReadLine(), out groupIndex) && groupIndex >= 1 && groupIndex <= groups.Count)
        {
            Group selectedGroup = groups[groupIndex - 1];

            Console.WriteLine("Введите имя студента:");
            string studentName = Console.ReadLine();
            Console.WriteLine("Введите фамилию студента:");
            string studentSurname = Console.ReadLine();

            Student newStudent = new Student() { StudentName = studentName, StudentSurname = studentSurname, Group = selectedGroup };
            storage.AddStudent(newStudent);

            Console.WriteLine("Студент успешно добавлен.");
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Попробуйте снова.");
        }
    }

    private static void RemoveStudent(StudentDBStorage storage)
    {
        Console.WriteLine("Удаление студента:");
        Console.WriteLine("Выберите номер группы:");

        List<Group> groups = storage.GetGroups();
        for (int i = 0; i < groups.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {groups[i].GroupName}");
        }

        int groupIndex;
        if (int.TryParse(Console.ReadLine(), out groupIndex) && groupIndex >= 1 && groupIndex <= groups.Count)
        {
            Group selectedGroup = groups[groupIndex - 1];

            Console.WriteLine("Выберите номер студента для удаления:");
            List<Student> students = selectedGroup.Students;
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {students[i].StudentName} {students[i].StudentSurname}");
            }

            int studentIndex;
            if (int.TryParse(Console.ReadLine(), out studentIndex) && studentIndex >= 1 && studentIndex <= students.Count)
            {
                Student selectedStudent = students[studentIndex - 1];
                storage.removeStudent(studentIndex);

                Console.WriteLine("Студент успешно удален.");
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Попробуйте снова.");
        }
    }
}