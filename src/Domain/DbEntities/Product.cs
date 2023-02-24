namespace Kolmeo.Domain.DbEntities
{
    public class Product
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public Guid Id { get; set; }        
        /// <summary>
        /// Product Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Product Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Product Price
        /// </summary>
        public decimal Price { get; set; }
    }
}
