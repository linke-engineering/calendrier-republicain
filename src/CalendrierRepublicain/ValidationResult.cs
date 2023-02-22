namespace Sinistrius.CalendrierRepublicain;


/// <summary>
/// The result of a validation process.
/// </summary>
internal class ValidationResult
{

    #region Constructors

    /// <summary>
    /// Initializes the <see cref="ValidationResult"/>.
    /// </summary>
    /// <param name="success">True if the validation succeeded, otherwise false.</param>
    internal ValidationResult(bool success)
    {
        Success = success;
    }


    /// <summary>
    /// Initializes the <see cref="ValidationResult"/>.
    /// </summary>
    /// <param name="success">True if the validation succeeded, otherwise false.</param>
    /// <param name="message">The error message that was created during the validation.</param>
    internal ValidationResult(bool success, string message) : this(success)
    {
        Message = message;
    }

    #endregion


    #region Properties

    /// <summary>
    /// Determines whether the validation succeeded.
    /// </summary>
    internal bool Success { get; }


    /// <summary>
    /// The error message that was created during the validation.
    /// </summary>
    internal string? Message { get; }

    #endregion

}
