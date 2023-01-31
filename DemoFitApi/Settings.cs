namespace DemoFitApi
{
	public class Settings
	{

		private bool enableAuthorization = true;
		private bool enableUserAgent = true;
		private string authorization_username = "demo";
		private string authorization_password = "demo";
		private string authorizationKey = "Basic ZGVtbzpkZW1v";
		private string userAgentKey = "ZGVtb3Byb2plY3Q=";

		public bool EnableAuthorization { get { return enableAuthorization; } }
		public bool EnableUserAgent { get { return enableUserAgent; } }
		public string Authorization_username { get { return authorization_username; } }
		public string Authorization_password { get { return authorization_password; } }
		public string AuthorizationKey { get { return authorizationKey; } }
		public string UserAgentKey { get { return userAgentKey; } }


	}
}