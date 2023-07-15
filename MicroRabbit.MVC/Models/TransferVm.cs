namespace MicroRabbit.MVC.Models
{
    public class TransferVm
    {
        public string TranferNotes { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
