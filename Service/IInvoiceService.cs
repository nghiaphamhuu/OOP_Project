using WebApplication2.Entity;

namespace WebApplication2.Service
{
	public interface IInvoiceService
	{
		List<InvoiceEntity> getAllInvoice();

		List<InvoiceEntity> getInvoiceBySearchName(string keySearch);

		void addInvoice(InvoiceEntity invoice, bool isAddInvoie);

		InvoiceEntity getInvoiceDetail(string invoiceCode);

		void updateInvoice(InvoiceEntity invoice);

		void deleteInvoice(string invoiceCode, bool isDeleteInvoice);

		int checkValid(InvoiceEntity invoice);
	}
}
