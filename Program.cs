using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using PersonLibrary;

namespace LaboratoryWork_12
{
    internal class Program
    {
        static void Menu()
        {
            int choice;
            Tree<Person> myCollection = new Tree<Person>();
            do
            {
                Console.Clear();
                Console.Write("0. Выход\n" +
                    "1. Добавления одного или нескольких элементов в коллекцию\n" +
                    "2. Удаление элемента из коллекции\n" +
                    "3. Поиск элемента по значению\n" +
                    "4. Перебор коллеции циклом foreach\n" +
                    "5. Глубокое клонирование коллекции (вместе с элементами)\n" +
                    "6. Поверхностное копирование\n" +
                    "7. Удаление коллекции из памяти\n");
                choice = CustomFunctions.InputInteger("Введите число: ");
                switch (choice)
                {
                    case 0:
                        {
                            Console.WriteLine("Завершение работы программы");
                            break;
                        }
                    case 1: // добавление
                        {
                            Console.Clear();
                            int countElements = CustomFunctions.InputInteger("Введите количество добавляемых элементов");
                            CustomFunctions.CheckNumber(1, 1000, ref countElements);
                            for (int i = 0; i < countElements; ++i)
                            {
                                Person address = new Person();
                                address.RandomInit();
                                myCollection.Add(address);
                            }
                            CustomFunctions.Pause();
                            break;
                        }
                    case 2: // удаление
                        {
                            Console.Clear();
                            Console.WriteLine("Для демонстрации удаления, сначала добавим объект производного класса и выведем дерево, а затем удалим его и снова выведем дерево:");
                            Person address = new Person();
                            address.RandomInit();
                            Console.WriteLine("\n Новый объект");
                            address.Show();

                            myCollection.Add(address);
                            Console.WriteLine("\n Дерево после добавления");
                            myCollection.ShowTree();

                            if (myCollection.Remove(address))
                                Console.WriteLine("Объект найден!");
                            Console.WriteLine("\n Дерево после удаления");
                            myCollection.ShowTree();

                            Console.WriteLine("\nПопытка удалить объект второй раз:");
                            if (!myCollection.Remove(address))
                                Console.WriteLine("Объекта не существует!");
                            CustomFunctions.Pause();
                            break;
                        }
                    case 3: // поиск
                        {
                            Console.Clear();
                            Console.WriteLine("Для демонстрации поиска, сначала добавим объект производного класса и выведем дерево, а затем продемонстрируем поиск:");
                            Person address = new Person();
                            address.RandomInit();
                            Console.WriteLine("\n Новый объект");
                            address.Show();

                            myCollection.Add(address);
                            Console.WriteLine("\n Дерево после добавления");
                            myCollection.ShowTree();

                            if (myCollection.Contains(address))
                                Console.WriteLine("Объект найден!\n");

                            Console.WriteLine("Удалим объект и попробуем снова его найти:");
                            if (myCollection.Remove(address))
                            {
                                Console.WriteLine("\n Дерево после удаления");
                                myCollection.ShowTree();
                            }

                            Console.WriteLine("\nПоиск:");
                            if (!myCollection.Contains(address))
                                Console.WriteLine("Объект не найден!");
                            CustomFunctions.Pause();
                            break;
                        }
                    case 4: // вывод коллекции
                        {
                            Console.Clear();
                            myCollection.ShowTree();
                            CustomFunctions.Pause();
                            break;
                        }
                    case 5: // глубокое копирование
                        {
                            Console.Clear();
                            if (myCollection.Count != 0)
                            {
                                Tree<Person> deepCopyCollection = myCollection.Clone();

                                Console.WriteLine("Демонстрация: сначала копируем -> затем изменяем элемент -> выводим коллекцию и клон коллекции");

                                myCollection.ElementAt(0).Show();
                                myCollection.ElementAt(0).Age = 0;
                                myCollection.ElementAt(0).Show();

                                Console.WriteLine("\nИсходная коллекция:");
                                myCollection.ShowTree();
                                Console.WriteLine("\nКлон коллекции:");
                                deepCopyCollection.ShowTree();

                            }
                            CustomFunctions.Pause();
                            break;
                        }
                    case 6: // поверхностное копирование
                        {
                            Console.Clear();
                            Tree<Person> shallowCopyCollection = myCollection.ShallowCopy();

                            Console.WriteLine("Демонстрация: сначала копируем -> затем добавляем в исходную коллекцию новый элемент -> выводим коллекцию и клон коллекции");
                            Person address = new Person();
                            address.RandomInit();
                            myCollection.Add(address);
                            Console.WriteLine("Добавленный элемент:");
                            address.Show();

                            Console.WriteLine("\nИсходная коллекция:");
                            myCollection.ShowTree();
                            Console.WriteLine("\nПоверхностная копия коллекции:");
                            shallowCopyCollection.ShowTree();

                            if (myCollection.Remove(address))
                                Console.WriteLine("\n Объект удалён");
                            CustomFunctions.Pause();
                            break;
                        }
                    case 7: // Удаление коллекции
                        {
                            Console.Clear();
                            myCollection.Clear();
                            Console.WriteLine("Коллекция удалена!");
                            CustomFunctions.Pause();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Выберите из списка");
                            break;
                        }
                }
            } while (choice != 0);
        }

        static void Main(string[] args)
        {
            Menu();
        }
    }
}
