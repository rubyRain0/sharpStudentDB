using System;
using System.Collections.Generic;
using ConsoleApp1;
using static sharpStudentDB.AddStudent;
using static sharpStudentDB.RemoveStudent;
class Program
{   
    static void Main(string[] args)
    {
        StudentDBStorage studentDBStorage = new StudentDBStorage(new StudentContext());

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
                    addStudent(studentDBStorage);
                    break;
                case "2":
                    removeStudent(studentDBStorage);
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
}