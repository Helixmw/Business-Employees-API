namespace Employees_API.DTOs.Users
{
    public interface ILoginUserDTO
    {
        string? Email { get; set; }
        string? Password { get; set; }
    }
}