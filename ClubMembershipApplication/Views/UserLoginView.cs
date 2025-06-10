using ClubMembershipApplication.Data;
using ClubMembershipApplication.FieldValidators;
using ClubMembershipApplication.Models;

namespace ClubMembershipApplication.Views
{
    public class UserLoginView : IView
    {
        ILogin _loginUser = null;

        public IFieldValidators FieldValidator => null;

        public UserLoginView (ILogin login)
        {
            _loginUser = login;
        }

        public void RunView()
        {
            CommonOutputText.WriteMainHeading();
            CommonOutputText.WriteRegistrationHeading();

            Console.WriteLine("Please enter your email address");
            string emailAddress = Console.ReadLine();

            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine();

            User user = _loginUser.Login(emailAddress, password);

            if(user != null)
            {
                WelcomeUserView welcomeUserView = new WelcomeUserView(user);
                welcomeUserView.RunView();
            } else
            {
                Console.Clear();
                CommonOutputFormat.ChangeFontColor(FontTheme.Danger);
                Console.WriteLine("The credentials that you entered do not match our records");
                CommonOutputFormat.ChangeFontColor(FontTheme.Default);
                Console.ReadKey();
            }
        }
    }
}
