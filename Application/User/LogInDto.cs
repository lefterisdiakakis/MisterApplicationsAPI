namespace Application.User
{
    public record LogInDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
