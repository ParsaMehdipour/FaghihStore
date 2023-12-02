namespace SH.Infrastructure.Consts;

public class ValidationMessage
{
    public const string NotEmpty = "{PropertyName} نمیتواند خالی باشد";
    public const string MaximumLength = "{PropertyName} نمیتواند بیش از {MaxLength} کاراکتر باشد";
    public const string MinimumLength = "{PropertyName} نمیتواند کمتر از {MinLength} کاراکتر باشد";
    public const string NotEqual = "{PropertyName} نمیتواند برابر {NotEqual} باشد";
    public const string BeUnique = "{PropertyName} نمیتواند تکراری باشد";
    public const string NotExists = "{PropertyName} وجود ندارد";
    public const string RegexIsInvalid = "{PropertyName} درست نمی باشد";

    public const string VerificationCodeIsInvalid = "کد فعال سازی صحیح نمی باشد.";
    public const string VerificationCodeNotEmpty = "کدفعال سازی را وارد کنید";
    public const string UserCannotSignin = "شما امکان ورود ندارید، لطفا به مدیریت مراجعه کنید.";
    public const string UserNotFound = "کاربری با این مشخصات پیدا نشد.";
}