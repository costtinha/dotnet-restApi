namespace OfficeApi.Dtos
{
    public record EmployeeResponseDto()
    {
        public required string name { get; init; }

        public required int OfficeCode { get; init; }
    }
}