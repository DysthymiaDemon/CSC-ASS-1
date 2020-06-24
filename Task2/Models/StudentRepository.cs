using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task2.Models
{
    public class StudentRepository : IStudentRepository
    {
        private List<Student> students = new List<Student>();
        private int _nextId = 1;

        public StudentRepository()
        {
            Add(new Student { Name = "Philip", Year = 3, Course = "Diploma in Information Technology", Email = "philip@gmail.com" });
            Add(new Student { Name = "Ameen", Year = 1, Course = "Diploma in Information Technology", Email = "ameen@gmail.com" });
            Add(new Student { Name = "Darius", Year = 2, Course = "Diploma in Information Technology", Email = "darius@gmail.com" });
            Add(new Student { Name = "Gary", Year = 3, Course = "Diploma in Business Information Technology", Email = "gary@gmail.com" });
            Add(new Student { Name = "Zion", Year = 2, Course = "Diploma in Business Information Technology", Email = "zion@gmail.com" });
        }
        public IEnumerable<Student> GetAll()
        {
            return students;
        }

        public Student Get(int id)
        {
            return students.Find(s => s.Id == id);
        }

        public Student Add(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException("student is null");
            }
            student.Id = _nextId++;
            students.Add(student);
            return student;
        }

        public void Remove(int id)
        {
            students.RemoveAll(s => s.Id == id);
        }

        public bool Update(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException("student is null");
            }
            int index = students.FindIndex(s => s.Id == student.Id);
            if (index == -1)
            {
                return false;
            }
            students.RemoveAt(index);
            students.Add(student);
            return true;
        }

    }
}