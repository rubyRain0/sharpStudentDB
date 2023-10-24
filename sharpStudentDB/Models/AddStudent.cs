using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpStudentDB
{
    public class AddStudent
    {
        public static void addStudent(StudentDBStorage storage)
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

                string studentName = PromptValidInput("Введите имя студента:", IsValidName);
                string studentSurname = PromptValidInput("Введите фамилию студента:", IsValidName);

                Student newStudent = new Student() { StudentName = studentName, StudentSurname = studentSurname, Group = selectedGroup };
                storage.AddStudent(newStudent);

                Console.WriteLine("Студент успешно добавлен.");
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
            }
        }
        private static string PromptValidInput(string promptMessage, Func<string, bool> isValidInput)
        {
            string input;
            do
            {
                Console.WriteLine(promptMessage);
                input = Console.ReadLine().Trim();
            } while (!isValidInput(input));

            return input;
        }
        private static bool IsValidName(string name)
        {

            System.Text.RegularExpressions.Regex regex = 
                new System.Text.RegularExpressions.Regex("^[a-zA-Z]+$");

            return name.Length > 0 && regex.IsMatch(name);
        }
    }
}
