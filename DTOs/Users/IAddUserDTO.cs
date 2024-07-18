namespace Employees_API.DTOs.Users
{
    public interface IAddUserDTO
    {
        string? Email { get; set; }
        string? Password { get; set; }
        string? UserName { get; set; }
    }
}