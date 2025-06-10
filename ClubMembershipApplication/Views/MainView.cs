using ClubMembershipApplication.FieldValidators;

namespace ClubMembershipApplication.Views
{
    internal class MainView : IView
    {
        public IFieldValidators FieldValidator => null;

        IView _registerView = null;
        IView _loginView = null;

        public MainView(IView registerView, IView loginView)
        {
            _registerView = registerView;
            _loginView = loginView;
        }

        public void RunView()
        {
            CommonOutputText.WriteMainHeading();

            Console.WriteLine("Please press '1' to login or if you are not yet registered please press 'r'");

            ConsoleKey Key = Console.ReadKey().Key;

            if(Key == ConsoleKey.R)
            {
                RunUserRegistrationView();
                RunUserLoginView();

            } else if(Key == ConsoleKey.L)
            {
                RunUserLoginView();

            } else
            {
                Console.Clear();
                Console.WriteLine("Goodbye");
                Console.ReadKey();
            }
        }

        private void RunUserRegistrationView()
        {
            _registerView.RunView();
        }

        private void RunUserLoginView()
        {
            _loginView.RunView();
        }
    }
}
