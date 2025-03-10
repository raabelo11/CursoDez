using CursoDez.Application.DTOs;
using CursoDez.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;
using System.Text.Json;
using Xunit;

namespace CursoDez.Tests
{
    public class TesteCurso : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public TesteCurso(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Test_PostCurso_CursoCriadoComSucesso()
        {
            var curso = new CursoDTO
            {
                Nome = "Curso Java2",
                CategoriaCurso = (CategoriaCurso)1,
                CursoAtivo = true
            };

            var content = new StringContent(
                JsonSerializer.Serialize(curso),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync("/api/v1/curso/cursos-incluir", content);

            response.EnsureSuccessStatusCode(); 
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Contains("Curso criado com sucesso !", responseContent);
        }
    }
}
