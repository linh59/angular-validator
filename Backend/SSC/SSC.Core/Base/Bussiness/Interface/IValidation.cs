using SSC.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Base.Bussiness.Interface
{
    /// <summary>
    /// Interface xác định tính đúng đắn của dữ liệu
    /// </summary>
    public interface IValidation
    {
        /// <summary>
        /// Tin nhắn khi validate sai
        /// </summary>
        string ErrorMessage { get; set; }
        /// <summary>
        /// Hàm validate
        /// </summary>
        /// <param name="value">Giá trị cần Validate</param>
        /// <returns>True nếu <paramref name="value"/> đúng với điều kiện</returns>
        bool Validate(object value);

        /// <summary>
        /// Lấy type của validation
        /// </summary>
        /// <returns><see cref="ValidationType"></returns>
        ValidationType GetValidationType();
    }
    /// <summary>
    /// Interface xác định tính đúng đắn của dữ liệu Generic
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu cần xác định tính đúng đắn</typeparam>
    public interface IValidation<T> : IValidation
    {
        T RootValue { get; }
        /// <summary>
        /// Hàm validate
        /// </summary>
        /// <param name="value">Giá trị cần Validate</param>
        /// <returns>True nếu <paramref name="value"/> đúng với điều kiện</returns>
        bool Validate(T value);
    }
}
