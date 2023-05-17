namespace CollectionHierarchy.Core
{
    using CollectionHierarchy.IO.Interfaces;
    using CollectionHierarchy.Models;
    using CollectionHierarchy.Models.Interfaces;
    using Interfaces;
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Start()
        {
            IAddCollection addCollection = new AddCollection();
            IAddRemoveCollection addremoveCollection = new AddRemoveCollection();
            IMyList myList = new MyList();

            string[] elements = reader.Readline().Split();
            int[,] matrix = new int[3, elements.Length];

            for (int i = 0; i < elements.Length; i++)
            {
                matrix[0, i] = addCollection.Add(elements[i]);
                addremoveCollection.Add(elements[i]);
                myList.Add(elements[i]);
            }

            int n = int.Parse(reader.Readline());

            string[,] deletedElements = new string[2, n];

            for (int i = 0; i < n; i++)
            {
                deletedElements[0, i] = addremoveCollection.Remove();
                deletedElements[1, i] = myList.Remove();
            }

            PrintMatrix(matrix);
            PrintMatrix(deletedElements);
            
        }

        public void PrintMatrix<T>(T[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    writer.Write(matrix[row, col].ToString() + " ");
                }
                writer.WriteLine(string.Empty);
            }
        }
    }
}
