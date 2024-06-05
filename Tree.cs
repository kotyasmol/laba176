using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonLibrary;

namespace LaboratoryWork_12
{
    public class Tree<T> : ICollection<T>, IEnumerable<T> where T : ICloneable
    {
        public class Point<T> : IComparable<T>
        {
            private T data;
            private Point<T> left;
            private Point<T> right;
            IComparer<T> comparer;

            public T Data { get; set; }
            public Point<T> Left { get; set; }
            public Point<T> Right { get; set; }
            public Point()
            {
                this.comparer = Comparer<T>.Default;
                Data = default(T);
                Left = null;
                Right = null;
            }
            public Point(T d, IComparer<T> comparer = null)
            {
                if (comparer is null)
                    this.comparer = Comparer<T>.Default;
                else
                    this.comparer = comparer;
                Data = d;
                Left = null;
                Right = null;
            }
            public Point(Point<T> p)
            {
                Data = p.Data;
                Left = p.Left;
                Right = p.Right;
                comparer = p.comparer;
            }
            public override string ToString()
            {
                return Data.ToString();
            }
            public int CompareTo(T other)
            {
                return comparer.Compare(Data, other);
            }
        }


        protected Point<T> root;
        int size;
        private IComparer<T> comparer;


        private int Size { get; set; }
        public int Count => Size;
        public bool isEmpty
        {
            get => root == null ? true : false;
        }

