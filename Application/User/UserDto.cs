namespace Application.User
{
    public record UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
