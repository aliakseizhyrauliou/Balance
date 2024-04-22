namespace Balance.BePaid.UseCases.Common;

public class CreatedEntityDto<T>(T id)
{
    public T Id { get; set; } = id;
}