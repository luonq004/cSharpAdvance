using ClubMembershipApplication.FieldValidators;

namespace ClubMembershipApplication.Views
{
    internal interface IView
    {
        void RunView();

        IFieldValidators FieldValidator { get; }
    }
}
