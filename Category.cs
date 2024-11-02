namespace CafeShopManagementSystem
{
    internal class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public Category(int categoryId, string categoryName)
        {
            this.CategoryID = categoryId;
            this.CategoryName = categoryName;
        }

        public override string ToString()
        {
            return CategoryName; // Display category name in ComboBox
        }
    }
}
