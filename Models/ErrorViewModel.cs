// Niklas Ekstein 910723-3133
using System;
/// <summary>
/// Error view model.
/// </summary>
namespace MvcMovie.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
