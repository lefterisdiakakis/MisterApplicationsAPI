namespace Application.User
{
    public record LogInDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Code_Challenge { get; set; }
    }
}
