using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpStudentDB
{
    public class RemoveStudent
    {
        public static void removeStudent(StudentDBStorage storage)
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
                    storage.removeStudent(selectedStudent.StudentId);

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
}
