namespace CollectionHierarchy.Models
{
    using Interfaces;
    public class AddCollection : IAddCollection
    {
        private List<string> list = new List<string>();
        public int Add(string value)
        {
            list.Add(value);
            return list.LastIndexOf(value);
        }
    }
}
