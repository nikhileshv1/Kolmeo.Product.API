namespace Kolmeo.Domain
{
    /// <summary>
    /// Products Model
    /// </summary>
    public class Products
    {
        /// <summary>
        /// List of Products Embedded in Items.
        /// </summary>
        public List<Product> Items { get; set; }
    }
}
