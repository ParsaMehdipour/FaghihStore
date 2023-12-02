namespace SH.Infrastructure.Consts;

public class RegularExpressionConsts
{
    public const string PhoneNumber = @"^(\+98|0)?9\d{9}$";
    public const string NationalCode = @"^([0-9]){10}$";
    //public const string Email = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
    //public const string Email = @"^[A-Z0-9+_.-]+@[A-Z0-9.-]+$";
    //public const string Email = @"\A\S+@\S+\Z";
}