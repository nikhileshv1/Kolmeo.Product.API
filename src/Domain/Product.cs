using System.ComponentModel.DataAnnotations;

namespace Kolmeo.Domain
{
    /// <summary>
    /// Product Model
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Product Name
        /// </summary>
        [MaxLength(17, ErrorMessage = "Name length too long")]
        public string Name { get; set; }
        /// <summary>
        /// Product Description
        /// </summary>
        [MaxLength(35, ErrorMessage = "Description length too long")]
        public string Description { get; set; }
        /// <summary>
        /// Product Price
        /// </summary>
        public decimal Price { get; set; }
    }
}