namespace SH.Application.Models;

public record SmsServiceDto
{
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}

public record PostSms
{
    public string Mobile { get; set; }
    public string TemplateId { get; set; }
    public List<PostSmsParameter> Parameters { get; set; }
}

public record PostSmsParameter
{
    public string Name { get; set; }
    public string Value { get; set; }
}