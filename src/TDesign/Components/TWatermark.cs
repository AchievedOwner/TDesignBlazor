using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDesign;
[CssClass("t-watermark")]
public class TWatermark:BlazorComponentBase
{
    [Parameter] public OneOf<int, string> Height { get; set; } = "100%";

    [Parameter][EditorRequired]public string Content { get; set; }

    protected override void AddContent(RenderTreeBuilder builder, int sequence)
    {
        builder.CreateElement(sequence, "div", attributes: new
        {
            style = HtmlHelper.CreateStyleBuilder()
                            .Append("height", Height.Match(value => $"{value}px", str => str))
        });

        builder.CreateElement(sequence + 1, "div", attributes: new
        {
            style=HtmlHelper.CreateStyleBuilder().Append("position: absolute; inset: 0px;")
            .Append("width: 100%; height: 100%")
            .Append("pointer-events: none")
            .Append("pointer-events: none")
            .Append("background-size:194px")
            .Append("background-repeat: repeat")
            .Append("background-image:url(\"data:image/png;base64,\")")
        });
    }

    protected override void BuildStyle(IStyleBuilder builder)
    {
        builder.Append($"position: relative; overflow: hidden; width: 100%;");
    }
}
