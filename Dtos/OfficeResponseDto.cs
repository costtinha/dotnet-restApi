namespace OfficeApi.Dtos
{
    public record OfficeResponseDto
    {
        public required string City { get; init; }
        public required string Phone { get; init; }
        public required string Address1 { get; init; }
        public required string Country { get; init; }
        public string? Territory { get; init; }
    }
}