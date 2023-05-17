namespace CollectionHierarchy.Models
{
    using Interfaces;
    public class MyList : IMyList
    {
        private List<string> list = new List<string>();
        public int Used
        {
            get
            {
                return list.Count;
            }
        }

        public int Add(string value)
        {
            list.Insert(0, value);
            return list.IndexOf(value);
        }

        public string Remove()
        {
            string item = list[0];
            list.RemoveAt(0);
            return item;
        }
    }
}
