namespace WebApplication2.Entity
{
	public class InvoiceEntity
	{
		public string invoiceCode { get; set; }
        public string insertDate { get; set; }
        public List<StoreEntity> stores { get; set; }

		public InvoiceEntity(string invoiceCode, List<StoreEntity> stores)
		{
			if (string.IsNullOrEmpty(invoiceCode))
			{
				throw new Exception("Please Input Invoice Code!");
			}
			this.invoiceCode = invoiceCode;
			this.insertDate = DateTime.Now.ToString("dd/MM/yyyy");
			this.stores = stores;
		}

		public InvoiceEntity() { }
	}
}
