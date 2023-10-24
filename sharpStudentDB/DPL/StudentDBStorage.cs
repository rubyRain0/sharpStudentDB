using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class StudentDBStorage
    {
        private readonly StudentContext _context;
        public StudentDBStorage (StudentContext context)
        {
            _context = context;
        }
        //добавление студента
        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public List<Student> GetAllStudents()
        {
            return _context.Students.ToList<Student>();
        }
        public List<Group> GetGroups()
        {
            return _context.Groups.ToList<Group>();
        }
        public void removeStudent(int studentId)
        {
            _context.Remove(_context.Students
            .FirstOrDefault(p => p.StudentId.Equals(studentId)));
            _context.SaveChanges();        
        }

        public void EditStudent(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();

        }


    }
}
