namespace SH.Infrastructure.Extensions;

public static class RandomUtilityExtension
{
    public static int ActivationCode(this Random random, int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue);
    }
}