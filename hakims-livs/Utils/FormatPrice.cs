namespace hakims_livs.Utils;

public static class FormatPrice
{
    public static string ToString(decimal value)
    {
        var price = value.ToString("F");
        price = price.Replace('.', ',');
        while (price[^1] == '0' || price[^1] == ',')
        {
            price = price.Remove(price.Length - 1, 1);
        }
        
        return price;
    }
    
}