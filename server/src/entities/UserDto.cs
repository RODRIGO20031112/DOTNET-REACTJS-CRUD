using System.Text.Json.Serialization;

namespace rodrigo_server;
public class UserDto
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime? CreatedAt { get; set; }
}
