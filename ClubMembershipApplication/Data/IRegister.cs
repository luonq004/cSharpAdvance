namespace ClubMembershipApplication.Data
{
    public interface IRegister
    {
        bool Register(string[] fields);
        bool EmailExist(string emailAddress);
    }
}