        public Tree()
        {
            this.comparer = Comparer<T>.Default;
        }
        public Tree(Tree<T> other)
        {
            this.comparer = other.comparer;
            var Point = other.root; // Текущий узел устанавливается в корень дерева
            var queue = new Queue<Point<T>>();
            while (queue.Count > 0 || Point != null)
            {
                if (Point == null) // Обработка левого поддерева
                {
                    Point = queue.Dequeue(); // извлекаем верхний элемент из стека
                    T newData = (T)Point.Data.Clone();
                    this.Add(newData); // возвращаем данные извлеченного узла
                    Point = Point.Right;
                }
                else
                {
                    queue.Enqueue(Point); // помещаем текущий узел в стек, чтобы вернуться к нему позже при обработке правого поддерева
                    Point = Point.Left; // переходим в левое поддерево
                }
            }
        }
        public Tree(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public virtual void Add(T d)
        {
            Size++;
            if (root == null)
            {
                root = new Point<T>(d, comparer);
                return;
            }
            Point<T> p = root;
            Point<T> r = null;
            while (p != null)
            {
                r = p;
                if (d.Equals(p.Data))
                {
                    return;
                }
                else if (comparer.Compare(d, r.Data) < 0)
                    p = p.Left;
                else
                    p = p.Right;
            }
            Point<T> NewPoint = new Point<T>(d, comparer);
            if (comparer.Compare(d, r.Data) < 0)
                r.Left = NewPoint;
            else
                r.Right = NewPoint;
        }

        public virtual bool Remove(T item)
        {
            if (root == null)
                return false;

            Point<T> current = root, parent = null;

            int result;
            do
            {
                result = comparer.Compare(item, current.Data);
                if (result < 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result > 0)
                {
                    parent = current;
                    current = current.Right;
                }
                if (current == null)
                    return false;
            }
            while (result != 0);

            if (current.Right == null)
            {
                if (current == root)
                    root = current.Left;
                else
                {
                    result = comparer.Compare(current.Data, parent.Data);
                    if (result < 0)
                        parent.Left = current.Left;
                    else
                        parent.Right = current.Left;
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (current == root)
                    root = current.Right;
                else
                {
                    result = comparer.Compare(current.Data, parent.Data);
                    if (result < 0)
                        parent.Left = current.Right;
                    else
                        parent.Right = current.Right;
                }
            }
            else
            {
                Point<T> min = current.Right.Left, prev = current.Right;
                while (min.Left != null)
                {
                    prev = min;
                    min = min.Left;
                }
                prev.Left = min.Right;
                min.Left = current.Left;
                min.Right = current.Right;

                if (current == root)
                    root = min;
                else
                {
                    result = comparer.Compare(current.Data, parent.Data);
                    if (result < 0)
                        parent.Left = min;
                    else
                        parent.Right = min;
                }
            }
            --Size;
            return true;
        }

        private Point<T> Find(T data)
        {
            int result;
            Point<T> current = root;
            while (current != null)
            {
                result = comparer.Compare(data, current.Data);
                if (result == 0)
                {
                    return current;
                }
                else if (result < 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }

            }
            return null;
        }

        /// <summary>
        /// Копирует элементы текущего дерева в массив array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(T[] array, int index)
        {
            foreach (var value in this) // Перебирает все элементы дерева с помощью this
                array[index++] = value; // Присваивает значение каждого элемента массиву array с индексом index
        }
        /// <summary>
        /// Возвращает false, поскольку класс по умолчанию не является "только для чтения"
        /// </summary>
        public bool IsReadOnly
        {
            get => false;
        }
        /// <summary>
        /// Проверяет, существует ли элемент data в дереве.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool Contains(T data)
        {
            if (Find(data) != null)
                return true;
            return false;
        }

        /// <summary>
        /// Создает поверхностную копию дерева.
        /// </summary>
        /// <returns></returns>
        public Tree<T> ShallowCopy()
        {
            return (Tree<T>)this.MemberwiseClone();
        }
        /// <summary>
        /// Создает глубокую копию дерева.
        /// </summary>
        /// <returns></returns>
        public Tree<T> Clone()
        {
            Tree<T> newTree = new Tree<T>(this);
            return newTree;
        }
        /// <summary>
        /// Очищает дерево. Вызывает вспомогательный метод Clear(root) для рекурсивной очистки дерева.
        /// </summary>
        public void Clear()
        {
            Clear(root);
            root = null;
        }
        /// <summary>
        /// Рекурсивно очищает поддерево, начиная с узла p
        /// </summary>
        /// <param name="p"></param>
        void Clear(Point<T> p)
        {
            if (p != null)
            {
                Clear(p.Left);
                p.Data = default(T);
                Clear(p.Right);
            }
        }

        public virtual void Print()
        {
            if (root == null)
                Console.WriteLine("Коллекция пустая\n");
            foreach (var t in this)
            {
                Console.WriteLine(t + "\n");
            }
        }
        private void ShowTree(Point<T> p, int l)
        {
            if (p != null)
            {
                String word = p.Data.ToString();
                String str = word.Substring(0, word.IndexOf('\t'));
                ShowTree(p.Left, l + 4);

                for (int i = 0; i < l; i++)
                    Console.Write(" ");

                Console.WriteLine(str);
                Console.Write("\n");
                ShowTree(p.Right, l + 4);
            }
        }
        public void ShowTree()
        {
            ShowTree(root, 0);
        }

        /*метод необобщенного интерфейса IEnumerable, который возвращает объект-нумератор*/
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /*метод обобщенного интерфейса IEnumerable<T>, который возвращает обобщенный объект-нумератор */
        public IEnumerator<T> GetEnumerator()
        {
            return Inorder().GetEnumerator();
        }

        /// <summary>
        /// Обеспечивает соответствие интерфейсу IDisposable, который используется для реализации управления ресурсами и очистки
        /// </summary>
        public void Dispose()
        {
            return;
        }

        /// <summary>
        /// Обход дерева в порядке:
        /// Левое поддерево - Корень - Правое поддерево. 
        /// По сути, это возвращает данные в упорядоченном виде.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Inorder()
        {
            if (root == null) // Проверка пустого дерева
                yield break;

            var stack = new Stack<Point<T>>(); // Стек создается для хранения узлов во время обхода
            var Point = root; // Текущий узел устанавливается в корень дерева

            while (stack.Count > 0 || Point != null)
            {
                if (Point == null) // Обработка левого поддерева
                {
                    Point = stack.Pop(); // извлекаем верхний элемент из стека
                    yield return Point.Data; // возвращаем данные извлеченного узла
                    Point = Point.Right;
                }
                else
                {
                    stack.Push(Point); // помещаем текущий узел в стек, чтобы вернуться к нему позже при обработке правого поддерева
                    Point = Point.Left; // переходим в левое поддерево
                }
            }
        }
    }
}
