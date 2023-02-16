using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class UserControllerIntegrationTests :
    IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _user;
        public UserControllerIntegrationTests(TestingWebAppFactory<Program> factory)
        {
            _user = factory.CreateClient();
        }

        // GET тесты
        // Проверка содержит ли главная страница кнопку "Добавить пользователя"
        [Fact]
        public async Task GetHomePage()
        {
            var response = await _user.GetAsync("/Home/Create");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Добавить пользователя",
            responseString);
        }



        // POST тесты Home/Details/1
        [Fact]
        public async Task Create_WithIncorrect_NumberPhone()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/home/create");
            var formModel = new Dictionary<string, string>
{
            { "Name", "Ruslann" },
            { "Email", "ruslan@mail.ru" },
            { "Phone", "8946578367" },
            { "Age", "19" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _user.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Номер телефона должен содержать 11 цифр", responseString);
        }

        ///////////

        [Fact]
        public async Task Create_WhenCalled_ReturnsCreateForm()
        {
            var response = await _user.GetAsync("/Home/Create");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Пожалуйста, введите данные нового пользователя",
            responseString);
        }

        [Fact]
        public async Task Create_SentWrongModel_ReturnsViewWithErrorMessages()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Home/create");
            var formModel = new Dictionary<string, string>
            {
            { "Email", "User@mail.ru" },
            { "Phone", "89465783674" },
            { "Age", "25" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _user.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Введены не все данные", responseString);
        }

        [Fact]
        public async Task Create_WhenPOSTExecuted_ReturnsToIndexViewWithCreatedUser()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/home/create");
            var formModel = new Dictionary<string, string>
{
            { "Name", "Kate" },
            { "Email", "Kate@mail.ru" },
            { "Phone", "89465783674" },
            { "Age", "25" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _user.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Kate", responseString);
        }


    }
}
