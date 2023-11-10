using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services.ClientService
{
    public interface IUserProfileClientService
    {
        Task<UserProfile> GetUserByUPNAsync(string UPN);
    }

    public class UserProfileClientService : IUserProfileClientService
    {
        private readonly HttpClient _client;
        private readonly AppSettingModel _appSettingModel;

        public UserProfileClientService(HttpClient client, IOptions<AppSettingModel> settings)
        {
            if (client != null && settings != null)
            {
                _appSettingModel = settings.Value;
                client.BaseAddress = new Uri(_appSettingModel.UserProfile);
                client.DefaultRequestHeaders.Add("Accept", _appSettingModel.MediaType);
                _client = client;
            }
        }

        public async Task<UserProfile> GetUserByUPNAsync(string UPN)
        {
            try
            {
                var response = await _client.GetAsync("UserProfile/GetByUPN?UPN=" + UPN);
                response.EnsureSuccessStatusCode();

                using var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<UserProfile>(responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message.ToString());
            }
        }
    }
}
