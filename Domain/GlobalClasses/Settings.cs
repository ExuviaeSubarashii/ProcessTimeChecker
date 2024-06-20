namespace PTC.Domain.GlobalClasses
{
	public class Settings
	{
		public bool TopMost { get; set; }
		public string CurrentTheme { get; set; } = null!;
		public int RefreshTime { get; set; }
		public string Language { get; set; } = null!;
	}
}
