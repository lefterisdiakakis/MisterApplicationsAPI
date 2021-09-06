namespace Application.Core
{
    public record ApplicationProperties
    {
        // TODO: rename JWT key cause we will make it at run time
        public string JWTKey { get; set; }
    }
}
