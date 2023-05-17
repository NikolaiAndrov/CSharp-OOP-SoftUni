
namespace CollectionHierarchy.Models
{
    using Interfaces;
    public class AddRemoveCollection : IAddRemoveCollection
    {
        private List<string> list = new List<string>();
        public int Add(string value)
        {
            list.Insert(0, value);
            return list.IndexOf(value);
        }

        public string Remove()
        {
            string lastElement = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return lastElement;
        }
    }
}
