namespace ClubMembershipApplication.FieldValidators
{
    public delegate bool FieldvalidatorDel(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMessage);
    public interface IFieldValidators
    {
        void InitialiseValidatorDelegates();
        string[] FieldArray { get; }
        FieldvalidatorDel validationDel { get; }
    }
}
