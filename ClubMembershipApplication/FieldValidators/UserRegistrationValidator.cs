using ClubMembershipApplication.Data;
using FieldValidatorAPI;

namespace ClubMembershipApplication.FieldValidators
{
    public class UserRegistrationValidator: IFieldValidators
    {
        const int FirstName_Min_Length = 2;
        const int FirstName_Max_Length = 100;
        const int LastName_Min_Length = 2;
        const int LastName_Max_Length = 100;

        delegate bool EmailExistsDel(string emailAddress);

        FieldvalidatorDel _fieldvalidatorDel = null;

        RequiredValidDel _requiredValidDel = null;
        StringLengthValidDel _stringLengthValidDel = null;
        DateValidDel _dateValidDel = null;
        PatternMatchDel _patternMatchValidDel = null;
        CompareFieldsValidDel _compareFieldsValidDel = null;
        EmailExistsDel _emailExistsDel = null;

        string[] _fieldArray = null;
        IRegister _register = null;

        public string[] FieldArray
        {
            get
            {
                if(_fieldArray == null)
                {
                    _fieldArray = new string[Enum.GetValues(typeof(FieldConstants.UserRegistrationField)).Length];
                }

                return _fieldArray;
            }
        }

        public FieldvalidatorDel ValidatorDel => _fieldvalidatorDel;

        public UserRegistrationValidator(IRegister register)
        {
            _register = register;
        }

        public FieldvalidatorDel validationDel => throw new NotImplementedException();

        public void InitialiseValidatorDelegates()
        {
            _fieldvalidatorDel = new FieldvalidatorDel(ValidField);
            _emailExistsDel = new EmailExistsDel(_register.EmailExist);

            _requiredValidDel = CommonFieldValidatorFunctions.RequiredValidDel;
            _stringLengthValidDel = CommonFieldValidatorFunctions.StringLengthValidDel;
            _dateValidDel = CommonFieldValidatorFunctions.DateFieldValidDel;
            _patternMatchValidDel = CommonFieldValidatorFunctions.PatternMatchValidDel;
            _compareFieldsValidDel = CommonFieldValidatorFunctions.FieldsCompareValidDel;
        }

        private bool ValidField(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMessage)
        {
            fieldInvalidMessage = "";

            FieldConstants.UserRegistrationField userRegistrationField = (FieldConstants.UserRegistrationField)fieldIndex;

            switch (userRegistrationField)
            {
                case FieldConstants.UserRegistrationField.EmailAddress:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? 
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" 
                        : "";

                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Email_Address_RegEx_Pattern)) ?
                        $"You must enter a value for email address{Environment.NewLine}"
                        : fieldInvalidMessage;

                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_emailExistsDel(fieldValue)) ?
                        $"This email address already  exists{Environment.NewLine}"
                        : fieldInvalidMessage;
                    break;

                case FieldConstants.UserRegistrationField.FirstName:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? 
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" 
                        : "";

                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_stringLengthValidDel(fieldValue, FirstName_Min_Length, FirstName_Max_Length)) ?
                        $"You must be between {FirstName_Min_Length} and {FirstName_Max_Length}{Environment.NewLine}"
                        : fieldInvalidMessage;
                    break;

                case FieldConstants.UserRegistrationField.LastName:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? 
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" 
                        : "";

                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_stringLengthValidDel(fieldValue, LastName_Min_Length, LastName_Max_Length)) ?
                        $"You must be between {LastName_Min_Length} and {LastName_Min_Length}{Environment.NewLine}"
                        : fieldInvalidMessage;
                    break;

                case FieldConstants.UserRegistrationField.Password:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? 
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" 
                        : "";

                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Strong_Password_RegEx_Pattern)) ?
                        $"Your password must be at least 1 small-case letter, 1 capital letter, 1 special character and the length should be  6 -10 characters{Environment.NewLine}"
                        : fieldInvalidMessage;
                    break;

                case FieldConstants.UserRegistrationField.PasswordCompare:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? 
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" 
                        : "";

                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_compareFieldsValidDel(fieldValue, fieldArray[(int)FieldConstants.UserRegistrationField.Password])) ?
                        $"Your password must be at least 1 small-case letter, 1 capital letter, 1 special character and the length should be  6 -10 characters{Environment.NewLine}"
                        : fieldInvalidMessage;
                    break;

                case FieldConstants.UserRegistrationField.DateOfBirth:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? 
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" 
                        : "";

                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_dateValidDel(fieldValue, out DateTime valiđateTime)) ?
                        "You did not enter a valid date" : fieldInvalidMessage;
                    break;

                case FieldConstants.UserRegistrationField.PhoneNumber:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? 
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" 
                        : "";

                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Uk_PhoneNumber_RegEx_Pattern)) ?
                        "You did not enter a valid phone number" : fieldInvalidMessage;
                    break;

                case FieldConstants.UserRegistrationField.AddressFirstLine:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? 
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" 
                        : "";
                    break;

                case FieldConstants.UserRegistrationField.AddressSecondLine:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? 
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" 
                        : "";
                    break;

                case FieldConstants.UserRegistrationField.AddressCity:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? 
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" 
                        : "";
                    break;

                case FieldConstants.UserRegistrationField.PostCode:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? 
                        $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" 
                        : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Uk_Post_Code_RegEx_Pattern)) ?
                        "You did not enter a valid post code" : fieldInvalidMessage;
                    break;

                default:
                    throw new ArgumentException("This field does not exist");
            }

            return (fieldInvalidMessage == "");
        }
    }
}
