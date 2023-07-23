namespace LibraryManagementBackend.DTO.Auth
{
	public class LoginResponseDto
	{
		public required string Username { get; set; }
		public required string Phone { get; set; }
		public required string CredentialCode { get; set; }
		public required string Token { get; set; }
		public required string Role { get; set; }

		public LoginResponseDto() {}
	}
}
