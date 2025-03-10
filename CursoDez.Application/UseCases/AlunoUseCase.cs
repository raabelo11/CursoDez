using CursoDez.Application.DTOs;
using CursoDez.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CursoDez.Application.UseCases
{
    public class AlunoUseCase : IAlunoUseCase
    {
        public readonly IAlunoRepository _alunoRepository;
        private readonly ILogger<AlunoUseCase> _logger;
        private readonly IRabbitMqPublisher _rabbitPublish;


        public AlunoUseCase(IAlunoRepository alunoRepository, ILogger<AlunoUseCase> logger, IRabbitMqPublisher rabbitPublish)
        {
            _alunoRepository = alunoRepository;
            _logger = logger;
            _rabbitPublish = rabbitPublish;
        }

        public async Task<bool> CreateMatricula(AlunoDTO alunoDTO)
        {
            bool retorno = false;

            try
            {
                if (alunoDTO == null)
                    _logger.LogError("Erro na validação dos dados do aluno");
                else
                {
                    retorno = await _alunoRepository.CreateMatriculaAsync(alunoDTO);
                    var serializaAluno = JsonSerializer.Serialize(alunoDTO);

                    await _rabbitPublish.PublishMessage(serializaAluno);
                    _logger.LogInformation($"Matricula enviada para fila com sucesso: {serializaAluno}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exeção gerada ao criar uma matrícula de aluno: {ex.Message}, {ex.InnerException}");
            }

            return retorno;
        }
    }
}
