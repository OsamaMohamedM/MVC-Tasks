namespace Data.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public decimal Balance { get; set; }
        public bool IsFrozen { get; set; }
    }
}