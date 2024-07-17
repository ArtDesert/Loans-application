namespace Loans.Application.AppServices.Contracts.BaseContracts;

/// <summary>
/// Базовый обобщённый интерфейс для валидации входных данных
/// </summary>
/// <typeparam name="T">Тип модели для валидации</typeparam>
public interface IBaseValidator<T>
{
    void Validate(T model, CancellationToken token = default);
}
