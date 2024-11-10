using Newtonsoft.Json;
using WebApplication2.Entity;

namespace WebApplication2.DAO
{
	public class InvoiceDAO : IInvoiceDAO
	{
        private static string filePath = System.IO.Directory.GetCurrentDirectory() + "\\DATA\\Invoice.txt";
        public List<InvoiceEntity> getAllInvoice()
        {
            List<InvoiceEntity> invoices = new List<InvoiceEntity>();
            StreamReader reader = new StreamReader(filePath);
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                InvoiceEntity invoice = JsonConvert.DeserializeObject<InvoiceEntity>(line);
                invoices.Add(invoice);
            }

            reader.Close();

            return invoices;
        }

        public void addInvoice(InvoiceEntity invoice)
        {
            StreamReader reader = new StreamReader(filePath);

            reader.Close();

            string json = JsonConvert.SerializeObject(invoice);

            StreamWriter writer = new StreamWriter(filePath, append: true);
            writer.WriteLine(json);
            writer.Close();
        }

        public InvoiceEntity getInvoiceDetail(string invoiceCode)
        {
            List<InvoiceEntity> invoices = getAllInvoice();
            return invoices.FirstOrDefault(x => x.invoiceCode.Equals(invoiceCode));
        }

        public List<InvoiceEntity> getInvoiceBySearchName(string keySearch)
        {
            List<InvoiceEntity> invoices = getAllInvoice();
            List<InvoiceEntity> result = new List<InvoiceEntity>();

            foreach (InvoiceEntity item in invoices)
            {
                if (item.invoiceCode.Contains(keySearch))
                {
                    result.Add(item);
                }
            }

            return result;

        }

        public void updateInvoice(InvoiceEntity invoice)
        {
            List<InvoiceEntity> invoices = getAllInvoice();
            StreamWriter writer = new StreamWriter(filePath);

            foreach (InvoiceEntity item in invoices)
            {
                string json = JsonConvert.SerializeObject(item);

                if (invoice.invoiceCode.Equals(item.invoiceCode))
                {
                    json = JsonConvert.SerializeObject(invoice);
                }

                writer.WriteLine(json);
            }

            writer.Close();
        }

        public void deleteInvoice(string invoiceCode)
        {
            List<InvoiceEntity> invoices = getAllInvoice();

            StreamWriter writer = new StreamWriter(filePath);

            foreach (InvoiceEntity invoice in invoices)
            {
                if (invoice.invoiceCode.Equals(invoiceCode))
                {
                    continue;
                }

                string json = JsonConvert.SerializeObject(invoice);

                writer.WriteLine(json);
            }

            writer.Close();
        }
    }
}
