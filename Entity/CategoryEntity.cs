namespace WebApplication2.Entity
{
    public class CategoryEntity
    {
        public string typeCd { get; set; }
        public string typeDesc { get; set; }

        public CategoryEntity(string typeCd, string typeDesc) 
        {
            if (string.IsNullOrEmpty(typeCd))
            {
                throw new Exception("Please Input Type Code Of Category!");
            }

            if (string.IsNullOrEmpty(typeDesc))
            {
                throw new Exception("Please Input Type Descriptiong Of Category!");
            }
            
            this.typeCd = typeCd;   
            this.typeDesc = typeDesc;
        }

        public CategoryEntity() { }
    }
}
