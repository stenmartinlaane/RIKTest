using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using App.DTO.v1_0.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace App.Test.Integration.api;

public class EventControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public EventControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    // [Fact]
    // public async Task IndexRequiresLogin()
    // {
    //     // Act
    //     var response = await _client.GetAsync("/api/v1/Event");
    //     // Assert
    //     Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    // }

    // [Fact]
    // public async Task IndexWithUser()
    // {
    //     var user = "admin@eesti.ee";
    //     var pass = "Kala.maja1";
    //
    //     // get jwt
    //     var response =
    //         await _client.PostAsJsonAsync("/api/v1.0/identity/Account/Login", new {email = user, password = pass});
    //     var contentStr = await response.Content.ReadAsStringAsync();
    //     response.EnsureSuccessStatusCode();
    //
    //     var loginData = JsonSerializer.Deserialize<JWTResponse>(contentStr, JsonHelper.CamelCase);
    //
    //     Assert.NotNull(loginData);
    //     Assert.NotNull(loginData.Jwt);
    //     Assert.True(loginData.Jwt.Length > 0);
    //
    //     var msg = new HttpRequestMessage(HttpMethod.Get, "/api/v1.0/Contests");
    //     msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.Jwt);
    //     msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //
    //     response = await _client.SendAsync(msg);
    //
    //     response.EnsureSuccessStatusCode();
    // }
}