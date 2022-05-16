namespace hakims_livs.Utils;

public static class FormatPrice
{
    
    public static string ToString(decimal value)
    {
        var price = value.ToString("F");
        price = price[^1] == '0' && price[^2] == '0' && price[^3] == '.' ? price.Remove(price.Length - 3, 3) : price.Replace('.', ',');
        return price;
    }
    
}