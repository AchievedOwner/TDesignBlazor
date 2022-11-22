using Microsoft.AspNetCore.Components.Rendering;

namespace TDesign;
/// <summary>
/// 标签输入框。用于输入文本标签。
/// </summary>
public class TTagInput : BlazorInputComponentBase<string[]>
{
    /// <summary>
    /// 设置标签多时换行显示。
    /// </summary>
    [Parameter] public bool Breakline { get; set; }

    protected override void AddContent(RenderTreeBuilder builder, int sequence)
    {
        builder.CreateElement(sequence, "div", tag =>
        {
            builder.CreateComponent<TInputText<string[]>>(0, attributes: new
            {
                Value,
                ValueExpression,
                ValueChanged,

            });
        },
        new
        {
            @class = HtmlHelper.CreateCssBuilder()
                            .Append("t-input__wrap t-tag-input")
                            .Append("t-tag-input--break-line", Breakline)
        });
    }

    void BuildTag()
    {

    }
}
