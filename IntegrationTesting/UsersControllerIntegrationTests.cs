using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using TISLR2;
using Program = TISLR2.Program;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

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
        // Проверка содержит ли футер надпись 2023 - TISLR2 Elisey Krasnov
        [Fact]
        public async Task GetFooter()
        {
            var response = await _user.GetAsync("/Home");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("2023 - TISLR2 Elisey Krasnov", responseString);
        }

        // Проверка содержит ли страница формы добавления заголовок "Add new user" V
        [Fact]
        public async Task GetCreatePage()
        {
            var response = await _user.GetAsync("/Home/create");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("<title>Add new user</title>", responseString);
        }

        // Проверка содержит ли главная страница заголовок h1 с надписью Главная страница V
        [Fact]
        public async Task GetHomeHeader()
        {
            var response = await _user.GetAsync("/Home");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("<h1>Главная страница</h1>", responseString);
        }


        // POST тесты
        // Тест с созданием пользователя и проверкой его на добавление в главной странице V
        [Fact]
        public async Task CreateAndCheckUser()
        {
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "/Home/create");
            var formModel = new Dictionary<string, string>
            {
                { "Name", "Elisey" },
                { "Email", "EliseyK@mail.ru" },
                { "Phone", "89123456789" },
                { "Age", "19" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _user.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Elisey", responseString);
        }

        // Тест создания пользователя с уже существующей в бд электронной почтой
        [Fact]
        public async Task Create_WithSame_Email()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Home/create");
            var formModel = new Dictionary<string, string>
{
            { "Name", "Elisey1" },
            { "Email", "Elisey1@mail.ru" },
            { "Phone", "88005553535" },
            { "Age", "19" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _user.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            // создание еще одного пользователя с таким же email
            var postRequestCopy = new HttpRequestMessage(HttpMethod.Post, "/Home/create");

            var formModelCopy = new Dictionary<string, string>
{
            { "Name", "Elisey2" },
            { "Email", "Elisey1@mail.ru" },
            { "Phone", "88005553535" },
            { "Age", "19" }
            };
            postRequestCopy.Content = new FormUrlEncodedContent(formModelCopy);
            var responseCopy = await _user.SendAsync(postRequestCopy);
            responseCopy.EnsureSuccessStatusCode();
            var responseString = await responseCopy.Content.ReadAsStringAsync();
            Assert.Contains("Пользователь с таким email уже существует", responseString);
        }

        // Тест создания с правильной длинной, но с символами
        [Fact]
        public async Task Create_WithIncorrect_NumberPhone()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Home/create");
            var formModel = new Dictionary<string, string>
{
            { "Name", "Elisey" },
            { "Email", "Elisey@mail.ru" },
            { "Phone", "89adfasfd34" },
            { "Age", "19" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _user.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Номер телефона должен состоять только из цифр", responseString);
        }

        
        // Delete тесты
        // Тест удаления пользователя с несуществующим Id
        [Fact]
        public async Task DeleteUser()
        {
            HttpRequestMessage deleteRequest = new HttpRequestMessage(HttpMethod.Delete, "/Home/DeleteUser/10001");
            var response = await _user.SendAsync(deleteRequest);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        // Put тесты
        // Тест с обновления пользователя с неправильно введенным номером телефона
        [Fact]
        public async Task UpdateUser()
        {
            HttpRequestMessage putRequest = new HttpRequestMessage(HttpMethod.Put, "/Home/UpdateUser");
            var formModel = new Dictionary<string, string>
            {
            { "Name", "Elisey" },
            { "Email", "Elisey@mail.ru" },
            { "Phone", "8800555353" },
            { "Age", "19" }
            };
            putRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _user.SendAsync(putRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Номер телефона должен содержать 11 цифр", responseString);
        }



    }
}
