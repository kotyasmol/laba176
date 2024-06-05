using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLibrary
{
    [Serializable]
    public class SchoolStudent : Person
    {
        protected string school = "school";
        protected int grade = 1;

        public string School
        {
            get { return school; }
            set
            {
                if (value == null || value == "")
                {
                    throw new ArgumentException("Ошибка: введённое значение не может являться именем!");
                }
                school = value;
            }
        }
        public int Grade
        {
            get { return grade; }
            set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException("Ошибка: значение должно быть в пределах от 1 до 5!");
                }
                grade = value;
            }
        }

        public Person BasePerson
        {
            get
            {
                return new Person(Name, Age);
            }
        }


        public SchoolStudent() { }

        public SchoolStudent(string name, int age, string school, int grade) : base(name, age)
        {
            School = school;
            this.grade = grade;
            Grade = grade;
        }

        public SchoolStudent(SchoolStudent other) : this(other.Name, other.Age, other.School , other.grade) { }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Школа: {school}");
            Console.WriteLine($"Класс: {grade}");
        }

 

    public override void Init()
        {
        base.Init();
        Console.Write("Введите название школы: ");
        School = Console.ReadLine();

        Console.Write("Введите класс (от 1 до 5): ");
        Grade = int.Parse(Console.ReadLine());
    }

        public override void RandomInit()
        {
            base.RandomInit();
            string[] schools = { "Школа №1", "Школа №3", "Лицей №1", "Лицей №2", "Политехническая школа" };
            School = schools[rnd.Next(schools.Length)];
            Grade = rnd.Next(1, 6); // Случайное число от 1 до 5 включительно
        }

        public override bool Equals(object obj)
        {
            return obj is SchoolStudent student &&
                               base.Equals(obj) &&
                               Name == student.Name &&
                               Age == student.Age &&
                               School == student.School &&
                               Grade == student.Grade;
        }

        public override int GetHashCode()
        {
            int hashCode = 17;
            hashCode = hashCode * 23 + base.GetHashCode();
            hashCode = hashCode * 23 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * 23 + Age.GetHashCode();
            hashCode = hashCode * 23 + EqualityComparer<string>.Default.GetHashCode(School);
            hashCode = hashCode * 23 + Grade.GetHashCode();
            return hashCode;
        }
    }
}
