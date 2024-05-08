namespace Warehouse
{
    internal class ContractorEntity
    {
        public long contractorId { get; set; }

        public string address { get; set; }

        public string title { get; set; }

        public string settlementAccount { get; set; }

        public string phoneNumber { get; set; }

        public string bankName { get; set; }

        public ContractorEntity()
        {
        }

        public ContractorEntity(long contractorId, string address, string title, string settlementAccount, string phoneNumber, string bankName)
        {
            this.contractorId = contractorId;
            this.address = address;
            this.title = title;
            this.settlementAccount = settlementAccount;
            this.phoneNumber = phoneNumber;
            this.bankName = bankName;
        }

        public ContractorEntity(string address, string title, string settlementAccount, string phoneNumber, string bankName)
        {
            this.address = address;
            this.title = title;
            this.settlementAccount = settlementAccount;
            this.phoneNumber = phoneNumber;
            this.bankName = bankName;
        }
    }
}
