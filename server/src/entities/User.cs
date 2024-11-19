using System.Text.Json.Serialization;

namespace rodrigo_server;
public class User
{
    [JsonIgnore]
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    [JsonIgnore]
    public DateTime? CreatedAt { get; set; }
}
