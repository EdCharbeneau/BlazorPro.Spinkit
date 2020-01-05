using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BlazorPro.Spinkit
{
    public class SpinnerBase : ComponentBase
    {
        [Parameter] public string Color { get; set; }
        [Parameter] public bool Center { get; set; }
        [Parameter] public string Size { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, Object>();

    }
}
