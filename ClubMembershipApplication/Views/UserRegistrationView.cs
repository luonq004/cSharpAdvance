using ClubMembershipApplication.Data;
using ClubMembershipApplication.FieldValidators;

namespace ClubMembershipApplication.Views
{
    public class UserRegistrationView : IView
    {
        IFieldValidators _fieldValidators = null;
        IRegister _register= null;

        public IFieldValidators FieldValidator { get => _fieldValidators; }

        public UserRegistrationView(IRegister register, IFieldValidators fieldValidators)
        {
            _fieldValidators = fieldValidators;
            _register = register;
        }

        public void RunView()
        {
            CommonOutputText.WriteMainHeading();
            CommonOutputText.WriteRegistrationHeading();

            _fieldValidators.FieldArray[(int)FieldConstants.UserRegistrationField.EmailAddress] = GetInputFromUser(FieldConstants.UserRegistrationField.EmailAddress, "Please enter your email address: ");
            _fieldValidators.FieldArray[(int)FieldConstants.UserRegistrationField.FirstName] = GetInputFromUser(FieldConstants.UserRegistrationField.FirstName, "Please enter your first name: ");
            _fieldValidators.FieldArray[(int)FieldConstants.UserRegistrationField.LastName] = GetInputFromUser(FieldConstants.UserRegistrationField.LastName, "Please enter your last name: ");
            _fieldValidators.FieldArray[(int)FieldConstants.UserRegistrationField.Password] = GetInputFromUser(FieldConstants.UserRegistrationField.Password, "Please enter your password: ");
            _fieldValidators.FieldArray[(int)FieldConstants.UserRegistrationField.PasswordCompare] = GetInputFromUser(FieldConstants.UserRegistrationField.PasswordCompare, "Please re-enter your password: ");
            _fieldValidators.FieldArray[(int)FieldConstants.UserRegistrationField.PhoneNumber] = GetInputFromUser(FieldConstants.UserRegistrationField.PhoneNumber, "Please enter your phone number: ");
            _fieldValidators.FieldArray[(int)FieldConstants.UserRegistrationField.DateOfBirth] = GetInputFromUser(FieldConstants.UserRegistrationField.DateOfBirth, "Please enter your date of birth: ");
            _fieldValidators.FieldArray[(int)FieldConstants.UserRegistrationField.AddressFirstLine] = GetInputFromUser(FieldConstants.UserRegistrationField.AddressFirstLine, "Please enter the first line of your address: ");
            _fieldValidators.FieldArray[(int)FieldConstants.UserRegistrationField.AddressSecondLine] = GetInputFromUser(FieldConstants.UserRegistrationField.AddressSecondLine, "Please enter the second line of your address: ");
            _fieldValidators.FieldArray[(int)FieldConstants.UserRegistrationField.AddressCity] = GetInputFromUser(FieldConstants.UserRegistrationField.AddressCity, "Please enter your address city: ");
            _fieldValidators.FieldArray[(int)FieldConstants.UserRegistrationField.PostCode] = GetInputFromUser(FieldConstants.UserRegistrationField.PostCode, "Please enter your post code: ");

            RegisterUser();
        }

        private void RegisterUser()
        {
            _register.Register(_fieldValidators.FieldArray);

            CommonOutputFormat.ChangeFontColor(FontTheme.Success);
            Console.WriteLine("You have successfully registered. Please press any key to login");
            CommonOutputFormat.ChangeFontColor(FontTheme.Default);
            Console.ReadKey();
        }

        private string GetInputFromUser(FieldConstants.UserRegistrationField field, string promtText)
        {
            string fieldVal = "";

            do
            {
                Console.Write(promtText);
                fieldVal = Console.ReadLine();
            }
            while (!FieldValid(field, fieldVal));
            return fieldVal;
        }

        private bool FieldValid(FieldConstants.UserRegistrationField field, string fieldValue)
        {
            if(!_fieldValidators.ValidatorDel((int)field, fieldValue, _fieldValidators.FieldArray, out string invalidMessage))
            {
                CommonOutputFormat.ChangeFontColor(FontTheme.Danger);
                Console.WriteLine(invalidMessage);
                CommonOutputFormat.ChangeFontColor(FontTheme.Default);

                return false;
            }

            return true;
        }
    }
}
