using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLibrary
{
    [Serializable]
    public class Student : Person
    {
        protected string university = "university";

        public string University
        {
            get { return university; }
            set
            {
                if (value == null || value == "")
                {
                    throw new ArgumentNullException("Ошибка: введённое значение не может являться названием университета!");
                }
                university = value;
            }
        }
        public Person BasePerson
        {
            get
            {
                return new Person(Name, Age);
            }
        }

        public Student() { }

        public Student(string name, int age, string university) : base(name, age)
        {
            University = university;
        }

        public Student(Student other) : this(other.Name, other.Age, other.University) { }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Вуз: {University}");
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Введите название вуза: ");
            University = GetString();
        }

        public override void RandomInit()
        {
            base.RandomInit();
            string[] universities = { "ПНИПУ", "ПГНИУ", "ВШЭ", "МГУ", "МФТИ", "СПбГУ" };
            University = universities[rnd.Next(universities.Length)];
        }

        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   base.Equals(obj) &&
                   Name == student.Name &&
                   Age == student.Age &&
                   University == student.University;
        }

        public override int GetHashCode()
        {
            int hashCode = 19;
            hashCode = hashCode * 23 + base.GetHashCode();
            hashCode = hashCode * 23 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * 23 + Age.GetHashCode();
            hashCode = hashCode * 23 + EqualityComparer<string>.Default.GetHashCode(University);
            return hashCode;
        }
    }
}
