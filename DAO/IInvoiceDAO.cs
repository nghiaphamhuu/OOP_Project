using WebApplication2.Entity;

namespace WebApplication2.DAO
{
	public interface IInvoiceDAO
	{
		List<InvoiceEntity> getAllInvoice();

		void addInvoice(InvoiceEntity invoice);

		InvoiceEntity getInvoiceDetail(string invoiceCode);

		List<InvoiceEntity> getInvoiceBySearchName(string keySearch);

		void updateInvoice(InvoiceEntity invoice);

		void deleteInvoice(string invoiceCode);
	}
}
