namespace DeskMarket.Common
{
    public static class CategoryConstants
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 20;

        public const string NameLengthErrorMessage = "Category name must be at least {0} characters long.";
        public const string NameMaxLengthErrorMessage = "Category name cannot exceed {0} characters.";
    }

    public static class ProductConstants
    {
        public const int MinProductNameLength = 2;
        public const int MaxProductNameLength = 60;
        public const int MinDescriptionLength = 10;
        public const int MaxDescriptionLength = 250;
        public const double MinPrice = 1.00;
        public const double MaxPrice = 3000.00;

        public const string ProductNameMaxLengthErrorMessage = "Product name must be between 2 and 60 characters.";
        public const string DescriptionMaxLengthErrorMessage = "Description must be between 10 and 250 characters.";
        public const string PriceRangeErrorMessage = "Price must be between 1 and 3000.";
        public const string UrlErrorMessage = "Pease enter a valid URL";
    }
    
}
