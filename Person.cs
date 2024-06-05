using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PersonLibrary
{
    [Serializable]
    public class Person : IInit, ICloneable, IComparable<Person>
    {
        protected static Random rnd = new Random();
        protected string name = "name";
        protected int age;

        // Статическое поле для автоинкрементации Id
        private static int currentId = 0;

        public string Name
        {
            get { return name; }
            set
            {
                if (value == null || value == "")
                {
                    throw new ArgumentNullException("Ошибка: введённое значение не может являться именем!");
                }
                name = value;
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentNullException("Ошибка: введённое значение не может являться возрастом!");
                }
                age = value;
            }
        }

        [XmlElement]
        public int Id { get; set; }

        public Person()
        {
            Id = ++currentId;
        }

        public Person(string name, int age) : this()
        {
            Name = name;
            Age = age;
        }

        public Person(Person other) : this(other.Name, other.Age)
        {
            Id = other.Id;
        }

        public virtual void Show()
        {
            Console.WriteLine($"Имя: {Name}");
            Console.WriteLine($"Возраст: {Age}");
        }

        public virtual void Init()
        {
            Console.Write("Введите имя: ");
            Name = GetString();
            Console.Write("Введите возраст: ");
            Age = GetInt();
        }

        public virtual void RandomInit()
        {
            string[] names = { "John", "Alice", "Mary", "Vlad", "Andrew", "Sarah", "Boris", "July" };
            Name = names[rnd.Next(names.Length)];
            Age = rnd.Next(7, 70);
        }

        public int GetInt()
        {
            int x;
            string buf;
            bool correct;
            do
            {
                buf = Console.ReadLine();
                correct = int.TryParse(buf, out x);
                if (!correct)
                    Console.Write("Ошибка! Введите ещё раз.\n  ");
            }
            while (!correct);
            return x;
        }

        public string GetString()
        {
            string x;
            do
            {
                x = Console.ReadLine();
                if (x == null || x == "")
                    Console.Write("Ошибка: пустая строка. Введите ещё раз...\n  ");
            }
            while (x == null || x == "");
            return x;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Имя: {Name}, Возраст: {Age}";
        }

        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   Name == person.Name &&
                   Age == person.Age &&
                   Id == person.Id;
        }

        public override int GetHashCode()
        {
            int hashCode = -1360180430;
            hashCode = hashCode * -1521134295 + Name.GetHashCode();
            hashCode = hashCode * -1521134295 + Age.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            return hashCode;
        }

        public virtual int CompareTo(Person other)
        {
            return string.Compare(Name, other.Name);
        }

        public Person ShallowCopy()
        {
            return (Person)this.MemberwiseClone();
        }

        public object Clone()
        {
            return new Person(this);
        }
    }
}