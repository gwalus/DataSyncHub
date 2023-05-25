using System.Text.Json.Serialization;

namespace DataSyncHub.Modules.Users.Core.Models
{
    internal class RandomUser
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("sex")]
        public string? Sex { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("birthday")]
        public string? Birthday { get; set; }
    }
}