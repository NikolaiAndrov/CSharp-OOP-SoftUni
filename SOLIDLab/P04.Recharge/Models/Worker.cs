namespace P04.Recharge.Models
{
    using Models.Interfaces;
    public abstract class Worker : IWorker
    {
        private string id;
        private int workingHours;

        public Worker(string id)
        {
            this.id = id;
        }

        public virtual void Work(int hours)
        {
            workingHours += hours;
        }
    }
}