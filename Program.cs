using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sec8
{
    internal class Program
    {
        public class Student
        {
            public string Name { get; }
            public string Class { get; }

            public Student(string name, string className)
            {
                Name = name;
                Class = className;
            }
        }

        static void Main(string[] args)
        {
            List<Student> students = ReadStudentData("student_data.txt");

            if (students.Count == 0)
            {
                Console.WriteLine("No student data found.");
                return;
            }

            Console.WriteLine("Sorted Student Data (by name):");
            foreach (Student student in students)
            {
                Console.WriteLine($"{student.Name}, Class {student.Class}");
            }

            Console.Write("\nEnter the name of the student to search: ");
            string searchName = Console.ReadLine();

            Student foundStudent = students.FirstOrDefault(s => s.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));
            if (foundStudent != null)
            {
                Console.WriteLine($"Student found: {foundStudent.Name}, {foundStudent.Class}th Class");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
            Console.ReadKey();
        }

        static List<Student> ReadStudentData(string filename)
        {
            List<Student> students = new List<Student>();

            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            string name = parts[0].Trim();
                            string className = parts[1].Trim();
                            students.Add(new Student(name, className));
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found. Make sure the 'student_data.txt' file exists.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while reading the file: " + ex.Message);
            }

            return students.OrderBy(s => s.Name).ToList();
        }
    }
}
