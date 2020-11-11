


using SSC.Core.Base.Bussiness.Interface;
using SSC.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SSC.Core.Bussiness
{
    /// <summary>
    /// Validation cho toán tử so sánh
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu cần so sánh</typeparam>
    [Serializable]
    public sealed class CompareValidation<T> : IValidation<T>, IValidation where T : IComparable, IComparable<T>
    {
        #region Implementations
        public string ErrorMessage { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Khởi tạo validation cho toán tử so sánh
        /// </summary>
        /// <param name="rootValue">Giá trị gốc cần so sánh</param>
        /// <param name="type">Kiểu so sánh</param>
        /// <param name="msg">Message cho người dùng trong trường hợp nhập sai</param>
        public CompareValidation(T rootValue, ComparisonType type, string msg = "You input wrong value")
        {
            RootValue = rootValue;
            ComparisonType = type;
            ErrorMessage = msg;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Giá trị gốc để so sánh
        /// </summary>
        public T RootValue { get; }
        /// <summary>
        /// Kiểu so sánh
        /// </summary>
        public ComparisonType ComparisonType { get; private set; }
        #endregion

        public bool Validate(object value)
        {
            if (value is T val)
            {
                return Validate(val);
            }

            try
            {
                var explicitValue = (T)Convert.ChangeType(value, typeof(T));
                return Validate(explicitValue);
            }
            catch (InvalidCastException)
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter.CanConvertFrom(value.GetType()))
                {
                    var explicitValue = (T)converter.ConvertFrom(value);
                    return Validate(explicitValue);
                }
                return false;
            }
        }
        public bool Validate(T value)
        {
            return ComparisonType switch
            {
                ComparisonType.Equal => RootValue.Equals(value),
                ComparisonType.Greater => RootValue.CompareTo(value) < 0,
                ComparisonType.GreaterOrEqual => RootValue.CompareTo(value) <= 0,
                ComparisonType.Lower => RootValue.CompareTo(value) > 0,
                ComparisonType.LowerOrEqual => RootValue.CompareTo(value) >= 0,
                _ => false,
            };
        }
        public ValidationType GetValidationType() => ComparisonType switch
        {
            ComparisonType.Equal => ValidationType.Equal,
            ComparisonType.Greater => ValidationType.Greater,
            ComparisonType.GreaterOrEqual => ValidationType.GreaterOrEqual,
            ComparisonType.Lower => ValidationType.Lower,
            ComparisonType.LowerOrEqual => ValidationType.LowerOrEqual,
            _ => throw new ArgumentException()
        };
    }


#warning Cần thời gian để test class này, không dùng.
    [Obsolete("Class này chưa được test, tạm thời không nên dùng", true)]
    public sealed class DynamicCompareValidation<T> : IValidation<IEnumerable<T>>, IValidation where T : IComparable, IComparable<T>
    {
        [Obsolete("Class này chưa được test, tạm thời không nên dùng", true)]
        public DynamicCompareValidation(ComparisonType comparisonType, string msg = "You input wrong value")
        {
            ErrorMessage = msg;
            ComparisonType = comparisonType;
        }

        public string ErrorMessage { get; set; }
        public IEnumerable<T> RootValue { get; }

        public ComparisonType ComparisonType { get; private set; }

        public bool Validate(object value)
        {
            if (value is IEnumerable<T> values)
            {
                return Validate(values);
            }
            return false;
        }
        public bool Validate(IEnumerable<T> values)
        {
            if (values.Count() != 2)
            {
                return false;
            }
            return ComparisonType switch
            {
                ComparisonType.Equal => values.First().Equals(values.Last()),
                ComparisonType.Greater => values.First().CompareTo(values.Last()) < 0,
                ComparisonType.GreaterOrEqual => values.First().CompareTo(values.Last()) <= 0,
                ComparisonType.Lower => values.First().CompareTo(values.Last()) > 0,
                ComparisonType.LowerOrEqual => values.First().CompareTo(values.Last()) >= 0,
                _ => false,
            };
        }

        public ValidationType GetValidationType() => ComparisonType switch
        {
            ComparisonType.Equal => ValidationType.Equal,
            ComparisonType.Greater => ValidationType.Greater,
            ComparisonType.GreaterOrEqual => ValidationType.GreaterOrEqual,
            ComparisonType.Lower => ValidationType.Lower,
            ComparisonType.LowerOrEqual => ValidationType.LowerOrEqual,
            _ => throw new ArgumentException()
        };
    }


    /// <summary>
    /// Validation cho trường bắt buộc
    /// </summary>
    [Serializable]
    public sealed class RequiredValidation : IValidation<string>, IValidation
    {
        #region Implementations
        public string ErrorMessage { get; set; }
        public string RootValue { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Khởi tạo validation cho trường bắt buộc
        /// </summary>
        /// <param name="msg">Message cho người dùng trong trường hợp nhập sai</param>
        public RequiredValidation(string msg = "You input wrong value")
        {
            ErrorMessage = msg;
        }
        #endregion

        public bool Validate(object value) => value != null && !string.IsNullOrEmpty(value.ToString());
        public bool Validate(string value) => !string.IsNullOrEmpty(value);

        public ValidationType GetValidationType() => ValidationType.Required;
    }



    /// <summary>
    /// Validation cho người dùng tự định nghĩa
    /// </summary>
    [Serializable]
    public class DefinedValidation : IValidation<string>, IValidation
    {

        #region Implementations
        public string ErrorMessage { get; set; }
        public string RootValue { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Khởi tạo validation người dùng tự định nghĩa
        /// </summary>
        /// <param name="regex">Regex</param>
        /// <param name="errorMessage">Message cho người dùng trong trường hợp nhập sai</param>
        public DefinedValidation(Regex regex, string errorMessage = "You input wrong value")
        {
            ErrorMessage = errorMessage;
            RootValue = regex.ToString();
            //Regex = regex;
        }
        public DefinedValidation(string pattern, string errorMessage = "You input wrong value")
        {
            ErrorMessage = errorMessage;
            RootValue = pattern;
            //Regex = regex;
        }
        #endregion

        #region Public Properties
        [NonSerialized]
        private Regex regex;
        public Regex Regex
        {
            get
            {
                return (regex ??= new Regex(RootValue));
            }
        }
        #endregion

        public virtual bool Validate(object value)
        {
            if (value is string val)
            {
                return Validate(val);
            }
            return false;
        }
        public virtual bool Validate(string value) => string.IsNullOrEmpty(value) || Regex.Match(value).Success;

        public virtual ValidationType GetValidationType() => ValidationType.Regex;
    }

    /// <summary>
    /// Ví dụ cho validation người dùng tự định nghĩa. Bắt đầu bằng kí tự xyz
    /// </summary>
    [Serializable]
    public sealed class StartWithValidation : DefinedValidation
    {
        #region Constructor
        /// <summary>
        /// Khởi tạo Validation bắt đầu bằng ...
        /// </summary>
        /// <param name="startWord">chuỗi bắt đầu</param>
        /// <param name="errorMessage">Tin nhắn khi người dùng nhập sai</param>
        public StartWithValidation(string startWord, string errorMessage = "You input wrong value") : base(new Regex($"^{startWord}.+"), errorMessage)
        {
        }
        #endregion

        public override ValidationType GetValidationType() => ValidationType.StartWith;
    }

    /// <summary>
    /// Ví dụ cho validation người dùng tự định nghĩa. Kêt thúc bằng kí tự xyz
    /// </summary>
    [Serializable]
    public sealed class EndWithValidation : DefinedValidation
    {
        #region Constructor
        /// <summary>
        /// Khởi tạo Validation kết thúc bằng ...
        /// </summary>
        /// <param name="startWord">chuỗi kết thúc</param>
        /// <param name="errorMessage">Tin nhắn khi người dùng nhập sai</param>
        public EndWithValidation(string endWord, string errorMessage = "You input wrong value") : base(new Regex($".+{endWord}$"), errorMessage)
        {
        }
        #endregion

        public override ValidationType GetValidationType() => ValidationType.EndWith;
    }

    /// <summary>
    /// Ví dụ cho validation người dùng tự định nghĩa. Chứa kí tự xyz
    /// </summary>
    /// <example>
    /// <code>
    /// new ContainsValidation("xyz");
    /// </code>
    /// </example>
    [Serializable]
    public sealed class ContainsValidation : DefinedValidation
    {
        #region Constructor
        /// <summary>
        /// Khởi tạo Validation chứa ...
        /// </summary>
        /// <param name="startWord">chuỗi cần check</param>
        /// <param name="errorMessage">Tin nhắn khi người dùng nhập sai</param>
        public ContainsValidation(string containWord, string errorMessage = "You input wrong value") : base(new Regex($".+{containWord}.+"), errorMessage)
        {
        }
        #endregion

        public override ValidationType GetValidationType() => ValidationType.Contains;
    }

    [Serializable]
    public sealed class FutureValidation : IValidation<DateTime>, IValidation
    {
        public FutureValidation(string errorMessage = "You input wrong value")
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
        public DateTime RootValue { get => DateTime.Now; }

        public bool Validate(object value)
        {
            if (value is DateTime dateTime)
            {
                return Validate(dateTime);
            }
            else if (value is string dateString)
            {
                if (string.IsNullOrEmpty(dateString))
                {
                    return true;
                }
                else if (DateTime.TryParse(dateString, out dateTime))
                {
                    return Validate(dateTime);
                }
            }
            return false;
        }
        public bool Validate(DateTime value) => value > RootValue;

        public ValidationType GetValidationType() => ValidationType.Future;
    }


    [Serializable]
    public sealed class PastValidation : IValidation<DateTime>, IValidation
    {
        public PastValidation(string errorMessage = "You input wrong value")
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
        public DateTime RootValue { get => DateTime.Now; }
        public bool Validate(object value)
        {
            if (value is DateTime dateTime)
            {
                return Validate(dateTime);
            }
            else if (value is string dateString)
            {
                if (string.IsNullOrEmpty(dateString))
                {
                    return true;
                }
                else if (DateTime.TryParse(dateString, out dateTime))
                {
                    return Validate(dateTime);
                }
            }
            return false;
        }
        public bool Validate(DateTime value) => value < RootValue;

        public ValidationType GetValidationType() => ValidationType.Past;
    }

    [Serializable]
    public sealed class IntegerValidation : IValidation
    {
        public IntegerValidation(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
        public ValidationType GetValidationType() => ValidationType.Integer;
        public bool Validate(object value) => string.IsNullOrEmpty(value.ToString()) || int.TryParse(value.ToString(), out var _);
    }

    [Serializable]
    public sealed class DayOfWeekValidation : IValidation
    {
        public IEnumerable<DayOfWeek> DayOfWeeks { get; }
        public string ErrorMessage { get; set; }

        public ValidationType GetValidationType() => throw new NotImplementedException();
        public bool Validate(object value)
        {
            if (value is DateTime dateTime)
            {
                return DayOfWeeks.Contains(dateTime.DayOfWeek);
            }
            else if (value is string stringVal && DateTime.TryParse(stringVal, out dateTime))
            {
                return DayOfWeeks.Contains(dateTime.DayOfWeek);
            }
            return false;
        }

        public DayOfWeekValidation(IEnumerable<DayOfWeek> dayOfWeeks, string errorMessage = General.DayOfWeekDefaultErrorMessage)
        {
            DayOfWeeks = dayOfWeeks;
            ErrorMessage = errorMessage;
        }
    }

    /// <summary>
    /// Nhóm các toán tử validate
    /// </summary>
    [Serializable]
    public sealed class ValidationGroup : IValidation
    {
        #region Implementations
        public IValidation this[int index] => _validations.ElementAt(index);
        public int Count => _validations.Count;

        public GroupType GroupType { get; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Private Field
        private readonly List<IValidation> _validations;
        #endregion

        #region Constructor
        /// <summary>
        /// Khởi tạo nhóm các validation
        /// </summary>
        /// <param name="groupType">Kiểu nhóm [hoặc] [và]</param>
        /// <param name="validations">Các thành phần cần được nhóm</param>
        public ValidationGroup(GroupType groupType, params IValidation[] validations)
        {
            GroupType = groupType;
            _validations = validations.ToList();
        }

        /// <summary>
        /// Khởi tạo nhóm các validation
        /// </summary>
        /// <param name="groupType">Kiểu nhóm [hoặc] [và]</param>
        /// <param name="validations">Các thành phần cần được nhóm</param>
        public ValidationGroup(GroupType groupType, ICollection<IValidation> validations)
        {
            GroupType = groupType;
            _validations = validations.ToList();
        }

        /// <summary>
        /// Khởi tạo nhóm các validation
        /// </summary>
        /// <param name="groupType">Kiểu nhóm [hoặc] [và]</param>
        /// <param name="validations">Các thành phần cần được nhóm</param>
        public ValidationGroup(GroupType groupType, IEnumerable<IValidation> validations)
        {
            GroupType = groupType;
            _validations = validations.ToList();
        }
        #endregion

        public bool Validate(object value)
        {
            var (result, msg) = GroupType switch
            {
                GroupType.And => (_validations.All(x => x.Validate(value)), string.Join("\n", _validations.Where(x => !x.Validate(value)).Select(x => x.ErrorMessage))),
                GroupType.Or => (_validations.Any(x => x.Validate(value)), string.Join("\n", _validations.Where(x => !x.Validate(value)).Select(x => x.ErrorMessage))),
                _ => (false, "Bad Ingruments")
            };
            ErrorMessage = msg;
            return result;
        }

        public void Add(IValidation validation)
        {
            _validations.Add(validation);
        }

        public static ValidationGroup operator +(ValidationGroup left, ValidationGroup right)
        {
            left.Add(right);
            return left;
        }

        public ValidationType GetValidationType() => ValidationType.None;
        public List<IValidation> ToList() => _validations;

        public IEnumerable<ValidationConstraints> ToValidationConstraints()
        {
            var items = _validations;
            foreach (var item in items)
            {
                if (item.GetType() != typeof(ValidationGroup))
                {
                    yield return new ValidationConstraints(item);
                }
                else
                {
                    items.AddRange((item as ValidationGroup).ToList());
                }
            }
        }
    }
}
