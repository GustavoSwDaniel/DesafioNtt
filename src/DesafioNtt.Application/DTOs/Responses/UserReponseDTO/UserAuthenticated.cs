using System.Text.Json.Serialization;

namespace DesafioNtt.Application.DTOs.Responses.UserReponseDTO
{
    public class UserAuthenticated
    {
        public bool Success => Errors.Count == 0;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string AccessToken { get; private set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string RefreshToken { get; private set; } = null!;

        public List<string> Errors { get; private set; }

        public UserAuthenticated() =>
            Errors = new List<string>();

        public UserAuthenticated(bool success, string accessToken, string refreshToken) : this()
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public void AddError(string error) =>
            Errors.Add(error);

        public void AddError(IEnumerable<string> erros) =>
            Errors.AddRange(erros);
    }
}