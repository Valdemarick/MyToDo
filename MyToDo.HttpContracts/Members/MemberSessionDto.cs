namespace MyToDo.HttpContracts.Members;

public sealed class MemberSessionDto
{
    public string FullName { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
    public int ExpiresIn { get; set; }
    public DateTime ExpiryTimeStamp { get; set; }
}
