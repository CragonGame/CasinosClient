#if !UNITY_WINRT || UNITY_EDITOR || (UNITY_WP8 &&  !UNITY_WP_8_1)
namespace Newtonsoft.Json
{
  /// <summary>
  /// Indicating whether a property is required.
  /// </summary>
  public enum Required
  {
    /// <summary>
    /// The property is not required. The default state.
    /// </summary>
    Default,
    /// <summary>
    /// The property must be defined in JSON but can be a null value.
    /// </summary>
    AllowNull,
    /// <summary>
    /// The property must be defined in JSON and cannot be a null value.
    /// </summary>
    Always
  }
}
#endif