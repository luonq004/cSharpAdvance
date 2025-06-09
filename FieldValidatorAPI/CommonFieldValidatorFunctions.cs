using System.Text.RegularExpressions;

namespace FieldValidatorAPI
{
    /* Đảm bảo các trường không đc để trống */
    public delegate bool RequiredValidDel(string fieldVal);

    /* Hạn chế độ dài của field  */
    public delegate bool StringLengthValidDel(string fieldVal, int min, int max);

    /* Validate thời gian */
    public delegate bool DateValidDel(string fieldVal, out DateTime valiđateTime);

    /* Để validate theo pattern */
    public delegate bool PatternMatchDel(string fieldVal, string pattern);

    /* Để xác định 1 giá trị của trường nhập so với 1 giá trị thông thông thg biểu mẫu (expression pattern) */
    public delegate bool CompareFieldsValidDel(string fieldVal, string fieldValCompare);

    public class CommonFieldValidatorFunctions
    {
        private static RequiredValidDel _requiredValidDel = null;
        private static StringLengthValidDel _stringLengthValidDel = null;
        private static DateValidDel _dateValidDel = null;
        private static PatternMatchDel _patternMatchDel = null;
        private static CompareFieldsValidDel _compareFieldsValidDel = null;

        public static RequiredValidDel RequiredValidDel
        {
            get
            {
                if (_requiredValidDel == null) _requiredValidDel = new RequiredValidDel(RequiredFieldValid);
                return _requiredValidDel;
            }
        }
        public static StringLengthValidDel StringLengthValidDel
        {
            get
            {
                if (_stringLengthValidDel == null) _stringLengthValidDel = new StringLengthValidDel(StringFieldLengthValid);
                return _stringLengthValidDel;
            }
        }
        public static DateValidDel DateFieldValidDel
        {
            get
            {
                if (_dateValidDel == null) _dateValidDel = new DateValidDel(DateFieldValid);
                return _dateValidDel;
            }
        }
        public static PatternMatchDel PatternMatchValidDel
        {
            get
            {
                if (_patternMatchDel == null) _patternMatchDel = new PatternMatchDel(FieldPatternValid);
                return _patternMatchDel;
            }
        }
        public static CompareFieldsValidDel FieldsCompareValidDel
        {
            get
            {
                if (_compareFieldsValidDel == null) _compareFieldsValidDel = new CompareFieldsValidDel(FieldComparisionValid);
                return _compareFieldsValidDel;
            }
        }
        /* ====================================================================== */

        private static bool RequiredFieldValid(string fieldVal)
        {
            if (!string.IsNullOrEmpty(fieldVal)) return true;

            return false;
        }

        private static bool StringFieldLengthValid(string fieldVal, int min, int max)
        {
            if(fieldVal.Length >= min && fieldVal.Length <= max) return true;

            return false;
        }

        private static bool DateFieldValid(string dateTime, out DateTime valiđateTime)
        {
            if (DateTime.TryParse(dateTime, out valiđateTime)) return true;

            return false;
        }

        private static bool FieldPatternValid(string fieldVal, string regularExpressionPattern)
        {
            Regex regex = new Regex(regularExpressionPattern);

            if (regex.IsMatch(fieldVal)) return true;

            return false;
        }

        private static bool FieldComparisionValid (string field1, string field2) 
        {
            if (field1.Equals(field2)) return true;

            return false;
        }

    }
}